using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace D17
{
	class Program
	{
		public static void Main(string[] args)
		{
			string[] file = System.IO.File.ReadAllLines(@"..\..\Input.txt");
			System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
			
			stopwatch.Start();
			Console.WriteLine(Part1(file));
			Console.WriteLine("Computation time: {0}", stopwatch.Elapsed);
			
			stopwatch.Restart();
			Console.WriteLine(Part2(file));
			Console.WriteLine("Computation time: {0}", stopwatch.Elapsed);
			
			stopwatch.Stop();
			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}
		
		private static long Part1(string[] file)
		{
			//create a cube from input file
			char[][][] input = new char[1][][];
			input[0] = new char[file.Length][];
			for (int i = 0; i < file.Length; i++)
			{
				input[0][i] = new char[file[0].Length];
			}
			
			for (int i = 0; i < input[0].Length; i++)
			{
				for (int j = 0; j < input[0][0].Length; j++)
				{
					input[0][i][j] = file[i][j];
				}
			}
			
			for (int i = 0; i < 6; i++)
			{
				input = SimulateCycle(input);
//				PrintMatrix(input);
//				Console.WriteLine();
			}
			
			long activeCount = 0;
			for (int z = 0; z < input.Length; z++)
			{
				for (int y = 0; y < input[z].Length; y++)
				{
					for (int x = 0; x < input[z][y].Length; x++)
					{
						if (input[z][y][x] == '#')
							activeCount++;
					}
				}
			}
			
			return activeCount;
		}
		
		private static char[][][] SimulateCycle(char[][][] input)
		{
			//put border of inactive elements around the input
			var inputWrapped = WrapMatrix(input, 2);			
			var semiStep = CopyArray(inputWrapped);
			
			//most left, right, up, down, back and forth values will be used to extract only necessary range for the next step (trim all inactive spaces) 
			int minX, maxX, minY, maxY, minZ, maxZ;
			minZ = inputWrapped.Length;
			minY = inputWrapped[0].Length;
			minX = inputWrapped[0][0].Length;
			maxX = maxY = maxZ = 0;
			//loop elements that can change a state (might have more than 0 active neighbours)
			for (int z = 1; z < inputWrapped.Length - 1; z++)
			{
				for (int y = 1; y < inputWrapped[0].Length - 1; y++)
				{
					for (int x = 1; x < inputWrapped[0][0].Length - 1; x++)
					{
						//count active neighbours
						int activeNeighboursCount = 0;
						for (int i = -1; i <= 1; i++)
						{
							for (int j = -1; j <= 1; j++)
							{
								for (int k = -1; k <= 1; k++)
								{
									if (i == 0 && j == 0 && k == 0)
										continue;
									
									if (inputWrapped[z + i][y + j][x + k] == '#')
										activeNeighboursCount++;
								}
							}
						}
						
						//apply rules
						switch (inputWrapped[z][y][x])
						{
							case '#':
								if (activeNeighboursCount != 2 && activeNeighboursCount != 3)
									semiStep[z][y][x] = '.';
								break;
							case '.':
								if (activeNeighboursCount == 3)
									semiStep[z][y][x] = '#';
								break;
						}
						
						//get bounds of active part of the space
						if (semiStep[z][y][x] == '#')
						{
							if (z < minZ)
								minZ = z;
							if (z > maxZ)
								maxZ = z;
							if (y < minY)
								minY = y;
							if (y > maxY)
								maxY = y;
							if (x < minX)
								minX = x;
							if (x > maxX)
								maxX = x;
						}
					}
				}
			}
			
			//create smaller array for the next step (trim inactive space)
			char[][][] nextStep = new char[maxZ - minZ + 1][][];
			for (int z = 0; z <= maxZ - minZ; z++)
			{
				nextStep[z] = new char[maxY - minY + 1][];
				for (int y = 0; y <= maxY - minY; y++)
				{
					nextStep[z][y] = new char[maxX - minX + 1];
					for (int x = 0; x <= maxX - minX; x++)
					{
						nextStep[z][y][x] = semiStep[z + minZ][y + minY][x + minX];
					}
				}
			}
			
			return nextStep;
		}
		
		private static char[][][] WrapMatrix(char[][][] M, int borderSize)
		{
			int m = M.Length + 2 * borderSize;
			int n = M[0].Length + 2 * borderSize;
			int o = M[0][0].Length + 2 * borderSize;
			char[][][] MWrapped = new char[m][][];
			for (int i = 0; i < m; i++)
			{
				MWrapped[i] = new char[n][];
				for (int j = 0; j < n; j++) {
					MWrapped[i][j] = new char[o];
				}
			}
			
			for (int i = 0; i < MWrapped.Length; i++)
			{
				for (int j = 0; j < MWrapped[0].Length; j++)
				{
					for (int k = 0; k < MWrapped[0][0].Length; k++)
					{
						if (i > borderSize - 1 && i < MWrapped.Length - borderSize && j > borderSize - 1 && j < MWrapped[0].Length - borderSize && k > borderSize - 1 && k < MWrapped[0][0].Length - borderSize) {
							MWrapped[i][j][k] = M[i - borderSize][j - borderSize][k - borderSize];
						}
						else
						{
							MWrapped[i][j][k] = '.';
						}
					}
				}
			}
			return MWrapped;
		}
		
		private static char[][][] CopyArray(char[][][] source)
		{
			var dest = new char[source.Length][][];
			
			for (int z = 0; z < source.Length; z++) {
				dest[z] = new char[source[0].Length][];
				for (int y = 0; y < source[0].Length; y++) {
					dest[z][y] = new char[source[0][0].Length];
					Array.Copy(source[z][y], dest[z][y], source[z][y].Length);
				}
			}
			
			return dest;
		}
		
		private static long Part2(string[] file)
		{
			//create a tenzor from input file
			char[][][][] input = new char[1][][][];
			input[0] = new char[1][][];
			input[0][0] = new char[file.Length][];
			for (int i = 0; i < file.Length; i++)
			{
				input[0][0][i] = new char[file[0].Length];
			}
			
			for (int y = 0; y < input[0][0].Length; y++)
			{
				for (int x = 0; x < input[0][0][0].Length; x++)
				{
					input[0][0][y][x] = file[y][x];
				}
			}
			
			for (int i = 0; i < 6; i++)
			{
				input = SimulateCycle(input);
//				PrintMatrix(input);
//				Console.WriteLine();
			}
			
			long activeCount = 0;
			for (int w = 0; w < input.Length; w++)
			{
				for (int z = 0; z < input[w].Length; z++)
				{
					for (int y = 0; y < input[w][z].Length; y++)
					{
						for (int x = 0; x < input[w][z][y].Length; x++)
						{
							if (input[w][z][y][x] == '#')
								activeCount++;
						}
					}
				}
			}
			
			return activeCount;
		}
		
		private static char[][][][] SimulateCycle(char[][][][] input)
		{
			//put border of inactive elements around the input
			var inputWrapped = WrapMatrix(input, 2);
			var semiStep = CopyArray(inputWrapped);
			
			int minX, maxX, minY, maxY, minZ, maxZ, minW, maxW;
			minW = inputWrapped.Length;
			minZ = inputWrapped[0].Length;
			minY = inputWrapped[0][0].Length;
			minX = inputWrapped[0][0][0].Length;
			maxX = maxY = maxZ = maxW = 0;
			for (int w = 1; w < inputWrapped.Length - 1; w++)
			{
				for (int z = 1; z < inputWrapped[0].Length - 1; z++)
				{
					for (int y = 1; y < inputWrapped[0][0].Length - 1; y++)
					{
						for (int x = 1; x < inputWrapped[0][0][0].Length - 1; x++)
						{
							//count active neighbours
							int activeNeighboursCount = 0;
							for (int i = -1; i <= 1; i++)
							{
								for (int j = -1; j <= 1; j++)
								{
									for (int k = -1; k <= 1; k++)
									{
										for (int l = -1; l <= 1; l++)
										{
											if (i == 0 && j == 0 && k == 0 && l == 0)
												continue;
											
											if (inputWrapped[w + i][z + j][y + k][x + l] == '#')
												activeNeighboursCount++;
										}
									}
								}
							}
							
							//apply rules
							switch (inputWrapped[w][z][y][x])
							{
								case '#':
									if (activeNeighboursCount != 2 && activeNeighboursCount != 3)
										semiStep[w][z][y][x] = '.';
									break;
								case '.':
									if (activeNeighboursCount == 3)
										semiStep[w][z][y][x] = '#';
									break;
							}
							
							//get bounds of active part of the space
							if (semiStep[w][z][y][x] == '#')
							{
								if (w < minW)
									minW = w;
								if (w > maxW)
									maxW = w;
								if (z < minZ)
									minZ = z;
								if (z > maxZ)
									maxZ = z;
								if (y < minY)
									minY = y;
								if (y > maxY)
									maxY = y;
								if (x < minX)
									minX = x;
								if (x > maxX)
									maxX = x;
							}
						}
					}
				}
			}
			
			//create smaller array for the next step (trim inactive space)
			char[][][][] nextStep = new char[maxW - minW + 1][][][];
			for (int w = 0; w <= maxW - minW; w++)
			{
				nextStep[w] = new char[maxZ - minZ + 1][][];
				for (int z = 0; z <= maxZ - minZ; z++)
				{
					nextStep[w][z] = new char[maxY - minY + 1][];
					for (int y = 0; y <= maxY - minY; y++)
					{
						nextStep[w][z][y] = new char[maxX - minX + 1];
						for (int x = 0; x <= maxX - minX; x++)
						{
							nextStep[w][z][y][x] = semiStep[w + minW][z + minZ][y + minY][x + minX];
						}
					}
				}
			}
			
			return nextStep;
		}
		
		private static char[][][][] WrapMatrix(char[][][][] M, int borderSize)
		{
			int m = M.Length + 2 * borderSize;
			int n = M[0].Length + 2 * borderSize;
			int o = M[0][0].Length + 2 * borderSize;
			int p = M[0][0][0].Length + 2 * borderSize;
			char[][][][] MWrapped = new char[m][][][];
			for (int w = 0; w < m; w++)
			{
				MWrapped[w] = new char[n][][];
				for (int z = 0; z < n; z++)
				{
					MWrapped[w][z] = new char[o][];
					for (int y = 0; y < o; y++)
					{
						MWrapped[w][z][y] = new char[p];
					}
				}
			}
			
			for (int w = 0; w < MWrapped.Length; w++)
			{
				for (int z = 0; z < MWrapped[0].Length; z++)
				{
					for (int y = 0; y < MWrapped[0][0].Length; y++)
					{
						for (int x = 0; x < MWrapped[0][0][0].Length; x++)
						{
							if (w > borderSize - 1 && w < MWrapped.Length - borderSize && z > borderSize - 1 && z < MWrapped[0].Length - borderSize && y > borderSize - 1 && y < MWrapped[0][0].Length - borderSize && x > borderSize - 1 && x < MWrapped[0][0][0].Length - borderSize) {
								MWrapped[w][z][y][x] = M[w - borderSize][z - borderSize][y - borderSize][x - borderSize];
							}
							else
							{
								MWrapped[w][z][y][x] = '.';
							}
						}
					}
				}
			}
			
			return MWrapped;
		}
		
		private static char[][][][] CopyArray(char[][][][] source)
		{
			var dest = new char[source.Length][][][];
			for (int w = 0; w < source.Length; w++)
			{
				dest[w] = new char[source[0].Length][][];
				for (int z = 0; z < source[0].Length; z++)
				{
					dest[w][z] = new char[source[0][0].Length][];
					for (int y = 0; y < source[0][0].Length; y++)
					{
						dest[w][z][y] = new char[source[0][0][0].Length];
						Array.Copy(source[w][z][y], dest[w][z][y], source[w][z][y].Length);
					}
				}
			}
			
			return dest;
		}
		
		private static void PrintMatrix(char[][][] M)
		{
			for (int z = 0; z < M.Length; z++)
			{
				Console.WriteLine("z = {0}", z - (int)(M.Length / 2));
				for (int y = 0; y < M[z].Length; y++)
				{
					for (int x = 0; x < M[z][y].Length; x++)
					{
						Console.Write("{0}", M[z][y][x]);
					}
					Console.WriteLine();
				}
				Console.WriteLine();
			}
		}
		private static void PrintMatrix(char[][][][] M)
		{
			for (int w = 0; w < M.Length; w++)
			{
				for (int z = 0; z < M[w].Length; z++)
				{
					Console.WriteLine("z = {0}, w = {1}", z - (int)(M[0].Length / 2), w - (int)(M.Length / 2));
					for (int y = 0; y < M[w][z].Length; y++)
					{
						for (int x = 0; x < M[w][z][y].Length; x++)
						{
							Console.Write("{0}", M[w][z][y][x]);
						}
						Console.WriteLine();
					}
					Console.WriteLine();
				}
			}
		}
	}
}