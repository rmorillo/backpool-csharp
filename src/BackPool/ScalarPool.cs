using System;
using System.Collections.Generic;
using System.Text;

namespace BackPool
{
    public class ScalarPool<T> : BackPool<T>, IScalarWriter<T>
    {
        public ScalarPool(int capacity, T initValue) : base(capacity, () => initValue)
        {

        }

        public void Write(T value)
        {
            _Items[_CurrentPosition] = value;
            MoveForward();
        }
    }
}
