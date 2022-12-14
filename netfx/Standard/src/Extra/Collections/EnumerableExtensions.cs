namespace Bearz.Extra.Collections;

public static class EnumerableExtensions
{
    public static bool EqualTo<T>(this IEnumerable<T>? left, IEnumerable<T>? right, IComparer<T> comparer)
    {
        if (left == null && right == null)
            return true;

        if (left == null || right == null)
            return false;

        using var leftEnumerator = left.GetEnumerator();
        using var rightEnumerator = right.GetEnumerator();

        while (true)
        {
            var lNext = leftEnumerator.MoveNext();
            var rNext = rightEnumerator.MoveNext();

            if (!lNext && !rNext)
                return true;

            if (!lNext || !rNext)
                return false;

            var lValue = leftEnumerator.Current;
            var rValue = rightEnumerator.Current;

            if (comparer.Compare(lValue, rValue) != 0)
                return false;
        }
    }

    public static bool EqualTo<T>(this IEnumerable<T>? left, IEnumerable<T>? right, Comparison<T> compare)
    {
        if (left == null && right == null)
            return true;

        if (left == null || right == null)
            return false;

        using var leftEnumerator = left.GetEnumerator();
        using var rightEnumerator = right.GetEnumerator();

        while (true)
        {
            var lNext = leftEnumerator.MoveNext();
            var rNext = rightEnumerator.MoveNext();

            if (!lNext && !rNext)
                return true;

            if (!lNext || !rNext)
                return false;

            var lValue = leftEnumerator.Current;
            var rValue = rightEnumerator.Current;

            if (compare(lValue, rValue) != 0)
                return false;
        }
    }

    public static bool EqualTo<T>(this IEnumerable<T>? left, IEnumerable<T>? right)
    {
        if (left == null && right == null)
            return true;

        if (left == null || right == null)
            return false;

        using var leftEnumerator = left.GetEnumerator();
        using var rightEnumerator = right.GetEnumerator();

        while (true)
        {
            var lNext = leftEnumerator.MoveNext();
            var rNext = rightEnumerator.MoveNext();

            if (!lNext && !rNext)
                return true;

            if (!lNext || !rNext)
                return false;

            var lValue = leftEnumerator.Current;
            var rValue = rightEnumerator.Current;

            if (lValue is IEquatable<T> lEquatable && !lEquatable.Equals(rValue))
                return false;

            if (lValue is IComparable<T> lComparable && lComparable.CompareTo(rValue) != 0)
                return false;

            if (lValue is null && rValue is null)
                continue;

            if (lValue is null || rValue is null)
                return false;

            if (!lValue.Equals(rValue))
                return false;
        }
    }
}