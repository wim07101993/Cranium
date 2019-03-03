using System.Windows;
using System.Windows.Controls;

namespace Cranium.WPF.Game.GameBoard
{
    public class GameBoardPanel : Panel
    {
        /*
         * 0  1  2  3  4  5  6  7  8
         * P  1  2  3  4  P  1  2  3  4 9
         *                            P 10
         *       tileCount = 25       1 11
         *       tilesPerSide= 8      2 12
         *       hCount = 9           3 13
         *       vCount = 8           4 14
         * 25 P                       P 15
         *    4  3  2  1  P  4  3  2  1 16
         *    24 23 22 21 20 19 18 17
         */

        protected override Size MeasureOverride(Size availableSize)
        {
            var panelDesiredSize = new Size();

            // don't count the last tile
            var tileCount = InternalChildren.Count - 1;
            var tileCountPerSide = tileCount / 3;
            
            // one extra for right side
            var countUntilFirstCorner = tileCount > tileCountPerSide * 3
                ? tileCountPerSide + 2
                : tileCountPerSide + 1;
            // subtract one because of the corner
            var countUntilSecondCorner = countUntilFirstCorner - 1 + tileCountPerSide;

            var i = 0;
            foreach (UIElement child in InternalChildren)
            {
                child.Measure(availableSize);
                if (i < countUntilFirstCorner)
                    panelDesiredSize.Width += child.DesiredSize.Width;
                else if (i < countUntilSecondCorner)
                    panelDesiredSize.Height += child.DesiredSize.Height;
                else
                    continue;

                i++;
            }

            return panelDesiredSize;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            // don't count the last tile
            var tileCount = InternalChildren.Count - 1;
            var tileCountPerSide = tileCount / 3;
            
            // one extra for the vertical side
            var countUntilFirstCorner = tileCount > tileCountPerSide * 3 
                ? tileCountPerSide + 2
                : tileCountPerSide + 1;
            // subtract one because of the corner
            var countUntilSecondCorner = countUntilFirstCorner - 1 + tileCountPerSide;

            double x = 0;
            double y = 0;

            var i = 0;
            foreach (UIElement child in InternalChildren)
            {
                child.Arrange(new Rect(new Point(x, y), child.DesiredSize));

                if (i < countUntilFirstCorner - 1)
                    x += child.DesiredSize.Width;
                else if (i < countUntilSecondCorner - 2)
                    y += child.DesiredSize.Height;
                else if (i < tileCount - 1)
                    x -= child.DesiredSize.Width;
                else
                    y -= child.DesiredSize.Height;

                i++;
            }

            return finalSize; // Returns the final Arranged size
        }
    }
}