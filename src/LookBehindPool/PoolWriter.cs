using System;
using System.Collections.Generic;
using System.Text;

namespace LookBehindPool
{
    public class PoolWriter<T>
    {
        private LookBehindPool<T> _Pool;
        private object _WriteLock = new object();

        public PoolWriter(LookBehindPool<T> pool)
        {
            _Pool = pool;
        }

        public void Update(Action<T> updateAction)
        {
            lock(_WriteLock)
            {
                _Pool.Update(updateAction);
            }
        }

        public void Assign(T value)
        {
            lock (_WriteLock)
            {
                _Pool.Assign(value);
            }
        }
    }
}
