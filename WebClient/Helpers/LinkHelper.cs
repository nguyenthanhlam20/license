using System.Text.RegularExpressions;

namespace WebClient.Helpers
{
    public static class LinkHelper
    {

        public static string GetSharedGoogleDriveLink(string link)
        {
            // Use a regular expression to extract the file ID from the link
            var match = Regex.Match(link, @"[?&]id=([a-zA-Z0-9_-]+)");
            if (match.Success)
            {
                return $"https://drive.google.com/file/d/{match.Groups[1].Value}/view";
            }

            // If the regular expression doesn't match, handle the link format accordingly
            return string.Empty; // or throw an exception, return a default value, etc.
        }

        public static string ExtractYouTubeVideoId(string url)
        {
            string pattern = @"[?&]v=([a-zA-Z0-9_-]+)";
            Regex regex = new Regex(pattern);
            Match match = regex.Match(url);

            if (match.Success)
            {
                string videoId = match.Groups[1].Value;
                return videoId;
            }
            else
            {
                return string.Empty;
            }
        }

        public static string GetEmbededGoogleFormUrl(string url)
        {
            Uri uri = new Uri(url);

            // Extract the form ID from the URL
            string[] segments = uri.Segments;
            if (segments.Length >= 4)
            {
                string formId = segments[3];

                // Construct the embedded form link
                string embeddedFormLink = $"https://docs.google.com/forms/d/e/{formId}/viewform?embedded=true";
                return embeddedFormLink;
            }
            else
            {
                return null;
            }
        }
        public static bool IsGoogleFormLink(string link)
        {
            // Check if the link contains the keywords "google.com/forms" or "google.com/url?q=forms"
            return link.Contains("google.com/forms") || link.Contains("google.com/url?q=forms");
        }

        public static string GetEmbededYoutubeUrl(string url)
        {
            Regex regex = new Regex(@"(?:youtu\.be/|v=|&v=)([a-zA-Z0-9_-]+)");
            Match match = regex.Match(url);

            return match.Success ? $"https://www.youtube.com/embed/{match.Groups[1].Value}" : string.Empty;
        }

        public static string ExtractVideoId(string url)
        {
            // Biểu thức chính quy để tìm kiếm ID video trong URL
            string[] patterns = {
            @"(?<=youtube\.com/watch\?v=)[^&]+",
            @"(?<=youtu\.be/)[^?]+",
            @"(?<=youtube\.com/embed/)[^?]+"
        };

            foreach (var pattern in patterns)
            {
                Match match = Regex.Match(url, pattern);
                if (match.Success)
                {
                    return $"https://www.youtube.com/embed/{match.Value}";
                }
            }

            // Nếu không tìm thấy ID video
            return "";
        }

    }
}
