using Microsoft.AspNetCore.Http;

namespace DTN.Services.Interface
{
    public interface IFileValidator
    {
        List<string> Validate(IFormFile file);
    }
}
