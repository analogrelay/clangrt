using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibClangSharp.Common;
using Xunit;

namespace LibClangSharp.Facts
{
    public class IndexFacts
    {
        public class Constructor
        {
            [Fact]
            public void DoesNotCrash()
            {
                using (new Index(true, true)) { }
            }
        }

        public class GlobalOptionsProperty
        {
            [Fact]
            public void IsStable()
            {
                using (var idx = new Index(true, true))
                {
                    idx.GlobalOptions = GlobalOptions.ThreadBackgroundPriorityForEditing;
                    Assert.Equal(GlobalOptions.ThreadBackgroundPriorityForEditing, idx.GlobalOptions);
                    idx.GlobalOptions = GlobalOptions.ThreadBackgroundPriorityForAll;
                    Assert.Equal(GlobalOptions.ThreadBackgroundPriorityForEditing | GlobalOptions.ThreadBackgroundPriorityForIndexing, idx.GlobalOptions);
                }
            }
        }

        public class ParseTranslationUnitMethod
        {
            [Fact]
            public void RequiresValidArguments()
            {
                using (var idx = new Index(true, true))
                {
                    ContractAssert.NotNullOrEmpty(s => idx.ParseTranslationUnit(s, Enumerable.Empty<string>(), Enumerable.Empty<UnsavedFile>(), TranslationUnitOptions.None), "sourceFileName");
                    ContractAssert.NotNull(() => idx.ParseTranslationUnit("Foo", null, Enumerable.Empty<UnsavedFile>(), TranslationUnitOptions.None), "commandLineArguments");
                    ContractAssert.NotNull(() => idx.ParseTranslationUnit("Foo", Enumerable.Empty<string>(), null, TranslationUnitOptions.None), "unsavedFiles");
                    ContractAssert.ValidEnumMember(
                        () => idx.ParseTranslationUnit("Foo", Enumerable.Empty<string>(), Enumerable.Empty<UnsavedFile>(), (TranslationUnitOptions)0x100),
                        "options",
                        typeof(TranslationUnitOptions));
                }
            }

            [Fact]
            public void ReturnsNonNull()
            {
                using (var idx = new Index(true, true))
                {
                    ContractAssert.NotNullOrEmpty(s => idx.ParseTranslationUnit(s, Enumerable.Empty<string>(), Enumerable.Empty<UnsavedFile>(), TranslationUnitOptions.None), "sourceFileName");
                    ContractAssert.NotNull(() => idx.ParseTranslationUnit("Foo", null, Enumerable.Empty<UnsavedFile>(), TranslationUnitOptions.None), "commandLineArguments");
                    ContractAssert.NotNull(() => idx.ParseTranslationUnit("Foo", Enumerable.Empty<string>(), null, TranslationUnitOptions.None), "unsavedFiles");
                    ContractAssert.ValidEnumMember(
                        () => idx.ParseTranslationUnit("Foo", Enumerable.Empty<string>(), Enumerable.Empty<UnsavedFile>(), (TranslationUnitOptions)0x100),
                        "options",
                        typeof(TranslationUnitOptions));
                }
            }
        }
    }
}
