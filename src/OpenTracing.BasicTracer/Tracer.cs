﻿using OpenTracing.Propagation;
using System;
using OpenTracing.BasicTracer.Propagation;
using System.Collections.Generic;

namespace OpenTracing.BasicTracer
{
    public class Tracer : ITracer
    {
        private readonly ISpanContextFactory _spanContextFactory;
        private readonly ISpanRecorder _spanRecorder;

        private readonly TextMapCarrierHandler _textMapCarrierHandler = new TextMapCarrierHandler();

        public Tracer(
            ISpanContextFactory spanContextFactory,
            ISpanRecorder spanRecorder)
        {
            if (spanContextFactory == null)
            {
                throw new ArgumentNullException(nameof(spanContextFactory));
            }

            if (spanRecorder == null)
            {
                throw new ArgumentNullException(nameof(spanRecorder));
            }

            _spanContextFactory = spanContextFactory;
            _spanRecorder = spanRecorder;
        }

        public ISpanBuilder BuildSpan(string operationName)
        {
            return new SpanBuilder(this, operationName);
        }

        public ISpan StartSpan(
            string operationName,
            DateTime? startTimestamp,
            IList<Tuple<string, ISpanContext>> references,
            IDictionary<string, object> tags)
        {
            var spanContext = _spanContextFactory.CreateSpanContext(references);

            var span = new Span(_spanRecorder, spanContext, operationName, startTimestamp ?? DateTime.UtcNow, tags);

            return span;
        }
        
        public void Inject<IInjectCarrier, IExtractCarrier>(ISpanContext spanContext, IFormat<IInjectCarrier, IExtractCarrier> format, IInjectCarrier carrier)
        {
            // TODO add other formats (and maybe don't use if/else :D )

            var typedContext = (SpanContext)spanContext;

            if (format.Equals(Formats.TextMap))
            {
                _textMapCarrierHandler.MapContextToCarrier(typedContext, (ITextMapInjectCarrier)carrier);
            }
            else
            {
                throw new UnsupportedFormatException($"The format '{format}' is not supported.");
            }
        }

        public ISpanContext Extract<IInjectCarrier, IExtractCarrier>(IFormat<IInjectCarrier, IExtractCarrier> format, IExtractCarrier carrier)
        {
            // TODO add other formats (and maybe don't use if/else :D )

            if (format.Equals(Formats.TextMap))
            {
                return _textMapCarrierHandler.MapCarrierToContext((ITextMapExtractCarrier) carrier);
            }
            
            throw new UnsupportedFormatException($"The format '{format}' is not supported.");
        }
    }
}
