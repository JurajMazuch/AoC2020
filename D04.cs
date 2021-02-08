using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace D04
{
	class Program
	{
		public static void Main(string[] args)
		{
			int count1 = 0;
			int count2 = 0;
			List<string> input = System.IO.File.ReadAllLines(@"C:\Users\mazucj\Documents\SharpDevelop Projects\AdventOfCode\AoC2020\D04\Input.txt").ToList();
			List<Passport> passports = GetPassports(input);
			foreach (Passport pass in passports) {
				if (pass.IsValid1()) {
					count1++;
					//Console.WriteLine("byr: {0}\niyr: {1}\neyr: {2}\nhgt: {3}\nhcl: {4}\necl: {5}\npid: {6}\ncid: {7}\n", pass.byr, pass.iyr, pass.eyr, pass.hgt, pass.hcl, pass.ecl, pass.pid, pass.cid);
				}
				if (pass.IsValid2()) {
					count2++;
					//Console.WriteLine("byr: {0}\niyr: {1}\neyr: {2}\nhgt: {3}\nhcl: {4}\necl: {5}\npid: {6}\ncid: {7}\n", pass.byr, pass.iyr, pass.eyr, pass.hgt, pass.hcl, pass.ecl, pass.pid, pass.cid);
				}
			}
			
			Console.WriteLine("Part 1: {0}\nPart 2: {1}", count1, count2);
			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}
		
		public static List<Passport> GetPassports(List<string> input)
		{
			List<Passport> passports = new List<Passport>();
			Passport p = new Passport();
			foreach (string line in input) {
				if (line != string.Empty)
				{
					string[] data = line.Split(' ');
					foreach (string item in data) {
						string key = item.Split(':')[0];
						string val = item.Split(':')[1];
						switch (key)
						{
							case "byr":
								p.byr = int.Parse(val);
								break;
							case "iyr":
								p.iyr = int.Parse(val);
								break;
							case "eyr":
								p.eyr = int.Parse(val);
								break;
							case "hgt":
								p.hgt = val;
								break;
							case "hcl":
								p.hcl = val;
								break;
							case "ecl":
								p.ecl = val;
								break;
							case "pid":
								p.pid = val;
								break;
							case "cid":
								p.cid = val;
								break;
						}
					}
				}
				else
				{
					passports.Add(p);
					p = new Passport();
				}
			}
			return passports;
		}
	}
	
	struct Passport
	{
		public int? byr { get; set; }
		public int? iyr { get; set; }
		public int? eyr { get; set; }
		public string hgt { get; set; }
		public string hcl { get; set; }
		public string ecl { get; set; }
		public string pid { get; set; }
		public string cid { get; set; }
		
		public bool IsValid1()
		{
			return (byr.HasValue && iyr.HasValue && eyr.HasValue && hgt != null && hcl != null && ecl != null && pid != null);
		}
		
		public bool IsValid2()
		{
			bool byrValid = false;
			if (byr.HasValue && byr >= 1920 && byr <= 2002) {
				byrValid = true;
			}
			
			bool iyrValid = false;
			if (iyr.HasValue && iyr >= 2010 && iyr <= 2020) {
				iyrValid = true;
			}
			
			bool eyrValid = false;
			if (eyr.HasValue && eyr >= 2020 && eyr <= 2030) {
				eyrValid = true;
			}
			
			bool hgtValid = false;
			if (hgt != null && hgt.Length > 3) {
				int lBound, uBound;
				int val = int.Parse(hgt.Substring(0, hgt.Length - 2));
				string unit = hgt.Substring(hgt.Length - 2, 2);
				switch (unit) {
					case "in":
						lBound = 59;
						uBound = 76;
						break;
					case "cm":
						lBound = 150;
						uBound = 193;
						break;
					default:
						lBound = 1;
						uBound = 0;
						break;
				}
				if (val >= lBound && val <= uBound) {
					hgtValid = true;
				}
			}
			
			bool hclValid = false;
			System.Text.RegularExpressions.Regex r = new System.Text.RegularExpressions.Regex(@"#[0-9a-f]{6}");
			if (hcl != null && hcl == r.Match(hcl).Value)
				hclValid = true;
			
			bool eclValid = false;
			r = new System.Text.RegularExpressions.Regex(@"amb|blu|brn|gry|grn|hzl|oth");
			if (ecl != null && ecl == r.Match(ecl).Value)
				eclValid = true;
			
			bool pidValid = false;
			r = new System.Text.RegularExpressions.Regex(@"\d{9}");
			if (pid != null && pid == r.Match(pid).Value)
				pidValid = true;
			
			return (byrValid && iyrValid && eyrValid && hgtValid && hclValid && eclValid && pidValid);
		}
	}
}