using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace D14
{
	class Program
	{
		public static void Main(string[] args)
		{
			string[] file = System.IO.File.ReadAllLines(@"..\..\Input.txt");
			
			Console.WriteLine("The sum of all values left in memory after initialization version 1 completes is {0}", Part1(file));
			Console.WriteLine("The sum of all values left in memory after initialization version 2 completes is {0}", Part2(file));
			
			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}
		
		private static ulong Part1(string[] input)
		{
			System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"mem\[(?<index>\d+)\] = (?<value>\d+)");
			string mask;
			UInt64 zeros = 0;
			UInt64 ones = 0;
			Dictionary<UInt64, UInt64> memory = new Dictionary<UInt64, UInt64>();
			
			foreach (string line in input)
			{
				if (line.StartsWith("mask = "))
				{
					mask = line.Replace("mask = ", string.Empty);
					zeros = Convert.ToUInt64(mask.Replace("X", "1"), 2);
					ones = Convert.ToUInt64(mask.Replace("X", "0"), 2);
				}
				else
				{
					System.Text.RegularExpressions.Match match = regex.Match(line);
					UInt64 index = uint.Parse(match.Groups["index"].Value);
					UInt64 val = uint.Parse(match.Groups["value"].Value);
					
					if (memory.ContainsKey(index))
						memory[index] = (ones | val) & zeros;
					else
						memory.Add(index, (ones | val) & zeros);
				}
			}
			
			return SumValues(memory);
		}
		
		private static ulong Part2(string[] input)
		{
			System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"mem\[(?<index>\d+)\] = (?<value>\d+)");
			string mask = string.Empty;
			UInt64 ones = 0;
			Dictionary<UInt64, UInt64> memory = new Dictionary<UInt64, UInt64>();
			
			foreach (string line in input)
			{
				if (line.StartsWith("mask = "))
				{
					mask = line.Replace("mask = ", string.Empty);
					ones = Convert.ToUInt64(mask.Replace("X", "0"), 2);
				}
				else
				{
					System.Text.RegularExpressions.Match match = regex.Match(line);
					UInt64 index = uint.Parse(match.Groups["index"].Value);
					UInt64 val = uint.Parse(match.Groups["value"].Value);
					
					//do bit OR, prepend zeros
					string address = ("000000000000000000000000000000000000" + Convert.ToString(Convert.ToInt64(ones | index), 2));
					address = address.Substring(address.Length - 36);
					
					//Copy X to propper indexes
					for (int i = 1; i <= mask.Length ; i++)
					{
						if (mask[mask.Length - i] == 'X')
						{
							StringBuilder s = new StringBuilder(address);
							s[address.Length - i] = 'X';
							address = s.ToString();
						}
					}
					
					List<string> addresses = new List<string>();
					addresses = GetAddressPermutations(address, addresses);
					
					foreach (string a in addresses)
					{
						UInt64 i = Convert.ToUInt64(Convert.ToInt64(a, 2));
						
						if (memory.ContainsKey(i))
							memory[i] = val;
						else
							memory.Add(i, val);
					}
					
				}
			}
			
			return SumValues(memory);
		}
		
		private static List<string> GetAddressPermutations(string address, List<string> permutations)
		{
			int xIndex = address.IndexOf('X');
			if (xIndex == -1)
			{
				permutations.Add(address);
				return permutations;
			}
			
			StringBuilder s = new StringBuilder(address);
			
			s[xIndex] = '0';
			address = s.ToString();
			GetAddressPermutations(address, permutations);
			
			s[xIndex] = '1';
			address = s.ToString();
			GetAddressPermutations(address, permutations);
			
			return permutations;
		}
		
		private static UInt64 SumValues(Dictionary<UInt64, UInt64> dict)
		{
			UInt64 sum = 0;
			foreach (UInt64 val in dict.Values)
				sum += val;
				
			return sum;
		}
	}
}