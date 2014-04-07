using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Invent.Interfaces
{
    public interface IRegister
    {
        int Id { get; }
        string InvName { get; }
        string InvNumber { get; }
        DateTime DateInput { get; }
        DateTime DateStatus { get; }
        string Comment { get; }
        IUser UserMaterial { get; }
        IUser UserOwner { get; }
        IBaseUnit Status { get; }
    }
}
