using MultiPrecision;
using System;

namespace MultiPrecisionAlgebra {
    ///<summary>ベクトルクラス</summary>
    public partial class Vector<N> where N : struct, IConstant {
        /// <summary>Any句</summary>
        public static bool Any(Vector<N> vector, Func<MultiPrecision<N>, bool> cond) {
            for (int i = 0; i < vector.Dim; i++) {
                if (cond(vector.v[i])) {
                    return true;
                }
            }

            return false;
        }

        /// <summary>All句</summary>
        public static bool All(Vector<N> vector, Func<MultiPrecision<N>, bool> cond) {
            for (int i = 0; i < vector.Dim; i++) {
                if (!cond(vector.v[i])) {
                    return false;
                }
            }

            return true;
        }

        /// <summary>Count句</summary>
        public static long Count(Vector<N> vector, Func<MultiPrecision<N>, bool> cond) {
            long cnt = 0;

            for (int i = 0; i < vector.Dim; i++) {
                if (cond(vector.v[i])) {
                    cnt++;
                }
            }

            return cnt;
        }
    }
}
