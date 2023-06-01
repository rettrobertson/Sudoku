using Sudoku.Solvers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Sudoku
{
    public static class FileHandler
    {
        public static (char[], char[][]) ReadFile(string file)
        {
            try
            {
                IEnumerable<string> temp = File.ReadLines(file);
                List<string> lines = new(temp);
                int size = int.Parse(lines[0].Replace(" ", string.Empty));
                char[] chars = lines[1].Replace(" ", string.Empty).ToCharArray();
                char[][] returnArray = new char[size][];
                for (int i = 2; i < size + 2; i++)
                {
                    returnArray[i - 2] = lines[i].Replace(" ", string.Empty).ToCharArray();
                }
                return (chars, returnArray);
            }
            catch
            {
                System.Console.WriteLine("File either doesn't exist or isn't formatted correctly.");
            return (new char[] { '1', '2', '3', '4' }, new char[][] { new char[] { '-', '-', '-', '1' }, new char[] { '1', '-', '-', '4' }, new char[] { '-', '-', '4', '-' }, new char[] { '-', '2', '-', '-' } });
        }
    }
        public static int[][] CharToInt(char[] chars, char[][] puzzle)
        {
            Dictionary<char, int> temp = new();
            for (int i = 0; i < chars.Length; i++)
            {
                temp.Add(chars[i], i + 1);
            }
            temp.Add('-', 0);
            int[][] returnArray = new int[puzzle.Length][];
            for (int i = 0; i < returnArray.Length; i++)
            {
                returnArray[i] = new int[returnArray.Length];
                for (int j = 0; j < returnArray.Length; j++)
                {
                    returnArray[i][j] = temp[puzzle[i][j]];
                }
            }
            return returnArray;
        }

        public static char[][] IntToChar(char[] chars, int[][] puzzle)
        {
            char[][] returnArray = new char[puzzle.Length][];
            for (int i = 0; i < returnArray.Length; i++)
            {
                returnArray[i] = new char[returnArray.Length];
                for (int j = 0; j < returnArray.Length; j++)
                {
                    if (puzzle[i][j] == 0)
                    {
                        returnArray[i][j] = '-';
                    }
                    else
                    {
                        returnArray[i][j] = chars[puzzle[i][j] - 1];
                    }
                }
            }
            return returnArray;
        }

        public static void Output(bool solved, char[] chars, char[][] puzzle, string file)
        {
            List<string> lines = new();
            if (solved)
            {
                lines.Add(puzzle.Length.ToString());
                string temp = "";
                foreach(char c in chars)
                {
                    temp += c;
                    temp += ' ';
                }
                lines.Add(temp);
                for (int i = 0; i < puzzle.Length; i++)
                {
                    temp = "";
                    foreach (char c in puzzle[i])
                    {
                        temp += c;
                        temp += ' ';
                    }
                    lines.Add(temp);
                }
            }
            else
            {
                lines.Add("Unsolvable");
                /*for (int i = 0; i < puzzle.Length; i++)
                {
                    string temp = "";
                    foreach (char c in puzzle[i])
                    {
                        temp += c;
                        temp += ' ';
                    }
                    lines.Add(temp);
                }*/
            }
            string docPath = "../../../../"; /*Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);*/
            using StreamWriter outputFile = new(Path.Combine(docPath, file));
            foreach (string line in lines)
                outputFile.WriteLine(line);
        }
    }
}
