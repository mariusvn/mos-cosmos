using Cosmos.System.Graphics;
using System.Drawing;
using Point = Cosmos.System.Graphics.Point;

namespace MOS
{
    class TaskBar : IDrawable
    {

        private uint color = (uint)Color.FromArgb(0, 24, 35, 35).ToArgb();
        private const ushort taskBarHeight = 40;
        private int screenWidth = GUI.ScreenWidth, screenHeight = GUI.ScreenHeight;

        void IDrawable.init(DoubleBufferedVMWareSVGAII canvas)
        {
        }

        void IDrawable.draw(DoubleBufferedVMWareSVGAII canvas)
        {
            canvas.DoubleBuffer_DrawFillRectangle(0, (uint)(screenHeight - taskBarHeight), (uint)(screenWidth), taskBarHeight, color);
        }
    }
}
