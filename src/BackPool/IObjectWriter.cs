using System;
using System.Collections.Generic;
using System.Text;

namespace BackPool
{
    public interface IObjectWriter<T>
    {
        void Write(Action<T> updateAction);        
    }
}
