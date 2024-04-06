using MultiPrecision;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;

namespace MultiPrecisionAlgebra {
    /// <summary>行列クラス</summary>
    [DebuggerDisplay("{Convert<MultiPrecision.Pow2.N4>().ToString(),nq}")]
    public partial class Matrix<N> :
        ICloneable, IFormattable,
        IEnumerable<(int row_index, int column_index, MultiPrecision<N> val)>,
        IAdditionOperators<Matrix<N>, Matrix<N>, Matrix<N>>,
        ISubtractionOperators<Matrix<N>, Matrix<N>, Matrix<N>>,
        IMultiplyOperators<Matrix<N>, Matrix<N>, Matrix<N>>,
        IUnaryPlusOperators<Matrix<N>, Matrix<N>>,
        IUnaryNegationOperators<Matrix<N>, Matrix<N>>
        where N : struct, IConstant {

        internal readonly MultiPrecision<N>[,] e;

        /// <summary>コンストラクタ</summary>
        /// <param name="m">行列要素配列</param>
        protected Matrix(MultiPrecision<N>[,] m, bool cloning) {
            this.e = cloning ? (MultiPrecision<N>[,])m.Clone() : m;
        }

        /// <summary>コンストラクタ</summary>
        /// <param name="rows">行数</param>
        /// <param name="columns">列数</param>
        protected Matrix(int rows, int columns) {
            if (rows <= 0 || columns <= 0) {
                throw new ArgumentOutOfRangeException($"{nameof(rows)},{nameof(columns)}");
            }

            this.e = new MultiPrecision<N>[rows, columns];

            for (int i = 0; i < Rows; i++) {
                for (int j = 0; j < Columns; j++) {
                    e[i, j] = MultiPrecision<N>.Zero;
                }
            }
        }

        /// <summary>コンストラクタ</summary>
        /// <param name="m">行列要素配列</param>
        public Matrix(double[,] m) : this(m.GetLength(0), m.GetLength(1)) {
            ArgumentNullException.ThrowIfNull(m, nameof(m));

            for (int i = 0; i < Rows; i++) {
                for (int j = 0; j < Columns; j++) {
                    e[i, j] = m[i, j];
                }
            }
        }

        /// <summary>コンストラクタ</summary>
        /// <param name="m">行列要素配列</param>
        public Matrix(MultiPrecision<N>[,] m) : this(m, cloning: true) { }

        /// <summary>行数</summary>
        public int Rows => e.GetLength(0);

        /// <summary>列数</summary>
        public int Columns => e.GetLength(1);

        /// <summary>形状</summary>
        public (int rows, int columns) Shape => (e.GetLength(0), e.GetLength(1));

        /// <summary>サイズ(正方行列のときのみ有効)</summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public int Size {
            get {
                if (!IsSquare(this)) {
                    throw new ArithmeticException("not square matrix");
                }

                return Rows;
            }
        }

        /// <summary>キャスト</summary>
        public static implicit operator MultiPrecision<N>[,](Matrix<N> matrix) {
            return (MultiPrecision<N>[,])matrix.e.Clone();
        }

        /// <summary>キャスト</summary>
        public static explicit operator double[,](Matrix<N> matrix) {
            MultiPrecision<N>[,] e = matrix.e;
            double[,] ret = new double[e.GetLength(0), e.GetLength(1)];

            for (int i = 0; i < e.GetLength(0); i++) {
                for (int j = 0; j < e.GetLength(1); j++) {
                    ret[i, j] = (double)e[i, j];
                }
            }

            return ret;
        }

        /// <summary>キャスト</summary>
        public static implicit operator Matrix<N>(MultiPrecision<N>[,] arr) {
            return new Matrix<N>(arr);
        }

        /// <summary>キャスト</summary>
        public static implicit operator Matrix<N>(double[,] arr) {
            return new Matrix<N>(arr);
        }

        /// <summary>転置</summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public Matrix<N> T => Transpose(this);

        /// <summary>転置</summary>
        public static Matrix<N> Transpose(Matrix<N> m) {
            Matrix<N> ret = new(m.Columns, m.Rows);

            for (int i = 0; i < m.Rows; i++) {
                for (int j = 0; j < m.Columns; j++) {
                    ret.e[j, i] = m.e[i, j];
                }
            }

            return ret;
        }

        /// <summary>逆行列</summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public Matrix<N> Inverse => Invert(this);

