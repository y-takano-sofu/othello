using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace othello0._0._2
{
    class SpriteBg :Sprite
    {
      
        //背景画像
        private Image m_bg = null;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SpriteBg() : base()
        {
            m_bg = ImageMng.loadImage("img/borad.png");
            Pos = new Point(0, 0);
        }

        public override void onMove()
        {
            base.onMove();
        }

        public override void onDraw(Graphics g)
        {
            base.onDraw(g);
            DrawImage(g, m_bg);
        }

        public override bool clickMouse(Point pos)
        {
            if (base.clickMouse(pos))
                return true;
            return false;

        }

        public override bool moveMouse(Point pos)
        {
            if (base.moveMouse(pos))
                return true;
            return false;
        }
    }
}
