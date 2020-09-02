using Cosmos.System.Graphics;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace MOS
{
    class GUI
    {
        public Canvas canvas;
        public Color resetColor = Color.LightBlue;
        public List<IDrawable> drawables;

        public GUI()
        {
            this.canvas = FullScreenCanvas.GetFullScreenCanvas();
            this.canvas.Clear(this.resetColor);
            this.drawables = this.getAllDrawables();
            for (int i = 0; i < this.drawables.Count(); i++)
            {
                drawables[i].init(this.canvas);
            }
        }

        public void draw()
        {
            for (int i = 0; i < this.drawables.Count(); i++)
            {
                this.drawables.ElementAt(i).draw(this.canvas);
            }
        }

        private void clear()
        {
            this.canvas.Clear(this.resetColor);
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
