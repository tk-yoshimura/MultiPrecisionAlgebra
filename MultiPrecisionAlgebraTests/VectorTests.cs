using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;

namespace MultiPrecisionAlgebra.Tests {
    [TestClass()]
    public class VectorTests {
        [TestMethod()]
        public void VectorTest() {
            Vector<Pow2.N4> vector = new(new double[] { 1, 2, 3, 4 });

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
        }

        [TestMethod()]
        public void NormTest() {
            Vector<Pow2.N4> vector = new(new double[] { -3, 4 });

            Assert.AreEqual(5, vector.Norm);
            Assert.AreEqual(25, vector.SquareNorm);
        }

        [TestMethod()]
        public void NormalTest() {
            Vector<Pow2.N4> vector = new(new double[] { 1, 2, -3 });

            Assert.AreEqual(1 / MultiPrecision<Pow2.N4>.Sqrt(1 + 4 + 9), vector.Normal.X);
            Assert.AreEqual(2 / MultiPrecision<Pow2.N4>.Sqrt(1 + 4 + 9), vector.Normal.Y);
            Assert.AreEqual(-3 / MultiPrecision<Pow2.N4>.Sqrt(1 + 4 + 9), vector.Normal.Z);
        }

        [TestMethod()]
        public void OperatorTest() {
            Vector<Pow2.N4> vector1 = new(new double[] { 1, 2 });
            Vector<Pow2.N4> vector2 = new(new double[] { 3, 4 });

            Assert.AreEqual(new Vector<Pow2.N4>(new double[] { 1, 2 }), +vector1);
            Assert.AreEqual(new Vector<Pow2.N4>(new double[] { -1, -2 }), -vector1);
            Assert.AreEqual(new Vector<Pow2.N4>(new double[] { 4, 6 }), vector1 + vector2);
            Assert.AreEqual(new Vector<Pow2.N4>(new double[] { 4, 6 }), vector2 + vector1);
            Assert.AreEqual(new Vector<Pow2.N4>(new double[] { -2, -2 }), vector1 - vector2);
            Assert.AreEqual(new Vector<Pow2.N4>(new double[] { 2, 2 }), vector2 - vector1);
            Assert.AreEqual(new Vector<Pow2.N4>(new double[] { 2, 4 }), vector1 * 2);
            Assert.AreEqual(new Vector<Pow2.N4>(new double[] { 2, 4 }), 2 * vector1);
            Assert.AreEqual(new Vector<Pow2.N4>(new double[] { 0.5, 1 }), vector1 / 2);
        }

        [TestMethod()]
        public void DistanceTest() {
            Vector<Pow2.N4> vector1 = new(new double[] { 1, 2 });
            Vector<Pow2.N4> vector2 = new(new double[] { 2, 1 });

            Assert.AreEqual(MultiPrecision<Pow2.N4>.Sqrt(2), Vector<Pow2.N4>.Distance(vector1, vector2));
            Assert.AreEqual(2, Vector<Pow2.N4>.SquareDistance(vector1, vector2));
        }

        [TestMethod()]
        public void InnerProductTest() {
            Vector<Pow2.N4> vector1 = new(new double[] { 1, 2 });
            Vector<Pow2.N4> vector2 = new(new double[] { 3, 4 });
            Vector<Pow2.N4> vector3 = new(new double[] { 2, -1 });

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
        public void IsZeroTest() {
            Vector<Pow2.N4> vector = Vector<Pow2.N4>.Zero(3);

            Assert.IsTrue(Vector<Pow2.N4>.IsZero(vector));

            vector.X = 1;

            Assert.IsFalse(Vector<Pow2.N4>.IsZero(vector));
        }

        [TestMethod()]
        public void InvalidTest() {
            Vector<Pow2.N4> vector = Vector<Pow2.N4>.Invalid(3);

            Assert.IsTrue(vector.X.IsNaN);
            Assert.IsTrue(vector.Y.IsNaN);
            Assert.IsTrue(vector.Z.IsNaN);
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
            Vector<Pow2.N4> vector = new(new double[] { 1, 2 });

            Assert.IsTrue(vector == new Vector<Pow2.N4>(new double[] { 1, 2 }));
            Assert.IsTrue(vector != new Vector<Pow2.N4>(new double[] { 2, 1 }));
            Assert.IsTrue(vector != new Vector<Pow2.N4>(new double[] { 1 }));
            Assert.IsTrue(vector != new Vector<Pow2.N4>(new double[] { 1, 2, 3 }));
            Assert.IsFalse(vector == null);
            Assert.IsTrue(vector != null);
            Assert.IsTrue(vector != new Vector<Pow2.N4>(new double[] { 1, double.NaN }));
        }

        [TestMethod()]
        public void EqualsTest() {
            Vector<Pow2.N4> vector = new(new double[] { 1, 2 });

            Assert.IsTrue(vector.Equals(new Vector<Pow2.N4>(new double[] { 1, 2 })));
            Assert.IsFalse(vector.Equals(new Vector<Pow2.N4>(new double[] { 2, 1 })));
            Assert.IsFalse(vector.Equals(new Vector<Pow2.N4>(new double[] { 1 })));
            Assert.IsFalse(vector.Equals(new Vector<Pow2.N4>(new double[] { 1, 2, 3 })));
            Assert.IsFalse(vector.Equals(null));
            Assert.IsFalse(vector.Equals(new Vector<Pow2.N4>(new double[] { 1, double.NaN })));
        }

        [TestMethod()]
        public void ToStringTest() {
            Vector<Pow2.N4> vector1 = new(new double[] { 1, 2, 3 });
            Vector<Pow2.N4> vector2 = new(new double[0]);
            Vector<Pow2.N4> vector3 = new(new double[] { 1d });

            Assert.AreEqual("1,2,3", vector1.ToString());
            Assert.AreEqual(string.Empty, vector2.ToString());
            Assert.AreEqual("1", vector3.ToString());
        }

        [TestMethod()]
        public void CopyTest() {
            Vector<Pow2.N4> vector1 = new(new double[] { 1, 2 });
            Vector<Pow2.N4> vector2 = vector1.Copy();

            vector2.X = 2;

            Assert.AreEqual(1, vector1.X);
            Assert.AreEqual(2, vector1.Y);
            Assert.AreEqual(2, vector2.X);
            Assert.AreEqual(2, vector2.Y);
        }
    }
}