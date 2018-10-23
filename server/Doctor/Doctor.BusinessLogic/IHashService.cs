using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctor.BusinessLogic
{
    public interface IHashService
    {
        bool VerifyHash(string value, byte[] hash, byte[] salt);
    }
}
