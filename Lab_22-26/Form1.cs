using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab_22_26
{
    public partial class Form1 : Form
    {
        private static readonly Random rnd = new Random();

        private int[] m;
        private int[] copy;
        private int size;

        private int comparisons;
        private int assignments;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ClientSize = new Size(panel1.Right + panel1.Left, panel1.Bottom + panel1.Top);

            SortButton.Click += SortButton_Click;

            numericUpDown2.Minimum = 0;
            numericUpDown2.Maximum = 100;

            string[] sortingNames =
            {
                "Простое 2ф",
                "Простое 1ф",
                "Естественное 2ф",
                "Естественное 1ф",
                "Поглощение"
            };

            dataGridView1.RowCount = sortingNames.Length;

            for (int i = 0; i < sortingNames.Length; i++)
            {
                dataGridView1.Rows[i].Cells[0].Value = i == 0;
                dataGridView1.Rows[i].Cells[1].Value = sortingNames[i];
            }

            dataGridView1.Rows[0].Cells[0].Value = true;
            dataGridView1.Rows[1].Cells[0].Value = true;
        }

        private void SortButton_Click(object sender, EventArgs e)
        {
            MassiveInitialize();

            bool runSimpleTwoPhase = Convert.ToBoolean(dataGridView1.Rows[0].Cells[0].Value);
            bool runSimpleOnePhase = Convert.ToBoolean(dataGridView1.Rows[1].Cells[0].Value);
            bool runNaturalTwoPhase = Convert.ToBoolean(dataGridView1.Rows[2].Cells[0].Value);

            if (runSimpleTwoPhase)
            {
                int[] workArray = (int[])copy.Clone();
                ResetCounters();

                int startTime = Environment.TickCount;
                workArray = SimpleTwoPhaseMergeSort(workArray);
                int endTime = Environment.TickCount - startTime;

                dataGridView1.Rows[0].Cells[2].Value = comparisons;
                dataGridView1.Rows[0].Cells[3].Value = assignments;
                dataGridView1.Rows[0].Cells[4].Value = endTime;
                dataGridView1.Rows[0].Cells[5].Value = SortingCheck(workArray) ? "Да" : "Нет";
            }
            else
            {
                ClearRowResult(0);
            }

            if (runSimpleOnePhase)
            {
                int[] workArray = (int[])copy.Clone();
                ResetCounters();

                int startTime = Environment.TickCount;
                workArray = SimpleOnePhaseMergeSort(workArray);
                int endTime = Environment.TickCount - startTime;

                dataGridView1.Rows[1].Cells[2].Value = comparisons;
                dataGridView1.Rows[1].Cells[3].Value = assignments;
                dataGridView1.Rows[1].Cells[4].Value = endTime;
                dataGridView1.Rows[1].Cells[5].Value = SortingCheck(workArray) ? "Да" : "Нет";
            }
            else
            {
                ClearRowResult(1);
            }

            if (runNaturalTwoPhase)
            {
                int[] workArray = (int[])copy.Clone();
                ResetCounters();

                int startTime = Environment.TickCount;
                workArray = NaturalTwoPhaseMergeSort(workArray);
                int endTime = Environment.TickCount - startTime;

                dataGridView1.Rows[2].Cells[2].Value = comparisons;
                dataGridView1.Rows[2].Cells[3].Value = assignments;
                dataGridView1.Rows[2].Cells[4].Value = endTime;
                dataGridView1.Rows[2].Cells[5].Value = SortingCheck(workArray) ? "Да" : "Нет";
            }
            else
            {
                ClearRowResult(2);
            }

            for (int i = 3; i < dataGridView1.RowCount; i++)
            {
                ClearRowResult(i);
            }
        }

        private void MassiveInitialize()
        {
            ResetCounters();

            size = (int)SizeNUD.Value;
            m = new int[size];
            copy = new int[size];

            for (int i = 0; i < size; i++)
            {
                m[i] = rnd.Next(0, size);
            }

            Array.Copy(m, copy, size);
        }

        private void ResetCounters()
        {
            comparisons = 0;
            assignments = 0;
        }

        private int[] SimpleTwoPhaseMergeSort(int[] source)
        {
            int n = source.Length;
            if (n <= 1)
            {
                return source;
            }

            int[] b = new int[n];
            int[] c = new int[n];

            int runLength = 1;

            while (runLength < n)
            {
                int bCount = 0;
                int cCount = 0;
                bool toB = true;

                for (int i = 0; i < n;)
                {
                    int currentRunLength = Math.Min(runLength, n - i);
                    if (toB)
                    {
                        for (int k = 0; k < currentRunLength; k++)
                        {
                            b[bCount++] = source[i++];
                            assignments++;
                        }
                    }
                    else
                    {
                        for (int k = 0; k < currentRunLength; k++)
                        {
                            c[cCount++] = source[i++];
                            assignments++;
                        }
                    }

                    toB = !toB;
                }

                int bi = 0;
                int ci = 0;
                int outIndex = 0;

                while (bi < bCount || ci < cCount)
                {
                    int bRunEnd = Math.Min(bi + runLength, bCount);
                    int cRunEnd = Math.Min(ci + runLength, cCount);

                    while (bi < bRunEnd && ci < cRunEnd)
                    {
                        comparisons++;
                        if (b[bi] <= c[ci])
                        {
                            source[outIndex++] = b[bi++];
                        }
                        else
                        {
                            source[outIndex++] = c[ci++];
                        }

                        assignments++;
                    }

                    while (bi < bRunEnd)
                    {
                        source[outIndex++] = b[bi++];
                        assignments++;
                    }

                    while (ci < cRunEnd)
                    {
                        source[outIndex++] = c[ci++];
                        assignments++;
                    }
                }

                runLength *= 2;

            }

            return source;
        }

        private int[] SimpleOnePhaseMergeSort(int[] source)
        {
            int n = source.Length;
            if (n <= 1)
            {
                return source;
            }

            int[] b = new int[n];
            int[] c = new int[n];
            int[] d = new int[n];
            int[] e = new int[n];

            int runLength = 1;
            int bCount;
            int cCount;
            SplitBySeries(source, n, runLength, b, c, out bCount, out cCount);

            int dCount = 0;
            int eCount = 0;
            bool readFromBC = true;

            while (runLength < n)
            {
                if (readFromBC)
                {
                    MergeSeriesToTwoOutputs(b, bCount, c, cCount, runLength, d, e, out dCount, out eCount);
                }
                else
                {
                    MergeSeriesToTwoOutputs(d, dCount, e, eCount, runLength, b, c, out bCount, out cCount);
                }

                runLength *= 2;
                readFromBC = !readFromBC;
            }

            if (readFromBC)
            {
                int index = 0;
                for (int i = 0; i < bCount; i++)
                {
                    source[index++] = b[i];
                    assignments++;
                }

                for (int i = 0; i < cCount; i++)
                {
                    source[index++] = c[i];
                    assignments++;
                }
            }
            else
            {
                int index = 0;
                for (int i = 0; i < dCount; i++)
                {
                    source[index++] = d[i];
                    assignments++;
                }

                for (int i = 0; i < eCount; i++)
                {
                    source[index++] = e[i];
                    assignments++;
                }
            }

            return source;
        }

        private void SplitBySeries(int[] source, int sourceCount, int runLength, int[] first, int[] second, out int firstCount, out int secondCount)
        {
            firstCount = 0;
            secondCount = 0;
            bool toFirst = true;

            for (int i = 0; i < sourceCount;)
            {
                int currentRunLength = Math.Min(runLength, sourceCount - i);
                if (toFirst)
                {
                    for (int k = 0; k < currentRunLength; k++)
                    {
                        first[firstCount++] = source[i++];
                        assignments++;
                    }
                }
                else
                {
                    for (int k = 0; k < currentRunLength; k++)
                    {
                        second[secondCount++] = source[i++];
                        assignments++;
                    }
                }

                toFirst = !toFirst;
            }
        }

        private void MergeSeriesToTwoOutputs(
            int[] firstInput,
            int firstCount,
            int[] secondInput,
            int secondCount,
            int runLength,
            int[] firstOutput,
            int[] secondOutput,
            out int firstOutputCount,
            out int secondOutputCount)
        {
            firstOutputCount = 0;
            secondOutputCount = 0;

            int firstIndex = 0;
            int secondIndex = 0;
            bool toFirstOutput = true;

            while (firstIndex < firstCount || secondIndex < secondCount)
            {
                int firstRunEnd = Math.Min(firstIndex + runLength, firstCount);
                int secondRunEnd = Math.Min(secondIndex + runLength, secondCount);

                while (firstIndex < firstRunEnd && secondIndex < secondRunEnd)
                {
                    comparisons++;
                    if (firstInput[firstIndex] <= secondInput[secondIndex])
                    {
                        if (toFirstOutput)
                        {
                            firstOutput[firstOutputCount++] = firstInput[firstIndex++];
                        }
                        else
                        {
                            secondOutput[secondOutputCount++] = firstInput[firstIndex++];
                        }
                    }
                    else
                    {
                        if (toFirstOutput)
                        {
                            firstOutput[firstOutputCount++] = secondInput[secondIndex++];
                        }
                        else
                        {
                            secondOutput[secondOutputCount++] = secondInput[secondIndex++];
                        }
                    }

                    assignments++;
                }

                while (firstIndex < firstRunEnd)
                {
                    if (toFirstOutput)
                    {
                        firstOutput[firstOutputCount++] = firstInput[firstIndex++];
                    }
                    else
                    {
                        secondOutput[secondOutputCount++] = firstInput[firstIndex++];
                    }

                    assignments++;
                }

                while (secondIndex < secondRunEnd)
                {
                    if (toFirstOutput)
                    {
                        firstOutput[firstOutputCount++] = secondInput[secondIndex++];
                    }
                    else
                    {
                        secondOutput[secondOutputCount++] = secondInput[secondIndex++];
                    }

                    assignments++;
                }

                toFirstOutput = !toFirstOutput;
            }
        }

        private int[] NaturalTwoPhaseMergeSort(int[] source)
        {
            int n = source.Length;
            if (n <= 1)
            {
                return source;
            }

            int[] b = new int[n];
            int[] c = new int[n];

            while (true)
            {
                int bCount;
                int cCount;
                List<int> bRunLengths;
                List<int> cRunLengths;

                SplitNaturalRuns(source, b, c, out bCount, out cCount, out bRunLengths, out cRunLengths);

                if (cRunLengths.Count == 0 && bRunLengths.Count == 1)
                {
                    return source;
                }

                MergeNaturalRuns(b, bRunLengths, c, cRunLengths, source);
            }
        }

        private void SplitNaturalRuns(
            int[] source,
            int[] first,
            int[] second,
            out int firstCount,
            out int secondCount,
            out List<int> firstRunLengths,
            out List<int> secondRunLengths)
        {
            firstCount = 0;
            secondCount = 0;
            firstRunLengths = new List<int>();
            secondRunLengths = new List<int>();

            bool toFirst = true;
            int i = 0;

            while (i < source.Length)
            {
                int runStart = i;
                i++;

                while (i < source.Length)
                {
                    comparisons++;
                    if (source[i - 1] <= source[i])
                    {
                        i++;
                    }
                    else
                    {
                        break;
                    }
                }

                int runLength = i - runStart;

                if (toFirst)
                {
                    for (int k = 0; k < runLength; k++)
                    {
                        first[firstCount++] = source[runStart + k];
                        assignments++;
                    }

                    firstRunLengths.Add(runLength);
                }
                else
                {
                    for (int k = 0; k < runLength; k++)
                    {
                        second[secondCount++] = source[runStart + k];
                        assignments++;
                    }

                    secondRunLengths.Add(runLength);
                }

                toFirst = !toFirst;
            }
        }

        private void MergeNaturalRuns(
            int[] first,
            List<int> firstRunLengths,
            int[] second,
            List<int> secondRunLengths,
            int[] output)
        {
            int firstPos = 0;
            int secondPos = 0;
            int outPos = 0;

            int runsToMerge = Math.Max(firstRunLengths.Count, secondRunLengths.Count);

            for (int run = 0; run < runsToMerge; run++)
            {
                int firstLen = run < firstRunLengths.Count ? firstRunLengths[run] : 0;
                int secondLen = run < secondRunLengths.Count ? secondRunLengths[run] : 0;

                int firstEnd = firstPos + firstLen;
                int secondEnd = secondPos + secondLen;

                while (firstPos < firstEnd && secondPos < secondEnd)
                {
                    comparisons++;
                    if (first[firstPos] <= second[secondPos])
                    {
                        output[outPos++] = first[firstPos++];
                    }
                    else
                    {
                        output[outPos++] = second[secondPos++];
                    }

                    assignments++;
                }

                while (firstPos < firstEnd)
                {
                    output[outPos++] = first[firstPos++];
                    assignments++;
                }

                while (secondPos < secondEnd)
                {
                    output[outPos++] = second[secondPos++];
                    assignments++;
                }
            }
        }

        private bool SortingCheck(int[] array)
        {
            for (int i = 0; i < array.Length - 1; i++)
            {
                if (array[i] > array[i + 1])
                {
                    return false;
                }
            }

            return true;
        }

        private void ClearRowResult(int rowIndex)
        {
            dataGridView1.Rows[rowIndex].Cells[2].Value = null;
            dataGridView1.Rows[rowIndex].Cells[3].Value = null;
            dataGridView1.Rows[rowIndex].Cells[4].Value = null;
            dataGridView1.Rows[rowIndex].Cells[5].Value = null;
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
