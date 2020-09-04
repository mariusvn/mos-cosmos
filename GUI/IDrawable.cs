using Cosmos.System.Graphics;

namespace MOS
{
    public interface IDrawable
    {
        void init(DoubleBufferedVMWareSVGAII canvas);
        void draw(DoubleBufferedVMWareSVGAII canvas);
    }
}
