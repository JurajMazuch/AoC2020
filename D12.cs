using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace D12
{
	class Program
	{
		public static void Main(string[] args)
		{
			string[] file = System.IO.File.ReadAllLines(@"..\..\Input.txt");
			
			Point p = Part1(file);
			Console.WriteLine("Part1: Manhattan distance between current location and the ship's starting position is {0}", Math.Abs(p.X) + Math.Abs(p.Y));
			
			Point q = Part2(file);
			Console.WriteLine("Part2: Manhattan distance between current location and the ship's starting position is {0}", Math.Abs(q.X) + Math.Abs(q.Y));
			
			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}
		
		public static Point Part2(string[] instructions)
		{
			Point ship = new Point();
			Point waypoint = new Point();
			Point direction = new Point();
			waypoint.X = 10;
			waypoint.Y = 1;
			double rotation = 0;
			foreach (string instruction in instructions)
			{
				char action = instruction[0];
				int val = int.Parse(instruction.Substring(1));
				switch (action) {
					case 'N':
						waypoint.Y += val;
						break;
					case 'S':
						waypoint.Y -= val;
						break;
					case 'E':
						waypoint.X += val;
						break;
					case 'W':
						waypoint.X -= val;
						break;
					case 'L':
						rotation = Math.PI * val / 180.0;
						direction.X = waypoint.X - ship.X;
						direction.Y = waypoint.Y - ship.Y;
						waypoint.X = (long)(ship.X + direction.X * Math.Round(Math.Cos(rotation)) - direction.Y * Math.Round(Math.Sin(rotation)));
						waypoint.Y = (long)(ship.Y + direction.X * Math.Round(Math.Sin(rotation)) + direction.Y * Math.Round(Math.Cos(rotation)));
						break;
					case 'R':
						rotation = Math.PI * val / 180.0;
						direction.X = waypoint.X - ship.X;
						direction.Y = waypoint.Y - ship.Y;
						waypoint.X = (long)(ship.X + direction.X * Math.Round(Math.Cos(rotation)) + direction.Y * Math.Round(Math.Sin(rotation)));
						waypoint.Y = (long)(ship.Y - direction.X * Math.Round(Math.Sin(rotation)) + direction.Y * Math.Round(Math.Cos(rotation)));
						break;
					case 'F':
						direction.X = waypoint.X - ship.X;
						direction.Y = waypoint.Y - ship.Y;
						ship.X += direction.X * val;
						ship.Y += direction.Y * val;
						waypoint.X = ship.X + direction.X;
						waypoint.Y = ship.Y + direction.Y;
						break;
				}
			}
			return ship;
		}
		
		public static Point Part1(string[] instructions)
		{
			Point ship = new Point();
			int direction = 0;
			foreach (string instruction in instructions)
			{
				char action = instruction[0];
				int val = int.Parse(instruction.Substring(1));
				switch (action) {
					case 'N':
						ship.Y += val;
						break;
					case 'S':
						ship.Y -= val;
						break;
					case 'E':
						ship.X += val;
						break;
					case 'W':
						ship.X -= val;
						break;
					case 'L':
						direction += val;
						break;
					case 'R':
						direction -= val;
						break;
					case 'F':
						direction %= 360;
						direction += direction < 0 ? 360 : 0;
						
						switch (direction) {
							case 0:
								ship.X += val;
								break;
							case 90:
								ship.Y += val;
								break;
							case 180:
								ship.X -= val;
								break;
							case 270:
								ship.Y -= val;
								break;
							default:
								throw new Exception("Non orthogonal direction: " + direction);
						}
						break;
				}
			}
			return ship;
		}
		
		public struct Point
		{
			public long X {get; set;}
			public long Y {get; set;}
		}
	}
}