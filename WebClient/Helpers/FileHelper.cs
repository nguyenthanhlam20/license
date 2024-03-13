using Core.Constants;
using Core.Enums;

namespace WebClient.Helpers
{
    public static class FileHelper
    {
        public static async Task<byte[]> ReadFormFileAsync(IFormFile file)
        {
            using (var memoryStream = new MemoryStream())
            {
                if (file != null)
                {
                    await file.CopyToAsync(memoryStream);
                    return memoryStream.ToArray();
                }
                return null;
            }
        }

        public static Tuple<bool, string> IsImageFile(IFormFile file, FileSizeLimit fileSize)
        {
            string message = String.Empty;
            try
            {
                if (file == null || file.Length == 0)
                {
                    throw new Exception("Chưa chọn file");
                }

                // Limit file size (in bytes)
                double fileSizeLimit = (int)fileSize * Math.Pow(1024, 2);

                if (file.Length > fileSizeLimit)
                {
                    throw new Exception($"File không thể vượt quá {(int)fileSize}MB");
                }

                // Get the file extension
                string fileExtension = GetFileExtension(file.FileName).ToLower();

                // Allowed file extensions
                string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif" };

                bool isImage = Array.IndexOf(allowedExtensions, fileExtension) != -1;

                if(isImage == false)
                {
                    throw new Exception($"File không đúng định dạng. Hãy chọn file định dạng .jpg, .jpeg, .png, .gif");
                }
                // Check if the file extension is allowed
                return new Tuple<bool, string>(isImage, "File đúng định dạng");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new Tuple<bool, string>(false, ex.Message);
            }
        }

        public static Tuple<bool, string> IsDocumentFile(IFormFile file, FileSizeLimit fileSize)
        {
            string message = String.Empty;
            try
            {
                if (file == null || file.Length == 0)
                {
                    throw new Exception("Chưa chọn file");
                }

                // Limit file size (in bytes)
                double fileSizeLimit = (int)fileSize * Math.Pow(1024, 2);

                if (file.Length > fileSizeLimit)
                {
                    throw new Exception($"File không thể vượt quá {(int)fileSize}MB");
                }

                // Get the file extension
                string fileExtension = GetFileExtension(file.FileName).ToLower();

                // Allowed document file extensions
                string[] allowedExtensions = { ".png", ".docx", ".pdf", ".txt" };

                bool isDocument = Array.IndexOf(allowedExtensions, fileExtension) != -1;

                if (isDocument == false)
                {
                    throw new Exception($"File không đúng định dạng. Hãy chọn file định dạng .docx, .pdf, .txt, .png");
                }

                // Check if the file extension is allowed
                return new Tuple<bool, string>(isDocument, "File đúng định dạng");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new Tuple<bool, string>(false, ex.Message);
            }
        }

        private static string GetFileExtension(string fileName)
        {
            try
            {
                if (string.IsNullOrEmpty(fileName))
                {
                    return string.Empty;
                }

                int lastDotIndex = fileName.LastIndexOf('.');
                return lastDotIndex >= 0 ? fileName.Substring(lastDotIndex) : string.Empty;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return String.Empty;
            }
        }

    }
}
