using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibClangSharp
{
    [Flags]
    public enum GlobalOptions
    {
        None = 0,
        ThreadBackgroundPriorityForIndexing = 1,
        ThreadBackgroundPriorityForEditing = 2,
        ThreadBackgroundPriorityForAll = ThreadBackgroundPriorityForIndexing | ThreadBackgroundPriorityForEditing
    }
}
