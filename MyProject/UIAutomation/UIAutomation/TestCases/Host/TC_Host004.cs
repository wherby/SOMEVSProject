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
using UIAutomation.UIMaps.AddHostWizardClasses;
using UIAutomation.UIMaps.MainWindowClasses;


namespace UIAutomation.TestCases.Host
{
    /// <summary>
    /// DEscription:
    ///      Verify ESI does not allow duplicate entry of Host System
    /// </summary>
    [CodedUITest]
    public class TC_Host004
    {
        public TC_Host004()
        {
        }

        [TestMethod]
        public void Host004()
        {
            // To generate code for this test, select "Generate Code for Coded UI Test" from the shortcut menu and select one of the menu items.
            // For more information on generated code, see http://go.microsoft.com/fwlink/?LinkId=179463
            string[] infor = new string[5] { null, "10.5.222.69", null, null, null };

            this.MainWindow.RemoveHostSystem(infor[1], RemoveSystem.AvailableSystemRightClick);
            this.MainWindow.AddHostSystem(AddSystem.LeftBranchAction);
            infor[1] = this.AddHostWizard.InputHostInfoAndTest(infor);
            this.AddHostWizard.ClickTestConnectionButton();
            this.AddHostWizard.AssertTestConnectionSucceed();
            this.AddHostWizard.ClickOKButton(OKButton.TestConnection);
            this.AddHostWizard.ClickAddButton();
            this.MainWindow.VerifyAddedHostSystem(infor[1], MachineType.VirtualMachine);

            this.MainWindow.AddHostSystem(AddSystem.LeftBranchAction);
            infor[1] = this.AddHostWizard.InputHostInfoAndTest(infor);
            this.AddHostWizard.ClickTestConnectionButton();
            this.AddHostWizard.AssertTestConnectionSucceed();
            this.AddHostWizard.ClickOKButton(OKButton.TestConnection);
            this.AddHostWizard.ClickAddButton();
            this.AddHostWizard.AssertHostPresent();
            this.AddHostWizard.ClickCancelButton();

            this.MainWindow.RemoveHostSystem(infor[1], RemoveSystem.AvailableSystemRightClick);
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
        
        public AddHostWizard AddHostWizard
        {
            get
            {
                if ((this.mAddHostWizardMap == null))
                {
                    this.mAddHostWizardMap = new AddHostWizard();
                }

                return this.mAddHostWizardMap;
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
        private AddHostWizard mAddHostWizardMap;
        private MainWindow mMainWindowMap;
    }
}
