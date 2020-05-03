using Common.Objects;

namespace Desktop.Models
{
    internal class DeptModel : BaseModel<Department>
    {
        internal DeptModel() : base("http://localhost:64195/", "api/Departments")
        {
        }
    }
}
