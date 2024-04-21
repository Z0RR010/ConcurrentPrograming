using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;

namespace LogicUnitTest
{
    internal class TestDataApi : DataAbstractApi
    {
        public TestDataApi() { }

        public override ICollection<T> GetRepository<T>()
        {
            return new ObservableCollection<T>();
        }
    }
}
