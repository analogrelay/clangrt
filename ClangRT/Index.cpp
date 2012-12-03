#include "Index.h"

using namespace ClangRT;

Index::Index(bool excludeDeclarationsFromPCH, bool displayDiagnostics) {
	_index = clang_createIndex(excludeDeclarationsFromPCH ? 1 : 0, displayDiagnostics ? 1 : 0);
}

Index::~Index() {
	clang_disposeIndex(_index);
}

IndexOptions Index::GlobalOptions::get() {
	return (IndexOptions)clang_CXIndex_getGlobalOptions(_index);
}

void Index::GlobalOptions::set(IndexOptions flags) {
	clang_CXIndex_setGlobalOptions(_index, (unsigned int)flags);
}