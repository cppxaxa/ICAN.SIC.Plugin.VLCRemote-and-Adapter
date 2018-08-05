using ICAN.SIC.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICAN.SIC.Plugin.VLCRemote.DataTypes
{
    public interface IVLCResponse : IMessage
    {
        string Log { get; }
    }
}
