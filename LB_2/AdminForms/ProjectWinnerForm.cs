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
    public partial class ProjectWinnerForm : Form
    {
        static private string connectionString =
    "Data Source=DESKTOP-JKS2HE1;Initial Catalog=ScienceContest;"
    + "Integrated Security=true";
        SqlConnection connection = new SqlConnection(connectionString);
        public int User_id;
        public int project_id;
        public ProjectWinnerForm()
        {
            InitializeComponent();
            connection.Open();
        }

        private void ProjectWinnerForm_Load(object sender, EventArgs e)
        {
            string queryString = "SELECT "
                + "[project].[project_name] as [Назва проекту] "
                + ",[directionAndTheme].[direction] as [Напрямок] "
                + ",[directionAndTheme].[theme] as [Тема] "
                + "FROM "
                + "[dbo].[project] INNER JOIN [dbo].[directionAndTheme] ON([dbo].[project].[directionAndTheme_id] =[dbo].[directionAndTheme].[directionAndTheme_id]) LEFT OUTER JOIN [group] ON ([group].[project_id]=[project].[project_id]) "
                + $"WHERE [project].[project_id]={project_id}";
            SqlCommand command = new SqlCommand(queryString, connection);
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                labelProjectName.Text = reader[0].ToString();
                labelDirection.Text = "Напрямок проекта: " + reader[1].ToString();
                labelTheme.Text = "Тема проекта: " + reader[2].ToString();

            }
            reader.Close();

            queryString = $"SELECT [user].[lastName],[user].[firstName],[user].[middleName] FROM [user] INNER JOIN [group] ON ([group].[user_id]=[user].[user_id]) WHERE [group].[project_id]={project_id}";
            command = new SqlCommand(queryString, connection);
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

            queryString = $"SELECT [user].[lastName],[user].[firstName],[user].[middleName] FROM [user] WHERE [user_id]={User_id}";
            command = new SqlCommand(queryString, connection);
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                user_lastName = reader[0].ToString();
                user_firstName = reader[1].ToString();
                user_middleName = reader[2].ToString();
                labelFIOAdmin.Text = $"{user_lastName} {user_firstName} {user_middleName} ";
            }
            reader.Close();

            label7.Text = DateTime.Now.Day.ToString() + "." + DateTime.Now.Month.ToString() + "." + DateTime.Now.Year.ToString() + ",Харьков";

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ProjectWinnerForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            connection.Close();
        }
        Bitmap bitmap;
        private void button1_Click(object sender, EventArgs e)
        {
            this.Size = new Size(634, 756);
            button1.Visible = false;
            button2.Visible = false;


            Panel panel = new Panel();
            this.Controls.Add(panel);

            Graphics graphics = panel.CreateGraphics();
            Size size = this.ClientSize;
            bitmap = new Bitmap(size.Width, size.Height, graphics);
            graphics = Graphics.FromImage(bitmap);

            Point point = PointToScreen(panel.Location);
            graphics.CopyFromScreen(point.X, point.Y, 0, 0, size);

            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();

            this.Size = new Size(634, 800);
            button1.Visible = true;
            button2.Visible = true;
            
        }

       
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(bitmap, 0, 0);
        }
    }
}
