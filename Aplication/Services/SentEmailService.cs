using Common.Exceptions;
using Common.GlobalExceptionsResponses;
using Domain.Entities;
using Domain.Enums;
using Microsoft.Extensions.Configuration;
using Repository.Common;
using System.Net.Mail;
using System.Net;
using Aplication.Abstractions;
using Aplication.DTOs;

namespace Aplication.Services;

public class SentEmailService(IUnitOfWork unitOfWork, IConfiguration configuration) : ISentEmailService
{
	private readonly IUnitOfWork _unitOfWork = unitOfWork;
	private readonly IConfiguration _configuration = configuration;


	public async Task<ErrorResponse> SendEmailForForgetPassword(ForgetPasswordDto forgetPasswordDto)
	{
		var isExist = await _unitOfWork.UserRepository.GetUserByEmailAsync(forgetPasswordDto.Email);

		if (isExist == null)
			throw new BadRequestException();

		string hostingerSmtpServer = "smtp.hostinger.com";
		int port = 587;
		string senderEmail = "noreply@birdoc.az";
		string senderPassword = "AciDil123))";
		string recipientEmail = forgetPasswordDto.Email;

		Random random = new Random();
		string securityKey = random.Next(100000, 1000000).ToString();

		string subject = "Account Activation and Password Setup";

		string body = @$"
            <!DOCTYPE html>
            <html lang=""en"">
            <head>
                <meta charset=""UTF-8"" />
                <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"" />
                <title>Setup Password</title>
                <link rel=""preconnect"" href=""https://fonts.googleapis.com"" />
                <link rel=""preconnect"" href=""https://fonts.gstatic.com"" crossorigin />
                <link href=""https://fonts.googleapis.com/css2?family=Montserrat:wght@100;200;300;400;500;600;700;800;900&display=swap"" rel=""stylesheet"" />
            </head>
            <body style=""margin: 0; padding: 0; font-family: 'Montserrat', sans-serif; background: #f0f0f0;"">
                <table role=""presentation"" cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""border-collapse: collapse;"">
                    <tr>
                        <td align=""center"" style=""padding: 20px 0;"">
                            <table role=""presentation"" cellpadding=""0"" cellspacing=""0"" width=""500"" style=""
                                border-radius: 6px;
                                border: 1px solid var(--borderColor);
                                box-shadow: 0 10px 20px -2px rgba(0, 0, 0, 0.1),
                                    0 4px 15px -2px rgba(0, 0, 0, 0.01);
                                background-color: #ffffff;
                            "">
                                <tr>
                                    <td style=""
                                        background: #3498db;
                                        border-top-left-radius: 6px;
                                        border-top-right-radius: 6px;
                                        text-align: center;
                                        padding: 20px;
                                    "">
                                        <h2 style=""color: #fff; text-transform: capitalize; font-weight: 500; text-align: center;"">
                                          Password Reset
                                        </h2>
                                    </td>
                                </tr>
                                <tr>
                                    <td style=""padding: 30px 20px;"">
                                        <p style=""
                                            color: #4a4a4a;
                                            text-align: start;
                                            margin: 0;
                                            padding-bottom: 10px;
                                        "">
                                            Təhlükəsizlik kodunu heç kəslə paylaşmayın. Kod 15 dəqiqə keçərlidir.
                                        </p>
                                        <div style=""
                                            height: 20px;
                                            padding: 10px;
                                            border: 1px solid #3498db;
                                            border-radius: 4px;
                                            display: flex;
                                            justify-content: start;
                                            align-items: center;
                                        "">
                                            <p style=""margin: 0; text-align: center"">" + securityKey + @"</p>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </body>
            </html>";

		MailMessage message = new MailMessage(senderEmail, recipientEmail, subject, body);

		message.IsBodyHtml = true;

		using (SmtpClient smtpClient = new SmtpClient(hostingerSmtpServer, port))
		{
			smtpClient.Credentials = new NetworkCredential(senderEmail, senderPassword);
			smtpClient.EnableSsl = true;
			smtpClient.Send(message);
		}

		var newEmail = new SentEmail()
		{
			SecurityKey = securityKey,
			Email = forgetPasswordDto.Email,
			Purpose = OtpPurposes.ForgetPassword
		};

		await _unitOfWork.SentEmailRepository.CreateAsync(newEmail);

		await _unitOfWork.SaveChangesAsync();

		return new ErrorResponse();
	}

