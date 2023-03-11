#region License, Terms and Author(s)
//
// ELMAH - Error Logging Modules and Handlers for ASP.NET
// Copyright (c) 2004-9 Atif Aziz. All rights reserved.
//
//  Author(s):
//
//      Thomas Ardal, http://www.thomasardal.com
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#endregion

namespace Elmah.Tests
{
    #region Imports

    extern alias e;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using Xunit;
    using e::Elmah;

    #endregion

    public class MemoryErrorLogTests
    {
        static MemoryErrorLog CreateLog(int? size = null)
        {
            return Resetting(size != null ? new MemoryErrorLog(size.Value) : new MemoryErrorLog());
        }

        static MemoryErrorLog Resetting(MemoryErrorLog log)
        {
            log.Reset();
            return log;
        }

        [Fact]
        public void CanLogError()
        {
            var errorId = CreateLog().Log(new Error());
            Assert.False(string.IsNullOrEmpty(errorId));
        }

        [Fact]
        public void CanGetError()
        {
            var errorLog = CreateLog();
            var expectedErrorId = errorLog.Log(new Error());
            var error = errorLog.GetError(expectedErrorId);
            Assert.Equal(expectedErrorId, error.Id);
        }

        [Fact]
        public void CanPageMultipleErrors()
        {
            var errorLog = CreateLog();
            var today = DateTime.Today;
            for (var i = 3; i >= 0; i--)
            {
                errorLog.Log(new Error { Time = today.AddDays(-i) });
            }

            var page1 = new List<ErrorLogEntry>();
            errorLog.GetErrors(0, 2, page1);
            var page2 = new List<ErrorLogEntry>();
            errorLog.GetErrors(1, 2, page2);

            Assert.Equal(2, page1.Count);
            Assert.Equal(today, page1[0].Error.Time);
            Assert.Equal(today.AddDays(-1), page1[1].Error.Time);
            Assert.Equal(2, page2.Count);
            Assert.Equal(today.AddDays(-2), page2[0].Error.Time);
            Assert.Equal(today.AddDays(-3), page2[1].Error.Time);
        }

        [Fact]
        public void CanLogMoreErrorsThanConfiguredSize()
        {
            var errorLog = CreateLog(2);
            var error1Id = errorLog.Log(new Error());
            var error2Id = errorLog.Log(new Error());
            var error3Id = errorLog.Log(new Error());

            var result = new List<ErrorLogEntry>();
            var count = errorLog.GetErrors(0, 3, result);
            
            Assert.Equal(2, count);
            Assert.False(result.Any(error => error.Id == error1Id));
            Assert.True(result.Any(error => error.Id == error2Id));
            Assert.True(result.Any(error => error.Id == error3Id));
        }

        [Fact]
        public void CanSetApplicationName()
        {
            var applicationName = "MyApp";
            var hashtable = new Hashtable {{"applicationName", applicationName}};
            var memoryErrorLog = new MemoryErrorLog(hashtable);
            Assert.Equal(applicationName, memoryErrorLog.ApplicationName);
        }

        [Fact]
        public void CanCreateWithoutApplicationName()
        {
            var memoryErrorLog = new MemoryErrorLog(new Hashtable());
            Assert.Equal(string.Empty, memoryErrorLog.ApplicationName);
        }

        [Fact]
        public void ThrowExceptionOnSizeLessThanZero()
        {
            var e = Assert.Throws<ArgumentOutOfRangeException>(() => new MemoryErrorLog(-1));
            Assert.Equal("size", e.ParamName);
        }

        [Fact]
        public void ThrowExceptionOnSizeMoreThanMaximumAllowed()
        {
            var e = Assert.Throws<ArgumentOutOfRangeException>(() => new MemoryErrorLog(1 + MemoryErrorLog.MaximumSize));
            Assert.Equal("size", e.ParamName);
        }

        [Fact]
        public void ThrowExceptionOnPageIndexBelowZero()
        {
            var e = Assert.Throws<ArgumentOutOfRangeException>(
                () => new MemoryErrorLog().GetErrors(-1, 0, new Collection<ErrorLogEntry>()));
            Assert.Equal("pageIndex", e.ParamName);
        }

        [Fact]
        public void ThrowExceptionOnPageSizeBelowZero()
        {
            var e = Assert.Throws<ArgumentOutOfRangeException>(
                () => new MemoryErrorLog().GetErrors(0, -1, new Collection<ErrorLogEntry>()));
            Assert.Equal("pageSize", e.ParamName);
        }

        [Fact]
        public void ThrowExceptionWhenErrorIsNull()
        {
            var e = Assert.Throws<ArgumentNullException>(() => new MemoryErrorLog().Log(null));
            Assert.Equal("error", e.ParamName);
        }
    }
}
