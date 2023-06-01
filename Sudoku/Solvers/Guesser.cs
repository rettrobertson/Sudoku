using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace Sudoku.Solvers
{
    public class Guesser : ISolver
    {
        public Guesser(ISolver solver)
        {
            this.solver = solver;
        }

        public Guesser() { }
        public override List<(int, int)>? Solve(int[][] input)
        {
            List<(int, int)>? undos = GetUndos(solver, input);
            if (undos == null) { return undos; }

            (int, int)? temp = Functions.FindEmpty(input);
            int row;
            int col;
            if (temp == null) { return null; }
            else
            {
                col = temp.Value.Item2;
                row = temp.Value.Item1;
            }
            List<int> ints = Functions.GetValid(input, col, row);
            foreach (int i in ints)
            {
                bool success = Guess(input, row, col, i);
                if (success) { return null; }

            }
            Undo(undos, input);
            undos = new List<(int, int)>() { (row, col) };
            return undos;
        }

        private bool Guess(int[][] input, int row, int col, int i)
        {
            input[row][col] = i ;
            List<(int, int)>? undos = Solve(input);
            if (undos == null) { return true; }
            foreach ((int, int) point in undos)
            {
                input[point.Item1][point.Item2] = 0;
            }
            return false;
        }
    }
}