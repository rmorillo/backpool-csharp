using System;
using System.Collections.Generic;
using System.Text;

namespace BackPool
{
    public interface IScalarWriter<T>
    {
        void Write(T value);
    }
}
