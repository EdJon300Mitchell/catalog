using System.IO;
using System.Reflection;

namespace Mitchell1.Online.Catalog.Host.API
{
    public static class HtmlErrorGenerator
    {
        private static string defaultErrorMessage = "Unable to connect to Catalog. Please check your Internet Connectivity and try again. If problem persists, check with Catalog Vendor for assistance.";

        private static string GetResource(int error, string title, string description, OnlineCatalogInformation catalog)
        {
            var resourceName = "Mitchell1.Online.Catalog.Host.API.ErrorPage.html";
            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
            {
                if (stream != null)
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        string result = reader.ReadToEnd();
                        string footer = "";
                        if (catalog != null && (!string.IsNullOrEmpty(catalog.SupportUrl) || !string.IsNullOrEmpty(catalog.SupportPhone)))
                        {
                            footer = $"<footer><p>{catalog.DisplayName} ";
                            if (!string.IsNullOrEmpty(catalog.SupportUrl))
                            {
                                footer += $"Support: <a target='_blank' href='{catalog.SupportUrl}'>{catalog.SupportUrl}</a> ";
                            }

                            if (!string.IsNullOrEmpty(catalog.SupportPhone))
                            {
                                footer += $"Phone: <b>{catalog.SupportPhone}</b> ";
                            }

                            footer += "</p></footer>";
                        }

                        return result.Replace("{error}", error.ToString()).Replace("{title}", title).Replace("{description}", description).Replace("{footer}", footer);
                    }
                }
            }

            return $"Catalog {title} Error {error} - {description}";
        }

        public static string GetFormattedError(int error, string errorString, OnlineCatalogInformation catalog)
        {
            return GetResource(error, errorString, defaultErrorMessage, catalog);
        }

        public static string GetFormattedError(int httpCode, OnlineCatalogInformation catalog)
        {
            string title = "Unknown Error";
            string description = defaultErrorMessage;
            switch (httpCode)
            {
                case 400:
                    title = "Bad Request";
                    description = "The server cannot process the request due to something that is perceived to be a client error.";
                    break;
                case 401:
                case 403:
                    title = "Access Denied - Unauthorized";
                    description = "The requested resource requires an authentication.";
                    break;
                case 404:
                    title = "Not Found";
                    description = "The requested resource was not found. Please try again and/or check with Catalog Vendor for assistance if problem persists";
                    break;
                case 500:
                case 501:
                case 502:
                case 503:
                    title = "Webserver Not Available";
                    description = "Catalog Vendor seems to be experiencing issues. Please try again and/or check with Catalog Vendor for assistance if problem persists.";
                    break;
            }
            return GetResource(httpCode, title, description, catalog);
        }
    }
}
