using System.Collections.Generic;

namespace OpenTracing.Propagation
{
    /// <summary>
    /// <see cref="ITextMapExtractCarrier"/> is a built-in carrier for <see cref="ITracer.Extract"/>. 
    /// ITextMapExtractCarrier implementations allow Tracers to read key:value String pairs from arbitrary underlying sources of data.
    /// </summary>
    public interface ITextMapExtractCarrier : IExtractCarrier
    {
        /// <summary>
        /// <para>Returns all key:value pairs from the underlying source.</para>
        /// <para>Note that for some Formats, the iterator may include entries that
        /// were never injected by a Tracer implementation (e.g., unrelated HTTP headers).</para>
        /// </summary>
        IEnumerable<KeyValuePair<string, string>> GetEntries();

        /// <summary>
        /// Returns the key's value from the underlying source, or null if the key doesn't exist.
        /// </summary>
        /// <param name="key">The key for which a value should be returned.</param>
        string Get(string key);
    }
}