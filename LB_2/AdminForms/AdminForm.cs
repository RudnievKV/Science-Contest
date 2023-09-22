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
using LB_2.AdminForms;
using LB_2.AdminForms.Old;

namespace LB_2
{
    public partial class AdminForm : Form
    {
        public static DataTable dataTable = new DataTable();
        static private string connectionString =
            "Data Source=DESKTOP-JKS2HE1;Initial Catalog=ScienceContest;"
            + "Integrated Security=true";
        SqlConnection connection = new SqlConnection(connectionString);
        private int flagWhatTableIsCurrentlyOnScreen = 1;
        public int User_id;
        public AdminForm()
        {
            InitializeComponent();
            connection.Open();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {

                dataTable.Rows.Clear();
                dataTable.Columns.Clear();
                string queryString = "SELECT [user].[user_id] AS [ID Користувача], " +
                    "[user].[email] AS [Пошта]," +
                    "[user].[password] AS [Пароль]," +
                    "[user].[firstName] AS [Ім'я]," +
                    "[user].[middleName] AS [По батькові]," +
                    "[user].[lastName] AS [Прізвище]," +
                    "[project].[project_name] AS [Назва проекту]," +
                    "[user].[dateOfBirth] AS [Дата нарождення], " +
                    "[role].[role_name] AS[Рівень доступа]" +
                    "FROM  [role] INNER JOIN ([dbo].[project] INNER JOIN ([dbo].[group] INNER JOIN [dbo].[user] ON ([dbo].[group].[user_id]=[dbo].[user].[user_id]))  ON([dbo].[group].[project_id] =[dbo].[project].[project_id])) ON ([user].[id_role]=[role].[role_id])";
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

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            connection.Close();
            Application.Exit();

        }





        private void button7_Click(object sender, EventArgs e)
        {
            DoSQLForm f1 = new DoSQLForm();
            f1.ShowDialog();
        }


        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (flagWhatTableIsCurrentlyOnScreen == 6)
                {
                    ProjectWinnerForm f = new ProjectWinnerForm();
                    f.User_id = User_id;
                    f.project_id = Int32.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                    f.ShowDialog();
                }
                else if (flagWhatTableIsCurrentlyOnScreen == 7)
                {
                    ProjectWinnerForm f = new ProjectWinnerForm();
                    f.User_id = User_id;
                    f.label2.Text = "Учасника";
                    f.project_id = Int32.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                    f.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
            Application.Exit();
        }

        private void printPreviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Owner.Visible = true;
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeUserInformationForm f = new ChangeUserInformationForm();
            f.User_id = User_id;
            f.ShowDialog();
        }



