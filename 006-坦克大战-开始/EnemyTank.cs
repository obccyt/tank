using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _006_坦克大战_开始
{
    class EnemyTank:Movething
    {
        //自动转向
        public int ChangeDirSpeed { get; set; }
        //转向计数器
        private int changeDirCount = 0;
        //速度
        public int AttackSpeed { get; set; }
        //计数器
        private int attackCount = 0;
        //随机数
        private Random r = new Random();
        public EnemyTank(int x, int y, int speed,Bitmap bmpDown,Bitmap bmpUp,Bitmap bmpRight, Bitmap bmpLeft)
        {
            this.X = x;
            this.Y = y;
            this.Speed = speed;
            BitmapDown = bmpDown;
            BitmapUp = bmpUp;
            BitmapRight = bmpRight;
            BitmapLeft = bmpLeft;
            this.Dir = Direction.Down;
            AttackSpeed = 60;
            ChangeDirSpeed = 70;
        }

        public override void Update()
        {
            //移动检查
            MoveCheck();
            //移动
            Move();
            //攻击检查
            AttackCheck();
            //自动移动
            AutoChangeDirection();

            base.Update();
        }

        //改变朝向
        private void ChangeDirection()
        {
            
            while (true)
            {
                Direction dir = (Direction)r.Next(4);
                if(dir==Dir)
                {
                    continue;
                }
                else
                {
                    Dir = dir;
                    break;
                }
            }
            MoveCheck();
            
        }

        //自动该表朝向
        private void AutoChangeDirection()
        {
            changeDirCount++;
            if (changeDirCount<ChangeDirSpeed) return;
            ChangeDirection();
            changeDirCount = 0;
        }

        //移动
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
            #region 检查有没有超出窗体边界
            if (Dir == Direction.Up)
            {
                if (Y - Speed < 0)
                {
                    ChangeDirection();
                    return;
                }
            }
            else if (Dir == Direction.Down)
            {
                if (Y + Speed + Height > 450)
                {
                    ChangeDirection();
                    return;
                }
            }
            else if (Dir == Direction.Left)
            {
                if (X - Speed < 0)
                {
                    ChangeDirection();
                    return;
                }
            }
            else if (Dir == Direction.Right)
            {
                if (X + Speed + Width > 450)
                {
                    ChangeDirection();
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
                ChangeDirection();
                return;
            }
            if (GameObjectManage.IsCollidedStell(rect) != null)
            {
                ChangeDirection();
                return;
            }
            if (GameObjectManage.IsCollidedBoss(rect))
            {
                ChangeDirection();
                return;
            }
        }
        private void AttackCheck()
        {
            attackCount++;
            if (attackCount < AttackSpeed) return;
            Attack();
            attackCount = 0;
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
