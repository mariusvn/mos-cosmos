﻿using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace MOS
{
    class GUI
    {
        public static readonly ushort ScreenWidth = 1280;
        public static readonly ushort ScreenHeight = 720;
        public DoubleBufferedVMWareSVGAII canvas;
        public Color resetColor = Color.LightBlue;
        public List<IDrawable> drawables;

        public GUI()
        {
            this.canvas = new DoubleBufferedVMWareSVGAII();
            this.canvas.SetMode(ScreenWidth, ScreenHeight);
            this.drawables = this.getAllDrawables();
            for (int i = 0; i < this.drawables.Count(); i++)
            {
                drawables[i].init(this.canvas);
            }
        }

        public void draw()
        {
            this.clear();
            for (int i = 0; i < this.drawables.Count(); i++)
            {
                this.drawables.ElementAt(i).draw(this.canvas);
            }
            this.update();
        }

        private void clear()
        {
            this.canvas.DoubleBuffer_Clear((uint)this.resetColor.ToArgb());
        }

        private void update()
        {
            this.canvas.DoubleBuffer_Update();
        }

        private List<IDrawable> getAllDrawables()
        {
            return new List<IDrawable>
            {
                new TaskBar(),
                new MouseManager()
            };
        } 
    }
}
