using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICAN.SIC.Plugin.VLCRemote.DataTypes
{
    public class VLCResponse : IVLCResponse
    {
        private string log;

        public VLCResponse(string log)
        {
            this.log = log;
        }

        public string Log { get { return this.log; } }
    }
}
