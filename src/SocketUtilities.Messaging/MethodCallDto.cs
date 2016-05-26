using System;
using System.Threading.Tasks;
using SocketUtilities.Core;

namespace SocketUtilities.Messaging
{
    public class MethodCallDto
    {
        private readonly ILogger _logger;

        public MethodCallDto()
            : this(new FileLogger(Environment.CurrentDirectory))
        {
        }

        public MethodCallDto(ILogger logger)
        {
            _logger = logger;
        }
        public void Invoke(object sender, MethodSignatureDto methodSignature,
            object[] arguments = null)
        {
            Invoke(sender, methodSignature, AppDomain.CurrentDomain, arguments);
        }

        public void Invoke(object sender, MethodSignatureDto methodSignature,
            AppDomain domain, object[] arguments = null)
        {
            try
            {
                methodSignature.ToMethod(domain).Invoke(sender, arguments);

                _logger.Debug($"{methodSignature.MethodName} invoked successfully");
            }
            catch (Exception e)
            {
                _logger.Fatal(e);
            }
        }

        public async Task<IAsyncResult> InvokeAsync(object sender, MethodSignatureDto methodSignature, object[] arguments = null)
        {
            return await InvokeAsync(sender, methodSignature, AppDomain.CurrentDomain, arguments);
        }

        public async Task<IAsyncResult> InvokeAsync(object sender, MethodSignatureDto methodSignature, AppDomain domain,
            object[] arguments = null)
        {
            var taskFactory = new TaskFactory();

            return await taskFactory.StartNew(async () =>
            {

                var result = (Task)methodSignature.ToMethod(domain).Invoke(sender, arguments);

                if (result.IsFaulted)
                {
                    if(result.Exception != null)
                        foreach(var exception in result.Exception.InnerExceptions)
                            _logger.Fatal($"{methodSignature.MethodName} failed to invoke with the following exception: {exception.Message}");

                    return;
                }

                if (result.IsCompleted)
                {
                    _logger.Debug($"{methodSignature.MethodName} invoked successfully");
                }

                await result;

            });
        }

    }


}