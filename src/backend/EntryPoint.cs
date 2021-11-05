using System.Threading.Tasks;

namespace backend
{
    public static class EntryPoint
    {
        public static Task Main(string[] args) => new Program(args).RunAsync();
    }

}