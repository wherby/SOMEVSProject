using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Input;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UITesting.WinControls;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UITest.Extension;
using UIAutomation.UIMaps;
using UIAutomation.UIMaps.AddStorageWizardClasses;
using UIAutomation.UIMaps.MainWindowClasses;
using Keyboard = Microsoft.VisualStudio.TestTools.UITesting.Keyboard;


namespace UIAutomation.TestCases.LUN
{
    /// <summary>
    /// Summary description for CodedUITest1
    /// </summary>
    [CodedUITest]
    public class TC_LUN001
    {
        public TC_LUN001()
        {
        }

        [TestMethod]
        public void LUN001()
        {
            // To generate code for this test, select "Generate Code for Coded UI Test" from the shortcut menu and select one of the menu items.
            // For more information on generated code, see http://go.microsoft.com/fwlink/?LinkId=179463
            string friendlyName = "VNXBlock-irene";
            string[] vnxBlockInfo = { "gadmin", "rdc4msl", "10.5.223.167", "10.5.223.168" };
            string storageType = "VNX-Block";
            
            // Add Storage
            this.MainWindow.AddStorageSystem(AddSystem.LeftRootRightClick);
            this.AddStorageWizard.InputStorageInfo(storageType, friendlyName, vnxBlockInfo);
            this.AddStorageWizard.ClickAddButton();
            
            // Verify the added storage is online
            this.MainWindow.AssertStorageOnline(friendlyName, storageType);
           
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
        private TestContext testContextInstance;

        public AddStorageWizard AddStorageWizard
        {
            get
            {
                if ((this.mAddStorageWizardMap == null))
                {
                    this.mAddStorageWizardMap = new AddStorageWizard();
                }

                return this.mAddStorageWizardMap;
            }
        }

        private AddStorageWizard mAddStorageWizardMap;

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

        private MainWindow mMainWindowMap;
   
    }
}
