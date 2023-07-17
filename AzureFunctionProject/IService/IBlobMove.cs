using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace AzureFunctionProject.IService
{
    public interface IBlobMove
    {
        bool Move(string blobName);
    }
}