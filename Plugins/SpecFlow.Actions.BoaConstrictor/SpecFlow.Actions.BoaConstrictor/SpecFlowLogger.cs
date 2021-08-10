// unset

using Boa.Constrictor.Logging;
using TechTalk.SpecFlow.Infrastructure;

namespace SpecFlow.Actions.BoaConstrictor
{
    public class SpecFlowLogger : AbstractLogger
    {
        private readonly ISpecFlowOutputHelper _specFlowOutputHelper;

        public SpecFlowLogger(ISpecFlowOutputHelper specFlowOutputHelper)
        {
            _specFlowOutputHelper = specFlowOutputHelper;
        }

        public override void Close()
        {
            
        }

        protected override void LogRaw(string message, LogSeverity severity = LogSeverity.Info)
        {
            _specFlowOutputHelper.WriteLine($"[{severity.ToString()}] {message}");
        }
    }
}