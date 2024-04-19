using MultiPrecision;
using System;
using System.Linq;

namespace MultiPrecisionAlgebra {
    /// <summary>行列クラス</summary>
    public partial class Matrix<N> {
        private static (int[] pivot, int pivot_det, Matrix<N> l, Matrix<N> u) LUKernel(Matrix<N> m) {
            if (!IsSquare(m)) {
                throw new ArgumentException("not square matrix", nameof(m));
            }

            int n = m.Size;

            int[] ps = (new int[n]).Select((_, idx) => idx).ToArray();
            int pivot_det = 1;

            if (!IsFinite(m)) {
                return (ps, 1, Invalid(n), Invalid(n));
            }
            if (IsZero(m)) {
                return (ps, 1, Invalid(n), Zero(n));
            }

            long exponent = m.MaxExponent;
            m = ScaleB(m, -exponent);

            Matrix<N> l = Zero(n), u = Zero(n);

            //LU分解
            for (int i = 0; i < n; i++) {
                MultiPrecision<N> pivot = MultiPrecision<N>.Abs(m.e[i, i]);
                int r = i;

                for (int j = i + 1; j < n; j++) {
                    if (MultiPrecision<N>.Abs(m.e[j, i]) > pivot) {
                        pivot = MultiPrecision<N>.Abs(m.e[j, i]);
                        r = j;
                    }
                }

                //ピボットが閾値以下ならばMは正則行列でない
                if (pivot.Exponent <= -MultiPrecision<N>.Bits + 4) {
                    return (ps, 0, Invalid(n), Zero(n));
                }

                if (r != i) {
                    for (int j = 0; j < n; j++) {
                        (m.e[r, j], m.e[i, j]) = (m.e[i, j], m.e[r, j]);
                    }

                    (ps[r], ps[i]) = (ps[i], ps[r]);

                    pivot_det = -pivot_det;
                }

                for (int j = i + 1; j < n; j++) {
                    MultiPrecision<N> mul = m.e[j, i] / m.e[i, i];
                    m.e[j, i] = mul;

                    for (int k = i + 1; k < n; k++) {
                        m.e[j, k] -= m.e[i, k] * mul;
                    }
                }
            }

            //三角行列格納
            for (int i = 0; i < n; i++) {
                l.e[i, i] = MultiPrecision<N>.One;

                int j = 0;
                for (; j < i; j++) {
                    l.e[i, j] = m.e[i, j];
                }
                for (; j < n; j++) {
                    u.e[i, j] = m.e[i, j];
                }
            }

            u = ScaleB(u, exponent);

            return (ps, pivot_det, l, u);
        }

        /// <summary>LU分解</summary>
        public static (Matrix<N> p, Matrix<N> l, Matrix<N> u) LU(Matrix<N> m) {
            (int[] ps, int pivot_det, Matrix<N> l, Matrix<N> u) = LUKernel(m);

            int n = m.Size;

            if (pivot_det == 0) {
                return (Identity(n), l, u);
            }

            Matrix<N> p = Zero(n, n);

            // ピボット行列
            for (int i = 0; i < n; i++) {
                p[ps[i], i] = MultiPrecision<N>.One;
            }

            return (p, l, u);
        }
    }
}
