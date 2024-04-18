using MultiPrecision;
using System;

namespace MultiPrecisionAlgebra {
    /// <summary>行列クラス</summary>
    public partial class Matrix<N> where N : struct, IConstant {
        /// <summary>QR分解</summary>
        public static (Matrix<N> q, Matrix<N> r) QR(Matrix<N> m) {
            if (!IsSquare(m)) {
                throw new ArgumentException("invalid size", nameof(m));
            }

            int n = m.Size;

            if (!IsFinite(m)) {
                return (Invalid(n), Invalid(n));
            }
            if (IsZero(m)) {
                return (Zero(n), Zero(n));
            }

            long exponent = m.MaxExponent;
            m = ScaleB(m, -exponent);

            Matrix<N> r = m, q = Identity(n);
            Vector<N> u = Vector<N>.Zero(n);

            for (int k = 0; k < n - 1; k++) {
                MultiPrecision<N> vsum = MultiPrecision<N>.Zero;
                for (int i = k; i < n; i++) {
                    vsum += MultiPrecision<N>.Square(r.e[i, k]);
                }
                MultiPrecision<N> vnorm = MultiPrecision<N>.Sqrt(vsum);

                if (MultiPrecision<N>.IsZero(vnorm)) {
                    continue;
                }

                MultiPrecision<N> x = r.e[k, k];
                u.v[k] = MultiPrecision<N>.IsPositive(x) ? (x + vnorm) : (x - vnorm);
                MultiPrecision<N> usum = MultiPrecision<N>.Square(u.v[k]);

                for (int i = k + 1; i < n; i++) {
                    u.v[i] = r.e[i, k];
                    usum += MultiPrecision<N>.Square(u.v[i]);
                }
                MultiPrecision<N> c = 2d / usum;

                Matrix<N> h = Identity(n);
                for (int i = k; i < n; i++) {
                    for (int j = k; j < n; j++) {
                        h.e[i, j] -= c * u[i] * u[j];
                    }
                }

                r = h * r;
                q *= h;
            }

            for (int i = 0; i < n; i++) {
                if (MultiPrecision<N>.IsNegative(r.e[i, i])) {
                    q[.., i] = -q[.., i];
                    r[i, ..] = -r[i, ..];
                }

                for (int k = 0; k < i; k++) {
                    r.e[i, k] = MultiPrecision<N>.Zero;
                }

                for (int k = i + 1; k < n; k++) {
                    if (MultiPrecision<N>.IsZero(r.e[i, k])) {
                        r.e[i, k] = MultiPrecision<N>.Zero;
                    }
                }
            }

            r = ScaleB(r, exponent);

            return (q, r);
        }
    }
}
