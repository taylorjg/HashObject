using System;
using System.Numerics;
using HashObject;
using NUnit.Framework;

namespace HashGraph
{
    [TestFixture]
    public class ObjectHasherTests
    {
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
        public void TwoDifferentInstancesOfSimpleClassGiveDifferentHashes()
        {
            var obj1 = new Simple(42, "42");
            var obj2 = new Simple(42, "42a");
            var hash1 = ObjectHasher.ComputeHash(obj1);
            var hash2 = ObjectHasher.ComputeHash(obj2);
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
        public void CanUseMetadataTypeAttributeToExcludeSomeThings()
        {
            var obj1 = new Complex
            {
                Prop1 = new[] {1.0f},
                Prop2 = new[] {1.0d},
                Prop3 = new[] {Vector3.One},
                Prop4 = DateTime.MinValue
            };
            var obj2 = new Complex
            {
                Prop1 = new[] {1.0f},
                Prop2 = new[] {1.0d},
                Prop3 = new[] {Vector3.One},
                Prop4 = DateTime.MaxValue
            };
            var hash1 = ObjectHasher.ComputeHash(obj1);
            var hash2 = ObjectHasher.ComputeHash(obj2);
            Assert.That(hash1, Is.EqualTo(hash2));
        }
    }
}
