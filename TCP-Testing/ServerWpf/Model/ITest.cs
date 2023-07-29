namespace ServerWpf.Model
{
    /// <summary>
    /// Интерфейс для всех тестов
    /// </summary>
    internal interface ITest
    {
        string TestTypeString { get; }

        TestTypes Type { get; }
    }
}
