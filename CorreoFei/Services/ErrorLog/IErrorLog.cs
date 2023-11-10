namespace CorreoFei.Services.ErrorLog
{
    public interface IErrorLog
    {
        public Task ErrorLogAsync(String Mensaje);
    }
}
