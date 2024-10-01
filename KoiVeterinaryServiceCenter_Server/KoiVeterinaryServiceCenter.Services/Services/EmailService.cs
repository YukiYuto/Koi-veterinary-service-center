using KoiVeterinaryServiceCenter.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using KoiVeterinaryServiceCenter.DataAccess.IRepository;
using KoiVeterinaryServiceCenter.Model.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace KoiVeterinaryServiceCenter.Services.Services
{
    public class EmailService : IEmailService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="unitOfWork"></param>
        /// <param name="mapper"></param>
        /// <param name="userManager"></param>
        public EmailService(IConfiguration configuration, IUnitOfWork unitOfWork, IMapper mapper,
            UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _configuration = configuration;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="toMail"></param>
        /// <param name="templateName"></param>
        /// <param name="replacementValue"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private async Task<bool> SendEmailFromTemplate(string toMail, string templateName, string replacementValue)
        {
            // Truy vấn cơ sở dữ liệu để lấy template
            var template = await _unitOfWork.EmailTemplateRepository.GetAsync(t => t.TemplateName == templateName);

            if (template == null)
            {
                // Xử lý khi template không tồn tại
                throw new Exception("Email template not found");
            }

            // Sử dụng thông tin từ template để tạo email
            string subject = template.SubjectLine;
            string body = $@"
        <html>
        <body>
            <h1>{template.SubjectLine}</h1>
            <h2>{template.PreHeaderText}</h2>
            <p>{template.BodyContent}</p>
            <p><a href='{replacementValue}' style='padding: 10px 20px; color: white; background-color: #007BFF; text-decoration: none;'>{template.CallToAction.Replace("{Login}", replacementValue)}</a></p>
            {template.FooterContent}
        </body>
        </html>";

            return await SendEmailToClientAsync(toMail, subject, body);
        }

        public async Task<bool> SendVerifyEmail(string toMail, string confirmationLink)
        {
            return await SendEmailFromTemplate(toMail, "SendVerifyEmail", confirmationLink);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="toEmail"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public async Task<bool> SendEmailToClientAsync(string toEmail, string subject, string body)
        {
            // Lấy thông tin cấu hình email từ file appsettings.json
            try
            {
                var fromEmail = _configuration["EmailSettings:FromEmail"];
                var fromPassword = _configuration["EmailSettings:FromPassword"];
                var smtpHost = _configuration["EmailSettings:SmtpHost"];
                var smtpPort = int.Parse(_configuration["EmailSettings:SmtpPort"]);

                // Tạo đối tượng MailMessage
                var message = new MailMessage(fromEmail, toEmail, subject, body);
                message.IsBodyHtml = true;

                // Tạo đối tượng SmtpClient và gửi email
                using var smtpClient = new SmtpClient(smtpHost, smtpPort)
                {
                    Credentials = new NetworkCredential(fromEmail, fromPassword),
                    EnableSsl = true
                };
                await smtpClient.SendMailAsync(message);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}