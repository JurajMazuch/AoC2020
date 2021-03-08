using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace D13
{
	class Program
	{
		public static void Main(string[] args)
		{
			string[] file = System.IO.File.ReadAllLines(@"..\..\Input.txt");
			
			//Console.WriteLine(Part1(file));
			System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
			stopwatch.Start();
			Console.WriteLine(Part2(file));
			Console.WriteLine("Computation time: {0}", stopwatch.Elapsed);
			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}
		
		public static long Part2(string[] input)
		{
			List<string> buses = input[1].Split(',').ToList();
			List<int> busList = buses.Where(x => x != "x").Select(x => int.Parse(x)).ToList();
			busList.Sort();
			busList.Reverse();
			int step = busList[0];
			int stepIndex = buses.IndexOf(step.ToString());
			long t = 100000000000000;
			//long t = step - buses.IndexOf(step.ToString());
			long i = (long)(t / step);
			
			do
			{
				bool found = true;
				t = step * i - stepIndex;
				
				foreach (int bus in busList)
				{
					int index = buses.IndexOf(bus.ToString());
					if (t % bus != (bus - index) % bus)
					{
						found = false;
						break;
					}
				}
				if (found)
					break;
				i++;
			} while (true);
			
			return t;
		}
		
		public static long LCM(int a, int b)
		{
			return a * b / GCD(a, b);
		}
		
		public static long LCM(int[] a)
		{
			long lcm = a[0];
			for (int i = 1; i < a.Length; i++) {
				lcm = lcm * a[i] / GCD(lcm, a[i]);
			}
			return lcm;
		}
		
		public static long GCD(long a, long b)
		{
			while (a != b)
			{
				if (a > b)
					a = a - b;
				else
					b = b - a;
			}
			return a;
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
	}
}