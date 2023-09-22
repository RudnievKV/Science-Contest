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
using LB_2.AdminForms.Old;
using LB_2.AdminForms;

namespace LB_2.UserForms
{
    
    public partial class UserMainForm : Form
    {
        private static DataTable dataTable = new DataTable();
        static private string connectionString =
            "Data Source=DESKTOP-JKS2HE1;Initial Catalog=ScienceContest;"
            + "Integrated Security=true";
        SqlConnection connection = new SqlConnection(connectionString);
        private int flagWhatTableIsCurrentlyOnScreen = 1;
        public int User_id;
        public UserMainForm()
        {
            InitializeComponent();
            connection.Open();
        }

        private void UserMainForm_Load(object sender, EventArgs e)
        {
            try
            {

                dataTable.Rows.Clear();
                dataTable.Columns.Clear();
                string queryString = "SELECT [project].[project_id] as [ID Проекту] "
                + ",[project].[project_name] as [Назва проекту] "
                + ",[directionAndTheme].[direction] as [Напрямок] "
                + ",[directionAndTheme].[theme] as [Тема] "
                + ",[project].[grade] as [Оцінка] "
                + ",[project].[popularity] as [Число переглядів] "
                + ",COUNT([group].[user_id]) as [Кількість учасників в проекті]"
                + "FROM "
                + "[dbo].[project] INNER JOIN [dbo].[directionAndTheme] ON([dbo].[project].[directionAndTheme_id] =[dbo].[directionAndTheme].[directionAndTheme_id]) LEFT OUTER JOIN [group] ON ([group].[project_id]=[project].[project_id]) "
                + "WHERE [project].[project_name]<>'None'"
                + "GROUP BY [project].[project_id],[project].[project_name],[directionAndTheme].[direction],[directionAndTheme].[theme],[project].[grade],[project].[popularity],[group].[project_id]";
                SqlCommand command = new SqlCommand(queryString, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                adapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
                foreach (DataGridViewColumn dgvc in dataGridView1.Columns)
                {
                    dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
                }
                flagWhatTableIsCurrentlyOnScreen = 1;



                queryString = $"SELECT [firstName] FROM [user] WHERE [user_id]='{User_id}'";
                command = new SqlCommand(queryString, connection);
                SqlDataReader reader = command.ExecuteReader();
                string user_firstName = "";
                while (reader.Read())
                {
                    user_firstName = reader[0].ToString();
                }
                reader.Close();

                
                this.Text = "Добридень, " + user_firstName + "!";



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void UserMainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            connection.Close();
            Application.Exit();
        }



        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string project_id_selected = dataGridView1.SelectedCells[0].Value.ToString();
            string query_id = "";
            SqlCommand command;
            if (flagWhatTableIsCurrentlyOnScreen == 1 || flagWhatTableIsCurrentlyOnScreen == 2)
            {
                query_id = $"UPDATE [project] SET [popularity]=[popularity]+1 WHERE [project_id]={project_id_selected}";
                command = new SqlCommand(query_id, connection);
                command.ExecuteNonQuery();
            }
            

            InformationProjectLeaveForm f;
            InformationProjectJoinForm f1;

            if (flagWhatTableIsCurrentlyOnScreen == 2)
            {
                f = new InformationProjectLeaveForm();
                f.project_id = project_id_selected;
                f.User_id = User_id;
                f.ShowDialog();


            }
            else if (flagWhatTableIsCurrentlyOnScreen == 1)
            {
                

                DataTable usersInProjectTable = new DataTable();
                query_id = $"SELECT [user_id] FROM [group] WHERE [project_id]={project_id_selected}";
                command = new SqlCommand(query_id, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(usersInProjectTable);

                for (int i = 0; i < usersInProjectTable.Rows.Count; i++)
                {
                    if (usersInProjectTable.Rows[i].ItemArray[0].ToString() == $"{User_id}")
                    {
                        f = new InformationProjectLeaveForm();
                        f.project_id = project_id_selected;
                        f.User_id = User_id;
                        f.ShowDialog();
                        return;
                    }
                }

                f1 = new InformationProjectJoinForm();
                f1.project_id = project_id_selected;
                f1.User_id = User_id;
                f1.ShowDialog();
            }
            else if (flagWhatTableIsCurrentlyOnScreen == 3)
            {
                CreateDirectionAndThemeForm ff = new CreateDirectionAndThemeForm();
                ff.button1.Visible = false;
                ff.textBox1.ReadOnly = true;
                ff.comboBox1.Visible = false;
                ff.comboBox2.Visible = false;
                ff.directionAndTheme_id = Int32.Parse(dataGridView1.SelectedCells[0].Value.ToString());

                query_id = $"SELECT [direction],[theme] FROM [directionAndTheme] WHERE [directionAndTheme_id]={dataGridView1.SelectedCells[0].Value.ToString()}";
                command = new SqlCommand(query_id, connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ff.label1.Text = "Напрямок: " + reader[0].ToString();
                    ff.label2.Text = "Тема: " + reader[1].ToString();
                }
                reader.Close();

                ff.ShowDialog();
            }
            else if (flagWhatTableIsCurrentlyOnScreen == 4)
            {
                CreateThemeForm f4 = new CreateThemeForm();
                f4.theme_name = dataGridView1.SelectedCells[0].Value.ToString();
                f4.textBox1.Visible = false;
                f4.textBox2.ReadOnly = true;
                f4.button1.Visible = false;
                f4.label2.Text = "Тема: " + dataGridView1.SelectedCells[0].Value.ToString();
                
                f4.ShowDialog();
            }
            else if (flagWhatTableIsCurrentlyOnScreen == 5)
            {
                CreateDirectionForm f5 = new CreateDirectionForm();
                f5.direction_name = dataGridView1.SelectedCells[0].Value.ToString();
                f5.textBox1.Visible = false;
                f5.textBox2.ReadOnly = true;
                f5.button1.Visible = false;


                f5.dateTimePicker1.Visible = false;
                f5.label1.Location = new Point(9, 41);
                f5.label3.Location = new Point(9, 80);
                f5.textBox2.Location = new Point(12, 112);
                f5.button2.Location = new Point(448, 413);
                f5.Size = new Size(674, 518);

                f5.label2.Text = "Напрямок: " + dataGridView1.SelectedCells[0].Value.ToString();



                f5.ShowDialog();
            }

        }

        



       

        

        private void exitAccountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Owner.Visible = true;
        }

        private void exitProgramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
            Application.Exit();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                dataTable.Rows.Clear();
                dataTable.Columns.Clear();
                string queryString = "SELECT [project].[project_id] as [ID Проекту] "
                + ",[project].[project_name] as [Назва проекту] "
                + ",[directionAndTheme].[direction] as [Напрямок] "
                + ",[directionAndTheme].[theme] as [Тема] "
                + ",[project].[grade] as [Оцінка] "
                + ",[project].[popularity] as [Число переглядів] "
                + ",COUNT([group].[user_id]) as [Кількість учасників в проекті]"
                + "FROM "
                + "[dbo].[project] INNER JOIN [dbo].[directionAndTheme] ON([dbo].[project].[directionAndTheme_id] =[dbo].[directionAndTheme].[directionAndTheme_id]) LEFT OUTER JOIN [group] ON ([group].[project_id]=[project].[project_id]) "
                + "WHERE [project].[project_name]<>'None'"
                + "GROUP BY [project].[project_id],[project].[project_name],[directionAndTheme].[direction],[directionAndTheme].[theme],[project].[grade],[project].[popularity],[group].[project_id]";
                SqlCommand command = new SqlCommand(queryString, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
                foreach (DataGridViewColumn dgvc in dataGridView1.Columns)
                {
                    dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
                }
                flagWhatTableIsCurrentlyOnScreen = 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                dataTable.Rows.Clear();
                dataTable.Columns.Clear();
                string queryString = "SELECT [project].[project_id] as [ID Проекту] "
                + ",[project].[project_name] as [Назва проекту] "
                + ",[directionAndTheme].[direction] as [Напрямок] "
                + ",[directionAndTheme].[theme] as [Тема] "
                + ",[project].[grade] as [Оцінка] "
                + ",[project].[popularity] as [Число переглядів] "
                + ",COUNT([group].[user_id]) as [Кількість учасників в проекті]"
                + "FROM "
                + "[dbo].[project] INNER JOIN [dbo].[directionAndTheme] "
                + "ON([dbo].[project].[directionAndTheme_id] =[dbo].[directionAndTheme].[directionAndTheme_id]) INNER JOIN [group] ON ([group].[project_id]=[project].[project_id])"
                + "WHERE [project].[project_name]<>'None' AND "
                + $"[group].[user_id]={User_id} "
                + "GROUP BY [project].[project_id],[project].[project_name],[directionAndTheme].[direction],[directionAndTheme].[theme],[project].[grade],[project].[popularity],[group].[project_id]";
                SqlCommand command = new SqlCommand(queryString, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
                foreach (DataGridViewColumn dgvc in dataGridView1.Columns)
                {
                    dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
                }
                flagWhatTableIsCurrentlyOnScreen = 2;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string queryString = $"SELECT COUNT([project_id]) FROM [group] WHERE [user_id]='{User_id}'";
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
                MessageBox.Show("Вийдіть або видаліть проект, в якому ви знаходитеся!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }



            AddProjectFormUser f = new AddProjectFormUser();
            f.user_id = User_id;
            f.ShowDialog();

            queryString = $"SELECT project_id FROM [project] WHERE project_name='{f.projectName}'";
            command = new SqlCommand(queryString, connection);
            reader = command.ExecuteReader();
            string project_id = "";
            while (reader.Read())
            {
                project_id = reader[0].ToString();
            }
            reader.Close();

            if (f.submit == 1)
            {
                for (int i = 0; i < f.Selected_users.Rows.Count; i++)
                {
                    queryString = $"UPDATE [group] SET project_id={project_id} WHERE [user_id]={f.Selected_users.Rows[i].ItemArray[0].ToString()}";
                    command = new SqlCommand(queryString, connection);
                    command.ExecuteNonQuery();

                }
            }
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (flagWhatTableIsCurrentlyOnScreen == 2)
            {
                try
                {
                    if (MessageBox.Show("Ви точно хочете видалити проект?", "Питання", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        string i = dataGridView1.SelectedCells[0].Value.ToString();
                        string query_id = $"SELECT [user_id] FROM [group] WHERE ([group].[project_id]={i})";
                        SqlCommand command = new SqlCommand(query_id, connection);
                        SqlDataReader reader = command.ExecuteReader();
                        int q = 0;
                        List<string> list = new List<string>();
                        while (reader.Read())
                        {
                            list.Add(reader[0].ToString());
                            q++;
                        }
                        reader.Close();
                        foreach (string str in list)
                        {
                            query_id = $"UPDATE [group] SET [project_id]=1 WHERE [user_id]={str}";
                            command = new SqlCommand(query_id, connection);
                            command.ExecuteNonQuery();
                        }
                        query_id = $"DELETE FROM [project] WHERE [project].[project_id]={i}";
                        command = new SqlCommand(query_id, connection);
                        command.ExecuteNonQuery();
                        MessageBox.Show("Проект видален");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Виберіть спочатку свої проекти", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            try
            {
                dataTable.Rows.Clear();
                dataTable.Columns.Clear();
                string project_name = textBox1.Text;

                string queryString = "SELECT [project].[project_id] as [ID Проекту] "
                + ",[project].[project_name] as [Назва проекту] "
                + ",[directionAndTheme].[direction] as [Напрямок] "
                + ",[directionAndTheme].[theme] as [Тема] "
                + ",[project].[grade] as [Оцінка] "
                + ",[project].[popularity] as [Число переглядів] "
                + ",COUNT([group].[user_id]) as [Кількість учасників в проекті]"
                + "FROM "
                + "[dbo].[project] INNER JOIN [dbo].[directionAndTheme] ON([dbo].[project].[directionAndTheme_id] =[dbo].[directionAndTheme].[directionAndTheme_id]) LEFT OUTER JOIN [group] ON ([group].[project_id]=[project].[project_id]) "
                + $"WHERE [project].[project_name]<>'None' AND [project].[project_name] LIKE '%{project_name}%'"
                + "GROUP BY [project].[project_id],[project].[project_name],[directionAndTheme].[direction],[directionAndTheme].[theme],[project].[grade],[project].[popularity],[group].[project_id]";

                SqlCommand command = new SqlCommand(queryString, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dataTable);
                foreach (DataGridViewColumn dgvc in dataGridView1.Columns)
                {
                    dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
                }
                dataGridView1.DataSource = dataTable;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ProfileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeUserInformationForm f = new ChangeUserInformationForm();
            f.User_id = User_id;
            f.ShowDialog();
        }

        private void TableDirectionAndThemeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                dataTable.Rows.Clear();
                dataTable.Columns.Clear();
                string queryString = "SELECT [directionAndTheme].[directionAndTheme_id] AS [ID напрямка з темою] "
                + ",[directionAndTheme].[direction] AS [Напрямок] "
                + ",[directionAndTheme].[theme] AS [Тема] "
                + "FROM [dbo].[directionAndTheme]"
                + "WHERE [dbo].[directionAndTheme].[direction]<>'None' AND [dbo].[directionAndTheme].[theme]<>'None'";
                SqlCommand command = new SqlCommand(queryString, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dataTable);
                foreach (DataGridViewColumn dgvc in dataGridView1.Columns)
                {
                    dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
                }
                dataGridView1.DataSource = dataTable;
                flagWhatTableIsCurrentlyOnScreen = 3;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void TableThemeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                dataTable.Rows.Clear();
                dataTable.Columns.Clear();
                string queryString = "SELECT [themes].[theme] AS [Назва теми] "
                + "FROM [dbo].[themes]"
                + "WHERE [dbo].[themes].[theme]<>'None'";
                SqlCommand command = new SqlCommand(queryString, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dataTable);
                foreach (DataGridViewColumn dgvc in dataGridView1.Columns)
                {
                    dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
                }
                dataGridView1.DataSource = dataTable;
                flagWhatTableIsCurrentlyOnScreen = 4;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void TableDirectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                dataTable.Rows.Clear();
                dataTable.Columns.Clear();
                string queryString = "SELECT [directions].[direction] AS [Назва напрямка] "
                + ",[directions].[date] AS [Дата початку презентацій]"
                + "FROM [dbo].[directions]"
                + "WHERE [dbo].[directions].[direction]<>'None'";
                SqlCommand command = new SqlCommand(queryString, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dataTable);
                foreach (DataGridViewColumn dgvc in dataGridView1.Columns)
                {
                    dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
                }
                dataGridView1.DataSource = dataTable;
                flagWhatTableIsCurrentlyOnScreen = 5;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void темаToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {

                dataTable.Rows.Clear();
                dataTable.Columns.Clear();
                string queryString = "SELECT [project].[project_id] as [ID Проекту] "
                + ",[project].[project_name] as [Назва проекту] "
                + ",[directionAndTheme].[direction] as [Напрямок] "
                + ",[directionAndTheme].[theme] as [Тема] "
                + ",[project].[grade] as [Оцінка] "
                + ",[project].[popularity] as [Число переглядів] "
                + ",COUNT([group].[user_id]) as [Кількість учасників в проекті]"
                + "FROM "
                + "[dbo].[project] INNER JOIN [dbo].[directionAndTheme] ON([dbo].[project].[directionAndTheme_id] =[dbo].[directionAndTheme].[directionAndTheme_id]) LEFT OUTER JOIN [group] ON ([group].[project_id]=[project].[project_id]) "
                + "WHERE [project].[project_name]<>'None' AND [project].[grade]=0"
                + "GROUP BY [project].[project_id],[project].[project_name],[directionAndTheme].[direction],[directionAndTheme].[theme],[project].[grade],[project].[popularity],[group].[project_id]";
                SqlCommand command = new SqlCommand(queryString, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dataTable);
                foreach (DataGridViewColumn dgvc in dataGridView1.Columns)
                {
                    dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
                }
                dataGridView1.DataSource = dataTable;
                flagWhatTableIsCurrentlyOnScreen = 2;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            try
            {

                dataTable.Rows.Clear();
                dataTable.Columns.Clear();
                string queryString = "SELECT [project].[project_id] as [ID Проекту] "
                + ",[project].[project_name] as [Назва проекту] "
                + ",[directionAndTheme].[direction] as [Напрямок] "
                + ",[directionAndTheme].[theme] as [Тема] "
                + ",[project].[grade] as [Оцінка] "
                + ",[project].[popularity] as [Число переглядів] "
                + ",COUNT([group].[user_id]) as [Кількість учасників в проекті]"
                + "FROM "
                + "[dbo].[project] INNER JOIN [dbo].[directionAndTheme] ON([dbo].[project].[directionAndTheme_id] =[dbo].[directionAndTheme].[directionAndTheme_id]) LEFT OUTER JOIN [group] ON ([group].[project_id]=[project].[project_id]) "
                + "WHERE [project].[project_name]<>'None'"
                + "GROUP BY [project].[project_id],[project].[project_name],[directionAndTheme].[direction],[directionAndTheme].[theme],[project].[grade],[project].[popularity],[group].[project_id]"
                + "ORDER BY COUNT([group].[user_id]) DESC";
                SqlCommand command = new SqlCommand(queryString, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dataTable);
                foreach (DataGridViewColumn dgvc in dataGridView1.Columns)
                {
                    dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
                }
                dataGridView1.DataSource = dataTable;
                flagWhatTableIsCurrentlyOnScreen = 2;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void toolStripMenuItem15_Click(object sender, EventArgs e)
        {
            try
            {

                dataTable.Rows.Clear();
                dataTable.Columns.Clear();
                string queryString = "SELECT [project].[project_id] as [ID Проекту] "
                + ",[project].[project_name] as [Назва проекту] "
                + ",[directionAndTheme].[direction] as [Напрямок] "
                + ",[directionAndTheme].[theme] as [Тема] "
                + ",[project].[grade] as [Оцінка] "
                + ",[project].[popularity] as [Число переглядів] "
                + ",COUNT([group].[user_id]) as [Кількість учасників в проекті]"
                + "FROM "
                + "[dbo].[project] INNER JOIN [dbo].[directionAndTheme] ON([dbo].[project].[directionAndTheme_id] =[dbo].[directionAndTheme].[directionAndTheme_id]) LEFT OUTER JOIN [group] ON ([group].[project_id]=[project].[project_id]) "
                + "WHERE [project].[project_name]<>'None'"
                + "GROUP BY [project].[project_id],[project].[project_name],[directionAndTheme].[direction],[directionAndTheme].[theme],[project].[grade],[project].[popularity],[group].[project_id]"
                + "ORDER BY [project].[grade] DESC";
                SqlCommand command = new SqlCommand(queryString, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dataTable);
                foreach (DataGridViewColumn dgvc in dataGridView1.Columns)
                {
                    dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
                }
                dataGridView1.DataSource = dataTable;
                flagWhatTableIsCurrentlyOnScreen = 2;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void toolStripMenuItem12_Click(object sender, EventArgs e)
        {
            try
            {

                dataTable.Rows.Clear();
                dataTable.Columns.Clear();
                string queryString = "SELECT [project].[project_id] as [ID Проекту] "
                + ",[project].[project_name] as [Назва проекту] "
                + ",[directionAndTheme].[direction] as [Напрямок] "
                + ",[directionAndTheme].[theme] as [Тема] "
                + ",[project].[grade] as [Оцінка] "
                + ",[project].[popularity] as [Число переглядів] "
                + ",COUNT([group].[user_id]) as [Кількість учасників в проекті]"
                + "FROM "
                + "[dbo].[project] INNER JOIN [dbo].[directionAndTheme] ON([dbo].[project].[directionAndTheme_id] =[dbo].[directionAndTheme].[directionAndTheme_id]) LEFT OUTER JOIN [group] ON ([group].[project_id]=[project].[project_id]) "
                + "WHERE [project].[project_name]<>'None'"
                + "GROUP BY [project].[project_id],[project].[project_name],[directionAndTheme].[direction],[directionAndTheme].[theme],[project].[grade],[project].[popularity],[group].[project_id]"
                + "ORDER BY [popularity] DESC";
                SqlCommand command = new SqlCommand(queryString, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dataTable);
                foreach (DataGridViewColumn dgvc in dataGridView1.Columns)
                {
                    dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
                }
                dataGridView1.DataSource = dataTable;
                flagWhatTableIsCurrentlyOnScreen = 2;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void NumberOfPeopleInProjectsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                dataTable.Rows.Clear();
                dataTable.Columns.Clear();
                string queryString = "SELECT [project].[project_id] as [ID Проекту] "
                + ",[project].[project_name] as [Назва проекту] "
                + ",COUNT([group].[user_id]) as [Кількість учасників в проекті]"
                + "FROM "
                + "[dbo].[project] INNER JOIN [dbo].[directionAndTheme] ON([dbo].[project].[directionAndTheme_id] =[dbo].[directionAndTheme].[directionAndTheme_id]) LEFT OUTER JOIN [group] ON ([group].[project_id]=[project].[project_id]) "
                + "WHERE [project].[project_name]<>'None'"
                + "GROUP BY [project].[project_id],[project].[project_name]";
                SqlCommand command = new SqlCommand(queryString, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dataTable);
                foreach (DataGridViewColumn dgvc in dataGridView1.Columns)
                {
                    dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
                }
                dataGridView1.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void NumberOfProjectsInDirectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                dataTable.Rows.Clear();
                dataTable.Columns.Clear();
                string queryString = "SELECT kek.[direction] AS [Напрямок], kek.[Number Of Projects] AS [Кількість проектів за напрямком] " +
                "FROM " +
                "( " +
                "SELECT d.[direction], COUNT(p.[project_id]) AS[Number Of Projects] " +
                "FROM[project] p RIGHT OUTER JOIN[directionAndTheme] d ON(p.[directionAndTheme_id] = d.[directionAndTheme_id]) " +
                "GROUP BY d.[direction] " +
                ") AS kek " +
                "WHERE kek.direction <> 'None'";
                SqlCommand command = new SqlCommand(queryString, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dataTable);
                foreach (DataGridViewColumn dgvc in dataGridView1.Columns)
                {
                    dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
                }
                dataGridView1.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void NumberOfProjectsInThemeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                dataTable.Rows.Clear();
                dataTable.Columns.Clear();
                string queryString = "SELECT kek.[theme] AS [Тема], kek.[Number Of Projects] AS [Кількість проектів за темою] " +
                "FROM " +
                "( " +
                "SELECT  d.[theme],COUNT(p.[project_id]) AS [Number Of Projects] " +
                "FROM[project] p RIGHT OUTER JOIN[directionAndTheme] d ON(p.[directionAndTheme_id] = d.[directionAndTheme_id]) " +
                "GROUP BY d.[theme] " +
                ") AS kek " +
                "WHERE kek.theme<>'None'";
                SqlCommand command = new SqlCommand(queryString, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dataTable);
                foreach (DataGridViewColumn dgvc in dataGridView1.Columns)
                {
                    dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
                }
                dataGridView1.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void NumberOfThemesInDirectionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                dataTable.Rows.Clear();
                dataTable.Columns.Clear();
                string queryString = "SELECT direction AS [Напрямок], COUNT(theme) AS [Кількість тем у напрямку] " +
                "FROM[directionAndTheme] " +
                "WHERE direction<>'None' " +
                "GROUP BY direction";
                SqlCommand command = new SqlCommand(queryString, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dataTable);
                foreach (DataGridViewColumn dgvc in dataGridView1.Columns)
                {
                    dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
                }
                dataGridView1.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
