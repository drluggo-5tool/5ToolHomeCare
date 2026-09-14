using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace FiveToolHomeCare.Services;

public class EmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendLeadNotificationAsync(
        string firstName,
        string lastName,
        string email,
        string? phone,
        string serviceInterest,
        string? message,
        int leadId)
    {
        var smtpServer = _configuration["Email:SmtpServer"];
        var smtpPortText = _configuration["Email:SmtpPort"];
        var username = _configuration["Email:Username"];
        var password = _configuration["Email:Password"];

        if (string.IsNullOrWhiteSpace(smtpServer) ||
            string.IsNullOrWhiteSpace(smtpPortText) ||
            string.IsNullOrWhiteSpace(username) ||
            string.IsNullOrWhiteSpace(password))
        {
            throw new InvalidOperationException(
                "Email configuration is incomplete."
            );
        }

        var smtpPort = int.Parse(smtpPortText);

        var emailMessage = new MimeMessage();

        emailMessage.From.Add(
            new MailboxAddress(
                "5 Tool Home Care Website",
                username
            )
        );

        emailMessage.To.Add(
            new MailboxAddress(
                "5 Tool Home Care",
                "info@5toolhomecare.com"
            )
        );

        emailMessage.ReplyTo.Add(
            new MailboxAddress(
                $"{firstName} {lastName}",
                email
            )
        );

        emailMessage.Subject =
            $"New Website Lead - {serviceInterest}";

        emailMessage.Body = new TextPart("plain")
        {
            Text =
$"""
A new lead was submitted through the 5 Tool Home Care website.

Lead ID: {leadId}

Name: {firstName} {lastName}
Email: {email}
Phone: {phone ?? "(not provided)"}
Service: {serviceInterest}

Message:
{message ?? "(no message provided)"}
"""
        };

        using var smtpClient = new SmtpClient();

        await smtpClient.ConnectAsync(
            smtpServer,
            smtpPort,
            SecureSocketOptions.StartTls
        );

        await smtpClient.AuthenticateAsync(
            username,
            password
        );

        await smtpClient.SendAsync(emailMessage);

        await smtpClient.DisconnectAsync(true);
    }
}