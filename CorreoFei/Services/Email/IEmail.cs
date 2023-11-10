using System.Net.Mail;

namespace CorreoFei.Services.Email
{
    public interface IEmail
    {
        public Task<bool> EnviarCorreoAsync(String tema, String para, String cc, String bcc, String cuerpo, Attachment adjunto = null);
    }
}
