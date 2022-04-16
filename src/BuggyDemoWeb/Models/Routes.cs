namespace BuggyDemoWeb.Models
{
    public class Routes
    {
        public string Url { get; set; }
        public DiagnosticType DiagnosticsGroup { get; set; }
    }

    public enum DiagnosticType
    {
        Crash,
        Exceptions,
        MemoryLeak,
        HighCPU,
        UnreponsiveLowCPU
    }
}
