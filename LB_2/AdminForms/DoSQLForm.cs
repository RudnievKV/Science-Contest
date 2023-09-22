using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace LB_2
{
    public partial class DoSQLForm : Form
    {
        private static DataTable dataTable = new DataTable();
        static private string connectionString =
            "Data Source=DESKTOP-JKS2HE1;Initial Catalog=ScienceContest;"
            + "Integrated Security=true";
        SqlConnection connection = new SqlConnection(connectionString);




        public DoSQLForm()
        {
            InitializeComponent();

            connection.Open();
        }

        private void DoSQLForm_Load(object sender, EventArgs e)
        {
         
        }

        private void DoSQLForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            connection.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            dataTable.Rows.Clear();
            dataTable.Columns.Clear();
            dataGridView1.DataSource = dataTable;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand command;
                string queryString;
                string q = textBox1.Text.ToLower();
                if (q.Contains("select"))
                {
                    dataTable.Rows.Clear();
                    dataTable.Columns.Clear();
                    queryString = textBox1.Text;
                    command = new SqlCommand(queryString, connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dataTable);
                    
                    try
                    {
                        dataGridView1.DataSource = dataTable;
                    }
                    catch
                    {

                    }
                    foreach (DataGridViewColumn dgvc in dataGridView1.Columns)
                    {
                        dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
                    }
                    MessageBox.Show("Запрос выполнен");
                }
                else
                {
                    queryString = textBox1.Text;
                    command = new SqlCommand(queryString, connection);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Запрос выполнен");
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }
    }
}
