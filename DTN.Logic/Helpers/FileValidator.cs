
using DTN.Logic.Helpers.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace DTN.Logic.Helpers
{
    public class FileValidator : IFileValidator
    {
        private readonly List<string> _permittedExtensions;
        private readonly long _fileSizeLimit;


        public FileValidator(IConfiguration config)
        {
            _fileSizeLimit = Convert.ToInt64(config["FileSizeLimit"]);
            _permittedExtensions = config["PermittedExtensions"].Split(new char[] { ',' }).ToList();
        }

        public List<string> Validate(IFormFile file)
        {
            var errors = new List<string>();
            if (file == null || file.Length <= 0)
            {
                errors.Add("The file is not provided");
                return errors;
            }

            // File extension validation
            var fileName = file.FileName;
            var ext = Path.GetExtension(fileName).ToLowerInvariant();
            if (string.IsNullOrEmpty(ext) || !_permittedExtensions.Contains(ext))
            {
                errors.Add("The file type is invalid.");
            }

            // File size validation
            if (file.Length > _fileSizeLimit)
            {
                errors.Add("The file is too large.");
            }

            return errors;
        }
    }
}
