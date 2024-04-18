using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;
using MultiPrecisionAlgebra;
using System;

namespace MultiPrecisionAlgebraTests {
    public partial class MatrixTests {
        [TestMethod()]
        public void SVDDecomposeN4Test() {
            foreach (Matrix<Pow2.N4> matrix in MatrixTestCases<Pow2.N4>.RegularMatrixs) {
                Console.WriteLine($"test: {matrix}");

                (Matrix<Pow2.N4> u, Vector<Pow2.N4> s, Matrix<Pow2.N4> v) = Matrix<Pow2.N4>.SVD(matrix);

                Matrix<Pow2.N4> matrix2 = u * Matrix<Pow2.N4>.FromDiagonals(s) * v.T;

                Assert.IsTrue((matrix - matrix2).Norm < 1e-25);
                Assert.IsTrue(MultiPrecision<Pow2.N4>.Abs(MultiPrecision<Pow2.N4>.Abs(u.Det) - 1d) < 1e-25);
                Assert.IsTrue(MultiPrecision<Pow2.N4>.Abs(MultiPrecision<Pow2.N4>.Abs(v.Det) - 1d) < 1e-25);
            }
        }

        [TestMethod()]
        public void SVDDecomposeLargeN4Test() {
            foreach (Matrix<Pow2.N4> matrix in MatrixTestCases<Pow2.N4>.LargeMatrixs) {
                Console.WriteLine($"test: {matrix}");

                (Matrix<Pow2.N4> u, Vector<Pow2.N4> s, Matrix<Pow2.N4> v) = Matrix<Pow2.N4>.SVD(matrix);

                Matrix<Pow2.N4> matrix2 = u * Matrix<Pow2.N4>.FromDiagonals(s) * v.T;

                Assert.IsTrue((matrix - matrix2).Norm < 1e-25);
                Assert.IsTrue(MultiPrecision<Pow2.N4>.Abs(MultiPrecision<Pow2.N4>.Abs(u.Det) - 1d) < 1e-25);
                Assert.IsTrue(MultiPrecision<Pow2.N4>.Abs(MultiPrecision<Pow2.N4>.Abs(v.Det) - 1d) < 1e-25);
            }
        }

        [TestMethod()]
        public void SVDDecomposeLargeRectN4Test() {
            foreach (Matrix<Pow2.N4> matrix in MatrixTestCases<Pow2.N4>.LargeMatrixs) {
                Matrix<Pow2.N4> matrix_rect = matrix[..^3, ..];

                Console.WriteLine($"test: {matrix_rect}");

                (Matrix<Pow2.N4> u, Vector<Pow2.N4> s, Matrix<Pow2.N4> v) = Matrix<Pow2.N4>.SVD(matrix_rect);

                Matrix<Pow2.N4> sm = Matrix<Pow2.N4>.HConcat(Matrix<Pow2.N4>.FromDiagonals(s), Matrix<Pow2.N4>.Zero(matrix.Size - 3, 3));

                Matrix<Pow2.N4> matrix2 = u * sm * v.T;

                Assert.IsTrue((matrix_rect - matrix2).Norm < 1e-25);
                Assert.IsTrue(MultiPrecision<Pow2.N4>.Abs(MultiPrecision<Pow2.N4>.Abs(u.Det) - 1d) < 1e-25);
                Assert.IsTrue(MultiPrecision<Pow2.N4>.Abs(MultiPrecision<Pow2.N4>.Abs(v.Det) - 1d) < 1e-25);
            }

            foreach (Matrix<Pow2.N4> matrix in MatrixTestCases<Pow2.N4>.LargeMatrixs) {
                Matrix<Pow2.N4> matrix_rect = matrix[.., ..^3];

                Console.WriteLine($"test: {matrix_rect}");

                (Matrix<Pow2.N4> u, Vector<Pow2.N4> s, Matrix<Pow2.N4> v) = Matrix<Pow2.N4>.SVD(matrix_rect);

                Matrix<Pow2.N4> sm = Matrix<Pow2.N4>.VConcat(Matrix<Pow2.N4>.FromDiagonals(s), Matrix<Pow2.N4>.Zero(3, matrix.Size - 3));

                Matrix<Pow2.N4> matrix2 = u * sm * v.T;

                Assert.IsTrue((matrix_rect - matrix2).Norm < 1e-25);
                Assert.IsTrue(MultiPrecision<Pow2.N4>.Abs(MultiPrecision<Pow2.N4>.Abs(u.Det) - 1d) < 1e-25);
                Assert.IsTrue(MultiPrecision<Pow2.N4>.Abs(MultiPrecision<Pow2.N4>.Abs(v.Det) - 1d) < 1e-25);
            }
        }

