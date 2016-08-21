﻿using System;

namespace OpenTracing
{
    public interface ISpan
    {
        ISpanContext Context { get; }

        ISpan SetTag(string key, object value);

        ISpan Log(string eventName, object payload = null);
        ISpan Log(DateTimeOffset timestamp, string eventName, object payload = null);

        void Finish(FinishSpanOptions options = null);
    }
}