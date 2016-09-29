﻿namespace OpenTracing.Propagation
{
    /// <summary>
    /// Provides access to the builtin <see cref="Format" />s.
    /// </summary>
    public static class Formats
    {
        /// <summary>
        /// <para>The BINARY format represents SpanContexts in an opaque byte array carrier.</para>
        /// <para>For both <see cref="ITracer.Inject"/> and <see cref="ITracer.Extract"/> the carrier should be a
        /// byte array instance. <see cref="ITracer.Inject"/> must append to the byte array carrier
        /// (rather than replace its contents).</para>
        /// </summary>
        public static readonly Format<byte[], byte[]> Binary = new Format<byte[], byte[]>("binary");

        /// <summary>
        /// <para>The TEXT_MAP format represents SpanContexts in a dictionary of string to string.</para>
        /// <para>Both the keys and the values have unrestricted character sets (unlike the HTTP_HEADERS format).</para>
        /// <para>NOTE: The TEXT_MAP carrier dict may contain unrelated data (e.g., arbitrary gRPC metadata). 
        /// As such, the Tracer implementation should use a prefix or other convention to distinguish Tracer-specific key:value pairs.</para>
        /// </summary>
        public static readonly Format<ITextMapInjectCarrier, ITextMapExtractCarrier> TextMap = new Format<ITextMapInjectCarrier, ITextMapExtractCarrier>("text_map");

        /// <summary>
        /// <para>The HTTP_HEADERS format represents SpanContexts in a dictionary of character-restricted string to string.</para>
        /// <para>Keys and values in the HTTP_HEADERS carrier must be suitable for use as HTTP headers (without modification or further escaping). 
        /// That is, the keys have a greatly restricted character set, casing for the keys may not be preserved by various intermediaries, 
        /// and the values should be URL-escaped.</para>
        /// <para>NOTE: The HTTP_HEADERS carrier dict may contain unrelated data (e.g., arbitrary gRPC metadata). 
        /// As such, the Tracer implementation should use a prefix or other convention to distinguish Tracer-specific key:value pairs.</para>
        /// </summary>
        public static readonly Format<ITextMapInjectCarrier, ITextMapExtractCarrier> HttpHeaders = new Format<ITextMapInjectCarrier, ITextMapExtractCarrier>("http_headers");
    }

    
}