using System.Collections.Generic;
using Raylib_cs;
using static Raylib_cs.Raylib;
using System.Numerics;

namespace RGCore
{
    class GameObjectList
    {
        /// <summary>
        /// Will serve as the Hirerachy 
        /// </summary>
        public static List<GameObject> objects = new List<GameObject>
        {
            
        };

        public static List<List<GameObject>> scenes = new List<List<GameObject>>
        {
            new List<GameObject>
            {
                new CollisionTestRectangle("Platform", new Vector2(800,850),new Vector2(1600,40)),
                new CollisionTestRectangle("Platform", new Vector2(1400,650),new Vector2(400,40)),
                new CollisionTestRectangle("Platform", new Vector2(200,700),new Vector2(400,40)),
                new CollisionTestRectangle("Platform", new Vector2(800,550),new Vector2(400,40)),
                new CollisionTestRectangle("Platform", new Vector2(200,450),new Vector2(400,40)),
                new CollisionTestRectangle("Platform", new Vector2(200,230),new Vector2(400,40)),
                new CollisionTestRectangle("Platform", new Vector2(800,110),new Vector2(400,40)),
                new CollisionTestRectangle("Platform", new Vector2(800,300),new Vector2(400,40)),
                new CollisionTestRectangle("Platform", new Vector2(1400,300),new Vector2(400,40)),
                new CollisionTestRectangle("Wall", new Vector2(600,150),new Vector2(40,340)),
                new CollisionTestRectangle("Wall", new Vector2(0,450),new Vector2(40,900)),
                new CollisionTestRectangle("Wall", new Vector2(1600,450),new Vector2(40,900)),
                new CollisionTestRectangle("Wall", new Vector2(800,0),new Vector2(1600,40)),
                new CollisionTestRectangle("Wall", new Vector2(800,880),new Vector2(1600,40)),
            },

            new List<GameObject>
            {
                new CollisionTestRectangle("Wall", new Vector2(600,150),new Vector2(40,340)),
                new CollisionTestRectangle("Wall", new Vector2(0,450),new Vector2(40,900)),
                new CollisionTestRectangle("Wall", new Vector2(1600,450),new Vector2(40,900)),
                new CollisionTestRectangle("Wall", new Vector2(800,0),new Vector2(1600,40)),
                new CollisionTestRectangle("Wall", new Vector2(800,880),new Vector2(1600,40)),
            }
        };


        public static void LoadScene(int sceneID) 
        {
            objects = new List<GameObject>();
            for (int i = 0; i < scenes[sceneID].Count; i++) 
            {
                objects.Add(scenes[sceneID][i].DeepClone());
            }
        }


        /// <summary>
        /// All preloaded textures
        /// </summary>
        public static Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();

        /// <summary>
        /// All preloaded sounds
        /// </summary>
        public static Dictionary<string, Sound> sounds = new Dictionary<string, Sound>();

        /// <summary>
        /// GameObjects waiting to be added to main objects list
        /// </summary>
        private static List<GameObject> Queue = new List<GameObject>();

        /// <summary>
        /// GameObjects waiting to be removed from the main objects list
        /// </summary>
        private static List<GameObject> Marked = new List<GameObject>();

        /// <summary>
        /// Loads all textures to be used into the texture list.
        /// </summary>
        public static void LoadTextures() 
        {
            //textures.Add("TestImage", LoadTexture(@"Textures\TestImage.png"));
            
        }
        /// <summary>
        /// Loads all sounds to be used into the sound list.
        /// </summary>
        public static void LoadSounds()
        {
            //sounds.Add("CoinSound", LoadSound(@"Sounds\CoinSound.wav"));
        }
        /// <summary>
        /// Add a GameObject to object list.
        /// </summary>
        public static void NewObject(GameObject gameObject) 
        {  
            Queue.Add(gameObject);
            gameObject.Start();
        }

        /// <summary>
        /// Will check the queue and marked lists for objects that need to be modified in the objects array.
        /// </summary>
        public static void UpdateObjectList() 
        {
            foreach (GameObject g in Queue) 
            {
                objects.Add(g);
            }
            Queue = new List<GameObject>();
            foreach (GameObject g in Marked)
            {
                objects.Remove(g);
            }
            Marked = new List<GameObject>();
        }

        /// <summary>
        /// Mark a GameObject for removal
        /// </summary>
        public static void Destroy(GameObject gameObject) 
        {
            Marked.Add(gameObject);
        }

        /// <summary>
        /// Get the types and names of all active GameObjects in a string format
        /// </summary>
        public static string GetObjectListString() 
        {
            string output = "";

            foreach (GameObject g in objects) 
            {
                output += $"{g.GetType()}:{g.name}\n";
            }

            return output;
        }

    }
}
