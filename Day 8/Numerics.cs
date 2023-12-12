using System.Numerics;

namespace Toolbox
{
	public static class Numerics
	{
		public static BigInteger LeastCommonMultiple(BigInteger[] nums)
		{
			BigInteger result = nums[0];
			for (int i = 1; i < nums.Length; i++)
			{
				result = LeastCommonMultiple(result, nums[i]);
			}
			return result;
		}

		public static BigInteger LeastCommonMultiple(BigInteger a, BigInteger b)
		{
			return a / GreatestCommonDivisor(a, b) * b;
		}

		public static BigInteger GreatestCommonDivisor(BigInteger a, BigInteger b)
		{
			while (b != 0)
			{
				BigInteger temp = b;
				b = a % b;
				a = temp;
			}
			return a;
		}
	}
}
