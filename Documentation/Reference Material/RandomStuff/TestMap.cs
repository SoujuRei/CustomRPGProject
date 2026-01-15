using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;
namespace CustomProjectRPG.RandomStuff
{
    public class TestMap 
    {
        private int[,] _mapData =
        {
            { 1, 1, 1, 1, 1 },
            { 1, 0, 0, 0, 1 },
            { 1, 0, 1, 0, 1 },
            { 1, 0, 0, 0, 1 },
            { 1, 1, 1, 1, 1 },
        };
        
        private Bitmap _grass = SplashKit.LoadBitmap("Grass", "Assets/Texture/Grass11.png");
        private Bitmap _wall = SplashKit.LoadBitmap("Wall", "Assets/Texture/wall128x128.png");

        public void Draw()
        {
            for (int y = 0; y < _mapData.GetLength(0); y++)
            {
                for (int x = 0; x < _mapData.GetLength(1); x++)
                {
                    Bitmap tile = _mapData[y, x] == 0 ? _grass : _wall;
                    
                }
            }
        }

        public bool IsWalkable(int tileX, int tileY)
        {
            return _mapData[tileY, tileX] == 0;
        }
    }
}
