using System;
using System.Collections.Generic;
using System.Text;

namespace LookBehindPool
{
    public class PoolReader<T>
    {
        private LookBehindPool<T> _Pool;

        public PoolReader(LookBehindPool<T> pool)
        {
            _Pool = pool;
        }

        public IEnumerable<T> KeepUp()
        {
            return null;
        }
    }
}
