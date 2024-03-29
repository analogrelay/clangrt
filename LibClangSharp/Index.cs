﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibClangSharp.Common;

namespace LibClangSharp
{
    public class Index : IDisposable
    {
        private IntPtr _handle;

        public GlobalOptions GlobalOptions
        {
            get { return NativeMethods.clang_CXIndex_getGlobalOptions(_handle); }
            set { NativeMethods.clang_CXIndex_setGlobalOptions(_handle, value); }
        }

        public Index(bool excludeDeclarationsFromPCH, bool displayDiagnostics)
        {
            _handle = NativeMethods.clang_createIndex(excludeDeclarationsFromPCH ? 1 : 0, displayDiagnostics ? 1 : 0);
        }

        ~Index()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            // Always dispose the Index, it's an unmanaged resource
            NativeMethods.clang_disposeIndex(_handle);
        }

        public TranslationUnit ParseTranslationUnit(string sourceFileName, IEnumerable<string> commandLineArguments, IEnumerable<UnsavedFile> unsavedFiles, TranslationUnitOptions options)
        {
            Requires.NotNullOrEmpty(sourceFileName, "sourceFileName");
            Requires.NotNull(commandLineArguments, "commandLineArguments");
            Requires.NotNull(unsavedFiles, "unsavedFiles");
            Requires.ValidEnumMember(options, TranslationUnitOptions.None, TranslationUnitOptions.IncludeBriefCommentsInCodeCompletion, "options");

            // Build input arrays
            string[] inputArgs = commandLineArguments.ToArray();
            IntPtr[] inputFiles = unsavedFiles.Select(f => f.Handle).ToArray();

            // Call the native method
            IntPtr tuHandle = NativeMethods.clang_parseTranslationUnit(
                _handle, sourceFileName, inputArgs, inputArgs.Length, inputFiles, inputFiles.Length, options);

            // Check for failure
            if (tuHandle == IntPtr.Zero)
            {
                throw new InvalidOperationException("Error parsing translation unit");
            }

            return new TranslationUnit(tuHandle);
        }
    }
}
