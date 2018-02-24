﻿// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System.Threading.Tasks;
using Microsoft.CodeAnalysis.Editor.CSharp.KeywordHighlighting.KeywordHighlighters;
using Roslyn.Test.Utilities;
using Xunit;

namespace Microsoft.CodeAnalysis.Editor.CSharp.UnitTests.KeywordHighlighting
{
    public class AsyncSimpleLambdaHighlighterTests : AbstractCSharpKeywordHighlighterTests
    {
        internal override IHighlighter CreateHighlighter()
        {
            return new AsyncAnonymousFunctionHighlighter();
        }

        [Fact, Trait(Traits.Feature, Traits.Features.KeywordHighlighting)]
        public async Task TestSimpleLambda()
        {
            await TestAsync(
@"using System;
using System.Threading.Tasks;

class AsyncExample
{
    async Task<int> AsyncMethod()
    {
        int hours = 24;
        return hours;
    }

    async Task UseAsync()
    {
        Func<int, Task<int>> lambda = {|Cursor:[|async|]|} _ =>
        {
            return [|await|] AsyncMethod();
        };
        int result = await AsyncMethod();
        Task<int> resultTask = AsyncMethod();
        result = await resultTask;
        result = await lambda(0);
    }
}");
        }
    }
}
