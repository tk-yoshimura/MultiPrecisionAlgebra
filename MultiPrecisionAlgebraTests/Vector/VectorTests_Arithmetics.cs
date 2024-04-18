using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;
using MultiPrecisionAlgebra;

namespace MultiPrecisionAlgebraTests {
    public partial class VectorTests {
        [TestMethod()]
        public void OperatorTest() {
            Vector<Pow2.N4> vector1 = new(1, 2);
            Vector<Pow2.N4> vector2 = new(3, 4);

            Assert.AreEqual(new Vector<Pow2.N4>(1, 2), +vector1);
            Assert.AreEqual(new Vector<Pow2.N4>(-1, -2), -vector1);
            Assert.AreEqual(new Vector<Pow2.N4>(4, 6), vector1 + vector2);
            Assert.AreEqual(new Vector<Pow2.N4>(4, 6), vector2 + vector1);
            Assert.AreEqual(new Vector<Pow2.N4>(4, 5), vector1 + 3);
            Assert.AreEqual(new Vector<Pow2.N4>(4, 5), 3 + vector1);
            Assert.AreEqual(new Vector<Pow2.N4>(-2, -2), vector1 - vector2);
            Assert.AreEqual(new Vector<Pow2.N4>(2, 2), vector2 - vector1);
            Assert.AreEqual(new Vector<Pow2.N4>(-2, -1), vector1 - 3);
            Assert.AreEqual(new Vector<Pow2.N4>(2, 1), 3 - vector1);
            Assert.AreEqual(new Vector<Pow2.N4>(2, 4), vector1 * 2);
            Assert.AreEqual(new Vector<Pow2.N4>(2, 4), 2 * vector1);
            Assert.AreEqual(new Vector<Pow2.N4>(0.5, 1), vector1 / 2);
            Assert.AreEqual(new Vector<Pow2.N4>(2, 1), 2 / vector1);
            Assert.AreEqual(new Vector<Pow2.N4>(3, 8), vector1 * vector2);
            Assert.AreEqual(new Vector<Pow2.N4>(MultiPrecision<Pow2.N4>.Div(1, 3), 0.5), vector1 / vector2);
            Assert.AreEqual(new Vector<Pow2.N4>(3, 2), vector2 / vector1);
        }



        [TestMethod()]
        public void OperatorEqualTest() {
            Vector<Pow2.N4> vector = new(1, 2);

            Assert.IsTrue(vector == new Vector<Pow2.N4>(1, 2));
            Assert.IsTrue(vector != new Vector<Pow2.N4>(2, 1));
            Assert.IsTrue(vector != new Vector<Pow2.N4>(1));
            Assert.IsTrue(vector != new Vector<Pow2.N4>(1, 2, 3));
            Assert.IsFalse(vector == null);
            Assert.IsTrue(vector != null);
            Assert.IsTrue(vector != new Vector<Pow2.N4>(1, double.NaN));
        }

        [TestMethod()]
        public void EqualsTest() {
            Vector<Pow2.N4> vector = new(1, 2);

            Assert.IsTrue(vector.Equals(new Vector<Pow2.N4>(1, 2)));
            Assert.IsFalse(vector.Equals(new Vector<Pow2.N4>(2, 1)));
            Assert.IsFalse(vector.Equals(new Vector<Pow2.N4>(1)));
            Assert.IsFalse(vector.Equals(new Vector<Pow2.N4>(1, 2, 3)));
            Assert.IsFalse(vector.Equals(null));
            Assert.IsFalse(vector.Equals(new Vector<Pow2.N4>(1, double.NaN)));
        }
    }
}