﻿using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Numerics;

namespace Data
{
    public class DataApi : DataAbstractApi
    {

        public override ICollection<T> GetRepository<T>()
        {
            return new Repository<T>();
        }

        public override IBallType GetBall(Vector2 Pos, Vector2 Move, Table table)
        {
            return new Ball(Pos, Move, table);
        }

        public override Table GetTable()
        {
            return new Table();
        }
    }
}