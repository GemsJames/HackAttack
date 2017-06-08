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
    class GamePlayScreen : GameScreen
    {
        EntityManager player, enemies,test;
        Map map;
        Texture2D background;
        Rectangle mainFrame,mainFrame2;

        public override void LoadContent(ContentManager content, InputManager input)
        {
            base.LoadContent(content, input);
            test = new EntityManager();
            player = new EntityManager();
            //enemies = new EntityManager();
            map = new Map();
            map.LoadContent(content, map, "Map1");
            //enemies.LoadContent("Enemy", content, "Load/Enem.jxz", "Level1", input);
            test.LoadContent("test", content, "Load/Enem.jxz", "", input);
            player.LoadContent("Player", content, "Load/Player.jxz" ,"", input);

            background = content.Load<Texture2D>("background");
            mainFrame = new Rectangle(0, 0, 800, 600);
            mainFrame2 = new Rectangle(800, 0, 800, 600);
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
            player.UnloadContent();
            map.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            inputManager.Update();
            player.Update(gameTime, map);
            test.Update(gameTime, map);
            map.Update(gameTime);

            Entity e;
            for (int i = 0; i <player.Entities.Count; i++)
            {
                e = player.Entities[i];
                map.UpdateCollision(ref e);
                player.Entities[i] = e;
            }


            for (int i = 0; i < test.Entities.Count; i++)
            {
                e = test.Entities[i];
                map.UpdateCollision(ref e);
                test.Entities[i] = e;
            }

            player.EntityCollision(test);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, mainFrame, Color.White);
            spriteBatch.Draw(background, mainFrame2, Color.White);
            base.Draw(spriteBatch);
            map.Draw(spriteBatch);
            player.Draw(spriteBatch);
            test.Draw(spriteBatch);
        }
    }
}
