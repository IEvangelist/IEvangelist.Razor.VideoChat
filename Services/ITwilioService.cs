namespace IEvangelist.Razor.VideoChat.Services
{
    public interface ITwilioService
    {
        string GetTwilioJwt(string identity);
    }
}
