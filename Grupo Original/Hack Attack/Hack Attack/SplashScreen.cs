using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace Hack_Attack
{
    public class SplashScreen : GameScreen
    {
        SpriteFont font;
        List<Animation> animation;
        List<Texture2D> images;

        FileManager fileManager;

        int imageNumber;

        FadeAnimation FAnimation;

        public override void LoadContent(ContentManager Content, InputManager inputManager)
        {
            base.LoadContent(Content, inputManager);
            if (font == null)
                font = this.content.Load<SpriteFont>("Font1");

            imageNumber = 0;
            fileManager = new FileManager();
            animation = new List<Animation>();
            FAnimation = new FadeAnimation();
            images = new List<Texture2D>();

            fileManager.LoadContent("Load/Splash.jxz", "");

            for(int i=0; i<fileManager.Attributes.Count; i++)
            {
                for(int j=0; j<fileManager.Attributes[i].Count; j++)
                {
                    switch (fileManager.Attributes[i][j])
                    {
                        case "Image":
                            images.Add(this.content.Load<Texture2D>(contents[i][j]));
                            animation.Add(new FadeAnimation());
                            break;
                    }
                }
            }
            for (int i = 0; i < animation.Count; i++)
            {
                animation[i].LoadContent(content, images[i], "",new Vector2(80,60));
                animation[i].Scale = 1.25f;
                animation[i].IsActive = true;
            }

            string[] attribute = { "Attribute1" };
            string[] ccontent = { "Content1", "Content2" };

            fileManager.SaveContent("Load/Splash.jxz", attribute, ccontent, "");
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
            fileManager = null;
        }

        public override void Update(GameTime gameTime)
        {
            inputManager.Update();

            Animation a = animation[imageNumber];
            FAnimation.Update(gameTime, ref a);
            animation[imageNumber] = a;

            if (animation[imageNumber].Alpha == 0.0f)
                imageNumber++;

            if(imageNumber >= animation.Count -1 || inputManager.KeyPressed(Keys.Z))
            {
                ScreenManager.Instance.AddScreen(new TitleScreen(), inputManager);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            animation[imageNumber].Draw(spriteBatch);
        }
    }
}
