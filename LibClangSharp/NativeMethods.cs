using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LibClangSharp
{
    internal static class NativeMethods
    {
        private const string LibClang = "libclang";

        [DllImport(LibClang)]
        public static extern IntPtr clang_createIndex(int excludeDeclarationsFromPCH, int displayDiagnostics);

        [DllImport(LibClang)]
        public static extern void clang_disposeIndex(IntPtr indexHandle);

        [DllImport(LibClang)]
        public static extern GlobalOptions clang_CXIndex_getGlobalOptions(IntPtr indexHandle);

        [DllImport(LibClang)]
        public static extern void clang_CXIndex_setGlobalOptions(IntPtr indexHandle, GlobalOptions options);

        [DllImport(LibClang)]
        public static extern IntPtr clang_parseTranslationUnit(
            /* 0 */ IntPtr indexHandle,
            /* 1 */ [MarshalAs(UnmanagedType.LPStr)] string fileName,
            /* 2 */ [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] string[] commandLineArguments,
            /* 3 */ int commandLineArgumentCount,
            /* 4 */ [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 5)] IntPtr[] unsavedFiles,
            /* 5 */ int unsavedFilesCount,
            /* 6 */ TranslationUnitOptions options);
    }
}
