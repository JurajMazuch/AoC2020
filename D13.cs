using System;
using System.Data;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace D13
{
	class Program
	{
		public static void Main(string[] args)
		{
			string[] file = System.IO.File.ReadAllLines(@"..\..\Input.txt");
			
			System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
			stopwatch.Start();
			Console.WriteLine("The ID of the earliest bus you can take to the airport multiplied by the number of minutes you'll need to wait for that bus is {0}", Part1(file));
			Console.WriteLine("Computation time: {0}", stopwatch.Elapsed);
			
			stopwatch.Restart();
			Console.WriteLine("The earliest timestamp such that all of the listed bus IDs depart at offsets matching their positions in the list is {0}", Part2(file));
			Console.WriteLine("Computation time: {0}", stopwatch.Elapsed);
			stopwatch.Stop();
			
			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}
		
		public static int Part1(string[] input)
		{
			int myTime = int.Parse(input[0]);
			string[] buses = input[1].Split(',');
			int min = int.Parse(buses[0]);
			int myBus = 0;
			
			foreach (string bus in buses) {
				if (bus == "x")
					continue;
				
				int busNum = int.Parse(bus);
				int nextBus = busNum - (myTime % busNum);
				if (nextBus < min) {
					min = nextBus;
					myBus = busNum;
				}
			}
			return myBus * min;
		}
		
		public static long Part2(string[] input)
		{
			input = input[1].Split(',');
			
			long t = 0;
			long step = long.Parse(input[0]);
			for (int i = 1; i < input.Length; i++)
			{
				if (input[i] == "x")
					continue;
				
				long busNum = long.Parse(input[i]);
				while ((t + i) % busNum != 0)
					t += step;
				
				step *= busNum;
			}
			
			return t;
		}
	}
}