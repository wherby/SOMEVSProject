using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using PowerShellTestTools;

namespace PowerShellAutomation
{
    /// <summary>
    /// [CMDString]Test: test class for [CMD] cmdlet
    /// </summary>
    [TestClass]
    public partial class [CMDString]Test
    {
        public [CMDString]Test()
        {
            //
            // TODO: Add constructor logic here
            //
        }


        private TestContext testContextInstance;

        private static TestLog log;
        private static PowershellMachine psMachine;
        
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        [TestInitialize()]
        public void TestInit()
        {
            log.LogInfo("--------Test Init Start---------");

            log.LogInfo("--------Test Init End---------");            
        }

        // Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void TestTearDown()
        {
            log.LogInfo("--------Test Clean Up Start---------");

            log.LogInfo("--------Test Clean Up End---------");            
        }

        // Use ClassInitialize to run code before running the first test in the class 
        [ClassInitialize]
        public static void ESIPSTestClassInit(TestContext testContext)
        {
            try
            {
                ClassInit();
            }
            catch(Exception e)
            {
                log.BypassTest(e);
            }
        }

        public static void ClassInit()
        {

            // Get log instance
            log = TestLog.GetInstance();

            log.LogInfo("--------Class Init Start---------");

            // Open PowerShell Session
            psMachine = new PowershellMachine();

            // Import ESIPSToolKit
            TestSetup.InitForEMCStorage(psMachine);
            TestSetup.DisconnectSystem(psMachine);
    

            log.LogInfo("--------Class Init End---------");
            
        }

        // Use ClassCleanup to run code after all tests in a class have run        
        [ClassCleanup]
        public static void ESIPSTestClassCleanUP()
        {
            log.LogInfo("--------Class Clean Up Start---------");
            
            log.LogInfo("--------Class Clean Up End---------");
        }
        #endregion

        /// <summary>  
        /// ParseCmd:
        ///    Parse command string to a [CMD] instance.  
        /// </summary>
        /// <param name="cmd">command string retrieved from parameter combination file</param>  
        /// <returns>[CMD] instance</returns>  
        public [CMDString] ParseCmd(string cmd)
        {
            #region AutoGenerate
[TEMP3]

            #endregion


            if (cmd.IndexOf("silent", StringComparison.OrdinalIgnoreCase) > 0)
            {
                silent = "Silent";
            }

            [CMDString] instance = new [CMDString]([parameters] cmd);
            return instance;
        }


        /// <summary>  
        /// [CMD]:
        ///    The method to implement [CMD] poistive test case.  
        /// </summary>  
        /// <param name="cmd">command string retrieved from parameter combination file</param>  
        /// <returns></returns>  
        public void [CMDString]TestMethod(string cmd)
        {

            log.LogTestCase(testContextInstance.TestName + ": " + cmd);

            [CMDString] cmdClass = ParseCmd(cmd);

            cmdClass.VerifyTheCMD(psMachine);
        }

        /// <summary>  
        /// [CMDString]NegativeTestMethod:
        ///    The method to implement [CMD] negative test case.  
        /// </summary>  
        /// <param name="cmd">command string retrieved from parameter combination file</param>  
        /// <returns></returns>  
        public void [CMDString]NegativeTestMethod(string cmd)
        {

            log.LogTestCase(testContextInstance.TestName + ": " + cmd);

            bool caseFail = false;

            [CMDString] [cmdstring]Class = ParseCmd(cmd);

            try
            {
                [cmdstring]Class.VerifyTheCMD(psMachine);
            }
            catch (PSException psEx)
            {
                log.LogTestCase(string.Format("Test with {0} failed.", [cmdstring]Class.GetFullString()));
                log.LogTestCase(psEx.messageDetail);
                caseFail = true;
            }
            log.AreEqual<bool>(true, caseFail, "Negative test case result:");

        }
    }
}
