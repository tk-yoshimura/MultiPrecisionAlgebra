﻿using MultiPrecision;
using System;

namespace MultiPrecisionAlgebra {
    /// <summary>行列クラス</summary>
    public partial class Matrix<N> where N : struct, IConstant {
        /// <summary>Any句</summary>
        public static bool Any(Matrix<N> matrix, Func<MultiPrecision<N>, bool> cond) {
            for (int i = 0; i < matrix.Rows; i++) {
                for (int j = 0; j < matrix.Columns; j++) {
                    if (cond(matrix.e[i, j])) {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>All句</summary>
        public static bool All(Matrix<N> matrix, Func<MultiPrecision<N>, bool> cond) {
            for (int i = 0; i < matrix.Rows; i++) {
                for (int j = 0; j < matrix.Columns; j++) {
                    if (!cond(matrix.e[i, j])) {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>Count句</summary>
        public static long Count(Matrix<N> matrix, Func<MultiPrecision<N>, bool> cond) {
            long cnt = 0;

            for (int i = 0; i < matrix.Rows; i++) {
                for (int j = 0; j < matrix.Columns; j++) {
                    if (cond(matrix.e[i, j])) {
                        cnt++;
                    }
                }
            }

            return cnt;
        }
    }
}
