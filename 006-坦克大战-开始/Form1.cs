using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _006_坦克大战_开始
{
    public partial class Form1 : Form
    {
        private Thread t;
        private static Graphics windowG;
        private static Bitmap tempBmp;
        public Form1()
        {
            InitializeComponent();

            //画布
            windowG = this.CreateGraphics();
            //GameFramework.g = g;

            //临时图片
            tempBmp = new Bitmap(450,450);

            //画布 画到临时图片
            Graphics bmpg = Graphics.FromImage(tempBmp);
            GameFramework.g = bmpg;

            t = new Thread(new ThreadStart(GameMainThread));
            t.Start();
        }

        //线程
        private static void GameMainThread()
        {
            GameFramework.Start();

            int sleepTime = 1000 / 60;
            while(true)
            {
                GameFramework.g.Clear(Color.Black); //涂黑
                GameFramework.Update();  //60

                windowG.DrawImage(tempBmp,0,0);
                //执行中休息时间 每秒调用60次
                Thread.Sleep(sleepTime);
            }

        }
  
        ///窗体关闭完成以后，把线程也关闭 <summary>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            
            t.Abort();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        //按键按下
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            GameObjectManage.KeyDown(e);
        }
        //按键松开
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            GameObjectManage.KeyUp(e);
        }
    }
}
