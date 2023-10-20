namespace Blog.Application.Common.Abstractions;

using Blog.Application.Common.Data;

public interface IEmailNotifier
{
    public Task SendMessage(EmailMessage message);
}
