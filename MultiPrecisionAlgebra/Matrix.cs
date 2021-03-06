using MultiPrecision;
using System;
using System.Diagnostics;

namespace MultiPrecisionAlgebra {
    /// <summary>行列クラス</summary>
    [DebuggerDisplay("{ToString(),nq}")]
    public partial class Matrix<N> : ICloneable where N : struct, IConstant {
        internal readonly MultiPrecision<N>[,] e;

        /// <summary>コンストラクタ</summary>
        /// <param name="m">行列要素配列</param>
        public Matrix(double[,] m) : this(m.GetLength(0), m.GetLength(1)) {
            if (m is null) {
                throw new ArgumentNullException(nameof(m));
            }

            for (int i = 0; i < Rows; i++) {
                for (int j = 0; j < Columns; j++) {
                    e[i, j] = m[i, j];
                }
            }
        }

        /// <summary>コンストラクタ</summary>
        /// <param name="m">行列要素配列</param>
        public Matrix(MultiPrecision<N>[,] m) {
            if (m is null) {
                throw new ArgumentNullException(nameof(m));
            }

            this.e = (MultiPrecision<N>[,])m.Clone();
        }

        /// <summary>コンストラクタ </summary>
        /// <param name="rows">行数</param>
        /// <param name="columns">列数</param>
        public Matrix(int rows, int columns) {
            if (rows <= 0 || columns <= 0) {
                throw new ArgumentOutOfRangeException($"{rows},{columns}");
            }

            this.e = new MultiPrecision<N>[rows, columns];

            for (int i = 0; i < Rows; i++) {
                for (int j = 0; j < Columns; j++) {
                    e[i, j] = 0;
                }
            }
        }

        /// <summary>インデクサ </summary>
        /// <param name="row_index">行</param>
        /// <param name="column_index">列</param>
        public MultiPrecision<N> this[int row_index, int column_index] {
            get => e[row_index, column_index];
            set => e[row_index, column_index] = value;
        }

        /// <summary>行数</summary>
        public int Rows => e.GetLength(0);

        /// <summary>列数</summary>
        public int Columns => e.GetLength(1);

        /// <summary>サイズ(正方行列のときのみ有効)</summary>
        public int Size {
            get {
                if (!IsSquare(this)) {
                    throw new InvalidOperationException();
                }

                return Rows;
            }
        }

        /// <summary>キャスト</summary>
        public static implicit operator MultiPrecision<N>[,](Matrix<N> matrix) {
            return (MultiPrecision<N>[,])matrix.e.Clone();
        }

        /// <summary>キャスト</summary>
        public static implicit operator Matrix<N>(MultiPrecision<N>[,] arr) {
            return new Matrix<N>(arr);
        }

        /// <summary>単項プラス</summary>
        public static Matrix<N> operator +(Matrix<N> matrix) {
            return matrix.Copy();
        }

        /// <summary>単項マイナス</summary>
        public static Matrix<N> operator -(Matrix<N> matrix) {
            Matrix<N> ret = matrix.Copy();

            for (int i = 0; i < ret.Rows; i++) {
                for (int j = 0; j < ret.Columns; j++) {
                    ret.e[i, j] = -ret.e[i, j];
                }
            }

            return ret;
        }

        /// <summary>行列加算</summary>
        public static Matrix<N> operator +(Matrix<N> matrix1, Matrix<N> matrix2) {
            if (!IsEqualSize(matrix1, matrix2)) {
                throw new ArgumentException("unmatch size", $"{matrix1},{matrix2}");
            }

            Matrix<N> ret = new(matrix1.Rows, matrix1.Columns);

            for (int i = 0, j; i < ret.Rows; i++) {
                for (j = 0; j < ret.Columns; j++) {
                    ret.e[i, j] = matrix1.e[i, j] + matrix2.e[i, j];
                }
            }

            return ret;
        }

        /// <summary>行列減算</summary>
        public static Matrix<N> operator -(Matrix<N> matrix1, Matrix<N> matrix2) {
            if (!IsEqualSize(matrix1, matrix2)) {
                throw new ArgumentException("unmatch size", $"{matrix1},{matrix2}");
            }

            Matrix<N> ret = new(matrix1.Rows, matrix1.Columns);

            for (int i = 0, j; i < ret.Rows; i++) {
                for (j = 0; j < ret.Columns; j++) {
                    ret.e[i, j] = matrix1.e[i, j] - matrix2.e[i, j];
                }
            }

            return ret;
        }

        /// <summary>行列乗算</summary>
        public static Matrix<N> operator *(Matrix<N> matrix1, Matrix<N> matrix2) {
            if (matrix1.Columns != matrix2.Rows) {
                throw new ArgumentException($"unmatch {matrix1.Columns} {matrix2.Rows}", $"{matrix1},{matrix2}");
            }

            Matrix<N> ret = new(matrix1.Rows, matrix2.Columns);
            int c = matrix1.Columns;

            for (int i = 0, j, k; i < ret.Rows; i++) {
                for (j = 0; j < ret.Columns; j++) {
                    for (k = 0; k < c; k++) {
                        ret.e[i, j] += matrix1.e[i, k] * matrix2.e[k, j];
                    }
                }
            }

            return ret;
        }

