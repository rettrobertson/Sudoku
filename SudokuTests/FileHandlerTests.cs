using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sudoku;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Sudoku.Tests
{
    [TestClass()]
    public class FileHandlerTests
    {
        [TestMethod()]
        public void CharToIntTest()
        {
            char[] characters = new char[4] { '1', '2', '3', '4' };
            char[][] chars = new char[2][] { new char[2] { '-', '1' }, new char[2] { '2', '-' } };
            int[][] ints = new int[2][] { new int[2] { 0, 1 }, new int[2] { 2, 0 } };
            int[][] functioned = FileHandler.CharToInt(characters, chars);
            for (int i = 0; i < functioned.Length; i++)
            {
                for (int j = 0; j < functioned[i].Length; j++)
                {
                    Assert.AreEqual(functioned[i][j], ints[i][j]);
                }
            }
        }

        [TestMethod()]
        public void IntToCharTest()
        {
            char[] characters = new char[4] { '1', '2', '3', '4' };
            char[][] chars = new char[2][] { new char[2] { '-', '1' }, new char[2] { '2', '-' } };
            int[][] ints = new int[2][] { new int[2] { 0, 1 }, new int[2] { 2, 0 } };
            char[][] functioned = FileHandler.IntToChar(characters, ints);
            for (int i = 0; i < functioned.Length; i++)
            {
                for (int j = 0; j < functioned[i].Length; j++)
                {
                    Assert.AreEqual(functioned[i][j], chars[i][j]);
                }
            }
        }

    }
}