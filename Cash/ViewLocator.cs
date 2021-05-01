using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Cash.ViewModels;
using Cash.Views;
using System;
using System.Collections.Generic;

namespace Cash
{
    public class ViewLocator : IDataTemplate
    {
        public bool SupportsRecycling => false;

        public IControl Build(object data)
        {
            Type type;
            string name = "";

            if (mappings.ContainsKey(data.GetType()))
            {
                type = mappings[data.GetType()];
            }
            else
            {

                name = data.GetType().FullName!.Replace(".ViewModels", ".View");
                type = Type.GetType(name);
            }

            if (type != null)
            {
                var result = (Control)App.ServiceProvider.GetService(type);

                return result;
            }
            else
            {
                return new TextBlock { Text = "Not Found: " + name };
            }
        }

        public bool Match(object data)
        {
            return data is ViewModelBase;
        }

        private Dictionary<Type, Type> mappings = new Dictionary<Type, Type>
        {
            { typeof(CurrentProductViewModel), typeof(CurrentProduct) },
            { typeof(ProductListViewModel), typeof (ProductList) }

        };
    }
}
