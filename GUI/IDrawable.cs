using Cosmos.System.Graphics;

namespace MOS
{
    interface IDrawable
    {
        void init(DoubleBufferedVMWareSVGAII canvas);
        void draw(DoubleBufferedVMWareSVGAII canvas);
    }
}
