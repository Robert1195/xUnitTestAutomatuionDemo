using Xunit;

namespace EaFramework.Reporting
{
    [CollectionDefinition("SharedReport")]
    public class SharedReportCollection : ICollectionFixture<SharedReportFixture>
    {
        // To tylko definicja, nic więcej tu nie trzeba
    }
}
