using MultiPrecision;
using System.Diagnostics;

namespace MultiPrecisionAlgebra {
    /// <summary>行列クラス</summary>
    public partial class Matrix<N> where N : struct, IConstant {
        private static MultiPrecision<N> Det2x2(Matrix<N> m) {
            Debug.Assert(m.Shape == (2, 2));

            MultiPrecision<N> det = m.e[0, 0] * m.e[1, 1] - m.e[0, 1] * m.e[1, 0];

            return det;
        }

        private static MultiPrecision<N> Det3x3(Matrix<N> m) {
            Debug.Assert(m.Shape == (3, 3));

            MultiPrecision<N> det =
                m.e[0, 0] * (m.e[1, 1] * m.e[2, 2] - m.e[2, 1] * m.e[1, 2]) +
                m.e[1, 0] * (m.e[2, 1] * m.e[0, 2] - m.e[0, 1] * m.e[2, 2]) +
                m.e[2, 0] * (m.e[0, 1] * m.e[1, 2] - m.e[1, 1] * m.e[0, 2]);

            return det;
        }

        /// <summary>行列式</summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public MultiPrecision<N> Det {
            get {
                if (Shape == (1, 1)) {
                    return e[0, 0];
                }
                if (Shape == (2, 2)) {
                    return Det2x2(this);
                }
                if (Shape == (3, 3)) {
                    return Det3x3(this);
                }

                (_, int pivot_det, _, Matrix<N> u) = LUKernel(this);

                MultiPrecision<N> prod = pivot_det;
                foreach (var diagonal in u.Diagonals) {
                    prod *= diagonal;
                }

                return prod;
            }
        }
    }
}
