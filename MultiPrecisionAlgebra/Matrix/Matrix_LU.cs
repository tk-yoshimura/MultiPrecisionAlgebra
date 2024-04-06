using MultiPrecision;
using System;

namespace MultiPrecisionAlgebra {
    /// <summary>行列クラス</summary>
    public partial class Matrix<N> where N : struct, IConstant {
        /// <summary>LU分解</summary>
        public static (Matrix<N> l, Matrix<N> u) LU(Matrix<N> m) {
            if (!IsSquare(m)) {
                throw new ArgumentException("invalid size", nameof(m));
            }

            int n = m.Size;

            if (!IsFinite(m)) {
                return (Invalid(n, n), Invalid(n, n));
            }

            long exponent = m.MaxExponent;
            m = ScaleB(m, -exponent);

            Matrix<N> l = Zero(n, n), u = Zero(n, n);

            //LU分解
            for (int i = 0; i < n; i++) {
                for (int j = i + 1; j < n; j++) {
                    MultiPrecision<N> mul = m.e[j, i] /= m.e[i, i];
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

            return (l, u);
        }
    }
}
