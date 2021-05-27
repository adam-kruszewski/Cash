using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using System.Linq;

namespace Cash.Controls
{
    public class OneControlGrowingDockPanel : Panel
    {
        private IControl GrowingControl
        {
            get
            {
                if (!string.IsNullOrEmpty(growingControlName))
                    return this.FindControl<IControl>(growingControlName);

                if (growingControlIndex >= 0)
                    return Children[growingControlIndex];

                return null;
            }
        }

        private string growingControlName;
        public string GrowingControlName
        {
            set
            {
                growingControlName = value;
            }
        }

        private int growingControlIndex = -1;
        public int GrowingControlIndex
        {
            set
            {
                growingControlIndex = value;
            }
        }

        public OneControlGrowingDockPanel()
        {
            //Orientation = Orientation.Vertical;
        }

        protected override Size MeasureOverride(Size constraint)
        {
            MeasureAllChildren(constraint);

            double height;

            if (double.IsInfinity(constraint.Height))
                height = Children.Sum(o => o.DesiredSize.Height);
            else
                height = constraint.Height;

            return new Size(constraint.Width, height);
        }

        private void MeasureAllChildren(Size constraint)
        {
            foreach (var control in Children)
                control.Measure(constraint);
        }

        private static double GetDesiredHeight(IControl o, Size avalaibleSize)
        {
            o.Measure(avalaibleSize);
            return o.DesiredSize.Height;
        }

        protected override Size ArrangeOverride(Size arrangeSize)
        {
            var notGrowingControls = Children.Where(o => o != GrowingControl);

            var sumConstSizeHeight = notGrowingControls.Sum(o => o.DesiredSize.Height);

            double currentY = 0;

            foreach (var child in Children)
            {
                if (child != GrowingControl)
                {
                    var childRect = new Rect(
                        0,
                        currentY,
                        arrangeSize.Width,
                        child.DesiredSize.Height);
                    child.Arrange(childRect);
                    currentY += child.DesiredSize.Height;
                }
                else
                {
                    var requestHeight = arrangeSize.Height - sumConstSizeHeight;
                    var childRect = new Rect(
                        0,
                        currentY,
                        arrangeSize.Width,
                        requestHeight);
                    child.Arrange(childRect);
                    (child as Layoutable).MaxHeight = requestHeight;
                    currentY += requestHeight;
                }
            }

            return arrangeSize;
        }
    }
}
