using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

public enum LoadingMode { Eager, Lazy, LazyExpanding };

public enum AccessMode { FIFO, LIFO, Circular };

public class Pool<T> : IDisposable
{
    private bool isDisposed;
    private Func<T> factory;
    private LoadingMode loadingMode;
    private IItemStore itemStore;
    private int size;
    private int count;
    private Semaphore sync;

    public Pool(int size, Func<T> factory)
        : this(size, factory, LoadingMode.Lazy, AccessMode.FIFO)
    {
    }

    public Pool(int size, Func<T> factory,
        LoadingMode loadingMode, AccessMode accessMode)
    {
        if (size <= 0)
            throw new ArgumentOutOfRangeException("size", size,
                "Argument 'size' must be greater than zero.");
        if (factory == null)
            throw new ArgumentNullException("factory");

        this.size = size;
        this.factory = factory;
        sync = new Semaphore(size, size);
        this.loadingMode = loadingMode;
        this.itemStore = CreateItemStore(accessMode, size);
        if (loadingMode == LoadingMode.Eager)
        {
            PreloadItems();
        }
    }

    public T Acquire()
    {
        sync.WaitOne();
        switch (loadingMode)
        {
            case LoadingMode.Eager:
                return AcquireEager();
            case LoadingMode.Lazy:
                return AcquireLazy();
            default:
                Debug.Assert(loadingMode == LoadingMode.LazyExpanding,
                    "Unknown LoadingMode encountered in Acquire method.");
                return AcquireLazyExpanding();
        }
    }

    public void Release(T item)
    {
        lock (itemStore)
        {
            itemStore.Store(item);
        }
        sync.Release();
    }

    public void Dispose()
    {
        if (isDisposed)
        {
            return;
        }
        isDisposed = true;
        if (typeof(IDisposable).IsAssignableFrom(typeof(T)))
        {
            lock (itemStore)
            {
                while (itemStore.Count > 0)
                {
                    IDisposable disposable = (IDisposable)itemStore.Fetch();
                    disposable.Dispose();
                }
            }
        }
        sync.Close();
    }

    #region Acquisition

    private T AcquireEager()
    {
        lock (itemStore)
        {
            return itemStore.Fetch();
        }
    }

    private T AcquireLazy()
    {
        lock (itemStore)
        {
            if (itemStore.Count > 0)
            {
                return itemStore.Fetch();
            }
        }
        Interlocked.Increment(ref count);
        return factory();
    }

    private T AcquireLazyExpanding()
    {
        bool shouldExpand = false;
        if (count < size)
        {
            int newCount = Interlocked.Increment(ref count);
            if (newCount <= size)
            {
                shouldExpand = true;
            }
            else
            {
                // Another thread took the last spot - use the store instead
                Interlocked.Decrement(ref count);
            }
        }
        if (shouldExpand)
        {
            return factory();
        }
        else
        {
            lock (itemStore)
            {
                return itemStore.Fetch();
            }
        }
    }

    private void PreloadItems()
    {
        for (int i = 0; i < size; i++)
        {
            T item = factory();
            itemStore.Store(item);
        }
        count = size;
    }

    #endregion

    #region Collection Wrappers

    interface IItemStore
    {
        T Fetch();
        void Store(T item);
        int Count { get; }
    }

    private IItemStore CreateItemStore(AccessMode mode, int capacity)
    {
        switch (mode)
        {
            case AccessMode.FIFO:
                return new QueueStore(capacity);
            case AccessMode.LIFO:
                return new StackStore(capacity);
            default:
                Debug.Assert(mode == AccessMode.Circular,
                    "Invalid AccessMode in CreateItemStore");
                return new CircularStore(capacity);
        }
    }

    class QueueStore : Queue<T>, IItemStore
    {
        public QueueStore(int capacity)
            : base(capacity)
        {
        }

        public T Fetch()
        {
            return Dequeue();
        }

        public void Store(T item)
        {
            Enqueue(item);
        }
    }

    class StackStore : Stack<T>, IItemStore
    {
        public StackStore(int capacity)
            : base(capacity)
        {
        }

        public T Fetch()
        {
            return Pop();
        }

        public void Store(T item)
        {
            Push(item);
        }
    }

    class CircularStore : IItemStore
    {
        private List<Slot> slots;
        private int freeSlotCount;
        private int position = -1;

        public CircularStore(int capacity)
        {
            slots = new List<Slot>(capacity);
        }

        public T Fetch()
        {
            if (Count == 0)
                throw new InvalidOperationException("The buffer is empty.");

            int startPosition = position;
            do
            {
                Advance();
                Slot slot = slots[position];
                if (!slot.IsInUse)
                {
                    slot.IsInUse = true;
                    --freeSlotCount;
                    return slot.Item;
                }
            } while (startPosition != position);
            throw new InvalidOperationException("No free slots.");
        }

        public void Store(T item)
        {
            Slot slot = slots.Find(s => object.Equals(s.Item, item));
            if (slot == null)
            {
                slot = new Slot(item);
                slots.Add(slot);
            }
            slot.IsInUse = false;
            ++freeSlotCount;
        }

        public int Count
        {
            get { return freeSlotCount; }
        }

        private void Advance()
        {
            position = (position + 1) % slots.Count;
        }

        class Slot
        {
            public Slot(T item)
            {
                this.Item = item;
            }

            public T Item { get; private set; }
            public bool IsInUse { get; set; }
        }
    }

    #endregion

    public bool IsDisposed
    {
        get { return isDisposed; }
    }
}