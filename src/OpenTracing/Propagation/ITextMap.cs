using System.Collections.Generic;

namespace OpenTracing.Propagation
{
    /// <summary>
    /// <see cref="ITextMap"/> is a built-in carrier for <see cref="ITracer.Inject"/> and <see cref="ITracer.Extract"/>. 
    /// ITextMap implementations allow Tracers to read and write key:value String pairs from arbitrary underlying sources of data.
    /// </summary>
    public interface ITextMap : ITextMapInjectCarrier, ITextMapExtractCarrier
    {
    }
}