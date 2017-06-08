using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Hack_Attack
{
    public class SpriteSheetAnimation : Animation
    {
        private int frameCounter, switchFrame;
        Vector2 currentFrame;

        public SpriteSheetAnimation()
        {
            frameCounter = 0;
            switchFrame = 100;
        }

        public override void Update(GameTime gameTime, ref Animation a)
        {
            currentFrame = a.CurrentFrame;

            if (a.IsActive)
            {
                frameCounter += (int)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (frameCounter >= switchFrame)
                {
                    frameCounter = 0;
                    currentFrame.X++;

                    if (currentFrame.X * a.FrameWidth >= a.Image.Width)
                        currentFrame.X = 0;
                }
            }
            else
            {
                frameCounter = 0;
                currentFrame.X = 0;
            }
            a.CurrentFrame = currentFrame;
            a.SourceRect = new Rectangle((int)a.CurrentFrame.X * a.FrameWidth, (int)a.CurrentFrame.Y * a.FrameHeight, a.FrameWidth, a.FrameHeight);
        }
    }
}