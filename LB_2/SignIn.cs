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
using LB_2.UserForms;
using LB_2.JuryForms;
using Microsoft.Win32.TaskScheduler;

namespace LB_2
{
    public partial class SignIn : Form
    {
        static private string connectionString =
    "Data Source=DESKTOP-JKS2HE1;Initial Catalog=ScienceContest;"
    + "Integrated Security=true";
        SqlConnection connection = new SqlConnection(connectionString);
        public SignIn()
        {
            InitializeComponent();
            connection.Open();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                if (textBox1.Text == "" || textBox2.Text == "")
                {
                    MessageBox.Show("Введите емейл и пароль пожалуйста.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                string query_id = $"SELECT [role].[role_permission],[user].user_id FROM [user] INNER JOIN [role] ON ([user].id_role=role.role_id) WHERE [user].[email]='{textBox1.Text}' AND [user].[password]='{textBox2.Text}'";
                SqlCommand command = new SqlCommand(query_id, connection);
                SqlDataReader reader = command.ExecuteReader();
                string permission = "";
                int user_id = 0;
                while (reader.Read())
                {
                    permission = reader[0].ToString();
                    user_id = Int32.Parse(reader[1].ToString());
                }
                reader.Close();
                
                if (permission == "")
                {
                    MessageBox.Show("Пользователя с таким емейлом и паролем не существует", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                if (permission == "2")
                {
                    AdminForm f = new AdminForm();
                    f.User_id = user_id;
                    f.Owner = this;
                    this.Visible = false;
                    f.ShowDialog();
                }
                if (permission == "1")
                {
                    UserMainForm f = new UserMainForm();
                    f.User_id = user_id;
                    f.Owner = this;
                    this.Visible = false;
                    f.ShowDialog();
                }
                if (permission == "3")
                {
                    JuryForm f = new JuryForm();
                    f.User_id = user_id;
                    f.Owner = this;
                    this.Visible = false;
                    f.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }


        }

        private void SignIn_Load(object sender, EventArgs e)
        {
            using (TaskService ts = new TaskService())
            {
                // Create a new task definition and assign properties
                TaskDefinition td = ts.NewTask();
                td.RegistrationInfo.Description = "Does something";

                // Create a trigger that will fire the task at this time every other day
                td.Triggers.Add(new DailyTrigger { DaysInterval = 1 });

                // Create an action that will launch Notepad whenever the trigger fires
                td.Actions.Add(new ExecAction("Email_Notification.exe", null, null));

                // Register the task in the root folder
                ts.RootFolder.RegisterTaskDefinition(@"Test", td);

                // Remove the task we just created
                // ts.RootFolder.DeleteTask("Test");
            }
        }

        private void SignIn_FormClosed(object sender, FormClosedEventArgs e)
        {
            connection.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RegistrationForm f = new RegistrationForm();
            f.ShowDialog();


        }
    }
}
