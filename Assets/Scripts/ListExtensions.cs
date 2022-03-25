using System.Collections.Generic;

public static class ListExtensions
{
    public static List<T> Append<T>(this List<T> list, T type)
    {
        list.Add(type);
        return list;
    }
}
