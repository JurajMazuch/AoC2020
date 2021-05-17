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
			int part1 = differences.Where(x => x == 1).Count() * differences.Where(x => x == 3).Count();
			Console.WriteLine("The number of 1-jolt differences multiplied by the number of 3-jolt differences is {0}", part1);
			Console.WriteLine("Computation time: {0}\n", stopWatch.Elapsed);
			
			stopWatch.Restart();
			Dictionary<int, UInt64> cache = new Dictionary<int, UInt64>();
			Console.WriteLine("The total number of arrangements that connect the charging outlet to your device is {0}", Part2(input, 0, cache));
			Console.WriteLine("Computation time: {0}\n", stopWatch.Elapsed);
			
			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}
		
		public static UInt64 Part2(List<int> input, int startAt, Dictionary<int, UInt64> cache)
		{
			if (startAt == input.Count - 1)
				return 1;
			
			if (cache.ContainsKey(startAt))
				return cache[startAt];
			
			UInt64 pathCount = 0;
			int i = 1;
			while (startAt + i < input.Count && input[startAt + i] - input[startAt] <= 3)
			{
				pathCount += Part2(input, startAt + i, cache);
				i++;
			}
			
			cache.Add(startAt, pathCount);
			return pathCount;
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
	}
}