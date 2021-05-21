using System;

namespace Cash.Controls.GenericListControl
{
    [AttributeUsage(AttributeTargets.Property)]
    public class HeaderAttribute : Attribute
    {
        public string Text { get; set; }

        public HeaderAttribute(string text = null)
        {
            if (text != null)
                Text = text;
        }
    }
}
