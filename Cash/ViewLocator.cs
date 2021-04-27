using System;
using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Cash.ViewModels;

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
                return (Control)Activator.CreateInstance(type)!;
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
            {typeof(CurrentProductViewModel), typeof(Cash.Views.CurrentProduct) }
        };
    }
}
