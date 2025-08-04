using System;
using SplashKitSDK;


namespace CustomProjectRPG.Core.Game
{

    public class Program
    {
        private static void Main(string[] args)
        {
           
            Game game = new Game();
            //var playerStats = new StatsComponent(
            //health: 100,
            //maxHealth: 100,
            //strength: 10,
            //defense: 5,
            //agility: 7
            //);
            //var player = new PlayerEntity("Carter", 0f, 0f, "High Knight", playerStats);

            game.Run();
        }
    }
}
