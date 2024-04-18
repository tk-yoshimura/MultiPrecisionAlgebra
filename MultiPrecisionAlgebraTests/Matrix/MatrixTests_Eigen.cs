using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;
using MultiPrecisionAlgebra;
using System;

namespace MultiPrecisionAlgebraTests {
    public partial class MatrixTests {
        [TestMethod()]
        public void EigenValuesN4Test() {
            Matrix<Pow2.N4> matrix = new double[,] { { 1, 2 }, { 4, 5 } };
            MultiPrecision<Pow2.N4>[] eigen_values = Matrix<Pow2.N4>.EigenValues(matrix);

            Assert.IsTrue(MultiPrecision<Pow2.N4>.Abs(eigen_values[0] - (3 + 2 * MultiPrecision<Pow2.N4>.Sqrt(3))) < 1e-29);
            Assert.IsTrue(MultiPrecision<Pow2.N4>.Abs(eigen_values[1] - (3 - 2 * MultiPrecision<Pow2.N4>.Sqrt(3))) < 1e-29);
        }

        [TestMethod()]
        public void EigenValuesEyeN4Test() {
            Matrix<Pow2.N4> matrix = new double[,] { { 1, 0, 0 }, { 0, 1, 0 }, { 0, 1, 0 } };
            MultiPrecision<Pow2.N4>[] eigen_values = Matrix<Pow2.N4>.EigenValues(matrix);

            Assert.IsTrue(MultiPrecision<Pow2.N4>.Abs(eigen_values[0] - 1) < 1e-29);
            Assert.IsTrue(MultiPrecision<Pow2.N4>.Abs(eigen_values[1] - 1) < 1e-29);
        }

        [TestMethod()]
        public void EigenValuesEyeEpsN4Test() {
            Matrix<Pow2.N4> matrix = new double[,] { { 1, 1e-30, 0 }, { 0, 1, 2e-30 }, { -1e-30, 1, 1e-30 } };
            MultiPrecision<Pow2.N4>[] eigen_values = Matrix<Pow2.N4>.EigenValues(matrix);

            Assert.IsTrue(MultiPrecision<Pow2.N4>.Abs(eigen_values[0] - 1) < 1e-29);
            Assert.IsTrue(MultiPrecision<Pow2.N4>.Abs(eigen_values[1] - 1) < 1e-29);
        }

        [TestMethod()]
        public void EigenVectorN4Test() {
            foreach (Matrix<Pow2.N4> matrix in MatrixTestCases<Pow2.N4>.PositiveMatrixs) {
                Console.WriteLine($"test: {matrix}");

                Matrix<Pow2.N4> matrix_scaled = Matrix<Pow2.N4>.ScaleB(matrix, -matrix.MaxExponent);

                (MultiPrecision<Pow2.N4>[] eigen_values, Vector<Pow2.N4>[] eigen_vectors) = Matrix<Pow2.N4>.EigenValueVectors(matrix_scaled);
                Vector<Pow2.N4> eigen_values_expected = Matrix<Pow2.N4>.EigenValues(matrix_scaled);

                Assert.IsTrue((eigen_values - eigen_values_expected).Norm < 1e-25);

                for (int i = 0; i < matrix_scaled.Size; i++) {
                    MultiPrecision<Pow2.N4> eigen_value = eigen_values[i];
                    Vector<Pow2.N4> eigen_vector = eigen_vectors[i];

                    Assert.IsTrue((matrix_scaled * eigen_vector - eigen_value * eigen_vector).Norm < 1e-1);
                }
            }
        }

        [TestMethod()]
        public void EigenVectorEyeN4Test() {
            Matrix<Pow2.N4> matrix = new double[,] { { 1, 0, 0 }, { 0, 1, 0 }, { 0, 0, 1 } };

            (MultiPrecision<Pow2.N4>[] eigen_values, Vector<Pow2.N4>[] eigen_vectors) = Matrix<Pow2.N4>.EigenValueVectors(matrix);

            Assert.AreEqual(new Matrix<Pow2.N4>(new double[,] { { 1, 0, 0 }, { 0, 1, 0 }, { 0, 0, 1 } }), matrix);

            for (int i = 0; i < matrix.Size; i++) {
                MultiPrecision<Pow2.N4> eigen_value = eigen_values[i];
                Vector<Pow2.N4> eigen_vector = eigen_vectors[i];

                Assert.IsTrue((matrix * eigen_vector - eigen_value * eigen_vector).Norm < 1e-15);
            }
        }

        [TestMethod()]
        public void EigenVectorEyeEpsN4Test() {
            Matrix<Pow2.N4> matrix = new double[,] { { 1, 1e-30, 0 }, { 0, 1, 2e-30 }, { -1e-30, 1e-30, 1 } };

            (MultiPrecision<Pow2.N4>[] eigen_values, Vector<Pow2.N4>[] eigen_vectors) = Matrix<Pow2.N4>.EigenValueVectors(matrix);

            Assert.AreEqual(new double[,] { { 1, 1e-30, 0 }, { 0, 1, 2e-30 }, { -1e-30, 1e-30, 1 } }, matrix);

            for (int i = 0; i < matrix.Size; i++) {
                MultiPrecision<Pow2.N4> eigen_value = eigen_values[i];
                Vector<Pow2.N4> eigen_vector = eigen_vectors[i];

                Assert.IsTrue((matrix * eigen_vector - eigen_value * eigen_vector).Norm < 1e-15);
            }
        }

