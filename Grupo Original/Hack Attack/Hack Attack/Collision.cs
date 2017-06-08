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
    public class Collision
    {
        FileManager fileManager;
        List<List<string>> attributes, contents, collisionMap;
        List<string> row;

        public List<List<string>> CollisionMap
        {
            get { return collisionMap; }
        }

        public void LoadContent(ContentManager content, string mapID)
        {
            fileManager = new FileManager();
            attributes = new List<List<string>>();
            contents = new List<List<string>>();
            collisionMap = new List<List<string>>();
            row = new List<string>();

            fileManager.LoadContent("Load/Maps/" + mapID + ".jxz", "Collision");

            for (int i = 0; i < fileManager.Contents.Count; i++)
            {
                for (int j = 0; j < fileManager.Contents[i].Count; j++)
                {
                    row.Add(contents[i][j]);
                }
                collisionMap.Add(row);
                row = new List<string>();
            }
        }

        public void Update(GameTime gameTime, ref Vector2 playerPosition, Vector2 pDimensions, Vector2 tileDimensions)
        {
            
        }
    }
}