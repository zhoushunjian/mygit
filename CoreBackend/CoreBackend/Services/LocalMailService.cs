using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CoreBackend.Services
{
    public interface IMailService
    {
        void Send(string subject, string msg);
    }

    public class LocalMailService : IMailService
    {
        private readonly string _mailTo = Startup.Configuration["mailSettings:mailToAddress"];
        private readonly string _mailFrom = Startup.Configuration["mailSettings:mailFromAddress"];

        public void Send(string subject, string msg)
        {
            Debug.WriteLine($"从{_mailFrom}给{_mailTo}通过{nameof(LocalMailService)}发送了邮件");
        }
    }

    public class CloudMailService : IMailService
    {
        private readonly string _mailTo = "www.429225820@qq.com";
        private readonly string _mailFrom = "www.429225820@qq.com";
        private readonly ILogger<CloudMailService> _logger;

        public CloudMailService(ILogger<CloudMailService> logger)
        {
            _logger = logger;
        }

        public void Send(string subject, string msg)
        {
            _logger.LogInformation($"从{_mailFrom}给{_mailTo}通过{nameof(LocalMailService)}发送了邮件");
        }
    }
}
