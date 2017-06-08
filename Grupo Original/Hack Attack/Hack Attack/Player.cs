using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Hack_Attack
{
    public class Player : Entity
    {
        float jumpSpeed = 20000f, bulletSpeed = 400f;
        FileManager fileManager;


        List<Bullet> bullets;
        Texture2D bulletImage;
        Boolean time;

        public override void LoadContent(ContentManager content, List<string> attributes, List<string> contents, InputManager input)
        {
            base.LoadContent(content, attributes, contents, input);
            string[] attribute = { "PlayerPosition" };
            string[] ccontent = { position.X.ToString() + "," + position.Y.ToString() };
            fileManager = new FileManager();
            fileManager.SaveContent("Load/Maps/Map1.jxz", attribute, ccontent, "");
            time = true;

            bullets = new List<Bullet>();
            bulletImage = content.Load<Texture2D>("bullet");
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
            moveAnimation.UnloadContent();
        }

        public override void Update(GameTime gameTime, InputManager input, Collision col, Layer layer)
        {
            
            base.Update(gameTime, input, col, layer);
            moveAnimation.DrawColor = Color.White;
            moveAnimation.IsActive = true;

            //BULLETS
            if (input.KeyPressed(Keys.Space))
            {
                Bullet bullet = new Bullet();
                bullet.position.Y = position.Y;
                if (moveAnimation.CurrentFrame.Y == 0)
                {
                    bullet.direction = true;
                    bullet.position.X = position.X + 32;
                }
                else
                {
                    bullet.direction = false;
                    bullet.position.X = position.X - 10;
                }

                bullets.Add(bullet);
            }

            if (input.KeyDown(Keys.LeftShift))
                time = false;
            else
                time = true;

            if (time)
            {
                for (int i = 0; i < bullets.Count; i++)
                {
                    float x = bullets[i].position.X;
                    if (bullets[i].direction)
                        x += bulletSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    else
                        x -= bulletSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    bullets[i].position = new Vector2(x, bullets[i].position.Y);
                }
            }


            if (input.KeyDown(Keys.Right, Keys.D))
            {
                moveAnimation.CurrentFrame = new Vector2(moveAnimation.CurrentFrame.X, 0);
                velocity.X = moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else if (input.KeyDown(Keys.Left, Keys.A))
            {
                moveAnimation.CurrentFrame = new Vector2(moveAnimation.CurrentFrame.X, 1);
                velocity.X = -moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                moveAnimation.IsActive = false;
                velocity.X = 0;
            }           

            if (input.KeyDown(Keys.Up, Keys.W) && !activateGravity)
            {
                activateGravity = true;
                velocity.Y = -jumpSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
               
            }

            if (activateGravity)
                velocity.Y += gravity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            else
                velocity.Y = 0;
    
            position += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

            moveAnimation.Position = position;
            ssAnimation.Update(gameTime, ref moveAnimation);

            Camera.Instance.SetFocalPoint(new Vector2(Position.X, ScreenManager.Instance.Dimensions.Y /2));
        }

        public override void OnCollision(Entity e)
        {
            Type type = e.GetType();
            if(type == typeof(test))
            {
                health--;
                moveAnimation.DrawColor = Color.Red;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            moveAnimation.Draw(spriteBatch);
            for(int i = 0; i<bullets.Count; i++)
                spriteBatch.Draw(bulletImage,bullets[i].position, Color.White);
        }
    }
}