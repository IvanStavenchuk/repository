using System;
using System.Windows.Forms;
using Common.Interfaces;
using Common.Objects;

namespace Desktop.Views
{
    public partial class UserEditForm : Form, IUserEditView
    {
        public UserEditForm()
        {
            InitializeComponent();
        }

        public event EventHandler<EventArgs> Save;

        public void Init(User item)
        {
            FirstNameTextBox.DataBindings.Add("Text", item, nameof(item.FirstName));
            MiddleNameTextBox.DataBindings.Add("Text", item, nameof(item.MiddletName));
            LastNameTextBox.DataBindings.Add("Text", item, nameof(item.LastName));
            DeptsCombo.DataBindings.Add("SelectedValue", item, nameof(item.DeptId));
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            Save?.Invoke(SaveButton, e);
        }
    }
}
