using MultiPrecision;
using System;

namespace MultiPrecisionAlgebra {
    /// <summary>行列クラス</summary>
    public partial class Matrix<N> where N : struct, IConstant {
        /// <summary>QR分解</summary>
        public (Matrix<N> orthogonal_matrix, Matrix<N> triangular_matrix) QRDecompose() {
            if (!IsSquare(this)) {
                throw new InvalidOperationException("not square matrix");
            }

            int n = Size;

            Matrix<N> q = new(n, n), r = new(n, n);

            Vector<N>[] e = new Vector<N>[n], u = new Vector<N>[n];
            for (int i = 0; i < n; i++) {
                e[i] = Vector<N>.Zero(n);
            }

            for (int i = 0; i < n; i++) {
                Vector<N> ai = Vertical(i);

                u[i] = ai.Copy();

                for (int j = 0; j < i; j++) {
                    u[i] -= u[j] * Vector<N>.Dot(ai, u[j]) / u[j].SquareNorm;
                }

                e[i] = u[i].Normal;

                for (int j = 0; j <= i; j++) {
                    r.e[j, i] = Vector<N>.Dot(ai, e[j]);
                }

                for (int j = 0; j < n; j++) {
                    q.e[j, i] = e[i].v[j];
                }
            }

            return (q, r);
        }
    }
}
