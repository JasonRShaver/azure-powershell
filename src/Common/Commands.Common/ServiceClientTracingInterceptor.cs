﻿// ----------------------------------------------------------------------------------
//
// Copyright Microsoft Corporation
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// ----------------------------------------------------------------------------------

using Microsoft.Rest;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.WindowsAzure.Commands.Common
{
    class ServiceClientTracingInterceptor : IServiceClientTracingInterceptor
    {
        public ServiceClientTracingInterceptor(ConcurrentQueue<string> queue)
        {
            MessageQueue = queue;
        }

        public ConcurrentQueue<string> MessageQueue { get; private set; }

        public void Configuration(string source, string name, string value)
        {
            // Ignore 
        }

        public void EnterMethod(string invocationId, object instance, string method, IDictionary<string, object> parameters)
        {
            // Ignore 
        }

        public void ExitMethod(string invocationId, object returnValue)
        {
            // Ignore 
        }

        public void Information(string message)
        {
            MessageQueue.Enqueue(message);
        }

        public void ReceiveResponse(string invocationId, System.Net.Http.HttpResponseMessage response)
        {
            string responseAsString = response == null ? string.Empty : response.AsFormattedString();
            MessageQueue.Enqueue(responseAsString);
        }

        public void SendRequest(string invocationId, System.Net.Http.HttpRequestMessage request)
        {
            string requestAsString = request == null ? string.Empty : request.AsFormattedString();
            MessageQueue.Enqueue(requestAsString);
        }

        public void TraceError(string invocationId, Exception exception)
        {
            // Ignore 
        }

        public static void RemoveTracingInterceptor(ServiceClientTracingInterceptor interceptor)
        {
            if (interceptor != null)
            {
                ServiceClientTracing.RemoveTracingInterceptor(interceptor);
            }
        }
    }
}
