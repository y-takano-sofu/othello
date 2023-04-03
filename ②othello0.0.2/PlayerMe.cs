using System.Collections.Generic;
using System.Drawing;

namespace othello0._0._2
{
    /// <summary>
    /// マウス操作によるプレイヤー
    /// </summary>
    class PlayerMe : Player
    {

        private List<Point> m_gouhou_list = null;


        public PlayerMe(SceneGame owner) : base(owner)
        {
        }

        /// <summary>
        /// 思考開始
        /// </summary>
        /// <param name="gouhou_list"></param>
        public override void beginThink(List<Point> gouhou_list)
        {
            base.beginThink(gouhou_list);
            m_gouhou_list = gouhou_list;
        }
        /// <summary>
        /// 指し手送信
        /// </summary>
        /// <param name="pos"></param>
        public override void resultThink(Point pos)
        {
            base.resultThink(pos);
            m_gouhou_list = null;
        }

        public override void onMove()
        {
            base.onMove();

            if (m_gouhou_list == null)
                return;
        }

        public override void onDraw(Graphics g)
        {
            base.onDraw(g);

            if (m_gouhou_list == null)
                return;

        }
        public override bool moveMouse(Point pos)
        {
            if (base.moveMouse(pos))
                return true;

            if (m_gouhou_list == null)
                return false;

            //選択解除
            for (int row = 0; row < 8; row++)
            {
                for (int line = 0; line < 8; line++)
                {
                    var sp = m_owner.getSpriteKoma(row, line);
                    sp.IsSelected = false;
                }
            }

            //マウスが乗っかっているか
            foreach (var mouse_pos in m_gouhou_list)
            {
                var sp = m_owner.getSpriteKoma(
                    mouse_pos.X,
                    mouse_pos.Y);
                if (sp.isHover(pos))
                {
                    sp.IsSelected = true;
                    return true;
                }
            }
            return false;
        }

        public override bool clickMouse(Point pos)
        {
            if (base.clickMouse(pos))
                return true;

            if (m_gouhou_list == null)
                return false;

            //マウスが乗っかっているか
            foreach (var mouse_pos in m_gouhou_list)
            {
                var sp = m_owner.getSpriteKoma(
                    mouse_pos.X,
                    mouse_pos.Y);
                if (sp.isHover(pos))
                {
                    //駒を置く
                    resultThink(mouse_pos);
                    return true;
                }
            }

            return false;
        }
    }
}
