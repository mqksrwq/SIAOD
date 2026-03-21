namespace Lab_18_20;

public partial class Form1 : Form
{
    private readonly Random _RND;
    private readonly static int _ROWS_COUNT = 4;

    public Form1()
    {
        InitializeComponent();
        TreeDataGridView.RowCount = _ROWS_COUNT;
        _RND = new Random();
        ArrayDataGridView.AllowUserToResizeColumns = false;
        ArrayDataGridView.AllowUserToResizeRows = false;
        TreeDataGridView.AllowUserToResizeColumns = false;
        TreeDataGridView.AllowUserToResizeRows = false;
        InsertDataGridView.AllowUserToResizeColumns = false;
        InsertDataGridView.AllowUserToResizeRows = false;
    }

    private void Print(int[] m)
    {
        for (int i = 0; i < m.Length; i++)
        {
            ArrayDataGridView.Rows[0].Cells[i].Value = m[i].ToString();
        }

        int index = 0;
        for (int i = 1; i <= _ROWS_COUNT; i++)
        {
            int j = m.Length / (int)Math.Pow(2, i);
            while (j < m.Length)
            {
                TreeDataGridView.Rows[i - 1].Cells[j].Value = m[index].ToString();
                index++;
                j += m.Length / (int)Math.Pow(2, i - 1) + 1;
            }
        }
    }


    private void CreateQueueButton_Click(object sender, EventArgs e)
    {
        int[] m = new int[15];
        for (int i = 0; i < m.Length; i++)
        {
            m[i] = _RND.Next(10, 100);
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

    private void ClearQueueButton_Click(object sender, EventArgs e)
    {
        ArrayDataGridView.Rows.Clear();

        InsertDataGridView.Rows.Clear();

        TreeDataGridView.Rows.Clear();
        TreeDataGridView.RowCount = _ROWS_COUNT;
    }

    private void ExitButton_Click(object sender, EventArgs e)
    {
        Application.Exit();
    }
}