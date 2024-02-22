using MultiPrecision;

namespace MultiPrecisionAlgebra {
    ///<summary>ベクトルクラス</summary>
    public partial class Vector<N> where N : struct, IConstant {
        /// <summary>メッシュグリッド</summary>
        public static (Vector<N> x, Vector<N> y) MeshGrid(Vector<N> x, Vector<N> y) {
            int n = checked(x.Dim * y.Dim);

            MultiPrecision<N>[] rx = x.v, ry = y.v;
            MultiPrecision<N>[] vx = new MultiPrecision<N>[n], vy = new MultiPrecision<N>[n];

            for (int i = 0, idx = 0; i < y.Dim; i++) {
                for (int j = 0; j < x.Dim; j++, idx++) {
                    vx[idx] = rx[j];
                    vy[idx] = ry[i];
                }
            }

            return (
                new Vector<N>(vx, cloning: false),
                new Vector<N>(vy, cloning: false)
            );
        }

        /// <summary>メッシュグリッド</summary>
        public static (Vector<N> x, Vector<N> y, Vector<N> z) MeshGrid(Vector<N> x, Vector<N> y, Vector<N> z) {
            int n = checked(x.Dim * y.Dim * z.Dim);

            MultiPrecision<N>[] rx = x.v, ry = y.v, rz = z.v;
            MultiPrecision<N>[] vx = new MultiPrecision<N>[n], vy = new MultiPrecision<N>[n], vz = new MultiPrecision<N>[n];

            for (int i = 0, idx = 0; i < z.Dim; i++) {
                for (int j = 0; j < y.Dim; j++) {
                    for (int k = 0; k < x.Dim; k++, idx++) {
                        vx[idx] = rx[k];
                        vy[idx] = ry[j];
                        vz[idx] = rz[i];
                    }
                }
            }

            return (
                new Vector<N>(vx, cloning: false),
                new Vector<N>(vy, cloning: false),
                new Vector<N>(vz, cloning: false)
            );
        }

        /// <summary>メッシュグリッド</summary>
        public static (Vector<N> x, Vector<N> y, Vector<N> z, Vector<N> w) MeshGrid(Vector<N> x, Vector<N> y, Vector<N> z, Vector<N> w) {
            int n = checked(x.Dim * y.Dim * z.Dim * w.Dim);

            MultiPrecision<N>[] rx = x.v, ry = y.v, rz = z.v, rw = w.v;
            MultiPrecision<N>[] vx = new MultiPrecision<N>[n], vy = new MultiPrecision<N>[n], vz = new MultiPrecision<N>[n], vw = new MultiPrecision<N>[n];

            for (int i = 0, idx = 0; i < w.Dim; i++) {
                for (int j = 0; j < z.Dim; j++) {
                    for (int k = 0; k < y.Dim; k++) {
                        for (int m = 0; m < x.Dim; m++, idx++) {
                            vx[idx] = rx[m];
                            vy[idx] = ry[k];
                            vz[idx] = rz[j];
                            vw[idx] = rw[i];
                        }
                    }
                }
            }

            return (
                new Vector<N>(vx, cloning: false),
                new Vector<N>(vy, cloning: false),
                new Vector<N>(vz, cloning: false),
                new Vector<N>(vw, cloning: false)
            );
        }
    }
}
