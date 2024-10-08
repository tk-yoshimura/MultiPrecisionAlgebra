﻿using MultiPrecision;
using System;
using System.Diagnostics;

namespace MultiPrecisionAlgebra {
    /// <summary>行列クラス</summary>
    public partial class Matrix<N> where N : struct, IConstant {
        private static Matrix<N> Invert2x2(Matrix<N> m) {
            Debug.Assert(m.Shape == (2, 2));

            long exponent = m.MaxExponent;
            m = ScaleB(m, -exponent);

            return new Matrix<N>(
                new MultiPrecision<N>[,] {
                    { m.e[1, 1], -m.e[0, 1] },
                    { -m.e[1, 0], m.e[0, 0] }
                }, cloning: false
            ) / MultiPrecision<N>.Ldexp(Det2x2(m), exponent);
        }

        private static Matrix<N> Invert3x3(Matrix<N> m) {
            Debug.Assert(m.Shape == (3, 3));

            long exponent = m.MaxExponent;
            m = ScaleB(m, -exponent);

            return new Matrix<N>(
                new MultiPrecision<N>[,] {
                    { m.e[1, 1] * m.e[2, 2] - m.e[1, 2] * m.e[2, 1],
                      m.e[0, 2] * m.e[2, 1] - m.e[0, 1] * m.e[2, 2],
                      m.e[0, 1] * m.e[1, 2] - m.e[0, 2] * m.e[1, 1] },
                    { m.e[1, 2] * m.e[2, 0] - m.e[1, 0] * m.e[2, 2],
                      m.e[0, 0] * m.e[2, 2] - m.e[0, 2] * m.e[2, 0],
                      m.e[0, 2] * m.e[1, 0] - m.e[0, 0] * m.e[1, 2] },
                    { m.e[1, 0] * m.e[2, 1] - m.e[1, 1] * m.e[2, 0],
                      m.e[0, 1] * m.e[2, 0] - m.e[0, 0] * m.e[2, 1],
                      m.e[0, 0] * m.e[1, 1] - m.e[0, 1] * m.e[1, 0] }
                }, cloning: false
            ) / MultiPrecision<N>.Ldexp(Det3x3(m), exponent);
        }

        /// <summary>ガウスの消去法</summary>
        private static Matrix<N> GaussianEliminate(Matrix<N> m) {
            if (!IsSquare(m)) {
                throw new ArgumentException("not square matrix", nameof(m));
            }

            int n = m.Size;

            if (!IsFinite(m)) {
                return Invalid(n, n);
            }

            long exponent = m.MaxExponent;
            Matrix<N> v = Identity(m.Rows), u = ScaleB(m, -exponent);

            for (int i = 0; i < n; i++) {
                MultiPrecision<N> pivot = MultiPrecision<N>.Abs(u.e[i, i]);
                int p = i;

                //ピボット選択
                for (int j = i + 1; j < n; j++) {
                    if (MultiPrecision<N>.Abs(u.e[j, i]) > pivot) {
                        pivot = MultiPrecision<N>.Abs(u.e[j, i]);
                        p = j;
                    }
                }

                //ピボットが閾値以下ならばMは正則行列でないので逆行列は存在しない
                if (pivot.Exponent <= -MultiPrecision<N>.Bits + 4) {
                    return Invalid(v.Rows, v.Columns);
                }

                //行入れ替え
                if (p != i) {
                    for (int j = 0; j < n; j++) {
                        (u.e[p, j], u.e[i, j]) = (u.e[i, j], u.e[p, j]);
                    }

                    for (int j = 0; j < n; j++) {
                        (v.e[p, j], v.e[i, j]) = (v.e[i, j], v.e[p, j]);
                    }
                }

                // 前進消去
                MultiPrecision<N> inv_mii = 1 / u.e[i, i];
                u.e[i, i] = 1;
                for (int j = i + 1; j < n; j++) {
                    u.e[i, j] *= inv_mii;
                }
                for (int j = 0; j < n; j++) {
                    v.e[i, j] *= inv_mii;
                }

                for (int j = i + 1; j < n; j++) {
                    MultiPrecision<N> mul = u.e[j, i];
                    u.e[j, i] = MultiPrecision<N>.Zero;
                    for (int k = i + 1; k < n; k++) {
                        u.e[j, k] -= u.e[i, k] * mul;
                    }
                    for (int k = 0; k < n; k++) {
                        v.e[j, k] -= v.e[i, k] * mul;
                    }
                }
            }

            // 後退代入
            for (int i = n - 1; i >= 0; i--) {
                for (int j = i - 1; j >= 0; j--) {
                    MultiPrecision<N> mul = u.e[j, i];
                    for (int k = i; k < n; k++) {
                        u.e[j, k] = MultiPrecision<N>.Zero;
                    }
                    for (int k = 0; k < n; k++) {
                        v.e[j, k] -= v.e[i, k] * mul;
                    }
                }
            }

            v = ScaleB(v, -exponent);

            return v;
        }

        /// <summary>連立方程式の解</summary>
        public static Vector<N> Solve(Matrix<N> m, Vector<N> v) {
            if (!IsSquare(m) || m.Size != v.Dim) {
                throw new ArgumentException("invalid size", $"{nameof(m)}, {nameof(v)}");
            }

            int n = m.Size;

            if (!IsFinite(m)) {
                return Vector<N>.Invalid(n);
            }

            long exponent = m.MaxExponent;
            Matrix<N> u = ScaleB(m, -exponent);
            v = v.Copy();

            for (int i = 0; i < n; i++) {
                MultiPrecision<N> pivot = MultiPrecision<N>.Abs(u.e[i, i]);
                int p = i;

                //ピボット選択
                for (int j = i + 1; j < n; j++) {
                    if (MultiPrecision<N>.Abs(u.e[j, i]) > pivot) {
                        pivot = MultiPrecision<N>.Abs(u.e[j, i]);
                        p = j;
                    }
                }

                //ピボットが閾値以下ならばMは正則行列でないので解は存在しない
                if (pivot.Exponent <= -MultiPrecision<N>.Bits + 4) {
                    return Vector<N>.Invalid(v.Dim);
                }

                //行入れ替え
                if (p != i) {
                    for (int j = 0; j < n; j++) {
                        (u.e[p, j], u.e[i, j]) = (u.e[i, j], u.e[p, j]);
                    }

                    (v[p], v[i]) = (v[i], v[p]);
                }

                // 前進消去
                MultiPrecision<N> inv_mii = 1 / u.e[i, i];
                u.e[i, i] = MultiPrecision<N>.One;
                for (int j = i + 1; j < n; j++) {
                    u.e[i, j] *= inv_mii;
                }
                v[i] *= inv_mii;

                for (int j = i + 1; j < n; j++) {
                    MultiPrecision<N> mul = u.e[j, i];
                    u.e[j, i] = MultiPrecision<N>.Zero;
                    for (int k = i + 1; k < n; k++) {
                        u.e[j, k] -= u.e[i, k] * mul;
                    }
                    v[j] -= v[i] * mul;
                }
            }

            // 後退代入
            for (int i = n - 1; i >= 0; i--) {
                for (int j = i - 1; j >= 0; j--) {
                    MultiPrecision<N> mul = u.e[j, i];
                    for (int k = i; k < n; k++) {
                        u.e[j, k] = MultiPrecision<N>.Zero;
                    }
                    v[j] -= v[i] * mul;
                }
            }

            v = Vector<N>.ScaleB(v, -exponent);

            return v;
        }
    }
}
