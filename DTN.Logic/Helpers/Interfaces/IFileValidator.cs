using Microsoft.AspNetCore.Http;

namespace DTN.Logic.Helpers.Interfaces
{
    public interface IFileValidator
    {
        List<string> Validate(IFormFile file);
    }
}
