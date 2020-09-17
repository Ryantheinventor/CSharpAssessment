using System.Collections.Generic;
using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using System.Numerics;
using MiniAtariArcade.Pong;
using MiniAtariArcade.Breakout;
using MiniAtariArcade;
using MiniAtariArcade.Asteroids;

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

        /// <summary>
        /// Contains all the scenes in default state
        /// </summary>
        public static List<List<GameObject>> scenes = new List<List<GameObject>>
        {
            new List<GameObject>//Main Menu 0
            {
                new VolumeSlider("Volume", new Vector2(50,700)),
                new Text("Title",new Vector2(250,100),"Mini Atari\nArcade",80,WHITE),
                new LoadSceneButton("StartPong",new Vector2(1100,225),"PongButton",2),
                new SceneButtonScore("StartBreakout",new Vector2(500,650),"BreakoutButton",3,"Breakout"),
                new SceneButtonScore("StartAsteroids",new Vector2(1100,650),"AsteroidsButton",4,"Asteroids"),
            },
            new List<GameObject>//Game Over 1
            {
                new Text("Instructions",new Vector2(600,100),"Type a name.",60,WHITE),
                new EnterName("EnterName",new Vector2(680,300)),
            },

            new List<GameObject>//Pong 2
            {

                new BackBoard("WallLeft", new Vector2(0,450),new Vector2(40,900),1),
                new BackBoard("WallRight", new Vector2(1600,450),new Vector2(40,900),2),
                new CollisionTestRectangle("Wall", new Vector2(800,0),new Vector2(1600,40)),
                new CollisionTestRectangle("Wall", new Vector2(800,900),new Vector2(1600,40)),
                new PongBall("Ball",new Vector2(800,450)),
                new PongPaddle("Player1",new Vector2(100,450),1),
                new PongPaddle("Player2",new Vector2(1500,450),2),
                new ScoreCounter("Score2",new Vector2(30,30),1),
                new ScoreCounter("Score1",new Vector2(1510,30),2),
                new LoadSceneButton("ExitButton",new Vector2(800,75),"Exit",0),

            },
            new List<GameObject>//Breakout 3
            {

                new CollisionTestRectangle("Wall", new Vector2(500,450),new Vector2(40,900)),
                new CollisionTestRectangle("Wall", new Vector2(1100,450),new Vector2(40,900)),
                new CollisionTestRectangle("Wall", new Vector2(800,0),new Vector2(600,40)),
                new CollisionTestRectangle("Floor", new Vector2(800,900),new Vector2(600,40)),
                new BreakoutPaddle("Player",new Vector2(800,850)),
                new BreakoutBall("Ball",new Vector2(800,850)),
                new BreakoutScore("Score",new Vector2(20,30)),
                //new LoadSceneButton("ExitButton",new Vector2(800,75),"Exit",0),
            },
            new List<GameObject>//Asteroids 4
            {
                new AsteroidsShip("Player",new Vector2(800,450),"Ship"),
                new AsteroidSpawner("Spawner",new Vector2(0,0)),
                new AsteroidsScore("Score",new Vector2(1000,20)),
            },



        };

        /// <summary>
        /// Loads scene and calls all Start() funtions
        /// </summary>
        /// <param name="sceneID">the index of the scene in the scenes list</param>
        public static void LoadScene(int sceneID) 
        {
            objects = new List<GameObject>();
            for (int i = 0; i < scenes[sceneID].Count; i++) 
            {
                objects.Add(scenes[sceneID][i].DeepClone());
            }
            Queue = new List<GameObject>();
            Marked = new List<GameObject>();
            Core_basic_window.Start();
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
            textures.Add("Exit", LoadTexture(@"Textures\Exit.png"));
            textures.Add("PongButton", LoadTexture(@"Textures\PongButton.png"));
            textures.Add("Ship", LoadTexture(@"Textures\Ship.png"));
            textures.Add("AsteroidSml0", LoadTexture(@"Textures\AsteroidSml0.png"));
            textures.Add("AsteroidMed0", LoadTexture(@"Textures\AsteroidMed0.png"));
            textures.Add("AsteroidBig0", LoadTexture(@"Textures\AsteroidBig0.png"));
            textures.Add("BreakoutButton", LoadTexture(@"Textures\BreakoutButton.png"));
            textures.Add("AsteroidsButton", LoadTexture(@"Textures\AsteroidsButton.png"));
            textures.Add("Mute", LoadTexture(@"Textures\Mute.png"));
            textures.Add("Full", LoadTexture(@"Textures\Full.png"));

        }
        /// <summary>
        /// Loads all sounds to be used into the sound list.
        /// </summary>
        public static void LoadSounds()
        {
            sounds.Add("bangMedium", LoadSound(@"Sounds\bangMedium.wav"));
            sounds.Add("fire", LoadSound(@"Sounds\fire.wav"));
            sounds.Add("thrust", LoadSound(@"Sounds\thrust.wav"));
            sounds.Add("pongBounce1", LoadSound(@"Sounds\pongBounce1.wav"));
            sounds.Add("pongBounce2", LoadSound(@"Sounds\pongBounce2.wav"));
            sounds.Add("brickBreak", LoadSound(@"Sounds\brickBreak.wav"));
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

        public static GameObject FindByName(string name) 
        {
            foreach (GameObject g in objects) 
            {
                if (g.name == name) 
                {
                    return g;
                }
            }
            return null;
        }

    }
}
