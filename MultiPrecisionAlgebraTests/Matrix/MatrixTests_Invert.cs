using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;
using MultiPrecisionAlgebra;
using System;

namespace MultiPrecisionAlgebraTests {
    public partial class MatrixTests {
        [TestMethod()]
        public void InverseTest() {
            Matrix<Pow2.N4> matrix1 = new(new double[,] { { 1, 2, 3 }, { 4, 5, 6 } });
            Matrix<Pow2.N4> matrix2 = new(new double[,] { { 1, 2 }, { 3, 4 }, { 5, 6 } });
            Matrix<Pow2.N4> matrix3 = new(new double[,] { { 1, 2 }, { 3, 4 } });
            Matrix<Pow2.N4> matrix4 = new(new double[,] { { 0, 1 }, { 0, 0 } });
            Matrix<Pow2.N4> matrix5 = new(new double[,] { { 2, 1, 1, 2 }, { 4, 2, 3, 1 }, { -2, -2, 0, -1 }, { 1, 1, 2, 6 } });
            Matrix<Pow2.N4> matrix6 = new(new double[,] {
                { 1, 0, 0, 0, 1, 0, 0, 0 },
                { 0, 1, 0, 0, 1, 1, 0, 0 },
                { 0, 0, 1, 0, 2, 1, 1, 0 },
                { 0, 0, 0, 1, 4, 2, 1, 1 },
                { 0, 0, 0, 0, 5, 4, 2, 1 },
                { 0, 0, 0, 0, 7, 5, 4, 2 },
                { 0, 0, 0, 0, 6, 7, 5, 4 },
                { 0, 0, 0, 0, 8, 6, 7, 5 },
            });

            Assert.AreEqual(matrix1.Rows, matrix1.T.Columns);
            Assert.AreEqual(matrix1.Columns, matrix1.T.Rows);

            Assert.AreEqual(matrix2.Rows, matrix2.T.Columns);
            Assert.AreEqual(matrix2.Columns, matrix2.T.Rows);

            Assert.AreEqual(matrix3.Rows, matrix3.T.Columns);
            Assert.AreEqual(matrix3.Columns, matrix3.T.Rows);

            Assert.IsTrue((matrix1 * matrix1.Inverse * matrix1 - matrix1).Norm < 1e-35);
            Assert.IsTrue((matrix2 * matrix2.Inverse * matrix2 - matrix2).Norm < 1e-35);
            Assert.IsTrue((matrix3 * matrix3.Inverse * matrix3 - matrix3).Norm < 1e-35);

            Assert.AreEqual(new Matrix<Pow2.N4>(new double[,] { { 1, 2, 3 }, { 4, 5, 6 } }), matrix1);
            Assert.AreEqual(new Matrix<Pow2.N4>(new double[,] { { 1, 2 }, { 3, 4 }, { 5, 6 } }), matrix2);
            Assert.AreEqual(new Matrix<Pow2.N4>(new double[,] { { 1, 2 }, { 3, 4 } }), matrix3);

            Assert.IsTrue((matrix1.Inverse * matrix1 * matrix1.Inverse - matrix1.Inverse).Norm < 1e-35);
            Assert.IsTrue((matrix2.Inverse * matrix2 * matrix2.Inverse - matrix2.Inverse).Norm < 1e-35);
            Assert.IsTrue((matrix3.Inverse * matrix3 * matrix3.Inverse - matrix3.Inverse).Norm < 1e-35);

            Assert.IsTrue(((matrix1 * matrix1.Inverse).T - matrix1 * matrix1.Inverse).Norm < 1e-35);
            Assert.IsTrue(((matrix2 * matrix2.Inverse).T - matrix2 * matrix2.Inverse).Norm < 1e-35);
            Assert.IsTrue(((matrix3 * matrix3.Inverse).T - matrix3 * matrix3.Inverse).Norm < 1e-35);

            Assert.IsTrue(((matrix1.Inverse * matrix1).T - matrix1.Inverse * matrix1).Norm < 1e-35);
            Assert.IsTrue(((matrix2.Inverse * matrix2).T - matrix2.Inverse * matrix2).Norm < 1e-35);
            Assert.IsTrue(((matrix3.Inverse * matrix3).T - matrix3.Inverse * matrix3).Norm < 1e-35);

            Assert.IsFalse(Matrix<Pow2.N4>.IsValid(matrix4.Inverse));
            Assert.IsFalse(Matrix<Pow2.N4>.IsValid(Matrix<Pow2.N4>.Zero(2, 2).Inverse));
            Assert.IsTrue(Matrix<Pow2.N4>.IsIdentity(Matrix<Pow2.N4>.Identity(2).Inverse));

            Assert.IsTrue((matrix5.Inverse.Inverse - matrix5).Norm < 1e-35);
            Assert.IsTrue((matrix6.Inverse.Inverse - matrix6).Norm < 1e-35);

            Assert.IsTrue(((matrix5 * "1e+1000").Inverse.Inverse - (matrix5 * "1e+1000")).Norm < "1e+965");
            Assert.IsTrue(((matrix6 * "1e+1000").Inverse.Inverse - (matrix6 * "1e+1000")).Norm < "1e+965");

            Assert.IsTrue(((matrix5 * "1e-1000").Inverse.Inverse - (matrix5 * "1e-1000")).Norm < "1e-1035");
            Assert.IsTrue(((matrix6 * "1e-1000").Inverse.Inverse - (matrix6 * "1e-1000")).Norm < "1e-1035");
        }

