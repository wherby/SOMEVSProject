using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Input;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UITest.Extension;
using Keyboard = Microsoft.VisualStudio.TestTools.UITesting.Keyboard;
using UIAutomation.UIMaps.CreateDiskWizardClasses;
using UIAutomation.UIMaps.MainWindowClasses;


namespace UIAutomation.TestCases.Host
{
    /// <summary>
    /// Summary description for TC_Host009
    /// </summary>
    [CodedUITest]
    public class TC_Host009
    {
        public TC_Host009()
        {
        }

        [TestMethod]
        public void Host009()
        {
            // To generate code for this test, select "Generate Code for Coded UI Test" from the shortcut menu and select one of the menu items.
            // For more information on generated code, see http://go.microsoft.com/fwlink/?LinkId=179463

            Dictionary<string, string> dict = new Dictionary<string, string>();

            dict.Add("StorageName", "vnx167_168 - APM00113902390");
            dict.Add("PoolName", "ESI_TEST_POOL_1");
           // dict.Add("Name", "lun_1");
           // dict.Add("Description", "hello");
            dict.Add("Size", "1");
            dict.Add("Unit", "GB");
            dict.Add("ServiceNode", "SPA");
            dict.Add("ProvisionType", "Thick");
       //     dict.Add("PartitionType", "GPT");
        //    dict.Add("VolumeSize", "");
       //     dict.Add("FileSystem", "NTFS");
      //      dict.Add("AllocationUnitSize", "512");
        //    dict.Add("VolumeLable", "Test");
        //    dict.Add("DriveLetter", "M");
            dict.Add("MountPath", @"c:\test1\test3");

            //this.CreateDiskWizard.SelectStorageSystem(dict);
            //this.CreateDiskWizard.SelectStoragePool(dict);        
            //this.CreateDiskWizard.InputNewLunInfo(dict);
            this.CreateDiskWizard.SetDiskPreparation(dict);
        }

        #region Additional test attributes

        // You can use the following additional attributes as you write your tests:

        ////Use TestInitialize to run code before running each test 
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{        
        //    // To generate code for this test, select "Generate Code for Coded UI Test" from the shortcut menu and select one of the menu items.
        //    // For more information on generated code, see http://go.microsoft.com/fwlink/?LinkId=179463
        //}

        ////Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{        
        //    // To generate code for this test, select "Generate Code for Coded UI Test" from the shortcut menu and select one of the menu items.
        //    // For more information on generated code, see http://go.microsoft.com/fwlink/?LinkId=179463
        //}

        #endregion

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

        public CreateDiskWizard CreateDiskWizard
        {
            get
            {
                if ((this.mCreateDiskWizardMap == null))
                {
                    this.mCreateDiskWizardMap = new CreateDiskWizard();
                }

                return this.mCreateDiskWizardMap;
            }
        }

        public MainWindow MainWindow
        {
            get
            {
                if ((this.mMainWindowMap == null))
                {
                    this.mMainWindowMap = new MainWindow();
                }

                return this.mMainWindowMap;
            }
        }

        private TestContext testContextInstance;
        private CreateDiskWizard mCreateDiskWizardMap;
        private MainWindow mMainWindowMap;
    }
}
