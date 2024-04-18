using MultiPrecision;
using System;
using System.Linq;

namespace MultiPrecisionAlgebra {
    /// <summary>行列クラス</summary>
    public partial class Matrix<N> where N : struct, IConstant {
        /// <summary>固有値計算</summary>
        /// <param name="precision_level">精度(収束ループを回す回数)</param>
        public static MultiPrecision<N>[] EigenValues(Matrix<N> m, int precision_level = -1) {
            if (!IsSquare(m) || m.Size < 1) {
                throw new ArgumentException("not square matrix", nameof(m));
            }

            if (m.Size <= 1) {
                return [m[0, 0]];
            }

            precision_level = precision_level >= 0 ? precision_level : MultiPrecision<N>.DecimalDigits;


            for (int iter = 0; iter < precision_level; iter++) {
                (Matrix<N> q, Matrix<N> r) = QR(m);
                m = r * q;
            }

            return m.Diagonals;
        }

        /// <summary>固有値・固有ベクトル</summary>
        /// <param name="eigen_values">固有値</param>
        /// <param name="eigen_vectors">固有ベクトル</param>
        /// <param name="precision_level">精度(収束ループを回す回数)</param>
        public static (MultiPrecision<N>[] eigen_values, Vector<N>[] eigen_vectors) EigenValueVectors(Matrix<N> m, int precision_level = -1) {
            if (!IsSquare(m) || m.Size < 1) {
                throw new ArgumentException("not square matrix", nameof(m));
            }

            if (m.Size <= 1) {
                return ([m[0, 0]], [new Vector<N>(1)]);
            }

            precision_level = precision_level >= 0 ? precision_level : MultiPrecision<N>.DecimalDigits;

            int n = m.Size;
            bool[] is_convergenced = new bool[n];
            MultiPrecision<N>[] eigen_values = Vector<N>.Fill(n, 1);
            Vector<N>[] eigen_vectors = Identity(n).Horizontals;

            Matrix<N> d = m, identity = Identity(n);

            Matrix<N>[] gs_prev = new Matrix<N>[n];

            for (int iter_qr = 0; iter_qr < precision_level; iter_qr++) {
                (Matrix<N> q, Matrix<N> r) = QR(d);
                d = r * q;

                eigen_values = d.Diagonals;

                for (int i = 0; i < n; i++) {
                    if (is_convergenced[i]) {
                        continue;
                    }

                    if (iter_qr < precision_level - 1) {
                        Matrix<N> h = m - eigen_values[i] * identity;
                        Matrix<N> g = h.Inverse;
                        if (IsFinite(g) && g.Norm < MultiPrecision<N>.Ldexp(h.Norm, MultiPrecision<N>.Bits - 2)) {
                            gs_prev[i] = g;
                            continue;
                        }

                        if (gs_prev[i] is null) {
                            is_convergenced[i] = true;
                            continue;
                        }
                    }

                    Matrix<N> gp = ScaleB(gs_prev[i], -gs_prev[i].MaxExponent);

                    MultiPrecision<N> norm, norm_prev = MultiPrecision<N>.NaN;
                    Vector<N> x = Vector<N>.Fill(n, 0.125), x_prev = x;
                    x[i] = MultiPrecision<N>.One;

                    for (int iter_vector = 0; iter_vector < precision_level; iter_vector++) {
                        x = (gp * x).Normal;

                        if (MultiPrecision<N>.IsNegative(Vector<N>.Dot(x, x_prev))) {
                            x = -x;
                        }

                        norm = (x - x_prev).Norm;

                        if (norm.Exponent < -MultiPrecision<N>.Bits ||
                            (norm.Exponent < -MultiPrecision<N>.Bits + 8 && norm >= norm_prev)) {

                            break;
                        }

                        x_prev = x;
                        norm_prev = norm;
                    }

                    eigen_vectors[i] = x;
                    is_convergenced[i] = true;
                }

                if (is_convergenced.All(b => b)) {
                    break;
                }
            }

            return (eigen_values, eigen_vectors);
        }
    }
}
