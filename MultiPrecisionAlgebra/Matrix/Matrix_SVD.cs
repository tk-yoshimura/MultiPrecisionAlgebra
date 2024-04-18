using MultiPrecision;
using System.Diagnostics;
using System.Linq;

namespace MultiPrecisionAlgebra {
    /// <summary>行列クラス</summary>
    public partial class Matrix<N> where N : struct, IConstant {
        /// <summary>特異値分解</summary>
        public static (Matrix<N> u, Vector<N> s, Matrix<N> v) SVD(Matrix<N> m) {
            if (m.Rows < m.Columns) {
                (Matrix<N> ut, Vector<N> st, Matrix<N> vt) = SVD(m.T);

                return (vt, st, ut);
            }

            int row = m.Rows, col = m.Columns;

            Debug.Assert(row >= col);

            if (!IsFinite(m)) {
                return (Invalid(row), Vector<N>.Invalid(col), Invalid(col));
            }
            if (IsZero(m)) {
                return (Identity(row), Vector<N>.Zero(col), Identity(col));
            }

            long exponent = m.MaxExponent;

            Vector<N>[] us = ScaleB(m, -exponent).Verticals, vs = Identity(col).Verticals;
            MultiPrecision<N>[] sqnorms = us.Select(u => u.SquareNorm).ToArray();

            // jacobi rotate cos, sin
            static (MultiPrecision<N> c, MultiPrecision<N> s) jacobi_coef(MultiPrecision<N> dot_ii, MultiPrecision<N> dot_ij, MultiPrecision<N> dot_jj) {
                MultiPrecision<N> v = (dot_jj - dot_ii) / dot_ij;

                if (v.Exponent <= MultiPrecision<N>.Bits / 4) {
                    MultiPrecision<N> tau = MultiPrecision<N>.Ldexp(v, -1);
                    MultiPrecision<N> t = (MultiPrecision<N>.IsPositive(tau))
                        ? (1 / (MultiPrecision<N>.Sqrt(1 + tau * tau) + tau))
                        : (-1 / (MultiPrecision<N>.Sqrt(1 + tau * tau) - tau));

                    MultiPrecision<N> c = 1 / MultiPrecision<N>.Sqrt(1 + t * t);
                    MultiPrecision<N> s = t * c;

                    return (c, s);
                }
                else if (v.Exponent <= MultiPrecision<N>.Bits) {
                    MultiPrecision<N> u = 1 / v, u2 = MultiPrecision<N>.Square(u);

                    MultiPrecision<N> c = (16 + u2 * (-8 + u2 * (22 + u2 * -69))) / 16;
                    MultiPrecision<N> s = u * (16 + u2 * (-24 + u2 * (62 + u2 * -187))) / 16;

                    return (c, s);
                }
                else {
                    return (MultiPrecision<N>.One, MultiPrecision<N>.Zero);
                }
            }

            MultiPrecision<N> error_sum_prev = MultiPrecision<N>.NaN;

            // one-side jacobi method
            for (long iter = 0, max_iter = (long)MultiPrecision<N>.Length * col; iter < max_iter; iter++) {
                bool convergenced = true;

                MultiPrecision<N> error_sum = MultiPrecision<N>.Zero;

                for (int i = 0; i < col - 1; i++) {
                    for (int j = i + 1; j < col; j++) {
                        Vector<N> ui = us[i], uj = us[j];

                        MultiPrecision<N> dot_ii = sqnorms[i], dot_jj = sqnorms[j], dot_ij = Vector<N>.Dot(ui, uj);

                        (MultiPrecision<N> cos, MultiPrecision<N> sin) = jacobi_coef(dot_ii, dot_ij, dot_jj);

                        MultiPrecision<N> error = MultiPrecision<N>.Abs(sin);
                        error_sum += error;

                        if (error.Exponent < -MultiPrecision<N>.Bits + 8) {
                            continue;
                        }

                        us[i] = cos * ui - sin * uj;
                        us[j] = sin * ui + cos * uj;

                        sqnorms[i] = us[i].SquareNorm;
                        sqnorms[j] = us[j].SquareNorm;

                        Vector<N> vi = vs[i], vj = vs[j];
                        vs[i] = cos * vi - sin * vj;
                        vs[j] = sin * vi + cos * vj;

                        convergenced = false;
                    }
                }

                if (convergenced || !MultiPrecision<N>.IsFinite(error_sum) || (iter > col && error_sum_prev <= error_sum)) {
                    break;
                }

                error_sum_prev = error_sum;
            }

            // determine eigen value
            MultiPrecision<N>[] sigmas = new MultiPrecision<N>[col];

            for (int i = 0; i < col; i++) {
                MultiPrecision<N> sigma = us[i].Norm;

                sigmas[i] = sigma;

                us[i] /= sigma;
            }

            // reorder by eigen value
            int[] order = sigmas.Select((d, idx) => (d, idx)).OrderByDescending(item => MultiPrecision<N>.Abs(item.d)).Select(item => item.idx).ToArray();

            sigmas = order.Select(idx => sigmas[idx]).ToArray();
            us = order.Select(idx => us[idx]).ToArray();
            vs = order.Select(idx => vs[idx]).ToArray();

            // truncate near zero eigen
            for (int i = 0; i < sigmas.Length; i++) {
                if (sigmas[i].Exponent < -MultiPrecision<N>.Bits + 8) {
                    sigmas[i] = MultiPrecision<N>.Zero;
                    us = us[..i];
                    break;
                }
            }

            // generate ortho vector
            for (int i = us.Length; i < row; i++) {
                Vector<N> u_new = Vector<N>.Zero(row);
                u_new[i] = MultiPrecision<N>.One;

                for (int k = 0; k < us.Length; k++) {
                    u_new -= us[k] * us[k][i];
                }

                if (Vector<N>.IsZero(u_new)) {
                    Vector<N> b = Vector<N>.Fill(row, MultiPrecision<N>.One).Normal;
                    u_new = b;

                    for (int k = 0; k < us.Length; k++) {
                        u_new -= us[k] * Vector<N>.Dot(us[k], b);
                    }
                }

                u_new = u_new.Normal;

                us = [.. us, u_new];
            }

            Matrix<N> u = HConcat(us), v = HConcat(vs);
            Vector<N> s = Vector<N>.ScaleB(sigmas, exponent);

            return (u, s, v);
        }
    }
}
