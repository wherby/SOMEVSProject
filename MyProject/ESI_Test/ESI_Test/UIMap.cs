namespace ESI_Test
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Input;
    using System.CodeDom.Compiler;
    using System.Text.RegularExpressions;
    using Microsoft.VisualStudio.TestTools.UITest.Extension;
    using Microsoft.VisualStudio.TestTools.UITesting;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Keyboard = Microsoft.VisualStudio.TestTools.UITesting.Keyboard;
    using Mouse = Microsoft.VisualStudio.TestTools.UITesting.Mouse;
    using MouseButtons = System.Windows.Forms.MouseButtons;
    using System.Security;
    using Microsoft.VisualStudio.TestTools.UITesting.WinControls;
    
    
    public partial class UIMap
    {
        public void ModifiedStartESI()
        {
            string user = "administrator";
            string pswd = "Password!";
            string domain = "sr5dom";
            SecureString s_pswd = new SecureString();

            foreach (char c in pswd)            
                s_pswd.AppendChar(c);

          
            // Launch '%SystemRoot%\System32\mmc.exe' with arguments '"C:\Program Files\EMC\EMC Storage Integrator\ESIx64.msc"'
            ApplicationUnderTest mmcApplication = ApplicationUnderTest.Launch(this.StartESIParams.ExePath, this.StartESIParams.AlternateExePath, 
                "\"C:\\Program Files\\EMC\\EMC Storage Integrator\\ESIx64.msc\"", user, s_pswd, domain);
         }
        
    }
}
