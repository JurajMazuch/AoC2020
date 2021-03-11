using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace D15
{
	class Program
	{
		public static void Main(string[] args)
		{
			string[] file = System.IO.File.ReadAllLines(@"..\..\Input.txt");
			List<int> input = file[0].Split(',').ToList().Select(x => int.Parse(x)).ToList();
			System.Diagnostics.Stopwatch stopWatch = new System.Diagnostics.Stopwatch();
			
			stopWatch.Start();
			Console.WriteLine("Given starting numbers {0}, the 2020th number spoken will be {1}", file[0], MemoryGame(input, 2020));
			Console.WriteLine("Computation Time: {0}", stopWatch.Elapsed);
			
			stopWatch.Restart();
			Console.WriteLine("Given starting numbers {0}, the 30000000th number spoken will be {1}", file[0], MemoryGame(input, 30000000));
			Console.WriteLine("Computation Time: {0}", stopWatch.Elapsed);
			
			stopWatch.Stop();
			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}
		
		private static int MemoryGame(List<int> input, int n)
		{
			Dictionary<int, List<int>> numbers = new Dictionary<int, List<int>>();
			int number = -1;
			
			for (int i = 0; i < input.Count; i++)
			{
				number = input[i];
				UpdateDictionary(numbers, number, i);
			}
			
			for (int i = input.Count; i < n; i++)
			{
				if (numbers[number].Count == 1)
				{
					number = 0;
				}
				else
				{
					int indexCount = numbers[number].Count;
					int lastIndex = numbers[number][indexCount - 1];
					int previousIndex = numbers[number][indexCount - 2];
					number = lastIndex - previousIndex;
				}
				UpdateDictionary(numbers, number, i);
			}
			
			return number;
		}
		
		private static void UpdateDictionary(Dictionary<int, List<int>> numbers, int number, int i)
		{
			if (numbers.ContainsKey(number))
				numbers[number].Add(i);
			else
			{
				List<int> l = new List<int>();
				l.Add(i);
				numbers.Add(number, l);
			}
		}
		
		private static int MemoryGame_Slow(List<int> numbers, int n)
		{
			numbers.Reverse();
			int previouslySpoken;
			do
			{
				previouslySpoken = numbers.IndexOf(numbers.First(), 1);
				
				if (previouslySpoken == -1)
					numbers.Insert(0, 0);
				else
					numbers.Insert(0, previouslySpoken);
				
			} while (numbers.Count() < n);
			
			return numbers.First();
		}
	}
}