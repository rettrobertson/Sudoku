using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Solvers
{
    public abstract class ISolver
    {
        protected ISolver? solver;
        public abstract List<(int, int)>? Solve(int[][] input);
        protected static List<(int, int)>? GetUndos(ISolver? solver, int[][] input)
        {
            List<(int, int)>? undos = new();
            if (solver != null)
            {
                undos = solver.Solve(input);
            }
            return undos;
        }
        protected static void Undo(List<(int, int)> undos, int[][] input)
        {
            foreach((int, int) point in undos)
            {
                input[point.Item1][point.Item2] = 0;
            }
        }
    }
}