        /// <summary>行列・列ベクトル乗算</summary>
        public static Vector<N> operator *(Matrix<N> matrix, Vector<N> vector) {
            if (matrix.Columns != vector.Dim) {
                throw new ArgumentException($"unmatch {matrix.Columns} {vector.Dim}", $"{matrix},{vector}");
            }

            Vector<N> ret = Vector<N>.Zero(matrix.Rows);

            for (int i = 0, j; i < matrix.Rows; i++) {
                for (j = 0; j < matrix.Columns; j++) {
                    ret.v[i] += matrix.e[i, j] * vector.v[j];
                }
            }

            return ret;
        }

        /// <summary>行列・行ベクトル乗算</summary>
        public static Vector<N> operator *(Vector<N> vector, Matrix<N> matrix) {
            if (vector.Dim != matrix.Rows) {
                throw new ArgumentException($"unmatch {vector.Dim} {matrix.Rows} ", $"{vector},{matrix}");
            }

            Vector<N> ret = Vector<N>.Zero(matrix.Columns);

            for (int j = 0, i; j < matrix.Columns; j++) {
                for (i = 0; i < matrix.Rows; i++) {
                    ret.v[j] += vector.v[i] * matrix.e[i, j];
                }
            }

            return ret;
        }

        /// <summary>行列スカラー倍</summary>
        public static Matrix<N> operator *(MultiPrecision<N> r, Matrix<N> matrix) {
            Matrix<N> ret = new(matrix.Rows, matrix.Columns);

            for (int i = 0, j; i < ret.Rows; i++) {
                for (j = 0; j < ret.Columns; j++) {
                    ret.e[i, j] = matrix.e[i, j] * r;
                }
            }

            return ret;
        }

        /// <summary>行列スカラー倍</summary>
        public static Matrix<N> operator *(Matrix<N> matrix, MultiPrecision<N> r) {
            return r * matrix;
        }

        /// <summary>行列スカラー逆数倍</summary>
        public static Matrix<N> operator /(Matrix<N> matrix, MultiPrecision<N> r) {
            return (1d / r) * matrix;
        }

