using Sudoku.Solvers;

namespace Sudoku
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            OpenFileDialog dialog = new();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string file = dialog.FileName;
                (char[] chars, char[][] puzzle) = FileHandler.ReadFile(file);
                int[][] temp = FileHandler.CharToInt(chars, puzzle);
                NakedSingle single = new();
                Guesser guesser = new(single);
                // NakedDouble nakedDouble = new(single);
                // Guesser guesser = new(nakedDouble);
                List<(int, int)>? undos = guesser.Solve(temp);
                // the nakedDouble was having a bug every once in a blue moon on the larger puzzles that I couldn't 
                // figure out what was happening. Using nakedDouble will sometimes be faster but also might be wrong on very
                // large puzzles
                puzzle = FileHandler.IntToChar(chars, temp);
                FileHandler.Output(undos == null, chars, puzzle, "Output.txt");
            }
        }
    }
}
