using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MES.Service.Services
{
    public class ApiService : Greeter.GreeterBase
    {
        private readonly ILogger<ApiService> _logger;
        public ApiService(ILogger<ApiService> logger)
        {
            _logger = logger;
        }

        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HelloReply
            {
                Message = "Hello " + request.Name,
                Success = false
            });
        }
        public override Task<HelloReply> Test(HelloRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HelloReply
            {
                Message = "test " + request.Name,
                Success = false
            });
        }
    }
}
