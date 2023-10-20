namespace Blog.Infrastructure.Email;

using System.Threading.Tasks;

using MailKit.Net.Smtp;
using MimeKit;

using Blog.Application.Common.Abstractions;
using Blog.Application.Common.Data;

public class EmailNotifier : IEmailNotifier
{
    private readonly EmailConfiguration emailConfig;

    public EmailNotifier(EmailConfiguration emailConfig)
    {
        this.emailConfig = emailConfig;
    }

    public async Task SendMessage(EmailMessage message)
    {
        MimeMessage emailMessage = createEmailMessage(message);
        await sendMessage(emailMessage);
    }

    private MimeMessage createEmailMessage(EmailMessage message)
    {
        var emailMessage = new MimeMessage();
        emailMessage.From.Add(new MailboxAddress("email", emailConfig.From));
        emailMessage.To.Add(new MailboxAddress("email", message.To));
        emailMessage.Subject = message.Title;
        emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = message.Content };
        return emailMessage;
    }

    async private Task sendMessage(MimeMessage mailMessage)
    {
        using var client = new SmtpClient();
        await client.ConnectAsync(emailConfig.SmtpServer, emailConfig.Port, false);
        client.AuthenticationMechanisms.Remove("XOAUTH2");
        await client.AuthenticateAsync(emailConfig.UserName, emailConfig.Password);
        await client.SendAsync(mailMessage);
        await client.DisconnectAsync(true);
    }
}