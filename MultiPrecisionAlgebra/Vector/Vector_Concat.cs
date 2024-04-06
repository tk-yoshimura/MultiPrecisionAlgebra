using MultiPrecision;
using System;
using System.Collections.Generic;

namespace MultiPrecisionAlgebra {
    ///<summary>ベクトルクラス</summary>
    public partial class Vector<N> where N : struct, IConstant {
        /// <summary>結合</summary>
        public static Vector<N> Concat(params object[] blocks) {
            List<MultiPrecision<N>> v = new();

            foreach (object obj in blocks) {
                if (obj is Vector<N> vector) {
                    v.AddRange(vector.v);
                }
                else if (obj is MultiPrecision<N> vmp) {
                    v.Add(vmp);
                }
                else if (obj is double vd) {
                    v.Add(vd);
                }
                else if (obj is int vi) {
                    v.Add(vi);
                }
                else if (obj is long vl) {
                    v.Add(vl);
                }
                else if (obj is float vf) {
                    v.Add(vf);
                }
                else if (obj is string vs) {
                    v.Add(vs);
                }
                else {
                    throw new ArgumentException($"unsupported type '{obj.GetType().Name}'", nameof(blocks));
                }
            }

            return new Vector<N>(v);
        }
    }
}
