using Common.Interfaces;
using Common.Objects;
using Desktop.Models;

namespace Desktop.Presenters
{
    internal class UsersEditPresenter : BaseEditPresenter<UsersModel, IUserEditView, User>
    {
        public UsersEditPresenter(UsersModel model, IUserEditView view, User user) : base(model, view, user)
        {
        }
    }
}
