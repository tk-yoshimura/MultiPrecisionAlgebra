using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;
using System;
using System.Numerics;

namespace MultiPrecisionAlgebra.Tests {
    [TestClass()]
    public class MatrixTests {
        [TestMethod()]
        public void MatrixTest() {
            Matrix<Pow2.N4> matrix1 = new(new double[,] { { 1, 2, 3 }, { 4, 5, 6 } });
            Matrix<Pow2.N4> matrix2 = Matrix<Pow2.N4>.Zero(3, 2);

            Assert.AreEqual(1, matrix1[0, 0]);
            Assert.AreEqual(2, matrix1[0, 1]);
            Assert.AreEqual(3, matrix1[0, 2]);
            Assert.AreEqual(4, matrix1[1, 0]);
            Assert.AreEqual(5, matrix1[1, 1]);
            Assert.AreEqual(6, matrix1[1, 2]);

            Assert.AreEqual(1, matrix1[^2, ^3]);
            Assert.AreEqual(2, matrix1[^2, ^2]);
            Assert.AreEqual(3, matrix1[^2, ^1]);
            Assert.AreEqual(4, matrix1[^1, ^3]);
            Assert.AreEqual(5, matrix1[^1, ^2]);
            Assert.AreEqual(6, matrix1[^1, ^1]);

            matrix1[0, 0] = 4;
            matrix1[0, 1] = 5;
            matrix1[^2, 2] = 6;
            matrix1[1, ^3] = 1;
            matrix1[^1, 1] = 2;
            matrix1[1, ^1] = 3;

            Assert.AreEqual(4, matrix1[0, 0]);
            Assert.AreEqual(5, matrix1[0, 1]);
            Assert.AreEqual(6, matrix1[^2, 2]);
            Assert.AreEqual(1, matrix1[1, ^3]);
            Assert.AreEqual(2, matrix1[^1, 1]);
            Assert.AreEqual(3, matrix1[1, ^1]);

            Assert.AreEqual(2, matrix1.Rows);
            Assert.AreEqual(3, matrix1.Columns);

            Assert.AreEqual(0, matrix2[0, 0]);
            Assert.AreEqual(0, matrix2[0, 1]);
            Assert.AreEqual(0, matrix2[1, 0]);
            Assert.AreEqual(0, matrix2[1, 1]);
            Assert.AreEqual(0, matrix2[2, 0]);
            Assert.AreEqual(0, matrix2[2, 1]);

            Assert.AreEqual(3, matrix2.Rows);
            Assert.AreEqual(2, matrix2.Columns);

            Assert.AreEqual(2, Matrix<Pow2.N4>.Zero(2, 2).Size);

            Assert.ThrowsException<InvalidOperationException>(() => {
                int n = Matrix<Pow2.N4>.Zero(2, 3).Size;
            });

            string str = string.Empty;
            foreach ((int row_index, int col_index, MultiPrecision<Pow2.N4> val) in matrix1) {
                str += $"({row_index},{col_index},{val}),";
            }

            Assert.AreEqual("(0,0,4),(0,1,5),(0,2,6),(1,0,1),(1,1,2),(1,2,3),", str);
        }

