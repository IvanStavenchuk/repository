using Common.Interfaces;
using Common.Objects;
using Desktop.Models;
using System;

namespace Desktop.Presenters
{
    internal class BaseEditPresenter <TModel, TView, TObject> 
        where TModel : BaseModel<TObject> 
        where TView : IBaseView<TObject>
        where TObject : BaseObject, new()
    {
        private TModel _model;
        private TView _view;
        private TObject _item;
        public BaseEditPresenter(TModel model, TView view, TObject item)
        {
            _model = model;
            _view = view;
            _item = item;

            if (_item == null)
                _item = new TObject();

            _view.Save += View_Save;
        }

        internal virtual void View_Save(object sender, EventArgs e)
        {
            if (_item.Id == 0)
                _item = _model.CreateProductAsync(_item).Result;
            else
                _item = _model.UpdateItemAsync(_item).Result;
        }
    }
}
