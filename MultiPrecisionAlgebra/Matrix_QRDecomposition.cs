using MultiPrecision;
using System;

namespace MultiPrecisionAlgebra {
    /// <summary>行列クラス</summary>
    public partial class Matrix<N> where N : struct, IConstant {
        /// <summary>QR分解</summary>
        public (Matrix<N> orthogonal_matrix, Matrix<N> triangular_matrix) QRDecomposition() {
            if (!IsSquare(this)) {
                throw new InvalidOperationException("not square matrix");
            }

            Matrix<N> q = new(Size, Size), r = new(Size, Size);

            int i, j, n = Size;

            Vector<N>[] e = new Vector<N>[n], u = new Vector<N>[n];
            for (i = 0; i < Size; i++) {
                e[i] = Vector<N>.Zero(n);
            }

            for (i = 0; i < n; i++) {
                Vector<N> ai = Vertical(i);

                u[i] = ai.Copy();

                for (j = 0; j < i; j++) {
                    u[i] -= u[j] * Vector<N>.InnerProduct(ai, u[j]) / u[j].SquareNorm;
                }

                e[i] = u[i].Normal;

                for (j = 0; j <= i; j++) {
                    r.e[j, i] = Vector<N>.InnerProduct(ai, e[j]);
                }

                for (j = 0; j < n; j++) {
                    q.e[j, i] = e[i].v[j];
                }
            }

            return (q, r);
        }
    }
}