        [TestMethod()]
        public void SVDDecompose4x4N4Test() {
            Matrix<Pow2.N4> matrix = new double[,] { { 12, -51, 4, 6 }, { 6, 167, -68, 3 }, { -4, 24, -41, 12 }, { 8, 13, 7, 2 } };

            (Matrix<Pow2.N4> u, Vector<Pow2.N4> s, Matrix<Pow2.N4> v) = Matrix<Pow2.N4>.SVD(matrix);

            Matrix<Pow2.N4> matrix2 = u * Matrix<Pow2.N4>.FromDiagonals(s) * v.T;

            Console.WriteLine(matrix2);

            Assert.IsTrue((matrix - matrix2).Norm < 1e-25);
            Assert.IsTrue(MultiPrecision<Pow2.N4>.Abs(MultiPrecision<Pow2.N4>.Abs(u.Det) - 1d) < 1e-25);
            Assert.IsTrue(MultiPrecision<Pow2.N4>.Abs(MultiPrecision<Pow2.N4>.Abs(v.Det) - 1d) < 1e-25);
        }

        [TestMethod()]
        public void SVDDecompose3x4N4Test() {
            Matrix<Pow2.N4> matrix = new double[,] { { 12, -51, 4 }, { 6, 167, -68 }, { -4, 24, -41 }, { 8, 13, 7 } };

            (Matrix<Pow2.N4> u, Vector<Pow2.N4> s, Matrix<Pow2.N4> v) = Matrix<Pow2.N4>.SVD(matrix);

            Matrix<Pow2.N4> sm = Matrix<Pow2.N4>.VConcat(Matrix<Pow2.N4>.FromDiagonals(s), Matrix<Pow2.N4>.Zero(1, 3));

            Matrix<Pow2.N4> matrix2 = u * sm * v.T;

            Assert.IsTrue((matrix - matrix2).Norm < 1e-25);
            Assert.IsTrue(MultiPrecision<Pow2.N4>.Abs(MultiPrecision<Pow2.N4>.Abs(u.Det) - 1d) < 1e-25);
            Assert.IsTrue(MultiPrecision<Pow2.N4>.Abs(MultiPrecision<Pow2.N4>.Abs(v.Det) - 1d) < 1e-25);
        }

        [TestMethod()]
        public void SVDDecompose4x3N4Test() {
            Matrix<Pow2.N4> matrix = new double[,] { { 12, -51, 4, 6 }, { 6, 167, -68, 3 }, { -4, 24, -41, 12 } };

            (Matrix<Pow2.N4> u, Vector<Pow2.N4> s, Matrix<Pow2.N4> v) = Matrix<Pow2.N4>.SVD(matrix);

            Matrix<Pow2.N4> sm = Matrix<Pow2.N4>.HConcat(Matrix<Pow2.N4>.FromDiagonals(s), Matrix<Pow2.N4>.Zero(3, 1));

            Matrix<Pow2.N4> matrix2 = u * sm * v.T;

            Console.WriteLine(matrix2);

            Assert.IsTrue((matrix - matrix2).Norm < 1e-25);
            Assert.IsTrue(MultiPrecision<Pow2.N4>.Abs(MultiPrecision<Pow2.N4>.Abs(u.Det) - 1d) < 1e-25);
            Assert.IsTrue(MultiPrecision<Pow2.N4>.Abs(MultiPrecision<Pow2.N4>.Abs(v.Det) - 1d) < 1e-25);
        }

        [TestMethod()]
        public void SVDDecomposeDet0N4Test() {
            Matrix<Pow2.N4> matrix = new double[,] { { 12, -51, 4 }, { -4, 24, -41 }, { -8, 48, -82 }, { -12, 72, -123 } };

            (Matrix<Pow2.N4> u, Vector<Pow2.N4> s, Matrix<Pow2.N4> v) = Matrix<Pow2.N4>.SVD(matrix);

            Matrix<Pow2.N4> sm = Matrix<Pow2.N4>.VConcat(Matrix<Pow2.N4>.FromDiagonals(s), Matrix<Pow2.N4>.Zero(1, 3));

            Matrix<Pow2.N4> matrix2 = u * sm * v.T;

            Assert.IsTrue((matrix - matrix2).Norm < 1e-25);
            Assert.IsTrue(MultiPrecision<Pow2.N4>.Abs(MultiPrecision<Pow2.N4>.Abs(u.Det) - 1d) < 1e-25);
            Assert.IsTrue(MultiPrecision<Pow2.N4>.Abs(MultiPrecision<Pow2.N4>.Abs(v.Det) - 1d) < 1e-25);
        }

