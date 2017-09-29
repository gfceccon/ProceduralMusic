public class Tuple<T1, T2>
{
    public T1 first;
    public T2 second;
    internal Tuple(T1 first, T2 second)
    {
        this.first = first;
        this.second = second;
    }
}