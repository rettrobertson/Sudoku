using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sudoku.Solvers;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sudoku.Solvers.Tests
{
    [TestClass()]
    public class FunctionsTests
    {
        [TestMethod()]
        public void FindEmptyTest()
        {
            int[][] input = new int[2][] { new int[2] { 0, 1 }, new int[2] { 2, 3 } };
            (int, int)? temp = Functions.FindEmpty(input);
            Assert.AreEqual(temp, (0, 0));
            input[0][0] = 1;
            temp = Functions.FindEmpty(input);
            Assert.AreEqual(temp, null);
        }

        [TestMethod()]
        public void InRowTest()
        {
            int[] input = new int[5] { 1, 2, 3, 4, 5 };
            Assert.IsTrue(Functions.InRow(input, 1));
            Assert.IsTrue(Functions.InRow(input, 2));
            Assert.IsTrue(Functions.InRow(input, 3));
            Assert.IsTrue(Functions.InRow(input, 4));
            Assert.IsTrue(Functions.InRow(input, 5));

            Assert.IsFalse(Functions.InRow(input, 0));
            Assert.IsFalse(Functions.InRow(input, 6));
            Assert.IsFalse(Functions.InRow(input, 10));
            Assert.IsFalse(Functions.InRow(input, 15));
        }


        [TestMethod()]
        public void GetValidTest()
        {
            int[][] input = new int[4][]
            {
                new int[4]{0, 1, 0, 0 },
                new int[4]{0, 0, 0, 3 },
                new int[4]{0, 0, 0, 0 },
                new int[4]{0, 2, 0, 0 }
            };
            List<int> valids = Functions.GetValid(input, 1, 1);
            Assert.AreEqual(valids[0], 4);
            valids = Functions.GetValid(input, 3, 3);
            int[] temp = new int[2] { 1, 4 };
            for (int i = 0; i < valids.Count; i ++)
            {
                Assert.AreEqual(valids[i], temp[i]);
            }
        }

        [TestMethod()]
        public void CompareTest()
        {
            List<int> a = new List<int> { 1, 2, 3, 4 };
            List<int> b = new List<int> { 1, 2, 3, 4 };
            List<int> c = new List<int> { 4, 3, 2, 1 };
            List<int> d = new List<int> { 5, 6, 7, 8 };
            List<int> e = new List<int> { 1, 2, 3 };

            Assert.IsTrue(Functions.Compare(a, b));
            Assert.IsTrue(Functions.Compare(a, c));
            Assert.IsFalse(Functions.Compare(a, d));
            Assert.IsFalse(Functions.Compare(a, e));
        }

        [TestMethod()]
        public void GetColTest()
        {
            int[][] input = new int[2][] { new int[] { 0, 1 }, new int[] { 0, 2 } };
            int[] first = Functions.GetCol(input, 0);
            int[] second = Functions.GetCol(input, 1);

            for (int i = 0; i < first.Length; i++)
            {
                Assert.AreEqual(first[i], input[i][0]);
                Assert.AreEqual(second[i], input[i][1]);
            }
        }

        [TestMethod()]
        public void GetBlockTest()
        {
            int[][] input = new int[4][]
            {
                new int[4]{1, 2, 0, 0 },
                new int[4]{3, 4, 0, 0 },
                new int[4]{0, 0, 0, 0 },
                new int[4]{0, 0, 0, 0 },
            };

            int[] ones = new int[4] { 1, 2, 3, 4 };

            int[] zeros = new int[4] { 0, 0, 0, 0 };

            int[] temp1 = Functions.GetBlock(input, 0, 0);
            int[] temp2 = Functions.GetBlock(input, 0, 1);
            int[] temp3 = Functions.GetBlock(input, 1, 0);
            int[] temp4 = Functions.GetBlock(input, 1, 1);

            int[] top_right = Functions.GetBlock(input, 0, 2);
            int[] bottom_left = Functions.GetBlock(input, 3, 1);
            int[] bottom_right = Functions.GetBlock(input, 3, 3);

            for (int i = 0; i < 4; i++)
            {
                Assert.AreEqual(ones[i], temp1[i]);
                Assert.AreEqual(ones[i], temp2[i]);
                Assert.AreEqual(ones[i], temp3[i]);
                Assert.AreEqual(ones[i], temp4[i]);

                Assert.AreEqual(zeros[i], top_right[i]);
                Assert.AreEqual(zeros[i], bottom_right[i]);
                Assert.AreEqual(zeros[i], bottom_left[i]);
            }
        }

        [TestMethod()]
        public void RemoveFromRowTest()
        {
            int[] col = new int[2] { 1, 2 };
            List<int> chars = new List<int> { 1, 2, 3, 4 };

            Functions.RemoveFromRow(col, chars);

            Assert.IsTrue(Functions.Compare(chars, new List<int> { 3, 4 }));
        }

        [TestMethod()]
        public void GetRowDoublesTest()
        {
            int[][] input = new int[4][]
            {
                new int[4]{0, 2, 0, 0 },
                new int[4]{0, 0, 1, 0 },
                new int[4]{0, 0, 0, 0 },
                new int[4]{0, 0, 0, 0 },
            };
            List<int> doubles = Functions.GetRowDoubles(input, 0, 0);
            List<int> correct = new List<int> { 3, 4 };
            Assert.IsTrue(Functions.Compare(doubles, correct));
        }

        [TestMethod()]
        public void GetColDoublesTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetBoxDoublesTest()
        {
            Assert.Fail();
        }
    }
}