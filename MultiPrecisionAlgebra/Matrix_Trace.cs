using MultiPrecision;

namespace MultiPrecisionAlgebra {
    /// <summary>行列クラス</summary>
    public partial class Matrix<N> where N : struct, IConstant {
        /// <summary>トレース</summary>
        public MultiPrecision<N> Trace {
            get {
                (_, Matrix<N> upper) = LUDecomposition();

                MultiPrecision<N> sum = 0;
                foreach (var diagonal in upper.Diagonals) {
                    sum += diagonal;
                }

                return sum;
            }
        }
    }
}
