using MultiPrecision;
using System;

namespace MultiPrecisionAlgebra {
    ///<summary>ベクトルクラス</summary>
    public partial class Vector<N> where N : struct, IConstant {
        public static implicit operator Vector<N>((MultiPrecision<N> x, MultiPrecision<N> y) v) {
            return new Vector<N>(new MultiPrecision<N>[] { v.x, v.y }, cloning: false);
        }

        public void Deconstruct(out MultiPrecision<N> x, out MultiPrecision<N> y) {
            if (Dim != 2) {
                throw new InvalidOperationException($"vector dim={Dim}");
            }

            (x, y) = (v[0], v[1]);
        }

        public static implicit operator Vector<N>((MultiPrecision<N> x, MultiPrecision<N> y, MultiPrecision<N> z) v) {
            return new Vector<N>(new MultiPrecision<N>[] { v.x, v.y, v.z }, cloning: false);
        }

        public void Deconstruct(out MultiPrecision<N> x, out MultiPrecision<N> y, out MultiPrecision<N> z) {
            if (Dim != 3) {
                throw new InvalidOperationException($"vector dim={Dim}");
            }

            (x, y, z) = (v[0], v[1], v[2]);
        }

        public static implicit operator Vector<N>((MultiPrecision<N> x, MultiPrecision<N> y, MultiPrecision<N> z, MultiPrecision<N> w) v) {
            return new Vector<N>(new MultiPrecision<N>[] { v.x, v.y, v.z, v.w }, cloning: false);
        }

        public void Deconstruct(out MultiPrecision<N> x, out MultiPrecision<N> y, out MultiPrecision<N> z, out MultiPrecision<N> w) {
            if (Dim != 4) {
                throw new InvalidOperationException($"vector dim={Dim}");
            }

            (x, y, z, w) = (v[0], v[1], v[2], v[3]);
        }

        public static implicit operator Vector<N>((MultiPrecision<N> e0, MultiPrecision<N> e1, MultiPrecision<N> e2, MultiPrecision<N> e3, MultiPrecision<N> e4) v) {
            return new Vector<N>(new MultiPrecision<N>[] { v.e0, v.e1, v.e2, v.e3, v.e4 }, cloning: false);
        }

        public void Deconstruct(out MultiPrecision<N> e0, out MultiPrecision<N> e1, out MultiPrecision<N> e2, out MultiPrecision<N> e3, out MultiPrecision<N> e4) {
            if (Dim != 5) {
                throw new InvalidOperationException($"vector dim={Dim}");
            }

            (e0, e1, e2, e3, e4) = (v[0], v[1], v[2], v[3], v[4]);
        }

        public static implicit operator Vector<N>((MultiPrecision<N> e0, MultiPrecision<N> e1, MultiPrecision<N> e2, MultiPrecision<N> e3, MultiPrecision<N> e4, MultiPrecision<N> e5) v) {
            return new Vector<N>(new MultiPrecision<N>[] { v.e0, v.e1, v.e2, v.e3, v.e4, v.e5 }, cloning: false);
        }

        public void Deconstruct(out MultiPrecision<N> e0, out MultiPrecision<N> e1, out MultiPrecision<N> e2, out MultiPrecision<N> e3, out MultiPrecision<N> e4, out MultiPrecision<N> e5) {
            if (Dim != 6) {
                throw new InvalidOperationException($"vector dim={Dim}");
            }

            (e0, e1, e2, e3, e4, e5) = (v[0], v[1], v[2], v[3], v[4], v[5]);
        }

        public static implicit operator Vector<N>((MultiPrecision<N> e0, MultiPrecision<N> e1, MultiPrecision<N> e2, MultiPrecision<N> e3, MultiPrecision<N> e4, MultiPrecision<N> e5, MultiPrecision<N> e6) v) {
            return new Vector<N>(new MultiPrecision<N>[] { v.e0, v.e1, v.e2, v.e3, v.e4, v.e5, v.e6 }, cloning: false);
        }

        public void Deconstruct(out MultiPrecision<N> e0, out MultiPrecision<N> e1, out MultiPrecision<N> e2, out MultiPrecision<N> e3, out MultiPrecision<N> e4, out MultiPrecision<N> e5, out MultiPrecision<N> e6) {
            if (Dim != 7) {
                throw new InvalidOperationException($"vector dim={Dim}");
            }

            (e0, e1, e2, e3, e4, e5, e6) = (v[0], v[1], v[2], v[3], v[4], v[5], v[6]);
        }

        public static implicit operator Vector<N>((MultiPrecision<N> e0, MultiPrecision<N> e1, MultiPrecision<N> e2, MultiPrecision<N> e3, MultiPrecision<N> e4, MultiPrecision<N> e5, MultiPrecision<N> e6, MultiPrecision<N> e7) v) {
            return new Vector<N>(new MultiPrecision<N>[] { v.e0, v.e1, v.e2, v.e3, v.e4, v.e5, v.e6, v.e7 }, cloning: false);
        }

        public void Deconstruct(out MultiPrecision<N> e0, out MultiPrecision<N> e1, out MultiPrecision<N> e2, out MultiPrecision<N> e3, out MultiPrecision<N> e4, out MultiPrecision<N> e5, out MultiPrecision<N> e6, out MultiPrecision<N> e7) {
            if (Dim != 8) {
                throw new InvalidOperationException($"vector dim={Dim}");
            }

            (e0, e1, e2, e3, e4, e5, e6, e7) = (v[0], v[1], v[2], v[3], v[4], v[5], v[6], v[7]);
        }
    }
}
