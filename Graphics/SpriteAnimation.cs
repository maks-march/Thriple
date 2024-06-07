using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thriple.Objects;

namespace Thriple.Graphics
{
    public class SpriteAnimation
    {
        public SpriteAnimation(float stepTime, params Sprite[] sprites) 
        {
            float timeStamp = 0;
            foreach (var sprite in sprites)
            {
                AddFrame(sprite, timeStamp);
                timeStamp += stepTime;
            }
        }

        private List<SpriteAnimationFrame> _frames = new List<SpriteAnimationFrame>();

        public bool ShouldLoop { get; set; } = true;
        public bool IsPlaying { get; private set; } = false;
        public float PlaybackProgress { get; private set; }
        public Vector2 Position { get; private set; }

        public SpriteAnimationFrame CurrentFrame
        {
            get
            {
                return _frames
                    .Where(frame => frame.TimeStamp <= PlaybackProgress)
                    .LastOrDefault();
            }
        }
        public float Duration
        {
            get
            {
                if (!_frames.Any())
                    return 0;
                return _frames.Max(frame => frame.TimeStamp);
            }
        }

        public SpriteAnimationFrame this[int index]
        {
            get
            {
                if (index < 0 || index >= _frames.Count)
                    throw new ArgumentOutOfRangeException(nameof(index));
                return _frames[index];
            }
        }

        public void AddFrame(Sprite sprite, float timeStamp)
        {
            SpriteAnimationFrame frame = new SpriteAnimationFrame(sprite, timeStamp);

            _frames.Add(frame);
        }

        public void Update(GameTime gameTime)
        {
            if (IsPlaying)
            {
                PlaybackProgress += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (PlaybackProgress > Duration)
                {
                    if (ShouldLoop)
                        PlaybackProgress -= Duration;
                    else
                        Stop();
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {

            SpriteAnimationFrame frame = CurrentFrame;

            if (frame != null)
            {
                frame.Draw(spriteBatch, position);
            }
        }

        public void Play()
        {
            IsPlaying = true;
        }

        public void Stop()
        {
            IsPlaying = false;
            PlaybackProgress = 0;
        }

        public void Clear()
        {
            Stop();
            _frames.Clear();
        }
    }
}
