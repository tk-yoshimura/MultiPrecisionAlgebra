using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;
using MultiPrecisionAlgebra;

namespace MultiPrecisionAlgebraTests {
    public partial class MatrixTests {
        [TestMethod()]
        public void OperatorTest() {
            Matrix<Pow2.N4> matrix1 = new(new double[,] { { 1, 2, 3 }, { 4, 5, 6 } });
            Matrix<Pow2.N4> matrix2 = new(new double[,] { { 7, 8, 9 }, { 1, 2, 3 } });
            Matrix<Pow2.N4> matrix3 = new(new double[,] { { 1, 2 }, { 3, 4 }, { 5, 6 } });
            Vector<Pow2.N4> vector1 = new(5, 4, 3);
            Vector<Pow2.N4> vector2 = new(5, 4);

            Assert.AreEqual(new Matrix<Pow2.N4>(new double[,] { { 1, 2, 3 }, { 4, 5, 6 } }), +matrix1);
            Assert.AreEqual(new Matrix<Pow2.N4>(new double[,] { { -1, -2, -3 }, { -4, -5, -6 } }), -matrix1);
            Assert.AreEqual(new Matrix<Pow2.N4>(new double[,] { { 8, 10, 12 }, { 5, 7, 9 } }), matrix1 + matrix2);
            Assert.AreEqual(new Matrix<Pow2.N4>(new double[,] { { 8, 10, 12 }, { 5, 7, 9 } }), matrix2 + matrix1);
            Assert.AreEqual(new Matrix<Pow2.N4>(new double[,] { { 3, 4, 5 }, { 6, 7, 8 } }), matrix1 + 2);
            Assert.AreEqual(new Matrix<Pow2.N4>(new double[,] { { 3, 4, 5 }, { 6, 7, 8 } }), 2 + matrix1);
            Assert.AreEqual(new Matrix<Pow2.N4>(new double[,] { { -6, -6, -6 }, { 3, 3, 3 } }), matrix1 - matrix2);
            Assert.AreEqual(new Matrix<Pow2.N4>(new double[,] { { 6, 6, 6 }, { -3, -3, -3 } }), matrix2 - matrix1);
            Assert.AreEqual(new Matrix<Pow2.N4>(new double[,] { { -1, 0, 1 }, { 2, 3, 4 } }), matrix1 - 2);
            Assert.AreEqual(new Matrix<Pow2.N4>(new double[,] { { 1, 0, -1 }, { -2, -3, -4 } }), 2 - matrix1);

            Assert.AreEqual(new Matrix<Pow2.N4>(new double[,] { { 22, 28 }, { 49, 64 } }), matrix1 * matrix3);
            Assert.AreEqual(new Matrix<Pow2.N4>(new double[,] { { 9, 12, 15 }, { 19, 26, 33 }, { 29, 40, 51 } }), matrix3 * matrix1);

            Assert.AreEqual(new Matrix<Pow2.N4>(new double[,] { { 7, 16, 27 }, { 4, 10, 18 } }), Matrix<Pow2.N4>.ElementwiseMul(matrix1, matrix2));
            Assert.AreEqual(new Matrix<Pow2.N4>(new MultiPrecision<Pow2.N4>[,] { { 7, 4, 3 }, { 0.25, "0.4", 0.5 } }), Matrix<Pow2.N4>.ElementwiseDiv(matrix2, matrix1));

            Assert.AreEqual(new Matrix<Pow2.N4>(new double[,] { { 2, 4, 6 }, { 8, 10, 12 } }), matrix1 * 2);
            Assert.AreEqual(new Matrix<Pow2.N4>(new double[,] { { 2, 4, 6 }, { 8, 10, 12 } }), 2 * matrix1);
            Assert.AreEqual(new Matrix<Pow2.N4>(new double[,] { { 0.5, 1, 1.5 }, { 2, 2.5, 3 } }), matrix1 / 2);
            Assert.AreEqual(new Matrix<Pow2.N4>(new MultiPrecision<Pow2.N4>[,] { { 2, 1, MultiPrecision<Pow2.N4>.Div(2, 3) }, { 0.5, "0.4", MultiPrecision<Pow2.N4>.Div(2, 6) } }), 2 / matrix1);

            Assert.AreEqual(new Vector<Pow2.N4>(22, 58), matrix1 * vector1);
            Assert.AreEqual(new Vector<Pow2.N4>(32, 44), vector1 * matrix3);
            Assert.AreEqual(new Vector<Pow2.N4>(21, 30, 39), vector2 * matrix1);
            Assert.AreEqual(new Vector<Pow2.N4>(13, 31, 49), matrix3 * vector2);

            Assert.AreEqual(new Matrix<Pow2.N4>(new double[,] { { 2, 4, 6 }, { 8, 10, 12 } }), matrix1 * 2);
            Assert.AreEqual(new Matrix<Pow2.N4>(new double[,] { { 2, 4, 6 }, { 8, 10, 12 } }), 2 * matrix1);
            Assert.AreEqual(new Matrix<Pow2.N4>(new double[,] { { 0.5, 1, 1.5 }, { 2, 2.5, 3 } }), matrix1 / 2);
        }

        [TestMethod()]
        public void OperatorEqualTest() {
            Matrix<Pow2.N4> matrix = new(new double[,] { { 1, 2, 3 }, { 4, 5, 6 } });

            Assert.IsTrue(matrix == new Matrix<Pow2.N4>(new double[,] { { 1, 2, 3 }, { 4, 5, 6 } }));
            Assert.IsTrue(matrix != new Matrix<Pow2.N4>(new double[,] { { 1, 2, 3 }, { 4, 5, 7 } }));
            Assert.IsTrue(matrix != new Matrix<Pow2.N4>(new double[,] { { 1, 2 }, { 4, 5 } }));
            Assert.IsTrue(matrix != new Matrix<Pow2.N4>(new double[,] { { 1, 2, 3, 4 }, { 4, 5, 6, 7 } }));
            Assert.IsTrue(matrix != new Matrix<Pow2.N4>(new double[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } }));
            Assert.IsFalse(matrix == null);
            Assert.IsTrue(matrix != null);
            Assert.IsTrue(matrix != new Matrix<Pow2.N4>(new double[,] { { 1, 2, 3 }, { 4, 5, double.NaN } }));
        }

        [TestMethod()]
        public void EqualsTest() {
            Matrix<Pow2.N4> matrix = new(new double[,] { { 1, 2, 3 }, { 4, 5, 6 } });

            Assert.IsTrue(matrix.Equals(new Matrix<Pow2.N4>(new double[,] { { 1, 2, 3 }, { 4, 5, 6 } })));
            Assert.IsFalse(matrix.Equals(new Matrix<Pow2.N4>(new double[,] { { 1, 2, 3 }, { 4, 5, 7 } })));
            Assert.IsFalse(matrix.Equals(new Matrix<Pow2.N4>(new double[,] { { 1, 2 }, { 4, 5 } })));
            Assert.IsFalse(matrix.Equals(new Matrix<Pow2.N4>(new double[,] { { 1, 2, 3, 4 }, { 4, 5, 6, 7 } })));
            Assert.IsFalse(matrix.Equals(new Matrix<Pow2.N4>(new double[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } })));
            Assert.IsFalse(matrix.Equals(null));
            Assert.IsFalse(matrix.Equals(new Matrix<Pow2.N4>(new double[,] { { 1, 2, 3 }, { 4, 5, double.NaN } })));
        }
    }
}