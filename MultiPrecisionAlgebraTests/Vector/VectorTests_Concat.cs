using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;
using MultiPrecisionAlgebra;
using System;

namespace MultiPrecisionAlgebraTests {
    public partial class VectorTests {
        [TestMethod()]
        public void ConcatTest() {
            Vector<Pow2.N4> vector1 = Vector<Pow2.N4>.Fill(1, value: -1);
            Vector<Pow2.N4> vector2 = Vector<Pow2.N4>.Fill(2, value: -2);
            Vector<Pow2.N4> vector4 = Vector<Pow2.N4>.Fill(4, value: -3);

            Assert.AreEqual(new Vector<Pow2.N4>(-1, -2, -2, -3, -3, -3, -3), Vector<Pow2.N4>.Concat(vector1, vector2, vector4));
            Assert.AreEqual(new Vector<Pow2.N4>(-2, -2, -3, -3, -3, -3, -1), Vector<Pow2.N4>.Concat(vector2, vector4, vector1));
            Assert.AreEqual(new Vector<Pow2.N4>(-1, -2, -2, 1, -3, -3, -3, -3), Vector<Pow2.N4>.Concat(vector1, vector2, 1, vector4));
            Assert.AreEqual(new Vector<Pow2.N4>(-1, -2, -2, 2, -3, -3, -3, -3), Vector<Pow2.N4>.Concat(vector1, vector2, 2L, vector4));
            Assert.AreEqual(new Vector<Pow2.N4>(-1, -2, -2, 3, -3, -3, -3, -3), Vector<Pow2.N4>.Concat(vector1, vector2, (MultiPrecision<Pow2.N4>)3, vector4));
            Assert.AreEqual(new Vector<Pow2.N4>(-1, -2, -2, 4, -3, -3, -3, -3), Vector<Pow2.N4>.Concat(vector1, vector2, 4d, vector4));
            Assert.AreEqual(new Vector<Pow2.N4>(-1, -2, -2, 5, -3, -3, -3, -3), Vector<Pow2.N4>.Concat(vector1, vector2, 5f, vector4));
            Assert.AreEqual(new Vector<Pow2.N4>(-1, -2, -2, "6.2", -3, -3, -3, -3), Vector<Pow2.N4>.Concat(vector1, vector2, "6.2", vector4));

            Assert.ThrowsExactly<ArgumentException>(() => {
                Vector<Pow2.N4>.Concat(vector1, vector2, 'b', vector4);
            });
        }
    }
}