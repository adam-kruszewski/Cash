using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using System;
using System.Linq;

namespace Cash.Controls
{
    class FillingAreaDockPanel : DockPanel
    {
        protected override Size MeasureOverride(Size constraint)
        {
            var children = Children;

            double parentWidth = 0;   // Our current required width due to children thus far.
            double parentHeight = 0;   // Our current required height due to children thus far.
            double accumulatedWidth = 0;   // Total width consumed by children.
            double accumulatedHeight = 0;   // Total height consumed by children.

            for (int i = 0, count = children.Count; i < count; ++i)
            {
                var child = children[i];
                Size childConstraint;             // Contains the suggested input constraint for this child.
                Size childDesiredSize;            // Contains the return size from child measure.

                if (child == null)
                { continue; }

                // Child constraint is the remaining size; this is total size minus size consumed by previous children.
                childConstraint = new Size(Math.Max(0.0, constraint.Width - accumulatedWidth),
                                           Math.Max(0.0, constraint.Height - accumulatedHeight));

                // Measure child.
                child.Measure(childConstraint);
                childDesiredSize = child.DesiredSize;

                // Now, we adjust:
                // 1. Size consumed by children (accumulatedSize).  This will be used when computing subsequent
                //    children to determine how much space is remaining for them.
                // 2. Parent size implied by this child (parentSize) when added to the current children (accumulatedSize).
                //    This is different from the size above in one respect: A Dock.Left child implies a height, but does
                //    not actually consume any height for subsequent children.
                // If we accumulate size in a given dimension, the next child (or the end conditions after the child loop)
                // will deal with computing our minimum size (parentSize) due to that accumulation.
                // Therefore, we only need to compute our minimum size (parentSize) in dimensions that this child does
                //   not accumulate: Width for Top/Bottom, Height for Left/Right.
                switch (DockPanel.GetDock((Control)child))
                {
                    case Dock.Left:
                    case Dock.Right:
                        parentHeight = Math.Max(parentHeight, accumulatedHeight + childDesiredSize.Height);
                        accumulatedWidth += childDesiredSize.Width;
                        break;

                    case Dock.Top:
                    case Dock.Bottom:
                        parentWidth = Math.Max(parentWidth, accumulatedWidth + childDesiredSize.Width);
                        accumulatedHeight += childDesiredSize.Height;
                        break;
                }
            }

            // Make sure the final accumulated size is reflected in parentSize.
            parentWidth = Math.Max(parentWidth, accumulatedWidth);
            parentHeight = Math.Max(parentHeight, accumulatedHeight);

            return (new Size(parentWidth, parentHeight));
        }

        protected override Size ArrangeOverride(Size arrangeSize)
        {
            var children = Children;

            if (children.Count == 3)
                return Arrange3Children(arrangeSize);

            int totalChildrenCount = children.Count;
            int nonFillChildrenCount = totalChildrenCount - (LastChildFill ? 1 : 0);

            double accumulatedLeft = 0;
            double accumulatedTop = 0;
            double accumulatedRight = 0;
            double accumulatedBottom = 0;

            for (int i = 0; i < totalChildrenCount; ++i)
            {
                var child = children[i];
                if (child == null)
                { continue; }

                Size childDesiredSize = child.DesiredSize;
                Rect rcChild = new Rect(
                    accumulatedLeft,
                    accumulatedTop,
                    Math.Max(0.0, arrangeSize.Width - (accumulatedLeft + accumulatedRight)),
                    Math.Max(0.0, arrangeSize.Height - (accumulatedTop + accumulatedBottom)));

                if (i < nonFillChildrenCount)
                {
                    switch (DockPanel.GetDock((Control)child))
                    {
                        case Dock.Left:
                            accumulatedLeft += childDesiredSize.Width;
                            rcChild = rcChild.WithWidth(childDesiredSize.Width);
                            break;

                        case Dock.Right:
                            accumulatedRight += childDesiredSize.Width;
                            rcChild = rcChild.WithX(Math.Max(0.0, arrangeSize.Width - accumulatedRight));
                            rcChild = rcChild.WithWidth(childDesiredSize.Width);
                            break;

                        case Dock.Top:
                            accumulatedTop += childDesiredSize.Height;
                            rcChild = rcChild.WithHeight(childDesiredSize.Height);
                            break;

                        case Dock.Bottom:
                            accumulatedBottom += childDesiredSize.Height;
                            rcChild = rcChild.WithY(Math.Max(0.0, arrangeSize.Height - accumulatedBottom));
                            rcChild = rcChild.WithHeight(childDesiredSize.Height);
                            break;
                    }
                }

                child.Arrange(rcChild);
            }

            return (arrangeSize);
        }

        private Size Arrange3Children(Size arrangeSize)
        {
            var first = Children.First();

            var firstChildRect = new Rect(0, 0, arrangeSize.Width, first.DesiredSize.Height);
            first.Arrange(firstChildRect);

            var lastChild = Children.Last();
            var lastChildRect = new Rect(
                0,
                arrangeSize.Height - lastChild.DesiredSize.Height,
                arrangeSize.Width,
                lastChild.DesiredSize.Height);
            lastChild.Arrange(lastChildRect);

            var midChild = Children[1];
            var midChildHeight = lastChildRect.Y - firstChildRect.Height;
            var midChildRect = new Rect(
                0,
                firstChildRect.Height,
                arrangeSize.Width,
                midChildHeight);
            var maxMidHeight = lastChildRect.Y - firstChildRect.Height;

            midChild.Arrange(midChildRect);

            (midChild as Layoutable).MaxHeight = midChildHeight;

            return arrangeSize;
        }
    }
}
