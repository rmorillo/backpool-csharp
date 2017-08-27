using System;
using System.Collections.Generic;
using System.Text;

namespace LookBehindPool
{
    public class FrozenTrailer<T>
    {
        private LookBehindPool<T> _Pool;

        public FrozenTrailer(LookBehindPool<T> pool)
        {
            _Pool = pool;
        }

        public T Current
        {
            get
            {
                return _Pool.Current;
            }
        }
    }
}
