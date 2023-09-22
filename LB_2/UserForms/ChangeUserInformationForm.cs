using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LB_2.UserForms
{
    public partial class ChangeUserInformationForm : Form
    {
        static private string connectionString =
            "Data Source=DESKTOP-JKS2HE1;Initial Catalog=ScienceContest;"
            + "Integrated Security=true";
        SqlConnection connection = new SqlConnection(connectionString);
        public int User_id;
        public ChangeUserInformationForm()
        {
            InitializeComponent();
            connection.Open();
        }



            private void ChangeUserInformationForm_Load(object sender, EventArgs e)
        {
            try
            {
                string query_id = $"SELECT [email],[password],[firstName],[middleName],[lastName],[dateOfBirth] FROM [user] WHERE [user_id]={User_id}";
                SqlCommand command = new SqlCommand(query_id, connection);
                SqlDataReader reader = command.ExecuteReader();
                
                string date = "";
                while (reader.Read())
                {
                    txtBoxEmail.Text = reader[0].ToString();
                    txtBoxPassword.Text = reader[1].ToString();
                    txtBoxName.Text = reader[2].ToString();
                    txtBoxLastName.Text = reader[3].ToString();
                    txtBoxMiddleName.Text = reader[4].ToString();
                    dateTimePicker1.Value = Convert.ToDateTime(reader[5]);
                }
                reader.Close();

                query_id = $"SELECT [direction] FROM [directions] WHERE [direction]<>'None'";
                command = new SqlCommand(query_id, connection);
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    comboBox1.Items.Add(reader[0].ToString());
                }
                reader.Close();

                query_id = $"SELECT [Prefer_direction] FROM [user] WHERE [user_id]={User_id}";
                command = new SqlCommand(query_id, connection);
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    comboBox1.SelectedIndex = comboBox1.Items.IndexOf(reader[0].ToString());
                }
                reader.Close();

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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ChangeUserInformationForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            connection.Close();
        }

        private void AddUserBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtBoxEmail.Text == "" || txtBoxLastName.Text == "" || txtBoxMiddleName.Text == "" || txtBoxName.Text == "" || txtBoxPassword.Text == "" || comboBox1.SelectedIndex == -1)
                {
                    MessageBox.Show("Заповніть всі необхідні поля", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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



                queryString = $"SELECT [id_role] FROM [user] WHERE [user_id]={User_id}";
                command = new SqlCommand(queryString, connection);
                reader = command.ExecuteReader();
                string id_role = "";
                while (reader.Read())
                {
                    id_role = reader[0].ToString();
                }
                reader.Close();

                Image img = pictureBox1.Image;
                byte[] arr;
                ImageConverter converter = new ImageConverter();
                arr = (byte[])converter.ConvertTo(img, typeof(byte[]));
                queryString = $"UPDATE [user] SET [email]=@email,[password]=@password, [firstName]=@firstName, [middleName]=@middleName, [lastName]=@lastName, [id_role]=@id_role, [dateOfBirth]=@dateOfBirth, [img]=@img, [Prefer_direction]=@Prefer_direction WHERE [user_id]={User_id}";
                command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@email", txtBoxEmail.Text);
                command.Parameters.AddWithValue("@password", txtBoxPassword.Text);
                command.Parameters.AddWithValue("@firstName", txtBoxName.Text);
                command.Parameters.AddWithValue("@middleName", txtBoxMiddleName.Text);
                command.Parameters.AddWithValue("@lastName", txtBoxLastName.Text);
                command.Parameters.AddWithValue("@id_role", id_role);
                command.Parameters.AddWithValue("@dateOfBirth", dateTimePicker1.Value);
                command.Parameters.AddWithValue("@img", arr);
                command.Parameters.AddWithValue("@Prefer_direction", comboBox1.SelectedItem.ToString());
                command.ExecuteNonQuery();



                MessageBox.Show("Користувач змінен");
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            pictureBox1.Image = null;
            textBox2.Text = "";

            
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

        private void ExitBtn_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
