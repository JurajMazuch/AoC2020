using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace D11
{
	class Program
	{
		public static void Main(string[] args)
		{
			string[] file = System.IO.File.ReadAllLines(@"..\..\Input.txt");
			
			//Console.WriteLine("{0} seats end up occupied.", Part1(file));
			Console.WriteLine("{0} seats end up occupied.", Part2(file));
			
			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}
		
		public static int Part2(string[] input)
		{
			string[] inputNew = new string[input.Length];
			
			int rowCount = input.Length;
			int colCount = input[0].Length;
			do
			{
				Array.Copy(input, inputNew, input.Length);
				for (int r = 0; r < rowCount; r++)
				{
					for (int c = 0; c < colCount; c++)
					{
						int i, j;
						char N, W, S, E, NW, NE, SW, SE, X;
						N = W = S = E = NW = NE = SW = SE = '0';
						X = input[r][c];
						
						i = r - 1;
						j = c;
						while (i >= 0) {
							N = input[i][j];
							if (N != '.') {
								break;
							}
							i--;
						}
						
						i = r;
						j = c - 1;
						while (j >= 0) {
							W = input[i][j];
							if (W != '.') {
								break;
							}
							j--;
						}
						
						i = r + 1;
						j = c;
						while (i < rowCount) {
							S = input[i][j];
							if (S != '.') {
								break;
							}
							i++;
						}
						
						i = r;
						j = c + 1;
						while (j < colCount) {
							E = input[i][j];
							if (E != '.') {
								break;
							}
							j++;
						}
						
						i = r - 1;
						j = c - 1;
						while (j >= 0 && i >= 0) {
							NW = input[i][j];
							if (NW != '.') {
								break;
							}
							i--;
							j--;
						}
						
						i = r + 1;
						j = c + 1;
						while (j < colCount && i < rowCount) {
							SE = input[i][j];
							if (SE != '.') {
								break;
							}
							i++;
							j++;
						}
						
						i = r + 1;
						j = c - 1;
						while (j >= 0 && i < rowCount) {
							SW = input[i][j];
							if (SW != '.') {
								break;
							}
							i++;
							j--;
						}
						
						i = r - 1;
						j = c + 1;
						while (j < colCount && i >= 0) {
							NE = input[i][j];
							if (NE != '.') {
								break;
							}
							i--;
							j++;
						}
						
						StringBuilder strBuilder = new StringBuilder();
						strBuilder.Append(N).Append(W).Append(S).Append(E).Append(NW).Append(SW).Append(NE).Append(SE);
						int occupiedCount = 8 - strBuilder.ToString().Replace("#", string.Empty).Length;
						
						//If a seat is empty (L) and there are no occupied seats adjacent to it, the seat becomes occupied.
						if (X == 'L' && occupiedCount == 0) {
							StringBuilder s = new StringBuilder(inputNew[r]);
							s[c] = '#';
							inputNew[r] = s.ToString();
						}
						//If a seat is occupied (#) and four or more seats adjacent to it are also occupied, the seat becomes empty.
						if (X == '#' && occupiedCount > 4) {
							StringBuilder s = new StringBuilder(inputNew[r]);
							s[c] = 'L';
							inputNew[r] = s.ToString();
						}
					}
				}
				
				if (input.SequenceEqual(inputNew))
				{
					break;
				}
				else
				{
					Array.Copy(inputNew, input, input.Length);
				}
			}
			while (true);
			
			return CountOccupied(input);
		}
		
		public static int Part1(string[] input)
		{
			string[] inputNew = new string[input.Length];
			
			int rowCount = input.Length;
			int colCount = input[0].Length;
			do
			{
				Array.Copy(input, inputNew, input.Length);
				for (int r = 0; r < rowCount; r++)
				{
					for (int c = 0; c < colCount; c++)
					{
						char N, W, S, E, NW, NE, SW, SE, X;
						N = W = S = E = NW = NE = SW = SE = '0';
						X = input[r][c];
						if (r > 0)
						{
							N = input[r - 1][c];
							if (c > 0)
								NW = input[r - 1][c - 1];
							if (c < colCount - 1)
								NE = input[r - 1][c + 1];
						}
						if (c > 0)
						{
							W = input[r][c - 1];
						}
						if (r < rowCount - 1)
						{
							S = input[r + 1][c];
							if (c > 0)
								SW = input[r + 1][c - 1];
							if (c < colCount - 1)
								SE = input[r + 1][c + 1];
						}
						if (c < colCount - 1)
						{
							E = input[r][c + 1];
						}
						
						StringBuilder strBuilder = new StringBuilder();
						strBuilder.Append(N).Append(W).Append(S).Append(E).Append(NW).Append(SW).Append(NE).Append(SE);
						int occupiedCount = 8 - strBuilder.ToString().Replace("#", string.Empty).Length;
						
						//If a seat is empty (L) and there are no occupied seats adjacent to it, the seat becomes occupied.
						if (X == 'L' && occupiedCount == 0) {
							StringBuilder s = new StringBuilder(inputNew[r]);
							s[c] = '#';
							inputNew[r] = s.ToString();
						}
						//If a seat is occupied (#) and four or more seats adjacent to it are also occupied, the seat becomes empty.
						if (X == '#' && occupiedCount > 3) {
							StringBuilder s = new StringBuilder(inputNew[r]);
							s[c] = 'L';
							inputNew[r] = s.ToString();
						}
					}
				}
				
				if (input.SequenceEqual(inputNew))
				{
					break;
				}
				else
				{
					Array.Copy(inputNew, input, input.Length);
				}
			}
			while (true);
			
			return CountOccupied(input);
		}
		
		public static int CountOccupied(string[] input)
		{
			int sum = 0;
			foreach (string line in input) {
				sum += line.Length - line.Replace("#", string.Empty).Length;
			}
			return sum;
		}
	}
}