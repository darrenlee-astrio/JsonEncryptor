using System.Security;

namespace Application.Services.Encryption.Models;

public class EncryptionBase
{
    protected SecureString CreateSecureString(string regularString)
    {
        SecureString secureString = new SecureString();
        foreach (char c in regularString)
        {
            secureString.AppendChar(c);
        }
        secureString.MakeReadOnly();
        return secureString;
    }
}
