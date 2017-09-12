using System;
using System.Collections.Generic;
using System.Text;

namespace BackPool
{
    public class ObjectPool<T> : BackPool<T>, IObjectWriter<T>
    {
        public ObjectPool(int capacity, Func<T> initItem) : base(capacity, initItem)
        {
        }

        public void Write(Action<T> updateItem)
        {
            updateItem(_Items[_CurrentPosition]);
            MoveForward();
        }
    }
}
