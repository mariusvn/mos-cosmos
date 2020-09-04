using Cosmos.Debug.Kernel;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace MOS.GUI.drawables
{
    class ContextMenu: IDrawable
    {
        public uint[] position = new uint[2];
        private ushort itemsIncrement = 0;
        private List<ContextMenuItem> itemList = new List<ContextMenuItem>();
        private readonly uint backgroundColor = (uint)Color.Gray.ToArgb();
        private Debugger debugger = new Debugger("drawables", "ContextMenu");

        public ContextMenu(uint x, uint y)
        {
            this.position[0] = x;
            this.position[1] = y;
            Kernel.gui.addDrawable(this, GUI.DrawableLayer.TOP);
        }

        void IDrawable.init(DoubleBufferedVMWareSVGAII canvas)
        {

        }

        void IDrawable.draw(DoubleBufferedVMWareSVGAII canvas)
        {
            this.debugger.Send("Loooooop");
            canvas.DoubleBuffer_DrawFillRectangle(this.position[0], this.position[1], 100, 200, backgroundColor);
        }

        public void Close()
        {
            Kernel.gui.removeDrawable(this, GUI.DrawableLayer.TOP);
        }

        public ushort AddMenuItem(string text, Action action)
        {
            ContextMenuItem item = new ContextMenuItem();
            item.text = text;
            item.action = action;
            item.id = this.itemsIncrement;
            item.isEnabled = true;
            this.itemsIncrement++;
            itemList.Add(item);
            return item.id;
        }

        public void RemoveItem(ushort itemId)
        {
            for (ushort i = 0; i < this.itemList.Count; i++)
            {
                if (this.itemList[i].id == itemId)
                {
                    this.itemList.RemoveAt(i);
                }
            }
        }

        public void SetItemEnabled(ushort itemId, bool isEnabled)
        {
            for (ushort i = 0; i < this.itemList.Count; i++)
            {
                if (this.itemList[i].id == itemId)
                {
                    ContextMenuItem item = this.itemList[i];
                    item.isEnabled = isEnabled;
                    this.itemList[i] = item;
                }
            }
        }

        private struct ContextMenuItem
        {
            public ushort id;
            public string text;
            public Action action;
            public bool isEnabled;
        }
    }
}
