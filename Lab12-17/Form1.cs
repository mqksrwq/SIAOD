using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab_12_17
{
    public partial class Form1 : Form
    {
        private int[] m, copy;
        private int size;
        private static readonly Random rnd = new Random();

        private int comparisons;
        private int permutations;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.RowCount = 8;
            dataGridView1.Rows[0].Cells[1].Value = "Обмен";
            dataGridView1.Rows[1].Cells[1].Value = "Выбор";
            dataGridView1.Rows[2].Cells[1].Value = "Включение";
            dataGridView1.Rows[3].Cells[1].Value = "Шелла";
            dataGridView1.Rows[4].Cells[1].Value = "Быстрая";
            dataGridView1.Rows[5].Cells[1].Value = "Линейная";
            dataGridView1.Rows[6].Cells[1].Value = "Встроенная";
            dataGridView1.Rows[7].Cells[1].Value = "Пирамидальная";

            dataGridView1.Rows[0].Cells[0].Value = false;
            dataGridView1.Rows[1].Cells[0].Value = false;
            dataGridView1.Rows[2].Cells[0].Value = false;
            dataGridView1.Rows[3].Cells[0].Value = true;
            dataGridView1.Rows[4].Cells[0].Value = true;
            dataGridView1.Rows[5].Cells[0].Value = true;
            dataGridView1.Rows[6].Cells[0].Value = true;
            dataGridView1.Rows[7].Cells[0].Value = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MassiveInitilize();
            if ((bool)dataGridView1.Rows[0].Cells[0].Value == true)
            {
                int StartTime = Environment.TickCount;
                m = BubbleSorting(m);
                int EndTime = Environment.TickCount - StartTime;
                dataGridView1.Rows[0].Cells[2].Value = comparisons;
                dataGridView1.Rows[0].Cells[3].Value = permutations;
                dataGridView1.Rows[0].Cells[4].Value = EndTime;
                dataGridView1.Rows[0].Cells[5].Value = SortingCheck(m) ? "Да" : "Нет";
                MassiveUpdate();
            }
            else
            {
                dataGridView1.Rows[0].Cells[2].Value = null;
                dataGridView1.Rows[0].Cells[3].Value = null;
                dataGridView1.Rows[0].Cells[4].Value = null;
                dataGridView1.Rows[0].Cells[5].Value = null;
            }

            if ((bool)dataGridView1.Rows[1].Cells[0].Value == true)
            {
                int StartTime = Environment.TickCount;
                m = DirectSelection(m);
                int EndTime = Environment.TickCount - StartTime;
                dataGridView1.Rows[1].Cells[2].Value = comparisons;
                dataGridView1.Rows[1].Cells[3].Value = permutations;
                dataGridView1.Rows[1].Cells[4].Value = EndTime;
                dataGridView1.Rows[1].Cells[5].Value = SortingCheck(m) ? "Да" : "Нет";
                MassiveUpdate();
            }
            else
            {
                dataGridView1.Rows[1].Cells[2].Value = null;
                dataGridView1.Rows[1].Cells[3].Value = null;
                dataGridView1.Rows[1].Cells[4].Value = null;
                dataGridView1.Rows[1].Cells[5].Value = null;
            }

            if ((bool)dataGridView1.Rows[2].Cells[0].Value == true)
            {
                int StartTime = Environment.TickCount;
                m = DirectInsertionWithBarrier(m);
                int EndTime = Environment.TickCount - StartTime;

                dataGridView1.Rows[2].Cells[2].Value = comparisons;
                dataGridView1.Rows[2].Cells[3].Value = permutations;
                dataGridView1.Rows[2].Cells[4].Value = EndTime;
                dataGridView1.Rows[2].Cells[5].Value = SortingCheck(m) ? "Да" : "Нет";

                MassiveUpdate();
            }
            else
            {
                dataGridView1.Rows[2].Cells[2].Value = null;
                dataGridView1.Rows[2].Cells[3].Value = null;
                dataGridView1.Rows[2].Cells[4].Value = null;
                dataGridView1.Rows[2].Cells[5].Value = null;
            }

            if ((bool)dataGridView1.Rows[3].Cells[0].Value == true)
            {
                int StartTime = Environment.TickCount;
                m = Shall(m);
                int EndTime = Environment.TickCount - StartTime;
                dataGridView1.Rows[3].Cells[2].Value = comparisons;
                dataGridView1.Rows[3].Cells[3].Value = permutations;
                dataGridView1.Rows[3].Cells[4].Value = EndTime;
                dataGridView1.Rows[3].Cells[5].Value = SortingCheck(m) ? "Да" : "Нет";
                MassiveUpdate();
            }
            else
            {
                dataGridView1.Rows[3].Cells[2].Value = null;
                dataGridView1.Rows[3].Cells[3].Value = null;
                dataGridView1.Rows[3].Cells[4].Value = null;
                dataGridView1.Rows[3].Cells[5].Value = null;
            }

            if ((bool)dataGridView1.Rows[4].Cells[0].Value == true)
            {
                int StartTime = Environment.TickCount;
                m = QuickSort(m, 0, size - 1);
                int EndTime = Environment.TickCount - StartTime;

                dataGridView1.Rows[4].Cells[2].Value = comparisons;
                dataGridView1.Rows[4].Cells[3].Value = permutations;
                dataGridView1.Rows[4].Cells[4].Value = EndTime;
                dataGridView1.Rows[4].Cells[5].Value = SortingCheck(m) ? "Да" : "Нет";

                MassiveUpdate();
            }
            else
            {
                dataGridView1.Rows[4].Cells[2].Value = null;
                dataGridView1.Rows[4].Cells[3].Value = null;
                dataGridView1.Rows[4].Cells[4].Value = null;
                dataGridView1.Rows[4].Cells[5].Value = null;
            }

            if ((bool)dataGridView1.Rows[5].Cells[0].Value == true)
            {
                int StartTime = Environment.TickCount;
                m = LinnerSorting(m);
                int EndTime = Environment.TickCount - StartTime;
                dataGridView1.Rows[5].Cells[2].Value = comparisons;
                dataGridView1.Rows[5].Cells[3].Value = permutations;
                dataGridView1.Rows[5].Cells[4].Value = EndTime;
                dataGridView1.Rows[5].Cells[5].Value = SortingCheck(m) ? "Да" : "Нет";
                MassiveUpdate();
            }
            else
            {
                dataGridView1.Rows[5].Cells[2].Value = null;
                dataGridView1.Rows[5].Cells[3].Value = null;
                dataGridView1.Rows[5].Cells[4].Value = null;
                dataGridView1.Rows[5].Cells[5].Value = null;
            }

            if ((bool)dataGridView1.Rows[6].Cells[0].Value == true)
            {
                int StartTime = Environment.TickCount;
                Array.Sort(m);
                int EndTime = Environment.TickCount - StartTime;
                dataGridView1.Rows[6].Cells[2].Value = "-";
                dataGridView1.Rows[6].Cells[3].Value = "-";
                dataGridView1.Rows[6].Cells[4].Value = EndTime;
                dataGridView1.Rows[6].Cells[5].Value = SortingCheck(m) ? "Да" : "Нет";
                MassiveUpdate();
            }
            else
            {
                dataGridView1.Rows[6].Cells[2].Value = null;
                dataGridView1.Rows[6].Cells[3].Value = null;
                dataGridView1.Rows[6].Cells[4].Value = null;
                dataGridView1.Rows[6].Cells[5].Value = null;
            }

            if ((bool)dataGridView1.Rows[7].Cells[0].Value == true)
            {
                int StartTime = Environment.TickCount;
                m = PyramidSorting(m);
                int EndTime = Environment.TickCount - StartTime;
                dataGridView1.Rows[7].Cells[2].Value = comparisons;
                dataGridView1.Rows[7].Cells[3].Value = permutations;
                dataGridView1.Rows[7].Cells[4].Value = EndTime;
                dataGridView1.Rows[7].Cells[5].Value = SortingCheck(m) ? "Да" : "Нет";
                MassiveUpdate();
            }
            else
            {
                dataGridView1.Rows[7].Cells[2].Value = null;
                dataGridView1.Rows[7].Cells[3].Value = null;
                dataGridView1.Rows[7].Cells[4].Value = null;
                dataGridView1.Rows[7].Cells[5].Value = null;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MassiveInitilize()
        {
            comparisons = 0; permutations =


0;
            size = (int)ArraySizeNumeric.Value;
            m = new int[size];
            copy = new int[size];

            for (int i = 0; i < size; i++)
            {
                int elem = rnd.Next(0, size);
                m[i] = elem;
                copy[i] = elem;
            }
        }

        private void MassiveUpdate()
        {
            comparisons = 0; permutations = 0;
            for (int i = 0; i < size; i++)
                m[i] = copy[i];
        }

        private bool SortingCheck(int[] m)
        {
            for (int i = 0; i < size - 1; i++)
                if (m[i] > m[i + 1]) return false;
            return true;
        }

        // пузырьковая
        public int[] BubbleSorting(int[] m)
        {
            for (int i = 0; i < size; i++)
            {
                bool fl = false;
                for (int j = 0; j < size - i - 1; j++)
                {
                    if (m[j] > m[j + 1])
                    {
                        (m[j], m[j + 1]) = (m[j + 1], m[j]);
                        permutations++;
                        fl = true;
                    }
                    comparisons++;
                }
                if (!fl) break;
            }
            return m;
        }

        // прямым выбором
        public int[] DirectSelection(int[] m)
        {
            for (int i = 0; i < size - 1; i++)
            {
                int index = i;
                for (int j = i + 1; j < size; j++)
                {
                    if (m[j] < m[index]) index = j;
                    comparisons++;
                }
                (m[i], m[index]) = (m[index], m[i]);
                permutations++;
            }
            return m;
        }

        // прямым включением с минимальным барьером 
        public int[] DirectInsertionWithBarrier(int[] m)
        {
            int minIndex = 0;
            for (int i = 1; i < size; i++)
            {
                if (m[i] < m[minIndex])
                    minIndex = i;
                comparisons++;
            }

            (m[0], m[minIndex]) = (m[minIndex], m[0]);
            permutations++;

            for (int i = 2; i < size; i++)
            {
                int temp = m[i];
                int j = i - 1;

                while (temp < m[j])
                {
                    m[j + 1] = m[j];
                    comparisons++;
                    permutations++;
                    j--;
                }
                comparisons++;
                m[j + 1] = temp;
                permutations++;
            }
            return m;
        }

        // быстрая
        public int[] QuickSort(int[] m, int left, int right)
        {
            if (left >= right) return m;

            int elem = m[left];
            int i = left + 1;
            int j = right;

            while (i <= j)
            {
                while (i <= right)
                {
                    comparisons++;
                    if (elem > m[i]) i++;
                    else break;
                }

                while (j > left)
                {
                    comparisons++;
                    if (elem < m[j]) j--;
                    else break;
                }

                comparisons++;
                if (i <= j)
                {
                    if (i != j)
                    {
                        (m[i], m[j]) = (m[j], m[i]);
                        permutations++;
                    }
                    i++;
                    j--;
                }
            }

            (m[left], m[j]) = (m[j], m[left]);
            permutations++;

            if (left < j - 1) QuickSort(m, left, j - 1);
            if (i < right) QuickSort(m, i, right);

            return m;
        }

        // Шелла
        public int[] Shall(int[] m)
        {
            int h = (int)Math.Pow(2, Math.Log(m.Length, 2)) - 1;
            while (h


>= 1)
            {
                comparisons++;
                for (int i = h; i < m.Length; i++)
                {
                    int key = m[i];
                    int j = i - h;

                    while (j >= 0 && m[j] > key)
                    {
                        m[j + h] = m[j];
                        comparisons++;
                        permutations++;
                        j -= h;
                    }
                    m[j + h] = key;
                    permutations++;
                }
                h /= 2;
            }
            return m;
        }

        // линейная
        public int[] LinnerSorting(int[] m)
        {
            int[] B = new int[m.Length + 1];
            for (int i = 0; i < m.Length; i++)
            {
                B[m[i]]++;
                permutations++;
            }
            int index = 0;
            for (int j = 0; j < B.Length; j++)
            {
                for (; B[j] > 0; B[j]--)
                {
                    m[index] = j;
                    index++;
                    comparisons++;
                    permutations++;
                }
            }
            return m;
        }

        // пирамидальная
        public int[] PyramidSorting(int[] m)
        {
            int n = m.Length - 1;

            for (int i = n / 2; i >= 0; i--)
            {
                FixDown(m, i, n);
            }
            for (int i = 0; i < m.Length; i++)
            {
                (m[0], m[n]) = (m[n], m[0]);
                permutations++;
                n--;
                FixDown(m, 0, n);
            }

            return m;
        }

        public void FixDown(int[] m, int index, int n)
        {
            while (2 * index + 1 <= n)
            {
                int j = 2 * (index + 1) - 1;
                if (j < n && m[j] < m[j + 1])
                {
                    j++;
                    comparisons++;
                }
                if (m[index] >= m[j])
                {
                    comparisons++;
                    return;
                }
                (m[index], m[j]) = (m[j], m[index]);
                permutations++;
                index = j;
                comparisons++;
            }
        }
    }
}