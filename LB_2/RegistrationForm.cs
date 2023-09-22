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

namespace LB_2
{
    public partial class RegistrationForm : Form
    {
        static private string connectionString =
            "Data Source=DESKTOP-JKS2HE1;Initial Catalog=ScienceContest;"
            + "Integrated Security=true";
        SqlConnection connection = new SqlConnection(connectionString);
        public RegistrationForm()
        {
            InitializeComponent();
            connection.Open();
        }

        private void ExitBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "Image files(*.jpg;*.png)|*.jpg;*.png", Multiselect = false })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    pictureBox1.Image = Image.FromFile(ofd.FileName);
                    textBox2.Text = ofd.FileName;
                }
            }
        }

        private void AddUserBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtBoxEmail.Text == "" || txtBoxLastName.Text == "" || txtBoxMiddleName.Text == "" || txtBoxName.Text == "" || txtBoxPassword.Text == "" || comboBox1.SelectedIndex == -1)
                {
                    MessageBox.Show("Введіть всі дані будь ласка", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                if (!txtBoxEmail.Text.Contains("@gmail.com") && !txtBoxEmail.Text.Contains("@mail.ru") && !txtBoxEmail.Text.Contains("@yahoo.com") && !txtBoxEmail.Text.Contains("@hotmail.com") && !txtBoxEmail.Text.Contains("@yandex.ru"))
                {
                    MessageBox.Show("Введіть коректну емейл адресу будь ласка", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DateTime date = dateTimePicker1.Value;

                DateTime dateNow = DateTime.UtcNow;
                DateTime date18YearsAgo = dateNow.AddYears(-18);

                int a = date.CompareTo(date18YearsAgo);

                if (a != -1)
                {
                    MessageBox.Show("Реєструватися мають право тільки повнолітні люди", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string queryString = $"SELECT COUNT([email]) FROM [user] WHERE [email]='{txtBoxEmail.Text}'";
                SqlCommand command = new SqlCommand(queryString, connection);
                SqlDataReader reader = command.ExecuteReader();
                int q = 0;
                while (reader.Read())
                {
                    q = Int32.Parse(reader[0].ToString());
                }
                reader.Close();

                if (q > 0)
                {
                    MessageBox.Show("Користувач з таким емейлом вже є!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Image img = pictureBox1.Image;
                byte[] arr;
                ImageConverter converter = new ImageConverter();
                arr = (byte[])converter.ConvertTo(img, typeof(byte[]));
                queryString = $"INSERT INTO [ScienceContest].[dbo].[user]([email],[password], [firstName], [middleName], [lastName], [id_role], [dateOfBirth], [img], [Prefer_direction]) VALUES (@email,@password,@firstName,@middleName,@lastName,@id_role,@dateOfBirth,@img,@Prefer_direction)";
                command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@email", txtBoxEmail.Text);
                command.Parameters.AddWithValue("@password", txtBoxPassword.Text);
                command.Parameters.AddWithValue("@firstName", txtBoxName.Text);
                command.Parameters.AddWithValue("@middleName", txtBoxMiddleName.Text);
                command.Parameters.AddWithValue("@lastName", txtBoxLastName.Text);
                command.Parameters.AddWithValue("@id_role", 6);
                command.Parameters.AddWithValue("@dateOfBirth", dateTimePicker1.Value);
                command.Parameters.AddWithValue("@img", arr);
                command.Parameters.AddWithValue("@Prefer_direction", comboBox1.SelectedItem.ToString());
                command.ExecuteNonQuery();



                queryString = "SELECT [user_id] FROM [user] ORDER BY [user_id]";
                command = new SqlCommand(queryString, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable ListOfUserID = new DataTable();
                adapter.Fill(ListOfUserID);

                queryString = $"INSERT INTO [ScienceContest].[dbo].[group] ([group].[user_id], [group].[project_id]) VALUES({ListOfUserID.Rows[ListOfUserID.Rows.Count - 1].ItemArray[0].ToString()}, 1)";
                command = new SqlCommand(queryString, connection);
                command.ExecuteNonQuery();



                MessageBox.Show("Користувач зареєстрован успішно!");
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void RegistrationForm_Load(object sender, EventArgs e)
        {
            string queryString = $"SELECT [direction] FROM [directions] WHERE [direction]<>'None'";
            SqlCommand command = new SqlCommand(queryString, connection);
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                comboBox1.Items.Add(reader[0].ToString());
            }
            reader.Close();
        }

        private void RegistrationForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            connection.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
            pictureBox1.Image = null;
        }
    }
}
