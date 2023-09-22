using LB_2.UserForms;
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

namespace LB_2.AdminForms
{
    public partial class ChangeProjectFormAdmin : Form
    {
        public int user_id;
        static private string connectionString =
            "Data Source=DESKTOP-JKS2HE1;Initial Catalog=ScienceContest;"
            + "Integrated Security=true";
        SqlConnection connection = new SqlConnection(connectionString);
        private List<DirectionAndThemes> s = new List<DirectionAndThemes>();
        public byte[] bytes_desc = new byte[0];
        private DataTable All_users = new DataTable();
        public DataTable Selected_users = new DataTable();
        public string projectName = "";
        public int project_id = 0;
        public int submit = 0;
        private string grade;
        int[] arrUsers;
        public ChangeProjectFormAdmin()
        {
            InitializeComponent();
            connection.Open();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ProjectDescriptionForm f = new ProjectDescriptionForm();
            f.project_id = project_id;
            f.bytes = bytes_desc; 
            f.ShowDialog();
            bytes_desc = f.bytes;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            pictureBox1.Image.Dispose();
            pictureBox1.Image = null;
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

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView2.Rows.Count > 5 || dataGridView2.Rows.Count < 2)
                {
                    MessageBox.Show("Кількість учасників проекту має варіюватися від 2 до 5 осіб включно", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (textBox1.Text == "" || comboBox1.SelectedIndex == -1 || comboBox2.SelectedIndex == -1 || comboBox3.SelectedIndex == -1)
                {
                    MessageBox.Show("Заповніть всі поля", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }





                string id = "";
                string queryString = $"SELECT [directionAndTheme_id] FROM [directionAndTheme] WHERE (direction='{comboBox1.SelectedItem.ToString()}') AND (theme='{comboBox2.SelectedItem.ToString()}')";
                SqlCommand command = new SqlCommand(queryString, connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    id = reader[0].ToString();
                }
                reader.Close();
                byte[] q = new byte[0];



                if (bytes_desc.Length == 0)
                {
                    queryString = $"SELECT [description] FROM [project] WHERE [project_id]={project_id}";
                    command = new SqlCommand(queryString, connection);
                    reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        try
                        {
                            bytes_desc = (byte[])reader["description"];
                        }
                        catch
                        {

                        }
                    }
                    reader.Close();

                }



                if (pictureBox1.Image != null && bytes_desc.Length == 0)
                {
                    Image img = pictureBox1.Image;
                    byte[] arr;
                    ImageConverter converter = new ImageConverter();
                    arr = (byte[])converter.ConvertTo(img, typeof(byte[]));
                    queryString = $"UPDATE [project] SET [project_name]=@Project_name,[directionAndTheme_id]=@DirectionAndTheme,[grade]=@Grade,[img]=@Img,[description]=@description WHERE [project_id]={project_id}";
                    command = new SqlCommand(queryString, connection);
                    command.Parameters.AddWithValue("@Project_name", textBox1.Text);
                    command.Parameters.AddWithValue("@DirectionAndTheme", id);
                    command.Parameters.AddWithValue("@Grade", comboBox3.SelectedItem.ToString());
                    command.Parameters.AddWithValue("@Img", arr);
                    command.Parameters.AddWithValue("@description", bytes_desc);
                    command.ExecuteNonQuery();
                }
                else if (pictureBox1.Image != null && bytes_desc.Length != 0)
                {
                    Image img = pictureBox1.Image;
                    byte[] arr;
                    ImageConverter converter = new ImageConverter();
                    arr = (byte[])converter.ConvertTo(img, typeof(byte[]));
                    queryString = $"UPDATE [project] SET [project_name]=@Project_name,[directionAndTheme_id]=@DirectionAndTheme,[grade]=@Grade,[img]=@Img,[description]=@description WHERE [project_id]={project_id}";
                    command = new SqlCommand(queryString, connection);
                    command.Parameters.AddWithValue("@Project_name", textBox1.Text);
                    command.Parameters.AddWithValue("@DirectionAndTheme", id);
                    command.Parameters.AddWithValue("@Grade", comboBox3.SelectedItem.ToString());
                    command.Parameters.AddWithValue("@Img", arr);
                    command.Parameters.AddWithValue("@description", bytes_desc);
                    command.ExecuteNonQuery();
                }
                else if (pictureBox1.Image == null && bytes_desc.Length != 0)
                {

                    queryString = $"UPDATE [project] SET [project_name]=@Project_name,[directionAndTheme_id]=@DirectionAndTheme,[grade]=@Grade,[img]=@Img,[description]=@description WHERE [project_id]={project_id}";
                    command = new SqlCommand(queryString, connection);
                    command.Parameters.AddWithValue("@Project_name", textBox1.Text);
                    command.Parameters.AddWithValue("@DirectionAndTheme", id);
                    command.Parameters.AddWithValue("@Grade", comboBox3.SelectedItem.ToString());
                    command.Parameters.AddWithValue("@Img", q);
                    command.Parameters.AddWithValue("@description", bytes_desc);
                    command.ExecuteNonQuery();
                }
                else if (pictureBox1.Image == null && bytes_desc.Length == 0)
                {
                    queryString = $"UPDATE [project] SET [project_name]=@Project_name,[directionAndTheme_id]=@DirectionAndTheme,[grade]=@Grade,[img]=@Img,[description]=@description WHERE [project_id]={project_id}";
                    command = new SqlCommand(queryString, connection);
                    command.Parameters.AddWithValue("@Project_name", textBox1.Text);
                    command.Parameters.AddWithValue("@DirectionAndTheme", id);
                    command.Parameters.AddWithValue("@Grade", comboBox3.SelectedItem.ToString());
                    command.Parameters.AddWithValue("@Img", q);
                    command.Parameters.AddWithValue("@description", bytes_desc);
                    command.ExecuteNonQuery();
                }

                for (int i = 0; i < dataGridView2.Rows.Count; i++)
                {
                    queryString = $"UPDATE [group] SET [project_id]={project_id} WHERE [user_id]={dataGridView2.Rows[i].Cells[0].Value.ToString()}";
                    command = new SqlCommand(queryString, connection);
                    command.ExecuteNonQuery();
                }

                bool kekleo = false;
                
                foreach(int j in arrUsers)
                {
                    for (int i = 0; i < dataGridView2.Rows.Count; i++)
                    {
                        if (j == Int32.Parse(dataGridView2.Rows[i].Cells[0].Value.ToString()))
                        {
                            kekleo = true;
                        }
                    }
                    if (kekleo == false)
                    {
                        queryString = $"UPDATE [group] SET [project_id]=1 WHERE [user_id]={j}";
                        command = new SqlCommand(queryString, connection);
                        command.ExecuteNonQuery();
                    }
                    kekleo = false;
                }

                projectName = textBox1.Text;
                /*
                textBox2.Text = "";
                textBox1.Text = "";
                pictureBox1.Image = null;
                txtProjectDescription = "";
                */
                submit = 1;
                MessageBox.Show("Проект змінен");
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                int selected_user_id = Int32.Parse(dataGridView2.SelectedCells[0].Value.ToString());
                var selected_row_index = dataGridView2.SelectedRows[0].Index;
                var selected_row = Selected_users.Rows[selected_row_index];

                All_users.Rows.Add(selected_row.ItemArray);
                Selected_users.Rows.RemoveAt(selected_row_index);
            }
            catch
            {
                
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                int selected_user_id = Int32.Parse(dataGridView1.SelectedCells[0].Value.ToString());
                var selected_row_index = dataGridView1.SelectedRows[0].Index;
                var selected_row = All_users.Rows[selected_row_index];





                Selected_users.Rows.Add(selected_row.ItemArray);
                All_users.Rows.RemoveAt(selected_row_index);
            }
            catch
            {
               
            }
        }

