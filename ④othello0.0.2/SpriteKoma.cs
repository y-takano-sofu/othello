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
    public class SpriteKoma : Sprite
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

        //駒の状態
        private KomaType m_koma_type = KomaType.None;
        //駒が選択されているかどうか
        private bool m_is_select = false;


        //黒
        Image m_black = null;
        //白
        Image m_white = null;
        //選択
        Image m_select = null;




        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SpriteKoma() : base()
        {
            m_black = ImageMng.loadImage("img/black.png");
            m_white = ImageMng.loadImage("img/white.png");
            m_select = ImageMng.loadImage("img/select.png");
        }

        /// <summary>
        /// 駒タイププロパティ
        /// </summary>
        public KomaType Type
        {
            get { return m_koma_type;}
            set { m_koma_type = value;}
        }

        /// <summary>
        /// 選択されているか
        /// </summary>
        public bool IsSelected
        {
            get { return m_is_select; }
            set { m_is_select = value; }
        }
        /// <summary>
        ///マウスがのっかっているかどうか
        /// </summary>
        /// <param name="mouse_pos"></param>
        /// <returns></returns>
        public bool isHover(Point mouse_pos)
        {
            if (Pos.X <= mouse_pos.X &&
                 mouse_pos.X < Pos.X + m_black.Width &&
                Pos.Y <= mouse_pos.Y &&
                mouse_pos.Y < Pos.Y + m_black.Height)
            {
                return true;
            }
            return false;
        }


        public override void onMove()
        {
            base.onMove();
        }
        public override void onDraw(Graphics g)
        {
            base.onDraw(g);

            if (Type == KomaType.Black)
                DrawImage(g, m_black);
            if (Type == KomaType.White)
                DrawImage(g, m_white);

            if (IsSelected)
                DrawImage(g, m_select , 0.5f);
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
