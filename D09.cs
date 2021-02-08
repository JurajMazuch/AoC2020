using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace D09
{
	class Program
	{
		public static void Main(string[] args)
		{
			string[] input = System.IO.File.ReadAllLines(@"C:\Users\mazucj\Documents\SharpDevelop Projects\AdventOfCode\AoC2020\D09\Input.txt");
			
			int preamble = 25;
			for (int i = preamble; i < input.Length; i++) {
				if (!IsValid(input, i, preamble))
				{
					Console.WriteLine("The first invalid number is {0}", input[i]);
					Console.WriteLine("the encryption weakness is {0}", EncryptionWeakness(input, int.Parse(input[i])));
					break;
				}	
			}
			
			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}
		
		public static int EncryptionWeakness(string[] input, int nonValidNumber)
		{
			for (int i = 0; i < input.Length; i++)
			{
				int sum = 0;
				int min = int.Parse(input[i]);
				int max = int.Parse(input[i]);
				for (int j = 0; j < input.Length - i; j++)
				{
					int val = int.Parse(input[i + j]);
					sum += val;
					
					if (sum > nonValidNumber)
						break;
					
					if (val > max)
						max = val;
					if (val < min)
						min = val;
					
					if (sum == nonValidNumber)
						return min + max;
				}
			}
			return -1;
		}
		
		public static bool IsValid(string[] input, int index, int preamble)
		{
			for (int i = index - 1; i > index - preamble - 1; i--)
			{
				for (int j = index - 1; j > index - preamble - 1; j--)
				{
					if (i == j)
						continue;
					if(int.Parse(input[i]) + int.Parse(input[j]) == int.Parse(input[index]))
						return true;
				}
			}
			return false;
		}
	}
}