using EasyRemote.ProgramsProtocols.Protocols;

namespace EasyRemote.ProgramsProtocols.Programs
{
    class Firefox : _HttpClient
    {
        public Firefox(HTTP http, HTTPS https)
            : base("Firefox", http, https)
        {
        }
    }
}
