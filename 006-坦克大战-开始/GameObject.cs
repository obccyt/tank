using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _006_坦克大战_开始
{
    abstract class GameObject
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        protected abstract Image GetImage();

        //绘制地图
        public virtual void DrawSelf()
        {
            Graphics g = GameFramework.g;
            g.DrawImage(GetImage(),X,Y);
        }

        public virtual void Update()
        {
            DrawSelf();
        }

        public Rectangle GetRectangle()
        {
            Rectangle rectangle = new Rectangle(X,Y,Width,Height);
            return rectangle;
        }
    }
}
