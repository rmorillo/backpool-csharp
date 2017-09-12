using System;
using System.Collections.Generic;
using System.Text;

namespace BackPool
{
    public class PoolTrailercs<T>
    {
        private BackPool<T> _Pool;

        public PoolTrailercs(BackPool<T> pool)
        {
            _Pool = pool;
        }

        public IEnumerable<T> KeepUp()
        {
            return null;
        }
    }
}
