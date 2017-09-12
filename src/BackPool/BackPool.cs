using BackPool.Properties;
using System;

namespace BackPool
{
    /// <summary>
    /// Pre-allocated object pool where items are accessed from the last position and is useful for storing historical and time series data.
    /// Works like a circular buffer such that when maximum capacity reached, the last position rolls over to the start of the collection and overwrites the previous data.
    /// </summary>
    public abstract class BackPool<T>: IPoolReader<T>
    {
        #region Member variables

        private long _Sequence = 0;
        private int _Capacity = 0;
        private bool _HasRolledOver = false;

        protected T[] _Items = null;
        protected int _CurrentPosition = 0;
        protected int _LastPosition = -1;
        protected int _Length = 0;
        protected int _Offset = 1;

        #endregion

        #region Constructor	
        /// <summary>
        /// Initializes a new instance of the LookBehind class.
        /// </summary>
        /// <param name='capacity'>
        /// The size of the buffer pool
        /// </param>
        public BackPool(int capacity, Func<T> initItem)
        {
            _Items = new T[capacity];

            //Fills the object pool
            for (int i = 0; i < capacity; i++)
            {
                _Items[i] = initItem();
            }

            _Capacity = capacity;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Moves object pool index to the next position.  Rolls over to index zero when Capacity is reached.
        /// </summary>
        public void MoveForward()
        {
            _LastPosition = _CurrentPosition;
            _Sequence++;

            if (!_HasRolledOver)
            {
                _Length++;
            }

            if (_CurrentPosition < (_Capacity - 1))
            {
                _CurrentPosition += _Offset;
            }
            else
            {
                _CurrentPosition = 0;
                _HasRolledOver = true;
            }            
        }

        public void Subscribe(Action<T> actionItem)
        {

        }
        
        private int GetAbsoluteIndex(int relativeIndex)
        {
            int targetIndex = _LastPosition - relativeIndex;

            if (targetIndex < 0)
            {
                int absoluteIndex = targetIndex + _Capacity;

                if (_HasRolledOver && absoluteIndex > _LastPosition)
                {
                    targetIndex = absoluteIndex;
                }
                else
                    throw new IndexOutOfRangeException(string.Format(Resources.BackPool_GetAbsoluteIndex_PoolIndexOutOfRange, _Length - 1));
            }

            return targetIndex;
        }

        #endregion

        #region Properties
        /// <summary>
        /// Gets the given type at the specified index.
        /// </summary>
        /// <param name='index'>
        /// Zero-based index
        /// </param>
        public T this[int index]
        {
            get
            {
                return _Items[GetAbsoluteIndex(index)];
            }
        }

        /// <summary>
        /// Gets the next item in the object pool
        /// </summary>
        /// <value>
        /// The next object pool item
        /// </value>
        protected T NextPoolItem
        {
            get { return _Items[_CurrentPosition]; }
        }

        /// <summary>
        /// Gets the current or last object pool item.
        /// </summary>
        /// <value>
        /// The current or last object pool item
        /// </value>
        public T Current
        {
            get
            {
                return _Items[_LastPosition];
            }
        }

        /// <summary>
        /// Gets the previous or 2nd to last object pool item
        /// </summary>
        /// <value>
        /// The previous or 2nd to last object pool item
        /// </value>
        public T Previous
        {
            get
            {
                return _Items[GetAbsoluteIndex(1)];
            }
        }

        /// <summary>
        /// Gets the length of filled items in the object pool.  Count will stop incrementing until Capacity is reached.
        /// </summary>
        /// <value>
        /// The count.
        /// </value>
        public int Length { get { return _Length; } }

        /// <summary>
        /// Gets the position of the current object pool item.
        /// </summary>
        /// <value>
        /// The position of the current object pool item.
        /// </value>
        public int Position { get { return _CurrentPosition; } }

        public long Sequence { get { return _Sequence; } }

        public int Capacity { get { return _Capacity; } }
        #endregion
    }
}
