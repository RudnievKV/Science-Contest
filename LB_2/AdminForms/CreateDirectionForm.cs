using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LB_2.AdminForms
{
    public partial class CreateDirectionForm : Form
    {
        static private string connectionString =
"Data Source=DESKTOP-JKS2HE1;Initial Catalog=ScienceContest;"
+ "Integrated Security=true";
        SqlConnection connection = new SqlConnection(connectionString);
        private byte[] bytes;
        public string direction_name = "";
        public CreateDirectionForm()
        {
            InitializeComponent();
            connection.Open();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void CreateDirectionForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            connection.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text == "")
                {
                    MessageBox.Show("Введіть назву напрямка", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (direction_name != "")
                {
                    if (textBox2.Text == "")
                    {
                        string queryString = $"UPDATE [directions] SET [direction]=@direction,[date]=@date WHERE [direction]='{direction_name}'";
                        SqlCommand command = new SqlCommand(queryString, connection);
                        command.Parameters.AddWithValue("@direction", textBox1.Text);
                        command.Parameters.AddWithValue("@date", dateTimePicker1.Value);
                        command.ExecuteNonQuery();

                    }
                    else
                    {
                        bytes = Encoding.Unicode.GetBytes(textBox2.Text);
                        string queryString = $"UPDATE [directions] SET [direction]=@direction,[date]=@date,[description]=@description WHERE [direction]='{direction_name}'";
                        SqlCommand command = new SqlCommand(queryString, connection);
                        command.Parameters.AddWithValue("@direction", textBox1.Text);
                        command.Parameters.AddWithValue("@date", dateTimePicker1.Value);
                        command.Parameters.AddWithValue("@description", bytes);
                        command.ExecuteNonQuery();
                    }
                    MessageBox.Show("Напрямок оновлено");
                    Close();
                }
                else
                {
                    if (textBox2.Text == "")
                    {
                        string queryString = $"INSERT INTO [directions] ([direction],[date]) VALUES (@direction,@date)";
                        SqlCommand command = new SqlCommand(queryString, connection);
                        
                        command.Parameters.AddWithValue("@direction", textBox1.Text);
                        command.Parameters.AddWithValue("@date", dateTimePicker1.Value);

                        command.ExecuteNonQuery();
                    }
                    else
                    {
                        bytes = Encoding.Unicode.GetBytes(textBox2.Text);
                        string queryString = $"INSERT INTO [directions] ([direction],[date],[description]) VALUES (@direction,@date,@description)";
                        SqlCommand command = new SqlCommand(queryString, connection);
                        command.Parameters.AddWithValue("@direction", textBox1.Text);
                        command.Parameters.AddWithValue("@date", dateTimePicker1.Value);
                        command.Parameters.AddWithValue("@description", bytes);
                        command.ExecuteNonQuery();
                    }
                    MessageBox.Show("Напрямок додан");
                    Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void CreateDirectionForm_Load(object sender, EventArgs e)
        {
            try
            {

                if (direction_name != "")
                {
                    string queryString = $"SELECT [direction],[description],[date] FROM [directions]  WHERE [direction]<>'None' AND [direction]='{direction_name}'";
                    SqlCommand command = new SqlCommand(queryString, connection);

                    SqlDataReader reader = command.ExecuteReader();

                    string description = "";

                    DateTime q = new DateTime();
                    while (reader.Read())
                    {
                        textBox1.Text = reader[0].ToString();
                        try
                        {
                            description = Encoding.Unicode.GetString((byte[])reader["description"]);
                        }
                        catch
                        {

                        }
                        q = Convert.ToDateTime(reader["date"]);
                    }
                    reader.Close();

                    string date = q.ToString("d");
                    label1.Text = "Дата початку презентацій: " + date;
                    textBox2.Text = description;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
