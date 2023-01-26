using MultiPrecision;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace MultiPrecisionAlgebra {
    ///<summary>ベクトルクラス</summary>
    [DebuggerDisplay("{Convert<MultiPrecision.Pow2.N4>().ToString(),nq}")]
    public class Vector<N> : ICloneable, IEnumerable<(int index, MultiPrecision<N> val)> where N : struct, IConstant {
        internal readonly MultiPrecision<N>[] v;

        /// <summary>コンストラクタ</summary>
        protected Vector(int size) {
            this.v = new MultiPrecision<N>[size];

            for (int i = 0; i < v.Length; i++) {
                this.v[i] = 0;
            }
        }

        /// <summary>コンストラクタ</summary>
        public Vector(params MultiPrecision<N>[] v) {
            this.v = (MultiPrecision<N>[])v.Clone();
        }

        /// <summary>コンストラクタ</summary>
        public Vector(params double[] v) : this(v.Length) {
            for (int i = 0; i < v.Length; i++) {
                this.v[i] = v[i];
            }
        }

        /// <summary>コンストラクタ</summary>
        public Vector(IEnumerable<MultiPrecision<N>> v) {
            this.v = v.ToArray();
        }

        /// <summary>コンストラクタ</summary>
        public Vector(IEnumerable<double> v) : this(v.ToArray()) { }

        /// <summary>インデクサ</summary>
        public MultiPrecision<N> this[int index] {
            get => v[index];
            set => v[index] = value;
        }

        /// <summary>インデクサ</summary>
        public MultiPrecision<N> this[Index index] {
            get => v[index.GetOffset(Dim)];
            set => v[index.GetOffset(Dim)] = value;
        }

        /// <summary>領域インデクサ</summary>
        public Vector<N> this[Range range] {
            get {
                (int index, int counts) = range.GetOffsetAndLength(Dim);

                MultiPrecision<N>[] ret = new MultiPrecision<N>[counts];
                for (int i = 0; i < counts; i++) {
                    ret[i] = v[i + index];
                }

                return new(ret);
            }

            set {
                (int index, int counts) = range.GetOffsetAndLength(Dim);

                if (value.Dim != counts) {
                    throw new ArgumentOutOfRangeException(nameof(range));
                }

                for (int i = 0; i < counts; i++) {
                    v[i + index] = value.v[i];
                }
            }
        }

        /// <summary>X成分</summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public MultiPrecision<N> X {
            get => v[0];
            set => v[0] = value;
        }

        /// <summary>Y成分</summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public MultiPrecision<N> Y {
            get => v[1];
            set => v[1] = value;
        }

        /// <summary>Z成分</summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public MultiPrecision<N> Z {
            get => v[2];
            set => v[2] = value;
        }

        /// <summary>W成分</summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public MultiPrecision<N> W {
            get => v[3];
            set => v[3] = value;
        }

        /// <summary>次元数</summary>
        public int Dim => v.Length;

        /// <summary>キャスト</summary>
        public static implicit operator MultiPrecision<N>[](Vector<N> vector) {
            return (MultiPrecision<N>[])vector.v.Clone();
        }

        /// <summary>キャスト</summary>
        public static implicit operator Vector<N>(MultiPrecision<N>[] arr) {
            return new Vector<N>(arr);
        }

        /// <summary>ノルム</summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public MultiPrecision<N> Norm => MultiPrecision<N>.Sqrt(SquareNorm);

        /// <summary>ノルム2乗</summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public MultiPrecision<N> SquareNorm {
            get {
                MultiPrecision<N> norm = 0;
                foreach (var vi in v) {
                    norm += vi * vi;
                }

                return norm;
            }
        }

        /// <summary>行ベクトル</summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public Matrix<N> Horizontal {
            get {
                Matrix<N> ret = Matrix<N>.Zero(1, Dim);
                for (int i = 0; i < Dim; i++) {
                    ret.e[0, i] = v[i];
                }

                return ret;
            }
        }

        /// <summary>列ベクトル</summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public Matrix<N> Vertical {
            get {
                Matrix<N> ret = Matrix<N>.Zero(Dim, 1);
                for (int i = 0; i < Dim; i++) {
                    ret.e[i, 0] = v[i];
                }

                return ret;
            }
        }

        /// <summary>正規化</summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public Vector<N> Normal => this / Norm;

        /// <summary>単項プラス</summary>
        public static Vector<N> operator +(Vector<N> vector) {
            return (Vector<N>)vector.Clone();
        }

        /// <summary>単項マイナス</summary>
        public static Vector<N> operator -(Vector<N> vector) {
            MultiPrecision<N>[] v = new MultiPrecision<N>[vector.Dim];

            for (int i = 0; i < vector.Dim; i++) {
                v[i] = -vector.v[i];
            }
            return new Vector<N>(v);
        }

        /// <summary>ベクトル加算</summary>
        public static Vector<N> operator +(Vector<N> vector1, Vector<N> vector2) {
            if (vector1.Dim != vector2.Dim) {
                throw new ArgumentException("mismatch size", $"{nameof(vector1)},{nameof(vector2)}");
            }

            int size = vector1.Dim;
            MultiPrecision<N>[] v = new MultiPrecision<N>[size];

            for (int i = 0; i < size; i++) {
                v[i] = vector1.v[i] + vector2.v[i];
            }

            return new Vector<N>(v);
        }

        /// <summary>ベクトル減算</summary>
        public static Vector<N> operator -(Vector<N> vector1, Vector<N> vector2) {
            if (vector1.Dim != vector2.Dim) {
                throw new ArgumentException("mismatch size", $"{nameof(vector1)},{nameof(vector2)}");
            }

            int size = vector1.Dim;
            MultiPrecision<N>[] v = new MultiPrecision<N>[size];

            for (int i = 0; i < size; i++) {
                v[i] = vector1.v[i] - vector2.v[i];
            }

            return new Vector<N>(v);
        }

        /// <summary>ベクトル乗算</summary>
        public static Vector<N> operator *(Vector<N> vector1, Vector<N> vector2) {
            if (vector1.Dim != vector2.Dim) {
                throw new ArgumentException("mismatch size", $"{nameof(vector1)},{nameof(vector2)}");
            }

            int size = vector1.Dim;
            MultiPrecision<N>[] v = new MultiPrecision<N>[size];

            for (int i = 0; i < size; i++) {
                v[i] = vector1.v[i] * vector2.v[i];
            }

            return new Vector<N>(v);
        }

        /// <summary>ベクトル除算</summary>
        public static Vector<N> operator /(Vector<N> vector1, Vector<N> vector2) {
            if (vector1.Dim != vector2.Dim) {
                throw new ArgumentException("mismatch size", $"{nameof(vector1)},{nameof(vector2)}");
            }

            int size = vector1.Dim;
            MultiPrecision<N>[] v = new MultiPrecision<N>[size];

            for (int i = 0; i < size; i++) {
                v[i] = vector1.v[i] / vector2.v[i];
            }

            return new Vector<N>(v);
        }

        /// <summary>スカラー倍</summary>
        public static Vector<N> operator *(MultiPrecision<N> r, Vector<N> vector) {
            MultiPrecision<N>[] v = new MultiPrecision<N>[vector.Dim];

            for (int i = 0; i < vector.Dim; i++) {
                v[i] = vector.v[i] * r;
            }

            return new Vector<N>(v);
        }

        /// <summary>スカラー倍</summary>
        public static Vector<N> operator *(Vector<N> vector, MultiPrecision<N> r) {
            return r * vector;
        }

        /// <summary>スカラー逆数倍</summary>
        public static Vector<N> operator /(Vector<N> vector, MultiPrecision<N> r) {
            return (1 / r) * vector;
        }

        /// <summary>ベクトル間距離</summary>
        public static MultiPrecision<N> Distance(Vector<N> vector1, Vector<N> vector2) {
            return (vector1 - vector2).Norm;
        }

        /// <summary>ベクトル間距離2乗</summary>
        public static MultiPrecision<N> SquareDistance(Vector<N> vector1, Vector<N> vector2) {
            return (vector1 - vector2).SquareNorm;
        }

        /// <summary>ベクトル内積</summary>
        public static MultiPrecision<N> InnerProduct(Vector<N> vector1, Vector<N> vector2) {
            if (vector1.Dim != vector2.Dim) {
                throw new ArgumentException("mismatch size", $"{nameof(vector1)},{nameof(vector2)}");
            }

            MultiPrecision<N> sum = 0;
            for (int i = 0, dim = vector1.Dim; i < dim; i++) {
                sum += vector1.v[i] * vector2.v[i];
            }

            return sum;
        }

        /// <summary>最大指数</summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public long MaxExponent {
            get {
                long max_exponent = long.MinValue;

                for (int i = 0; i < Dim; i++) {
                    if (v[i].IsFinite) {
                        max_exponent = Math.Max(v[i].Exponent, max_exponent);
                    }
                }

                return max_exponent;
            }
        }

        /// <summary>2べき乗スケーリング</summary>
        public static Vector<N> ScaleB(Vector<N> vector, long n) {
            Vector<N> ret = vector.Copy();

            for (int i = 0; i < ret.Dim; i++) {
                ret.v[i] = MultiPrecision<N>.Ldexp(ret.v[i], n);
            }

            return ret;
        }

        /// <summary>ゼロベクトル</summary>
        public static Vector<N> Zero(int size) {
            return new Vector<N>(size);
        }

        /// <summary>ゼロベクトルか判定</summary>
        public static bool IsZero(Vector<N> vector) {
            for (int i = 0; i < vector.Dim; i++) {
                if (!vector.v[i].IsZero) {
                    return false;
                }
            }

            return true;
        }

        /// <summary>不正なベクトル</summary>
        public static Vector<N> Invalid(int size) {
            MultiPrecision<N>[] v = new MultiPrecision<N>[size];
            for (int i = 0; i < size; i++) {
                v[i] = MultiPrecision<N>.NaN;
            }

            return new Vector<N>(v);
        }

        /// <summary>有効なベクトルか判定</summary>
        public static bool IsValid(Vector<N> vector) {
            for (int i = 0; i < vector.Dim; i++) {
                if (!vector.v[i].IsFinite) {
                    return false;
                }
            }

            return true;
        }

        /// <summary>ベクトルが等しいか</summary>
        public static bool operator ==(Vector<N> vector1, Vector<N> vector2) {
            if (ReferenceEquals(vector1, vector2)) {
                return true;
            }
            if (vector1 is null || vector2 is null) {
                return false;
            }

            return vector1.v.SequenceEqual(vector2.v);
        }

        /// <summary>ベクトルが異なるか判定</summary>
        public static bool operator !=(Vector<N> vector1, Vector<N> vector2) {
            return !(vector1 == vector2);
        }

        /// <summary>等しいか判定</summary>
        public override bool Equals(object obj) {
            return (obj is not null) && obj is Vector<N> vector && vector == this;
        }

        /// <summary>ハッシュ値</summary>
        public override int GetHashCode() {
            return Dim > 0 ? v[0].GetHashCode() : 0;
        }

        /// <summary>クローン</summary>
        public object Clone() {
            return new Vector<N>(v);
        }

        /// <summary>コピー</summary>
        public Vector<N> Copy() {
            return new Vector<N>(v);
        }

        /// <summary>文字列化</summary>
        public override string ToString() {
            if (Dim <= 0) {
                return string.Empty;
            }

            string str = $"{v[0]}";

            for (int i = 1; i < Dim; i++) {
                str += $",{v[i]}";
            }

            return str;
        }

        /// <summary>精度変更</summary>
        public Vector<M> Convert<M>() where M : struct, IConstant {
            MultiPrecision<M>[] ret = new MultiPrecision<M>[Dim];

            for (int i = 0; i < Dim; i++) {
                ret[i] = v[i].Convert<M>();
            }

            return ret;
        }

        public IEnumerator<(int index, MultiPrecision<N> val)> GetEnumerator() {
            for (int i = 0; i < Dim; i++) {
                yield return (i, v[i]);
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
