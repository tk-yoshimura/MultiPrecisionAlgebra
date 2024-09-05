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
    }
}