        [TestMethod()]
        public void SVDDecomposeEyeN4Test() {
            Matrix<Pow2.N4> matrix = Matrix<Pow2.N4>.Identity(4);

            (Matrix<Pow2.N4> u, Vector<Pow2.N4> s, Matrix<Pow2.N4> v) = Matrix<Pow2.N4>.SVD(matrix);

            Matrix<Pow2.N4> matrix2 = u * Matrix<Pow2.N4>.FromDiagonals(s) * v.T;

            Console.WriteLine(matrix2);

            Assert.IsTrue((matrix - matrix2).Norm < 1e-25);
            Assert.IsTrue(MultiPrecision<Pow2.N4>.Abs(MultiPrecision<Pow2.N4>.Abs(u.Det) - 1d) < 1e-25);
            Assert.IsTrue(MultiPrecision<Pow2.N4>.Abs(MultiPrecision<Pow2.N4>.Abs(v.Det) - 1d) < 1e-25);
        }

        [TestMethod()]
        public void SVDDecomposeZeroVectorN4Test() {
            Matrix<Pow2.N4> matrix = Matrix<Pow2.N4>.Identity(4);

            matrix[0, 0] = 0;

            (Matrix<Pow2.N4> u, Vector<Pow2.N4> s, Matrix<Pow2.N4> v) = Matrix<Pow2.N4>.SVD(matrix);

            Matrix<Pow2.N4> matrix2 = u * Matrix<Pow2.N4>.FromDiagonals(s) * v.T;

            Console.WriteLine(matrix2);

            Assert.IsTrue((matrix - matrix2).Norm < 1e-25);
            Assert.IsTrue(MultiPrecision<Pow2.N4>.Abs(MultiPrecision<Pow2.N4>.Abs(u.Det) - 1d) < 1e-25);
            Assert.IsTrue(MultiPrecision<Pow2.N4>.Abs(MultiPrecision<Pow2.N4>.Abs(v.Det) - 1d) < 1e-25);
        }

        [TestMethod()]
        public void SVDDecomposeN8Test() {
            foreach (Matrix<Pow2.N8> matrix in MatrixTestCases<Pow2.N8>.RegularMatrixs) {
                Console.WriteLine($"test: {matrix}");

                (Matrix<Pow2.N8> u, Vector<Pow2.N8> s, Matrix<Pow2.N8> v) = Matrix<Pow2.N8>.SVD(matrix);

                Matrix<Pow2.N8> matrix2 = u * Matrix<Pow2.N8>.FromDiagonals(s) * v.T;

                Assert.IsTrue((matrix - matrix2).Norm < 1e-65);
                Assert.IsTrue(MultiPrecision<Pow2.N8>.Abs(MultiPrecision<Pow2.N8>.Abs(u.Det) - 1d) < 1e-65);
                Assert.IsTrue(MultiPrecision<Pow2.N8>.Abs(MultiPrecision<Pow2.N8>.Abs(v.Det) - 1d) < 1e-65);
            }
        }

        [TestMethod()]
        public void SVDDecomposeLargeN8Test() {
            foreach (Matrix<Pow2.N8> matrix in MatrixTestCases<Pow2.N8>.LargeMatrixs) {
                Console.WriteLine($"test: {matrix}");

                (Matrix<Pow2.N8> u, Vector<Pow2.N8> s, Matrix<Pow2.N8> v) = Matrix<Pow2.N8>.SVD(matrix);

                Matrix<Pow2.N8> matrix2 = u * Matrix<Pow2.N8>.FromDiagonals(s) * v.T;

                Assert.IsTrue((matrix - matrix2).Norm < 1e-65);
                Assert.IsTrue(MultiPrecision<Pow2.N8>.Abs(MultiPrecision<Pow2.N8>.Abs(u.Det) - 1d) < 1e-65);
                Assert.IsTrue(MultiPrecision<Pow2.N8>.Abs(MultiPrecision<Pow2.N8>.Abs(v.Det) - 1d) < 1e-65);
            }
        }

