using System;
using System.Collections.Generic;
using System.Text;

namespace ClassModel.Hashpass
{
    public interface IHashpass
    {
        string hashPass(string pass);
        Boolean checkPass(string hashedPassword, string providedPassword);
    }
}
