using Microsoft.VisualStudio.TestTools.UnitTesting;
using BackPool;
using System;

namespace BackPool.UnitTest
{
    [TestClass]
    public class ObjectPoolTest
    {
        [TestMethod]
        public void ObjectPool_Constructor_Works()
        {
            //Arrange & Act
            int capacity = 10;
            var numberPool = new ObjectPool<FloatPoolItem>(capacity, ( ) => new FloatPoolItem() { value = float.NaN });

            //Assert
            Assert.IsNotNull(numberPool.Length == 0);
            Assert.IsNotNull(numberPool.Capacity == capacity);
        }

        [TestMethod]
        public void ObjectPool_Update_Works()
        {
            //Arrange
            int capacity = 10;
            var poolValue = 45;            
            var pool = new ObjectPool<FloatPoolItem>(capacity, () => new FloatPoolItem() { value = float.NaN });

            //Act
            pool.Write((s) => s.value = poolValue);

            //Assert
            Assert.AreEqual(pool.Length, 1);
            Assert.AreEqual(pool[0].value, poolValue);
            Assert.AreEqual(pool.Current.value, poolValue);
        }

        [TestMethod]
        public void ObjectPool_UpdateRollover_Works()
        {
            //Arrange
            int capacity = 3;
            var pool = new ObjectPool<FloatPoolItem>(capacity, () => new FloatPoolItem() { value = float.NaN });

            //Act
            pool.Write((s) => s.value = 2);
            pool.Write((s) => s.value = 1);
            pool.Write((s) => s.value = 0);

            //Assert
            Assert.AreEqual(pool[0].value, 0);
            Assert.AreEqual(pool.Previous.value, 1);
            Assert.AreEqual(pool.Length, 3);
        }

        [TestMethod]
        public void ObjectPool_PoolIndexOutOfRange_ThrowsAnException()
        {
            //Arrange
            int capacity = 10;
            var pool = new ObjectPool<FloatPoolItem>(capacity, () => new FloatPoolItem() { value = float.NaN });
            pool.Write((s) => s.value = 1);

            //Act & Assert

            Assert.ThrowsException<IndexOutOfRangeException>(() => pool[1]);
        }
    }
}
