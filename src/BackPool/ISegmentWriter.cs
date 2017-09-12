using System;
using System.Collections.Generic;
using System.Text;

namespace BackPool
{
    public interface ISegmentWriter<T>
    {
        void Write(params T[] values);
    }
}
