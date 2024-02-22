using MultiPrecision;
using System;
using System.Linq;

namespace MultiPrecisionAlgebra {
    ///<summary>ベクトルクラス</summary>
    public partial class Vector<N> where N : struct, IConstant {
        /// <summary>単項プラス</summary>
        public static Vector<N> operator +(Vector<N> vector) {
            return (Vector<N>)vector.Clone();
        }

        /// <summary>単項マイナス</summary>
        public static Vector<N> operator -(Vector<N> vector) {
            MultiPrecision<N>[] ret = new MultiPrecision<N>[vector.Dim], v = vector.v;

            for (int i = 0; i < ret.Length; i++) {
                ret[i] = -v[i];
            }

            return new Vector<N>(ret, cloning: false);
        }

        /// <summary>ベクトル加算</summary>
        public static Vector<N> operator +(Vector<N> vector1, Vector<N> vector2) {
            if (vector1.Dim != vector2.Dim) {
                throw new ArgumentException("mismatch size", $"{nameof(vector1)},{nameof(vector2)}");
            }

            MultiPrecision<N>[] ret = new MultiPrecision<N>[vector1.Dim], v1 = vector1.v, v2 = vector2.v;

            for (int i = 0; i < ret.Length; i++) {
                ret[i] = v1[i] + v2[i];
            }

            return new Vector<N>(ret, cloning: false);
        }

        /// <summary>ベクトル減算</summary>
        public static Vector<N> operator -(Vector<N> vector1, Vector<N> vector2) {
            if (vector1.Dim != vector2.Dim) {
                throw new ArgumentException("mismatch size", $"{nameof(vector1)},{nameof(vector2)}");
            }

            MultiPrecision<N>[] ret = new MultiPrecision<N>[vector1.Dim], v1 = vector1.v, v2 = vector2.v;

            for (int i = 0; i < ret.Length; i++) {
                ret[i] = v1[i] - v2[i];
            }

            return new Vector<N>(ret, cloning: false);
        }

        /// <summary>ベクトル乗算</summary>
        public static Vector<N> operator *(Vector<N> vector1, Vector<N> vector2) {
            if (vector1.Dim != vector2.Dim) {
                throw new ArgumentException("mismatch size", $"{nameof(vector1)},{nameof(vector2)}");
            }

            MultiPrecision<N>[] ret = new MultiPrecision<N>[vector1.Dim], v1 = vector1.v, v2 = vector2.v;

            for (int i = 0; i < ret.Length; i++) {
                ret[i] = v1[i] * v2[i];
            }

            return new Vector<N>(ret, cloning: false);
        }

        /// <summary>ベクトル除算</summary>
        public static Vector<N> operator /(Vector<N> vector1, Vector<N> vector2) {
            if (vector1.Dim != vector2.Dim) {
                throw new ArgumentException("mismatch size", $"{nameof(vector1)},{nameof(vector2)}");
            }

            MultiPrecision<N>[] ret = new MultiPrecision<N>[vector1.Dim], v1 = vector1.v, v2 = vector2.v;

            for (int i = 0; i < ret.Length; i++) {
                ret[i] = v1[i] / v2[i];
            }

            return new Vector<N>(ret, cloning: false);
        }

        /// <summary>スカラー加算</summary>
        public static Vector<N> operator +(MultiPrecision<N> r, Vector<N> vector) {
            MultiPrecision<N>[] ret = new MultiPrecision<N>[vector.Dim], v = vector.v;

            for (int i = 0; i < ret.Length; i++) {
                ret[i] = r + v[i];
            }

            return new Vector<N>(ret, cloning: false);
        }

        /// <summary>スカラー加算</summary>
        public static Vector<N> operator +(Vector<N> vector, MultiPrecision<N> r) {
            return r + vector;
        }

        /// <summary>スカラー減算</summary>
        public static Vector<N> operator -(MultiPrecision<N> r, Vector<N> vector) {
            MultiPrecision<N>[] ret = new MultiPrecision<N>[vector.Dim], v = vector.v;

            for (int i = 0; i < ret.Length; i++) {
                ret[i] = r - v[i];
            }

            return new Vector<N>(ret, cloning: false);
        }

        /// <summary>スカラー減算</summary>
        public static Vector<N> operator -(Vector<N> vector, MultiPrecision<N> r) {
            return (-r) + vector;
        }

        /// <summary>スカラー倍</summary>
        public static Vector<N> operator *(MultiPrecision<N> r, Vector<N> vector) {
            MultiPrecision<N>[] ret = new MultiPrecision<N>[vector.Dim], v = vector.v;

            for (int i = 0; i < ret.Length; i++) {
                ret[i] = r * v[i];
            }

            return new Vector<N>(ret, cloning: false);
        }

        /// <summary>スカラー倍</summary>
        public static Vector<N> operator *(Vector<N> vector, MultiPrecision<N> r) {
            return r * vector;
        }

        /// <summary>スカラー除算</summary>
        public static Vector<N> operator /(MultiPrecision<N> r, Vector<N> vector) {
            MultiPrecision<N>[] ret = new MultiPrecision<N>[vector.Dim], v = vector.v;

            for (int i = 0; i < ret.Length; i++) {
                ret[i] = r / v[i];
            }

            return new Vector<N>(ret, cloning: false);
        }

        /// <summary>スカラー除算</summary>
        public static Vector<N> operator /(Vector<N> vector, MultiPrecision<N> r) {
            return (1d / r) * vector;
        }

        /// <summary>内積</summary>
        public static MultiPrecision<N> Dot(Vector<N> vector1, Vector<N> vector2) {
            if (vector1.Dim != vector2.Dim) {
                throw new ArgumentException("mismatch size", $"{nameof(vector1)},{nameof(vector2)}");
            }

            MultiPrecision<N> sum = MultiPrecision<N>.Zero;

            for (int i = 0, dim = vector1.Dim; i < dim; i++) {
                sum += vector1.v[i] * vector2.v[i];
            }

            return sum;
        }

        /// <summary>クロス積</summary>
        public static Vector<N> Cross(Vector<N> vector1, Vector<N> vector2) {
            if (vector1.Dim != 3 || vector1.Dim != 3) {
                throw new ArgumentException("invalid size", $"{nameof(vector1)},{nameof(vector2)}");
            }

            MultiPrecision<N> x1 = vector1.X, x2 = vector2.X, y1 = vector1.Y, y2 = vector2.Y, z1 = vector1.Z, z2 = vector2.Z;

            MultiPrecision<N>[] v = [
                y1 * z2 - z1 * y2,
                z1 * x2 - x1 * z2,
                x1 * y2 - y1 * x2,
            ];

            return new Vector<N>(v, cloning: false);
        }

        /// <summary>多項式</summary>
        public static MultiPrecision<N> Polynomial(MultiPrecision<N> x, Vector<N> coef) {
            if (coef.Dim < 1) {
                return MultiPrecision<N>.Zero;
            }

            MultiPrecision<N> y = coef[^1];

            for (int i = coef.Dim - 2; i >= 0; i--) {
                y = coef[i] + x * y;
            }

            return y;
        }

        /// <summary>多項式</summary>
        public static Vector<N> Polynomial(Vector<N> x, Vector<N> coef) {
            if (coef.Dim < 1) {
                return Zero(x.Dim);
            }

            Vector<N> y = Fill(x.Dim, coef[^1]);

            for (int i = coef.Dim - 2; i >= 0; i--) {
                MultiPrecision<N> c = coef[i];

                for (int j = 0, n = x.Dim; j < n; j++) {
                    y[j] = c + x[j] * y[j];
                }
            }

            return y;
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
    }
}
