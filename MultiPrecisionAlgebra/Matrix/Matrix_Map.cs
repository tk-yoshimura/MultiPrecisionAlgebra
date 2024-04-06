using MultiPrecision;
using System;

namespace MultiPrecisionAlgebra {
    /// <summary>行列クラス</summary>
    public partial class Matrix<N> where N : struct, IConstant {
        /// <summary>写像キャスト</summary>
        public static implicit operator Matrix<N>((Func<MultiPrecision<N>, MultiPrecision<N>> func, Matrix<N> arg) sel) {
            return Func(sel.func, sel.arg);
        }

        /// <summary>写像キャスト</summary>
        public static implicit operator Matrix<N>((Func<MultiPrecision<N>, MultiPrecision<N>, MultiPrecision<N>> func, (Matrix<N> matrix1, Matrix<N> matrix2) args) sel) {
            return Func(sel.func, sel.args.matrix1, sel.args.matrix2);
        }

        /// <summary>写像キャスト</summary>
        public static implicit operator Matrix<N>((Func<MultiPrecision<N>, MultiPrecision<N>, MultiPrecision<N>, MultiPrecision<N>> func, (Matrix<N> matrix1, Matrix<N> matrix2, Matrix<N> matrix3) args) sel) {
            return Func(sel.func, sel.args.matrix1, sel.args.matrix2, sel.args.matrix3);
        }

        /// <summary>写像キャスト</summary>
        public static implicit operator Matrix<N>((Func<MultiPrecision<N>, MultiPrecision<N>, MultiPrecision<N>, MultiPrecision<N>, MultiPrecision<N>> func, (Matrix<N> matrix1, Matrix<N> matrix2, Matrix<N> matrix3, Matrix<N> matrix4) args) sel) {
            return Func(sel.func, sel.args.matrix1, sel.args.matrix2, sel.args.matrix3, sel.args.matrix4);
        }

        /// <summary>写像キャスト</summary>
        public static implicit operator Matrix<N>((Func<MultiPrecision<N>, MultiPrecision<N>, MultiPrecision<N>> func, Vector<N> vector_row, Vector<N> vector_column) sel) {
            return Map(sel.func, sel.vector_row, sel.vector_column);
        }

        /// <summary>写像</summary>
        public static Matrix<N> Func(Func<MultiPrecision<N>, MultiPrecision<N>> f, Matrix<N> matrix) {
            MultiPrecision<N>[,] x = matrix.e, v = new MultiPrecision<N>[matrix.Rows, matrix.Columns];

            for (int i = 0; i < v.GetLength(0); i++) {
                for (int j = 0; j < v.GetLength(1); j++) {
                    v[i, j] = f(x[i, j]);
                }
            }

            return new Matrix<N>(v, cloning: false);
        }

        /// <summary>写像</summary>
        public static Matrix<N> Func(Func<MultiPrecision<N>, MultiPrecision<N>, MultiPrecision<N>> f, Matrix<N> matrix1, Matrix<N> matrix2) {
            if (matrix1.Shape != matrix2.Shape) {
                throw new ArgumentException("mismatch size", $"{nameof(matrix1)},{nameof(matrix2)}");
            }

            MultiPrecision<N>[,] x = matrix1.e, y = matrix2.e, v = new MultiPrecision<N>[matrix1.Rows, matrix1.Columns];

            for (int i = 0; i < v.GetLength(0); i++) {
                for (int j = 0; j < v.GetLength(1); j++) {
                    v[i, j] = f(x[i, j], y[i, j]);
                }
            }

            return new Matrix<N>(v, cloning: false);
        }

        /// <summary>写像</summary>
        public static Matrix<N> Func(Func<MultiPrecision<N>, MultiPrecision<N>, MultiPrecision<N>, MultiPrecision<N>> f, Matrix<N> matrix1, Matrix<N> matrix2, Matrix<N> matrix3) {
            if (matrix1.Shape != matrix2.Shape || matrix1.Shape != matrix3.Shape) {
                throw new ArgumentException("mismatch size", $"{nameof(matrix1)},{nameof(matrix2)},{nameof(matrix3)}");
            }

            MultiPrecision<N>[,] x = matrix1.e, y = matrix2.e, z = matrix3.e, v = new MultiPrecision<N>[matrix1.Rows, matrix1.Columns];

            for (int i = 0; i < v.GetLength(0); i++) {
                for (int j = 0; j < v.GetLength(1); j++) {
                    v[i, j] = f(x[i, j], y[i, j], z[i, j]);
                }
            }

            return new Matrix<N>(v, cloning: false);
        }

        /// <summary>写像</summary>
        public static Matrix<N> Func(Func<MultiPrecision<N>, MultiPrecision<N>, MultiPrecision<N>, MultiPrecision<N>, MultiPrecision<N>> f, Matrix<N> matrix1, Matrix<N> matrix2, Matrix<N> matrix3, Matrix<N> matrix4) {
            if (matrix1.Shape != matrix2.Shape || matrix1.Shape != matrix3.Shape || matrix1.Shape != matrix4.Shape) {
                throw new ArgumentException("mismatch size", $"{nameof(matrix1)},{nameof(matrix2)},{nameof(matrix3)},{nameof(matrix4)}");
            }

            MultiPrecision<N>[,] x = matrix1.e, y = matrix2.e, z = matrix3.e, w = matrix4.e, v = new MultiPrecision<N>[matrix1.Rows, matrix1.Columns];

            for (int i = 0; i < v.GetLength(0); i++) {
                for (int j = 0; j < v.GetLength(1); j++) {
                    v[i, j] = f(x[i, j], y[i, j], z[i, j], w[i, j]);
                }
            }

            return new Matrix<N>(v, cloning: false);
        }

        /// <summary>写像</summary>
        public static Matrix<N> Map(Func<MultiPrecision<N>, MultiPrecision<N>, MultiPrecision<N>> f, Vector<N> vector_row, Vector<N> vector_column) {
            MultiPrecision<N>[] row = vector_row.v, col = vector_column.v;
            MultiPrecision<N>[,] v = new MultiPrecision<N>[row.Length, col.Length];

            for (int i = 0; i < v.GetLength(0); i++) {
                for (int j = 0; j < v.GetLength(1); j++) {
                    v[i, j] = f(row[i], col[j]);
                }
            }

            return new Matrix<N>(v, cloning: false);
        }
    }
}
