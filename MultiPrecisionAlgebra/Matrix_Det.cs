using MultiPrecision;

namespace MultiPrecisionAlgebra {
    /// <summary>行列クラス</summary>
    public partial class Matrix<N> where N : struct, IConstant {
        /// <summary>行列式</summary>
        public MultiPrecision<N> Det {
            get {
                (_, Matrix<N> upper) = LUDecomposition();

                MultiPrecision<N> prod = 1d;
                foreach (var diagonal in upper.Diagonals) {
                    prod *= diagonal;
                }

                return prod;
            }
        }
    }
}
