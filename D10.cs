using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace D10
{
	class Program
	{
		public static void Main(string[] args)
		{
			string[] file = System.IO.File.ReadAllLines(@"..\..\Input.txt");
			List<int> input = file.Select(x => int.Parse(x)).ToList();
			input.Sort();
			input.Insert(0, 0);
			input.Add(input.Last() + 3);
			
			//PrintInput(input);
			
			System.Diagnostics.Stopwatch stopWatch = System.Diagnostics.Stopwatch.StartNew();
			List<int> differences = GetDifferences(input);
			//PrintInput(differences);
			int part1 = differences.Where(x => x == 1).Count() * differences.Where(x => x == 3).Count();
			Console.WriteLine("The number of 1-jolt differences multiplied by the number of 3-jolt differences is {0}", part1);
			Console.WriteLine("Computation time: {0}\n", stopWatch.Elapsed);
			
			stopWatch.Restart();
			Console.WriteLine("The total number of arrangements that connect the charging outlet to your device is {0}", BruteForce(input, 1) + 1);
			Console.WriteLine("Computation time: {0}\n", stopWatch.Elapsed);
			
			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}
		
		public static double BruteForce(List<int> input, int startAt)
		{
			double sum = 0;
			for (int i = startAt; i < input.Count() - 1; i++)
			{
				int val = input[i];
				input.RemoveAt(i);
				if (IsValid(input))
				{
					//PrintInput(input);
					//Console.WriteLine("----------------------");
					sum += BruteForce(input, i) + 1;
//					if (sum % 10 == 0) {
//						Console.WriteLine(sum);
//					}
				}
				input.Insert(i, val);
			}
			return sum;
		}
		
		public static bool IsValid(List<int> input)
		{
			return !GetDifferences(input).Any(x => x > 3);
		}
		
		public static List<int> GetDifferences(List<int> input)
		{
			List<int> differences = new List<int>();
			for (int i = 0; i < input.Count() - 1; i++)
			{
				differences.Add(input[i + 1] - input[i]);
			}
			return differences;
		}
		
		public static void PrintInput(List<int> input)
		{
			List<int> differences = GetDifferences(input);
			Console.Write("(0), ");
			for (int i = 1; i < input.Count() - 1; i++)
			{
				Console.Write("{0,3}, ", input[i]);
			}
			Console.Write("({0})\n", input[input.Count() - 1]);
			
			for (int i = 0; i < differences.Count() - 1; i++)
			{
				Console.Write("{0,3}, ", differences[i]);
			}
			Console.Write("{0, 3}\n", differences[differences.Count() - 1]);
		}
	}
}