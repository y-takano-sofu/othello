using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace othello0._0._2
{
    class SceneGame :Sprite
    {
        //背景
        private SpriteBg m_bg = null;
        //駒
        private SpriteKoma[] m_koma = new SpriteKoma[64];
        //手ばん
        private SpriteKoma.KomaType m_teban = SpriteKoma.KomaType.Black;


        public SceneGame() : base()
        {
            m_bg = new SpriteBg();
            for (int i = 0; i < m_koma.Length; ++i)
                m_koma[i] = new SpriteKoma();
            
            addSprite(m_bg);
            foreach (var sp in m_koma)
                addSprite(sp);

            for (int row = 0; row < 8; ++row)
            {
                for (int line = 0; line < 8; ++line)
                {
                    var sp = getSpriteKoma(row, line);
                    sp.Pos = new Point(row * 90, line * 90);
                }
            }
        }

        public void clearGame()
        {
            foreach (var sp in m_koma)
                sp.Opacity = 0.0f;

            m_bg.Opacity = 1.0f;


            getSpriteKoma(3, 3).Type = SpriteKoma.KomaType.Black;
            getSpriteKoma(4, 3).Type = SpriteKoma.KomaType.White;
            getSpriteKoma(3, 4).Type = SpriteKoma.KomaType.White;
            getSpriteKoma(4, 4).Type = SpriteKoma.KomaType.Black;

            getSpriteKoma(3, 3).Opacity = 1.0f;
            getSpriteKoma(4, 3).Opacity = 1.0f;
            getSpriteKoma(3, 4).Opacity = 1.0f;
            getSpriteKoma(4, 4).Opacity = 1.0f;

            m_teban = SpriteKoma.KomaType.Black;

        }

        /// <summary>
        /// 駒スプライト取得
        /// </summary>
        /// <param name="row"></param>
        /// <param name="line"></param>
        /// <returns></returns>
        public SpriteKoma getSpriteKoma(int row, int line)
        {
            if (line < 0 || line >= 8 || row < 0 || row >= 8)
                return null;
            return m_koma[line * 8 + row];
        }



        public override void onMove()
        {
            base.onMove();
        }

        public override void onDraw(Graphics g)
        {
            base.onDraw(g);
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
