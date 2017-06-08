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
    public class Map
    {
        public Layer layer;
        public Collision collision;

        string id;

        public string ID
        {
            get { return id; }
        }

        public void LoadContent(ContentManager content, Map map, string mapID)
        {
            layer = new Layer();
            collision = new Collision();
            id = mapID;

            layer.LoadContent(map, "Layer1");
            collision.LoadContent(content, mapID);


        }

        public void UnloadContent()
        {
            //layer.UnloadContent();
            //collision.UnloadContent();
        }

        public void Update(GameTime gameTime)
        {
            layer.Update(gameTime);
        }

        public void UpdateCollision(ref Entity e)
        {
            layer.UpdateCollision(ref e);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            layer.Draw(spriteBatch);
        }
    }
}
