using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;
using MultiPrecisionAlgebra;
using System;

namespace MultiPrecisionAlgebraTests {
    public partial class MatrixTests {
        [TestMethod()]
        public void CholeskyTest() {
            foreach (Matrix<Pow2.N4> matrix in MatrixTestCases<Pow2.N4>.PositiveMatrixs) {
                Console.WriteLine($"test: {matrix}");

                Matrix<Pow2.N4> l = Matrix<Pow2.N4>.Cholesky(matrix);
                Matrix<Pow2.N4> v = l * l.T;

                Assert.IsTrue((matrix - v).Norm < 1e-32);
            }
        }
    }
}