using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thriple.Graphics;

namespace Thriple.Objects
{
    public class ScoreBoard : IGameObject
    {
        private const int NUM_SPRITE_POS_Y = 1380;
        private const int NUM_SPRITE_POS_X = 0;
        private const int NUM_SPRITE_WIDTH = 70;
        private const int NUM_SPRITE_HEIGHT = 1470 - NUM_SPRITE_POS_Y;
        private const int NUM_POS_Y = 120;
        private const int LEFT_MARGIN = 1450;
        private const int TOP_MARGIN = 150 + NUM_SPRITE_HEIGHT;

        private List<Sprite> _numsSprites;
        private int[] _nums;
        private int[] _numsHighScore;

        public int Score { get; set; }
        private int HighScore { get; set; }

        private Vector2 _position;

        public int DrawOrder => 10;

        public ScoreBoard(Texture2D spriteSheet)
        {
            _nums = new int[] { 0, 0, 0, 0, 0, 0 };
            _numsHighScore = new int[] { 0, 0, 0, 0, 0, 0 };
            _numsSprites = new List<Sprite>();
            _position = new Vector2(LEFT_MARGIN, NUM_POS_Y);
            for (int i = 0; i < 10; i++)
            {
                _numsSprites.Add(
                    new Sprite(spriteSheet, NUM_SPRITE_POS_X + NUM_SPRITE_WIDTH * i, NUM_SPRITE_POS_Y, NUM_SPRITE_WIDTH, NUM_SPRITE_HEIGHT)
                    );
            }
        }

        public void Update(GameTime gameTime)
        {
            if (Score > 999999)
                Score = 999999;
            if (Score >= HighScore)
                HighScore = Score;
            var score = Score.ToString();
            int i = _nums.Length - 1;
            foreach (var num in score.Reverse())
            {
                _nums[i] = int.Parse(num.ToString());
                i--;
            }
            score = HighScore.ToString();
            i = _nums.Length-1;
            foreach (var num in score.Reverse())
            {
                _numsHighScore[i] = int.Parse(num.ToString());
                i--;
            }
        }



        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            for (int i = 0; i < _nums.Length; i++)
            {
                var num = _nums[i];
                var position = new Vector2(_position.X + i * NUM_SPRITE_WIDTH, _position.Y);
                _numsSprites[num].Draw(spriteBatch, position);
                num = _numsHighScore[i];
                position = new Vector2(_position.X + i * NUM_SPRITE_WIDTH, _position.Y + TOP_MARGIN);
                _numsSprites[num].Draw(spriteBatch, position);
            }
        }

        public void Clear()
        {
            _nums = new int[] { 0, 0, 0, 0, 0, 0 };
            Score = 0;
        }
    }
}
