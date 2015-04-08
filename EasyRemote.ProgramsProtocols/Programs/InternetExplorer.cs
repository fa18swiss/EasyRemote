using EasyRemote.ProgramsProtocols.Protocols;

namespace EasyRemote.ProgramsProtocols.Programs
{
    class InternetExplorer : _HttpClient
    {
        public InternetExplorer(HTTP http, HTTPS https)
            : base("InternetExplorer", http, https)
        {
        }
    }
}
