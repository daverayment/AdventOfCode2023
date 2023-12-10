namespace Toolbox;

public static class Helpers
{
	public static void Deconstruct<T>(this IEnumerable<T> sequence, out T first, out T second, out T third)
	{
		var array = sequence.ToArray();
		first = array[0]; second = array[1]; third = array[2];
	}

	public static (long Start, long Length)? GetOverlap(
		this (long Start, long Length) interval1, (long Start, long Length) interval2)
	{
		long startMax = Math.Max(interval1.Start, interval2.Start);
		long endMin = Math.Min(interval1.Start + interval1.Length - 1, interval2.Start + interval2.Length - 1);
		if (startMax > endMin) { return null; }

		return (startMax, endMin - startMax + 1);
	}
}