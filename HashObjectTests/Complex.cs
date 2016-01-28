using System;
using System.ComponentModel.DataAnnotations;
using System.Numerics;
using Newtonsoft.Json;

namespace HashGraph
{
    [MetadataType(typeof(ComplexJsonMetadata))]
    internal class Complex
    {
// Field 'X' is assigned but its value is never used
#pragma warning disable 414
        public float[] Prop1;
        public double[] Prop2;
        public Vector3[] Prop3;
        public DateTime Prop4;
#pragma warning restore 414
    }

    internal class ComplexJsonMetadata
    {
        [JsonIgnore]
        public DateTime Prop4;
    }
}
