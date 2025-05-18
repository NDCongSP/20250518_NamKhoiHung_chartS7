using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiamSat.Scada
{
    public class model
    {
        public string Operator { get; set; } = "ZAHADI";
        public string LogMasterSN { get; set; } = "LM2-586";
        public string TorqueMasterSN { get; set; } = "8025-5020";
        public string Date { get; set; } = "09 APR 25 (10:29:50)";
        public string ConsoleSN { get; set; } = "8018-5032";
        public string Tool { get; set; } = "Series Jar";
        public string JobNo { get; set; } = "ESSEMBLE HQ650 JAR";
        public string Series { get; set; } = "";
        public string ToolSN { get; set; } = "OSC115748A";

        public string ConnectionName { get; set; } = "LFJ-KM";

        public double Max { get; set; } = 0;
        public double Target { get; set; } = 0;
        public double LoggedTorque { get; set; } = 0;
    }
}