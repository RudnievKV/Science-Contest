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

namespace LB_2.UserForms
{
    public partial class AddProjectFormUser : Form
    {
        public int user_id;
        static private string connectionString =
            "Data Source=DESKTOP-JKS2HE1;Initial Catalog=ScienceContest;"
            + "Integrated Security=true";
        SqlConnection connection = new SqlConnection(connectionString);
        private List<DirectionAndThemes> s = new List<DirectionAndThemes>();
        private DataTable All_users = new DataTable();
        public DataTable Selected_users = new DataTable();
        public string projectName = "";
        public byte[] bytes_desc = new byte[0];
        public int project_id = 0;
        public int submit = 0;
        public AddProjectFormUser()
        {
            InitializeComponent();
            connection.Open();
        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
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

        private void AddProjectForm_Load(object sender, EventArgs e)
        {
            try
            {

                DataTable ListOfDirections = new DataTable();
                string queryString = "SELECT [direction] FROM [directions] WHERE [direction]<>'None'";
                SqlCommand command = new SqlCommand(queryString, connection);


                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(ListOfDirections);



                int j = 0;
                foreach (DataRow i in ListOfDirections.Rows)
                {
                    DirectionAndThemes temp = new DirectionAndThemes();
                    s.Add(temp);
                    s[j].Theme = new List<string>();
                    s[j].Direction = i[0].ToString();
                    j++;
                }

                queryString = "SELECT [direction],[theme] FROM [directionAndTheme] WHERE [direction]<>'None'";
                command = new SqlCommand(queryString, connection);

                SqlDataReader reader = command.ExecuteReader();

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

                comboBox1.SelectedIndex = 0;
                comboBox2.SelectedIndex = 0;


                queryString = $"SELECT [role].[role_name] FROM [user] INNER JOIN [role] ON ([user].[id_role]=[role].[role_id]) WHERE [user].[user_id]={user_id}";
                command = new SqlCommand(queryString, connection);

                reader = command.ExecuteReader();
                string role_name = "";
                while (reader.Read())
                {
                    role_name = reader[0].ToString();
                }
                reader.Close();

                if (role_name == "Admin")
                {
                    queryString = $"SELECT user_id AS [ID Користувача],lastName AS [Прізвище],firstName AS [Ім'я],middleName AS [По батькові] FROM [user] WHERE [id_role]=123211";
                    command = new SqlCommand(queryString, connection);
                    adapter = new SqlDataAdapter(command);
                    adapter.Fill(Selected_users);
                    dataGridView2.DataSource = Selected_users;
                }
                else
                {
                    queryString = $"SELECT user_id AS [ID Користувача],lastName AS [Прізвище],firstName AS [Ім'я],middleName AS [По батькові] FROM [user] WHERE user_id={user_id}";
                    command = new SqlCommand(queryString, connection);
                    adapter = new SqlDataAdapter(command);
                    adapter.Fill(Selected_users);
                    dataGridView2.DataSource = Selected_users;
                }

                queryString = $"SELECT [user].user_id AS [ID Користувача],[user].lastName AS [Прізвище],[user].firstName AS [Ім'я],[user].middleName AS [По батькові] FROM [user] INNER JOIN [group] ON ([group].[user_id]=[user].[user_id]) WHERE [group].[project_id]=1 AND [user].[user_id]<>{user_id}";
                command = new SqlCommand(queryString, connection);
                adapter = new SqlDataAdapter(command);
                adapter.Fill(All_users);
                dataGridView1.DataSource = All_users;


                




            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void AddProjectForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            connection.Close();
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

        private void button4_Click(object sender, EventArgs e)
        {
            ProjectDescriptionForm f = new ProjectDescriptionForm();
            f.project_id = project_id;
            f.bytes = bytes_desc;
            f.ShowDialog();
            bytes_desc = f.bytes;



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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView2.Rows.Count > 5 || dataGridView2.Rows.Count < 2)
                {
                    MessageBox.Show("Кількість учасників має варіюватися від 2 до 5 осіб включно", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (textBox1.Text == "" || comboBox1.SelectedIndex == -1 || comboBox2.SelectedIndex == -1)
                {
                    MessageBox.Show("Заповніть всі поля вводу даних", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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



                if (pictureBox1.Image != null && bytes_desc.Length != 0)
                {
                    Image img = pictureBox1.Image;
                    byte[] arr;
                    ImageConverter converter = new ImageConverter();
                    arr = (byte[])converter.ConvertTo(img, typeof(byte[]));
                    queryString = $"INSERT INTO [ScienceContest].[dbo].[project]([project_name],[directionAndTheme_id], [grade], [img], [description]) VALUES (@Project_name,@DirectionAndTheme,@Grade,@Img,@description)";
                    command = new SqlCommand(queryString, connection);
                    command.Parameters.AddWithValue("@Project_name", textBox1.Text);
                    command.Parameters.AddWithValue("@DirectionAndTheme", id);
                    command.Parameters.AddWithValue("@Grade", 0);
                    command.Parameters.AddWithValue("@Img", arr);
                    command.Parameters.AddWithValue("@description", bytes_desc);
                    command.ExecuteNonQuery();
                }
                else if (pictureBox1.Image != null && bytes_desc.Length == 0)
                {
                    Image img = pictureBox1.Image;
                    byte[] arr;
                    ImageConverter converter = new ImageConverter();
                    arr = (byte[])converter.ConvertTo(img, typeof(byte[]));
                    queryString = $"INSERT INTO [ScienceContest].[dbo].[project]([project_name],[directionAndTheme_id], [grade], [img]) VALUES (@Project_name,@DirectionAndTheme,@Grade,@Img)";
                    command = new SqlCommand(queryString, connection);
                    command.Parameters.AddWithValue("@Project_name", textBox1.Text);
                    command.Parameters.AddWithValue("@DirectionAndTheme", id);
                    command.Parameters.AddWithValue("@Grade", 0);
                    command.Parameters.AddWithValue("@Img", arr);
                    command.ExecuteNonQuery();
                }
                else if (pictureBox1.Image == null && bytes_desc.Length != 0)
                {
                    queryString = $"INSERT INTO [ScienceContest].[dbo].[project]([project_name],[directionAndTheme_id], [grade], [description]) VALUES(@Project_name, @DirectionAndTheme, @Grade, @description)";
                    command = new SqlCommand(queryString, connection);
                    command.Parameters.AddWithValue("@Project_name", textBox1.Text);
                    command.Parameters.AddWithValue("@DirectionAndTheme", id);
                    command.Parameters.AddWithValue("@Grade", 0);
                    command.Parameters.AddWithValue("@description", bytes_desc);
                    command.ExecuteNonQuery();
                }
                else if (pictureBox1.Image == null && bytes_desc.Length == 0)
                {
                    queryString = $"INSERT INTO [ScienceContest].[dbo].[project]([project_name],[directionAndTheme_id], [grade]) VALUES(@Project_name, @DirectionAndTheme, @Grade)";
                    command = new SqlCommand(queryString, connection);
                    command.Parameters.AddWithValue("@Project_name", textBox1.Text);
                    command.Parameters.AddWithValue("@DirectionAndTheme", id);
                    command.Parameters.AddWithValue("@Grade", 0);

                    command.ExecuteNonQuery();
                }




                projectName = textBox1.Text;
                /*
                textBox2.Text = "";
                textBox1.Text = "";
                pictureBox1.Image = null;
                txtProjectDescription = "";
                */
                submit = 1;
                MessageBox.Show("Проект додан");
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Close();
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            pictureBox1.Image.Dispose();
            pictureBox1.Image = null;
        }
    }
}
