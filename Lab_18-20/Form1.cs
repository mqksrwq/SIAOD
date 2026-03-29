namespace Lab_18_20;

public partial class Form1 : Form
{
    private readonly Random _RND;
    private readonly static int _ROWS_COUNT = 4;
    private readonly static int _CELLS_COUNT = 15;

    private int[] m;
    private int n;

    private int _extractedCount;

    public Form1()
    {
        InitializeComponent();
        _RND = new Random();
        m = new int[_CELLS_COUNT];
        n = 0;
        _extractedCount = 0;
    }

    private void Print(int[] m)
    {
        for (int i = 0; i < ArrayDataGridView.Columns.Count; i++)
        {
            ArrayDataGridView.Rows[0].Cells[i].Value = null;
        }

        for (int i = 0; i < n; i++)
        {
            if (i < ArrayDataGridView.Columns.Count)
                ArrayDataGridView.Rows[0].Cells[i].Value = m[i].ToString();
        }

        for (int i = 0; i < TreeDataGridView.Rows.Count; i++)
        {
            for (int j = 0; j < TreeDataGridView.Columns.Count; j++)
            {
                TreeDataGridView.Rows[i].Cells[j].Value = null;
            }
        }

        int index = 0;
        for (int i = 1; i <= _ROWS_COUNT; i++)
        {
            int j = m.Length / (int)Math.Pow(2, i);
            while (j < m.Length)
            {
                if (index < n && m[index] != 0)
                {
                    if (i - 1 < TreeDataGridView.Rows.Count && j < TreeDataGridView.Rows[i - 1].Cells.Count)
                        TreeDataGridView.Rows[i - 1].Cells[j].Value = m[index].ToString();
                }

                index++;
                j += m.Length / (int)Math.Pow(2, i - 1) + 1;
            }
        }
    }

    private void CreateQueueButton_Click(object sender, EventArgs e)
    {
        m = new int[_CELLS_COUNT];
        n = 0;
        _extractedCount = 0;

        if (InsertDataGridView.Rows.Count == 0) InsertDataGridView.Rows.Add();
        for (int i = 0; i < InsertDataGridView.Columns.Count; i++)
        {
            InsertDataGridView.Rows[0].Cells[i].Value = null;
        }

        for (int i = 0; i < ArrayDataGridView.Columns.Count; i++)
        {
            ArrayDataGridView.Rows[0].Cells[i].Value = null;
        }

        for (int i = 0; i < m.Length; i++)
        {
            m[i] = _RND.Next(10, 100);
            n++;
            FixUp(m, i);
        }

        Print(m);
    }

    private void FixUp(int[] m, int index)
    {
        int parentIndex = (index - 1) / 2;
        while (index > 0 && m[parentIndex] < m[index])
        {
            (m[parentIndex], m[index]) = (m[index], m[parentIndex]);
            index = parentIndex;
            parentIndex = (index - 1) / 2;
        }
    }

    private void FixDown(int[] m, int index)
    {
        while (2 * index <= n)
        {
            int j = 2 * (index + 1) - 1;
            if (j < n && m[j] < m[j + 1])
                j++;
            if (m[index] >= m[j])
                return;
            (m[index], m[j]) = (m[j], m[index]);
            index = j;
        }
    }

    private void ClearQueueButton_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < ArrayDataGridView.Columns.Count; i++)
        {
            ArrayDataGridView.Rows[0].Cells[i].Value = null;
        }

        if (InsertDataGridView.Rows.Count == 0) InsertDataGridView.Rows.Add();
        for (int i = 0; i < InsertDataGridView.Columns.Count; i++)
        {
            InsertDataGridView.Rows[0].Cells[i].Value = null;
        }

        for (int i = 0; i < TreeDataGridView.Rows.Count; i++)
        {
            for (int j = 0; j < TreeDataGridView.Columns.Count; j++)
            {
                TreeDataGridView.Rows[i].Cells[j].Value = null;
            }
        }

        _extractedCount = 0;
        n = 0;
    }

    private void ExitButton_Click(object sender, EventArgs e)
    {
        Application.Exit();
    }

    private void InsertMaximumButton_Click(object sender, EventArgs e)
    {
        try
        {
            if (n == 0)
                throw new ArgumentException("Невозможно извлечь наибольший - очередь пуста!");

            int maximum = m[0];
            (m[0], m[n - 1]) = (m[n - 1], m[0]);
            m[n - 1] = 0;
            n--;
            FixDown(m, 0);

            Print(m);

            _extractedCount++;

            if (_extractedCount <= InsertDataGridView.Columns.Count)
            {
                PrintInsert(maximum, _extractedCount - 1);
            }
            else
            {
                MessageBox.Show("Таблица извлеченных элементов переполнена!", "Внимание", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void AddNew_Click(object sender, EventArgs e)
    {
        try
        {
            if (n == 15)
                throw new ArgumentException("Невозможно добавить новый - очередь переполнена!");
            int num = (int)NewElemNumericUpDown.Value;

            n++;
            m[n - 1] = num;
            FixUp(m, n - 1);

            Print(m);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void ChangePriorityButton_Click(object sender, EventArgs e)
    {
        try
        {
            if (n == 0)
                throw new ArgumentException("Невозможно поменять приоритет - очередь пуста!");

            int down = (int)DownNumericUpDown.Value;
            int top = (int)TopNumericUpDown.Value;

            for (int i = 0; i < n; i++)
            {
                if (m[i] == down)
                {
                    m[i] = top;
                    if (top < down)
                        FixDown(m, i);
                    else
                        FixUp(m, i);
                    Print(m);
                    return;
                }
            }

            throw new ArgumentException($"Число {down} не найдено!");
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void PrintInsert(int num, int index)
    {
        if (InsertDataGridView.Rows.Count == 0) InsertDataGridView.Rows.Add();

        if (index < InsertDataGridView.Rows[0].Cells.Count)
        {
            InsertDataGridView.Rows[0].Cells[index].Value = num;
        }
    }
}