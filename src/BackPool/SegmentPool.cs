using System;
using System.Collections.Generic;
using System.Text;

namespace BackPool
{
    public class SegmentPool<T>: BackPool<T>, ISegmentWriter<T>
    {
        public SegmentPool(int segmentSize, int capacity, T initValue) : base(capacity, () => initValue)
        {

        }

        public void Write(params T[] values)
        {
            _Items[_CurrentPosition] = values[0];
            MoveForward();
        }
    }
}
