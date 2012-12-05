using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibClangSharp
{
    public enum TranslationUnitOptions
    {
        None = 0x0, 
        DetailedPreprocessingRecord = 0x01, 
        Incomplete = 0x02, 
        PrecompiledPreamble = 0x04,
        CacheCompletionResults = 0x08, 
        ForSerialization = 0x10, 
        CXXChainedPCH = 0x20, 
        SkipFunctionBodies = 0x40,
        IncludeBriefCommentsInCodeCompletion = 0x80
    }
}
