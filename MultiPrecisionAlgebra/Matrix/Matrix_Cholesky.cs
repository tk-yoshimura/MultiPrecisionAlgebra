using MultiPrecision;
using System;

namespace MultiPrecisionAlgebra {
    /// <summary>行列クラス</summary>
    public partial class Matrix<N> where N : struct, IConstant {
        /// <summary>コレスキー分解</summary>
        public static Matrix<N> Cholesky(Matrix<N> m, bool enable_check_symmetric = true) {
            if (!IsSquare(m)) {
                throw new ArgumentException("not square matrix", nameof(m));
            }

            int n = m.Size;

            if ((enable_check_symmetric && !IsSymmetric(m)) || !IsFinite(m)) {
                return Invalid(n);
            }

            if (IsZero(m)) {
                return Zero(n);
            }

            long exponent = (m.MaxExponent / 2) * 2;

            Matrix<N> u = ScaleB(m, -exponent);

            MultiPrecision<N>[,] v = new MultiPrecision<N>[n, n];

            for (int i = 0; i < n; i++) {
                for (int j = 0; j < i; j++) {
                    MultiPrecision<N> v_ij = u[i, j];
                    for (int k = 0; k < j; k++) {
                        v_ij -= v[i, k] * v[j, k];
                    }
                    v[i, j] = v_ij / v[j, j];
                }

                MultiPrecision<N> v_ii = u[i, i];
                for (int k = 0; k < i; k++) {
                    v_ii -= v[i, k] * v[i, k];
                }
                v[i, i] = MultiPrecision<N>.Sqrt(v_ii);

                for (int j = i + 1; j < n; j++) {
                    v[i, j] = MultiPrecision<N>.Zero;
                }
            }

            Matrix<N> l = ScaleB(new Matrix<N>(v, cloning: false), exponent / 2);

            return l;
        }

        /// <summary>正定値対称行列に対する逆行列</summary>
        public static Matrix<N> InversePositiveSymmetric(Matrix<N> m, bool enable_check_symmetric = true) {
            if (!IsSquare(m)) {
                throw new ArgumentException("not square matrix", nameof(m));
            }

            int n = m.Size;

            if (!IsFinite(m)) {
                return Invalid(n, n);
            }

            Matrix<N> l = Cholesky(m, enable_check_symmetric);

            if (!IsFinite(l)) {
                return Invalid(n);
            }

            Matrix<N> v = Identity(n);

            for (int i = 0; i < n; i++) {
                MultiPrecision<N> inv_mii = 1 / l.e[i, i];
                for (int j = 0; j < n; j++) {
                    v.e[i, j] *= inv_mii;
                }

                for (int j = i + 1; j < n; j++) {
                    MultiPrecision<N> mul = l.e[j, i];
                    for (int k = 0; k < n; k++) {
                        v.e[j, k] -= v.e[i, k] * mul;
                    }
                }
            }

            MultiPrecision<N>[,] ret = new MultiPrecision<N>[n, n];
            v = v.T;

            for (int i = 0; i < n; i++) {
                for (int j = 0; j <= i; j++) {
                    MultiPrecision<N> s = 0d;

                    for (int k = i; k < n; k++) {
                        s += v.e[i, k] * v.e[j, k];
                    }

                    ret[i, j] = ret[j, i] = s;
                }
            }

            Matrix<N> w = new(ret, cloning: false);

            return w;
        }

        /// <summary>正定値対称行列に対する連立方程式の解</summary>
        public static Vector<N> SolvePositiveSymmetric(Matrix<N> m, Vector<N> v, bool enable_check_symmetric = true) {
            if (!IsSquare(m) || m.Size != v.Dim) {
                throw new ArgumentException("invalid size", $"{nameof(m)}, {nameof(v)}");
            }

            int n = m.Size;

            if (!IsFinite(m)) {
                return Vector<N>.Invalid(n);
            }

            Matrix<N> l = Cholesky(m, enable_check_symmetric);

            if (!IsFinite(l)) {
                return Vector<N>.Invalid(n);
            }

            v = v.Copy();

            // 前進消去
            for (int i = 0; i < n; i++) {
                MultiPrecision<N> inv_mii = 1 / l.e[i, i];
                v[i] *= inv_mii;

                for (int j = i + 1; j < n; j++) {
                    MultiPrecision<N> mul = l.e[j, i];
                    v[j] -= v[i] * mul;
                }
            }

            // 後退代入
            for (int i = n - 1; i >= 0; i--) {
                MultiPrecision<N> inv_mii = 1 / l.e[i, i];
                v[i] *= inv_mii;

                for (int j = i - 1; j >= 0; j--) {
                    MultiPrecision<N> mul = l.e[i, j];
                    v[j] -= v[i] * mul;
                }
            }

            return v;
        }
    }
}
