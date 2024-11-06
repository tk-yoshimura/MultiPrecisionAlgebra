using MultiPrecision;
using System;
using System.Diagnostics;
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
            if (m.Size == 2) {
                return SortEigenByNorm(EigenValues2x2(m));
            }

            precision_level = precision_level >= 0 ? precision_level : MultiPrecision<N>.Length * m.Size;

            int n = m.Size, notconverged = n;
            long exponent = m.MaxExponent;
            (Matrix<N> u, _, _) = PermutateDiagonal(ScaleB(m, -exponent));

            Vector<N> eigen_values = Vector<N>.Fill(n, 1);
            Vector<N> eigen_values_prev = eigen_values.Copy();

            Vector<N> eigen_diffnorms = Vector<N>.Fill(n, MultiPrecision<N>.PositiveInfinity);
            Vector<N> eigen_diffnorms_prev = eigen_diffnorms.Copy();

            Matrix<N> d = u;

            for (int iter_qr = 0; iter_qr <= precision_level; iter_qr++) {
                if (d.Size > 2) {
                    MultiPrecision<N> mu = EigenValues2x2(d[^2.., ^2..])[1];

                    if (MultiPrecision<N>.IsFinite(mu)) {
                        (Matrix<N> q, Matrix<N> r) = QR(DiagonalAdd(d, -mu));
                        d = DiagonalAdd(r * q, mu);
                    }
                    else {
                        (Matrix<N> q, Matrix<N> r) = QR(d);
                        d = r * q;
                    }

                    eigen_values[..d.Size] = d.Diagonals[..d.Size];
                }
                else {
                    eigen_values[..2] = EigenValues2x2(d);
                }

                for (int i = notconverged - 1; i >= 0; i--) {
                    MultiPrecision<N> eigen_diffnorm = MultiPrecision<N>.Abs(eigen_values[i] - eigen_values_prev[i]);
                    eigen_diffnorms[i] = eigen_diffnorm;
                }

                for (int i = notconverged - 1; i >= 0; i--) {
                    if (i >= 2 && iter_qr < precision_level) {
                        if (eigen_diffnorms[i].Exponent > -MultiPrecision<N>.Bits + 8 || eigen_diffnorms_prev[i] > eigen_diffnorms[i]) {
                            break;
                        }
                    }

                    notconverged--;
                }

                if (notconverged <= 0) {
                    break;
                }

                if (d.Size > 2) {
                    Vector<N> lower = d[^1, ..^1];
                    MultiPrecision<N> eigen = d[^1, ^1];

                    if (lower.MaxExponent < eigen.Exponent - MultiPrecision<N>.Bits) {
                        d = d[..^1, ..^1];
                    }
                }

                eigen_values_prev[..notconverged] = eigen_values[..notconverged];
                eigen_diffnorms_prev[..notconverged] = eigen_diffnorms[..notconverged];
            }

            eigen_values = Vector<N>.ScaleB(eigen_values, exponent);

            return SortEigenByNorm(eigen_values);
        }

        /// <summary>固有値・固有ベクトル</summary>
        /// <param name="eigen_values">固有値</param>
        /// <param name="eigen_vectors">固有ベクトル</param>
        /// <param name="precision_level">精度(収束ループを回す回数)</param>
        public static (MultiPrecision<N>[] eigen_values, Vector<N>[] eigen_vectors) EigenValueVectors(Matrix<N> m, int precision_level = -1) {
            if (!IsSquare(m) || m.Size < 1) {
                throw new ArgumentException("not square matrix", nameof(m));
            }

            if (IsZero(m)) {
                return (Vector<N>.Zero(m.Size), Identity(m.Size).Horizontals);
            }
            if (m.Size <= 1) {
                return ([m[0, 0]], [new Vector<N>(1)]);
            }
            if (m.Size == 2) {
                return SortEigenByNorm(EigenValueVectors2x2(m));
            }

            precision_level = precision_level >= 0 ? precision_level : MultiPrecision<N>.Length * m.Size;

            int n = m.Size, notconverged = n;
            long exponent = m.MaxExponent;
            (Matrix<N> u, _, int[] perm_indexes) = PermutateDiagonal(ScaleB(m, -exponent));

            Vector<N> eigen_values = Vector<N>.Fill(n, 1);
            Vector<N> eigen_values_prev = eigen_values.Copy();

            Vector<N> eigen_diffnorms = Vector<N>.Fill(n, MultiPrecision<N>.PositiveInfinity);
            Vector<N> eigen_diffnorms_prev = eigen_diffnorms.Copy();

            Vector<N>[] eigen_vectors = Identity(n).Horizontals;

            Matrix<N> d = u;

            for (int iter_qr = 0; iter_qr <= precision_level; iter_qr++) {
                if (d.Size > 2) {
                    MultiPrecision<N> mu = EigenValues2x2(d[^2.., ^2..])[1];

                    if (MultiPrecision<N>.IsFinite(mu)) {
                        (Matrix<N> q, Matrix<N> r) = QR(DiagonalAdd(d, -mu));
                        d = DiagonalAdd(r * q, mu);
                    }
                    else {
                        (Matrix<N> q, Matrix<N> r) = QR(d);
                        d = r * q;
                    }

                    eigen_values[..d.Size] = d.Diagonals[..d.Size];
                }
                else {
                    eigen_values[..2] = EigenValues2x2(d);
                }

                for (int i = notconverged - 1; i >= 0; i--) {
                    MultiPrecision<N> eigen_diffnorm = MultiPrecision<N>.Abs(eigen_values[i] - eigen_values_prev[i]);
                    eigen_diffnorms[i] = eigen_diffnorm;
                }

                for (int i = notconverged - 1; i >= 0; i--) {
                    if (i >= 2 && iter_qr < precision_level) {
                        if (eigen_diffnorms[i].Exponent > -MultiPrecision<N>.Bits + 8 || eigen_diffnorms_prev[i] > eigen_diffnorms[i]) {
                            break;
                        }
                    }

                    MultiPrecision<N> eigen_val = eigen_values[i];

                    Vector<N> v = u[.., i], h = u[i, ..];
                    MultiPrecision<N> nondiagonal_absmax = MultiPrecision<N>.Zero;
                    for (int k = 0; k < v.Dim; k++) {
                        if (k == i) {
                            continue;
                        }

                        nondiagonal_absmax =
                            MultiPrecision<N>.Max(MultiPrecision<N>.Max(
                                nondiagonal_absmax, MultiPrecision<N>.Abs(v[k])), MultiPrecision<N>.Abs(h[k])
                            );
                    }

                    MultiPrecision<N> eps = MultiPrecision<N>.Ldexp(nondiagonal_absmax, -MultiPrecision<N>.Bits + 32);

                    Matrix<N> g = DiagonalAdd(u, -eigen_val + eps).Inverse;

                    Vector<N> x;

                    if (IsFinite(g)) {
                        MultiPrecision<N> norm, norm_prev = MultiPrecision<N>.NaN;
                        x = Vector<N>.Fill(n, 0.125);
                        x[i] = MultiPrecision<N>.One;

                        for (int iter_vector = 0; iter_vector < precision_level; iter_vector++) {
                            x = (g * x).Normal;

                            norm = (u * x - eigen_val * x).Norm;

                            if (norm.Exponent < -MultiPrecision<N>.Bits / 2 && norm >= norm_prev) {
                                break;
                            }

                            norm_prev = norm;
                        }
                    }
                    else {
                        x = Vector<N>.Zero(n);
                        x[i] = MultiPrecision<N>.One;
                    }

                    eigen_vectors[i] = x[perm_indexes];
                    notconverged--;
                }

                if (notconverged <= 0) {
                    break;
                }

                if (d.Size > 2) {
                    Vector<N> lower = d[^1, ..^1];
                    MultiPrecision<N> eigen = d[^1, ^1];

                    if (lower.MaxExponent < eigen.Exponent - MultiPrecision<N>.Bits) {
                        d = d[..^1, ..^1];
                    }
                }

                eigen_values_prev[..notconverged] = eigen_values[..notconverged];
                eigen_diffnorms_prev[..notconverged] = eigen_diffnorms[..notconverged];
            }

            eigen_values = Vector<N>.ScaleB(eigen_values, exponent);

            return SortEigenByNorm((eigen_values, eigen_vectors));
        }

        private static MultiPrecision<N>[] EigenValues2x2(Matrix<N> m) {
            Debug.Assert(m.Size == 2);

            MultiPrecision<N> m00 = m[0, 0], m11 = m[1, 1];
            MultiPrecision<N> m01 = m[0, 1], m10 = m[1, 0];

            MultiPrecision<N> b = m00 + m11, c = m00 - m11;

            MultiPrecision<N> d = MultiPrecision<N>.Sqrt(c * c + 4 * m01 * m10);

            MultiPrecision<N> val0 = (b + d) / 2;
            MultiPrecision<N> val1 = (b - d) / 2;

            if (MultiPrecision<N>.Abs(val0 - m11) >= MultiPrecision<N>.Abs(val1 - m11)) {
                return [val0, val1];
            }
            else {
                return [val1, val0];
            }
        }

        private static (MultiPrecision<N>[] eigen_values, Vector<N>[] eigen_vectors) EigenValueVectors2x2(Matrix<N> m) {
            Debug.Assert(m.Size == 2);

            MultiPrecision<N> m00 = m[0, 0], m11 = m[1, 1];
            MultiPrecision<N> m01 = m[0, 1], m10 = m[1, 0];

            long diagonal_scale = long.Max(m00.Exponent, m11.Exponent);

            long m10_scale = m10.Exponent, m01_scale = m01.Exponent;

            if (diagonal_scale - m10_scale < MultiPrecision<N>.Bits && !MultiPrecision<N>.IsZero(m10)) {
                MultiPrecision<N> b = m00 + m11, c = m00 - m11;

                MultiPrecision<N> d = MultiPrecision<N>.Sqrt(c * c + 4 * m01 * m10);

                MultiPrecision<N> val0 = (b + d) / 2;
                MultiPrecision<N> val1 = (b - d) / 2;

                Vector<N> vec0 = new Vector<N>((c + d) / (2 * m10), 1).Normal;
                Vector<N> vec1 = new Vector<N>((c - d) / (2 * m10), 1).Normal;

                if (MultiPrecision<N>.Abs(val0 - m11) >= MultiPrecision<N>.Abs(val1 - m11)) {
                    return (new MultiPrecision<N>[] { val0, val1 }, new Vector<N>[] { vec0, vec1 });
                }
                else {
                    return (new MultiPrecision<N>[] { val1, val0 }, new Vector<N>[] { vec1, vec0 });
                }
            }
            else if (diagonal_scale - m01_scale < MultiPrecision<N>.Bits && !MultiPrecision<N>.IsZero(m01)) {
                MultiPrecision<N> b = m00 + m11, c = m00 - m11;

                MultiPrecision<N> d = MultiPrecision<N>.Sqrt(c * c + 4 * m01 * m10);

                MultiPrecision<N> val0 = (b + d) / 2;
                MultiPrecision<N> val1 = (b - d) / 2;

                Vector<N> vec0 = new Vector<N>(1, (c + d) / (2 * m01)).Normal;
                Vector<N> vec1 = new Vector<N>(1, (c - d) / (2 * m01)).Normal;

                if (MultiPrecision<N>.Abs(val0 - m11) >= MultiPrecision<N>.Abs(val1 - m11)) {
                    return (new MultiPrecision<N>[] { val0, val1 }, new Vector<N>[] { vec0, vec1 });
                }
                else {
                    return (new MultiPrecision<N>[] { val1, val0 }, new Vector<N>[] { vec1, vec0 });
                }
            }
            else {
                if (m00 != m11) {
                    Vector<N> vec0 = (1, 0);
                    Vector<N> vec1 = new Vector<N>(m01 / (m11 - m00), 1).Normal;

                    return (new MultiPrecision<N>[] { m00, m11 }, new Vector<N>[] { vec0, vec1 });
                }
                else {
                    return (new MultiPrecision<N>[] { m00, m11 }, new Vector<N>[] { (1, 0), (0, 1) });
                }
            }
        }

        private static MultiPrecision<N>[] SortEigenByNorm(MultiPrecision<N>[] eigen_values) {
            MultiPrecision<N>[] eigen_values_sorted = [.. eigen_values.OrderByDescending(MultiPrecision<N>.Abs)];

            return eigen_values_sorted;
        }

        private static (MultiPrecision<N>[] eigen_values, Vector<N>[] eigen_vectors) SortEigenByNorm((MultiPrecision<N>[] eigen_values, Vector<N>[] eigen_vectors) eigens) {
            Debug.Assert(eigens.eigen_values.Length == eigens.eigen_vectors.Length);

            IOrderedEnumerable<(MultiPrecision<N> val, Vector<N> vec)> eigens_sorted =
                eigens.eigen_values.Zip(eigens.eigen_vectors).OrderByDescending(item => MultiPrecision<N>.Abs(item.First));

            MultiPrecision<N>[] eigen_values_sorted = eigens_sorted.Select(item => item.val).ToArray();
            Vector<N>[] eigen_vectors_sorted = eigens_sorted.Select(item => item.vec).ToArray();

            return (eigen_values_sorted, eigen_vectors_sorted);
        }

        private static (Matrix<N> matrix, int[] indexes, int[] indexes_invert) PermutateDiagonal(Matrix<N> m) {
            Debug.Assert(IsSquare(m));

            int n = m.Size;

            Vector<N> rates = Vector<N>.Zero(n);

            MultiPrecision<N> eps = MultiPrecision<N>.Ldexp(1, -MultiPrecision<N>.Bits * 16);

            for (int i = 0; i < n; i++) {
                MultiPrecision<N> diagonal = m[i, i];

                Vector<N> nondigonal = Vector<N>.Concat(m[i, ..i], m[i, (i + 1)..]);

                MultiPrecision<N> nondigonal_norm = nondigonal.Norm;

                MultiPrecision<N> rate = MultiPrecision<N>.Abs(diagonal) / (nondigonal_norm + eps);

                rates[i] = rate;
            }

            int[] indexes = rates.Select(item => (item.index, item.val)).OrderBy(item => item.val).Select(item => item.index).ToArray();

            Matrix<N> m_perm = m[indexes, indexes];

            int[] indexes_invert = new int[n];

            for (int i = 0; i < n; i++) {
                indexes_invert[indexes[i]] = i;
            }

            return (m_perm, indexes, indexes_invert);
        }
    }
}
