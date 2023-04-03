using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace othello0._0._2
{
    public partial class Form1 : Form
    {
        //ゲームシーン
        private SceneGame m_game = null;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Form1()
        {
            InitializeComponent();

            //画面サイズ
            this.ClientSize = new Size(725, 725);
            this.DoubleBuffered = true;
        }
        /// <summary>
        /// フォームが読み子ℳわれたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void onLoad(object sender, EventArgs e)
        {
            m_game = new SceneGame();
            m_game.clearGame();//関数の中で関数を呼び出している。メソッド
            //タイマを動かす
            tm_update.Start();
        }
        /// <summary>
        /// 再描画
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void onPaint(object sender, PaintEventArgs e)
        {
            //描画処理
            m_game.onDraw(e.Graphics);
        }
        /// <summary>
        /// タイマハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tm_update_Tick(object sender, EventArgs e)
        {
            //変数処理
            m_game.onMove();

            //再描画
            Invalidate(false);

        }
        /// <summary>
        /// マウス押上
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void onMouseUp(object sender, MouseEventArgs e)
        {
            m_game.clickMouse(new Point(e.X, e.Y));

        }
        /// <summary>
        /// マウス移動
        /// </summary>
        private void onMouseMove(object sender, MouseEventArgs e)
        {
            m_game.moveMouse(new Point(e.X, e.Y));

        }
    }
}
