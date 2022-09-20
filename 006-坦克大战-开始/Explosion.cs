using _006_坦克大战_开始.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _006_坦克大战_开始
{
    class Explosion : GameObject
    {
        //中止标志
        public bool IsNeedDestroy { get; set; }
        //爆炸速度
        private int playSpeed = 1;
        //计数器
        private int playCount = 0;
        //索引
        private int index = 0;
        //爆炸资源
        private Bitmap[] bmpArray = new Bitmap[]
        {
            Resources.EXP1,
            Resources.EXP2,
            Resources.EXP3,
            Resources.EXP4,
            Resources.EXP5
        };

        public Explosion(int x,int y)
        {
            //透明化
            foreach (Bitmap bmp in bmpArray)
            {
                bmp.MakeTransparent(Color.Black);
            }
            this.X = x - bmpArray[0].Width / 2;
            this.Y = y - bmpArray[0].Height / 2;
            IsNeedDestroy = false;
        }

        protected override Image GetImage()
        {
            if (index>4)
            {
                return bmpArray[4];
            }
            return bmpArray[index];
        }

        public override void Update()
        {
            //base.DrawSelf();
            playCount++;
            index = (playCount - 1) / playSpeed;
            if(index>4)
            {
                IsNeedDestroy = true;
            }
            base.Update();
        }
    }
}
