using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibClangSharp
{
    public class TranslationUnit
    {
        private IntPtr _handle;

        internal TranslationUnit(IntPtr handle)
        {
            _handle = handle;
        }
    }
}
