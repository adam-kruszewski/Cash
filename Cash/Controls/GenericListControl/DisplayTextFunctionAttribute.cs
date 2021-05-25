using System;

namespace Cash.Controls.GenericListControl
{
    public class DisplayTextFunctionAttribute : Attribute
    {
        public string MethodNameForDisplay { get; set; }

        public DisplayTextFunctionAttribute()
        {
        }
    }
}
