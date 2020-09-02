using Cosmos.System.Graphics;
using System.Drawing;
using Point = Cosmos.System.Graphics.Point;

namespace MOS
{
    class TaskBar : IDrawable
    {

        private Pen pen = new Pen(Color.FromArgb(0, 24, 35, 35));
        private const ushort taskBarHeight = 40;
        private int screenWidth, screenHeight;

        void IDrawable.init(Canvas canvas)
        {
            screenWidth = canvas.Mode.Columns;
            screenHeight = canvas.Mode.Rows;
        }

        void IDrawable.draw(Canvas canvas)
        {
            canvas.DrawFilledRectangle(this.pen, new Point(0, screenHeight - taskBarHeight), screenWidth, taskBarHeight);
        }
    }
}
