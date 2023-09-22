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

namespace LB_2.AdminForms.Old
{
    public partial class CreateDirectionAndThemeForm : Form
    {
        private List<DirectionAndThemes> s = new List<DirectionAndThemes>();
        static private string connectionString =
    "Data Source=DESKTOP-JKS2HE1;Initial Catalog=ScienceContest;"
    + "Integrated Security=true";
        SqlConnection connection = new SqlConnection(connectionString);
        public int directionAndTheme_id;
        private byte[] bytes;
        public CreateDirectionAndThemeForm()
        {
            InitializeComponent();
            connection.Open();
        }

        private void CreateDirectionAndThemeForm_Load(object sender, EventArgs e)
        {
            try
            {
                if (directionAndTheme_id != 0)
                {
                    string queryString = "SELECT [direction] FROM [directions] WHERE [direction]<>'None'";
                    SqlCommand command = new SqlCommand(queryString, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        comboBox1.Items.Add(reader[0].ToString());
                    }
                    reader.Close();



                    queryString = "SELECT [theme] FROM [themes]  WHERE [theme]<>'None'";
                    command = new SqlCommand(queryString, connection);

                    reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        comboBox2.Items.Add(reader[0].ToString());
                    }
                    reader.Close();



                    queryString = $"SELECT [direction],[theme],[description] FROM [directionAndTheme] WHERE [direction]<>'None' AND [directionAndTheme_id]={directionAndTheme_id}";
                    command = new SqlCommand(queryString, connection);
                    reader = command.ExecuteReader();

                    string direction = "";
                    string theme = "";
                    string description = "";

                    while (reader.Read())
                    {
                        direction = reader[0].ToString();
                        theme = reader[1].ToString();
                        try
                        {
                            description = Encoding.Unicode.GetString((byte[])reader["description"]);
                        }
                        catch
                        {

                        }

                    }
                    reader.Close();

                    comboBox1.SelectedIndex = comboBox1.FindStringExact(direction);
                    comboBox2.SelectedIndex = comboBox2.FindStringExact(theme);
                    
                    
                    
                    textBox1.Text = description;


                    bytes = Encoding.Unicode.GetBytes(textBox1.Text);

                }
                else
                {
                    string queryString = "SELECT [direction] FROM [directions] WHERE [direction]<>'None'";
                    SqlCommand command = new SqlCommand(queryString, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        comboBox1.Items.Add(reader[0].ToString());
                    }
                    reader.Close();


                    queryString = "SELECT [theme] FROM [themes]  WHERE [theme]<>'None'";
                    command = new SqlCommand(queryString, connection);

                    reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        comboBox2.Items.Add(reader[0].ToString());
                    }
                    reader.Close();
                }



                


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void CreateDirectionAndThemeForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            connection.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.SelectedIndex == -1 || comboBox2.SelectedIndex == -1)
                {
                    MessageBox.Show("Виберіть напрямок та тему", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (directionAndTheme_id != 0)
                {

                    if (textBox1.Text == "")
                    {
                        string queryString = $"UPDATE [directionAndTheme] SET direction='{comboBox1.SelectedItem.ToString()}',theme='{comboBox2.SelectedItem.ToString()}' WHERE [directionAndTheme_id]={directionAndTheme_id}";
                        SqlCommand command = new SqlCommand(queryString, connection);
                        command.ExecuteNonQuery();
                    }
                    else
                    {
                        bytes = Encoding.Unicode.GetBytes(textBox1.Text);
                        string queryString = $"UPDATE [directionAndTheme] SET direction=@direction,theme=@theme,description=@description WHERE [directionAndTheme_id]={directionAndTheme_id}";
                        SqlCommand command = new SqlCommand(queryString, connection);
                        command.Parameters.AddWithValue("@direction", comboBox1.SelectedItem.ToString());
                        command.Parameters.AddWithValue("@theme", comboBox2.SelectedItem.ToString());
                        command.Parameters.AddWithValue("@description", bytes);
                        command.ExecuteNonQuery();
                    }
                    MessageBox.Show("Напрямок з темою оновлені");
                    Close();
                }
                else
                {
                    
                    if (textBox1.Text == "")
                    {
                        string queryString = $"INSERT INTO [directionAndTheme] (direction,theme) VALUES ('{comboBox1.SelectedItem.ToString()}','{comboBox2.SelectedItem.ToString()}')";
                        SqlCommand command = new SqlCommand(queryString, connection);
                        command.ExecuteNonQuery();
                    }
                    else
                    {
                        bytes = Encoding.Unicode.GetBytes(textBox1.Text);
                        string queryString = $"INSERT INTO [directionAndTheme] (direction,theme,description) VALUES (@direction,@theme,@description)";
                        SqlCommand command = new SqlCommand(queryString, connection);
                        command.Parameters.AddWithValue("@direction", comboBox1.SelectedItem.ToString());
                        command.Parameters.AddWithValue("@theme", comboBox2.SelectedItem.ToString());
                        command.Parameters.AddWithValue("@description",bytes);
                        command.ExecuteNonQuery();
                    }
                    MessageBox.Show("Напрямок з темою додані");
                    Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
