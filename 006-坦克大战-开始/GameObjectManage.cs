using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using _006_坦克大战_开始.Properties;

namespace _006_坦克大战_开始
{
    //控制所有物体的产生
    internal class GameObjectManage
    {
        //木头墙
        private static List<NotMovething> wallList = new List<NotMovething>();
        //石头墙
        private static List<NotMovething> steelList = new List<NotMovething>();
        //子弹
        private static List<Bullet> bulletList= new List<Bullet>();
        //boss
        private static NotMovething boss;
        //我的坦克
        private static MyTank myTank;
        //敌方坦克集合
        private static List<EnemyTank> tankList = new List<EnemyTank>();
        //爆炸
        private static List<Explosion> expList = new List<Explosion>();
        //生成速度
        private static int enemyBornSpeed = 60;
        //计数器
        private static int enemyTankCount = 60;
        //三个出生点
        public static Point[] points=new Point[3];
        //敌机出生点
        public static void Start()
        {
            points[0].X = 0;
            points[0].Y = 0;
            points[1].X = 7*30;
            points[1].Y = 0;
            points[2].X = 14*30;
            points[2].Y = 0;
            
        }

        public static void Update()
        {
            foreach (NotMovething nm in wallList)
            {
                nm.Update();
            }
            foreach (NotMovething nm in steelList)
            {
                nm.Update();
            }
            foreach(EnemyTank tank in tankList)
            {
                tank.Update();
            }
            foreach (Explosion exp in expList)
            {
                exp.Update();
            }
            //foreach (Bullet bullet in bulletList)
            //{
            //    bullet.Update();

            //}
            for (int i=0;i< bulletList.Count;i++)
            {
                bulletList[i].Update();
            }
            CheckAndDestroyExplosion();
            boss.Update();
            myTank.Update();
            EnemyBorn();
        }
        //生成子弹
        public static void CreateBullet(int x,int y,Tag tag,Direction dir)
        {
            Bullet bullet = new Bullet(x,y,5,dir,tag);
            bulletList.Add(bullet);
        }

        //移除子弹
        public static void DestroyBullet(Bullet bullet)
        {
            bulletList.Remove(bullet);
        }

        //爆炸
        public static void CreateExplosion(int x,int y)
        {
            Explosion exp =new Explosion(x,y);
            expList.Add(exp);
        }
        //敌人生成
        private static void EnemyBorn()
        {
            enemyTankCount++;
            if (enemyTankCount < enemyBornSpeed) return;
            SoundManager.PlayAdd();
            //随机 生成
            Random rd = new Random();
            int index = rd.Next(0,3);
            Point position = points[index];
            int enemyType = rd.Next(1,5);
            switch (enemyType)
            {
                case 1:
                    CreateEnemyTank1(position.X,position.Y);
                    break;
                case 2:
                    CreateEnemyTank2(position.X, position.Y);
                    break;
                case 3:
                    CreateEnemyTank3(position.X, position.Y);
                    break;
                case 4:
                    CreateEnemyTank4(position.X, position.Y);
                    break;
            }


            enemyTankCount = 0;
        }
        //移除敌人
        public static  void DestroyTank(EnemyTank tank)
        {
            tankList.Remove(tank);
        }
        private static void CheckAndDestroyExplosion()
        {
            List<Explosion> needToDestroy = new List<Explosion>();
            foreach (Explosion exp in expList)
            {
                if(exp.IsNeedDestroy==true)
                {
                    needToDestroy.Add(exp);
                }
            }
            foreach (Explosion exp in needToDestroy)
            {
                expList.Remove(exp);
            }
        }
        #region 坦克部队
        private static void CreateEnemyTank1(int x,int y)
        {
            EnemyTank tank = new EnemyTank(x,y,2,Resources.GrayDown, Resources.GrayUp, Resources.GrayRight, Resources.GrayLeft);
            tankList.Add(tank);
        }
        private static void CreateEnemyTank2(int x, int y)
        {
            EnemyTank tank = new EnemyTank(x, y, 2, Resources.GreenDown, Resources.GreenUp, Resources.GreenRight, Resources.GreenLeft);
            tankList.Add(tank);
        }
        private static void CreateEnemyTank3(int x, int y)
        {
            EnemyTank tank = new EnemyTank(x, y, 2, Resources.QuickDown, Resources.QuickUp, Resources.QuickRight, Resources.QuickLeft);
            tankList.Add(tank);
        }
        private static void CreateEnemyTank4(int x, int y)
        {
            EnemyTank tank = new EnemyTank(x, y, 2, Resources.SlowDown, Resources.SlowUp, Resources.SlowRight, Resources.SlowLeft);
            tankList.Add(tank);
        }
        #endregion
        //创建地图
        public static void CreateMap()
        {
            CreateWall(1,1,5, Resources.wall, wallList);
            CreateWall(3, 1, 5, Resources.wall, wallList);
            CreateWall(5, 1, 4, Resources.wall, wallList);
            CreateWall(7, 1, 3, Resources.wall, wallList);
            CreateWall(9, 1, 4, Resources.wall, wallList);
            CreateWall(11, 1, 5, Resources.wall, wallList);
            CreateWall(13, 1, 5, Resources.wall, wallList);

            CreateWall(7, 5, 1, Resources.steel, steelList);

            CreateWall(2, 7, 1, Resources.wall, wallList);
            CreateWall(3, 7, 1, Resources.wall, wallList);
            CreateWall(4, 7, 1, Resources.wall, wallList);

            CreateWall(0, 7, 1, Resources.steel, steelList);
            CreateWall(14, 7, 1, Resources.steel, steelList);

            CreateWall(6, 7, 1, Resources.wall, wallList);
            CreateWall(7, 6, 2, Resources.wall, wallList);
            CreateWall(8, 7, 1, Resources.wall, wallList);

            CreateWall(10, 7, 1, Resources.wall, wallList);
            CreateWall(11, 7, 1, Resources.wall, wallList);
            CreateWall(12, 7, 1, Resources.wall, wallList);

            CreateWall(1, 9, 5, Resources.wall, wallList);
            CreateWall(3, 9, 5, Resources.wall, wallList);
            CreateWall(5, 9, 3, Resources.wall, wallList);

            CreateWall(6, 10, 1, Resources.wall, wallList);
            CreateWall(7, 10, 2, Resources.wall, wallList);
            CreateWall(8, 10, 1, Resources.wall, wallList);
            CreateWall(9, 9, 3, Resources.wall, wallList);

            CreateWall(11, 9, 5, Resources.wall, wallList);
            CreateWall(13, 9, 5, Resources.wall, wallList);

            CreateWall(6, 13, 2, Resources.wall, wallList);
            CreateBoss(7, 14, Resources.Boss);
            CreateWall(7, 13, 1, Resources.wall, wallList);
            CreateWall(8, 13, 2, Resources.wall, wallList);
        }

