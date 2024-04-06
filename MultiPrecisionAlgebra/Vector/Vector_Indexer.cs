using MultiPrecision;
using System;

namespace MultiPrecisionAlgebra {
    ///<summary>ベクトルクラス</summary>
    public partial class Vector<N> where N : struct, IConstant {
        /// <summary>インデクサ</summary>
        public MultiPrecision<N> this[int index] {
            get => v[index];
            set => v[index] = value;
        }

        /// <summary>インデクサ</summary>
        public MultiPrecision<N> this[Index index] {
            get => v[index.GetOffset(Dim)];
            set => v[index.GetOffset(Dim)] = value;
        }

        /// <summary>領域インデクサ</summary>
        public Vector<N> this[Range range] {
            get {
                (int index, int counts) = range.GetOffsetAndLength(Dim);

                MultiPrecision<N>[] ret = new MultiPrecision<N>[counts];
                for (int i = 0; i < counts; i++) {
                    ret[i] = v[i + index];
                }

                return new(ret);
            }

            set {
                (int index, int counts) = range.GetOffsetAndLength(Dim);

                if (value.Dim != counts) {
                    throw new ArgumentOutOfRangeException(nameof(range));
                }

                for (int i = 0; i < counts; i++) {
                    v[i + index] = value.v[i];
                }
            }
        }
    }
}
