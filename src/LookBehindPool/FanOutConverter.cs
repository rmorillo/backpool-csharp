using System;
using System.Collections.Generic;
using System.Text;

namespace LookBehindPool
{
    public class FanOutConverter<TPoolSource, TItemSource> where TPoolSource: LookBehindPool<TItemSource>
    {
        public FanOutConverter(TPoolSource sourcePool, params Action<TItemSource>[] targetActionItems)
        {
            foreach (var actionItem in targetActionItems)
            {
                sourcePool.Subscribe(actionItem);
            }
        }
    }
}