        [TestMethod()]
        public void Inverse3x3Test() {
            Matrix<Pow2.N4> matrix = new double[,] { { 4, -5, 6 }, { -10, 8, -1 }, { -3, -10, -1 } };
            Matrix<Pow2.N4> matrix_inv = matrix.Inverse;

            Assert.IsTrue((matrix * matrix_inv * matrix - matrix).Norm < 1e-28);
        }

        [TestMethod()]
        public void InverseCaseN4Test() {
            foreach (Matrix<Pow2.N4> matrix in MatrixTestCases<Pow2.N4>.RegularMatrixs) {
                Console.WriteLine($"test: {matrix}");

                Matrix<Pow2.N4> matrix_inv = matrix.Inverse;

                Assert.IsTrue(MultiPrecision<Pow2.N4>.Abs(matrix.Det * matrix_inv.Det - 1) < 1e-25);
                Assert.IsTrue((matrix * matrix_inv - Matrix<Pow2.N4>.Identity(matrix.Size)).Norm < 1e-28);
            }
        }

        [TestMethod()]
        public void InverseLargeCaseN4Test() {
            foreach (Matrix<Pow2.N4> matrix in MatrixTestCases<Pow2.N4>.LargeMatrixs) {
                Console.WriteLine($"test: {matrix}");

                Matrix<Pow2.N4> matrix_inv = matrix.Inverse;

                Assert.IsTrue(MultiPrecision<Pow2.N4>.Abs(matrix.Det * matrix_inv.Det - 1) < 1e-25);
                Assert.IsTrue((matrix * matrix_inv - Matrix<Pow2.N4>.Identity(matrix.Size)).Norm < 1e-28);
            }
        }

        [TestMethod()]
        public void InverseCaseN8Test() {
            foreach (Matrix<Pow2.N8> matrix in MatrixTestCases<Pow2.N8>.RegularMatrixs) {
                Console.WriteLine($"test: {matrix}");

                Matrix<Pow2.N8> matrix_inv = matrix.Inverse;

                Assert.IsTrue(MultiPrecision<Pow2.N8>.Abs(matrix.Det * matrix_inv.Det - 1) < 1e-50);
                Assert.IsTrue((matrix * matrix_inv - Matrix<Pow2.N8>.Identity(matrix.Size)).Norm < 1e-56);
            }
        }

        [TestMethod()]
        public void InverseLargeCaseN8Test() {
            foreach (Matrix<Pow2.N8> matrix in MatrixTestCases<Pow2.N8>.LargeMatrixs) {
                Console.WriteLine($"test: {matrix}");

                Matrix<Pow2.N8> matrix_inv = matrix.Inverse;

                Assert.IsTrue(MultiPrecision<Pow2.N8>.Abs(matrix.Det * matrix_inv.Det - 1) < 1e-50);
                Assert.IsTrue((matrix * matrix_inv - Matrix<Pow2.N8>.Identity(matrix.Size)).Norm < 1e-56);
            }
        }

