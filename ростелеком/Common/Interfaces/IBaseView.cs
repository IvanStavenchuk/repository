using Common.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Interfaces
{
    public interface IBaseView<TObject> where TObject : BaseObject
    {
        event EventHandler<EventArgs> Save;
        void Init(TObject item);
    }
}
