using UnityEngine;

[CreateAssetMenu(fileName = "ErrorMessages", menuName = "Errors/ErrorMessages", order = 1)]
public class ErrorMessages : ScriptableObject
{
    public ErrorType currentErrorType;
    public ErrorMessage[] errorMessages;

    // Function to get error message based on the selected enum
    public string GetErrorMessage(ErrorType errorType)
    {
        foreach (ErrorMessage errorMessage in errorMessages)
        {
            if (errorMessage.errorType == errorType)
            {
                return errorMessage.message;
            }
        }

        return "Unknown error type.";
    }
}
 [System.Serializable]
    public class ErrorMessage
    {
        public ErrorType errorType;
        [TextArea]
        public string message;
    }

public enum ErrorType
{
    None,
    NetworkError,
    FileNotFoundError,
    InvalidInput,
    UnauthorizedAccess,
    UnknownError
}