        [TestMethod()]
        public void SolveN4Test() {
            Matrix<Pow2.N4> matrix1 = new(new double[,] { { 1, 2 }, { 3, 4 } });
            Matrix<Pow2.N4> matrix2 = new(new double[,] { { 0, 1 }, { 0, 0 } });
            Matrix<Pow2.N4> matrix3 = new(new double[,] { { 1, 2, -3 }, { 2, -1, 3 }, { -3, 2, 1 } });
            Matrix<Pow2.N4> matrix4 = new(new double[,] { { 2, 1, 1, 2 }, { 4, 2, 3, 1 }, { -2, -2, 0, -1 }, { 1, 1, 2, 6 } });
            Matrix<Pow2.N4> matrix5 = new(new double[,] {
                { 1, 0, 0, 0, 1, 0, 0, 0 },
                { 0, 1, 0, 0, 1, 1, 0, 0 },
                { 0, 0, 1, 0, 2, 1, 1, 0 },
                { 0, 0, 0, 1, 4, 2, 1, 1 },
                { 0, 0, 0, 0, 5, 4, 2, 1 },
                { 0, 0, 0, 0, 7, 5, 4, 2 },
                { 0, 0, 0, 0, 6, 7, 5, 4 },
                { 0, 0, 0, 0, 8, 6, 7, 5 },
            });

            Vector<Pow2.N4> vector1 = new(4, 3);
            Vector<Pow2.N4> vector2 = new(3, 1);
            Vector<Pow2.N4> vector3 = new(5, 4, 1);
            Vector<Pow2.N4> vector4 = new(1, -2, 3, 2);
            Vector<Pow2.N4> vector5 = new(5, -3, 1, -2, 6, 2, 4, 2);

            Assert.IsTrue((matrix1.Inverse * vector1 - Matrix<Pow2.N4>.Solve(matrix1, vector1)).Norm < 1e-35, $"{Matrix<Pow2.N4>.Solve(matrix1, vector1)}");
            Assert.IsTrue((matrix3.Inverse * vector3 - Matrix<Pow2.N4>.Solve(matrix3, vector3)).Norm < 1e-35);
            Assert.IsFalse(Vector<Pow2.N4>.IsValid(Matrix<Pow2.N4>.Solve(matrix2, vector2)));
            Assert.IsTrue((matrix4.Inverse * vector4 - Matrix<Pow2.N4>.Solve(matrix4, vector4)).Norm < 1e-35);
            Assert.IsTrue((matrix5.Inverse * vector5 - Matrix<Pow2.N4>.Solve(matrix5, vector5)).Norm < 1e-35);

            Assert.AreEqual(new Matrix<Pow2.N4>(new double[,] { { 1, 2 }, { 3, 4 } }), matrix1);
            Assert.AreEqual(new Matrix<Pow2.N4>(new double[,] { { 0, 1 }, { 0, 0 } }), matrix2);
            Assert.AreEqual(new Matrix<Pow2.N4>(new double[,] { { 1, 2, -3 }, { 2, -1, 3 }, { -3, 2, 1 } }), matrix3);

            Assert.AreEqual(new Vector<Pow2.N4>(4, 3), vector1);
            Assert.AreEqual(new Vector<Pow2.N4>(3, 1), vector2);
            Assert.AreEqual(new Vector<Pow2.N4>(5, 4, 1), vector3);

            Assert.IsTrue(((matrix4 * "1e+1000").Inverse * vector4 - Matrix<Pow2.N4>.Solve(matrix4 * "1e+1000", vector4)).Norm < "1e-1035");
            Assert.IsTrue(((matrix5 * "1e+1000").Inverse * vector5 - Matrix<Pow2.N4>.Solve(matrix5 * "1e+1000", vector5)).Norm < "1e-1035");

            Assert.IsTrue(((matrix4 * "1e-1000").Inverse * vector4 - Matrix<Pow2.N4>.Solve(matrix4 * "1e-1000", vector4)).Norm < "1e+965");
            Assert.IsTrue(((matrix5 * "1e-1000").Inverse * vector5 - Matrix<Pow2.N4>.Solve(matrix5 * "1e-1000", vector5)).Norm < "1e+965");
        }

