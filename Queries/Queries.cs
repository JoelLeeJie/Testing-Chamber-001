using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queries
{
    public class GameObject
    {
        public int ID { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double MaxHP { get; set; }
        public double CurrentHP { get; set; }
        public int PlayerID { get; set; }

    }

    public class Ship : GameObject
    {

    }

    public class Player
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string TeamColour { get; set; }
    }

    public class Game
    {
        static void Main()
        {
            Game game = new Game();
            List<GameObject> gameObjectList = new List<GameObject>();
            gameObjectList.Add(new Ship() { ID = 1, X = 5, Y = 1, MaxHP = 1000, CurrentHP = 900, PlayerID = 1 });
            gameObjectList.Add(new Ship() { ID = 2, X = 7, Y = 1, MaxHP = 500, CurrentHP = 1000, PlayerID = 1 });
            gameObjectList.Add(new Ship() { ID = 3, X = 8, Y = 10, MaxHP = 1500, CurrentHP = 20, PlayerID = 2 });

            List<Player> playerList = new List<Player>();
            playerList.Add(new Player() { ID = 1, UserName = "Player 1", TeamColour = "Blue" });
            playerList.Add(new Player() { ID = 2, UserName = "Player 2", TeamColour = "Red" });
            game.GetPercentHealth(gameObjectList, playerList);
        }
     
        List<GameObject> GetGameObjectWithFullHealth(List<GameObject> gameObjectList) //returns collection of full-health gameobjects
        {
            var GameObjectWithFullHealth = from gameObject in gameObjectList
                                           where gameObject.CurrentHP == gameObject.MaxHP
                                           select gameObject;

            return GameObjectWithFullHealth.ToList<GameObject>();
        }
        List<int> GetGameObjectWithFullHealthID(List<GameObject> gameObjectWithFullHealth)
        {
            var GameObjectWithFullHealthID = from gameObject in gameObjectWithFullHealth
                                             select gameObject.ID;
            return GameObjectWithFullHealthID.ToList<int>();
        }

        IEnumerable<IEnumerable<GameObject>> GetPercentHealth(List<GameObject> gameObjectList, List<Player> playerList) //returns gameObject in groups of PlayerID, ordered by percent of hp left.
        {
            IEnumerable<IGrouping<int, GameObject>> PercentHealthQuery = from gameObject in gameObjectList
                                                                         orderby gameObject.CurrentHP / gameObject.MaxHP
                                                                         group gameObject by gameObject.PlayerID into x
                                                                         orderby x.Key
                                                                         select x;
                                    


            foreach (var x in PercentHealthQuery)
            {
                foreach (var y in x)
                {
                    Console.WriteLine(y.CurrentHP/y.MaxHP);
                }
                Console.WriteLine();
                Console.WriteLine(x.Key);
                Console.WriteLine("\n");

            }
            return PercentHealthQuery;

        }
    }
}
