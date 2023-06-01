﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Solvers
{
    public class NakedDouble : ISolver
    {
        public NakedDouble() { }
        public NakedDouble(ISolver solver)
        {
            this.solver = solver;
        }
        public override List<(int, int)>? Solve(int[][] input)
        {
            List<(int, int)>? undos = GetUndos(solver, input);
            if (undos == null) { return undos; }

            List<(int, int)> returnList = new();
            int i = 0;
            int j = 0;
            while (i < input.Length)
            {
                while (j < input.Length)
                {
                    if (input[i][j] == 0)
                    {
                        List<int> chars = new();
                        for (int k = 1; k <= input.Length; k++)
                        {
                            chars.Add(k);
                        }
                        Functions.RemoveFromRow(input[i], chars);
                        Functions.RemoveFromRow(Functions.GetCol(input, j), chars);
                        Functions.RemoveFromRow(Functions.GetBlock(input, j, i), chars);
                        if (chars.Count > 1)
                        {
                            List<int> row = Functions.GetRowDoubles(input, i, j);

                            Functions.RemoveFromRow(row, chars);
                        }
                        if (chars.Count > 1)
                        {
                            List<int> col = Functions.GetColDoubles(input, i, j);
                            Functions.RemoveFromRow(col, chars);

                        }
                        if (chars.Count > 1)
                        {
                            List<int> box = Functions.GetBoxDoubles(input, i, j);
                            Functions.RemoveFromRow(box, chars);

                        }
                        if (chars.Count == 1)
                        {
                            input[i][j] = chars[0];
                            returnList.Add((i, j));
                            i = 0;
                            j = -1;
                        }
                    }
                    j++;
                }
                i++;
                j = 0;
            }
            (int, int)? temp = Functions.FindEmpty(input);
            if (temp == null)
            {
                return null;
            }
            else
            {
                Undo(undos, input);
                return returnList;
            }
        }
    }
}
