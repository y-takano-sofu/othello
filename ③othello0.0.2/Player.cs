using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace othello0._0._2
{
   public class Player:Sprite
    {
        //オーナークラス
        protected SceneGame m_owner = null;

        public Player(SceneGame owner):base()
        {
            m_owner = owner;
        }
        /// <summary>
        /// 思考開始
        /// </summary>
        /// <param name="gouhou_list"></param>
        public virtual void beginThink(List<Point> gouhou_list)
        {
        }
        public  virtual void resultThink(Point pos)
        {
            m_owner.goMove(pos);
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
