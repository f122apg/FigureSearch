using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Lucene.Net;
using Lucene.Net.Search.Spell;

namespace FigureSearch.WebScraping
{
    class Lucene_test
    {
        LevensteinDistance mLevensteinDistance;

        public Lucene_test()
        {
            mLevensteinDistance = new LevensteinDistance();
        }

        public string MostSimilarString(string Target, string[] Source)
        {
            int RetStringNum = 0;
            float MaxDistance = 0F;
            float[] Distances = new float[Source.Length];

            for(int i = 0; i < Source.Length; i ++)
            {
                float NowDistance = mLevensteinDistance.GetDistance(Target, Source[i]);
                if (NowDistance >= MaxDistance)
                {
                    MaxDistance = NowDistance;
                    RetStringNum = i;
                }
            }

            return Source[RetStringNum];
        }
    }
}