namespace EaFramework.Reporting
{
    public class SharedReportFixture : IDisposable
    {
        public IReportManager ReportManager { get; }

        public SharedReportFixture()
        {
            ReportManager = new ReportManager();
        }

        public void Dispose()
        {
            ReportManager.GetExtent().Flush();
        }
    }
}
