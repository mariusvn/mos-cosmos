using Cosmos.System.Graphics;

namespace MOS
{
    interface IDrawable
    {
        void init(Canvas canvas);
        void draw(Canvas canvas);
    }
}
