using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace D05
{
	class Program
	{
		public static void Main(string[] args)
		{
			List<string> input = System.IO.File.ReadAllLines(@"C:\Users\mazucj\Documents\SharpDevelop Projects\AdventOfCode\AoC2020\D05\Input.txt").ToList();
			List<Seat> seats = new List<Seat>();
			foreach (string line in input) {
				Seat s = GetSeat(line);
				seats.Add(s);
			}
			
			//part 1
			Console.WriteLine("Highest seat ID: {0}", seats.Max((x)=>x.id));
			
			//part 2
			for (int id = 0; id < seats.Max((x)=>x.id); id++) {
				if (seats.Any((x)=>x.id == id) && !seats.Any((x)=>x.id == id + 1) && seats.Any((x)=>x.id == id + 2))
				{
					Console.WriteLine("My seat: {0}", id + 1);
					break;
				}
			}
			
			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}
		
		public static Seat GetSeat(string code)
		{
			Seat s = new Seat();
			s.row = Decode(code.Substring(0, 7));
			s.col = Decode(code.Substring(7, 3));
			s.id = s.row * 8 + s.col;
			
			return s;
		}
		
		public static int Decode(string code)
		{
			int pow = code.Length - 1;
			int lBound = 0;
			int uBound = (int)Math.Pow(2, code.Length) - 1;
			foreach (char c in code.ToCharArray()) {
				int half = (int)Math.Pow(2, pow);
				switch (c) {
					case 'F':
					case 'L':
						uBound -= half;
						break;
					case 'B':
					case 'R':
						lBound += half;
						break;
				}
				
				pow--;
			}
			return lBound;
		}
		
		public struct Seat
		{
			public int row { get; set; }
			public int col { get; set; }
			public int id { get; set; }
		}
	}
}