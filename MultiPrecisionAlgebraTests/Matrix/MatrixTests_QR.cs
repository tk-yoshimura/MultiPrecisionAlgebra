using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;
using MultiPrecisionAlgebra;
using System;

namespace MultiPrecisionAlgebraTests {
    public partial class MatrixTests {
        [TestMethod()]
        public void QRDecomposeN4Test() {
            foreach (Matrix<Pow2.N4> matrix in MatrixTestCases<Pow2.N4>.RegularMatrixs) {
                Console.WriteLine($"test: {matrix}");

                (Matrix<Pow2.N4> q, Matrix<Pow2.N4> r) = Matrix<Pow2.N4>.QR(matrix);

                Assert.IsTrue((matrix - q * r).Norm < 1e-25);
                Assert.IsTrue((q * q.T - Matrix<Pow2.N4>.Identity(matrix.Size)).Norm < 1e-28);
            }
        }

        [TestMethod()]
        public void QRDecomposeLargeN4Test() {
            foreach (Matrix<Pow2.N4> matrix in MatrixTestCases<Pow2.N4>.LargeMatrixs) {
                Console.WriteLine($"test: {matrix}");

                (Matrix<Pow2.N4> q, Matrix<Pow2.N4> r) = Matrix<Pow2.N4>.QR(matrix);

                Assert.IsTrue((matrix - q * r).Norm < 1e-25);
                Assert.IsTrue((q * q.T - Matrix<Pow2.N4>.Identity(matrix.Size)).Norm < 1e-28);
            }
        }

        [TestMethod()]
        public void QRDecompose2x2N4Test() {
            Matrix<Pow2.N4> matrix = new double[,] { { 1, 2 }, { 3, 4 } };

            (Matrix<Pow2.N4> q, Matrix<Pow2.N4> r) = Matrix<Pow2.N4>.QR(matrix);

            Assert.AreEqual(0d, r[1, 0]);

            Assert.IsTrue((matrix - q * r).Norm < 1e-25);
            Assert.IsTrue((q * q.T - Matrix<Pow2.N4>.Identity(matrix.Size)).Norm < 1e-31);
        }

        [TestMethod()]
        public void QRDecompose3x3N4Test() {
            Matrix<Pow2.N4> matrix = new double[,] { { 12, -51, 4 }, { 6, 167, -68 }, { -4, 24, -41 } };

            (Matrix<Pow2.N4> q, Matrix<Pow2.N4> r) = Matrix<Pow2.N4>.QR(matrix);

            Assert.AreEqual(new Matrix<Pow2.N4>(new double[,] { { 12, -51, 4 }, { 6, 167, -68 }, { -4, 24, -41 } }), matrix);

            Assert.AreEqual(0d, r[1, 0]);
            Assert.AreEqual(0d, r[2, 0]);
            Assert.AreEqual(0d, r[2, 1]);

            Assert.IsTrue((matrix - q * r).Norm < 1e-25);
            Assert.IsTrue((q * q.T - Matrix<Pow2.N4>.Identity(matrix.Size)).Norm < 1e-31);
        }

        [TestMethod()]
        public void QRDecompose4x4N4Test() {
            Matrix<Pow2.N4> matrix = new double[,] { { 12, -51, 4, 6 }, { 6, 167, -68, 3 }, { -4, 24, -41, 12 }, { 8, 13, 7, 2 } };

            (Matrix<Pow2.N4> q, Matrix<Pow2.N4> r) = Matrix<Pow2.N4>.QR(matrix);

            Assert.AreEqual(0d, r[1, 0]);
            Assert.AreEqual(0d, r[2, 0]);
            Assert.AreEqual(0d, r[3, 0]);
            Assert.AreEqual(0d, r[2, 1]);
            Assert.AreEqual(0d, r[3, 1]);
            Assert.AreEqual(0d, r[3, 2]);

            Assert.IsTrue((matrix - q * r).Norm < 1e-25);
            Assert.IsTrue((q * q.T - Matrix<Pow2.N4>.Identity(matrix.Size)).Norm < 1e-31);
        }

        [TestMethod()]
        public void QRDecomposeEyeN4Test() {
            Matrix<Pow2.N4> matrix = new double[,] { { 1, 0, 0 }, { 0, 1, 0 }, { 0, 0, 1 } };

            (Matrix<Pow2.N4> q, Matrix<Pow2.N4> r) = Matrix<Pow2.N4>.QR(matrix);

            Assert.IsTrue((matrix - q * r).Norm < 1e-25);
            Assert.IsTrue((q * q.T - Matrix<Pow2.N4>.Identity(matrix.Size)).Norm < 1e-31);
        }

        [TestMethod()]
        public void QRDecomposeEyeEpsN4Test() {
            Matrix<Pow2.N4> matrix = new double[,] { { 1, 1e-30, 0 }, { 2e-30, 1, 1e-30 }, { 0, -1e-30, 1 } };

            (Matrix<Pow2.N4> q, Matrix<Pow2.N4> r) = Matrix<Pow2.N4>.QR(matrix);

            Assert.IsTrue((matrix - q * r).Norm < 1e-25);
            Assert.IsTrue((q * q.T - Matrix<Pow2.N4>.Identity(matrix.Size)).Norm < 1e-31);
        }

