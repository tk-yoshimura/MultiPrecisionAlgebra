using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;
using MultiPrecisionAlgebra;
using System;

namespace MultiPrecisionAlgebraTests {
    public partial class MatrixTests {
        [TestMethod()]
        public void LUDecomposeN4Test() {
            foreach (Matrix<Pow2.N4> matrix in MatrixTestCases<Pow2.N4>.RegularMatrixs) {
                Console.WriteLine($"test: {matrix}");

                (Matrix<Pow2.N4> pivot, Matrix<Pow2.N4> lower, Matrix<Pow2.N4> upper) = Matrix<Pow2.N4>.LU(matrix);

                foreach (var diagonal in lower.Diagonals) {
                    Assert.AreEqual(1, diagonal);
                }

                Assert.IsTrue((pivot * matrix - lower * upper).Norm < 1e-28);
            }
        }

        [TestMethod()]
        public void LUDecomposeLargeN4Test() {
            foreach (Matrix<Pow2.N4> matrix in MatrixTestCases<Pow2.N4>.LargeMatrixs) {
                Console.WriteLine($"test: {matrix}");

                (Matrix<Pow2.N4> pivot, Matrix<Pow2.N4> lower, Matrix<Pow2.N4> upper) = Matrix<Pow2.N4>.LU(matrix);

                foreach (var diagonal in lower.Diagonals) {
                    Assert.AreEqual(1, diagonal);
                }

                Assert.IsTrue((pivot * matrix - lower * upper).Norm < 1e-28);
            }
        }

        [TestMethod()]
        public void LUDecomposeN8Test() {
            foreach (Matrix<Pow2.N8> matrix in MatrixTestCases<Pow2.N8>.RegularMatrixs) {
                Console.WriteLine($"test: {matrix}");

                (Matrix<Pow2.N8> pivot, Matrix<Pow2.N8> lower, Matrix<Pow2.N8> upper) = Matrix<Pow2.N8>.LU(matrix);

                foreach (var diagonal in lower.Diagonals) {
                    Assert.AreEqual(1, diagonal);
                }

                Assert.IsTrue((pivot * matrix - lower * upper).Norm < 1e-56);
            }
        }

        [TestMethod()]
        public void LUDecomposeLargeN8Test() {
            foreach (Matrix<Pow2.N8> matrix in MatrixTestCases<Pow2.N8>.LargeMatrixs) {
                Console.WriteLine($"test: {matrix}");

                (Matrix<Pow2.N8> pivot, Matrix<Pow2.N8> lower, Matrix<Pow2.N8> upper) = Matrix<Pow2.N8>.LU(matrix);

                foreach (var diagonal in lower.Diagonals) {
                    Assert.AreEqual(1, diagonal);
                }

                Assert.IsTrue((pivot * matrix - lower * upper).Norm < 1e-56);
            }
        }

        [TestMethod()]
        public void LUDecomposeSingularTest() {
            foreach (Matrix<Pow2.N8> matrix in MatrixTestCases<Pow2.N8>.SingularMatrixs) {

                Console.WriteLine($"test: {matrix}");

                (Matrix<Pow2.N8> pivot, Matrix<Pow2.N8> lower, Matrix<Pow2.N8> upper) = Matrix<Pow2.N8>.LU(matrix);

                Assert.IsTrue(Matrix<Pow2.N8>.IsFinite(pivot));
                Assert.IsTrue(!Matrix<Pow2.N8>.IsValid(lower));
                Assert.IsTrue(Matrix<Pow2.N8>.IsZero(upper));
                Assert.AreEqual(0d, matrix.Det);
            }
        }
    }
}