        [TestMethod()]
        public void EigenValuesN8Test() {
            Matrix<Pow2.N8> matrix = new double[,] { { 1, 2 }, { 4, 5 } };
            MultiPrecision<Pow2.N8>[] eigen_values = Matrix<Pow2.N8>.EigenValues(matrix);

            Assert.IsTrue(MultiPrecision<Pow2.N8>.Abs(eigen_values[0] - (3 + 2 * MultiPrecision<Pow2.N8>.Sqrt(3))) < 1e-58);
            Assert.IsTrue(MultiPrecision<Pow2.N8>.Abs(eigen_values[1] - (3 - 2 * MultiPrecision<Pow2.N8>.Sqrt(3))) < 1e-58);
        }

        [TestMethod()]
        public void EigenValuesEyeN8Test() {
            Matrix<Pow2.N8> matrix = new double[,] { { 1, 0, 0 }, { 0, 1, 0 }, { 0, 1, 0 } };
            MultiPrecision<Pow2.N8>[] eigen_values = Matrix<Pow2.N8>.EigenValues(matrix);

            Assert.IsTrue(MultiPrecision<Pow2.N8>.Abs(eigen_values[0] - 1) < 1e-58);
            Assert.IsTrue(MultiPrecision<Pow2.N8>.Abs(eigen_values[1] - 1) < 1e-58);
        }

        [TestMethod()]
        public void EigenValuesEyeEpsN8Test() {
            Matrix<Pow2.N8> matrix = new double[,] { { 1, 1e-60, 0 }, { 0, 1, 2e-60 }, { -1e-60, 1, 1e-60 } };
            MultiPrecision<Pow2.N8>[] eigen_values = Matrix<Pow2.N8>.EigenValues(matrix);

            Assert.IsTrue(MultiPrecision<Pow2.N8>.Abs(eigen_values[0] - 1) < 1e-58);
            Assert.IsTrue(MultiPrecision<Pow2.N8>.Abs(eigen_values[1] - 1) < 1e-58);
        }

        [TestMethod()]
        public void EigenVectorN8Test() {
            foreach (Matrix<Pow2.N8> matrix in MatrixTestCases<Pow2.N8>.PositiveMatrixs) {
                Console.WriteLine($"test: {matrix}");

                Matrix<Pow2.N8> matrix_scaled = Matrix<Pow2.N8>.ScaleB(matrix, -matrix.MaxExponent);

                (MultiPrecision<Pow2.N8>[] eigen_values, Vector<Pow2.N8>[] eigen_vectors) = Matrix<Pow2.N8>.EigenValueVectors(matrix_scaled);
                Vector<Pow2.N8> eigen_values_expected = Matrix<Pow2.N8>.EigenValues(matrix_scaled);

                Assert.IsTrue((eigen_values - eigen_values_expected).Norm < 1e-58);

                for (int i = 0; i < matrix_scaled.Size; i++) {
                    MultiPrecision<Pow2.N8> eigen_value = eigen_values[i];
                    Vector<Pow2.N8> eigen_vector = eigen_vectors[i];

                    Assert.IsTrue((matrix_scaled * eigen_vector - eigen_value * eigen_vector).Norm < 1e-2);
                }
            }
        }

        [TestMethod()]
        public void EigenVectorEyeN8Test() {
            Matrix<Pow2.N8> matrix = new double[,] { { 1, 0, 0 }, { 0, 1, 0 }, { 0, 0, 1 } };

            (MultiPrecision<Pow2.N8>[] eigen_values, Vector<Pow2.N8>[] eigen_vectors) = Matrix<Pow2.N8>.EigenValueVectors(matrix);

            Assert.AreEqual(new Matrix<Pow2.N8>(new double[,] { { 1, 0, 0 }, { 0, 1, 0 }, { 0, 0, 1 } }), matrix);

            for (int i = 0; i < matrix.Size; i++) {
                MultiPrecision<Pow2.N8> eigen_value = eigen_values[i];
                Vector<Pow2.N8> eigen_vector = eigen_vectors[i];

                Assert.IsTrue((matrix * eigen_vector - eigen_value * eigen_vector).Norm < 1e-30);
            }
        }

        [TestMethod()]
        public void EigenVectorEyeEpsN8Test() {
            Matrix<Pow2.N8> matrix = new double[,] { { 1, 1e-60, 0 }, { 0, 1, 2e-60 }, { -1e-60, 1e-60, 1 } };

            (MultiPrecision<Pow2.N8>[] eigen_values, Vector<Pow2.N8>[] eigen_vectors) = Matrix<Pow2.N8>.EigenValueVectors(matrix);

            Assert.AreEqual(new double[,] { { 1, 1e-60, 0 }, { 0, 1, 2e-60 }, { -1e-60, 1e-60, 1 } }, matrix);

            for (int i = 0; i < matrix.Size; i++) {
                MultiPrecision<Pow2.N8> eigen_value = eigen_values[i];
                Vector<Pow2.N8> eigen_vector = eigen_vectors[i];

                Assert.IsTrue((matrix * eigen_vector - eigen_value * eigen_vector).Norm < 1e-30);
            }
        }
    }
}