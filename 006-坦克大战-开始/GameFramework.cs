using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _006_坦克大战_开始
{
    enum GameState
    { 
        Running,
        GameOver
    }

    internal class GameFramework
    {
        public static Graphics g;
        private static GameState gameState=GameState.Running;
        //开始前调用的方法
        public static void Start()
        {
            SoundManager.InitSound();
            GameObjectManage.Start();
            GameObjectManage.CreateMap();
            GameObjectManage.CreateMyTank();
            SoundManager.PlayStart();
        }

        //游戏时候不断调用的方法
        public static void  Update()
        {
            //GameObjectManage.DrawMAp();
            //GameObjectManage.DrawMyTank();
            //GameObjectManage.Update();
            if(gameState==GameState.Running)
            {
                GameObjectManage.Update();
            }
            else
            {
                GameOverUpdate();
            }
        }
        public  static void GameOverUpdate()
        {
            Bitmap bmp= Properties.Resources.GameOver;
            bmp.MakeTransparent(Color.Black);
            int x = 450 / 2 - Properties.Resources.GameOver.Width / 2;
            int y =450/2- Properties.Resources.GameOver.Height / 2;
                
            g.DrawImage(bmp, x,y);
        }
        public static void ChangeToGameOver()
        {
            gameState=GameState.GameOver;
        }
    }
}
