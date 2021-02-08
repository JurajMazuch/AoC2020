using System;
using System.Collections.Generic;
using System.Linq;

namespace D01
{
	class Program
	{
		public static void Main(string[] args)
		{
			List<string> input = System.IO.File.ReadAllLines(@"C:\Users\mazucj\Documents\SharpDevelop Projects\AdventOfCode\AoC2020\AoC2020\Input.txt").ToList();
			
			
			Console.WriteLine(Part2(input, input, input));
			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}
		
		private static double Part1(List<string> l1, List<string> l2)
		{
			foreach (string s1 in l1) {
				foreach (string s2 in l2) {
					if (int.Parse(s1) + int.Parse(s2) == 2020) {
						return(int.Parse(s1) * int.Parse(s2));
					}
				}
			}
			return 0;
		}
		
		private static double Part2(List<string> l1, List<string> l2, List<string> l3)
		{
			foreach (string s1 in l1) {
				foreach (string s2 in l2) {
					foreach (string s3 in l3) {
						if (int.Parse(s1) + int.Parse(s2) + int.Parse(s3) == 2020) {
							return(int.Parse(s1) * int.Parse(s2) * int.Parse(s3));
						}
					}
				}
			}
			return 0;
		}
	}
}