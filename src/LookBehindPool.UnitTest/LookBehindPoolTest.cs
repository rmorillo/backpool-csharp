using Microsoft.VisualStudio.TestTools.UnitTesting;
using LookBehindPool;
using System;

namespace LookBehindPool.UnitTest
{
    [TestClass]
    public class LookBehindPoolTest
    {
        [TestMethod]
        public void LookBehindPool_Constructor_Works()
        {
            //Arrange & Act
            int capacity = 10;
            var numberPool = new LookBehindPool<FloatPoolItem>(capacity, ( ) => new FloatPoolItem() { value = float.NaN });

            //Assert
            Assert.IsNotNull(numberPool.Length == 0);
            Assert.IsNotNull(numberPool.Capacity == capacity);
        }

        [TestMethod]
        public void LookBehindPool_Update_Works()
        {
            //Arrange
            int capacity = 10;
            var poolValue = 45;            
            var pool = new LookBehindPool<FloatPoolItem>(capacity, () => new FloatPoolItem() { value = float.NaN });

            //Act
            pool.Update((s) => s.value = poolValue);

            //Assert
            Assert.AreEqual(pool.Length, 1);
            Assert.AreEqual(pool[0].value, poolValue);
            Assert.AreEqual(pool.Current.value, poolValue);
        }

        [TestMethod]
        public void LookBehindPool_UpdateRollover_Works()
        {
            //Arrange
            int capacity = 3;
            var pool = new LookBehindPool<FloatPoolItem>(capacity, () => new FloatPoolItem() { value = float.NaN });

            //Act
            pool.Update((s) => s.value = 2);
            pool.Update((s) => s.value = 1);
            pool.Update((s) => s.value = 0);

            //Assert
            Assert.AreEqual(pool[0].value, 0);
            Assert.AreEqual(pool.Previous.value, 1);
            Assert.AreEqual(pool.Length, 3);
        }

        [TestMethod]
        public void LookBehindPool_PoolIndexOutOfRange_ThrowsAnException()
        {
            //Arrange
            int capacity = 10;
            var pool = new LookBehindPool<FloatPoolItem>(capacity, () => new FloatPoolItem() { value = float.NaN });
            pool.Update((s) => s.value = 1);

            //Act & Assert

            Assert.ThrowsException<IndexOutOfRangeException>(() => pool[1]);
        }
    }
}