        [TestMethod()]
        public void RangeIndexerGetterTest() {
            Matrix<Pow2.N4> matrix = new(new double[,] { { 1, 2, 3, 4, 5 }, { 6, 7, 8, 9, 10 }, { 11, 12, 13, 14, 15 }, { 16, 17, 18, 19, 20 } });

            Assert.AreEqual(new Matrix<Pow2.N4>(new double[,] { { 1, 2, 3, 4, 5 }, { 6, 7, 8, 9, 10 }, { 11, 12, 13, 14, 15 }, { 16, 17, 18, 19, 20 } }), matrix[.., ..]);

            Assert.AreEqual(new Matrix<Pow2.N4>(new double[,] { { 6, 7, 8, 9, 10 }, { 11, 12, 13, 14, 15 }, { 16, 17, 18, 19, 20 } }), matrix[1.., ..]);
            Assert.AreEqual(new Matrix<Pow2.N4>(new double[,] { { 11, 12, 13, 14, 15 }, { 16, 17, 18, 19, 20 } }), matrix[2.., ..]);
            Assert.AreEqual(new Matrix<Pow2.N4>(new double[,] { { 1, 2, 3, 4, 5 }, { 6, 7, 8, 9, 10 }, { 11, 12, 13, 14, 15 } }), matrix[..^1, ..]);
            Assert.AreEqual(new Matrix<Pow2.N4>(new double[,] { { 1, 2, 3, 4, 5 }, { 6, 7, 8, 9, 10 }, { 11, 12, 13, 14, 15 } }), matrix[..3, ..]);
            Assert.AreEqual(new Matrix<Pow2.N4>(new double[,] { { 1, 2, 3, 4, 5 }, { 6, 7, 8, 9, 10 } }), matrix[..^2, ..]);
            Assert.AreEqual(new Matrix<Pow2.N4>(new double[,] { { 1, 2, 3, 4, 5 }, { 6, 7, 8, 9, 10 } }), matrix[..2, ..]);

            Assert.AreEqual(new Matrix<Pow2.N4>(new double[,] { { 11, 12, 13, 14, 15 } }), matrix[2..3, ..]);
            Assert.AreEqual(new Matrix<Pow2.N4>(new double[,] { { 11, 12, 13, 14, 15 } }), matrix[2..^1, ..]);

            Assert.AreEqual(new Matrix<Pow2.N4>(new double[,] { { 2, 3, 4, 5 }, { 7, 8, 9, 10 }, { 12, 13, 14, 15 }, { 17, 18, 19, 20 } }), matrix[.., 1..]);
            Assert.AreEqual(new Matrix<Pow2.N4>(new double[,] { { 3, 4, 5 }, { 8, 9, 10 }, { 13, 14, 15 }, { 18, 19, 20 } }), matrix[.., 2..]);
            Assert.AreEqual(new Matrix<Pow2.N4>(new double[,] { { 1, 2, 3, 4 }, { 6, 7, 8, 9 }, { 11, 12, 13, 14 }, { 16, 17, 18, 19 } }), matrix[.., ..^1]);
            Assert.AreEqual(new Matrix<Pow2.N4>(new double[,] { { 1, 2, 3, 4 }, { 6, 7, 8, 9 }, { 11, 12, 13, 14 }, { 16, 17, 18, 19 } }), matrix[.., ..4]);
            Assert.AreEqual(new Matrix<Pow2.N4>(new double[,] { { 1, 2, 3 }, { 6, 7, 8 }, { 11, 12, 13 }, { 16, 17, 18 } }), matrix[.., ..^2]);
            Assert.AreEqual(new Matrix<Pow2.N4>(new double[,] { { 1, 2, 3 }, { 6, 7, 8 }, { 11, 12, 13 }, { 16, 17, 18 } }), matrix[.., ..3]);

            Assert.AreEqual(new Matrix<Pow2.N4>(new double[,] { { 2, 3, 4 }, { 7, 8, 9 }, { 12, 13, 14 }, { 17, 18, 19 } }), matrix[.., 1..4]);
            Assert.AreEqual(new Matrix<Pow2.N4>(new double[,] { { 2, 3, 4 }, { 7, 8, 9 }, { 12, 13, 14 }, { 17, 18, 19 } }), matrix[.., 1..^1]);

            Assert.AreEqual(new Matrix<Pow2.N4>(new double[,] { { 7, 8, 9 }, { 12, 13, 14 } }), matrix[1..^1, 1..^1]);

            Assert.AreEqual(new Vector<Pow2.N4>(6, 7, 8, 9, 10), matrix[1, ..]);
            Assert.AreEqual(new Vector<Pow2.N4>(7, 8, 9, 10), matrix[1, 1..]);
            Assert.AreEqual(new Vector<Pow2.N4>(6, 7, 8, 9), matrix[1, ..^1]);
            Assert.AreEqual(new Vector<Pow2.N4>(7, 8, 9), matrix[1, 1..^1]);

            Assert.AreEqual(new Vector<Pow2.N4>(6, 7, 8, 9, 10), matrix[^3, ..]);
            Assert.AreEqual(new Vector<Pow2.N4>(7, 8, 9, 10), matrix[^3, 1..]);
            Assert.AreEqual(new Vector<Pow2.N4>(6, 7, 8, 9), matrix[^3, ..^1]);
            Assert.AreEqual(new Vector<Pow2.N4>(7, 8, 9), matrix[^3, 1..^1]);

            Assert.AreEqual(new Vector<Pow2.N4>(3, 8, 13, 18), matrix[.., 2]);
            Assert.AreEqual(new Vector<Pow2.N4>(8, 13, 18), matrix[1.., 2]);
            Assert.AreEqual(new Vector<Pow2.N4>(3, 8, 13), matrix[..^1, 2]);
            Assert.AreEqual(new Vector<Pow2.N4>(8, 13), matrix[1..^1, 2]);

            Assert.AreEqual(new Vector<Pow2.N4>(3, 8, 13, 18), matrix[.., ^3]);
            Assert.AreEqual(new Vector<Pow2.N4>(8, 13, 18), matrix[1.., ^3]);
            Assert.AreEqual(new Vector<Pow2.N4>(3, 8, 13), matrix[..^1, ^3]);
            Assert.AreEqual(new Vector<Pow2.N4>(8, 13), matrix[1..^1, ^3]);
        }

