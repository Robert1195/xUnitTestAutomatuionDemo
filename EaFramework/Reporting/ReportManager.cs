using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;

namespace EaFramework.Reporting
{
    public interface IReportManager
    {
        void Fail(string message);
        ExtentReports GetExtent();
        void Log(string message);
        void Pass(string message);
        void StartTest(string testName, string description = null);
    }

    public class ReportManager : IReportManager
    {
        private ExtentReports _extent;
        private ExtentSparkReporter _htmlReporter;
        private string _reportPath;
        public ExtentTest _currentTest;

        public ReportManager()
        {
            // Report's path
            //_reportPath = $"{AppDomain.CurrentDomain.BaseDirectory}TestReport.html";
            //Console.WriteLine(_reportPath);

            var reportDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestResults");
            Directory.CreateDirectory(reportDir); // bezpieczne - nie rzuca wyjątku, jeśli już istnieje

            _reportPath = Path.Combine(reportDir, "TestReport.html");


            // Report's configuration
            _htmlReporter = new ExtentSparkReporter(_reportPath);
            _htmlReporter.Config.DocumentTitle = "Driver Portal Test Report";
            _htmlReporter.Config.ReportName = "Test Execution Report";

            _extent = new ExtentReports();
            _extent.AttachReporter(_htmlReporter);

            AppDomain.CurrentDomain.ProcessExit += (s, e) => _extent.Flush();
        }

        public ExtentReports GetExtent()
        {
            return _extent;
        }

        public void StartTest(string testName, string description = null)
        {
            _currentTest = _extent.CreateTest(testName, description);
        }

        public void Log(string message) => _currentTest?.Info(message);
        public void Pass(string message) => _currentTest?.Pass(message);
        public void Fail(string message) => _currentTest?.Fail(message);
    }
}
