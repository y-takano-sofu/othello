using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace othello0._0._2
{
    public class Sprite
    {
        //スプライトリスト
        List<Sprite> m_sprite_list = new List<Sprite>();
        //表示位置
        private Point m_pos = new Point();
        //透明度
        private float m_opacity = 0.0f;

        public void DrawImage(Graphics g, Image im)
        {
            ColorMatrix cm = new ColorMatrix();
            cm.Matrix00 = cm.Matrix11 = cm.Matrix22 = 1;
            cm.Matrix33 = Opacity;
            cm.Matrix44 = 1;
            ImageAttributes ia = new ImageAttributes();
            ia.SetColorMatrix(cm);
            Rectangle rc = new Rectangle();
            rc.X = Pos.X;
            rc.Y = Pos.Y;
            rc.Width = im.Size.Width;
            rc.Height = im.Size.Height;

            g.DrawImage(im, rc, 0, 0, im.Width, im.Height, GraphicsUnit.Pixel, ia);

        }


        /// <summary>
        /// 表示位置プロパティ
        /// </summary>
        public Point Pos
        {
            get { return m_pos; }
            set { m_pos = value; }
        }

        /// <summary>
        /// 透明度プロパティ
        /// </summary>
        public float Opacity
        {
            get { return m_opacity; }
            set { m_opacity = value; }
        }

        /// <summary>
        /// スプライト追加
        /// </summary>
        /// <param name=""></param>
        public void addSprite(Sprite　sp)
        {
            m_sprite_list.Add(sp);
        }
        /// <summary>
        /// スプライト削除
        /// </summary>
        public void delSprite(Sprite sp)
        {
            m_sprite_list.Remove(sp);
        }

        /// <summary>
        /// スプライト全削除
        /// </summary>
        public void delAllSprite()
        {
            m_sprite_list.Clear();
        }

        //変数処理  
        public virtual void onMove()
        {
            foreach (var sp in m_sprite_list)
                sp.onMove();
        }

        //描画処理
        public virtual void onDraw(Graphics g)
        {
            foreach (var sp in m_sprite_list)
                sp.onDraw(g);
        }

        //マウスクリック処理
        public virtual bool clickMouse(Point pos)
        {
            int cnt = m_sprite_list.Count;
            for (int i = cnt - 1; i >= 0; --i)
                if (m_sprite_list[i].clickMouse(pos))
                    return true;
            return false;
        }

        /// <summary>
        /// マウス位置処理
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public virtual bool moveMouse(Point pos)
        {
            int cnt = m_sprite_list.Count;
            for (int i = cnt - 1; i >= 0; --i)
                if (m_sprite_list[i].moveMouse(pos))
                    return true;
            return false;
        }
    }
}
