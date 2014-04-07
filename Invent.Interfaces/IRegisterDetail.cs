using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Invent.Interfaces
{
    public interface IRegisterDetail
    {
        int Id { get; }
        int Npp { get; }
        string Name { get; }
        string Comment { get; }
        bool Questions { get; }

        IRegister Register { get; }
        IUser User { get; }
        IBaseUnit WorkPlace { get; }
        IBaseUnit Place { get; }
        IBaseUnit DetailType { get; }
    }
}