        [TestMethod()]
        public void SVDDecomposeLargeRectN8Test() {
            foreach (Matrix<Pow2.N8> matrix in MatrixTestCases<Pow2.N8>.LargeMatrixs) {
                Matrix<Pow2.N8> matrix_rect = matrix[..^3, ..];

                Console.WriteLine($"test: {matrix_rect}");

                (Matrix<Pow2.N8> u, Vector<Pow2.N8> s, Matrix<Pow2.N8> v) = Matrix<Pow2.N8>.SVD(matrix_rect);

                Matrix<Pow2.N8> sm = Matrix<Pow2.N8>.HConcat(Matrix<Pow2.N8>.FromDiagonals(s), Matrix<Pow2.N8>.Zero(matrix.Size - 3, 3));

                Matrix<Pow2.N8> matrix2 = u * sm * v.T;

                Assert.IsTrue((matrix_rect - matrix2).Norm < 1e-65);
                Assert.IsTrue(MultiPrecision<Pow2.N8>.Abs(MultiPrecision<Pow2.N8>.Abs(u.Det) - 1d) < 1e-65);
                Assert.IsTrue(MultiPrecision<Pow2.N8>.Abs(MultiPrecision<Pow2.N8>.Abs(v.Det) - 1d) < 1e-65);
            }

            foreach (Matrix<Pow2.N8> matrix in MatrixTestCases<Pow2.N8>.LargeMatrixs) {
                Matrix<Pow2.N8> matrix_rect = matrix[.., ..^3];

                Console.WriteLine($"test: {matrix_rect}");

                (Matrix<Pow2.N8> u, Vector<Pow2.N8> s, Matrix<Pow2.N8> v) = Matrix<Pow2.N8>.SVD(matrix_rect);

                Matrix<Pow2.N8> sm = Matrix<Pow2.N8>.VConcat(Matrix<Pow2.N8>.FromDiagonals(s), Matrix<Pow2.N8>.Zero(3, matrix.Size - 3));

                Matrix<Pow2.N8> matrix2 = u * sm * v.T;

                Assert.IsTrue((matrix_rect - matrix2).Norm < 1e-65);
                Assert.IsTrue(MultiPrecision<Pow2.N8>.Abs(MultiPrecision<Pow2.N8>.Abs(u.Det) - 1d) < 1e-65);
                Assert.IsTrue(MultiPrecision<Pow2.N8>.Abs(MultiPrecision<Pow2.N8>.Abs(v.Det) - 1d) < 1e-65);
            }
        }

        [TestMethod()]
        public void SVDDecompose4x4N8Test() {
            Matrix<Pow2.N8> matrix = new double[,] { { 12, -51, 4, 6 }, { 6, 167, -68, 3 }, { -4, 24, -41, 12 }, { 8, 13, 7, 2 } };

            (Matrix<Pow2.N8> u, Vector<Pow2.N8> s, Matrix<Pow2.N8> v) = Matrix<Pow2.N8>.SVD(matrix);

            Matrix<Pow2.N8> matrix2 = u * Matrix<Pow2.N8>.FromDiagonals(s) * v.T;

            Console.WriteLine(matrix2);

            Assert.IsTrue((matrix - matrix2).Norm < 1e-65);
            Assert.IsTrue(MultiPrecision<Pow2.N8>.Abs(MultiPrecision<Pow2.N8>.Abs(u.Det) - 1d) < 1e-65);
            Assert.IsTrue(MultiPrecision<Pow2.N8>.Abs(MultiPrecision<Pow2.N8>.Abs(v.Det) - 1d) < 1e-65);
        }

        [TestMethod()]
        public void SVDDecompose3x4N8Test() {
            Matrix<Pow2.N8> matrix = new double[,] { { 12, -51, 4 }, { 6, 167, -68 }, { -4, 24, -41 }, { 8, 13, 7 } };

            (Matrix<Pow2.N8> u, Vector<Pow2.N8> s, Matrix<Pow2.N8> v) = Matrix<Pow2.N8>.SVD(matrix);

            Matrix<Pow2.N8> sm = Matrix<Pow2.N8>.VConcat(Matrix<Pow2.N8>.FromDiagonals(s), Matrix<Pow2.N8>.Zero(1, 3));

            Matrix<Pow2.N8> matrix2 = u * sm * v.T;

            Assert.IsTrue((matrix - matrix2).Norm < 1e-65);
            Assert.IsTrue(MultiPrecision<Pow2.N8>.Abs(MultiPrecision<Pow2.N8>.Abs(u.Det) - 1d) < 1e-65);
            Assert.IsTrue(MultiPrecision<Pow2.N8>.Abs(MultiPrecision<Pow2.N8>.Abs(v.Det) - 1d) < 1e-65);
        }

