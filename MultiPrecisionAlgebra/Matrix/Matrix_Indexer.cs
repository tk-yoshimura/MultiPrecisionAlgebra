﻿using MultiPrecision;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MultiPrecisionAlgebra {
    /// <summary>行列クラス</summary>
    public partial class Matrix<N> where N : struct, IConstant {
        /// <summary>インデクサ</summary>
        /// <param name="row_index">行</param>
        /// <param name="column_index">列</param>
        public MultiPrecision<N> this[int row_index, int column_index] {
            get => e[row_index, column_index];
            set => e[row_index, column_index] = value;
        }

        /// <summary>インデクサ</summary>
        /// <param name="row_index">行</param>
        /// <param name="column_index">列</param>
        public MultiPrecision<N> this[Index row_index, Index column_index] {
            get => e[row_index.GetOffset(Rows), column_index.GetOffset(Columns)];
            set => e[row_index.GetOffset(Rows), column_index.GetOffset(Columns)] = value;
        }

        /// <summary>領域インデクサ</summary>
        /// <param name="row_range">行</param>
        /// <param name="column_range">列</param>
        public Matrix<N> this[Range row_range, Range column_range] {
            get {
                (int ri, int rn) = row_range.GetOffsetAndLength(Rows);
                (int ci, int cn) = column_range.GetOffsetAndLength(Columns);

                MultiPrecision<N>[,] m = new MultiPrecision<N>[rn, cn];
                for (int i = 0; i < rn; i++) {
                    for (int j = 0; j < cn; j++) {
                        m[i, j] = e[i + ri, j + ci];
                    }
                }

                return new(m);
            }

            set {
                (int ri, int rn) = row_range.GetOffsetAndLength(Rows);
                (int ci, int cn) = column_range.GetOffsetAndLength(Columns);

                if (value.Rows != rn || value.Columns != cn) {
                    throw new ArgumentOutOfRangeException($"{nameof(row_range)},{nameof(column_range)}");
                }

                for (int i = 0; i < rn; i++) {
                    for (int j = 0; j < cn; j++) {
                        e[i + ri, j + ci] = value.e[i, j];
                    }
                }
            }
        }

        /// <summary>領域インデクサ</summary>
        /// <param name="row_range">行</param>
        /// <param name="column_index">列</param>
        public Vector<N> this[Range row_range, Index column_index] {
            get {
                int c = column_index.GetOffset(Columns);
                (int ri, int rn) = row_range.GetOffsetAndLength(Rows);

                MultiPrecision<N>[] m = new MultiPrecision<N>[rn];
                for (int i = 0; i < rn; i++) {
                    m[i] = e[i + ri, c];
                }

                return new(m);
            }

            set {
                int c = column_index.GetOffset(Columns);
                (int ri, int rn) = row_range.GetOffsetAndLength(Rows);

                if (value.Dim != rn) {
                    throw new ArgumentOutOfRangeException($"{nameof(row_range)}");
                }

                for (int i = 0; i < rn; i++) {
                    e[i + ri, c] = value.v[i];
                }
            }
        }

        /// <summary>領域インデクサ</summary>
        /// <param name="row_index">行</param>
        /// <param name="column_range">列</param>
        public Vector<N> this[Index row_index, Range column_range] {
            get {
                int r = row_index.GetOffset(Rows);
                (int ci, int cn) = column_range.GetOffsetAndLength(Columns);

                MultiPrecision<N>[] m = new MultiPrecision<N>[cn];
                for (int j = 0; j < cn; j++) {
                    m[j] = e[r, j + ci];
                }

                return new(m);
            }

            set {
                int r = row_index.GetOffset(Rows);
                (int ci, int cn) = column_range.GetOffsetAndLength(Columns);

                if (value.Dim != cn) {
                    throw new ArgumentOutOfRangeException($"{nameof(column_range)}");
                }

                for (int j = 0; j < cn; j++) {
                    e[r, j + ci] = value.v[j];
                }
            }
        }

        /// <summary>配列インデクサ</summary>
        public Matrix<N> this[int[] row_indexes, int[] column_indexes] {
            get {
                MultiPrecision<N>[,] m = new MultiPrecision<N>[row_indexes.Length, column_indexes.Length];
                for (int i = 0; i < row_indexes.Length; i++) {
                    for (int j = 0; j < column_indexes.Length; j++) {
                        m[i, j] = e[row_indexes[i], column_indexes[j]];
                    }
                }

                return new(m);
            }

            set {
                if (value.Shape != (row_indexes.Length, column_indexes.Length)) {
                    throw new ArgumentException("invalid size", $"{nameof(row_indexes)}, {nameof(column_indexes)}");
                }

                for (int i = 0; i < row_indexes.Length; i++) {
                    for (int j = 0; j < column_indexes.Length; j++) {
                        e[row_indexes[i], column_indexes[j]] = value.e[i, j];
                    }
                }
            }
        }

        /// <summary>配列インデクサ</summary>
        public Matrix<N> this[IEnumerable<int> row_indexes, IEnumerable<int> column_indexes] {
            get => this[row_indexes.ToArray(), column_indexes.ToArray()];
            set => this[row_indexes.ToArray(), column_indexes.ToArray()] = value;
        }

        /// <summary>配列インデクサ</summary>
        public Matrix<N> this[int[] row_indexes, Range column_range] {
            get {
                (int ci, int cn) = column_range.GetOffsetAndLength(Columns);

                MultiPrecision<N>[,] m = new MultiPrecision<N>[row_indexes.Length, cn];
                for (int i = 0; i < row_indexes.Length; i++) {
                    for (int j = 0; j < cn; j++) {
                        m[i, j] = e[row_indexes[i], j + ci];
                    }
                }

                return new(m);
            }

            set {
                (int ci, int cn) = column_range.GetOffsetAndLength(Columns);

                if (value.Shape != (row_indexes.Length, cn)) {
                    throw new ArgumentException("invalid size", $"{nameof(row_indexes)}, {nameof(column_range)}");
                }

                for (int i = 0; i < row_indexes.Length; i++) {
                    for (int j = 0; j < cn; j++) {
                        e[row_indexes[i], j + ci] = value.e[i, j];
                    }
                }
            }
        }

        /// <summary>配列インデクサ</summary>
        public Matrix<N> this[IEnumerable<int> row_indexes, Range column_range] {
            get => this[row_indexes.ToArray(), column_range];
            set => this[row_indexes.ToArray(), column_range] = value;
        }

        /// <summary>配列インデクサ</summary>
        public Matrix<N> this[Range row_range, int[] column_indexes] {
            get {
                (int ri, int rn) = row_range.GetOffsetAndLength(Rows);

                MultiPrecision<N>[,] m = new MultiPrecision<N>[rn, column_indexes.Length];
                for (int i = 0; i < rn; i++) {
                    for (int j = 0; j < column_indexes.Length; j++) {
                        m[i, j] = e[i + ri, column_indexes[j]];
                    }
                }

                return new(m);
            }

            set {
                (int ri, int rn) = row_range.GetOffsetAndLength(Rows);

                if (value.Shape != (rn, column_indexes.Length)) {
                    throw new ArgumentException("invalid size", $"{nameof(row_range)}, {nameof(column_indexes)}");
                }

                for (int i = 0; i < rn; i++) {
                    for (int j = 0; j < column_indexes.Length; j++) {
                        e[i + ri, column_indexes[j]] = value.e[i, j];
                    }
                }
            }
        }

        /// <summary>配列インデクサ</summary>
        public Matrix<N> this[Range row_range, IEnumerable<int> column_indexes] {
            get => this[row_range, column_indexes.ToArray()];
            set => this[row_range, column_indexes.ToArray()] = value;
        }
    }
}
