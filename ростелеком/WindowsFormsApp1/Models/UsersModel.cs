using Common.Objects;
using System.Net.Http;

namespace Desktop.Models
{
    internal class UsersModel : BaseModel<User>
    {
        internal UsersModel() : base("http://localhost:64196/", "api/Users")
        {
        }
    }
}
