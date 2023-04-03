﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace othello0._0._2
{
   public class SceneGame :Sprite
    {
        //背景
        private SpriteBg m_bg = null;
        //駒
        private SpriteKoma[] m_koma = new SpriteKoma[64];
        //手番
        private SpriteKoma.KomaType m_teban = SpriteKoma.KomaType.Black;
        //プレイヤー
        private Player[] m_player = new Player[2];

        public enum Direction
        {
            None,       //なし
            Left,　     //左
            Right,      //右
            Up,         //上
            Down,       //下
            LeftUp,     //左上
            LeftDown,   //左下
            RightUp,    //右上
            RightDown,  //右下

            End,　　　  //終わり
        }
        public SceneGame() : base()
        {
            m_player[0] = new PlayerMe(this);
            m_player[1] = new PlayerMe(this);
            // m_player[1] = new PlayerRandom(this);

            m_bg = new SpriteBg();
            for (int i = 0; i < m_koma.Length; ++i)
                m_koma[i] = new SpriteKoma();
            
            addSprite(m_bg);
            foreach (var sp in m_koma)
                addSprite(sp);
            foreach (var pl in m_player)
                addSprite(pl);

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

            //初期位置にする
            getSpriteKoma(3, 3).Type = SpriteKoma.KomaType.Black;
            getSpriteKoma(4, 3).Type = SpriteKoma.KomaType.White;
            getSpriteKoma(3, 4).Type = SpriteKoma.KomaType.White;
            getSpriteKoma(4, 4).Type = SpriteKoma.KomaType.Black;

            getSpriteKoma(3, 3).Opacity = 1.0f;
            getSpriteKoma(4, 3).Opacity = 1.0f;
            getSpriteKoma(3, 4).Opacity = 1.0f;
            getSpriteKoma(4, 4).Opacity = 1.0f;

            m_teban = SpriteKoma.KomaType.Black;

            //プレイヤーに思考させる
            var ply = getNowPlayer();
            ply.beginThink(GetGouhouTe());

        }
        /// <summary>
        /// ゲームを進める
        /// </summary>
        /// <param name="pos"></param>
        public void goMove(Point pos)
        {
            var sp = getSpriteKoma(pos.X, pos.Y);
            //駒を置く
            sp.Type = Teban;
            sp.Opacity = 1.0f;


            //手番をカエル
            if (Teban == SpriteKoma.KomaType.Black)
                Teban = SpriteKoma.KomaType.White;
            else
                Teban = SpriteKoma.KomaType.Black;

            //選択解除
            for (int row = 0; row < 8; row++)
            {
                for (int line = 0; line < 8; line++)
                {
                    var koma = getSpriteKoma(row, line);
                    koma.IsSelected = false;
                }
            }
            //プレイヤーに思考させる
            var ply = getNowPlayer();
            ply.beginThink(GetGouhouTe());

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

        /// <summary>
        /// 現在の手番取得
        /// </summary>
        public SpriteKoma.KomaType Teban
        {
            get { return m_teban; }
            set { m_teban = value; }
        }

        /// <summary>
        /// 現在のプレイヤー
        /// </summary>
        /// <returns></returns>
        private Player getNowPlayer()
        {
            if (Teban == SpriteKoma.KomaType.Black)
                return m_player[0];
            else
                return m_player[1];
        }
        /// <summary>
        /// 合法手取得
        /// </summary>
        /// <returns></returns>
        public List<Point> GetGouhouTe()
        {
            List<Point> rtn = new List<Point>();

            for (int row = 0; row < 8; ++row)
            {
                for (int line = 0; line < 8; ++line)
                {
                    var sp = getSpriteKoma(row, line);
                    if (sp.Type == SpriteKoma.KomaType.None)
                    {
                        //合法手かどうか
                        bool is_gouhou = false;

                        //全方向シーク
                        for(Direction i = Direction.Left; i<Direction.End;++i)
                        {
                            //シーク場所
                            int seek_row = row;
                            int seek_line = line;

                            //駒のタイプ
                            SpriteKoma.KomaType prev_type = SpriteKoma.KomaType.None;


                            for (;;)
                            {
                                switch(i)
                                {
                                    case Direction.Left: seek_row--; break;
                                    case Direction.Right: seek_row++; break;
                                    case Direction.Up: seek_line--; break;
                                    case Direction.Down: seek_line++; break;
                                    case Direction.LeftUp: seek_row--; seek_line--; break;
                                    case Direction.LeftDown: seek_row--; seek_line++; break;
                                    case Direction.RightUp: seek_row++; seek_line--; break;
                                    case Direction.RightDown: seek_row++; seek_line++; break;
                                }

                                //隣接が相手の駒かどうか
                                var seek_sp = getSpriteKoma(seek_row, seek_line);

                                if (prev_type == SpriteKoma.KomaType.None)
                                {

                                    //範囲外
                                    if (seek_sp == null)
                                        break;
                                    //駒がない
                                    if (seek_sp.Type == SpriteKoma.KomaType.None)
                                        break;
                                    //自分の駒
                                    if (seek_sp.Type == Teban)
                                        break;

                                    prev_type = seek_sp.Type;
                                }
                                else
                                {
                                    //範囲外
                                    if (seek_sp == null)
                                        break;
                                    //駒がない
                                    if (seek_sp.Type == SpriteKoma.KomaType.None)
                                        break;
                                    //自分の駒
                                    if (seek_sp.Type == Teban)
                                    {
                                        //合法手
                                        is_gouhou = true;
                                        break;
                                    }
                                }
                            }//for(;;)

                            if (is_gouhou)
                                break;
                        }

                        //合法手
                        if(is_gouhou)
                           rtn.Add(new Point(row, line));
                    }
                }
            }
            
            return rtn;

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
