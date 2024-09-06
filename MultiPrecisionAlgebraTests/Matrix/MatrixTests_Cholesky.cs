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

        [TestMethod()]
        public void InversePositiveSymmetric() {
            foreach (Matrix<Pow2.N4> m in MatrixTestCases<Pow2.N4>.PositiveMatrixs) {
                Console.WriteLine($"test: {m}");
                Matrix<Pow2.N4> r = Matrix<Pow2.N4>.InversePositiveSymmetric(m);

                Assert.IsTrue((m * r - Matrix<Pow2.N4>.Identity(m.Size)).Norm < 1e-28);
            }
        }

        [TestMethod()]
        public void SlovePositiveSymmetric() {
            foreach (Matrix<Pow2.N4> m in MatrixTestCases<Pow2.N4>.PositiveMatrixs) {
                Console.WriteLine($"test: {m}");

                Vector<Pow2.N4> v = Vector<Pow2.N4>.Zero(m.Size);
                for (int i = 0; i < v.Dim; i++) {
                    v[i] = i + 2;
                }
                Matrix<Pow2.N4> r = Matrix<Pow2.N4>.InversePositiveSymmetric(m);

                Vector<Pow2.N4> u = Matrix<Pow2.N4>.SolvePositiveSymmetric(m, v);
                Vector<Pow2.N4> t = r * v;

                Assert.IsTrue((t - u).Norm < 1e-28);
            }
        }
    }
}