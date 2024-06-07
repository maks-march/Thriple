using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Thriple.Graphics;

namespace Thriple.Windows
{
    public interface IWindow
    {
        void Draw(SpriteBatch spriteBatch, GameTime gameTime);
    }
}
