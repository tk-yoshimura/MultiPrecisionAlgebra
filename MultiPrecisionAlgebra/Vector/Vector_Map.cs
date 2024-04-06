using MultiPrecision;
using System;

namespace MultiPrecisionAlgebra {
    ///<summary>ベクトルクラス</summary>
    public partial class Vector<N> where N : struct, IConstant {
        /// <summary>写像キャスト</summary>
        public static implicit operator Vector<N>((Func<MultiPrecision<N>, MultiPrecision<N>> func, Vector<N> arg) sel) {
            return Func(sel.func, sel.arg);
        }

        /// <summary>写像キャスト</summary>
        public static implicit operator Vector<N>((Func<MultiPrecision<N>, MultiPrecision<N>, MultiPrecision<N>> func, (Vector<N> vector1, Vector<N> vector2) args) sel) {
            return Func(sel.func, sel.args.vector1, sel.args.vector2);
        }

        /// <summary>写像キャスト</summary>
        public static implicit operator Vector<N>((Func<MultiPrecision<N>, MultiPrecision<N>, MultiPrecision<N>, MultiPrecision<N>> func, (Vector<N> vector1, Vector<N> vector2, Vector<N> vector3) args) sel) {
            return Func(sel.func, sel.args.vector1, sel.args.vector2, sel.args.vector3);
        }

        /// <summary>写像キャスト</summary>
        public static implicit operator Vector<N>((Func<MultiPrecision<N>, MultiPrecision<N>, MultiPrecision<N>, MultiPrecision<N>, MultiPrecision<N>> func, (Vector<N> vector1, Vector<N> vector2, Vector<N> vector3, Vector<N> vector4) args) sel) {
            return Func(sel.func, sel.args.vector1, sel.args.vector2, sel.args.vector3, sel.args.vector4);
        }

        /// <summary>写像</summary>
        public static Vector<N> Func(Func<MultiPrecision<N>, MultiPrecision<N>> f, Vector<N> vector) {
            MultiPrecision<N>[] x = vector.v, v = new MultiPrecision<N>[vector.Dim];

            for (int i = 0; i < v.Length; i++) {
                v[i] = f(x[i]);
            }

            return new Vector<N>(v, cloning: false);
        }

        /// <summary>写像</summary>
        public static Vector<N> Func(Func<MultiPrecision<N>, MultiPrecision<N>, MultiPrecision<N>> f, Vector<N> vector1, Vector<N> vector2) {
            if (vector1.Dim != vector2.Dim) {
                throw new ArgumentException("mismatch size", $"{nameof(vector1)},{nameof(vector2)}");
            }

            MultiPrecision<N>[] x = vector1.v, y = vector2.v, v = new MultiPrecision<N>[vector1.Dim];

            for (int i = 0; i < v.Length; i++) {
                v[i] = f(x[i], y[i]);
            }

            return new Vector<N>(v, cloning: false);
        }

        /// <summary>写像</summary>
        public static Vector<N> Func(Func<MultiPrecision<N>, MultiPrecision<N>, MultiPrecision<N>, MultiPrecision<N>> f, Vector<N> vector1, Vector<N> vector2, Vector<N> vector3) {
            if (vector1.Dim != vector2.Dim || vector1.Dim != vector3.Dim) {
                throw new ArgumentException("mismatch size", $"{nameof(vector1)},{nameof(vector2)},{nameof(vector3)}");
            }

            MultiPrecision<N>[] x = vector1.v, y = vector2.v, z = vector3.v, v = new MultiPrecision<N>[vector1.Dim];

            for (int i = 0; i < v.Length; i++) {
                v[i] = f(x[i], y[i], z[i]);
            }

            return new Vector<N>(v, cloning: false);
        }

        /// <summary>写像</summary>
        public static Vector<N> Func(Func<MultiPrecision<N>, MultiPrecision<N>, MultiPrecision<N>, MultiPrecision<N>, MultiPrecision<N>> f, Vector<N> vector1, Vector<N> vector2, Vector<N> vector3, Vector<N> vector4) {
            if (vector1.Dim != vector2.Dim || vector1.Dim != vector3.Dim || vector1.Dim != vector4.Dim) {
                throw new ArgumentException("mismatch size", $"{nameof(vector1)},{nameof(vector2)},{nameof(vector3)},{nameof(vector4)}");
            }

            MultiPrecision<N>[] x = vector1.v, y = vector2.v, z = vector3.v, w = vector4.v, v = new MultiPrecision<N>[vector1.Dim];

            for (int i = 0; i < v.Length; i++) {
                v[i] = f(x[i], y[i], z[i], w[i]);
            }

            return new Vector<N>(v, cloning: false);
        }
    }
}
