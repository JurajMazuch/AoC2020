using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace D06
{
	class Program
	{
		public static void Main(string[] args)
		{
			string[] input = System.IO.File.ReadAllLines(@"C:\Users\mazucj\Documents\SharpDevelop Projects\AdventOfCode\AoC2020\D06\Input.txt");
			
			Console.WriteLine("Part 1: Sum = {0}", Part1(input));
			Console.WriteLine("Part 2: Sum = {0}", Part2(input));
			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}
		
		public static int Part1(string[] input)
		{
			string hlp = System.Text.RegularExpressions.Regex.Replace(string.Join("\n", input), @"^$", "|", System.Text.RegularExpressions.RegexOptions.Multiline);
			string[] groups = hlp.Replace("\n", string.Empty).Split('|');
			return groups.Sum(x => x.Distinct().Count());
		}
		
		public static int Part2(string[] input)
		{
			string hlp = System.Text.RegularExpressions.Regex.Replace(string.Join("\n", input), @"^$", "|", System.Text.RegularExpressions.RegexOptions.Multiline);
			string[] groups = hlp.Split('|');
			
			int count = 0;
			int i = 1;
			foreach (string g in groups) {
				string distAnswers = string.Join("", g.Replace("\n", string.Empty).Distinct());
				foreach (char answer in distAnswers.ToCharArray()) {
					bool allYes = true;
					foreach (string person in g.Split('\n').Where(x => x.Length > 0)) {
						if (!person.Contains(answer.ToString())) {
							allYes = false;
							break;
						}
					}
					if (allYes) 
						count++;
				}
				i++;
			}
			
			return count;
		}
	}
}