        /// <summary>行列が等しいか</summary>
        public static bool operator ==(Matrix<N> matrix1, Matrix<N> matrix2) {
            if (ReferenceEquals(matrix1, matrix2)) {
                return true;
            }
            if (matrix1 is null || matrix2 is null) {
                return false;
            }

            if (!IsEqualSize(matrix1, matrix2)) {
                return false;
            }

            for (int i = 0, j; i < matrix1.Rows; i++) {
                for (j = 0; j < matrix2.Columns; j++) {
                    if (matrix1.e[i, j] != matrix2.e[i, j]) {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>行列が異なるか判定</summary>
        public static bool operator !=(Matrix<N> matrix1, Matrix<N> matrix2) {
            return !(matrix1 == matrix2);
        }

        /// <summary>等しいか判定</summary>
        public override bool Equals(object obj) {
            return (obj is not null) && obj is Matrix<N> matrix && matrix == this;
        }

        /// <summary>ハッシュ値</summary>
        public override int GetHashCode() {
            return e[0, 0].GetHashCode();
        }

        /// <summary>クローン</summary>
        public object Clone() {
            return new Matrix<N>(e);
        }

        /// <summary>ディープコピー</summary>
        public Matrix<N> Copy() {
            return new Matrix<N>(e);
        }

        /// <summary>転置</summary>
        public Matrix<N> Transpose {
            get {
                Matrix<N> ret = new(Columns, Rows);

                for (int i = 0, j; i < Rows; i++) {
                    for (j = 0; j < Columns; j++) {
                        ret.e[j, i] = e[i, j];
                    }
                }

                return ret;
            }
        }

        /// <summary>逆行列</summary>
        public Matrix<N> Inverse {
            get {
                if (IsZero(this) || !IsValid(this)) {
                    return Invalid(Columns, Rows);
                }
                if (Rows == Columns) {
                    return GaussianEliminate(this);
                }
                else if (Rows < Columns) {
                    Matrix<N> m = this * Transpose;
                    return Transpose * m.Inverse;
                }
                else {
                    Matrix<N> m = Transpose * this;
                    return m.Inverse * Transpose;
                }
            }
        }

        /// <summary>行列ノルム</summary>
        public MultiPrecision<N> Norm {
            get {
                MultiPrecision<N> sum_sq = 0;
                for (int i = 0, j; i < Rows; i++) {
                    for (j = 0; j < Columns; j++) {
                        sum_sq += e[i, j] * e[i, j];
                    }
                }

                return MultiPrecision<N>.Sqrt(sum_sq);
            }
        }

        /// <summary>行ベクトル</summary>
        /// <param name="row_index">行</param>
        public Vector<N> Horizontal(int row_index) {
            Vector<N> ret = new(Columns);
            for (int i = 0; i < Columns; i++) {
                ret.v[i] = e[row_index, i];
            }

            return ret;
        }

        /// <summary>列ベクトル</summary>
        /// <param name="column_index">列</param>
        public Vector<N> Vertical(int column_index) {
            Vector<N> ret = new(Rows);
            for (int i = 0; i < Rows; i++) {
                ret.v[i] = e[i, column_index];
            }

            return ret;
        }

        /// <summary>ゼロ行列</summary>
        /// <param name="rows">行数</param>
        /// <param name="columns">列数</param>
        public static Matrix<N> Zero(int rows, int columns) {
            return new Matrix<N>(rows, columns);
        }

        /// <summary>単位行列</summary>
        /// <param name="size">行列サイズ</param>
        public static Matrix<N> Identity(int size) {
            Matrix<N> ret = new(size, size);

            for (int i = 0, j; i < size; i++) {
                for (j = 0; j < size; j++) {
                    ret.e[i, j] = (i == j) ? 1 : 0;
                }
            }

            return ret;
        }

        /// <summary>不正な行列</summary>
        /// <param name="rows">行数</param>
        /// <param name="columns">列数</param>
        public static Matrix<N> Invalid(int rows, int columns) {
            Matrix<N> ret = new(rows, columns);
            for (int i = 0, j; i < ret.Rows; i++) {
                for (j = 0; j < ret.Columns; j++) {
                    ret.e[i, j] = MultiPrecision<N>.NaN;
                }
            }

            return ret;
        }

        /// <summary>行列のサイズが等しいか判定</summary>
        public static bool IsEqualSize(Matrix<N> matrix1, Matrix<N> matrix2) {
            return (matrix1.Rows == matrix2.Rows) && (matrix1.Columns == matrix2.Columns);
        }

        /// <summary>正方行列か判定</summary>
        public static bool IsSquare(Matrix<N> matrix) {
            return matrix.Rows == matrix.Columns;
        }

        /// <summary>対角行列か判定</summary>
        public static bool IsDiagonal(Matrix<N> matrix) {
            if (!IsSquare(matrix)) {
                return false;
            }

            for (int i = 0, j; i < matrix.Rows; i++) {
                for (j = 0; j < matrix.Columns; j++) {
                    if (i != j && matrix.e[i, j] != 0) {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>ゼロ行列か判定</summary>
        public static bool IsZero(Matrix<N> matrix) {
            for (int i = 0, j; i < matrix.Rows; i++) {
                for (j = 0; j < matrix.Columns; j++) {
                    if (matrix.e[i, j] != 0d) {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>単位行列か判定</summary>
        public static bool IsIdentity(Matrix<N> matrix) {
            if (!IsSquare(matrix)) {
                return false;
            }

            for (int i = 0, j; i < matrix.Rows; i++) {
                for (j = 0; j < matrix.Columns; j++) {
                    if (i == j) {
                        if (matrix.e[i, j] != 1d) {
                            return false;
                        }
                    }
                    else {
                        if (matrix.e[i, j] != 0d) {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        /// <summary>対称行列か判定</summary>
        public static bool IsSymmetric(Matrix<N> matrix) {
            if (!IsSquare(matrix)) {
                return false;
            }

            for (int i = 0, j; i < matrix.Rows; i++) {
                for (j = i + 1; j < matrix.Columns; j++) {
                    if (matrix.e[i, j] != matrix.e[j, i]) {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>有効な行列か判定</summary>
        public static bool IsValid(Matrix<N> matrix) {
            if (matrix.Rows < 1 || matrix.Columns < 1) {
                return false;
            }

            for (int i = 0, j; i < matrix.Rows; i++) {
                for (j = 0; j < matrix.Columns; j++) {
                    if (!matrix.e[i, j].IsFinite) {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>正則行列か判定</summary>
        public static bool IsRegular(Matrix<N> matrix) {
            return IsValid(matrix.Inverse);
        }

        /// <summary>対角成分</summary>
        public MultiPrecision<N>[] Diagonals {
            get {
                if (!IsSquare(this)) {
                    throw new InvalidOperationException();
                }

                MultiPrecision<N>[] diagonals = new MultiPrecision<N>[Size];

                for (int i = 0; i < Size; i++) {
                    diagonals[i] = e[i, i];
                }

                return diagonals;
            }
        }

        /// <summary>文字列化</summary>
        public override string ToString() {
            if (!IsValid(this)) {
                return "Invalid Matrix";
            }

            string str = "[ ";

            str += "[ ";
            str += $"{e[0, 0]}";
            for (int j = 1; j < Columns; j++) {
                str += $", {e[0, j]}";
            }
            str += " ]";

            for (int i = 1, j; i < Rows; i++) {
                str += ", [ ";
                str += $"{e[i, 0]}";
                for (j = 1; j < Columns; j++) {
                    str += $", {e[i, j]}";
                }
                str += " ]";
            }

            str += " ]";

            return str;
        }
    }
}
