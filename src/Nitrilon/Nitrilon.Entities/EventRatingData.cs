using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nitrilon.Entities
{
    public class EventRatingData
    {
        #region Fields
        private int badRatingCount;
        private int neutralRatingCount;
        private int goodRatingCount;
        #endregion

        #region Constructors
        public EventRatingData(int badRatingCount, int neutralRatingCount, int goodRatingCount)
        {
            BadRatingCount = badRatingCount;
            NeutralRatingCount = neutralRatingCount;
            GoodRatingCount = goodRatingCount;
        }
        #endregion

        #region Properties
        public int BadRatingCount { get => badRatingCount; set => badRatingCount = value; }
        public int NeutralRatingCount { get => neutralRatingCount; set => neutralRatingCount = value; }
        public int GoodRatingCount { get => goodRatingCount; set => goodRatingCount = value; }
        #endregion
    }
}