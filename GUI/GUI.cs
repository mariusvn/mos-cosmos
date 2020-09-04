using MOS.GUI.drawables;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace MOS.GUI
{
    public class GUI
    {
        public static readonly ushort ScreenWidth = 1280;
        public static readonly ushort ScreenHeight = 720;
        public DoubleBufferedVMWareSVGAII canvas;
        public Color resetColor = Color.LightBlue;
        public enum DrawableLayer : byte
        {
            BOTTOM = 0,
            MIDDLE = 1,
            TOP = 2
        };
        public List<IDrawable> lowerDrawableLayer = new List<IDrawable>
        {
            new TaskBar()
        };
        public List<IDrawable> drawables = new List<IDrawable>
        {
            
        };
        public List<IDrawable> topDrawableLayer = new List<IDrawable>
        {
            new MouseManager()
        };

        public GUI()
        {
            this.canvas = new DoubleBufferedVMWareSVGAII();
            this.canvas.SetMode(ScreenWidth, ScreenHeight);
            for (int i = 0; i < this.lowerDrawableLayer.Count(); i++)
            {
                lowerDrawableLayer[i].init(this.canvas);
            }
            for (int i = 0; i < this.drawables.Count(); i++)
            {
                drawables[i].init(this.canvas);
            }
            for (int i = 0; i < this.topDrawableLayer.Count(); i++)
            {
                topDrawableLayer[i].init(this.canvas);
            }
        }

        public void draw()
        {
            this.clear();
            this.drawLayer(this.lowerDrawableLayer);
            this.drawLayer(this.drawables);
            this.drawLayer(this.topDrawableLayer);
            this.update();
        }

        private void drawLayer(List<IDrawable> layer)
        {
            for (int i = 0, len = layer.Count(); i < len; i++)
            {
                layer.ElementAt(i).draw(this.canvas);
            }
        }

        public void addDrawable(IDrawable drawable, DrawableLayer layer)
        {
            switch (layer)
            {
                case DrawableLayer.BOTTOM:
                    this.lowerDrawableLayer.Add(drawable);
                    break;
                case DrawableLayer.MIDDLE:
                    this.drawables.Add(drawable);
                    break;
                case DrawableLayer.TOP:
                    this.topDrawableLayer.Insert(0, drawable);
                    break;
            }
            drawable.init(canvas);
        }

        public void removeDrawable(IDrawable drawable, DrawableLayer layer)
        {
            switch (layer)
            {
                case DrawableLayer.BOTTOM:
                    this.lowerDrawableLayer.Remove(drawable);
                    break;
                case DrawableLayer.MIDDLE:
                    this.drawables.Remove(drawable);
                    break;
                case DrawableLayer.TOP:
                    this.topDrawableLayer.Remove(drawable);
                    break;
            }
        }

        private void clear()
        {
            this.canvas.DoubleBuffer_Clear((uint)this.resetColor.ToArgb());
        }

        private void update()
        {
            this.canvas.DoubleBuffer_Update();
        }
    }
}
