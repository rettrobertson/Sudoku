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
                // int[][] justInCase = temp;
                NakedSingle single = new();
                NakedDouble nakedDouble = new(single);
                Guesser guesser = new(nakedDouble);
                List<(int, int)>? undos = guesser.Solve(temp);
                // the nakedDouble was having a bug every once in a blue moon on the larger puzzles that I couldn't 
                // figure out what was happening. This code can run and not implement nakedDouble if that bug happens,
                // but will double the time if it needs to brute force it which could be really long.
                /*if (undos != null)
                {
                    guesser = new(single);
                    List<(int, int)>? otherUndos = guesser.Solve(justInCase);
                    puzzle = FileHandler.IntToChar(chars, justInCase);
                    FileHandler.Output(otherUndos == null, chars, puzzle, "Output.txt");
                    return;
                }*/
                puzzle = FileHandler.IntToChar(chars, temp);
                FileHandler.Output(undos == null, chars, puzzle, "Output.txt");
            }
        }
    }
}
