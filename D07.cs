using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace D07
{
	class Program
	{
		public static void Main(string[] args)
		{
			string[] input = System.IO.File.ReadAllLines(@"C:\Users\mazucj\Documents\SharpDevelop Projects\AdventOfCode\AoC2020\D07\Input.txt");
			Dictionary<string, Bag> Bags = new Dictionary<string, Bag>();
			
			foreach (string line in input) {
				Bag b = GetBag(line);
				if (!Bags.ContainsKey(b.color)) {
					Bags.Add(b.color, b);
				}
			}
			
			int counter = 0;
			foreach (string color in Bags.Keys) {
				if (ContainsColor(Bags, Bags[color], "shiny gold"))
				    counter++;
			}
			Console.WriteLine("Part1: {0} bag colors can eventually contain at least one shiny gold bag.", counter);
			Console.WriteLine("Part2: {0} individual bags are required inside your single shiny gold bag.", SumContents(Bags, Bags["shiny gold"]));
			
			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}
		
		public static int SumContents(Dictionary<string, Bag> bags, Bag bag)
		{
			int sum = 0;
			foreach (string content in bag.contents.Keys) {
				sum += bag.contents[content] * (SumContents(bags, bags[content]) + 1);
			}
			return sum;
		}
		
		public static bool ContainsColor(Dictionary<string, Bag> bags, Bag bag, string color)
		{
			foreach (string content in bag.contents.Keys) {
				if (content == color) {
					return true;
				}
				if (ContainsColor(bags, bags[content], color)) {
					return true;
				}
			}
			return false;
		}
		
		public static Bag GetBag(string str)
		{
			Bag bag = new Bag();
			bag.color = str.Substring(0, str.IndexOf("contain")).Replace("bags", string.Empty).Trim();
			
			Dictionary<string, int> contents = new Dictionary<string, int>();
			foreach (string content in str.Substring(str.IndexOf("contain") + "contain".Length).Replace("bags", string.Empty).Replace("bag", string.Empty).Trim().Split(',')) {
				if (content.Contains("no other"))
					break;
				
				int number = int.Parse(System.Text.RegularExpressions.Regex.Match(content.Replace(".", string.Empty).Trim(), @"\d+").Value);
				string color = System.Text.RegularExpressions.Regex.Match(content.Replace(".", string.Empty).Trim(), @"[a-z]+ [a-z]+").Value;
				contents.Add(color, number);
			}
			bag.contents = contents;
			return bag;
		}
		
		public struct Bag
		{
			public string color {get; set;}
			public Dictionary<string, int> contents {get; set;}
		}
	}
}