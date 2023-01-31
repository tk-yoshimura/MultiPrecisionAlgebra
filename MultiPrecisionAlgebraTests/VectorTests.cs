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
        public void DistanceTest() {
            Vector<Pow2.N4> vector1 = new(1, 2);
            Vector<Pow2.N4> vector2 = new(2, 1);

            Assert.AreEqual(MultiPrecision<Pow2.N4>.Sqrt(2), Vector<Pow2.N4>.Distance(vector1, vector2));
            Assert.AreEqual(2, Vector<Pow2.N4>.SquareDistance(vector1, vector2));
        }

        [TestMethod()]
        public void InnerProductTest() {
            Vector<Pow2.N4> vector1 = new(1, 2);
            Vector<Pow2.N4> vector2 = new(3, 4);
            Vector<Pow2.N4> vector3 = new(2, -1);

            Assert.AreEqual(11, Vector<Pow2.N4>.InnerProduct(vector1, vector2));
            Assert.AreEqual(11, Vector<Pow2.N4>.InnerProduct(vector2, vector1));
            Assert.AreEqual(0, Vector<Pow2.N4>.InnerProduct(vector1, vector3));
            Assert.AreEqual(2, Vector<Pow2.N4>.InnerProduct(vector2, vector3));
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

            Assert.IsTrue(vector.X.IsNaN);
            Assert.IsTrue(vector.Y.IsNaN);
            Assert.IsTrue(vector.Z.IsNaN);
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