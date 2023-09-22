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
    public partial class CreateThemeForm : Form
    {
        static private string connectionString =
"Data Source=DESKTOP-JKS2HE1;Initial Catalog=ScienceContest;"
+ "Integrated Security=true";
        SqlConnection connection = new SqlConnection(connectionString);
        private byte[] bytes;
        public string theme_name = "";

        public CreateThemeForm()
        {
            InitializeComponent();
            connection.Open();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void CreateThemeForm_Load(object sender, EventArgs e)
        {
            try
            {

                if (theme_name != "")
                {
                    string queryString = $"SELECT [theme],[description] FROM [themes]  WHERE [theme]<>'None' AND [theme]='{theme_name}'";
                    SqlCommand command = new SqlCommand(queryString, connection);

                    SqlDataReader reader = command.ExecuteReader();

                    string description = "";
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
                    }
                    reader.Close();

                    textBox2.Text = description;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text == "")
                {
                    MessageBox.Show("Введіть назву теми", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (theme_name != "")
                {
                    if (textBox2.Text == "")
                    {
                        string queryString = $"UPDATE [themes] SET [theme]='{textBox1.Text}' WHERE [theme]='{theme_name}'";
                        SqlCommand command = new SqlCommand(queryString, connection);
                        command.ExecuteNonQuery();
                    }
                    else
                    {
                        bytes = Encoding.Unicode.GetBytes(textBox2.Text);
                        string queryString = $"UPDATE [themes] SET [theme]=@theme,[description]=@description WHERE [theme]='{theme_name}'";
                        SqlCommand command = new SqlCommand(queryString, connection);
                        command.Parameters.AddWithValue("@theme", textBox1.Text);
                        command.Parameters.AddWithValue("@description", bytes);
                        command.ExecuteNonQuery();
                    }
                    MessageBox.Show("Тема оновлена");
                    Close();
                }
                else
                {
                    if (textBox2.Text == "")
                    {
                        string queryString = $"INSERT INTO [themes] ([theme]) VALUES ('{textBox1.Text}')";
                        SqlCommand command = new SqlCommand(queryString, connection);
                        command.ExecuteNonQuery();
                    }
                    else
                    {
                        bytes = Encoding.Unicode.GetBytes(textBox2.Text);
                        string queryString = $"INSERT INTO [themes] ([theme],[description]) VALUES (@theme,@description)";
                        SqlCommand command = new SqlCommand(queryString, connection);
                        command.Parameters.AddWithValue("@theme", textBox1.Text);
                        command.Parameters.AddWithValue("@description", bytes);
                        command.ExecuteNonQuery();
                    }
                    MessageBox.Show("Тема додана");
                    Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void CreateThemeForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            connection.Close();
        }
    }
}