        /// <summary>逆行列</summary>
        public static Matrix<N> Invert(Matrix<N> m) {
            if (IsZero(m) || !IsFinite(m)) {
                return Invalid(m.Columns, m.Rows);
            }
            if (m.Rows == m.Columns) {
                return GaussianEliminate(m);
            }
            else if (m.Rows < m.Columns) {
                Matrix<N> mt = m.T, mr = m * mt;
                return mt * mr.Inverse;
            }
            else {
                Matrix<N> mt = m.T, mr = m.T * m;
                return mr.Inverse * mt;
            }
        }

        /// <summary>ノルム</summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public MultiPrecision<N> Norm =>
            IsFinite(this) ? (IsZero(this) ? MultiPrecision<N>.Zero : MultiPrecision<N>.Ldexp(MultiPrecision<N>.Sqrt(ScaleB(this, -MaxExponent).SquareNorm), MaxExponent))
            : !IsValid(this) ? MultiPrecision<N>.NaN : MultiPrecision<N>.PositiveInfinity;

        /// <summary>ノルム2乗</summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public MultiPrecision<N> SquareNorm {
            get {
                MultiPrecision<N> sum_sq = MultiPrecision<N>.Zero;

                for (int i = 0; i < Rows; i++) {
                    for (int j = 0; j < Columns; j++) {
                        sum_sq += e[i, j] * e[i, j];
                    }
                }

                return sum_sq;
            }
        }

        /// <summary>合計</summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public MultiPrecision<N> Sum {
            get {
                MultiPrecision<N> sum = MultiPrecision<N>.Zero;

                for (int i = 0; i < Rows; i++) {
                    for (int j = 0; j < Columns; j++) {
                        sum += e[i, j];
                    }
                }

                return sum;
            }
        }

        /// <summary>最大指数</summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public long MaxExponent {
            get {
                long max_exponent = long.MinValue;

                for (int i = 0; i < Rows; i++) {
                    for (int j = 0; j < Columns; j++) {
                        if (MultiPrecision<N>.IsFinite(e[i, j])) {
                            max_exponent = Math.Max(e[i, j].Exponent, max_exponent);
                        }
                    }
                }

                return max_exponent;
            }
        }

        /// <summary>2べき乗スケーリング</summary>
        public static Matrix<N> ScaleB(Matrix<N> matrix, long n) {
            Matrix<N> ret = matrix.Copy();

            for (int i = 0; i < ret.Rows; i++) {
                for (int j = 0; j < ret.Columns; j++) {
                    ret.e[i, j] = MultiPrecision<N>.Ldexp(ret.e[i, j], n);
                }
            }

            return ret;
        }

        /// <summary>行ベクトル</summary>
        /// <param name="row_index">行</param>
        public Vector<N> Horizontal(int row_index) {
            Vector<N> ret = Vector<N>.Zero(Columns);

            for (int i = 0; i < Columns; i++) {
                ret.v[i] = e[row_index, i];
            }

            return ret;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public Vector<N>[] Horizontals => (new Vector<N>[Rows]).Select((_, idx) => Horizontal(idx)).ToArray();

        /// <summary>列ベクトル</summary>
        /// <param name="column_index">列</param>
        public Vector<N> Vertical(int column_index) {
            Vector<N> ret = Vector<N>.Zero(Rows);

            for (int i = 0; i < Rows; i++) {
                ret.v[i] = e[i, column_index];
            }

            return ret;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public Vector<N>[] Verticals => (new Vector<N>[Columns]).Select((_, idx) => Vertical(idx)).ToArray();

        /// <summary>対角成分</summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public MultiPrecision<N>[] Diagonals {
            get {
                if (!IsSquare(this)) {
                    throw new ArithmeticException("not square matrix");
                }

                MultiPrecision<N>[] diagonals = new MultiPrecision<N>[Size];

                for (int i = 0; i < Size; i++) {
                    diagonals[i] = e[i, i];
                }

                return diagonals;
            }
        }


        public static Matrix<N> FromDiagonals(MultiPrecision<N>[] vs) {
            MultiPrecision<N>[,] v = new MultiPrecision<N>[vs.Length, vs.Length];

            for (int i = 0; i < vs.Length; i++) {
                for (int j = 0; j < vs.Length; j++) {
                    v[i, j] = MultiPrecision<N>.Zero;
                }

                v[i, i] = vs[i];
            }

            return new Matrix<N>(v, cloning: false);
        }

        /// <summary>ゼロ行列</summary>
        /// <param name="rows">行数</param>
        /// <param name="columns">列数</param>
        public static Matrix<N> Zero(int rows, int columns) {
            return new Matrix<N>(rows, columns);
        }

        /// <summary>定数行列</summary>
        public static Matrix<N> Fill(int rows, int columns, MultiPrecision<N> value) {
            MultiPrecision<N>[,] v = new MultiPrecision<N>[rows, columns];

            for (int i = 0; i < rows; i++) {
                for (int j = 0; j < columns; j++) {
                    v[i, j] = value;
                }
            }

            return new Matrix<N>(v, cloning: false);
        }

        /// <summary>単位行列</summary>
        /// <param name="size">行列サイズ</param>
        public static Matrix<N> Identity(int size) {
            Matrix<N> ret = new(size, size);

            for (int i = 0; i < size; i++) {
                for (int j = 0; j < size; j++) {
                    ret.e[i, j] = (i == j) ? 1 : 0;
                }
            }

            return ret;
        }

        /// <summary>不正な行列</summary>
        /// <param name="rows">行数</param>
        /// <param name="columns">列数</param>
        public static Matrix<N> Invalid(int rows, int columns) {
            return Fill(rows, columns, value: MultiPrecision<N>.NaN);
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

            for (int i = 0; i < matrix.Rows; i++) {
                for (int j = 0; j < matrix.Columns; j++) {
                    if (i != j && matrix.e[i, j] != 0) {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>ゼロ行列か判定</summary>
        public static bool IsZero(Matrix<N> matrix) {
            for (int i = 0; i < matrix.Rows; i++) {
                for (int j = 0; j < matrix.Columns; j++) {
                    if (!MultiPrecision<N>.IsZero(matrix.e[i, j])) {
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

            for (int i = 0; i < matrix.Rows; i++) {
                for (int j = 0; j < matrix.Columns; j++) {
                    if (i == j) {
                        if (matrix.e[i, j] != MultiPrecision<N>.One) {
                            return false;
                        }
                    }
                    else {
                        if (!MultiPrecision<N>.IsZero(matrix.e[i, j])) {
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

            for (int i = 0; i < matrix.Rows; i++) {
                for (int j = i + 1; j < matrix.Columns; j++) {
                    if (matrix.e[i, j] != matrix.e[j, i]) {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>有限行列か判定</summary>
        public static bool IsFinite(Matrix<N> matrix) {
            for (int i = 0; i < matrix.Rows; i++) {
                for (int j = 0; j < matrix.Columns; j++) {
                    if (!MultiPrecision<N>.IsFinite(matrix.e[i, j])) {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>無限要素を含む行列か判定</summary>
        public static bool IsInfinity(Matrix<N> matrix) {
            if (!IsValid(matrix)) {
                return false;
            }

            for (int i = 0; i < matrix.Rows; i++) {
                for (int j = 0; j < matrix.Columns; j++) {
                    if (MultiPrecision<N>.IsInfinity(matrix.e[i, j])) {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>有効な行列か判定</summary>
        public static bool IsValid(Matrix<N> matrix) {
            if (matrix.Rows < 1 || matrix.Columns < 1) {
                return false;
            }

            for (int i = 0; i < matrix.Rows; i++) {
                for (int j = 0; j < matrix.Columns; j++) {
                    if (MultiPrecision<N>.IsNaN(matrix.e[i, j])) {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>正則行列か判定</summary>
        public static bool IsRegular(Matrix<N> matrix) {
            return IsFinite(Invert(matrix));
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

        public IEnumerator<(int row_index, int column_index, MultiPrecision<N> val)> GetEnumerator() {
            for (int i = 0; i < Rows; i++) {
                for (int j = 0; j < Columns; j++) {
                    yield return (i, j, e[i, j]);
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>精度変更</summary>
        public Matrix<M> Convert<M>() where M : struct, IConstant {
            MultiPrecision<M>[,] ret = new MultiPrecision<M>[Rows, Columns];

            for (int i = 0; i < Rows; i++) {
                for (int j = 0; j < Columns; j++) {
                    ret[i, j] = e[i, j].Convert<M>();
                }
            }

            return ret;
        }
    }
}
