using MultiPrecision;
using System;

namespace MultiPrecisionAlgebra {
    /// <summary>行列クラス</summary>
    public partial class Matrix<N> where N : struct, IConstant {
        /// <summary>LU分解</summary>
        public (Matrix<N> lower_matrix, Matrix<N> upper_matrix) LUDecomposition() {
            if (!IsSquare(this)) {
                throw new InvalidOperationException();
            }

            Matrix<N> m = Copy();
            Matrix<N> l = Zero(Size, Size), u = Zero(Size, Size);

            int i, j, k, n = Size;

            //LU分解
            for (i = 0; i < n; i++) {
                for (j = i + 1; j < n; j++) {
                    MultiPrecision<N> mul = m.e[j, i] /= m.e[i, i];
                    m.e[j, i] = mul;

                    for (k = i + 1; k < n; k++) {
                        m.e[j, k] -= m.e[i, k] * mul;
                    }
                }
            }

            //三角行列格納
            for (i = 0; i < n; i++) {
                l.e[i, i] = 1;
                for (j = 0; j < i; j++) {
                    l.e[i, j] = m.e[i, j];
                }
                for (; j < n; j++) {
                    u.e[i, j] = m.e[i, j];
                }
            }

            return (l, u);
        }
    }
}
