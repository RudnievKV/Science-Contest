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

namespace LB_2.UserForms
{
    public partial class InformationProjectLeaveForm : Form
    {
        static private string connectionString =
            "Data Source=DESKTOP-JKS2HE1;Initial Catalog=ScienceContest;"
            + "Integrated Security=true";
        SqlConnection connection = new SqlConnection(connectionString);
        public string project_id;
        public int User_id;
        string project_description = "";
        public byte[] bytes_desc = new byte[0];
        public InformationProjectLeaveForm()
        {
            InitializeComponent();
            connection.Open();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void InformationProjectLeaveForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            connection.Close();
        }

        private void labelDesc_Click(object sender, EventArgs e)
        {

        }

        private void InformationProjectLeaveForm_Load(object sender, EventArgs e)
        {
            string query_id = "SELECT "
            + "[project].[project_name]"
            + ",[directionAndTheme].[direction]"
            + ",[directionAndTheme].[theme]"
            + ",[project].[grade]"
            + ",[project].[description]"
            + "FROM "
            + "[dbo].[project] INNER JOIN [dbo].[directionAndTheme] "
            + $"ON([dbo].[project].[directionAndTheme_id] =[dbo].[directionAndTheme].[directionAndTheme_id]) WHERE [project].[project_id]={project_id}";
            SqlCommand command = new SqlCommand(query_id, connection);
            SqlDataReader reader = command.ExecuteReader();
            string project_name = "";
            string project_direction = "";
            string project_theme = "";
            string project_grade = "";
            project_description = "";
            while (reader.Read())
            {
                project_name = reader[0].ToString();
                project_direction = reader[1].ToString();
                project_theme = reader[2].ToString();
                project_grade = reader[3].ToString();
                try 
                {
                    project_description = Encoding.Unicode.GetString((byte[])reader[4]);
                }
                catch
                {

                }
                
            }
            reader.Close();


            query_id = $"SELECT [user].[lastName],[user].[firstName],[user].[middleName] FROM [user] INNER JOIN [group] ON ([group].[user_id]=[user].[user_id]) WHERE [group].[project_id]={project_id}";
            command = new SqlCommand(query_id, connection);
            reader = command.ExecuteReader();
            string user_firstName = "";
            string user_middleName = "";
            string user_lastName = "";
            while (reader.Read())
            {
                user_lastName = reader[0].ToString();
                user_firstName = reader[1].ToString();
                user_middleName = reader[2].ToString();
                labelPeople.Text += $"\n{user_lastName} {user_firstName} {user_middleName}";
            }
            reader.Close();



            SqlDataAdapter dataAdapter = new SqlDataAdapter(new SqlCommand($"SELECT img FROM [project] WHERE project_id={project_id}", connection));
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);

            if (dataSet.Tables[0].Rows[0].ItemArray[0].ToString() != "")
            {
                Byte[] data = new Byte[0];
                data = (Byte[])(dataSet.Tables[0].Rows[0]["img"]);
                MemoryStream mem = new MemoryStream(data);
                try
                {
                    pictureBox1.Image = Image.FromStream(mem);
                }
                catch
                {

                }
                
            }
            labelProjectName.Text = $"{project_name}";
            labelDirection.Text = $"Напрямок проекту: {project_direction}";
            labelTheme.Text = $"Тема проекту: {project_theme}";
            labelGrade.Text = $"Оцінка проекту: {project_grade}";





        }

        private void button1_Click(object sender, EventArgs e)
        {

            string query_id = $"UPDATE [group] SET [project_id]=1 WHERE [user_id]={User_id}";
            SqlCommand command = new SqlCommand(query_id, connection);
            command.ExecuteNonQuery();
            MessageBox.Show("Ви успішно покинули даний проект.");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ProjectDescriptionForm f = new ProjectDescriptionForm();
            f.textBox1.Text = project_description;
            f.Size = new Size(715, 398);
            f.button1.Visible = false;
            f.button2.Visible = false;
            f.textBox1.ReadOnly = true;

            f.project_id = Int32.Parse(project_id);
            f.bytes = bytes_desc;
            
            f.ShowDialog();
            bytes_desc = f.bytes;
        }
    }
}
