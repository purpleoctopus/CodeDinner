using System.ComponentModel.DataAnnotations;

namespace CodeBreakfast.API;

public class UploadFileRequest
{
    [Required]
    public IFormFile File { get; set; }
}