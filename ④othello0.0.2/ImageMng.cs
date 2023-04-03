using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace othello0._0._2
{
    public class ImageMng
    {
        /// <summary>
        /// イメージハッシュ
        /// Key:画像ファイル名
        /// Value:Imageインスタンス
        /// </summary>
        static private Hashtable m_image_hash = new Hashtable();

        /// <summary>
        /// イメージのロード
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        static public Image loadImage(string filename)
        {
            //イメージがすでにロードされていればそれを返す
            if (m_image_hash.ContainsKey(filename))
                return (Image)m_image_hash[filename];

            //イメージがロードされていなければ、ここでロードする
            m_image_hash[filename] = Image.FromFile(filename);
            return (Image)m_image_hash[filename];
        }
        /// <summary>
        /// イメージ削除
        /// </summary>
        /// <param name="filename"></param>
        static public void delImage(string filename)
        {
            m_image_hash.Remove(filename);
        }
        /// <summary>
        /// イメージ全削除
        /// </summary>
        static public void delAllImage()
        {
            m_image_hash.Clear();
        }
    }
}
