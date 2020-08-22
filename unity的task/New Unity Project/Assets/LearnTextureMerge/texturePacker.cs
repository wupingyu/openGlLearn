using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class texturePacker : MonoBehaviour
{
    private class Atlas
    {
        public int m_Width;
        public Texture2D m_Atlas;

    }

    private int m_padding;
    public void Packages(Texture2D[] textures, int padding)
    {
        m_padding = padding;
        bool finished = false;
        List<Texture2D> notMergedList = new List<Texture2D>();
        notMergedList.AddRange(textures);
        //每次循环都得到新的list图片，合并好的图片，把合并好的图按着要求检查，不符合要求的再处理
        while(!finished)
        {
            
        }
    }

    private void PackageOneTex(Texture2D tex)
    {
        RectInt needRect = new RectInt(0, 0, tex.width + 2 * m_padding, tex.height + 2 * m_padding);
        int needArea = (tex.width + m_padding * 2) * (tex.height + m_padding * 2);
        Vector2Int index = new Vector2Int(-1, -1);
        //找到一个最合适的位置，记录下来；
        //找到最合适的位置 就是面积相差最小

    }
}
