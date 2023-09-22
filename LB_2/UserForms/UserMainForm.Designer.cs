namespace LB_2.UserForms
{
    partial class UserMainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button6 = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ProfileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.exitAccountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitProgramToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem15 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem12 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.НапрямокИТемаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TableDirectionAndThemeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.темаToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.TableThemeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.НапрямокToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TableDirectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.статистикаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.NumberOfPeopleInProjectsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.NumberOfProjectsInDirectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.NumberOfProjectsInThemeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.NumberOfThemesInDirectionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 30);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(698, 416);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            // 
            // button6
            // 
            this.button6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.button6.Location = new System.Drawing.Point(716, 84);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(235, 58);
            this.button6.TabIndex = 10;
            this.button6.Text = "Пошук";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click_1);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.НапрямокИТемаToolStripMenuItem,
            this.темаToolStripMenuItem1,
            this.НапрямокToolStripMenuItem,
            this.статистикаToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(963, 29);
            this.menuStrip1.TabIndex = 13;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ProfileToolStripMenuItem,
            this.toolStripSeparator,
            this.exitAccountToolStripMenuItem,
            this.exitProgramToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(80, 25);
            this.fileToolStripMenuItem.Text = "Аккаунт";
            // 
            // ProfileToolStripMenuItem
            // 
            this.ProfileToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ProfileToolStripMenuItem.Name = "ProfileToolStripMenuItem";
            this.ProfileToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.ProfileToolStripMenuItem.ShowShortcutKeys = false;
            this.ProfileToolStripMenuItem.Size = new System.Drawing.Size(212, 26);
            this.ProfileToolStripMenuItem.Text = "Редагувати профіль";
            this.ProfileToolStripMenuItem.Click += new System.EventHandler(this.ProfileToolStripMenuItem_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(209, 6);
            // 
            // exitAccountToolStripMenuItem
            // 
            this.exitAccountToolStripMenuItem.Name = "exitAccountToolStripMenuItem";
            this.exitAccountToolStripMenuItem.Size = new System.Drawing.Size(212, 26);
            this.exitAccountToolStripMenuItem.Text = "Вийти з аккаунту";
            this.exitAccountToolStripMenuItem.Click += new System.EventHandler(this.exitAccountToolStripMenuItem_Click);
            // 
            // exitProgramToolStripMenuItem
            // 
            this.exitProgramToolStripMenuItem.Name = "exitProgramToolStripMenuItem";
            this.exitProgramToolStripMenuItem.Size = new System.Drawing.Size(212, 26);
            this.exitProgramToolStripMenuItem.Text = "Вийти з програми";
            this.exitProgramToolStripMenuItem.Click += new System.EventHandler(this.exitProgramToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoToolStripMenuItem,
            this.redoToolStripMenuItem,
            this.toolStripMenuItem1,
            this.toolStripSeparator2,
            this.toolStripMenuItem2,
            this.toolStripMenuItem15,
            this.toolStripMenuItem12,
            this.toolStripSeparator3,
            this.cutToolStripMenuItem,
            this.copyToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(74, 25);
            this.editToolStripMenuItem.Text = "Проект";
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            this.undoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.undoToolStripMenuItem.ShowShortcutKeys = false;
            this.undoToolStripMenuItem.Size = new System.Drawing.Size(376, 26);
            this.undoToolStripMenuItem.Text = "Список всіх проектів";
            this.undoToolStripMenuItem.Click += new System.EventHandler(this.undoToolStripMenuItem_Click);
            // 
            // redoToolStripMenuItem
            // 
            this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            this.redoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.redoToolStripMenuItem.ShowShortcutKeys = false;
            this.redoToolStripMenuItem.Size = new System.Drawing.Size(376, 26);
            this.redoToolStripMenuItem.Text = "Список моіх проектів";
            this.redoToolStripMenuItem.Click += new System.EventHandler(this.redoToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.toolStripMenuItem1.ShowShortcutKeys = false;
            this.toolStripMenuItem1.Size = new System.Drawing.Size(376, 26);
            this.toolStripMenuItem1.Text = "Список проектів без оцінки";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(373, 6);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(376, 26);
            this.toolStripMenuItem2.Text = "Сортування за кількістю людей в проекті";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // toolStripMenuItem15
            // 
            this.toolStripMenuItem15.Name = "toolStripMenuItem15";
            this.toolStripMenuItem15.Size = new System.Drawing.Size(376, 26);
            this.toolStripMenuItem15.Text = "Сортування за рейтингом";
            this.toolStripMenuItem15.Click += new System.EventHandler(this.toolStripMenuItem15_Click);
            // 
            // toolStripMenuItem12
            // 
            this.toolStripMenuItem12.Name = "toolStripMenuItem12";
            this.toolStripMenuItem12.Size = new System.Drawing.Size(376, 26);
            this.toolStripMenuItem12.Text = "Сортування за популярністю";
            this.toolStripMenuItem12.Click += new System.EventHandler(this.toolStripMenuItem12_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(373, 6);
            // 
            // cutToolStripMenuItem
            // 
            this.cutToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            this.cutToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.cutToolStripMenuItem.ShowShortcutKeys = false;
            this.cutToolStripMenuItem.Size = new System.Drawing.Size(376, 26);
            this.cutToolStripMenuItem.Text = "Створити проект";
            this.cutToolStripMenuItem.Click += new System.EventHandler(this.cutToolStripMenuItem_Click);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.copyToolStripMenuItem.ShowShortcutKeys = false;
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(376, 26);
            this.copyToolStripMenuItem.Text = "Видалити проект";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // НапрямокИТемаToolStripMenuItem
            // 
            this.НапрямокИТемаToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TableDirectionAndThemeToolStripMenuItem});
            this.НапрямокИТемаToolStripMenuItem.Name = "НапрямокИТемаToolStripMenuItem";
            this.НапрямокИТемаToolStripMenuItem.Size = new System.Drawing.Size(152, 25);
            this.НапрямокИТемаToolStripMenuItem.Text = "Напрямок та тема";
            // 
            // TableDirectionAndThemeToolStripMenuItem
            // 
            this.TableDirectionAndThemeToolStripMenuItem.Name = "TableDirectionAndThemeToolStripMenuItem";
            this.TableDirectionAndThemeToolStripMenuItem.Size = new System.Drawing.Size(281, 26);
            this.TableDirectionAndThemeToolStripMenuItem.Text = "Таблиця напрямків з темою";
            this.TableDirectionAndThemeToolStripMenuItem.Click += new System.EventHandler(this.TableDirectionAndThemeToolStripMenuItem_Click);
            // 
            // темаToolStripMenuItem1
            // 
            this.темаToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TableThemeToolStripMenuItem});
            this.темаToolStripMenuItem1.Name = "темаToolStripMenuItem1";
            this.темаToolStripMenuItem1.Size = new System.Drawing.Size(57, 25);
            this.темаToolStripMenuItem1.Text = "Тема";
            this.темаToolStripMenuItem1.Click += new System.EventHandler(this.темаToolStripMenuItem1_Click);
            // 
            // TableThemeToolStripMenuItem
            // 
            this.TableThemeToolStripMenuItem.Name = "TableThemeToolStripMenuItem";
            this.TableThemeToolStripMenuItem.Size = new System.Drawing.Size(170, 26);
            this.TableThemeToolStripMenuItem.Text = "Таблиця тем";
            this.TableThemeToolStripMenuItem.Click += new System.EventHandler(this.TableThemeToolStripMenuItem_Click);
            // 
            // НапрямокToolStripMenuItem
            // 
            this.НапрямокToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TableDirectionToolStripMenuItem});
            this.НапрямокToolStripMenuItem.Name = "НапрямокToolStripMenuItem";
            this.НапрямокToolStripMenuItem.Size = new System.Drawing.Size(95, 25);
            this.НапрямокToolStripMenuItem.Text = "Напрямок";
            // 
            // TableDirectionToolStripMenuItem
            // 
            this.TableDirectionToolStripMenuItem.Name = "TableDirectionToolStripMenuItem";
            this.TableDirectionToolStripMenuItem.Size = new System.Drawing.Size(218, 26);
            this.TableDirectionToolStripMenuItem.Text = "Таблиця напрямків";
            this.TableDirectionToolStripMenuItem.Click += new System.EventHandler(this.TableDirectionToolStripMenuItem_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.label2.Location = new System.Drawing.Point(712, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(226, 20);
            this.label2.TabIndex = 14;
            this.label2.Text = "Пошук проекту за назвою";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.textBox1.Location = new System.Drawing.Point(716, 52);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(235, 26);
            this.textBox1.TabIndex = 15;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // статистикаToolStripMenuItem
            // 
            this.статистикаToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NumberOfPeopleInProjectsToolStripMenuItem,
            this.NumberOfProjectsInDirectionToolStripMenuItem,
            this.NumberOfProjectsInThemeToolStripMenuItem,
            this.NumberOfThemesInDirectionsToolStripMenuItem});
            this.статистикаToolStripMenuItem.Name = "статистикаToolStripMenuItem";
            this.статистикаToolStripMenuItem.Size = new System.Drawing.Size(102, 25);
            this.статистикаToolStripMenuItem.Text = "Статистика";
            // 
            // NumberOfPeopleInProjectsToolStripMenuItem
            // 
            this.NumberOfPeopleInProjectsToolStripMenuItem.Name = "NumberOfPeopleInProjectsToolStripMenuItem";
            this.NumberOfPeopleInProjectsToolStripMenuItem.Size = new System.Drawing.Size(314, 26);
            this.NumberOfPeopleInProjectsToolStripMenuItem.Text = "Кількість людей в проекті";
            this.NumberOfPeopleInProjectsToolStripMenuItem.Click += new System.EventHandler(this.NumberOfPeopleInProjectsToolStripMenuItem_Click);
            // 
            // NumberOfProjectsInDirectionToolStripMenuItem
            // 
            this.NumberOfProjectsInDirectionToolStripMenuItem.Name = "NumberOfProjectsInDirectionToolStripMenuItem";
            this.NumberOfProjectsInDirectionToolStripMenuItem.Size = new System.Drawing.Size(314, 26);
            this.NumberOfProjectsInDirectionToolStripMenuItem.Text = "Кількість проектів за напрямком";
            this.NumberOfProjectsInDirectionToolStripMenuItem.Click += new System.EventHandler(this.NumberOfProjectsInDirectionToolStripMenuItem_Click);
            // 
            // NumberOfProjectsInThemeToolStripMenuItem
            // 
            this.NumberOfProjectsInThemeToolStripMenuItem.Name = "NumberOfProjectsInThemeToolStripMenuItem";
            this.NumberOfProjectsInThemeToolStripMenuItem.Size = new System.Drawing.Size(314, 26);
            this.NumberOfProjectsInThemeToolStripMenuItem.Text = "Кількість проектів за темою";
            this.NumberOfProjectsInThemeToolStripMenuItem.Click += new System.EventHandler(this.NumberOfProjectsInThemeToolStripMenuItem_Click);
            // 
            // NumberOfThemesInDirectionsToolStripMenuItem
            // 
            this.NumberOfThemesInDirectionsToolStripMenuItem.Name = "NumberOfThemesInDirectionsToolStripMenuItem";
            this.NumberOfThemesInDirectionsToolStripMenuItem.Size = new System.Drawing.Size(314, 26);
            this.NumberOfThemesInDirectionsToolStripMenuItem.Text = "Кількість тем в напрямках";
            this.NumberOfThemesInDirectionsToolStripMenuItem.Click += new System.EventHandler(this.NumberOfThemesInDirectionsToolStripMenuItem_Click);
            // 
            // UserMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(963, 458);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "UserMainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Добридень, Ім\'я";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.UserMainForm_FormClosed);
            this.Load += new System.EventHandler(this.UserMainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ProfileToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem exitAccountToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitProgramToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem redoToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ToolStripMenuItem темаToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem НапрямокToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem НапрямокИТемаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem TableDirectionAndThemeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem TableThemeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem TableDirectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem15;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem12;
        private System.Windows.Forms.ToolStripMenuItem статистикаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem NumberOfPeopleInProjectsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem NumberOfProjectsInDirectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem NumberOfProjectsInThemeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem NumberOfThemesInDirectionsToolStripMenuItem;
    }
}