using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Media;
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
        //エフェクト
        Image m_effect = null;
        //完了までのエフェクト時間（FPS)
        const int m_effect_time = 10;
        //エフェクトカウント
        int m_effect_cnt = 0;
        //完了までのこまめめくりエフェクト時間（FPS）
        const int m_koma_reverse_effect_time = 10;
        //こまめ栗エフェクト
        int m_koma_reverse_effect_cnt = 0;


        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SpriteKoma() : base()
        {
            m_black = ImageMng.loadImage("img/black.png");
            m_white = ImageMng.loadImage("img/white.png");
            m_select = ImageMng.loadImage("img/select.png");
            m_effect = ImageMng.loadImage("img/put.png");
        }

        /// <summary>
        /// 駒タイププロパティ
        /// </summary>
        public KomaType Type
        {
            get { return m_koma_type; }
            set
            {
                //こまめくりエフェクト
                if (m_koma_type != KomaType.None && value != m_koma_type)
                    m_koma_reverse_effect_cnt = 1;
                m_koma_type = value;
            }
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
        /// <summary>
        /// 置いたときのエフェクト
        /// </summary>
        public void putEffect()
        {
            //置いたときのエフェクト
            m_effect_cnt = 1;
            //サウンド再生
            var player = new SoundPlayer("img/komasound.wav");
            player.Play();

        }



        public override void onMove()
        {
            base.onMove();
            //駒を置いたときのエフェクト
            if (m_effect_cnt > 0)
            {
                m_effect_cnt++;
                if (m_effect_cnt >= m_effect_time)
                    m_effect_cnt = 0;
            }
            //こまめくりエフェクト
            if (m_koma_reverse_effect_cnt > 0)
            {
                m_koma_reverse_effect_cnt++;
                if (m_koma_reverse_effect_cnt >= m_koma_reverse_effect_time)
                    m_koma_reverse_effect_cnt = 0;
            }
        }
        public override void onDraw(Graphics g)
        {
            base.onDraw(g);

            if (m_effect_cnt > 0)
            {
                float o = (float)(m_effect_time - m_effect_cnt) / (float)m_effect_time;
                if (o >= 1.0f)
                    o = 1.0f;

                Rectangle rc = new Rectangle();
                rc.X = 0;
                rc.Y = 0;
                rc.Width = (int)(m_black.Width * 4.0f + m_effect_cnt / m_effect_time);
                rc.Height = (int)(m_black.Height * 4.0f * m_effect_cnt / m_effect_time);
                rc.X = Pos.X + (m_black.Width - rc.Width) / 2;
                rc.Y = Pos.Y + (m_black.Height - rc.Height) / 2;

                DrawImage(g, m_effect, rc, o);

            }

            if (m_koma_reverse_effect_cnt == 0)
            {
                if (Type == KomaType.Black)
                    DrawImage(g, m_black);
                if (Type == KomaType.White)
                    DrawImage(g, m_white);
            }
            else
            {
                //こまめくりエフェクト
                float prev_o = (float)(m_koma_reverse_effect_cnt - m_koma_reverse_effect_time) / (float)m_koma_reverse_effect_time;

                float next_o = (float)(m_koma_reverse_effect_cnt) / (float)m_koma_reverse_effect_time;

                //めくるまえのこま
                if (Type == KomaType.Black)
                    DrawImage(g, m_white, prev_o);
                if (Type == KomaType.White)
                    DrawImage(g, m_black, prev_o);

                //めくった後のこま
                if (Type == KomaType.Black)
                    DrawImage(g, m_black, next_o);
                if (Type == KomaType.White)
                    DrawImage(g, m_white, next_o);
            }

            if (IsSelected)
                DrawImage(g, m_select, 0.7f);
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
