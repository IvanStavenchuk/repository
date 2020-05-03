using Desktop.Models;
using Desktop.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace Desktop.Presenters
{
    internal class DeptEditPresenter : BaseEditPresenter<DeptModel, DeptEditView>
    {
        public DeptEditPresenter(DeptModel model, DeptEditView view) : base(model, view)
        {
        }
    }
}
