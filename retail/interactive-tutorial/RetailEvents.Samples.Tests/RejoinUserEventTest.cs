﻿// Copyright 2022 Google LLC.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//      http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using Xunit;

namespace RetailEvents.Samples.Tests
{
    public class RejoinUserEventTest
    {
        [Fact]
        public void TestRejoinUserEvent()
        {
            string projectId = Environment.GetEnvironmentVariable("GOOGLE_PROJECT_ID");
            string projectNumber = Environment.GetEnvironmentVariable("PROJECT_NUMBER");

            string defaultCatalog = $"projects/{projectId}/locations/global/catalogs/default_catalog";
            string expectedRejoinResponseName = $"projects/{projectNumber}/locations/global/catalogs/default_catalog/operations/rejoin-user-events";

            WriteUserEventSample.CallWriteUserEvent(defaultCatalog);

            var rejoinResponse = RejoinUserEventSample.CallRejoinUserEvents(defaultCatalog);

            Assert.StartsWith(expectedRejoinResponseName, rejoinResponse.Name);
        }
    }
}
