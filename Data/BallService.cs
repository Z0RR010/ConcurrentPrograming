using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class BallService
    {
        private readonly IRepository<T> _ballRepository;

        public BallService(IRepository<T> ballRepository)
        {
            _ballRepository = ballRepository;
        }

        public void addBall()
    }
}
