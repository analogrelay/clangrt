#pragma once
#include <Index.h>

namespace ClangRT {
    public enum class IndexOptions {
        None = CXIndexOpt_None,
        ThreadBackgroundPriorityForIndexing = CXGlobalOpt_ThreadBackgroundPriorityForIndexing,
        ThreadBackgroundPriorityForEditing = CXGlobalOpt_ThreadBackgroundPriorityForEditing,
        ThreadBackgroundPriorityForAll = ThreadBackgroundPriorityForIndexing | ThreadBackgroundPriorityForEditing
    };

    public ref class Index sealed {
    public:
        Index(bool excludeDeclarationsFromPCH, bool displayDiagnostics);
        property IndexOptions GlobalOptions {
            IndexOptions get();
            void set(IndexOptions value);
        }
    private:
        ~Index();

        CXIndex _index;
    };
}