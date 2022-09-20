using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _006_坦克大战_开始.Properties;

namespace _006_坦克大战_开始
{
    enum Tag
    { 
        MyTank,
        EnemyTank
    }

    internal class Bullet : Movething
    {
        public Tag Tag { get; set; }
        public bool IsDestroy { get; set; }

        public Bullet(int x, int y, int speed,Direction dir,Tag tag)
        {
            this.X = x;
            this.Y = y;
            this.Speed = speed;
            BitmapDown = Resources.BulletDown;
            BitmapUp = Resources.BulletUp;
            BitmapRight = Resources.BulletRight;
            BitmapLeft = Resources.BulletLeft;
            this.Dir = dir;
            this.Tag = tag;
            this.X-= Width/2;
            this.Y-= Height/2;
        }

        public override void Update()
        {
            //移动检查
            MoveCheck();
            //移动
            Move();
            base.Update();
        }

        private void Move()
        {
            switch (Dir)
            {
                case Direction.Up:
                    Y -= Speed;
                    break;
                case Direction.Down:
                    Y += Speed;
                    break;
                case Direction.Right:
                    X += Speed;
                    break;
                case Direction.Left:
                    X -= Speed;
                    break;
            }

        }
        private void MoveCheck()
        {

            #region 检查有没有超过窗体边界
            if (Dir == Direction.Up)
            {
                if (Y + Height / 2 + 3 < 0)
                {
                    IsDestroy = true; return;
                }
            }
            else if (Dir == Direction.Down)
            {
                if (Y + Height / 2 - 3 > 450)
                {
                    IsDestroy = true; return;
                }
            }
            else if (Dir == Direction.Left)
            {
                if (X + Width / 2 - 3 < 0)
                {
                    IsDestroy = true; return;
                }
            }
            else if (Dir == Direction.Right)
            {
                if (X + Width / 2 + 3 > 450)
                {
                    IsDestroy = true; return;
                }
            }
            #endregion


            //检查有没有和其他元素发生碰撞

            Rectangle rect = GetRectangle();
            rect.X = X + Width / 2 - 3;
            rect.Y = Y + Height / 2 - 3;
            rect.Height = 3;
            rect.Width = 3;
            //1.坦克 2.钢铁 .3. 墙 
            int xExplosion = this.X + Width / 2;
            int yExplosion = this.Y + Height / 2;

            NotMovething wall = null;
            if ((wall = GameObjectManage.IsCollidedWall(rect)) != null)
            {
                GameObjectManage.DestroyBullet(this);
                GameObjectManage.DestroyWall(wall);
                GameObjectManage.CreateExplosion(xExplosion,yExplosion);
                SoundManager.PlayBlast();
                return;
            }
            if (GameObjectManage.IsCollidedStell(rect) != null)
            {
                GameObjectManage.DestroyBullet(this);
                return;
            }
            if (GameObjectManage.IsCollidedBoss(rect))
            {
                SoundManager.PlayBlast();
                GameFramework.ChangeToGameOver(); 
                return;
            }
            if (Tag==Tag.MyTank)
            {
                EnemyTank tank = null;
                if((tank=GameObjectManage.IsCollidedEnemyTank(rect))!=null)
                {
                    GameObjectManage.DestroyBullet(this);
                    GameObjectManage.DestroyTank(tank);
                    GameObjectManage.CreateExplosion(xExplosion, yExplosion);
                    SoundManager.PlayHit();
                    return;
                }
            }
            else if(Tag == Tag.EnemyTank)
            {
                MyTank tank = null;
                if((tank=GameObjectManage.IsCollidedMyTank(rect))!=null)
                {
                    GameObjectManage.DestroyBullet(this);
                    GameObjectManage.CreateExplosion(xExplosion,yExplosion);
                    SoundManager.PlayBlast();
                    tank.takeDamge();
                }
            }
        }
        private void Attack()
        {
            //发射子弹
            int x = this.X;
            int y = this.Y;

            switch (Dir)
            {
                case Direction.Up:
                    x = x + Width / 2;
                    break;
                case Direction.Down:
                    x = x + Width / 2;
                    y += Height;
                    break;
                case Direction.Left:
                    y = y + Height / 2;
                    break;
                case Direction.Right:
                    x += Width;
                    y = y + Height / 2;
                    break;
            }


            GameObjectManage.CreateBullet(x, y, Tag.EnemyTank, Dir);
        }

    }
}
