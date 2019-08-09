using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace SalesForceLeadLogic
{
    public interface ISalesForceProcessor
    {
        Task<bool> CreateSFObject<T>(T sfObject, string objectName, IConfiguration config);
    }
}