        [TestMethod()]
        public void SolveN8Test() {
            Matrix<Pow2.N8> matrix1 = new(new double[,] { { 1, 2 }, { 3, 4 } });
            Matrix<Pow2.N8> matrix2 = new(new double[,] { { 0, 1 }, { 0, 0 } });
            Matrix<Pow2.N8> matrix3 = new(new double[,] { { 1, 2, -3 }, { 2, -1, 3 }, { -3, 2, 1 } });
            Matrix<Pow2.N8> matrix4 = new(new double[,] { { 2, 1, 1, 2 }, { 4, 2, 3, 1 }, { -2, -2, 0, -1 }, { 1, 1, 2, 6 } });
            Matrix<Pow2.N8> matrix5 = new(new double[,] {
                { 1, 0, 0, 0, 1, 0, 0, 0 },
                { 0, 1, 0, 0, 1, 1, 0, 0 },
                { 0, 0, 1, 0, 2, 1, 1, 0 },
                { 0, 0, 0, 1, 4, 2, 1, 1 },
                { 0, 0, 0, 0, 5, 4, 2, 1 },
                { 0, 0, 0, 0, 7, 5, 4, 2 },
                { 0, 0, 0, 0, 6, 7, 5, 4 },
                { 0, 0, 0, 0, 8, 6, 7, 5 },
            });

            Vector<Pow2.N8> vector1 = new(4, 3);
            Vector<Pow2.N8> vector2 = new(3, 1);
            Vector<Pow2.N8> vector3 = new(5, 4, 1);
            Vector<Pow2.N8> vector4 = new(1, -2, 3, 2);
            Vector<Pow2.N8> vector5 = new(5, -3, 1, -2, 6, 2, 4, 2);

            Assert.IsTrue((matrix1.Inverse * vector1 - Matrix<Pow2.N8>.Solve(matrix1, vector1)).Norm < 1e-70, $"{Matrix<Pow2.N8>.Solve(matrix1, vector1)}");
            Assert.IsTrue((matrix3.Inverse * vector3 - Matrix<Pow2.N8>.Solve(matrix3, vector3)).Norm < 1e-70);
            Assert.IsFalse(Vector<Pow2.N8>.IsValid(Matrix<Pow2.N8>.Solve(matrix2, vector2)));
            Assert.IsTrue((matrix4.Inverse * vector4 - Matrix<Pow2.N8>.Solve(matrix4, vector4)).Norm < 1e-70);
            Assert.IsTrue((matrix5.Inverse * vector5 - Matrix<Pow2.N8>.Solve(matrix5, vector5)).Norm < 1e-70);

            Assert.AreEqual(new Matrix<Pow2.N8>(new double[,] { { 1, 2 }, { 3, 4 } }), matrix1);
            Assert.AreEqual(new Matrix<Pow2.N8>(new double[,] { { 0, 1 }, { 0, 0 } }), matrix2);
            Assert.AreEqual(new Matrix<Pow2.N8>(new double[,] { { 1, 2, -3 }, { 2, -1, 3 }, { -3, 2, 1 } }), matrix3);

            Assert.AreEqual(new Vector<Pow2.N8>(4, 3), vector1);
            Assert.AreEqual(new Vector<Pow2.N8>(3, 1), vector2);
            Assert.AreEqual(new Vector<Pow2.N8>(5, 4, 1), vector3);

            Assert.IsTrue(((matrix4 * "1e+1000").Inverse * vector4 - Matrix<Pow2.N8>.Solve(matrix4 * "1e+1000", vector4)).Norm < "1e-1070");
            Assert.IsTrue(((matrix5 * "1e+1000").Inverse * vector5 - Matrix<Pow2.N8>.Solve(matrix5 * "1e+1000", vector5)).Norm < "1e-1070");

            Assert.IsTrue(((matrix4 * "1e-1000").Inverse * vector4 - Matrix<Pow2.N8>.Solve(matrix4 * "1e-1000", vector4)).Norm < "1e+930");
            Assert.IsTrue(((matrix5 * "1e-1000").Inverse * vector5 - Matrix<Pow2.N8>.Solve(matrix5 * "1e-1000", vector5)).Norm < "1e+930");
        }
    }
}