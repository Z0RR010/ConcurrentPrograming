using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public abstract class ModelAbstractApi
    {
        public abstract int TableHeight { get; }
        public abstract int TableWidth { get; }
        public abstract int BallRadius { get; }
        public abstract int BorderWidth { get; }

        public static ModelAbstractApi CreateApi()
        {
            return new ModelApi();
        }
    }
}
