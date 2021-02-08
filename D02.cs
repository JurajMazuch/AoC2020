using System;
using System.Collections.Generic;
using System.Linq;

namespace D02
{
	class Program
	{
		public static void Main(string[] args)
		{
			List<string> input = System.IO.File.ReadAllLines(@"C:\Users\mazucj\Documents\SharpDevelop Projects\AdventOfCode\AoC2020\D02\Input.txt").ToList();
			int counter = 0;
			foreach (string line in input) {
				if (IsValid2(line)) {
					counter++;
				}
			}
			Console.WriteLine(counter);
			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}
		
		private static bool IsValid1(string line)
		{
			string password, letter, bounds;
			int lowBound, upBound;
			
			string[] parts = line.Split(' ');
			bounds = parts[0];
			letter = parts[1].Replace(":", string.Empty);
			password = parts[2];
			lowBound = int.Parse(bounds.Split('-')[0]);
			upBound = int.Parse(bounds.Split('-')[1]);
			
			return (password.Length - password.Replace(letter, string.Empty).Length) >= lowBound && (password.Length - password.Replace(letter, string.Empty).Length) <= upBound;
		}
		
		private static bool IsValid2(string line)
		{
			string password, letter, positions;
			int pos1, pos2;
			
			string[] parts = line.Split(' ');
			positions = parts[0].Trim();
			letter = parts[1].Replace(":", string.Empty).Trim();
			password = parts[2].Trim();
			pos1 = int.Parse(positions.Split('-')[0]) - 1;
			pos2 = int.Parse(positions.Split('-')[1]) - 1;
			
			return password[pos1].ToString() == letter ^ password[pos2].ToString() == letter;
		}
	}
}