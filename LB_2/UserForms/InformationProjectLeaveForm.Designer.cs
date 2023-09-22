namespace LB_2.UserForms
{
    partial class InformationProjectLeaveForm
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
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.labelGrade = new System.Windows.Forms.Label();
            this.labelPeople = new System.Windows.Forms.Label();
            this.labelTheme = new System.Windows.Forms.Label();
            this.labelDirection = new System.Windows.Forms.Label();
            this.labelDescription = new System.Windows.Forms.Label();
            this.labelProjectName = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button3 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.button2.Location = new System.Drawing.Point(527, 416);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(192, 51);
            this.button2.TabIndex = 29;
            this.button2.Text = "Закрити";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.button1.Location = new System.Drawing.Point(14, 416);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(192, 51);
            this.button1.TabIndex = 28;
            this.button1.Text = "Покинути проект";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.label1.Location = new System.Drawing.Point(15, 330);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(225, 27);
            this.label1.TabIndex = 27;
            this.label1.Text = "Опис проекту";
            // 
            // labelGrade
            // 
            this.labelGrade.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.labelGrade.Location = new System.Drawing.Point(334, 291);
            this.labelGrade.Name = "labelGrade";
            this.labelGrade.Size = new System.Drawing.Size(385, 24);
            this.labelGrade.TabIndex = 25;
            this.labelGrade.Text = "Оцінка проекту:";
            // 
            // labelPeople
            // 
            this.labelPeople.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.labelPeople.Location = new System.Drawing.Point(334, 164);
            this.labelPeople.Name = "labelPeople";
            this.labelPeople.Size = new System.Drawing.Size(385, 128);
            this.labelPeople.TabIndex = 24;
            this.labelPeople.Text = "Люди в проекті:";
            // 
            // labelTheme
            // 
            this.labelTheme.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.labelTheme.Location = new System.Drawing.Point(334, 106);
            this.labelTheme.Name = "labelTheme";
            this.labelTheme.Size = new System.Drawing.Size(385, 27);
            this.labelTheme.TabIndex = 23;
            this.labelTheme.Text = "Тема проекту:";
            // 
            // labelDirection
            // 
            this.labelDirection.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.labelDirection.Location = new System.Drawing.Point(334, 54);
            this.labelDirection.Name = "labelDirection";
            this.labelDirection.Size = new System.Drawing.Size(385, 27);
            this.labelDirection.TabIndex = 22;
            this.labelDirection.Text = "Напрямок проекту:";
            // 
            // labelDescription
            // 
            this.labelDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.labelDescription.Location = new System.Drawing.Point(10, 295);
            this.labelDescription.MaximumSize = new System.Drawing.Size(709, 0);
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Size = new System.Drawing.Size(709, 0);
            this.labelDescription.TabIndex = 21;
            this.labelDescription.Text = "Описание проекта";
            // 
            // labelProjectName
            // 
            this.labelProjectName.AutoSize = true;
            this.labelProjectName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.labelProjectName.Location = new System.Drawing.Point(472, 11);
            this.labelProjectName.Name = "labelProjectName";
            this.labelProjectName.Size = new System.Drawing.Size(141, 24);
            this.labelProjectName.TabIndex = 20;
            this.labelProjectName.Text = "Назва проекту";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(15, 11);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(313, 301);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 19;
            this.pictureBox1.TabStop = false;
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.button3.Location = new System.Drawing.Point(14, 360);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(225, 37);
            this.button3.TabIndex = 30;
            this.button3.Text = "Відкрити опис проекту";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // InformationProjectLeaveForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(728, 479);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelGrade);
            this.Controls.Add(this.labelPeople);
            this.Controls.Add(this.labelTheme);
            this.Controls.Add(this.labelDirection);
            this.Controls.Add(this.labelDescription);
            this.Controls.Add(this.labelProjectName);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "InformationProjectLeaveForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Інформація про проект";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.InformationProjectLeaveForm_FormClosed);
            this.Load += new System.EventHandler(this.InformationProjectLeaveForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelGrade;
        private System.Windows.Forms.Label labelPeople;
        private System.Windows.Forms.Label labelTheme;
        private System.Windows.Forms.Label labelDirection;
        private System.Windows.Forms.Label labelDescription;
        private System.Windows.Forms.Label labelProjectName;
        private System.Windows.Forms.PictureBox pictureBox1;
        public System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button3;
    }
}