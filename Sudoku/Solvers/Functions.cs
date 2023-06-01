using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// this is a class for static functions that may be used between Solvers.
// It's really just to save space in other files
namespace Sudoku.Solvers
{
    public static class Functions
    {
        public static (int, int) ? FindEmpty(int[][] input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < input.Length; j++)
                {
                    if (input[i][j] == 0)
                    {
                        return (i, j);
                    }
                }
            }
            return null;
        }

        public static bool InRow(int[] input, int num)
        {
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == num)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsValid(int[][] input, int col, int row, int num)
        {
            if (InRow(input[row], num) || InRow(GetCol(input, col), num) || InRow(GetBlock(input, col, row), num))
            {
                return false;
            }
            return true;
        }

        public static List<int> GetValid(int[][] input, int col, int row)
        {
            List<int> result = new();
            if (input[row][col] != 0)
            {
                result.Add(input[row][col]);
                return result;
            }
            for (int i = 1; i <= input.Length; i++)
            {
                if (IsValid(input, col, row, i))
                {
                    result.Add(i);
                }
            }
            return result;
        }

        public static bool Compare(List<int> a, List<int> b)
        {
            var A = a.Except(b).ToList();
            var B = b.Except(a).ToList();
            return !A.Any() && !B.Any();
        }
        public static int[] GetCol(int[][] input, int col)
        {
            int[] result = new int[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                result[i] = input[i][col];
            }
            return result;
        }
        public static int[] GetBlock(int[][] input, int col, int row)
        {
            List<int> result = new();
            double ratio = Math.Sqrt(input.Length);
            int box_col = (int)(col / ratio);
            int box_row = (int)(row / ratio);
            for (int i = (int)(box_row * ratio); i < box_row * ratio + ratio; i++)
            {
                for (int j = (int)(box_col * ratio); j < box_col * ratio + ratio; j++)
                {
                    result.Add(input[i][j]);
                }
            }
            return result.ToArray();
        }

        public static void RemoveFromRow(int[] col, List<int> chars)
        {
            foreach (int i in col)
            {
                chars.Remove(i);
            }
        }
        public static void RemoveFromRow(List<int> col, List<int> chars)
        {
            foreach (int i in col)
            {
                chars.Remove(i);
            }
        }

        public static List<int> GetRowDoubles(int[][] input, int row, int col)
        {
            List<int> returnList = new();
            List<List<int>> columns = new();

            for (int i = 0; i < input.Length; i++)
            {
                if (i == col)
                {
                    continue;
                }

                List<int> temp = GetValid(input, i, row);
                if (temp.Count == 2)
                {
                    foreach (List<int> currents in columns)
                    {
                        if (Compare(temp, currents))
                        {
                            returnList.AddRange(temp);
                        }
                    }
                    columns.Add(temp);
                }
            }
            return returnList;
        }
        public static List<int> GetColDoubles(int[][] input, int row, int col)
        {
            List<int> returnList = new();
            List<List<int>> columns = new();

            for (int i = 0; i < input.Length; i++)
            {
                if (i == row)
                {
                    continue;
                }

                List<int> temp = GetValid(input, col, i);
                if (temp.Count == 2)
                {
                    foreach (List<int> currents in columns)
                    {
                        if (Compare(temp, currents))
                        {
                            returnList.AddRange(temp);
                        }
                    }
                    columns.Add(temp);
                }
            }
            return returnList;
        }
        public static List<int> GetBoxDoubles(int[][] input, int row, int col)
        {
            List<int> returnList = new();
            List<List<int>> columns = new();

            double ratio = Math.Sqrt(input.Length);
            int box_col = (int)(col / ratio);
            int box_row = (int)(row / ratio);
            for (int i = (int)(box_row * ratio); i < box_row * ratio + ratio; i++)
            {
                for (int j = (int)(box_col * ratio); j < box_col * ratio + ratio; j++)
                {
                    if (i == row && j == col)
                    {
                        continue;
                    }

                    List<int> temp = GetValid(input, j, i);
                    if (temp.Count == 2)
                    {
                        foreach (List<int> currents in columns)
                        {
                            if (Compare(temp, currents))
                            {
                                returnList.AddRange(temp);
                            }
                        }
                        columns.Add(temp);
                    }
                }
            }

            return returnList;
        }
    }
}
