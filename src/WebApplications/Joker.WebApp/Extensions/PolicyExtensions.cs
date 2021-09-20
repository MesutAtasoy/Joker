using System;
using System.Net.Http;
using Polly;
using Polly.Extensions.Http;
using Serilog;

namespace Joker.WebApp.Extensions
{
    public static class PolicyExtensions
    {
        public static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .AdvancedCircuitBreakerAsync(0.01,
                    TimeSpan.FromSeconds(10),
                    4,
                    TimeSpan.FromSeconds(10),
                    onBreak: (result, span) => { Log.Error("Circuit is broke"); },
                    onReset: () => { Log.Error("Circuit reset"); },
                    onHalfOpen: () => { Log.Error("Circuit is half-open"); });
        }
    }
}