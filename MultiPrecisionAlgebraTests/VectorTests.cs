using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;
using System;

namespace MultiPrecisionAlgebra.Tests {
    [TestClass()]
    public class VectorTests {
        [TestMethod()]
        public void VectorTest() {
            Vector<Pow2.N4> vector = new(1, 2, 3, 4);

            Assert.AreEqual(1, vector[0]);
            Assert.AreEqual(2, vector[1]);
            Assert.AreEqual(3, vector[2]);
            Assert.AreEqual(4, vector[3]);

            Assert.AreEqual(1, vector.X);
            Assert.AreEqual(2, vector.Y);
            Assert.AreEqual(3, vector.Z);
            Assert.AreEqual(4, vector.W);

            Assert.AreEqual(4, vector.Dim);

            vector.X = 4;
            vector.Y = 3;
            vector.Z = 2;
            vector.W = 1;

            Assert.AreEqual(4, vector.X);
            Assert.AreEqual(3, vector.Y);
            Assert.AreEqual(2, vector.Z);
            Assert.AreEqual(1, vector.W);

            vector[0] = 1;
            vector[1] = 2;
            vector[2] = 3;
            vector[3] = 4;

            Assert.AreEqual(1, vector[0]);
            Assert.AreEqual(2, vector[1]);
            Assert.AreEqual(3, vector[2]);
            Assert.AreEqual(4, vector[3]);

            vector[^1] = 1;
            vector[^2] = 2;
            vector[^3] = 3;
            vector[^4] = 4;

            Assert.AreEqual(1, vector[^1]);
            Assert.AreEqual(2, vector[^2]);
            Assert.AreEqual(3, vector[^3]);
            Assert.AreEqual(4, vector[^4]);

            string str = string.Empty;
            foreach ((int index, MultiPrecision<Pow2.N4> val) in vector) {
                str += $"({index},{val}),";
            }

            Assert.AreEqual("(0,4),(1,3),(2,2),(3,1),", str);
        }

        [TestMethod()]
        public void RangeIndexerGetterTest() {
            Vector<Pow2.N4> vector = new(1, 2, 3, 4, 5);

            Assert.AreEqual(new Vector<Pow2.N4>(1, 2, 3, 4, 5), vector[..]);

            Assert.AreEqual(new Vector<Pow2.N4>(2, 3, 4, 5), vector[1..]);
            Assert.AreEqual(new Vector<Pow2.N4>(3, 4, 5), vector[2..]);
            Assert.AreEqual(new Vector<Pow2.N4>(1, 2, 3, 4), vector[..^1]);
            Assert.AreEqual(new Vector<Pow2.N4>(1, 2, 3, 4), vector[..4]);
            Assert.AreEqual(new Vector<Pow2.N4>(1, 2, 3), vector[..^2]);
            Assert.AreEqual(new Vector<Pow2.N4>(1, 2, 3), vector[..3]);

            Assert.AreEqual(new Vector<Pow2.N4>(2, 3, 4), vector[1..4]);
            Assert.AreEqual(new Vector<Pow2.N4>(2, 3, 4), vector[1..^1]);
        }

