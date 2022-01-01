using MultiPrecision;
using System;

namespace MultiPrecisionAlgebra {
    /// <summary>行列クラス</summary>
    public partial class Matrix<N> where N : struct, IConstant {
        /// <summary>固有値計算</summary>
        /// <param name="precision_level">精度(収束ループを回す回数)</param>
        public MultiPrecision<N>[] CalculateEigenValues(int precision_level = 28) {
            Matrix<N> m = Copy();
            for (int i = 0; i < precision_level; i++) {
                (Matrix<N> q, Matrix<N> r) = m.QRDecomposition();
                m = r * q;
            }

            return m.Diagonals;
        }

        /// <summary>固有値・固有ベクトル</summary>
        /// <param name="eigen_values">固有値</param>
        /// <param name="eigen_vectors">固有ベクトル</param>
        /// <param name="precision_level">精度(収束ループを回す回数)</param>
        public (MultiPrecision<N>[] eigen_values, Vector<N>[] eigen_vectors) CalculateEigenValueVectors(int precision_level = 32) {
            if (!IsSquare(this)) {
                throw new InvalidOperationException();
            }

            MultiPrecision<N>[] eigen_values = null;
            Vector<N>[] eigen_vectors = new Vector<N>[Size];

            const int vector_converge_times = 3;

            MultiPrecision<N> eigen_value;
            bool[] is_converged_vector = new bool[Size];
            Matrix<N> m = Copy(), g;
            Vector<N> x_init = Vector<N>.Zero(Size), x;

            for (int i = 0; i < Size; i++) {
                eigen_vectors[i] = Vector<N>.Invalid(Size);
                x_init.v[i] = 1;
            }
            x_init /= x_init.Norm;

            for (int i = 0; i < precision_level; i++) {
                (Matrix<N> q, Matrix<N> r) = m.QRDecomposition();
                m = r * q;

                eigen_values = m.Diagonals;

                bool is_all_converged = true;

                for (int j = 0; j < Size; j++) {
                    if (is_converged_vector[j]) {
                        continue;
                    }

                    is_all_converged = false;

                    eigen_value = eigen_values[j];

                    g = (this - eigen_value * Identity(Size)).Inverse;
                    if (!IsValid(g)) {
                        is_converged_vector[j] = true;
                        break;
                    }

                    x = x_init;

                    for (int k = 0; k < vector_converge_times; k++) {
                        x = (g * x).Normal;
                    }

                    eigen_vectors[j] = x;
                }

                if (is_all_converged) {
                    break;
                }
            }

            return (eigen_values, eigen_vectors);
        }
    }
}