        private void ChangeProjectFormAdmin_Load(object sender, EventArgs e)
        {
            try
            {
                



                DataTable ListOfDirections = new DataTable();

                string queryString = "SELECT [direction] FROM [directions] WHERE [direction]<>'None'";
                SqlCommand command = new SqlCommand(queryString, connection);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(ListOfDirections);


                string direction = "";
                string theme = "";
                string name = "";
                grade = "";
                queryString = $"SELECT [directionAndTheme].[direction],[directionAndTheme].[theme],[project].[project_name],[project].[grade] FROM [project] INNER JOIN [directionAndTheme] ON ([project].[directionAndTheme_id]=[directionAndTheme].[directionAndTheme_id]) WHERE [project].[project_id]={project_id} AND [directionAndTheme].[direction]<>'None'";
                command = new SqlCommand(queryString, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    direction = reader[0].ToString();
                    theme = reader[1].ToString();
                    name = reader[2].ToString();
                    grade = reader[3].ToString();
                }
                reader.Close();


                int j = 0;
                foreach (DataRow i in ListOfDirections.Rows)
                {
                    DirectionAndThemes temp = new DirectionAndThemes();
                    s.Add(temp);
                    s[j].Theme = new List<string>();
                    s[j].Direction = i[0].ToString();
                    j++;
                }

                queryString = "SELECT [direction],[theme] FROM [directionAndTheme] WHERE [directionAndTheme].[direction]<>'None'";
                command = new SqlCommand(queryString, connection);

                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    foreach (DirectionAndThemes i in s)
                    {
                        if (reader[0].ToString() == i.Direction)
                        {
                            i.Theme.Add(reader[1].ToString());
                        }
                    }
                }
                reader.Close();


                foreach (DirectionAndThemes i in s)
                {
                    comboBox1.Items.Add(i.Direction.ToString());
                }

                comboBox1.SelectedIndex = comboBox1.FindStringExact(direction);


                comboBox2.Enabled = true;


                
                comboBox2.SelectedIndex = comboBox2.FindStringExact(theme);
                comboBox3.Items.Add(0);
                comboBox3.Items.Add(1);
                comboBox3.Items.Add(2);
                comboBox3.Items.Add(3);
                comboBox3.Items.Add(4);
                comboBox3.Items.Add(5);
                comboBox3.SelectedIndex = comboBox3.FindStringExact(grade);

                textBox1.Text = name;

                

                DataSet ds = new DataSet();

                SqlCommand cmd = new SqlCommand($"SELECT [img] FROM [project] WHERE [project_id]={project_id}", connection);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);

                



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










