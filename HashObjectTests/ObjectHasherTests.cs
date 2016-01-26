using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using HashObject;
using NUnit.Framework;

namespace HashGraph
{
    [TestFixture]
    public class ObjectHasherTests
    {
        private static readonly Random Random = new Random((int)DateTime.Now.Ticks);

        [Test]
        public void CanCreateHashOfSimpleClass()
        {
            var obj = new Simple(42, "42");
            var hash = ObjectHasher.ComputeHash(obj);
            Assert.That(hash.Length, Is.EqualTo(32));
        }

        [Test]
        public void TwoEquivalentInstancesOfSimpleClassGiveSameHashes()
        {
            var obj1 = new Simple(42, "42");
            var obj2 = new Simple(42, "42");
            var hash1 = ObjectHasher.ComputeHash(obj1);
            var hash2 = ObjectHasher.ComputeHash(obj2);
            Assert.That(hash1, Is.EqualTo(hash2));
        }

        [Test]
        public void TwoEquivalentInstancesOfSimpleClassGiveSameHashesStreamVersion()
        {
            var obj1 = new Simple(42, "42");
            var obj2 = new Simple(42, "42");
            var hash1 = ObjectHasher.ComputeHashUsingStream(obj1);
            var hash2 = ObjectHasher.ComputeHashUsingStream(obj2);
            Assert.That(hash1, Is.EqualTo(hash2));
        }

        [Test]
        public void TwoDifferentInstancesOfSimpleClassGiveDifferentHashes()
        {
            var obj1 = new Simple(42, "42");
            var obj2 = new Simple(42, "42a");
            var hash1 = ObjectHasher.ComputeHash(obj1);
            var hash2 = ObjectHasher.ComputeHash(obj2);
            Assert.That(hash1, Is.Not.EqualTo(hash2));
        }

        [Test]
        public void TwoDifferentInstancesOfSimpleClassGiveDifferentHashesStreamVersion()
        {
            var obj1 = new Simple(42, "42");
            var obj2 = new Simple(42, "42a");
            var hash1 = ObjectHasher.ComputeHashUsingStream(obj1);
            var hash2 = ObjectHasher.ComputeHashUsingStream(obj2);
            Assert.That(hash1, Is.Not.EqualTo(hash2));
        }

        [Test]
        public void CanCreateHashOfComplexClass()
        {
            var obj = new Complex
            {
                Prop1 = new[] {1.0f},
                Prop2 = new [] {2.0d, 3.0d},
                Prop3 = new[] {Vector3.Zero, Vector3.One, new Vector3(1.0f, 2.0f, 3.0f) }
            };
            var hash = ObjectHasher.ComputeHash(obj);
            Assert.That(hash.Length, Is.EqualTo(32));
        }

        [Test]
        public void TwoEquivalentInstancesOfComplexClassGiveSameHashes()
        {
            var obj1 = new Complex
            {
                Prop1 = new[] { 1.0f },
                Prop2 = new[] { 2.0d, 3.0d },
                Prop3 = new[] { Vector3.Zero, Vector3.One, new Vector3(1.0f, 2.0f, 3.0f) }
            };
            var obj2 = new Complex
            {
                Prop1 = new[] { 1.0f },
                Prop2 = new[] { 2.0d, 3.0d },
                Prop3 = new[] { Vector3.Zero, Vector3.One, new Vector3(1.0f, 2.0f, 3.0f) }
            };
            var hash1 = ObjectHasher.ComputeHash(obj1);
            var hash2 = ObjectHasher.ComputeHash(obj2);
            Assert.That(hash1, Is.EqualTo(hash2));
        }

        [Test]
        public void TwoDifferentInstancesOfComplexClassGiveDifferentHashes()
        {
            var obj1 = new Complex
            {
                Prop1 = new[] { 1.0f },
                Prop2 = new[] { 2.0d, 3.0d },
                Prop3 = new[] { Vector3.Zero, Vector3.One, new Vector3(1.0f, 2.0f, 3.0f) }
            };
            var obj2 = new Complex
            {
                Prop1 = new[] { 1.0f },
                Prop2 = new[] { 2.0d, 3.0d },
                Prop3 = new[] { Vector3.Zero, Vector3.One, new Vector3(1.0f, 2.0f, 4.0f) }
            };
            var hash1 = ObjectHasher.ComputeHash(obj1);
            var hash2 = ObjectHasher.ComputeHash(obj2);
            Assert.That(hash1, Is.Not.EqualTo(hash2));
        }

        [Test]
        public void TwoDifferentWaysOfComputingHashGiveTheSameAnswer()
        {
            var obj = new Complex
            {
                Prop1 = new[] { 1.0f },
                Prop2 = new[] { 2.0d, 3.0d },
                Prop3 = new[] { Vector3.Zero, Vector3.One, new Vector3(1.0f, 2.0f, 3.0f) }
            };
            var hash1 = ObjectHasher.ComputeHash(obj);
            var hash2 = ObjectHasher.ComputeHashUsingStream(obj);
            Assert.That(hash1, Is.EqualTo(hash2));
        }

        [Test]
        public void ComputingHashUsingStreamIsFaster()
        {
            const int numberOfIterations = 10000;
            var objs = MakeRandomComplexObjects(numberOfIterations).ToList();

            var tuple1 = FuncExtensions.TimeIt(() => objs.Select(ObjectHasher.ComputeHash));
            var tuple2 = FuncExtensions.TimeIt(() => objs.Select(ObjectHasher.ComputeHashUsingStream));

            var hashes1 = tuple1.Item1;
            var hashes2 = tuple2.Item1;
            CollectionAssert.AreEquivalent(hashes1, hashes2);

            var elapsedTicks1 = tuple1.Item2;
            var elapsedTicks2 = tuple2.Item2;
            Console.WriteLine($"elapsedTicks1: {elapsedTicks1}; elapsedTicks2: {elapsedTicks2}; elapsedTicks1/elapsedTicks2: {elapsedTicks1/elapsedTicks2}");
            Assert.That(elapsedTicks1/elapsedTicks2, Is.GreaterThanOrEqualTo(30), "Speedup not as good as expected!");
        }

        private static IEnumerable<Complex> MakeRandomComplexObjects(int numberOfIterations)
        {
            return Enumerable.Range(0, numberOfIterations).Select(_ => MakeRandomComplexObject());
        }

        private static Complex MakeRandomComplexObject()
        {
            return new Complex
            {
                Prop1 = new[] {NextRandomFloat},
                Prop2 = new[] {NextRandomDouble, NextRandomDouble},
                Prop3 = new[]
                {
                    Vector3.Zero,
                    Vector3.One,
                    new Vector3(NextRandomFloat, NextRandomFloat, NextRandomFloat)
                }
            };
        }

        private static float NextRandomFloat => Random.Next(1, 100);
        private static double NextRandomDouble => Random.Next(1, 100);
    }
}
