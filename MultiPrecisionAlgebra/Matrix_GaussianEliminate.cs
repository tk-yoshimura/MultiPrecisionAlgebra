using MultiPrecision;
using System;

namespace MultiPrecisionAlgebra {
    /// <summary>行列クラス</summary>
    public partial class Matrix<N> where N : struct, IConstant {
        /// <summary>ガウスの消去法</summary>
        public static Matrix<N> GaussianEliminate(Matrix<N> m) {
            if (!IsSquare(m)) {
                throw new ArgumentException("invalid size", $"{m}");
            }

            int i, j, k, p, n = m.Rows;
            MultiPrecision<N> pivot, inv_mii, mul, swap;
            Matrix<N> v = Identity(m.Rows);
            m = m.Copy();

            for (i = 0; i < n; i++) {
                pivot = MultiPrecision<N>.Abs(m.e[i, i]);
                p = i;

                //ピボット選択
                for (j = i + 1; j < n; j++) {
                    if (MultiPrecision<N>.Abs(m.e[j, i]) > pivot) {
                        pivot = MultiPrecision<N>.Abs(m.e[j, i]);
                        p = j;
                    }
                }

                //ピボットが閾値以下ならばMは正則行列でないので逆行列は存在しない
                if (pivot.Exponent < -MultiPrecision<N>.Bits) {
                    return Invalid(v.Rows, v.Columns);
                }

                //行入れ替え
                if (p != i) {
                    for (j = 0; j < n; j++) {
                        swap = m.e[i, j];
                        m.e[i, j] = m.e[p, j];
                        m.e[p, j] = swap;
                    }

                    for (j = 0; j < v.Columns; j++) {
                        swap = v.e[i, j];
                        v.e[i, j] = v.e[p, j];
                        v.e[p, j] = swap;
                    }
                }

                // 前進消去
                inv_mii = 1d / m.e[i, i];
                m.e[i, i] = 1;
                for (j = i + 1; j < n; j++) {
                    m.e[i, j] *= inv_mii;
                }
                for (j = 0; j < v.Columns; j++) {
                    v.e[i, j] *= inv_mii;
                }

                for (j = i + 1; j < n; j++) {
                    mul = m.e[j, i];
                    m.e[j, i] = 0;
                    for (k = i + 1; k < n; k++) {
                        m.e[j, k] -= m.e[i, k] * mul;
                    }
                    for (k = 0; k < v.Columns; k++) {
                        v.e[j, k] -= v.e[i, k] * mul;
                    }
                }
            }

            // 後退代入
            for (i = n - 1; i >= 0; i--) {
                for (j = i - 1; j >= 0; j--) {
                    mul = m.e[j, i];
                    for (k = i; k < n; k++) {
                        m.e[j, k] = 0;
                    }
                    for (k = 0; k < v.Columns; k++) {
                        v.e[j, k] -= v.e[i, k] * mul;
                    }
                }
            }

            return v;
        }
    }
}