        [TestMethod()]
        public void RangeIndexerSetterTest() {
            Matrix<Pow2.N4> matrix_src = new(new double[,] { { 1, 2, 3, 4, 5 }, { 6, 7, 8, 9, 10 }, { 11, 12, 13, 14, 15 }, { 16, 17, 18, 19, 20 } });
            Matrix<Pow2.N4> matrix_dst;

            matrix_dst = Matrix<Pow2.N4>.Zero(matrix_src.Rows, matrix_src.Columns);
            matrix_dst[.., ..] = matrix_src;
            Assert.AreEqual(new Matrix<Pow2.N4>(new double[,] { { 1, 2, 3, 4, 5 }, { 6, 7, 8, 9, 10 }, { 11, 12, 13, 14, 15 }, { 16, 17, 18, 19, 20 } }), matrix_dst);

            matrix_dst = Matrix<Pow2.N4>.Zero(matrix_src.Rows, matrix_src.Columns);
            matrix_dst[1.., ..] = matrix_src[1.., ..];
            Assert.AreEqual(new Matrix<Pow2.N4>(new double[,] { { 0, 0, 0, 0, 0 }, { 6, 7, 8, 9, 10 }, { 11, 12, 13, 14, 15 }, { 16, 17, 18, 19, 20 } }), matrix_dst);

            matrix_dst = Matrix<Pow2.N4>.Zero(matrix_src.Rows, matrix_src.Columns);
            matrix_dst[2.., ..] = matrix_src[2.., ..];
            Assert.AreEqual(new Matrix<Pow2.N4>(new double[,] { { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 11, 12, 13, 14, 15 }, { 16, 17, 18, 19, 20 } }), matrix_dst);

            matrix_dst = Matrix<Pow2.N4>.Zero(matrix_src.Rows, matrix_src.Columns);
            matrix_dst[..^1, ..] = matrix_src[..^1, ..];
            Assert.AreEqual(new Matrix<Pow2.N4>(new double[,] { { 1, 2, 3, 4, 5 }, { 6, 7, 8, 9, 10 }, { 11, 12, 13, 14, 15 }, { 0, 0, 0, 0, 0 } }), matrix_dst);

            matrix_dst = Matrix<Pow2.N4>.Zero(matrix_src.Rows, matrix_src.Columns);
            matrix_dst[..3, ..] = matrix_src[..3, ..];
            Assert.AreEqual(new Matrix<Pow2.N4>(new double[,] { { 1, 2, 3, 4, 5 }, { 6, 7, 8, 9, 10 }, { 11, 12, 13, 14, 15 }, { 0, 0, 0, 0, 0 } }), matrix_dst);

            matrix_dst = Matrix<Pow2.N4>.Zero(matrix_src.Rows, matrix_src.Columns);
            matrix_dst[..^2, ..] = matrix_src[..^2, ..];
            Assert.AreEqual(new Matrix<Pow2.N4>(new double[,] { { 1, 2, 3, 4, 5 }, { 6, 7, 8, 9, 10 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 } }), matrix_dst);

            matrix_dst = Matrix<Pow2.N4>.Zero(matrix_src.Rows, matrix_src.Columns);
            matrix_dst[..2, ..] = matrix_src[..2, ..];
            Assert.AreEqual(new Matrix<Pow2.N4>(new double[,] { { 1, 2, 3, 4, 5 }, { 6, 7, 8, 9, 10 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 } }), matrix_dst);

            matrix_dst = Matrix<Pow2.N4>.Zero(matrix_src.Rows, matrix_src.Columns);
            matrix_dst[2..3, ..] = matrix_src[2..3, ..];
            Assert.AreEqual(new Matrix<Pow2.N4>(new double[,] { { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 11, 12, 13, 14, 15 }, { 0, 0, 0, 0, 0 } }), matrix_dst);

            matrix_dst = Matrix<Pow2.N4>.Zero(matrix_src.Rows, matrix_src.Columns);
            matrix_dst[2..^1, ..] = matrix_src[2..^1, ..];
            Assert.AreEqual(new Matrix<Pow2.N4>(new double[,] { { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 11, 12, 13, 14, 15 }, { 0, 0, 0, 0, 0 } }), matrix_dst);

            matrix_dst = Matrix<Pow2.N4>.Zero(matrix_src.Rows, matrix_src.Columns);
            matrix_dst[.., 1..] = matrix_src[.., 1..];
            Assert.AreEqual(new Matrix<Pow2.N4>(new double[,] { { 0, 2, 3, 4, 5 }, { 0, 7, 8, 9, 10 }, { 0, 12, 13, 14, 15 }, { 0, 17, 18, 19, 20 } }), matrix_dst);

            matrix_dst = Matrix<Pow2.N4>.Zero(matrix_src.Rows, matrix_src.Columns);
            matrix_dst[.., 2..] = matrix_src[.., 2..];
            Assert.AreEqual(new Matrix<Pow2.N4>(new double[,] { { 0, 0, 3, 4, 5 }, { 0, 0, 8, 9, 10 }, { 0, 0, 13, 14, 15 }, { 0, 0, 18, 19, 20 } }), matrix_dst);

            matrix_dst = Matrix<Pow2.N4>.Zero(matrix_src.Rows, matrix_src.Columns);
            matrix_dst[.., ..^1] = matrix_src[.., ..^1];
            Assert.AreEqual(new Matrix<Pow2.N4>(new double[,] { { 1, 2, 3, 4, 0 }, { 6, 7, 8, 9, 0 }, { 11, 12, 13, 14, 0 }, { 16, 17, 18, 19, 0 } }), matrix_dst);

            matrix_dst = Matrix<Pow2.N4>.Zero(matrix_src.Rows, matrix_src.Columns);
            matrix_dst[.., ..4] = matrix_src[.., ..4];
            Assert.AreEqual(new Matrix<Pow2.N4>(new double[,] { { 1, 2, 3, 4, 0 }, { 6, 7, 8, 9, 0 }, { 11, 12, 13, 14, 0 }, { 16, 17, 18, 19, 0 } }), matrix_dst);

            matrix_dst = Matrix<Pow2.N4>.Zero(matrix_src.Rows, matrix_src.Columns);
            matrix_dst[.., ..^2] = matrix_src[.., ..^2];
            Assert.AreEqual(new Matrix<Pow2.N4>(new double[,] { { 1, 2, 3, 0, 0 }, { 6, 7, 8, 0, 0 }, { 11, 12, 13, 0, 0 }, { 16, 17, 18, 0, 0 } }), matrix_dst);

            matrix_dst = Matrix<Pow2.N4>.Zero(matrix_src.Rows, matrix_src.Columns);
            matrix_dst[.., ..3] = matrix_src[.., ..3];
            Assert.AreEqual(new Matrix<Pow2.N4>(new double[,] { { 1, 2, 3, 0, 0 }, { 6, 7, 8, 0, 0 }, { 11, 12, 13, 0, 0 }, { 16, 17, 18, 0, 0 } }), matrix_dst);

            matrix_dst = Matrix<Pow2.N4>.Zero(matrix_src.Rows, matrix_src.Columns);
            matrix_dst[.., 1..4] = matrix_src[.., 1..4];
            Assert.AreEqual(new Matrix<Pow2.N4>(new double[,] { { 0, 2, 3, 4, 0 }, { 0, 7, 8, 9, 0 }, { 0, 12, 13, 14, 0 }, { 0, 17, 18, 19, 0 } }), matrix_dst);

            matrix_dst = Matrix<Pow2.N4>.Zero(matrix_src.Rows, matrix_src.Columns);
            matrix_dst[.., 1..^1] = matrix_src[.., 1..^1];
            Assert.AreEqual(new Matrix<Pow2.N4>(new double[,] { { 0, 2, 3, 4, 0 }, { 0, 7, 8, 9, 0 }, { 0, 12, 13, 14, 0 }, { 0, 17, 18, 19, 0 } }), matrix_dst);

            matrix_dst = Matrix<Pow2.N4>.Zero(matrix_src.Rows, matrix_src.Columns);
            matrix_dst[1..^1, 1..^1] = matrix_src[1..^1, 1..^1];
            Assert.AreEqual(new Matrix<Pow2.N4>(new double[,] { { 0, 0, 0, 0, 0 }, { 0, 7, 8, 9, 0 }, { 0, 12, 13, 14, 0 }, { 0, 0, 0, 0, 0 } }), matrix_dst);

            matrix_dst = Matrix<Pow2.N4>.Zero(matrix_src.Rows, matrix_src.Columns);
            matrix_dst[0..^2, 0..^2] = matrix_src[1..^1, 1..^1];
            Assert.AreEqual(new Matrix<Pow2.N4>(new double[,] { { 7, 8, 9, 0, 0 }, { 12, 13, 14, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 } }), matrix_dst);

            matrix_dst = matrix_src.Copy();
            matrix_dst[1, ..] = new Vector<Pow2.N4>(-1, -2, -3, -4, -5);
            Assert.AreEqual(new Matrix<Pow2.N4>(new double[,] { { 1, 2, 3, 4, 5 }, { -1, -2, -3, -4, -5 }, { 11, 12, 13, 14, 15 }, { 16, 17, 18, 19, 20 } }), matrix_dst);

            matrix_dst = matrix_src.Copy();
            matrix_dst[^2, ..] = new Vector<Pow2.N4>(-1, -2, -3, -4, -5);
            Assert.AreEqual(new Matrix<Pow2.N4>(new double[,] { { 1, 2, 3, 4, 5 }, { 6, 7, 8, 9, 10 }, { -1, -2, -3, -4, -5 }, { 16, 17, 18, 19, 20 } }), matrix_dst);

            matrix_dst = matrix_src.Copy();
            matrix_dst[.., 1] = new Vector<Pow2.N4>(-1, -2, -3, -4);
            Assert.AreEqual(new Matrix<Pow2.N4>(new double[,] { { 1, -1, 3, 4, 5 }, { 6, -2, 8, 9, 10 }, { 11, -3, 13, 14, 15 }, { 16, -4, 18, 19, 20 } }), matrix_dst);

            matrix_dst = matrix_src.Copy();
            matrix_dst[.., ^2] = new Vector<Pow2.N4>(-1, -2, -3, -4);
            Assert.AreEqual(new Matrix<Pow2.N4>(new double[,] { { 1, 2, 3, -1, 5 }, { 6, 7, 8, -2, 10 }, { 11, 12, 13, -3, 15 }, { 16, 17, 18, -4, 20 } }), matrix_dst);

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => {
                matrix_dst = Matrix<Pow2.N4>.Zero(matrix_src.Rows, matrix_src.Columns);
                matrix_dst[0..^2, 0..^2] = matrix_src[1..^2, 1..^1];
            });

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => {
                matrix_dst = Matrix<Pow2.N4>.Zero(matrix_src.Rows, matrix_src.Columns);
                matrix_dst[0..^2, 0..^2] = matrix_src[1.., 1..^1];
            });

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => {
                matrix_dst = Matrix<Pow2.N4>.Zero(matrix_src.Rows, matrix_src.Columns);
                matrix_dst[0..^2, 0..^2] = matrix_src[1..^1, 1..^2];
            });

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => {
                matrix_dst = Matrix<Pow2.N4>.Zero(matrix_src.Rows, matrix_src.Columns);
                matrix_dst[0..^2, 0..^2] = matrix_src[1..^1, 1..];
            });
        }

        [TestMethod()]
        public void NormTest() {
            Matrix<Pow2.N4> matrix = new(new double[,] { { 1, 2 }, { 3, 4 } });

            Assert.AreEqual(MultiPrecision<Pow2.N4>.Sqrt(30), matrix.Norm);
        }

        [TestMethod()]
        public void TransposeTest() {
            Matrix<Pow2.N4> matrix1 = new(new double[,] { { 1, 2, 3 }, { 4, 5, 6 } });
            Matrix<Pow2.N4> matrix2 = matrix1.Transpose;
            Matrix<Pow2.N4> matrix3 = matrix2.Transpose;

            Assert.AreEqual(new Matrix<Pow2.N4>(new double[,] { { 1, 4 }, { 2, 5 }, { 3, 6 } }), matrix2);
            Assert.AreEqual(matrix1, matrix3);
        }

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

            Assert.AreEqual(matrix1.Rows, matrix1.Transpose.Columns);
            Assert.AreEqual(matrix1.Columns, matrix1.Transpose.Rows);

            Assert.AreEqual(matrix2.Rows, matrix2.Transpose.Columns);
            Assert.AreEqual(matrix2.Columns, matrix2.Transpose.Rows);

            Assert.AreEqual(matrix3.Rows, matrix3.Transpose.Columns);
            Assert.AreEqual(matrix3.Columns, matrix3.Transpose.Rows);

            Assert.IsTrue((matrix1 * matrix1.Inverse * matrix1 - matrix1).Norm < 1e-35);
            Assert.IsTrue((matrix2 * matrix2.Inverse * matrix2 - matrix2).Norm < 1e-35);
            Assert.IsTrue((matrix3 * matrix3.Inverse * matrix3 - matrix3).Norm < 1e-35);

            Assert.IsTrue((matrix1.Inverse * matrix1 * matrix1.Inverse - matrix1.Inverse).Norm < 1e-35);
            Assert.IsTrue((matrix2.Inverse * matrix2 * matrix2.Inverse - matrix2.Inverse).Norm < 1e-35);
            Assert.IsTrue((matrix3.Inverse * matrix3 * matrix3.Inverse - matrix3.Inverse).Norm < 1e-35);

            Assert.IsTrue(((matrix1 * matrix1.Inverse).Transpose - matrix1 * matrix1.Inverse).Norm < 1e-35);
            Assert.IsTrue(((matrix2 * matrix2.Inverse).Transpose - matrix2 * matrix2.Inverse).Norm < 1e-35);
            Assert.IsTrue(((matrix3 * matrix3.Inverse).Transpose - matrix3 * matrix3.Inverse).Norm < 1e-35);

            Assert.IsTrue(((matrix1.Inverse * matrix1).Transpose - matrix1.Inverse * matrix1).Norm < 1e-35);
            Assert.IsTrue(((matrix2.Inverse * matrix2).Transpose - matrix2.Inverse * matrix2).Norm < 1e-35);
            Assert.IsTrue(((matrix3.Inverse * matrix3).Transpose - matrix3.Inverse * matrix3).Norm < 1e-35);

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
        public void HorizontalTest() {
            Vector<Pow2.N4> vector = new(1, 2, 3);

            Assert.AreEqual(new Matrix<Pow2.N4>(new double[,] { { 1, 2, 3 } }), vector.Horizontal);

            Matrix<Pow2.N4> matrix = new(new double[,] { { 1, 2, 3 }, { 4, 5, 6 } });

            Assert.AreEqual(new Vector<Pow2.N4>(new MultiPrecision<Pow2.N4>[] { 1, 2, 3 }), matrix.Horizontal(0));
            Assert.AreEqual(new Vector<Pow2.N4>(new MultiPrecision<Pow2.N4>[] { 4, 5, 6 }), matrix.Horizontal(1));
        }

        [TestMethod()]
        public void VerticalTest() {
            Vector<Pow2.N4> vector = new(1, 2, 3);

            Assert.AreEqual(new Matrix<Pow2.N4>(new double[,] { { 1 }, { 2 }, { 3 } }), vector.Vertical);

            Matrix<Pow2.N4> matrix = new(new double[,] { { 1, 2, 3 }, { 4, 5, 6 } });

            Assert.AreEqual(new Vector<Pow2.N4>(new MultiPrecision<Pow2.N4>[] { 1, 4 }), matrix.Vertical(0));
            Assert.AreEqual(new Vector<Pow2.N4>(new MultiPrecision<Pow2.N4>[] { 2, 5 }), matrix.Vertical(1));
            Assert.AreEqual(new Vector<Pow2.N4>(new MultiPrecision<Pow2.N4>[] { 3, 6 }), matrix.Vertical(2));
        }

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
            Assert.AreEqual(new Matrix<Pow2.N4>(new double[,] { { -6, -6, -6 }, { 3, 3, 3 } }), matrix1 - matrix2);
            Assert.AreEqual(new Matrix<Pow2.N4>(new double[,] { { 6, 6, 6 }, { -3, -3, -3 } }), matrix2 - matrix1);
            Assert.AreEqual(new Matrix<Pow2.N4>(new double[,] { { 22, 28 }, { 49, 64 } }), matrix1 * matrix3);
            Assert.AreEqual(new Matrix<Pow2.N4>(new double[,] { { 9, 12, 15 }, { 19, 26, 33 }, { 29, 40, 51 } }), matrix3 * matrix1);

            Assert.AreEqual(new Matrix<Pow2.N4>(new double[,] { { 7, 16, 27 }, { 4, 10, 18 } }), Matrix<Pow2.N4>.ElementwiseMul(matrix1, matrix2));
            Assert.AreEqual(new Matrix<Pow2.N4>(new MultiPrecision<Pow2.N4>[,] { { 7, 4, 3 }, { 0.25, "0.4", 0.5 } }), Matrix<Pow2.N4>.ElementwiseDiv(matrix2, matrix1)); ;

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

        [TestMethod()]
        public void ZeroTest() {
            Matrix<Pow2.N4> matrix = Matrix<Pow2.N4>.Zero(2, 2);

            Assert.AreEqual(0, matrix[0, 0]);
            Assert.AreEqual(0, matrix[1, 0]);
            Assert.AreEqual(0, matrix[0, 1]);
            Assert.AreEqual(0, matrix[1, 1]);
        }

        [TestMethod()]
        public void IdentityTest() {
            Matrix<Pow2.N4> matrix = Matrix<Pow2.N4>.Identity(2);

            Assert.AreEqual(1, matrix[0, 0]);
            Assert.AreEqual(0, matrix[1, 0]);
            Assert.AreEqual(0, matrix[0, 1]);
            Assert.AreEqual(1, matrix[1, 1]);
        }

        [TestMethod()]
        public void InvalidTest() {
            Matrix<Pow2.N4> matrix = Matrix<Pow2.N4>.Invalid(2, 1);

            Assert.IsTrue(matrix[0, 0].IsNaN);
            Assert.IsTrue(matrix[1, 0].IsNaN);
        }

        [TestMethod()]
        public void IsEqualSizeTest() {
            Matrix<Pow2.N4> matrix1 = Matrix<Pow2.N4>.Zero(2, 2);
            Matrix<Pow2.N4> matrix2 = Matrix<Pow2.N4>.Zero(2, 3);

            Assert.IsTrue(Matrix<Pow2.N4>.IsEqualSize(matrix1, matrix1));
            Assert.IsFalse(Matrix<Pow2.N4>.IsEqualSize(matrix1, matrix2));
        }

        [TestMethod()]
        public void IsSquareTest() {
            Matrix<Pow2.N4> matrix1 = Matrix<Pow2.N4>.Zero(2, 2);
            Matrix<Pow2.N4> matrix2 = Matrix<Pow2.N4>.Zero(2, 3);

            Assert.IsTrue(Matrix<Pow2.N4>.IsSquare(matrix1));
            Assert.IsFalse(Matrix<Pow2.N4>.IsSquare(matrix2));
        }

        [TestMethod()]
        public void IsDiagonalTest() {
            Matrix<Pow2.N4> matrix1 = new(new double[,] { { 1, 0 }, { 0, 2 } });
            Matrix<Pow2.N4> matrix2 = new(new double[,] { { 1, 1 }, { 0, 2 } });
            Matrix<Pow2.N4> matrix3 = new(new double[,] { { 1, 0, 0 }, { 0, 2, 0 } });
            Matrix<Pow2.N4> matrix4 = new(new double[,] { { 1, 0 }, { 0, 2 }, { 0, 0 } });

            Assert.IsTrue(Matrix<Pow2.N4>.IsDiagonal(matrix1));
            Assert.IsFalse(Matrix<Pow2.N4>.IsDiagonal(matrix2));
            Assert.IsFalse(Matrix<Pow2.N4>.IsDiagonal(matrix3));
            Assert.IsFalse(Matrix<Pow2.N4>.IsDiagonal(matrix4));
        }

        [TestMethod()]
        public void IsZeroTest() {
            Matrix<Pow2.N4> matrix = Matrix<Pow2.N4>.Zero(2, 3);

            Assert.IsTrue(Matrix<Pow2.N4>.IsZero(matrix));

            matrix[0, 0] = 1;

            Assert.IsFalse(Matrix<Pow2.N4>.IsZero(matrix));
        }

        [TestMethod()]
        public void IsIdentityTest() {
            Matrix<Pow2.N4> matrix = Matrix<Pow2.N4>.Identity(2);

            Assert.IsTrue(Matrix<Pow2.N4>.IsIdentity(matrix));

            matrix[0, 1] = 1;

            Assert.IsFalse(Matrix<Pow2.N4>.IsIdentity(matrix));
        }

        [TestMethod()]
        public void IsSymmetricTest() {
            Matrix<Pow2.N4> matrix1 = new(new double[,] { { 1, 2 }, { 2, 3 } });
            Matrix<Pow2.N4> matrix2 = new(new double[,] { { 1, 2 }, { 3, 3 } });

            Assert.IsTrue(Matrix<Pow2.N4>.IsSymmetric(matrix1));
            Assert.IsFalse(Matrix<Pow2.N4>.IsSymmetric(matrix2));
        }

        [TestMethod()]
        public void IsValidTest() {
            Assert.IsTrue(Matrix<Pow2.N4>.IsValid(Matrix<Pow2.N4>.Zero(3, 2)));
            Assert.IsFalse(Matrix<Pow2.N4>.IsValid(Matrix<Pow2.N4>.Invalid(2, 3)));
        }

        [TestMethod()]
        public void IsRegularTest() {
            Matrix<Pow2.N4> matrix1 = new(new double[,] { { 1, 2, 3 }, { 4, 5, 6 } });
            Matrix<Pow2.N4> matrix2 = new(new double[,] { { 1, 2 }, { 3, 4 }, { 5, 6 } });
            Matrix<Pow2.N4> matrix3 = new(new double[,] { { 1, 2 }, { 3, 4 } });
            Matrix<Pow2.N4> matrix4 = new(new double[,] { { 0, 1 }, { 0, 0 } });

            Assert.IsTrue(Matrix<Pow2.N4>.IsRegular(matrix1));
            Assert.IsTrue(Matrix<Pow2.N4>.IsRegular(matrix2));
            Assert.IsTrue(Matrix<Pow2.N4>.IsRegular(matrix3));
            Assert.IsFalse(Matrix<Pow2.N4>.IsRegular(matrix4));
            Assert.IsFalse(Matrix<Pow2.N4>.IsRegular(Matrix<Pow2.N4>.Zero(2, 2)));
            Assert.IsTrue(Matrix<Pow2.N4>.IsRegular(Matrix<Pow2.N4>.Identity(2)));
        }

        [TestMethod()]
        public void DiagonalsTest() {
            Matrix<Pow2.N4> matrix = new(new double[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } });

            MultiPrecision<Pow2.N4>[] diagonals = matrix.Diagonals;

            Assert.AreEqual(1, diagonals[0]);
            Assert.AreEqual(5, diagonals[1]);
            Assert.AreEqual(9, diagonals[2]);
        }

        [TestMethod()]
        public void LUDecomposeTest() {
            Matrix<Pow2.N4> matrix = new(new double[,] { { 2, 3, 1, 2 }, { 4, 1, 3, -2 }, { 2, 2, -3, 1 }, { 1, -3, 2, 4 } });

            (Matrix<Pow2.N4> lower, Matrix<Pow2.N4> upper) = matrix.LUDecompose();

            foreach (var diagonal in lower.Diagonals) {
                Assert.AreEqual(1, diagonal);
            }

            Assert.IsTrue((matrix - lower * upper).Norm < 1e-35);
        }

        [TestMethod()]
        public void QRDecomposeTest() {
            Matrix<Pow2.N4> matrix = new(new double[,] { { 12, -51, 4 }, { 6, 167, -68 }, { -4, 24, -41 } });

            (Matrix<Pow2.N4> q, Matrix<Pow2.N4> r) = matrix.QRDecompose();

            Assert.IsTrue((matrix - q * r).Norm < 1e-12);
            Assert.IsTrue((q * q.Transpose - Matrix<Pow2.N4>.Identity(matrix.Size)).Norm < 1e-35);
        }

        [TestMethod()]
        public void CalculateEigenValuesTest() {
            Matrix<Pow2.N4> matrix = new(new double[,] { { 1, 2 }, { 4, 5 } });
            MultiPrecision<Pow2.N4>[] eigen_values = matrix.CalculateEigenValues();

            Assert.IsTrue(MultiPrecision<Pow2.N4>.Abs(eigen_values[0] - (3 + 2 * MultiPrecision<Pow2.N4>.Sqrt(3))) < 1e-35);
            Assert.IsTrue(MultiPrecision<Pow2.N4>.Abs(eigen_values[1] - (3 - 2 * MultiPrecision<Pow2.N4>.Sqrt(3))) < 1e-35);
        }

        [TestMethod()]
        public void CalculateEigenVectorTest() {
            Matrix<Pow2.N4> matrix = new(new double[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 8, 7, 9 } });

            (MultiPrecision<Pow2.N4>[] eigen_values, Vector<Pow2.N4>[] eigen_vectors) = matrix.CalculateEigenValueVectors();

            for (int i = 0; i < matrix.Size; i++) {
                MultiPrecision<Pow2.N4> eigen_value = eigen_values[i];
                Vector<Pow2.N4> eigen_vector = eigen_vectors[i];

                Assert.IsTrue((matrix * eigen_vector - eigen_value * eigen_vector).Norm < 1e-15);
            }
        }

        [TestMethod()]
        public void DetTest() {
            Matrix<Pow2.N4> matrix1 = new(new double[,] { { 1, 2 }, { 3, 4 } });
            Matrix<Pow2.N4> matrix2 = new(new double[,] { { 1, 2 }, { 2, 4 } });

            Assert.AreEqual(-2, matrix1.Det);
            Assert.AreEqual(0, matrix2.Det);
        }

        [TestMethod()]
        public void TraceTest() {
            Matrix<Pow2.N4> matrix1 = new(new double[,] { { 1, 2 }, { 3, 4 } });
            Matrix<Pow2.N4> matrix2 = new(new double[,] { { 1, 2 }, { 2, 4 } });

            Assert.AreEqual(-1, matrix1.Trace);
            Assert.AreEqual(1, matrix2.Trace);
        }

        [TestMethod()]
        public void CopyTest() {
            Matrix<Pow2.N4> matrix1 = Matrix<Pow2.N4>.Zero(2, 2);
            Matrix<Pow2.N4> matrix2 = matrix1.Copy();

            matrix2[1, 1] = 1;

            Assert.AreEqual(0, matrix1[1, 1]);
            Assert.AreEqual(1, matrix2[1, 1]);
        }

        [TestMethod()]
        public void ToStringTest() {
            Matrix<Pow2.N4> matrix1 = new(new double[,] { { 1, 2, 3 }, { 4, 5, 6 } });
            Matrix<Pow2.N4> matrix2 = new(new double[,] { { 1, 2, 3 } });
            Matrix<Pow2.N4> matrix3 = new(new double[,] { { 1 }, { 2 }, { 3 } });
            Matrix<Pow2.N4> matrix4 = Matrix<Pow2.N4>.Invalid(2, 2);

            Assert.AreEqual("[ [ 1, 2, 3 ], [ 4, 5, 6 ] ]", matrix1.ToString());
            Assert.AreEqual("[ [ 1, 2, 3 ] ]", matrix2.ToString());
            Assert.AreEqual("[ [ 1 ], [ 2 ], [ 3 ] ]", matrix3.ToString());
            Assert.AreEqual("Invalid Matrix", matrix4.ToString());
        }
    }
}