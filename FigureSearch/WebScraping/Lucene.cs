using Lucene.Net.Search.Spell;
using System.Collections.Generic;
using System.Linq;

namespace FigureSearch.WebScraping
{
    class Lucene
    {
        LevensteinDistance mLevensteinDistance;

        public Lucene()
        {
            mLevensteinDistance = new LevensteinDistance();
        }

        /// <summary>
        /// 指定した文字列に一番近似している文字列を探す
        /// </summary>
        /// <param name="target">比較される文字列。</param>
        /// <param name="source">targetと比較したい文字列の配列。</param>
        /// <returns>Targetにもっとも近似しているSourceの配列の要素番号。</returns>
        public int MostSimilarString(string target, string[] source)
        {
            int retStringNum = 0;
            float maxDistance = 0F;
#if DEBUG
            float[] distances = new float[source.Length];
#endif

            for(int i = 0; i < source.Length; i ++)
            {
#if DEBUG
                //デバッグ用に文字列毎に距離を保存する
                distances[i] = mLevensteinDistance.GetDistance(target, source[i]);
#endif
                float nowDistance = mLevensteinDistance.GetDistance(target, source[i]);
                if (nowDistance >= maxDistance)
                {
                    maxDistance = nowDistance;
                    retStringNum = i;
                }
            }

//#if DEBUG
//            if (false)
//            {
//                for (int k = 0; k < Source.Length - 1; k++)
//                {
//                    for (int j = Source.Length - 1; j > k; j--)
//                    {
//                        if (Distances[j - 1] > Distances[j])
//                        {
//                            float temp = Distances[j - 1];
//                            Distances[j - 1] = Distances[j];
//                            Distances[j] = temp;

//                            string temp_s = Source[j - 1];
//                            Source[j - 1] = Source[j];
//                            Source[j] = temp_s;
//                        }
//                    }
//                }

//                for (int i = 0; i < Source.Length; i++)
//                {
//                    System.Console.WriteLine(Source[i]);
//                    System.Console.WriteLine(Distances[i]);
//                }
//            }
//#endif
            return retStringNum;
        }
    }
}