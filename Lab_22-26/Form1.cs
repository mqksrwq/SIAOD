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
        }

        private void SortButton_Click(object sender, EventArgs e)
        {
            MassiveInitialize();

            bool runSimpleTwoPhase = Convert.ToBoolean(dataGridView1.Rows[0].Cells[0].Value);

            if (runSimpleTwoPhase)
            {
                int[] workArray = (int[])m.Clone();

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

            for (int i = 1; i < dataGridView1.RowCount; i++)
            {
                ClearRowResult(i);
            }
        }

        private void MassiveInitialize()
        {
            comparisons = 0;
            assignments = 0;

            size = (int)SizeNUD.Value;
            m = new int[size];
            copy = new int[size];

            for (int i = 0; i < size; i++)
            {
                m[i] = rnd.Next(0, size);
            }

            Array.Copy(m, copy, size);
        }

        private void ApplyOrderedPercent(int[] array, int orderedPercent)
        {
            if (array.Length == 0)
            {
                return;
            }

            if (orderedPercent < 0)
            {
                orderedPercent = 0;
            }
            else if (orderedPercent > 100)
            {
                orderedPercent = 100;
            }

            int orderedPartLength = array.Length * orderedPercent / 100;
            if (orderedPartLength > 1)
            {
                Array.Sort(array, 0, orderedPartLength);
            }
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
                        }
                    }
                    else
                    {
                        for (int k = 0; k < currentRunLength; k++)
                        {
                            c[cCount++] = source[i++];
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