        [TestMethod()]
        public void QRDecomposeN8Test() {
            foreach (Matrix<Pow2.N8> matrix in MatrixTestCases<Pow2.N8>.RegularMatrixs) {
                Console.WriteLine($"test: {matrix}");

                (Matrix<Pow2.N8> q, Matrix<Pow2.N8> r) = Matrix<Pow2.N8>.QR(matrix);

                Assert.IsTrue((matrix - q * r).Norm < 1e-50);
                Assert.IsTrue((q * q.T - Matrix<Pow2.N8>.Identity(matrix.Size)).Norm < 1e-56);
            }
        }

        [TestMethod()]
        public void QRDecomposeLargeN8Test() {
            foreach (Matrix<Pow2.N8> matrix in MatrixTestCases<Pow2.N8>.LargeMatrixs) {
                Console.WriteLine($"test: {matrix}");

                (Matrix<Pow2.N8> q, Matrix<Pow2.N8> r) = Matrix<Pow2.N8>.QR(matrix);

                Assert.IsTrue((matrix - q * r).Norm < 1e-50);
                Assert.IsTrue((q * q.T - Matrix<Pow2.N8>.Identity(matrix.Size)).Norm < 1e-56);
            }
        }

        [TestMethod()]
        public void QRDecompose2x2N8Test() {
            Matrix<Pow2.N8> matrix = new double[,] { { 1, 2 }, { 3, 4 } };

            (Matrix<Pow2.N8> q, Matrix<Pow2.N8> r) = Matrix<Pow2.N8>.QR(matrix);

            Assert.AreEqual(0d, r[1, 0]);

            Assert.IsTrue((matrix - q * r).Norm < 1e-50);
            Assert.IsTrue((q * q.T - Matrix<Pow2.N8>.Identity(matrix.Size)).Norm < 1e-62);
        }

        [TestMethod()]
        public void QRDecompose3x3N8Test() {
            Matrix<Pow2.N8> matrix = new double[,] { { 12, -51, 4 }, { 6, 167, -68 }, { -4, 24, -41 } };

            (Matrix<Pow2.N8> q, Matrix<Pow2.N8> r) = Matrix<Pow2.N8>.QR(matrix);

            Assert.AreEqual(new Matrix<Pow2.N8>(new double[,] { { 12, -51, 4 }, { 6, 167, -68 }, { -4, 24, -41 } }), matrix);

            Assert.AreEqual(0d, r[1, 0]);
            Assert.AreEqual(0d, r[2, 0]);
            Assert.AreEqual(0d, r[2, 1]);

            Assert.IsTrue((matrix - q * r).Norm < 1e-50);
            Assert.IsTrue((q * q.T - Matrix<Pow2.N8>.Identity(matrix.Size)).Norm < 1e-62);
        }

        [TestMethod()]
        public void QRDecompose4x4N8Test() {
            Matrix<Pow2.N8> matrix = new double[,] { { 12, -51, 4, 6 }, { 6, 167, -68, 3 }, { -4, 24, -41, 12 }, { 8, 13, 7, 2 } };

            (Matrix<Pow2.N8> q, Matrix<Pow2.N8> r) = Matrix<Pow2.N8>.QR(matrix);

            Assert.AreEqual(0d, r[1, 0]);
            Assert.AreEqual(0d, r[2, 0]);
            Assert.AreEqual(0d, r[3, 0]);
            Assert.AreEqual(0d, r[2, 1]);
            Assert.AreEqual(0d, r[3, 1]);
            Assert.AreEqual(0d, r[3, 2]);

            Assert.IsTrue((matrix - q * r).Norm < 1e-50);
            Assert.IsTrue((q * q.T - Matrix<Pow2.N8>.Identity(matrix.Size)).Norm < 1e-62);
        }

        [TestMethod()]
        public void QRDecomposeEyeN8Test() {
            Matrix<Pow2.N8> matrix = new double[,] { { 1, 0, 0 }, { 0, 1, 0 }, { 0, 0, 1 } };

            (Matrix<Pow2.N8> q, Matrix<Pow2.N8> r) = Matrix<Pow2.N8>.QR(matrix);

            Assert.IsTrue((matrix - q * r).Norm < 1e-50);
            Assert.IsTrue((q * q.T - Matrix<Pow2.N8>.Identity(matrix.Size)).Norm < 1e-62);
        }

        [TestMethod()]
        public void QRDecomposeEyeEpsN8Test() {
            Matrix<Pow2.N8> matrix = new double[,] { { 1, 1e-60, 0 }, { 2e-60, 1, 1e-60 }, { 0, -1e-60, 1 } };

            (Matrix<Pow2.N8> q, Matrix<Pow2.N8> r) = Matrix<Pow2.N8>.QR(matrix);

            Assert.IsTrue((matrix - q * r).Norm < 1e-25);
            Assert.IsTrue((q * q.T - Matrix<Pow2.N8>.Identity(matrix.Size)).Norm < 1e-62);
        }
    }
}