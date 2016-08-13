﻿using OpenTracing.BasicTracer.Context;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace OpenTracing.BasicTracer
{
    public sealed class Span<TContext> : ISpan where TContext : Context.ISpanContext
    {
        private readonly ITracer _tracer;
        private readonly TContext _spanContext;

        private ISpanRecorder<TContext> _spanRecorder;

        public ISpanContext GetSpanContext()
        {
            return _spanContext;
        }

        internal Span(ITracer tracer, ISpanRecorder<TContext> spanRecorder, TContext spanContext, string operationName, DateTime startTime)
        {
            _tracer = tracer;
            _spanContext = spanContext;
            OperationName = operationName;

            _spanRecorder = spanRecorder;

            StartTime = startTime;
        }

        private bool isFinished = false;

        public void Finish()
        {
            FinishWithOptions(DateTime.Now);
        }

        public void FinishWithOptions(DateTime finishTime)
        {
            if (isFinished)
                return;

            Duration = finishTime - StartTime;

            var spanData = new SpanData<TContext>()
            {
                Context = _spanContext,
                OperationName = OperationName,
                StartTime = StartTime,
                Duration = Duration,
                Tags = Tags,
                LogData = LogData,
            };

            _spanRecorder.RecordSpan(spanData);
            isFinished = true;
        }

        public string OperationName { get; private set; }
        public DateTime StartTime { get; private set; }
        public TimeSpan Duration { get; private set; }
        public Dictionary<string, string> Tags { get; } = new Dictionary<string, string>();
        public List<LogData> LogData { get; } = new List<OpenTracing.LogData>();

        public void SetTag(string message, string value)
        {
            Tags[message] = value;
        }

        public void SetTag(string message, bool value)
        {
            SetTag(message, value);
        }

        public void SetTag(string message, int value)
        {
            SetTag(message, value);
        }

        public void SetBaggageItem(string restrictedKey, string value)
        {
            if (!IsValidBaggaeKey(restrictedKey))
                throw new ArgumentException("Invalid baggage key: '" + restrictedKey + "'");

            _spanContext.SetBaggageItem(restrictedKey.ToLower(), value);
        }

        public string GetBaggageItem(string restrictedKey)
        {
            return _spanContext.GetBaggageItems()[restrictedKey.ToLower()];
        }

        public void Log(string message, object obj)
        {
            Log(DateTime.Now, message, obj);
        }

        public void Log(DateTime dateTime, string message, object obj)
        {
            LogData.Add(new LogData(dateTime, message, obj));
        }

        private bool IsValidBaggaeKey(string key)
        {
            var regEx = new Regex(@"^(?i:[a-z0-9][-a-z0-9]*)$");
            return regEx.IsMatch(key);
        }

        public ITracer GetTracer()
        {
            return _tracer;
        }
    }
}