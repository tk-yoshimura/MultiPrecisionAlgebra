﻿using MultiPrecision;
using System.Diagnostics;

namespace MultiPrecisionAlgebra {
    /// <summary>行列クラス</summary>
    public partial class Matrix<N> where N : struct, IConstant {
        /// <summary>トレース</summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public MultiPrecision<N> Trace {
            get {
                MultiPrecision<N> sum = MultiPrecision<N>.Zero;
                foreach (var diagonal in Diagonals) {
                    sum += diagonal;
                }

                return sum;
            }
        }
    }
}
