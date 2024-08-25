using MultiPrecision;
using System;
using System.Collections.Generic;
using System.Linq;

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

        /// <summary>配列インデクサ</summary>
        public Vector<N> this[int[] indexes] {
            get {
                MultiPrecision<N>[] ret = new MultiPrecision<N>[indexes.Length];
                for (int i = 0; i < indexes.Length; i++) {
                    ret[i] = v[indexes[i]];
                }

                return new(ret);
            }

            set {
                if (value.Dim != indexes.Length) {
                    throw new ArgumentException("invalid size", nameof(indexes));
                }

                for (int i = 0; i < indexes.Length; i++) {
                    v[indexes[i]] = value.v[i];
                }
            }
        }

        /// <summary>配列インデクサ</summary>
        public Vector<N> this[IEnumerable<int> indexes] {
            get => this[indexes.ToArray()];
            set => this[indexes.ToArray()] = value;
        }
    }
}
