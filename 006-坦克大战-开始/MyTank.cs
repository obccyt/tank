using _006_坦克大战_开始.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _006_坦克大战_开始
{
    internal class MyTank:Movething
    {
        public int HP { get; set; }
        private int originalX;
        private int originalY;
        public bool IsMoving { get; set; }

        public MyTank(int x,int y,int speed)
        {
            IsMoving = false ;
            this.X = x;
            this.Y = y;
            originalX = x;
            originalY = y;
            this.Speed = speed;
            HP = 4;
            BitmapDown = Resources.MyTankDown;
            BitmapUp = Resources.MyTankUp;
            BitmapRight = Resources.MyTankRight;
            BitmapLeft = Resources.MyTankLeft;
            this.Dir = Direction.Up;
        }

        public override void Update()
        {
            //移动检查
            MoveCheck();
            //移动
            Move();
            base.Update();
        }

        //移动
        private void Move()
        {
            if(IsMoving==false)
            {
                return;
            }
            switch (Dir)
            {
                case Direction.Up:
                    Y -= Speed;
                    break;
                case Direction.Down:
                    Y += Speed;
                    break;
                case Direction.Right:
                    X+= Speed;
                    break;
                case Direction.Left:
                   X -= Speed;
                    break;
            }
        
    }

        //移动检查
        private void MoveCheck()
        {
            #region 检查有没有超出窗体边界
            if (Dir==Direction.Up)
            {
                if(Y-Speed<0)
                {
                    IsMoving = false;
                    return;
                }
            }
            else if (Dir == Direction.Down)
            {
                if (Y + Speed +Height>450)
                {
                    IsMoving = false;
                    return;
                }
            }
            else if(Dir == Direction.Left)
            {
                if (X - Speed < 0)
                {
                    IsMoving = false;
                    return;
                }
            }
            else if (Dir == Direction.Right)
            {
                if (X+ Speed +Width>450)
                {
                    IsMoving = false;
                    return;
                }
            }
            #endregion

            //有没有和其他元素发生碰撞
            Rectangle rect = GetRectangle();
            switch (Dir)
            {
                case Direction.Up:
                    rect.Y -= Speed;
                    break;
                case Direction.Down:
                    rect.Y += Speed;
                    break;
                case Direction.Left:
                    rect.X -= Speed;
                    break;
                case Direction.Right:
                    rect.X += Speed;
                    break;
            }
            if (GameObjectManage.IsCollidedWall(rect) != null)
            {
                IsMoving = false; return;
            }
            if (GameObjectManage.IsCollidedStell(rect) != null)
            {
                IsMoving = false; return;
            }
            if (GameObjectManage.IsCollidedBoss(rect))
            {
                IsMoving = false; return;
            }
        }

        public void KeyDown(KeyEventArgs args)
        {
            //控制方向
            //判断冲突方法 1 
            //case Keys.W:
            //        if(Dir!= Direction.Up)
            //         {
            //         Dir= Direction.Up;
            //         }  
            //IsMoving = true;
            //break;

            //方法2 加锁
            switch (args.KeyCode)
            {
                case Keys.W:
                    Dir= Direction.Up;
                    IsMoving = true ;
                    break;
                case Keys.S:
                    Dir = Direction.Down;
                    IsMoving= true ;
                    break;
                case Keys.A:
                    Dir = Direction.Left;
                    IsMoving = true;
                    break;
                case Keys.D:
                    Dir = Direction.Right;
                    IsMoving = true;
                    break;
                case Keys.Space:
                    //发射子弹
                    Attack();
                    break;
            }

        }
        private void Attack()
        {
            SoundManager.PlayFire();
            int x = this.X;
            int y = this.Y;
            switch (Dir)
            { 
                case Direction.Up:
                    x =x+ Width/2;
                    break;
                case Direction.Down:
                    x =x+Width/2;
                    y += Height;
                    break;
                case Direction.Left: 
                    y = y+Width/2;
                    break ;
                case Direction.Right:
                    x += Width;
                    y=y+Height/2;
                    break;
            }

            GameObjectManage.CreateBullet(x,y,Tag.MyTank,Dir);
        }

        //被攻击
        public void takeDamge()
        {
            HP--;
            if(HP<=0)
            {
                //这样写死了直接重生
                //GameObjectManage.CreateMyTank();
                //方法2
                X = originalX;
                Y = originalY;
                HP = 4;
            }
        }
        public void KeyUp(KeyEventArgs args)
        {
            switch (args.KeyCode)
            {
                case Keys.W:
                    Dir = Direction.Up;
                    IsMoving = false;
                    break;
                case Keys.S:
                    Dir = Direction.Down;
                    IsMoving = false;
                    break;
                case Keys.A:
                    Dir = Direction.Left;
                    IsMoving = false;
                    break;
                case Keys.D:
                    Dir = Direction.Right;
                    IsMoving = false;
                    break;
            }
        }
    }
}
