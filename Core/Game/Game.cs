using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;
using RPGGame.Entities;
using RPGGame.Maps;

namespace CustomProjectRPG.Core.Game
{
    public class Game
    {
        private Window _window;
        private Player _player;
        private IMap1 _currentMap;

        public Game()
        {
            _window = new Window("My RPG", 800, 600);
            _player = new Player();
            _currentMap = new TestMap(); // Can swap to ForestMap, etc.
        }

        public void Run()
        {
            while (!_window.CloseRequested)
            {
                SplashKit.ProcessEvents();
                SplashKit.ClearScreen(Color.Black);

                _currentMap.Draw();
               

                SplashKit.RefreshScreen(60);
            }
        }
    }
}
