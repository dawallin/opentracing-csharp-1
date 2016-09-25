using OpenTracing.Propagation;

namespace OpenTracing.NullTracer
{
    public class NullTracer : ITracer
    {
        public static readonly NullTracer Instance = new NullTracer();

        private NullTracer()
        {
        }

        public ISpanBuilder BuildSpan(string operationName)
        {
            return NullSpanBuilder.Instance;
        }

        public void Inject<TCarrier>(ISpanContext spanContext, IFormat<TCarrier> format, TCarrier carrier)
            where TCarrier : IInjectCarrier
        {
        }

        public ISpanContext Extract<TCarrier>(IFormat<TCarrier> format, TCarrier carrier)
            where TCarrier : IExtractCarrier
        {
            return null;
        }
    }
}