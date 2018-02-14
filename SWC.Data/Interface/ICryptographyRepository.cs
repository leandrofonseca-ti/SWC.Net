using SWC.Data.Entity;
using System.Collections.Generic;

namespace SWC.Data.Interface
{
    public interface ICryptographyRepository
    {
        string Encrypt(string text);

        string Decrypt(string text);


    }
}
