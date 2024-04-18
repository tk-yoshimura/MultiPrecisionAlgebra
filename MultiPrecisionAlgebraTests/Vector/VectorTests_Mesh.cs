﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;
using MultiPrecisionAlgebra;

namespace MultiPrecisionAlgebraTests {
    public partial class VectorTests {
        [TestMethod()]
        public void MeshGridTest() {
            Vector<Pow2.N4> x = new double[] { 1, 2, 3 };
            Vector<Pow2.N4> y = new double[] { 4, 5, 6, 7 };
            Vector<Pow2.N4> z = new double[] { 8, 9, 10, 11, 12 };
            Vector<Pow2.N4> w = new double[] { 13, 14 };

            Assert.AreEqual((
                new Vector<Pow2.N4>(1, 2, 3, 1, 2, 3, 1, 2, 3, 1, 2, 3),
                new Vector<Pow2.N4>(4, 4, 4, 5, 5, 5, 6, 6, 6, 7, 7, 7)
                ),
                Vector<Pow2.N4>.MeshGrid(x, y)
            );

            Assert.AreEqual((
                new Vector<Pow2.N4>(
                    1, 2, 3, 1, 2, 3, 1, 2, 3, 1, 2, 3,
                    1, 2, 3, 1, 2, 3, 1, 2, 3, 1, 2, 3,
                    1, 2, 3, 1, 2, 3, 1, 2, 3, 1, 2, 3,
                    1, 2, 3, 1, 2, 3, 1, 2, 3, 1, 2, 3,
                    1, 2, 3, 1, 2, 3, 1, 2, 3, 1, 2, 3),
                new Vector<Pow2.N4>(
                    4, 4, 4, 5, 5, 5, 6, 6, 6, 7, 7, 7,
                    4, 4, 4, 5, 5, 5, 6, 6, 6, 7, 7, 7,
                    4, 4, 4, 5, 5, 5, 6, 6, 6, 7, 7, 7,
                    4, 4, 4, 5, 5, 5, 6, 6, 6, 7, 7, 7,
                    4, 4, 4, 5, 5, 5, 6, 6, 6, 7, 7, 7),
                new Vector<Pow2.N4>(
                    8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8,
                    9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9,
                    10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10,
                    11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11,
                    12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12)
                ),
                Vector<Pow2.N4>.MeshGrid(x, y, z)
            );

            Assert.AreEqual((
                new Vector<Pow2.N4>(
                    1, 2, 3, 1, 2, 3, 1, 2, 3, 1, 2, 3,
                    1, 2, 3, 1, 2, 3, 1, 2, 3, 1, 2, 3,
                    1, 2, 3, 1, 2, 3, 1, 2, 3, 1, 2, 3,
                    1, 2, 3, 1, 2, 3, 1, 2, 3, 1, 2, 3,
                    1, 2, 3, 1, 2, 3, 1, 2, 3, 1, 2, 3,
                    1, 2, 3, 1, 2, 3, 1, 2, 3, 1, 2, 3,
                    1, 2, 3, 1, 2, 3, 1, 2, 3, 1, 2, 3,
                    1, 2, 3, 1, 2, 3, 1, 2, 3, 1, 2, 3,
                    1, 2, 3, 1, 2, 3, 1, 2, 3, 1, 2, 3,
                    1, 2, 3, 1, 2, 3, 1, 2, 3, 1, 2, 3
                ),
                new Vector<Pow2.N4>(
                    4, 4, 4, 5, 5, 5, 6, 6, 6, 7, 7, 7,
                    4, 4, 4, 5, 5, 5, 6, 6, 6, 7, 7, 7,
                    4, 4, 4, 5, 5, 5, 6, 6, 6, 7, 7, 7,
                    4, 4, 4, 5, 5, 5, 6, 6, 6, 7, 7, 7,
                    4, 4, 4, 5, 5, 5, 6, 6, 6, 7, 7, 7,
                    4, 4, 4, 5, 5, 5, 6, 6, 6, 7, 7, 7,
                    4, 4, 4, 5, 5, 5, 6, 6, 6, 7, 7, 7,
                    4, 4, 4, 5, 5, 5, 6, 6, 6, 7, 7, 7,
                    4, 4, 4, 5, 5, 5, 6, 6, 6, 7, 7, 7,
                    4, 4, 4, 5, 5, 5, 6, 6, 6, 7, 7, 7
                ),
                new Vector<Pow2.N4>(
                    8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8,
                    9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9,
                    10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10,
                    11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11,
                    12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12,
                    8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8,
                    9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9,
                    10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10,
                    11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11,
                    12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12
                ),
                new Vector<Pow2.N4>(
                    13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13,
                    13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13,
                    13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13,
                    13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13,
                    13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13,

                    14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14,
                    14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14,
                    14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14,
                    14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14,
                    14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14)
                ),
                Vector<Pow2.N4>.MeshGrid(x, y, z, w)
            );
        }
    }
}