        [TestMethod()]
        public void RangeIndexerSetterTest() {
            Vector<Pow2.N4> vector_src = new(1, 2, 3, 4, 5);
            Vector<Pow2.N4> vector_dst;

            vector_dst = Vector<Pow2.N4>.Zero(vector_src.Dim);
            vector_dst[..] = vector_src;
            Assert.AreEqual(new Vector<Pow2.N4>(1, 2, 3, 4, 5), vector_dst);

            vector_dst = Vector<Pow2.N4>.Zero(vector_src.Dim);
            vector_dst[1..] = vector_src[1..];
            Assert.AreEqual(new Vector<Pow2.N4>(0, 2, 3, 4, 5), vector_dst);

            vector_dst = Vector<Pow2.N4>.Zero(vector_src.Dim);
            vector_dst[2..] = vector_src[2..];
            Assert.AreEqual(new Vector<Pow2.N4>(0, 0, 3, 4, 5), vector_dst);

            vector_dst = Vector<Pow2.N4>.Zero(vector_src.Dim);
            vector_dst[..^1] = vector_src[..^1];
            Assert.AreEqual(new Vector<Pow2.N4>(1, 2, 3, 4, 0), vector_dst);

            vector_dst = Vector<Pow2.N4>.Zero(vector_src.Dim);
            vector_dst[..4] = vector_src[..4];
            Assert.AreEqual(new Vector<Pow2.N4>(1, 2, 3, 4, 0), vector_dst);

            vector_dst = Vector<Pow2.N4>.Zero(vector_src.Dim);
            vector_dst[..^2] = vector_src[..^2];
            Assert.AreEqual(new Vector<Pow2.N4>(1, 2, 3, 0, 0), vector_dst);

            vector_dst = Vector<Pow2.N4>.Zero(vector_src.Dim);
            vector_dst[..3] = vector_src[..3];
            Assert.AreEqual(new Vector<Pow2.N4>(1, 2, 3, 0, 0), vector_dst);

            vector_dst = Vector<Pow2.N4>.Zero(vector_src.Dim);
            vector_dst[1..4] = vector_src[1..4];
            Assert.AreEqual(new Vector<Pow2.N4>(0, 2, 3, 4, 0), vector_dst);

            vector_dst = Vector<Pow2.N4>.Zero(vector_src.Dim);
            vector_dst[1..^1] = vector_src[1..^1];
            Assert.AreEqual(new Vector<Pow2.N4>(0, 2, 3, 4, 0), vector_dst);

            vector_dst = Vector<Pow2.N4>.Zero(vector_src.Dim);
            vector_dst[0..^2] = vector_src[1..^1];
            Assert.AreEqual(new Vector<Pow2.N4>(2, 3, 4, 0, 0), vector_dst);

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => {
                vector_dst = Vector<Pow2.N4>.Zero(vector_src.Dim);
                vector_dst[0..^2] = vector_src[1..^2];
            });

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => {
                vector_dst = Vector<Pow2.N4>.Zero(vector_src.Dim);
                vector_dst[0..^2] = vector_src[1..];
            });
        }

        [TestMethod()]
        public void NormTest() {
            Vector<Pow2.N4> vector = new(-3, 4);

            Assert.AreEqual(5, vector.Norm);
            Assert.AreEqual(25, vector.SquareNorm);
        }

        [TestMethod()]
        public void SumTest() {
            Vector<Pow2.N4> vector = new(1, 2, 3, 4);

            Assert.AreEqual(10, vector.Sum);
        }

        [TestMethod()]
        public void NormalTest() {
            Vector<Pow2.N4> vector = new(1, 2, -3);

            Assert.AreEqual(1 / MultiPrecision<Pow2.N4>.Sqrt(1 + 4 + 9), vector.Normal.X);
            Assert.AreEqual(2 / MultiPrecision<Pow2.N4>.Sqrt(1 + 4 + 9), vector.Normal.Y);
            Assert.AreEqual(-3 / MultiPrecision<Pow2.N4>.Sqrt(1 + 4 + 9), vector.Normal.Z);
        }

        [TestMethod()]
        public void OperatorTest() {
            Vector<Pow2.N4> vector1 = new(1, 2);
            Vector<Pow2.N4> vector2 = new(3, 4);

            Assert.AreEqual(new Vector<Pow2.N4>(1, 2), +vector1);
            Assert.AreEqual(new Vector<Pow2.N4>(-1, -2), -vector1);
            Assert.AreEqual(new Vector<Pow2.N4>(4, 6), vector1 + vector2);
            Assert.AreEqual(new Vector<Pow2.N4>(4, 6), vector2 + vector1);
            Assert.AreEqual(new Vector<Pow2.N4>(4, 5), vector1 + 3);
            Assert.AreEqual(new Vector<Pow2.N4>(4, 5), 3 + vector1);
            Assert.AreEqual(new Vector<Pow2.N4>(-2, -2), vector1 - vector2);
            Assert.AreEqual(new Vector<Pow2.N4>(2, 2), vector2 - vector1);
            Assert.AreEqual(new Vector<Pow2.N4>(-2, -1), vector1 - 3);
            Assert.AreEqual(new Vector<Pow2.N4>(2, 1), 3 - vector1);
            Assert.AreEqual(new Vector<Pow2.N4>(2, 4), vector1 * 2);
            Assert.AreEqual(new Vector<Pow2.N4>(2, 4), 2 * vector1);
            Assert.AreEqual(new Vector<Pow2.N4>(0.5, 1), vector1 / 2);
            Assert.AreEqual(new Vector<Pow2.N4>(2, 1), 2 / vector1);
            Assert.AreEqual(new Vector<Pow2.N4>(3, 8), vector1 * vector2);
            Assert.AreEqual(new Vector<Pow2.N4>(MultiPrecision<Pow2.N4>.Div(1, 3), 0.5), vector1 / vector2);
            Assert.AreEqual(new Vector<Pow2.N4>(3, 2), vector2 / vector1);
        }

        [TestMethod()]
        public void DotTest() {
            Vector<Pow2.N4> vector1 = new(1, 2);
            Vector<Pow2.N4> vector2 = new(3, 4);
            Vector<Pow2.N4> vector3 = new(2, -1);

            Assert.AreEqual(11, Vector<Pow2.N4>.Dot(vector1, vector2));
            Assert.AreEqual(11, Vector<Pow2.N4>.Dot(vector2, vector1));
            Assert.AreEqual(0, Vector<Pow2.N4>.Dot(vector1, vector3));
            Assert.AreEqual(2, Vector<Pow2.N4>.Dot(vector2, vector3));
        }

        [TestMethod()]
        public void CrossTest() {
            Vector<Pow2.N4> vector1 = new(1, 2, 3);
            Vector<Pow2.N4> vector2 = new(4, -5, -6);
            Vector<Pow2.N4> vector3 = new(3, 1, 0);
            Vector<Pow2.N4> vector4 = new(2, 5, 1);

            Assert.AreEqual(Vector<Pow2.N4>.Zero(3), Vector<Pow2.N4>.Cross(vector1, vector2) + Vector<Pow2.N4>.Cross(vector2, vector1));
            Assert.AreEqual(new Vector<Pow2.N4>(1, -3, 13), Vector<Pow2.N4>.Cross(vector3, vector4));
        }

        [TestMethod()]
        public void PolynomialTest() {
            Vector<Pow2.N4> x = new(1, 2, 3, 4);
            Vector<Pow2.N4> coef = new(5, 7, 11, 13, 17);

            Assert.AreEqual(new Vector<Pow2.N4>(53, 439, 1853, 5393), Vector<Pow2.N4>.Polynomial(x, coef));
            Assert.AreEqual(13, Vector<Pow2.N4>.Polynomial(-1, coef));

            Assert.AreEqual(new Vector<Pow2.N4>(5, 5, 5, 5), Vector<Pow2.N4>.Polynomial(x, new Vector<Pow2.N4>(5)));
            Assert.AreEqual(5, Vector<Pow2.N4>.Polynomial(-1, new Vector<Pow2.N4>(5)));

            Assert.AreEqual(new Vector<Pow2.N4>(0, 0, 0, 0), Vector<Pow2.N4>.Polynomial(x, new Vector<Pow2.N4>(new double[0])));
            Assert.AreEqual(0, Vector<Pow2.N4>.Polynomial(-1, new Vector<Pow2.N4>(new double[0])));
        }

        [TestMethod()]
        public void DistanceTest() {
            Vector<Pow2.N4> vector1 = new(1, 2);
            Vector<Pow2.N4> vector2 = new(2, 1);

            Assert.AreEqual(MultiPrecision<Pow2.N4>.Sqrt(2), Vector<Pow2.N4>.Distance(vector1, vector2));
            Assert.AreEqual(2, Vector<Pow2.N4>.SquareDistance(vector1, vector2));
        }

        [TestMethod()]
        public void ZeroTest() {
            Vector<Pow2.N4> vector = Vector<Pow2.N4>.Zero(3);

            Assert.AreEqual(0, vector.X);
            Assert.AreEqual(0, vector.Y);
            Assert.AreEqual(0, vector.Z);
        }

        [TestMethod()]
        public void FillTest() {
            Vector<Pow2.N4> vector = Vector<Pow2.N4>.Fill(3, value: 7);

            Assert.AreEqual(7, vector.X);
            Assert.AreEqual(7, vector.Y);
            Assert.AreEqual(7, vector.Z);
        }

        [TestMethod()]
        public void ArangeTest() {
            Vector<Pow2.N4> vector = Vector<Pow2.N4>.Arange(8);

            for (int i = 0; i < vector.Dim; i++) {
                Assert.AreEqual(i, vector[i]);
            }
        }

        [TestMethod()]
        public void InvalidTest() {
            Vector<Pow2.N4> vector = Vector<Pow2.N4>.Invalid(3);

            Assert.IsTrue(MultiPrecision<Pow2.N4>.IsNaN(vector.X));
            Assert.IsTrue(MultiPrecision<Pow2.N4>.IsNaN(vector.Y));
            Assert.IsTrue(MultiPrecision<Pow2.N4>.IsNaN(vector.Z));
        }

        [TestMethod()]
        public void IsZeroTest() {
            Vector<Pow2.N4> vector = Vector<Pow2.N4>.Zero(3);

            Assert.IsTrue(Vector<Pow2.N4>.IsZero(vector));

            vector.X = 1;

            Assert.IsFalse(Vector<Pow2.N4>.IsZero(vector));
        }

        [TestMethod()]
        public void IsValidTest() {
            Vector<Pow2.N4> vector = Vector<Pow2.N4>.Zero(3);

            Assert.IsTrue(Vector<Pow2.N4>.IsValid(vector));

            vector.X = double.NaN;

            Assert.IsFalse(Vector<Pow2.N4>.IsValid(vector));
        }

        [TestMethod()]
        public void OperatorEqualTest() {
            Vector<Pow2.N4> vector = new(1, 2);

            Assert.IsTrue(vector == new Vector<Pow2.N4>(1, 2));
            Assert.IsTrue(vector != new Vector<Pow2.N4>(2, 1));
            Assert.IsTrue(vector != new Vector<Pow2.N4>(1));
            Assert.IsTrue(vector != new Vector<Pow2.N4>(1, 2, 3));
            Assert.IsFalse(vector == null);
            Assert.IsTrue(vector != null);
            Assert.IsTrue(vector != new Vector<Pow2.N4>(1, double.NaN));
        }

        [TestMethod()]
        public void EqualsTest() {
            Vector<Pow2.N4> vector = new(1, 2);

            Assert.IsTrue(vector.Equals(new Vector<Pow2.N4>(1, 2)));
            Assert.IsFalse(vector.Equals(new Vector<Pow2.N4>(2, 1)));
            Assert.IsFalse(vector.Equals(new Vector<Pow2.N4>(1)));
            Assert.IsFalse(vector.Equals(new Vector<Pow2.N4>(1, 2, 3)));
            Assert.IsFalse(vector.Equals(null));
            Assert.IsFalse(vector.Equals(new Vector<Pow2.N4>(1, double.NaN)));
        }

        [TestMethod()]
        public void FuncTest() {
            Vector<Pow2.N4> vector1 = new(1, 2, 4, 8);
            Vector<Pow2.N4> vector2 = new(2, 3, 5, 9);
            Vector<Pow2.N4> vector3 = new(3, 4, 6, 10);
            Vector<Pow2.N4> vector4 = new(4, 5, 7, 11);
            Vector<Pow2.N4> vector5 = new(5, 6, 8, 12, 20);

            Assert.AreEqual(new Vector<Pow2.N4>(2, 4, 8, 16), Vector<Pow2.N4>.Func(vector1, v => 2 * v));
            Assert.AreEqual(new Vector<Pow2.N4>(5, 8, 14, 26), Vector<Pow2.N4>.Func(vector1, vector2, (v1, v2) => v1 + 2 * v2));
            Assert.AreEqual(new Vector<Pow2.N4>(17, 24, 38, 66), Vector<Pow2.N4>.Func(vector1, vector2, vector3, (v1, v2, v3) => v1 + 2 * v2 + 4 * v3));
            Assert.AreEqual(new Vector<Pow2.N4>(49, 64, 94, 154), Vector<Pow2.N4>.Func(vector1, vector2, vector3, vector4, (v1, v2, v3, v4) => v1 + 2 * v2 + 4 * v3 + 8 * v4));
            Assert.AreEqual(new Vector<Pow2.N4>(49, 64, 94, 154), Vector<Pow2.N4>.Func(vector1, vector2, vector3, vector4, (v1, v2, v3, v4) => v1 + 2 * v2 + 4 * v3 + 8 * v4));

            Assert.ThrowsException<ArgumentException>(() => {
                Vector<Pow2.N4>.Func(vector1, vector5, (v1, v2) => v1 + 2 * v2);
            });

            Assert.ThrowsException<ArgumentException>(() => {
                Vector<Pow2.N4>.Func(vector5, vector1, (v1, v2) => v1 + 2 * v2);
            });

            Assert.ThrowsException<ArgumentException>(() => {
                Vector<Pow2.N4>.Func(vector1, vector2, vector5, (v1, v2, v3) => v1 + 2 * v2 + 4 * v3);
            });

            Assert.ThrowsException<ArgumentException>(() => {
                Vector<Pow2.N4>.Func(vector1, vector5, vector2, (v1, v2, v3) => v1 + 2 * v2 + 4 * v3);
            });

            Assert.ThrowsException<ArgumentException>(() => {
                Vector<Pow2.N4>.Func(vector5, vector1, vector2, (v1, v2, v3) => v1 + 2 * v2 + 4 * v3);
            });

            Assert.ThrowsException<ArgumentException>(() => {
                Vector<Pow2.N4>.Func(vector1, vector2, vector3, vector5, (v1, v2, v3, v4) => v1 + 2 * v2 + 4 * v3 + 8 * v4);
            });

            Assert.ThrowsException<ArgumentException>(() => {
                Vector<Pow2.N4>.Func(vector1, vector2, vector5, vector3, (v1, v2, v3, v4) => v1 + 2 * v2 + 4 * v3 + 8 * v4);
            });

            Assert.ThrowsException<ArgumentException>(() => {
                Vector<Pow2.N4>.Func(vector1, vector5, vector2, vector3, (v1, v2, v3, v4) => v1 + 2 * v2 + 4 * v3 + 8 * v4);
            });

            Assert.ThrowsException<ArgumentException>(() => {
                Vector<Pow2.N4>.Func(vector5, vector1, vector2, vector3, (v1, v2, v3, v4) => v1 + 2 * v2 + 4 * v3 + 8 * v4);
            });
        }

        [TestMethod()]
        public void ConcatTest() {
            Vector<Pow2.N4> vector1 = Vector<Pow2.N4>.Fill(1, value: -1);
            Vector<Pow2.N4> vector2 = Vector<Pow2.N4>.Fill(2, value: -2);
            Vector<Pow2.N4> vector4 = Vector<Pow2.N4>.Fill(4, value: -3);

            Assert.AreEqual(new Vector<Pow2.N4>(-1, -2, -2, -3, -3, -3, -3), Vector<Pow2.N4>.Concat(vector1, vector2, vector4));
            Assert.AreEqual(new Vector<Pow2.N4>(-2, -2, -3, -3, -3, -3, -1), Vector<Pow2.N4>.Concat(vector2, vector4, vector1));
            Assert.AreEqual(new Vector<Pow2.N4>(-1, -2, -2, 1, -3, -3, -3, -3), Vector<Pow2.N4>.Concat(vector1, vector2, 1, vector4));
            Assert.AreEqual(new Vector<Pow2.N4>(-1, -2, -2, 2, -3, -3, -3, -3), Vector<Pow2.N4>.Concat(vector1, vector2, 2L, vector4));
            Assert.AreEqual(new Vector<Pow2.N4>(-1, -2, -2, 3, -3, -3, -3, -3), Vector<Pow2.N4>.Concat(vector1, vector2, (MultiPrecision<Pow2.N4>)3, vector4));
            Assert.AreEqual(new Vector<Pow2.N4>(-1, -2, -2, 4, -3, -3, -3, -3), Vector<Pow2.N4>.Concat(vector1, vector2, 4d, vector4));
            Assert.AreEqual(new Vector<Pow2.N4>(-1, -2, -2, 5, -3, -3, -3, -3), Vector<Pow2.N4>.Concat(vector1, vector2, 5f, vector4));
            Assert.AreEqual(new Vector<Pow2.N4>(-1, -2, -2, "6.2", -3, -3, -3, -3), Vector<Pow2.N4>.Concat(vector1, vector2, "6.2", vector4));

            Assert.ThrowsException<ArgumentException>(() => {
                Vector<Pow2.N4>.Concat(vector1, vector2, 'b', vector4);
            });
        }

        [TestMethod()]
        public void TupleTest() {
            MultiPrecision<Pow2.N4> x, y, z, w, e0, e1, e2, e3, e4, e5, e6, e7;

            Vector<Pow2.N4> vector2 = (2, 4);
            (x, y) = vector2;
            Assert.AreEqual((2, 4), (x, y));

            Vector<Pow2.N4> vector3 = (2, 4, 6);
            (x, y, z) = vector3;
            Assert.AreEqual((2, 4, 6), (x, y, z));

            Vector<Pow2.N4> vector4 = (2, 4, 6, 8);
            (x, y, z, w) = vector4;
            Assert.AreEqual((2, 4, 6, 8), (x, y, z, w));

            Vector<Pow2.N4> vector5 = (2, 4, 6, 8, 1);
            (e0, e1, e2, e3, e4) = vector5;
            Assert.AreEqual((2, 4, 6, 8, 1), (e0, e1, e2, e3, e4));

            Vector<Pow2.N4> vector6 = (2, 4, 6, 8, 1, 3);
            (e0, e1, e2, e3, e4, e5) = vector6;
            Assert.AreEqual((2, 4, 6, 8, 1, 3), (e0, e1, e2, e3, e4, e5));

            Vector<Pow2.N4> vector7 = (2, 4, 6, 8, 1, 3, 5);
            (e0, e1, e2, e3, e4, e5, e6) = vector7;
            Assert.AreEqual((2, 4, 6, 8, 1, 3, 5), (e0, e1, e2, e3, e4, e5, e6));

            Vector<Pow2.N4> vector8 = (2, 4, 6, 8, 1, 3, 5, 7);
            (e0, e1, e2, e3, e4, e5, e6, e7) = vector8;
            Assert.AreEqual((2, 4, 6, 8, 1, 3, 5, 7), (e0, e1, e2, e3, e4, e5, e6, e7));

            Assert.ThrowsException<InvalidOperationException>(() => {
                (x, y, z) = vector2;
            });
            Assert.ThrowsException<InvalidOperationException>(() => {
                (x, y, z, w) = vector3;
            });
            Assert.ThrowsException<InvalidOperationException>(() => {
                (e0, e1, e2, e3, e4) = vector4;
            });
            Assert.ThrowsException<InvalidOperationException>(() => {
                (e0, e1, e2, e3, e4, e5) = vector5;
            });
            Assert.ThrowsException<InvalidOperationException>(() => {
                (e0, e1, e2, e3, e4, e5, e6) = vector6;
            });
            Assert.ThrowsException<InvalidOperationException>(() => {
                (e0, e1, e2, e3, e4, e5, e6, e7) = vector7;
            });
            Assert.ThrowsException<InvalidOperationException>(() => {
                (e0, e1, e2, e3, e4, e5, e6) = vector8;
            });
        }

        [TestMethod()]
        public void CopyTest() {
            Vector<Pow2.N4> vector1 = new(1, 2);
            Vector<Pow2.N4> vector2 = vector1.Copy();

            vector2.X = 2;

            Assert.AreEqual(1, vector1.X);
            Assert.AreEqual(2, vector1.Y);
            Assert.AreEqual(2, vector2.X);
            Assert.AreEqual(2, vector2.Y);
        }

        [TestMethod()]
        public void ToStringTest() {
            Vector<Pow2.N4> vector1 = new(1, 2, 3);
            Vector<Pow2.N4> vector2 = new(new double[0]);
            Vector<Pow2.N4> vector3 = new(1);

            Assert.AreEqual("1,2,3", vector1.ToString());
            Assert.AreEqual(string.Empty, vector2.ToString());
            Assert.AreEqual("1", vector3.ToString());
        }
    }
}