using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace othello0._0._2
{
    /// <summary>
    /// 駒クラス
    /// </summary>
    class SpriteKoma : Sprite
    {

        /// <summary>
        /// 駒タイプ
        /// </summary>
        public enum KomaType
        {
            None　= 0,　//なし
            Black = 1,  //黒
            White = 2,  //白
        }

        private KomaType m_koma_type = KomaType.None;


        //黒
        Image m_black = null;
        //白
        Image m_white = null;



        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SpriteKoma() : base()
        {
            m_black = ImageMng.loadImage("img/black.png");
            m_white = ImageMng.loadImage("img/white.png");

        }

        public KomaType Type
        {
            get { return m_koma_type;}
            set { m_koma_type = value;}
        }

        public override void onMove()
        {
            base.onMove();
        }
        public override void onDraw(Graphics g)
        {
            base.onDraw(g);
            if (Type == KomaType.None)
                return;
            if (Type == KomaType.Black)
                DrawImage(g, m_black);
            if (Type == KomaType.White)
                DrawImage(g, m_white);

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