                queryString = $"SELECT [user].user_id AS [ID Користувача],[user].lastName AS [Прізвище],[user].firstName AS [Ім'я],[user].middleName AS [По батькові] FROM [user] INNER JOIN [group] ON ([group].[user_id]=[user].[user_id]) WHERE [group].[project_id]=1";
                command = new SqlCommand(queryString, connection);
                adapter = new SqlDataAdapter(command);
                adapter.Fill(All_users);
                dataGridView1.DataSource = All_users;


                queryString = $"SELECT [user].[user_id] AS [ID Користувача],[user].lastName AS [Прізвище],[user].firstName AS [Ім'я],[user].middleName AS [По батькові] FROM [user] INNER JOIN [group] ON ([group].[user_id]=[user].[user_id]) WHERE [group].[project_id]={project_id}";
                command = new SqlCommand(queryString, connection);
                adapter = new SqlDataAdapter(command);
                adapter.Fill(Selected_users);
                dataGridView2.DataSource = Selected_users;


                arrUsers = new int[dataGridView2.Rows.Count];
                for(int i = 0; i < dataGridView2.Rows.Count; i++)
                {
                    arrUsers[i] = Int32.Parse(dataGridView2.Rows[i].Cells[0].Value.ToString());
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ChangeProjectFormAdmin_FormClosed(object sender, FormClosedEventArgs e)
        {
            connection.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            foreach (DirectionAndThemes i in s)
            {
                if (comboBox1.SelectedItem.ToString() == i.Direction)
                {
                    foreach (string q in i.Theme)
                    {
                        comboBox2.Items.Add(q);
                    }
                }
            }
            comboBox2.Enabled = true;
        }
    }
}
