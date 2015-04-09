using EasyRemote.ProgramsProtocols.Protocols;

namespace EasyRemote.ProgramsProtocols.Programs
{
    class Chrome : _HttpClient
    {
        public Chrome(HTTP http, HTTPS https)
            : base("Chrome", http, https)
        {
        }
    }
}
