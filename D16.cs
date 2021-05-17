using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace D16
{
	class Program
	{
		public static void Main(string[] args)
		{
			string[] file = System.IO.File.ReadAllLines(@"..\..\Input.txt");
			System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
			
			stopwatch.Start();
			Console.WriteLine(GetInvalid(file.ToList()));
			Console.WriteLine("Computation time: {0}", stopwatch.Elapsed);
			
			stopwatch.Restart();
			Console.WriteLine(Part2(file.ToList()));
			Console.WriteLine("Computation time: {0}", stopwatch.Elapsed);
			
			stopwatch.Stop();
			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}
		
		private static int GetInvalid(List<string> file)
		{
			List<string> rules, myTicket, nearbyTickets;
			Dictionary<string, List<int>> ruleBounds;
			ParseInput(file, out rules, out myTicket, out nearbyTickets, out ruleBounds);
			int[] nearbyTicketValues = Array.ConvertAll(String.Join(",", nearbyTickets.ToArray()).Split(','), int.Parse);
			int sum = 0;
			
			foreach (int val in nearbyTicketValues) {
				bool valid = false;
				foreach (string rule in ruleBounds.Keys)
				{
					if ((val >= ruleBounds[rule][0] && val <= ruleBounds[rule][1]) || (val >= ruleBounds[rule][2] && val <= ruleBounds[rule][3]))
					{
						valid = true;
						break;
					}
				}
				if (!valid)
					sum += val;
			}
			
			return sum;
		}
		
		private static long Part2(List<string> file)
		{
			List<string> rules, myTicket, nearbyTickets;
			Dictionary<string, List<int>> ruleBounds;
			ParseInput(file, out rules, out myTicket, out nearbyTickets, out ruleBounds);
			List<string> validTickets = GetValidTickets(nearbyTickets, ruleBounds);
			
			//initialize possible positions for each rule
			Dictionary<string, List<int>> rulePositions = new Dictionary<string, List<int>>();
			foreach (string rule in ruleBounds.Keys)
			{
				List<int> positions = new List<int>();
				for (int i = 0; i < ruleBounds.Keys.Count; i++)
				{
					positions.Add(i);
				}
				rulePositions.Add(rule, positions);
			}
			
			//check every value of valid tickets against every rule, if not valid, remove position from possible rule positions
			foreach (string ticket in validTickets)
			{
				int[] values = Array.ConvertAll(ticket.Split(','), int.Parse);
				for (int i = 0; i < values.Length; i++)
				{
					int val = values[i];
					foreach (string field in rulePositions.Keys)
					{
						if ((val < ruleBounds[field][0] || val > ruleBounds[field][1]) && (val < ruleBounds[field][2] || val > ruleBounds[field][3]))
						{
							rulePositions[field].Remove(i);
						}
					}
				}
			}
			
			int[] myTicketValues = Array.ConvertAll(myTicket[0].Split(','), int.Parse);
			long result = 1;
			foreach (KeyValuePair<string, int> rulePosition in SolveRulePositions(rulePositions)) {
				//Console.WriteLine("{0} {1}", rulePosition.Key, rulePosition.Value);
				if (rulePosition.Key.StartsWith("departure")){
					result *= myTicketValues[rulePosition.Value];
				}
			}
			return result;
		}
		
		private static Dictionary<string, int> SolveRulePositions(Dictionary<string, List<int>> rulePositions)
		{
			rulePositions = rulePositions.OrderBy(x => x.Value.Count).ToDictionary(x => x.Key, x => x.Value);
			List<string> rules = rulePositions.Keys.ToList();
			int n = rules.Count;
			int[][] M = new int[n][];
			for (int i = 0; i < n; i++) {
				M[i] = new int[n];
			}
			for (int i = 0; i < n; i++) {
				for (int j = 0; j < n; j++) {
					M[i][j] = 0;
				}
			}
			
			for (int i = 0; i < rules.Count; i++) {
				string rule = rules[i];
				foreach (int position in rulePositions[rule]) {
					M[i][position] = 1;
				}
			}
			
			int[] prevRow = new int[n];
			for (int i = 0; i < n; i++) {
				for (int j = 0; j < n; j++) {
					M[i][j] -= prevRow[j];
					prevRow[j] += M[i][j];
				}
			}
			
			Dictionary<string, int> result = new Dictionary<string, int>();
			for (int i = 0; i < n; i++) {
				result.Add(rules[i], M[i].ToList().IndexOf(1));
			}
			
			return result;
		}
		
		private static void ParseInput(List<string> input, out List<string> rules, out List<string> myTicket, out List<string> nearbyTickets, out Dictionary<string, List<int>> rulesDict)
		{
			rules = input.ToList().TakeWhile(x => x.Trim() != string.Empty).ToList();
			input.RemoveRange(0, rules.Count + 2);
			myTicket = input.ToList().TakeWhile(x => x.Trim() != string.Empty).ToList();
			input.RemoveRange(0, myTicket.Count + 2);
			nearbyTickets = input.ToList();
			
			rulesDict = new Dictionary<string, List<int>>();
			System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"(?<rule>[a-z ]+): (?<lb1>\d+)-(?<ub1>\d+) or (?<lb2>\d+)-(?<ub2>\d+)");
			
			foreach (string line in rules)
			{
				System.Text.RegularExpressions.GroupCollection groups = regex.Match(line).Groups;
				List<int> bounds = new List<int>();
				bounds.Add(int.Parse(groups["lb1"].Value));
				bounds.Add(int.Parse(groups["ub1"].Value));
				bounds.Add(int.Parse(groups["lb2"].Value));
				bounds.Add(int.Parse(groups["ub2"].Value));
				rulesDict.Add(groups["rule"].Value, bounds);
			}
		}
		
		public static List<string> GetValidTickets(List<string> nearbyTickets, Dictionary<string, List<int>> ruleBounds)
		{
			List<string> validTickets = new List<string>();
			bool valid = false;
			foreach (string ticket in nearbyTickets)
			{
				int[] nearbyTicketValues = Array.ConvertAll(ticket.Split(','), int.Parse);
				foreach (int val in nearbyTicketValues)
				{
					valid = false;
					foreach (string rule in ruleBounds.Keys)
					{
						if ((val >= ruleBounds[rule][0] && val <= ruleBounds[rule][1]) || (val >= ruleBounds[rule][2] && val <= ruleBounds[rule][3]))
						{
							valid = true;
							break;
						}
					}
					if (!valid)
						break;
				}
				if (valid)
					validTickets.Add(ticket);
			}
			
			return validTickets;
		}
	}
}