using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;
using MultiPrecisionAlgebra;
using System;

namespace MultiPrecisionAlgebraTests {
    [TestClass()]
    public partial class MatrixTests {
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

            Assert.ThrowsException<ArithmeticException>(() => {
                int n = Matrix<Pow2.N4>.Zero(2, 3).Size;
            });

            string str = string.Empty;
            foreach ((int row_index, int col_index, MultiPrecision<Pow2.N4> val) in matrix1) {
                str += $"({row_index},{col_index},{val}),";
            }

            Assert.AreEqual("(0,0,4),(0,1,5),(0,2,6),(1,0,1),(1,1,2),(1,2,3),", str);
        }



        [TestMethod()]
        public void NormTest() {
            Matrix<Pow2.N4> matrix = new(new double[,] { { 1, 2 }, { 3, 4 } });

            Assert.AreEqual(MultiPrecision<Pow2.N4>.Sqrt(30), matrix.Norm);
            Assert.AreEqual(30, matrix.SquareNorm);
        }

        [TestMethod()]
        public void SumTest() {
            Matrix<Pow2.N4> matrix = new(new double[,] { { 1, 2 }, { 3, 4 } });

            Assert.AreEqual(10, matrix.Sum);
        }

        [TestMethod()]
        public void TransposeTest() {
            Matrix<Pow2.N4> matrix1 = new(new double[,] { { 1, 2, 3 }, { 4, 5, 6 } });
            Matrix<Pow2.N4> matrix2 = matrix1.T;
            Matrix<Pow2.N4> matrix3 = matrix2.T;

            Assert.AreEqual(new Matrix<Pow2.N4>(new double[,] { { 1, 4 }, { 2, 5 }, { 3, 6 } }), matrix2);
            Assert.AreEqual(matrix1, matrix3);
        }

        [TestMethod()]
        public void HorizontalTest() {
            Vector<Pow2.N4> vector = new(1, 2, 3);

            Assert.AreEqual(new Matrix<Pow2.N4>(new double[,] { { 1, 2, 3 } }), vector.Horizontal);

            Matrix<Pow2.N4> matrix = new(new double[,] { { 1, 2, 3 }, { 4, 5, 6 } });

            Assert.AreEqual(new Vector<Pow2.N4>(1, 2, 3), matrix.Horizontal(0));
            Assert.AreEqual(new Vector<Pow2.N4>(4, 5, 6), matrix.Horizontal(1));
        }

        [TestMethod()]
        public void VerticalTest() {
            Vector<Pow2.N4> vector = new(1, 2, 3);

            Assert.AreEqual(new Matrix<Pow2.N4>(new double[,] { { 1 }, { 2 }, { 3 } }), vector.Vertical);

            Matrix<Pow2.N4> matrix = new(new double[,] { { 1, 2, 3 }, { 4, 5, 6 } });

            Assert.AreEqual(new Vector<Pow2.N4>(1, 4), matrix.Vertical(0));
            Assert.AreEqual(new Vector<Pow2.N4>(2, 5), matrix.Vertical(1));
            Assert.AreEqual(new Vector<Pow2.N4>(3, 6), matrix.Vertical(2));
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
        public void FillTest() {
            Matrix<Pow2.N4> matrix = Matrix<Pow2.N4>.Fill(2, 2, value: 7);

            Assert.AreEqual(7, matrix[0, 0]);
            Assert.AreEqual(7, matrix[1, 0]);
            Assert.AreEqual(7, matrix[0, 1]);
            Assert.AreEqual(7, matrix[1, 1]);
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

            Assert.IsTrue(MultiPrecision<Pow2.N4>.IsNaN(matrix[0, 0]));
            Assert.IsTrue(MultiPrecision<Pow2.N4>.IsNaN(matrix[1, 0]));
        }

        [TestMethod()]
        public void IsEqualSizeTest() {
            Matrix<Pow2.N4> matrix1 = Matrix<Pow2.N4>.Zero(2, 2);
            Matrix<Pow2.N4> matrix2 = Matrix<Pow2.N4>.Zero(2, 3);

            Assert.AreEqual(matrix1.Shape, matrix1.Shape);
            Assert.AreNotEqual(matrix1.Shape, matrix2.Shape);
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
        public void DetTest() {
            Matrix<Pow2.N4> matrix1 = new double[,] { { 1, 2 }, { 3, 4 } };
            Matrix<Pow2.N4> matrix2 = new double[,] { { 1, 2 }, { 2, 4 } };
            Matrix<Pow2.N4> matrix3 = new double[,] { { -7, -6, 4 }, { 0, 5, -6 }, { 6, -2, 6 } };
            Matrix<Pow2.N4> matrix4 = new double[,] { { 1, 1, -9 }, { -8, -10, -4 }, { 7, 10, 9 } };
            Matrix<Pow2.N4> matrix5 = new double[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };

            Assert.AreEqual(-2, matrix1.Det);
            Assert.AreEqual(0, matrix2.Det);
            Assert.AreEqual(-30, matrix3.Det);
            Assert.AreEqual(84, matrix4.Det);
            Assert.AreEqual(0, matrix5.Det);
        }

        [TestMethod()]
        public void TraceTest() {
            Matrix<Pow2.N4> matrix1 = new(new double[,] { { 1, 2 }, { 3, 4 } });
            Matrix<Pow2.N4> matrix2 = new(new double[,] { { 1, 2 }, { 2, 4 } });

            Assert.AreEqual(5, matrix1.Trace);
            Assert.AreEqual(5, matrix2.Trace);
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
            Assert.AreEqual("invalid", matrix4.ToString());

            Assert.AreEqual("[ [ 1.0000e0, 2.0000e0, 3.0000e0 ], [ 4.0000e0, 5.0000e0, 6.0000e0 ] ]", matrix1.ToString("e4"));
            Assert.AreEqual("[ [ 1.0000e0, 2.0000e0, 3.0000e0 ], [ 4.0000e0, 5.0000e0, 6.0000e0 ] ]", $"{matrix1:e4}");
        }

        [TestMethod()]
        public void SampleTest() {
            // solve for v: Av=x
            Matrix<Pow2.N4> a = new double[,] { { 1, 2 }, { 3, 4 } };
            Vector<Pow2.N4> x = (4, 3);

            Vector<Pow2.N4> v = Matrix<Pow2.N4>.Solve(a, x);

            Console.WriteLine(v);
        }
    }
}