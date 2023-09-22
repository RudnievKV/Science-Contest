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
using System.IO;

namespace LB_2.AdminForms
{
    public partial class AddUserForm : Form
    {
        static private string connectionString =
            "Data Source=DESKTOP-JKS2HE1;Initial Catalog=ScienceContest;"
            + "Integrated Security=true";
        SqlConnection connection = new SqlConnection(connectionString);
        public int User_id;
        private string email_first = "";
        public AddUserForm()
        {
            InitializeComponent();
            connection.Open();
        }

        private void AddUserForm_Load(object sender, EventArgs e)
        {
            try
            {
                string queryString = $"SELECT [role_name] FROM [role]";
                SqlCommand command = new SqlCommand(queryString, connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    comboBox2.Items.Add(reader[0].ToString());
                }
                reader.Close();

                queryString = $"SELECT [direction] FROM [directions] WHERE [direction]<>'None'";
                command = new SqlCommand(queryString, connection);
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    comboBox1.Items.Add(reader[0].ToString());
                }
                reader.Close();



                if (User_id != 0)
                {

                    string query_id = $"SELECT [email],[password],[firstName],[middleName],[lastName],[dateOfBirth],[id_role] FROM [user] WHERE [user_id]={User_id}";
                    command = new SqlCommand(query_id, connection);
                    reader = command.ExecuteReader();
                    int id_role = 0;

                    while (reader.Read())
                    {
                        txtBoxEmail.Text = reader[0].ToString();
                        txtBoxPassword.Text = reader[1].ToString();
                        txtBoxName.Text = reader[2].ToString();
                        txtBoxLastName.Text = reader[3].ToString();
                        txtBoxMiddleName.Text = reader[4].ToString();
                        dateTimePicker1.Value = Convert.ToDateTime(reader[5]);
                        id_role = Int32.Parse(reader[6].ToString());
                    }
                    reader.Close();

                    email_first = txtBoxEmail.Text;

                    query_id = $"SELECT [role_name] FROM [role] WHERE [role_id]={id_role}";
                    command = new SqlCommand(query_id, connection);
                    reader = command.ExecuteReader();
                    string role_name = "";
                    while (reader.Read())
                    {
                        role_name = reader[0].ToString();
                    }
                    reader.Close();
                    comboBox2.SelectedIndex = comboBox2.FindStringExact(role_name);

                    

                    query_id = $"SELECT [Prefer_direction] FROM [user] WHERE [user_id]={User_id}";
                    command = new SqlCommand(query_id, connection);
                    reader = command.ExecuteReader();
                    string direction = "";
                    while (reader.Read())
                    {
                        direction = reader[0].ToString();                 
                    }
                    reader.Close();
                    comboBox1.SelectedIndex = comboBox1.Items.IndexOf(direction);


                    DataSet ds = new DataSet();

                    SqlCommand cmd = new SqlCommand($"SELECT [img] FROM [user] WHERE [user_id]={User_id}", connection);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);

                    var q = cmd.ExecuteScalar();



                    try
                    {
                        if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0].ItemArray[0].ToString() != "")
                        {
                            Byte[] bytes = new Byte[0];

                            bytes = (Byte[])(ds.Tables[0].Rows[0].ItemArray[0]);

                            MemoryStream memStream = new MemoryStream(bytes);

                            Bitmap myImage = new Bitmap(memStream);
                            pictureBox1.Image = Image.FromStream(memStream);

                        }
                    }
                    catch
                    {

                    }

                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
        }

        private void AddUserForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            connection.Close();
        }

        private void AddUserBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtBoxEmail.Text == "" || txtBoxLastName.Text == "" || txtBoxMiddleName.Text == "" || txtBoxName.Text == "" || txtBoxPassword.Text == "" || comboBox1.SelectedIndex == -1 || comboBox2.SelectedIndex == -1)
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
                string queryString = $"SELECT COUNT([email]),[email] FROM [user] WHERE [email]='{txtBoxEmail.Text}' GROUP BY [email]";
                SqlCommand command = new SqlCommand(queryString, connection);
                SqlDataReader reader = command.ExecuteReader();
                int q = 0;
                string email = "";
                while (reader.Read())
                {
                    q = Int32.Parse(reader[0].ToString());
                    email = reader[1].ToString();
                }
                reader.Close();

                

                if (User_id != 0)
                {
                    if (q > 0 && email != email_first)
                    {
                        MessageBox.Show("Користувач з таким емейлом вже є!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    queryString = $"SELECT [role_id] FROM [role] WHERE [role_name]='{comboBox2.SelectedItem.ToString()}'";
                    command = new SqlCommand(queryString, connection);
                    reader = command.ExecuteReader();
                    int role_id = 6;
                    while (reader.Read())
                    {
                        role_id = Int32.Parse(reader[0].ToString());
                    }
                    reader.Close();
                    Image img = pictureBox1.Image;
                    byte[] arr;
                    ImageConverter converter = new ImageConverter();
                    arr = (byte[])converter.ConvertTo(img, typeof(byte[]));
                    queryString = $"UPDATE [user] SET [email]=@email,[password]=@password,[firstName]=@firstName,[middleName]=@middleName,[lastName]=@lastName,[id_role]=@id_role,[dateOfBirth]=@dateOfBirth,[img]=@img,[Prefer_direction]=@Prefer_direction WHERE [user_id]={User_id}";
                    command = new SqlCommand(queryString, connection);
                    command.Parameters.AddWithValue("@email", txtBoxEmail.Text);
                    command.Parameters.AddWithValue("@password", txtBoxPassword.Text);
                    command.Parameters.AddWithValue("@firstName", txtBoxName.Text);
                    command.Parameters.AddWithValue("@middleName", txtBoxMiddleName.Text);
                    command.Parameters.AddWithValue("@lastName", txtBoxLastName.Text);
                    command.Parameters.AddWithValue("@id_role", role_id);
                    command.Parameters.AddWithValue("@dateOfBirth", dateTimePicker1.Value);
                    command.Parameters.AddWithValue("@img", arr);
                    command.Parameters.AddWithValue("@Prefer_direction", comboBox1.SelectedItem.ToString());
                    command.ExecuteNonQuery();




                    MessageBox.Show("Користувач змінений");
                    Close();
                }
                else
                {
                    if (q > 0)
                    {
                        MessageBox.Show("Користувач з таким емейлом вже є!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    queryString = $"SELECT [role_id] FROM [role] WHERE [role_name]='{comboBox2.SelectedItem.ToString()}'";
                    command = new SqlCommand(queryString, connection);
                    reader = command.ExecuteReader();
                    int role_id = 6;
                    while (reader.Read())
                    {
                        role_id = Int32.Parse(reader[0].ToString());
                    }
                    reader.Close();
                    Image img = pictureBox1.Image;
                    byte[] arr;
                    ImageConverter converter = new ImageConverter();
                    arr = (byte[])converter.ConvertTo(img, typeof(byte[]));
                    queryString = $"INSERT INTO [ScienceContest].[dbo].[user]([email],[password], [firstName], [middleName], [lastName], [id_role], [dateOfBirth], [img],[Prefer_direction]) VALUES (@email,@password,@firstName,@middleName,@lastName,@id_role,@dateOfBirth,@img,@Prefer_direction)";
                    command = new SqlCommand(queryString, connection);
                    command.Parameters.AddWithValue("@email", txtBoxEmail.Text);
                    command.Parameters.AddWithValue("@password", txtBoxPassword.Text);
                    command.Parameters.AddWithValue("@firstName", txtBoxName.Text);
                    command.Parameters.AddWithValue("@middleName", txtBoxMiddleName.Text);
                    command.Parameters.AddWithValue("@lastName", txtBoxLastName.Text);
                    command.Parameters.AddWithValue("@id_role", role_id);
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



                    MessageBox.Show("Користувач доданий");
                    Close();
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
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

        private void button1_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
            pictureBox1.Image = null;
        }

        private void ExitBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
