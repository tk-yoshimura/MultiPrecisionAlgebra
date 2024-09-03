using MultiPrecision;
using System.Collections.Generic;
using System;
using System.Linq;

namespace MultiPrecisionAlgebra {
    /// <summary>行列クラス</summary>
    public partial class Matrix<N> where N : struct, IConstant {
        /// <summary>特異値分解</summary>
        public static (Matrix<N> u, Vector<N> s, Matrix<N> v) SVD(Matrix<N> m) {
            if (m.Rows < 1 || m.Columns < 1) {
                throw new ArgumentException("empty matrix", nameof(m));
            }

            if (m.Rows >= m.Columns) {
                (Vector<N> eigen_vals, Vector<N>[] eigen_vecs) = EigenValueVectors(m.T * m);

                Matrix<N> v = HConcat(eigen_vecs);
                Vector<N> s = (x => x.Sign == Sign.Plus ? MultiPrecision<N>.Sqrt(x) : 0d, eigen_vals);
                Matrix<N> l = m * v;

                for (int i = 0; i < s.Dim; i++) {
                    l[.., i] /= s[i];
                }

                List<Vector<N>> ls = [.. l.Verticals];
                GramSchmidtMethod(ls);

                Matrix<N> u = HConcat(ls.ToArray());

                return (u, s, v);
            }
            else {
                (Vector<N> eigen_vals, Vector<N>[] eigen_vecs) = EigenValueVectors(m * m.T);

                Matrix<N> u = HConcat(eigen_vecs);
                Vector<N> s = (x => x.Sign == Sign.Plus ? MultiPrecision<N>.Sqrt(x) : 0d, eigen_vals);
                Matrix<N> r = u.T * m;

                for (int i = 0; i < s.Dim; i++) {
                    r[i, ..] /= s[i];
                }

                List<Vector<N>> rs = [.. r.Horizontals];
                GramSchmidtMethod(rs);

                Matrix<N> v = HConcat(rs.ToArray());

                return (u, s, v);
            }
        }

        private static void GramSchmidtMethod(List<Vector<N>> vs) {
            int n = vs[0].Dim;

            for (int k = vs.Count - 1; k >= 0; k--) {
                if (Vector<N>.IsFinite(vs[k]) && vs[k].Norm >= 0.75) {
                    break;
                }
                vs.RemoveAt(k);
            }

            if (vs.Count < 1) {
                vs.AddRange(Identity(n).Verticals);
                return;
            }

            List<Vector<N>> init_vecs = [];
            for (int i = 0; i < n; i++) {
                Vector<N> v = Vector<N>.Zero(n);
                v[i] = 1;
                init_vecs.Add(v);
            }

            while (vs.Count < n) {
                Vector<N> g = init_vecs.OrderBy(u => vs.Select(v => MultiPrecision<N>.Square(Vector<N>.Dot(u, v))).Sum()).First();

                Vector<N> v = g;

                for (int i = 0; i < vs.Count; i++) {
                    v -= vs[i] * Vector<N>.Dot(g, vs[i]) / vs[i].SquareNorm;
                }

                v = v.Normal;

                vs.Add(v);
            }
        }
    }
}
