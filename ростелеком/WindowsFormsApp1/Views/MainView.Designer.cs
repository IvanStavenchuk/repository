namespace Desktop.Views
{
    partial class MainView
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
            this.DeptsListBox = new System.Windows.Forms.ListBox();
            this.UsersListBox = new System.Windows.Forms.ListBox();
            this.DeptsLabel = new System.Windows.Forms.Label();
            this.UsersLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // DeptsListBox
            // 
            this.DeptsListBox.FormattingEnabled = true;
            this.DeptsListBox.ItemHeight = 15;
            this.DeptsListBox.Location = new System.Drawing.Point(13, 31);
            this.DeptsListBox.Name = "DeptsListBox";
            this.DeptsListBox.Size = new System.Drawing.Size(280, 334);
            this.DeptsListBox.TabIndex = 0;
            // 
            // UsersListBox
            // 
            this.UsersListBox.FormattingEnabled = true;
            this.UsersListBox.ItemHeight = 15;
            this.UsersListBox.Location = new System.Drawing.Point(299, 31);
            this.UsersListBox.Name = "UsersListBox";
            this.UsersListBox.Size = new System.Drawing.Size(272, 334);
            this.UsersListBox.TabIndex = 1;
            // 
            // DeptsLabel
            // 
            this.DeptsLabel.AutoSize = true;
            this.DeptsLabel.Location = new System.Drawing.Point(13, 13);
            this.DeptsLabel.Name = "DeptsLabel";
            this.DeptsLabel.Size = new System.Drawing.Size(49, 15);
            this.DeptsLabel.TabIndex = 2;
            this.DeptsLabel.Text = "Отделы";
            // 
            // UsersLabel
            // 
            this.UsersLabel.AutoSize = true;
            this.UsersLabel.Location = new System.Drawing.Point(299, 13);
            this.UsersLabel.Name = "UsersLabel";
            this.UsersLabel.Size = new System.Drawing.Size(85, 15);
            this.UsersLabel.TabIndex = 3;
            this.UsersLabel.Text = "Пользователи";
            // 
            // MainView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(583, 450);
            this.Controls.Add(this.UsersLabel);
            this.Controls.Add(this.DeptsLabel);
            this.Controls.Add(this.UsersListBox);
            this.Controls.Add(this.DeptsListBox);
            this.Name = "MainView";
            this.Text = "MainView";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox DeptsListBox;
        private System.Windows.Forms.ListBox UsersListBox;
        private System.Windows.Forms.Label DeptsLabel;
        private System.Windows.Forms.Label UsersLabel;
    }
}