        [TestMethod()]
        public void SVDDecompose4x3N8Test() {
            Matrix<Pow2.N8> matrix = new double[,] { { 12, -51, 4, 6 }, { 6, 167, -68, 3 }, { -4, 24, -41, 12 } };

            (Matrix<Pow2.N8> u, Vector<Pow2.N8> s, Matrix<Pow2.N8> v) = Matrix<Pow2.N8>.SVD(matrix);

            Matrix<Pow2.N8> sm = Matrix<Pow2.N8>.HConcat(Matrix<Pow2.N8>.FromDiagonals(s), Matrix<Pow2.N8>.Zero(3, 1));

            Matrix<Pow2.N8> matrix2 = u * sm * v.T;

            Console.WriteLine(matrix2);

            Assert.IsTrue((matrix - matrix2).Norm < 1e-65);
            Assert.IsTrue(MultiPrecision<Pow2.N8>.Abs(MultiPrecision<Pow2.N8>.Abs(u.Det) - 1d) < 1e-65);
            Assert.IsTrue(MultiPrecision<Pow2.N8>.Abs(MultiPrecision<Pow2.N8>.Abs(v.Det) - 1d) < 1e-65);
        }

        [TestMethod()]
        public void SVDDecomposeDet0N8Test() {
            Matrix<Pow2.N8> matrix = new double[,] { { 12, -51, 4 }, { -4, 24, -41 }, { -8, 48, -82 }, { -12, 72, -123 } };

            (Matrix<Pow2.N8> u, Vector<Pow2.N8> s, Matrix<Pow2.N8> v) = Matrix<Pow2.N8>.SVD(matrix);

            Matrix<Pow2.N8> sm = Matrix<Pow2.N8>.VConcat(Matrix<Pow2.N8>.FromDiagonals(s), Matrix<Pow2.N8>.Zero(1, 3));

            Matrix<Pow2.N8> matrix2 = u * sm * v.T;

            Assert.IsTrue((matrix - matrix2).Norm < 1e-65);
            Assert.IsTrue(MultiPrecision<Pow2.N8>.Abs(MultiPrecision<Pow2.N8>.Abs(u.Det) - 1d) < 1e-65);
            Assert.IsTrue(MultiPrecision<Pow2.N8>.Abs(MultiPrecision<Pow2.N8>.Abs(v.Det) - 1d) < 1e-65);
        }

        [TestMethod()]
        public void SVDDecomposeEyeN8Test() {
            Matrix<Pow2.N8> matrix = Matrix<Pow2.N8>.Identity(4);

            (Matrix<Pow2.N8> u, Vector<Pow2.N8> s, Matrix<Pow2.N8> v) = Matrix<Pow2.N8>.SVD(matrix);

            Matrix<Pow2.N8> matrix2 = u * Matrix<Pow2.N8>.FromDiagonals(s) * v.T;

            Console.WriteLine(matrix2);

            Assert.IsTrue((matrix - matrix2).Norm < 1e-65);
            Assert.IsTrue(MultiPrecision<Pow2.N8>.Abs(MultiPrecision<Pow2.N8>.Abs(u.Det) - 1d) < 1e-65);
            Assert.IsTrue(MultiPrecision<Pow2.N8>.Abs(MultiPrecision<Pow2.N8>.Abs(v.Det) - 1d) < 1e-65);
        }

        [TestMethod()]
        public void SVDDecomposeZeroVectorN8Test() {
            Matrix<Pow2.N8> matrix = Matrix<Pow2.N8>.Identity(4);

            matrix[0, 0] = 0;

            (Matrix<Pow2.N8> u, Vector<Pow2.N8> s, Matrix<Pow2.N8> v) = Matrix<Pow2.N8>.SVD(matrix);

            Matrix<Pow2.N8> matrix2 = u * Matrix<Pow2.N8>.FromDiagonals(s) * v.T;

            Console.WriteLine(matrix2);

            Assert.IsTrue((matrix - matrix2).Norm < 1e-65);
            Assert.IsTrue(MultiPrecision<Pow2.N8>.Abs(MultiPrecision<Pow2.N8>.Abs(u.Det) - 1d) < 1e-65);
            Assert.IsTrue(MultiPrecision<Pow2.N8>.Abs(MultiPrecision<Pow2.N8>.Abs(v.Det) - 1d) < 1e-65);
        }
    }
}