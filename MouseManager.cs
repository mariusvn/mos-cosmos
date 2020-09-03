using Cosmos.System.Graphics;
using System.Drawing;
using Cosmos.Debug.Kernel;
using System.Numerics;

namespace MOS
{
    class MouseManager : IDrawable
    {
        private Pen blackPen = new Pen(Color.Black);
        private Pen whitePen = new Pen(Color.White);
        private Debugger debugger = new Debugger("Drawable", "MouseManager");
        private uint screenSizeX = GUI.ScreenWidth;
        private uint screenSizeY = GUI.ScreenHeight;
        private bool firstLoop = true;

        int[] cursor = new int[]
            {
                1,0,0,0,0,0,0,0,0,0,0,0,
                1,1,0,0,0,0,0,0,0,0,0,0,
                1,2,1,0,0,0,0,0,0,0,0,0,
                1,2,2,1,0,0,0,0,0,0,0,0,
                1,2,2,2,1,0,0,0,0,0,0,0,
                1,2,2,2,2,1,0,0,0,0,0,0,
                1,2,2,2,2,2,1,0,0,0,0,0,
                1,2,2,2,2,2,2,1,0,0,0,0,
                1,2,2,2,2,2,2,2,1,0,0,0,
                1,2,2,2,2,2,2,2,2,1,0,0,
                1,2,2,2,2,2,2,2,2,2,1,0,
                1,2,2,2,2,2,2,2,2,2,2,1,
                1,2,2,2,2,2,2,1,1,1,1,1,
                1,2,2,2,1,2,2,1,0,0,0,0,
                1,2,2,1,0,1,2,2,1,0,0,0,
                1,2,1,0,0,1,2,2,1,0,0,0,
                1,1,0,0,0,0,1,2,2,1,0,0,
                0,0,0,0,0,0,1,2,2,1,0,0,
                0,0,0,0,0,0,0,1,1,0,0,0
            };

        uint[] savedSubLayer = new uint[12 * 19];
        int[] savedPosition = new int[2];

        void IDrawable.init(DoubleBufferedVMWareSVGAII canvas)
        {
            Cosmos.System.MouseManager.ScreenWidth = screenSizeX;
            Cosmos.System.MouseManager.ScreenHeight = screenSizeY;
        }

        void IDrawable.draw(DoubleBufferedVMWareSVGAII canvas)
        {
            int x = (int)(Cosmos.System.MouseManager.X);
            int y = (int)(Cosmos.System.MouseManager.Y);
            if (x < 0) x = 0;
            if (y < 0) y = 0;
            if (x >= screenSizeX) x = (int) screenSizeX - 1;
            if (y >= screenSizeY) y = (int) screenSizeY - 1;

            if (!firstLoop)
            {
                RestoreSavedSublayer(canvas);
            } 
            else
            {
                firstLoop = false;
            }
            this.CaptureCurrentSublayer(x, y, canvas);
            this.DrawCursorAt(x, y, canvas);
        }

        private void DrawCursorAt(int x, int y, DoubleBufferedVMWareSVGAII canvas)
        {
            for (ushort h = 0; h < 19; h++)
            {
                for (ushort w = 0; w < 12; w++)
                {
                    if (cursor[h * 12 + w] == 1)
                    {
                        canvas.DoubleBuffer_SetPixel((uint)(w + x), (uint)(h + y), (uint)Color.Black.ToArgb());
                    }
                    if (cursor[h * 12 + w] == 2)
                    {
                        canvas.DoubleBuffer_SetPixel((uint)(w + x), (uint)(h + y), (uint)Color.White.ToArgb());
                    }
                }
            }
        }

        private void CaptureCurrentSublayer(int x, int y, DoubleBufferedVMWareSVGAII canvas)
        {
            savedPosition[0] = x;
            savedPosition[1] = y;
            for (ushort h = 0; h < 19; h++)
            {
                for (ushort w = 0; w < 12; w++)
                {
                    savedSubLayer[h * 12 + w] = canvas.DoubleBuffer_GetPixel((uint)(w + x), (uint)(h + y));
                }
            }
        }

        private void RestoreSavedSublayer(DoubleBufferedVMWareSVGAII canvas)
        {
            //canvas.DrawArray(savedSubLayer, x, y, 12, 19);
            for (ushort h = 0; h < 19; h++)
            {
                for (ushort w = 0; w < 12; w++)
                {
                    //savedSubLayer[h * 12 + w] = canvas.GetPointColor(w + x, h + y);
                   // Pen pen = new Pen(savedSubLayer[h * 12 + w]);
                    canvas.DoubleBuffer_SetPixel((uint)(savedPosition[0] + w), (uint)(savedPosition[1] + h), savedSubLayer[h * 12 + w]);
                }
            }
        }
    }
}
