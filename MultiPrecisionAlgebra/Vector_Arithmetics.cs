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

        /// <summary>スカラー加算</summary>
        public static Vector<N> operator +(MultiPrecision<N> r, Vector<N> vector) {
            MultiPrecision<N>[] v = new MultiPrecision<N>[vector.Dim];

            for (int i = 0; i < vector.Dim; i++) {
                v[i] = r + vector.v[i];
            }

            return new Vector<N>(v);
        }

        /// <summary>スカラー加算</summary>
        public static Vector<N> operator +(Vector<N> vector, MultiPrecision<N> r) {
            return r + vector;
        }

        /// <summary>スカラー減算</summary>
        public static Vector<N> operator -(MultiPrecision<N> r, Vector<N> vector) {
            MultiPrecision<N>[] v = new MultiPrecision<N>[vector.Dim];

            for (int i = 0; i < vector.Dim; i++) {
                v[i] = r - vector.v[i];
            }

            return new Vector<N>(v);
        }

        /// <summary>スカラー減算</summary>
        public static Vector<N> operator -(Vector<N> vector, MultiPrecision<N> r) {
            return (-r) + vector;
        }

        /// <summary>スカラー倍</summary>
        public static Vector<N> operator *(MultiPrecision<N> r, Vector<N> vector) {
            MultiPrecision<N>[] v = new MultiPrecision<N>[vector.Dim];

            for (int i = 0; i < vector.Dim; i++) {
                v[i] = r * vector.v[i];
            }

            return new Vector<N>(v);
        }

        /// <summary>スカラー倍</summary>
        public static Vector<N> operator *(Vector<N> vector, MultiPrecision<N> r) {
            return r * vector;
        }

        /// <summary>スカラー除算</summary>
        public static Vector<N> operator /(MultiPrecision<N> r, Vector<N> vector) {
            MultiPrecision<N>[] v = new MultiPrecision<N>[vector.Dim];

            for (int i = 0; i < vector.Dim; i++) {
                v[i] = r / vector.v[i];
            }

            return new Vector<N>(v);
        }

        /// <summary>スカラー除算</summary>
        public static Vector<N> operator /(Vector<N> vector, MultiPrecision<N> r) {
            return (1 / r) * vector;
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
