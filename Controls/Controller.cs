using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thriple.Objects;

namespace Thriple.Controls
{
    public class Controller
    {
        Figure _figure;

        public Controller(Figure figure) 
        {
            _figure = figure;
        }

        public bool ProcessControlls(KeyboardState keyboardState)
        {
            if (_figure.IsClear)
                return false;
            if (keyboardState.IsKeyDown(Keys.Space))
            {
                _figure.ChangeColors();
                return true;
            }
            if (keyboardState.IsKeyDown(Keys.Left) ||  keyboardState.IsKeyDown(Keys.A))
            {
                if (_figure.MoveLeft())
                    _figure.IsMovableRight = true;
                return true;
            }
            if (keyboardState.IsKeyDown(Keys.Right) ||  keyboardState.IsKeyDown(Keys.D))
            {
                if (_figure.MoveRight())
                    _figure.IsMovableLeft = true;
                return true;
            }
            if (keyboardState.IsKeyDown(Keys.Down) ||  keyboardState.IsKeyDown(Keys.S))
            {
                _figure.MoveDown();
                return true;
            }
            return false;
        }
    }
}
