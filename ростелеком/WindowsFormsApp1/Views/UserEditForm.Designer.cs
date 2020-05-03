namespace Desktop.Views
{
    partial class UserEditForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.LastNameTextBox = new System.Windows.Forms.TextBox();
            this.LastNameLabel = new System.Windows.Forms.Label();
            this.FirstNameLabel = new System.Windows.Forms.Label();
            this.MiddleNameLabel = new System.Windows.Forms.Label();
            this.DeptLabel = new System.Windows.Forms.Label();
            this.FirstNameTextBox = new System.Windows.Forms.TextBox();
            this.MiddleNameTextBox = new System.Windows.Forms.TextBox();
            this.DeptsCombo = new System.Windows.Forms.ComboBox();
            this.SaveButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LastNameTextBox
            // 
            this.LastNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LastNameTextBox.Location = new System.Drawing.Point(120, 12);
            this.LastNameTextBox.Name = "LastNameTextBox";
            this.LastNameTextBox.Size = new System.Drawing.Size(206, 23);
            this.LastNameTextBox.TabIndex = 0;
            // 
            // LastNameLabel
            // 
            this.LastNameLabel.AutoSize = true;
            this.LastNameLabel.Location = new System.Drawing.Point(31, 16);
            this.LastNameLabel.Name = "LastNameLabel";
            this.LastNameLabel.Size = new System.Drawing.Size(58, 15);
            this.LastNameLabel.TabIndex = 1;
            this.LastNameLabel.Text = "Фамилия";
            // 
            // FirstNameLabel
            // 
            this.FirstNameLabel.AutoSize = true;
            this.FirstNameLabel.Location = new System.Drawing.Point(31, 44);
            this.FirstNameLabel.Name = "FirstNameLabel";
            this.FirstNameLabel.Size = new System.Drawing.Size(31, 15);
            this.FirstNameLabel.TabIndex = 2;
            this.FirstNameLabel.Text = "Имя";
            // 
            // MiddleNameLabel
            // 
            this.MiddleNameLabel.AutoSize = true;
            this.MiddleNameLabel.Location = new System.Drawing.Point(31, 73);
            this.MiddleNameLabel.Name = "MiddleNameLabel";
            this.MiddleNameLabel.Size = new System.Drawing.Size(58, 15);
            this.MiddleNameLabel.TabIndex = 3;
            this.MiddleNameLabel.Text = "Отчество";
            // 
            // DeptLabel
            // 
            this.DeptLabel.AutoSize = true;
            this.DeptLabel.Location = new System.Drawing.Point(31, 103);
            this.DeptLabel.Name = "DeptLabel";
            this.DeptLabel.Size = new System.Drawing.Size(40, 15);
            this.DeptLabel.TabIndex = 5;
            this.DeptLabel.Text = "Отдел";
            // 
            // FirstNameTextBox
            // 
            this.FirstNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FirstNameTextBox.Location = new System.Drawing.Point(120, 41);
            this.FirstNameTextBox.Name = "FirstNameTextBox";
            this.FirstNameTextBox.Size = new System.Drawing.Size(206, 23);
            this.FirstNameTextBox.TabIndex = 0;
            // 
            // MiddleNameTextBox
            // 
            this.MiddleNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MiddleNameTextBox.Location = new System.Drawing.Point(120, 70);
            this.MiddleNameTextBox.Name = "MiddleNameTextBox";
            this.MiddleNameTextBox.Size = new System.Drawing.Size(206, 23);
            this.MiddleNameTextBox.TabIndex = 0;
            // 
            // DeptsCombo
            // 
            this.DeptsCombo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DeptsCombo.FormattingEnabled = true;
            this.DeptsCombo.Location = new System.Drawing.Point(120, 100);
            this.DeptsCombo.Name = "DeptsCombo";
            this.DeptsCombo.Size = new System.Drawing.Size(206, 23);
            this.DeptsCombo.TabIndex = 6;
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(251, 129);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(75, 23);
            this.SaveButton.TabIndex = 8;
            this.SaveButton.Text = "Сохранить";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // UserEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(360, 157);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.DeptsCombo);
            this.Controls.Add(this.DeptLabel);
            this.Controls.Add(this.MiddleNameLabel);
            this.Controls.Add(this.FirstNameLabel);
            this.Controls.Add(this.LastNameLabel);
            this.Controls.Add(this.MiddleNameTextBox);
            this.Controls.Add(this.FirstNameTextBox);
            this.Controls.Add(this.LastNameTextBox);
            this.Name = "UserEditForm";
            this.Text = "Пользователь:";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox LastNameTextBox;
        private System.Windows.Forms.Label LastNameLabel;
        private System.Windows.Forms.Label FirstNameLabel;
        private System.Windows.Forms.Label MiddleNameLabel;
        private System.Windows.Forms.Label DeptLabel;
        private System.Windows.Forms.TextBox FirstNameTextBox;
        private System.Windows.Forms.TextBox MiddleNameTextBox;
        private System.Windows.Forms.ComboBox DeptsCombo;
        private System.Windows.Forms.Button SaveButton;
    }
}

