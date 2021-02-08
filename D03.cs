using System;
using System.Collections.Generic;
using System.Linq;

namespace D03
{
	class Program
	{
		public static void Main(string[] args)
		{
			List<string> input = System.IO.File.ReadAllLines(@"C:\Users\mazucj\Documents\SharpDevelop Projects\AdventOfCode\AoC2020\D03\Input.txt").ToList();
			
			Console.WriteLine("Part 1: {0}", CountTrees(input, 3, 1));
			
			Console.WriteLine("Part 2: {0}", CountTrees(input, 1, 1) * CountTrees(input, 3, 1) * CountTrees(input, 5, 1) * CountTrees(input, 7, 1) * CountTrees(input, 1, 2));
			
			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}
		
		private static int CountTrees(List<string> input, int right, int down)
		{
			int count = 0;
			int pos = 0;
			foreach (string line in input.Where((x, i) => i % down == 0)) {
				if (line[pos] == '#') {
					count++;
				}
				pos = (pos + right) % line.Length;
			}
			return count;
		}
	}
}