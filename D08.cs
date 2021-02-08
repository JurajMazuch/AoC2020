using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace D08
{
	class Program
	{
		public static void Main(string[] args)
		{
			string[] input = System.IO.File.ReadAllLines(@"C:\Users\mazucj\Documents\SharpDevelop Projects\AdventOfCode\AoC2020\D08\Input.txt");
			
			DataTable instructions = GetInstructions(input);
			
			Console.WriteLine("Part 1: {0}", Part1(instructions.Copy()));
			Console.WriteLine("Part 2: {0}", Part2(instructions.Copy()));
			
			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}
		
		public static int Part2(DataTable instructions)
		{
			int acc = 0;
			DataRow end = instructions.NewRow();
			end["Instruction"] = "end";
			end["Used"] = false;
			instructions.Rows.Add(end);
			for (int i = 0; i < instructions.Rows.Count; i++)
			{
				DataTable dt = instructions.Copy();
				switch (dt.Rows[i]["Instruction"].ToString())
				{
					case "jmp":
						dt.Rows[i]["Instruction"] = "nop";
						break;
					case "nop":
						dt.Rows[i]["Instruction"] = "jmp";
						break;
					default:
						continue;
				}
				
				try
				{
					acc = FindEnd(dt);
				} catch
				{
					continue;
				}
			}
			
			return acc;
		}
		
		public static int FindEnd(DataTable instructions)
		{
			int accumulator = 0;
			int index = 0;
			do
			{
				DataRow instruction = instructions.Rows[index];
				if ((bool)instruction["Used"]) {
					throw(new System.Exception("infinite loop"));
				}
				switch (instruction["Instruction"].ToString()) {
					case "nop":
						index++;
						instruction["Used"] = true;
						break;
					case "acc":
						switch (instruction["Sign"].ToString()) {
							case "+":
								accumulator += (int)instruction["number"];
								break;
							case "-":
								accumulator -= (int)instruction["number"];
								break;
						}
						index++;
						instruction["Used"] = true;
						break;
					case "jmp":
						switch (instruction["Sign"].ToString()) {
							case "+":
								index += (int)instruction["number"];
								break;
							case "-":
								index -= (int)instruction["number"];
								break;
						}
						instruction["Used"] = true;
						break;
					case "end":
						return accumulator;
				}
			} while (true);
		}
		
		public static int Part1(DataTable instructions)
		{
			int accumulator = 0;
			int index = 0;
			do
			{
				DataRow instruction = instructions.Rows[index];
				if ((bool)instruction["Used"]) {
					return accumulator;
				}
				switch (instruction["Instruction"].ToString()) {
					case "nop":
						index++;
						instruction["Used"] = true;
						break;
					case "acc":
						switch (instruction["Sign"].ToString()) {
							case "+":
								accumulator += (int)instruction["number"];
								break;
							case "-":
								accumulator -= (int)instruction["number"];
								break;
						}
						index++;
						instruction["Used"] = true;
						break;
					case "jmp":
						switch (instruction["Sign"].ToString()) {
							case "+":
								index += (int)instruction["number"];
								break;
							case "-":
								index -= (int)instruction["number"];
								break;
						}
						instruction["Used"] = true;
						break;
				}
			} while (true);
		}
		
		public static DataTable GetInstructions(string[] input)
		{
			bool b = false;
			DataTable instructions = new DataTable();
			instructions.Columns.Add(new DataColumn("Instruction", System.Type.GetType("System.String")));
			instructions.Columns.Add(new DataColumn("Sign", System.Type.GetType("System.String")));
			instructions.Columns.Add(new DataColumn("Number", System.Type.GetType("System.Int32")));
			instructions.Columns.Add(new DataColumn("Used", b.GetType()));
			
			foreach (string line in input) {
				DataRow row = instructions.NewRow();
				row["Instruction"] = line.Substring(0, 3);
				row["Sign"] = line.Substring(4, 1);
				row["Number"] = int.Parse(line.Substring(5));
				row["Used"] = false;
				instructions.Rows.Add(row);
			}
			
			return instructions;
		}
	}
}