        //创建墙 双面
        private static void CreateWall(int x,int y,int count,Image img, List<NotMovething> wallList)
        {
            int xPosition = x * 30;
            int yPosition = y * 30;
            for(int i = yPosition;i<yPosition+count*30; i+=15)
            {
                //创捷一列的墙 一列是两个
                NotMovething wall1 = new NotMovething(xPosition,i,img); //左边的墙
                NotMovething wall2 = new NotMovething(xPosition+15, i, img);//右边的墙
                wallList.Add(wall1);
                wallList.Add(wall2);
            }
        }

        //销毁墙
        public static void DestroyWall(NotMovething wall)
        {
            wallList.Remove(wall);
        }

        //创建BOSS
        private static void CreateBoss(int x,int y,Image img)
        {
            int xPosition = x * 30;
            int yPosition = y * 30;
            boss = new NotMovething(xPosition, yPosition, img);
        }

        //创建我的坦克
        public static void CreateMyTank()
        {
            int x = 5 * 30;
            int y = 14 * 30;
            myTank = new MyTank(x,y,2);

        }

        public static void KeyDown(KeyEventArgs args)
        {
            myTank.KeyDown(args);
        }
        public static void KeyUp(KeyEventArgs args)
        {
            myTank.KeyUp(args);
        }

        public static NotMovething IsCollidedWall (Rectangle rt)
        {
            foreach(NotMovething wall in wallList)
            {
                if (wall.GetRectangle().IntersectsWith(rt))
                {
                    return wall;
                }
            }
            return null;
        }

        public static NotMovething IsCollidedStell(Rectangle rt)
        {
            foreach (NotMovething steel in steelList)
            {
                if (steel.GetRectangle().IntersectsWith(rt))
                {
                    return steel;
                }
            }
            return null;
        }

        public static bool IsCollidedBoss(Rectangle rt)
        {
            return boss.GetRectangle().IntersectsWith(rt);
        }

        public static MyTank IsCollidedMyTank(Rectangle rt)
        {
            if(myTank.GetRectangle().IntersectsWith(rt)) 
                return myTank;
            else
            {
                return null;
            }
        }
        public static EnemyTank IsCollidedEnemyTank(Rectangle rt)
        {
            foreach (EnemyTank tank in tankList)
            {
                if(tank.GetRectangle().IntersectsWith(rt))
                {
                    return tank;
                }
            }
            return null;
        }

        //单面墙
        //private static void CreateoneWall(int x, int y, int count, Image img, List<NotMovething> wallList)
        //{
        //    int xPosition = x * 15;
        //    int yPosition = y * 15;
        //    for (int i = yPosition; i < yPosition + count * 15; i += 15)
        //    {
        //        //创捷一列的墙 一列是两个
        //        NotMovething wall1 = new NotMovething(xPosition, i, img); //左边的墙
        //        NotMovething wall2 = new NotMovething(xPosition + 15, i, img);//右边的墙
        //        wallList.Add(wall1);
        //        wallList.Add(wall2);
        //    }
        //}


    }
}
