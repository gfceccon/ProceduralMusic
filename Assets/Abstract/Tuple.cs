public class Tuple<T1, T2>
{
    public T1 value;
    public T2 next;
    internal Tuple(T1 first, T2 second)
    {
        value = first;
        next = second;
    }
}