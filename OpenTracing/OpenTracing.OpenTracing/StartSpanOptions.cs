﻿using System;
using System.Collections.Generic;

namespace OpenTracing
{
    public class StartSpanOptions
    { 
        public DateTime StartTime { get; set; } = DateTime.Now;
        public Dictionary<string, string> Tag { get; set; } = new Dictionary<string, string>() { };

        public List<SpanReference> References = new List<SpanReference> { };
    }
}
