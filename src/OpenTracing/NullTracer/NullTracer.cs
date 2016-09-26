using System;
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

        public void Inject<IInjectCarrier, IExtractCarrier>(ISpanContext spanContext, IFormat<IInjectCarrier, IExtractCarrier> format, IInjectCarrier carrier)
        {
        }

        public ISpanContext Extract<IInjectCarrier, IExtractCarrier>(IFormat<IInjectCarrier, IExtractCarrier> format, IExtractCarrier carrier)
        {
            return null;
        }
    }
}