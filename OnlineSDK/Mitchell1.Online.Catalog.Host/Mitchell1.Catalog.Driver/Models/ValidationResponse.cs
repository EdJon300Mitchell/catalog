namespace Mitchell1.Catalog.Driver.Models
{
    public class ValidationResponse
    {
        private string _errorMessage;
        public bool IsTrue
        {
            get
            {
                return string.IsNullOrEmpty(_errorMessage);
            }
        }
        public string ErrorMessage
        {
            get
            {
                return _errorMessage;
            }
        }
        public ValidationResponse(string errorMessage)
        {
            _errorMessage = errorMessage;
        }
    }
}
