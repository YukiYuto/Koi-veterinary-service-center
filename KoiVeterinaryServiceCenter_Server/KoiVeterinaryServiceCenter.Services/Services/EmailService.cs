using KoiVeterinaryServiceCenter.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace KoiVeterinaryServiceCenter.Services.Services
{
    public class EmailService : IEmailService
    {
        public Task<bool> SendEmailAsync(string toEmail, string subject, string body)
        {
            throw new NotImplementedException();
        }
    }
}
