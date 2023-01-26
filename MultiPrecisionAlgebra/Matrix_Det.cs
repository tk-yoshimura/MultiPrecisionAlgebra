using MultiPrecision;
using System.Diagnostics;

namespace MultiPrecisionAlgebra {
    /// <summary>行列クラス</summary>
    public partial class Matrix<N> where N : struct, IConstant {
        /// <summary>行列式</summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public MultiPrecision<N> Det {
            get {
                (_, Matrix<N> upper) = LUDecompose();

                MultiPrecision<N> prod = 1;
                foreach (var diagonal in upper.Diagonals) {
                    prod *= diagonal;
                }

                return prod;
            }
        }
    }
}
