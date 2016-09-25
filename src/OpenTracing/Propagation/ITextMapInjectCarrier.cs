using System.Collections.Generic;

namespace OpenTracing.Propagation
{
    /// <summary>
    /// <see cref="ITextMapInjectCarrier"/> is a built-in carrier for <see cref="ITracer.Inject"/>. 
    /// ITextMapInjectCarrier implementations allow Tracers to write key:value String pairs from arbitrary underlying sources of data.
    /// </summary>
    public interface ITextMapInjectCarrier : IInjectCarrier
    {
        /// <summary>
        /// Adds a key:value pair into the underlying source. If the source already contains the key, the value will be overwritten.
        /// </summary>
        /// <param name="key">A string, possibly with constraints dictated by the particular Format this <see cref="ITextMapInjectCarrier"/> is paired with.</param>
        /// <param name="value">A String, possibly with constraints dictated by the particular Format this <see cref="ITextMapInjectCarrier"/> is paired with.</param>
        void Set(string key, string value);
    }
}