        private void TableProjectsToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void CreateProjectToolStripMenuItem1_Click(object sender, EventArgs e)
        {

            int q = 0;
            string queryString = $"SELECT COUNT(project_id) FROM [group] WHERE [user_id]='{User_id}'";
            SqlCommand command = new SqlCommand(queryString, connection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
               
            }
            reader.Close();



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

        private void toolStripMenuItem13_Click(object sender, EventArgs e)
        {
            try
            {
                if (flagWhatTableIsCurrentlyOnScreen == 2)
                {
                    int id = Int32.Parse(dataGridView1.SelectedCells[0].Value.ToString());
                    ChangeProjectFormAdmin f = new ChangeProjectFormAdmin();
                    f.Text = "Змінити дані проекту";
                    f.project_id = id;

                    f.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Виберіть проект!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void toolStripMenuItem14_Click(object sender, EventArgs e)
        {
            try
            {
                if (flagWhatTableIsCurrentlyOnScreen == 2)
                {
                    if (MessageBox.Show("Ви точно хочете видалити цей проект?", "Питання", MessageBoxButtons.YesNo) == DialogResult.Yes)
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
                else
                {
                    MessageBox.Show("Виберіть проект!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void TableUsersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                dataTable.Rows.Clear();
                dataTable.Columns.Clear();
                string queryString = "SELECT [user].[user_id] AS [ID Користувача], " +
                    "[user].[email] AS [Пошта]," +
                    "[user].[password] AS [Пароль]," +
                    "[user].[firstName] AS [Ім'я]," +
                    "[user].[middleName] AS [По батькові]," +
                    "[user].[lastName] AS [Прізвище]," +
                    "[project].[project_name] AS [Назва проекту]," +
                    "[user].[dateOfBirth] AS [Дата нарождення], " +
                    "[role].[role_name] AS[Рівень доступа]" +
                    "FROM  [role] INNER JOIN ([dbo].[project] INNER JOIN ([dbo].[group] INNER JOIN [dbo].[user] ON ([dbo].[group].[user_id]=[dbo].[user].[user_id]))  ON([dbo].[group].[project_id] =[dbo].[project].[project_id])) ON ([user].[id_role]=[role].[role_id])";
                SqlCommand command = new SqlCommand(queryString, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dataTable);
                foreach (DataGridViewColumn dgvc in dataGridView1.Columns)
                {
                    dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
                }
                dataGridView1.DataSource = dataTable;
                flagWhatTableIsCurrentlyOnScreen = 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void CreateUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddUserForm f = new AddUserForm();
            f.ShowDialog();

        }

        private void ChangeUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (flagWhatTableIsCurrentlyOnScreen == 1)
            {
                AddUserForm f = new AddUserForm();
                f.AddUserBtn.Text = "Змінити";
                f.Text = "Змінити дані користувача";
                f.User_id = Int32.Parse(dataGridView1.SelectedCells[0].Value.ToString());
                f.ShowDialog();
            }
            else
            {
                MessageBox.Show("Виберіть користувача!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void DeleteUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (flagWhatTableIsCurrentlyOnScreen == 1)
            {
                if (MessageBox.Show("Ви точно хочете видалити цього користувача?", "Питання", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    string i = dataGridView1.SelectedCells[0].Value.ToString();
                    string query_id = $"DELETE FROM[user] WHERE [user].[user_id] = '{i}'";
                    SqlCommand command = new SqlCommand(query_id, connection);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Користувач видален");
                }
            }
            else
            {
                MessageBox.Show("Виберіть користувача!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

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

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CreateDirectionAndThemeForm f = new CreateDirectionAndThemeForm();
            f.ShowDialog();

        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
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

        private void toolStripMenuItem8_Click(object sender, EventArgs e)
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

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            try
            {
                if (flagWhatTableIsCurrentlyOnScreen == 3)
                {
                    if (MessageBox.Show("Ви точно хочете видалити цей напрямок з темою?", "Питання", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        string i = dataGridView1.SelectedCells[0].Value.ToString();
                        string query_id = $"DELETE FROM [directionAndTheme] WHERE [directionAndTheme_id]={i}";
                        SqlCommand command = new SqlCommand(query_id, connection);
                        command.ExecuteNonQuery();
                        MessageBox.Show("Напрямок з темою видалені");
                    }
                }
                else
                {
                    MessageBox.Show("Виберіть напрямок з темою!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            try
            {
                if (flagWhatTableIsCurrentlyOnScreen == 3)
                {
                    CreateDirectionAndThemeForm f = new CreateDirectionAndThemeForm();
                    f.button1.Text = "Змінити";
                    f.Text = "Змінити дані напрямка з темою";
                    f.directionAndTheme_id = Int32.Parse(dataGridView1.SelectedCells[0].Value.ToString());
                    f.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Виберіть напрямок з темою!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            CreateThemeForm f = new CreateThemeForm();
            f.ShowDialog();
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            try
            {
                if (flagWhatTableIsCurrentlyOnScreen == 4)
                {
                    CreateThemeForm f = new CreateThemeForm();
                    f.theme_name = dataGridView1.SelectedCells[0].Value.ToString();
                    f.Text = "Змінити дані теми";
                    f.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Виберіть тему!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            try
            {
                if (flagWhatTableIsCurrentlyOnScreen == 4)
                {
                    if (MessageBox.Show("Ви точно хочете видалити цю тему?", "Питання", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        string i = dataGridView1.SelectedCells[0].Value.ToString();
                        string query_id = $"DELETE FROM [themes] WHERE [theme]='{i}'";
                        SqlCommand command = new SqlCommand(query_id, connection);
                        command.ExecuteNonQuery();
                        MessageBox.Show("Тема видалена");
                    }
                }
                else
                {
                    MessageBox.Show("Виберіть тему!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch
            {
                MessageBox.Show("Видаліть спочатку всі напрямки і проекти з цією темою", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {
            CreateDirectionForm f = new CreateDirectionForm();
            f.ShowDialog();
        }

        private void toolStripMenuItem10_Click(object sender, EventArgs e)
        {
            try
            {
                if (flagWhatTableIsCurrentlyOnScreen == 5)
                {
                    CreateDirectionForm f = new CreateDirectionForm();
                    f.direction_name = dataGridView1.SelectedCells[0].Value.ToString();
                    f.Text = "Змінити дані теми";
                    f.button1.Text = "Змінити";
                    f.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Виберіть напрямок!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void toolStripMenuItem11_Click(object sender, EventArgs e)
        {
            try
            {
                if (flagWhatTableIsCurrentlyOnScreen == 5)
                {
                    if (MessageBox.Show("Ви точно хочете видалити цей напрям?", "Питання", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        string i = dataGridView1.SelectedCells[0].Value.ToString();
                        string query_id = $"DELETE FROM [directions] WHERE [direction]='{i}'";
                        SqlCommand command = new SqlCommand(query_id, connection);
                        command.ExecuteNonQuery();
                        MessageBox.Show("Напрямок видален");
                    }
                }
                else
                {
                    MessageBox.Show("Виберіть напрямок!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch
            {
                MessageBox.Show("Видаліть спочатку усі теми і проекти з цим напрямом", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void toolStripMenuItem12_Click(object sender, EventArgs e)
        {
            try
            {

                dataTable.Rows.Clear();
                dataTable.Columns.Clear();
                string queryString = "SELECT [user].[user_id] AS [ID Користувача], " +
                    "[user].[email] AS [Пошта]," +
                    "[user].[password] AS [Пароль]," +
                    "[user].[firstName] AS [Ім'я]," +
                    "[user].[middleName] AS [По батькові]," +
                    "[user].[lastName] AS [Прізвище]," +
                    "[project].[project_name] AS [Назва проекту]," +
                    "[user].[dateOfBirth] AS [Дата нарождення], " +
                    "[role].[role_name] AS[Рівень доступа]" +
                    "FROM  [role] INNER JOIN ([dbo].[project] INNER JOIN ([dbo].[group] INNER JOIN [dbo].[user] ON ([dbo].[group].[user_id]=[dbo].[user].[user_id]))  ON([dbo].[group].[project_id] =[dbo].[project].[project_id])) ON ([user].[id_role]=[role].[role_id])";
                SqlCommand command = new SqlCommand(queryString, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dataTable);
                foreach (DataGridViewColumn dgvc in dataGridView1.Columns)
                {
                    dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
                }
                dataGridView1.DataSource = dataTable;
                flagWhatTableIsCurrentlyOnScreen = 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void toolStripMenuItem12_Click_1(object sender, EventArgs e)
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

        private void toolStripMenuItem1_Click_1(object sender, EventArgs e)
        {

        }

        private void TableParticipantsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                dataTable.Rows.Clear();
                dataTable.Columns.Clear();
                string queryString = "SELECT TOP (SELECT COUNT([project_id]) FROM [project] WHERE [project_name] NOT IN (SELECT TOP 3 [project_name] FROM [project] WHERE [project_name]<>'None' ORDER BY [grade] DESC, [popularity] DESC) AND [project_name]<>'None') [project].[project_id] as [ID Проекту] "
                + ",[project].[project_name] as [Назва проекту] "
                + ",[directionAndTheme].[direction] as [Напрямок] "
                + ",[directionAndTheme].[theme] as [Тема] "
                + ",[project].[grade] as [Оцінка] "
                + ",[project].[popularity] as [Число переглядів] "
                + ",COUNT([group].[user_id]) as [Кількість учасників в проекті]"
                + "FROM "
                + "[dbo].[project] INNER JOIN [dbo].[directionAndTheme] ON([dbo].[project].[directionAndTheme_id] =[dbo].[directionAndTheme].[directionAndTheme_id]) LEFT OUTER JOIN [group] ON ([group].[project_id]=[project].[project_id]) "
                + "WHERE [project].[project_name]<>'None'"
                + "GROUP BY [project].[project_id],[project].[project_name],[directionAndTheme].[direction],[directionAndTheme].[theme],[project].[grade],[project].[popularity],[group].[project_id] "
                + "ORDER BY [project].[grade] ASC, [project].[popularity] ASC";
                SqlCommand command = new SqlCommand(queryString, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dataTable);
                foreach (DataGridViewColumn dgvc in dataGridView1.Columns)
                {
                    dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
                }
                dataGridView1.DataSource = dataTable;
                flagWhatTableIsCurrentlyOnScreen = 7;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void TableWinnersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                dataTable.Rows.Clear();
                dataTable.Columns.Clear();
                string queryString = "SELECT TOP 3 [project].[project_id] as [ID Проекту] "
                + ",[project].[project_name] as [Назва проекту] "
                + ",[directionAndTheme].[direction] as [Напрямок] "
                + ",[directionAndTheme].[theme] as [Тема] "
                + ",[project].[grade] as [Оцінка] "
                + ",[project].[popularity] as [Число переглядів] "
                + ",COUNT([group].[user_id]) as [Кількість учасників в проекті]"
                + "FROM "
                + "[dbo].[project] INNER JOIN [dbo].[directionAndTheme] ON([dbo].[project].[directionAndTheme_id] =[dbo].[directionAndTheme].[directionAndTheme_id]) LEFT OUTER JOIN [group] ON ([group].[project_id]=[project].[project_id]) "
                + "WHERE [project].[project_name]<>'None'"
                + "GROUP BY [project].[project_id],[project].[project_name],[directionAndTheme].[direction],[directionAndTheme].[theme],[project].[grade],[project].[popularity],[group].[project_id] "
                + "ORDER BY [project].[grade] DESC, [project].[popularity] DESC";
                SqlCommand command = new SqlCommand(queryString, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dataTable);
                foreach (DataGridViewColumn dgvc in dataGridView1.Columns)
                {
                    dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
                }
                dataGridView1.DataSource = dataTable;
                flagWhatTableIsCurrentlyOnScreen = 6;
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

        private void toolStripMenuItem1_Click_2(object sender, EventArgs e)
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

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripMenuItem16_Click(object sender, EventArgs e)
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

        private void button6_Click(object sender, EventArgs e)
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
