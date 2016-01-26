namespace HashGraph
{
    internal class Simple
    {
        public Simple(int prop1, string prop2)
        {
            Prop1 = prop1;
            Prop2 = prop2;
        }

        // ReSharper disable MemberCanBePrivate.Global
        // ReSharper disable UnusedAutoPropertyAccessor.Global
        public int Prop1 { get; }
        public string Prop2 { get; }
        // ReSharper restore MemberCanBePrivate.Global
        // ReSharper restore UnusedAutoPropertyAccessor.Global
    }
}