	public async Task<ErrorResponse> SendEmailForUpdateProfile(int userId)
	{
		var user = await _unitOfWork.UserRepository.GetByIdAsync(userId);

		if (user == null)
			throw new BadRequestException();

		string hostingerSmtpServer = "smtp.hostinger.com";
		int port = 587;
		string senderEmail = "noreply@birdoc.az";
		string senderPassword = "AciDil123))";
		string recipientEmail = user.Email;

		Random random = new Random();
		string securityKey = random.Next(100000, 1000000).ToString();

		string subject = "Account Activation and Password Setup";

		string body = @$"
            <!DOCTYPE html>
            <html lang=""en"">
            <head>
                <meta charset=""UTF-8"" />
                <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"" />
                <title>Setup Password</title>
                <link rel=""preconnect"" href=""https://fonts.googleapis.com"" />
                <link rel=""preconnect"" href=""https://fonts.gstatic.com"" crossorigin />
                <link href=""https://fonts.googleapis.com/css2?family=Montserrat:wght@100;200;300;400;500;600;700;800;900&display=swap"" rel=""stylesheet"" />
            </head>
            <body style=""margin: 0; padding: 0; font-family: 'Montserrat', sans-serif; background: #f0f0f0;"">
                <table role=""presentation"" cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""border-collapse: collapse;"">
                    <tr>
                        <td align=""center"" style=""padding: 20px 0;"">
                            <table role=""presentation"" cellpadding=""0"" cellspacing=""0"" width=""500"" style=""
                                border-radius: 6px;
                                border: 1px solid var(--borderColor);
                                box-shadow: 0 10px 20px -2px rgba(0, 0, 0, 0.1),
                                    0 4px 15px -2px rgba(0, 0, 0, 0.01);
                                background-color: #ffffff;
                            "">
                                <tr>
                                    <td style=""
                                        background: #3498db;
                                        border-top-left-radius: 6px;
                                        border-top-right-radius: 6px;
                                        text-align: center;
                                        padding: 20px;
                                    "">
                                        <h2 style=""color: #fff; text-transform: capitalize; font-weight: 500; text-align: center;"">
                                          Profile Update
                                        </h2>
                                    </td>
                                </tr>
                                <tr>
                                    <td style=""padding: 30px 20px;"">
                                        <p style=""
                                            color: #4a4a4a;
                                            text-align: start;
                                            margin: 0;
                                            padding-bottom: 10px;
                                        "">
                                            Təhlükəsizlik kodunu heç kəslə paylaşmayın. Kod 15 dəqiqə keçərlidir.
                                        </p>
                                        <div style=""
                                            height: 20px;
                                            padding: 10px;
                                            border: 1px solid #3498db;
                                            border-radius: 4px;
                                            display: flex;
                                            justify-content: start;
                                            align-items: center;
                                        "">
                                            <p style=""margin: 0; text-align: center"">" + securityKey + @"</p>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </body>
            </html>";

		MailMessage message = new MailMessage(senderEmail, recipientEmail, subject, body);

		message.IsBodyHtml = true;

		using (SmtpClient smtpClient = new SmtpClient(hostingerSmtpServer, port))
		{
			smtpClient.Credentials = new NetworkCredential(senderEmail, senderPassword);
			smtpClient.EnableSsl = true;
			smtpClient.Send(message);
		}

		var newEmail = new SentEmail()
		{
			SecurityKey = securityKey,
			Email = user.Email,
			Purpose = OtpPurposes.UpdateProfile
		};

		await _unitOfWork.SentEmailRepository.CreateAsync(newEmail);

		await _unitOfWork.SaveChangesAsync();

		return new ErrorResponse();
	}

	public async Task<ErrorResponse> SendEmailForRegistration(string email)
	{
		string hostingerSmtpServer = "smtp.hostinger.com";
		int port = 587;
		string senderEmail = "noreply@birdoc.az";
		string senderPassword = "AciDil123))";
		string recipientEmail = email;

		Random random = new Random();
		string securityKey = random.Next(100000, 1000000).ToString();

		string subject = "Account Activation and Password Setup";

		string body = @$"
            <!DOCTYPE html>
            <html lang=""en"">
            <head>
                <meta charset=""UTF-8"" />
                <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"" />
                <title>Setup Password</title>
                <link rel=""preconnect"" href=""https://fonts.googleapis.com"" />
                <link rel=""preconnect"" href=""https://fonts.gstatic.com"" crossorigin />
                <link href=""https://fonts.googleapis.com/css2?family=Montserrat:wght@100;200;300;400;500;600;700;800;900&display=swap"" rel=""stylesheet"" />
            </head>
            <body style=""margin: 0; padding: 0; font-family: 'Montserrat', sans-serif; background: #f0f0f0;"">
                <table role=""presentation"" cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""border-collapse: collapse;"">
                    <tr>
                        <td align=""center"" style=""padding: 20px 0;"">
                            <table role=""presentation"" cellpadding=""0"" cellspacing=""0"" width=""500"" style=""
                                border-radius: 6px;
                                border: 1px solid var(--borderColor);
                                box-shadow: 0 10px 20px -2px rgba(0, 0, 0, 0.1),
                                    0 4px 15px -2px rgba(0, 0, 0, 0.01);
                                background-color: #ffffff;
                            "">
                                <tr>
                                    <td style=""
                                        background: #3498db;
                                        border-top-left-radius: 6px;
                                        border-top-right-radius: 6px;
                                        text-align: center;
                                        padding: 20px;
                                    "">
                                        <h2 style=""color: #fff; text-transform: capitalize; font-weight: 500; text-align: center;"">
                                          Registrasiya
                                        </h2>
                                    </td>
                                </tr>
                                <tr>
                                    <td style=""padding: 30px 20px;"">
                                        <p style=""
                                            color: #4a4a4a;
                                            text-align: start;
                                            margin: 0;
                                            padding-bottom: 10px;
                                        "">
                                            Təhlükəsizlik kodunu heç kəslə paylaşmayın. Kod 15 dəqiqə keçərlidir.
                                        </p>
                                        <div style=""
                                            height: 20px;
                                            padding: 10px;
                                            border: 1px solid #3498db;
                                            border-radius: 4px;
                                            display: flex;
                                            justify-content: start;
                                            align-items: center;
                                        "">
                                            <p style=""margin: 0; text-align: center"">" + securityKey + @"</p>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </body>
            </html>";

		MailMessage message = new MailMessage(senderEmail, recipientEmail, subject, body);

		message.IsBodyHtml = true;

		using (SmtpClient smtpClient = new SmtpClient(hostingerSmtpServer, port))
		{
			smtpClient.Credentials = new NetworkCredential(senderEmail, senderPassword);
			smtpClient.EnableSsl = true;
			smtpClient.Send(message);
		}

		var newEmail = new SentEmail()
		{
			SecurityKey = securityKey,
			Email = email,
			Purpose = OtpPurposes.Registration
		};

		await _unitOfWork.SentEmailRepository.CreateAsync(newEmail);

		await _unitOfWork.SaveChangesAsync();

		return new ErrorResponse();
	}


}
