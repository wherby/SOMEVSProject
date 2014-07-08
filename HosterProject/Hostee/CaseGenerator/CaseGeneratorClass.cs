using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HosteeBase;
using DomainFile;
using System.IO;
using System.Reflection;


namespace CaseGenerator
{
    [Serializable]
    public partial class CaseGeneratorClass : BaseHostee
    {
        public CaseGeneratorClass()
        {
            this.PrepareEnv();
        }

        public override void Init()
        {

        }

        public void Invoke()
        {
 
        }

    }
}
