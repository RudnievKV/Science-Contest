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

namespace LB_2.UserForms
{
    public partial class ProjectDescriptionForm : Form
    {
        public string txt;
        static private string connectionString =
            "Data Source=DESKTOP-JKS2HE1;Initial Catalog=ScienceContest;"
            + "Integrated Security=true";
        SqlConnection connection = new SqlConnection(connectionString);
        public int project_id;
        string description = "";
        public byte[] bytes;
        public ProjectDescriptionForm()
        {
            InitializeComponent();
            connection.Open();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            bytes = Encoding.Unicode.GetBytes(textBox1.Text);
            MessageBox.Show("Опис проекту додан");
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ProjectDescriptionForm_Load(object sender, EventArgs e)
        {
            
            if (bytes.Length == 0)
            {
                string queryString = $"SELECT [description] FROM [project] WHERE [project_id]={project_id}";
                SqlCommand command = new SqlCommand(queryString, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    try
                    {
                        description = Encoding.Unicode.GetString((byte[])reader["description"]);
                        bytes = (byte[])reader["description"];
                    }
                    catch
                    {

                    }
                }
                reader.Close();
                textBox1.Text = description;

            }
            textBox1.Text = Encoding.Unicode.GetString(bytes);
            

        }

        private void ProjectDescriptionForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            connection.Close();
        }
    }
}
