namespace UIAutomation.UIMaps.MainWindowClasses
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
    using Microsoft.VisualStudio.TestTools.UITesting.WinControls;


    public enum AddSystem
    {
        // Right click the root of left tree "EMC Storage Integrator"
        LeftRootRightClick,
        // Right click left tree item
        LeftBranchRightClick,
        // Click the middle pane
        MiddlePaneClick,
        // Click the button in action pane when 
        // left tree "EMC Storage Integrator" is selected
        LeftRootAction,
        // Click the button in action pane when 
        // related left tree item is selected
        LeftBranchAction
    }

    public enum RemoveSystem
    {
        LeftItemRightClick,
        LeftItemAction,
        MiddlePaneRightClick,
        AvailableSystemRightClick
    }

    public enum MachineType
    {
        VirtualMachine,
        Hypervisor
    }

    public enum CreateLUN
    {
        // Right Click Storage item in the left tree
        RightClickStorage,
        // Click button in the right Action Pane specified for storage 
        StorageActionPane,
        // Click button in the right Action Pane specified for pool
        PoolActionPane
    }

    public partial class MainWindow
    {        
        /// <summary>
        /// RemoveHostSystem 
        /// Description:
        ///     Remove the host system if it is already added in ESI
        /// Input:
        ///     iPAddress: the IP address of the host system
        ///     removeSystem: different steps to remove system
        /// Output:
        ///     None
        /// </summary>
        public void RemoveHostSystem(string iPAddress, RemoveSystem removeSystem)
        {
            #region Variable Declarations                    
            WinMenuItem uIRemoveSystemMenuItem = this.UIItemWindow.UIContextMenu.UIRemoveSystemMenuItem;
            WinButton uIYesButton = this.UIRemoveSystemWindow.UIRemoveSystemDialog.UIYesButton;
            WinTreeItem uIHostsTreeItem = this.UIEMCStorageIntegratorWindow.UIEMCStorageIntegratorWindow1.UIItemWindow.UIEMCStorageIntegratorTreeItem.UIHostsTreeItem;           
            WinButton uIRemoveSystemButton = this.UIEMCStorageIntegratorWindow.UIEMCStorageIntegratorWindow1.UIActionsPane.UIRemoveSystemButton;
            WinTreeItem uIEMCStorageIntegratorTreeItem = this.UIEMCStorageIntegratorWindow.UIEMCStorageIntegratorWindow1.UIItemWindow.UIEMCStorageIntegratorTreeItem;
            WinTreeItem uIHostSystemTreeItem = GetHostSystemTreeItem(iPAddress);
            WinClient uIHostDataPanelClient = this.UIEMCStorageIntegratorWindow.UIEMCStorageIntegratorWindow1.UIHostSystemNodeGridCoWindow.UIDataPanelClient;
            WinClient uIDataPanelClient = this.UIEMCStorageIntegratorWindow.UIEMCStorageIntegratorWindow1.UIListGridControlWindow.UIDataPanelClient;
            WinRow uIRow = null;
            #endregion
            
            if (null == uIHostSystemTreeItem)//the host system is not added
            {
                return;
            }

            switch (removeSystem)
            {
                case RemoveSystem.LeftItemRightClick:

                    Mouse.Click(uIHostSystemTreeItem, new Point(34, 4));

                    this.UIEMCStorageIntegratorWindow.UIEMCStorageIntegratorWindow1.UIStatusProgressControWindow.UIItem30ProgressBar.WaitForControlNotExist();

                    // Right-Click 'EMC Storage Integrator' -> 'Hosts' tree item
                    Mouse.Click(uIHostSystemTreeItem, MouseButtons.Right, ModifierKeys.None, new Point(18, 6));

                    // Click 'Remove System' menu item
                    Mouse.Click(uIRemoveSystemMenuItem, new Point(45, 5));
                                        
                    break;
                case RemoveSystem.LeftItemAction:

                    Mouse.Click(uIHostSystemTreeItem, new Point(34, 4));

                    uIRemoveSystemButton.WaitForControlReady();

                    // Click 'Remove System' button
                    Mouse.Click(uIRemoveSystemButton, new Point(69, 13));

                    break;
                case RemoveSystem.MiddlePaneRightClick:

                    // Click 'EMC Storage Integrator' tree item
                    Mouse.Click(uIEMCStorageIntegratorTreeItem, new Point(103, 8));

                    uIRow = GetHostSystemRow(uIHostDataPanelClient, iPAddress);

                    if (null == uIRow)
                    {
                        return;
                    }

                    Mouse.Click(uIRow, MouseButtons.Right, ModifierKeys.None, uIRow.GetClickablePoint());

                    // Click 'Remove System' menu item
                    Mouse.Click(uIRemoveSystemMenuItem, new Point(45, 5));            

                    break;
                case RemoveSystem.AvailableSystemRightClick:

                    // Click 'EMC Storage Integrator' -> 'Hosts' tree item
                    Mouse.Click(uIHostsTreeItem, new Point(17, 9));

                    uIRow = GetHostSystemRow(uIDataPanelClient, iPAddress);

                    if (null == uIRow)
                    {
                        return;
                    }

                    Mouse.Click(uIRow, MouseButtons.Right, ModifierKeys.None, uIRow.GetClickablePoint());

                    // Click 'Remove System' menu item
                    Mouse.Click(uIRemoveSystemMenuItem, new Point(45, 5));

                    break;
            }

            // Click '&Yes' button
            Mouse.Click(uIYesButton, new Point(48, 12));               
                    
        }

        /// <summary>
        /// GetHostSystemRow 
        /// Description:
        ///     Get the row from DataPanelClient matching the IP address of a host system 
        /// Input:
        ///     uIHostDataPanelClient:the control which contains multiple rows
        ///     iPAddress: the IP address of a host system
        /// Output:
        ///     The row of a host system 
        /// </summary>
        WinRow GetHostSystemRow(WinClient uIHostDataPanelClient, string iPAddress)
        {
            #region Variable Declarations
            WinRow uIRow = null;
            UITestControlCollection controls = uIHostDataPanelClient.GetChildren();
            #endregion

            Assert.IsNotNull(iPAddress);

            foreach (UITestControl ctrl in controls)
            {
                uIRow = (WinRow)ctrl;
                string[] values = uIRow.Value.Split(new char[] { ';' });
                
                if (values[2] == iPAddress)
                {
                    return uIRow;
                }
            }

            return null;
        }
        

        /// <summary>
        /// GetHostSystemTreeItem 
        /// Description:
        ///     Get the tree item from left pane tree matching the IP address of a host system 
        /// Input:
        ///     iPAddress: the IP address of a host system
        /// Output:
        ///     The tree item of a host system 
        /// </summary>
        WinTreeItem GetHostSystemTreeItem(string iPAddress)
        {
            #region Variable Declarations
            WinTreeItem uIHostsTreeItem = this.UIEMCStorageIntegratorWindow.UIEMCStorageIntegratorWindow1.UIItemWindow.UIEMCStorageIntegratorTreeItem.UIHostsTreeItem;
            WinTreeItem uIHosToRemoveTreeItem = null;
            #endregion
            
            UITestControlCollection controls = uIHostsTreeItem.Nodes;

            foreach (UITestControl ctrl in controls)
            {
                string[] nameIP = ctrl.GetProperty("Name").ToString().Split(new string[]{" - "}, 2, StringSplitOptions.None);

                if (nameIP[1] == iPAddress)
                {
                    uIHosToRemoveTreeItem = (WinTreeItem)ctrl;
                    break;
                }
            }
            return uIHosToRemoveTreeItem;
        }

        
        /// <summary>
        /// AddHostSystem 
        /// Description:
        ///     Click "Add Host System" in main window 
        /// Input:
        ///     addHostSystem: the enumeration value of AddSystem     
        /// Output:
        ///     None
        /// </summary>
        public void AddHostSystem(AddSystem addHostSystem)
        {
            #region Variable Declarations
            WinTreeItem uIEMCStorageIntegratorTreeItem = this.UIEMCStorageIntegratorWindow.UIEMCStorageIntegratorWindow1.UIItemWindow.UIEMCStorageIntegratorTreeItem;
            WinMenuItem uIAddHostMenuItem = this.UIItemWindow.UIContextMenu.UIAddHostMenuItem;
            WinTreeItem uIHostsTreeItem = this.UIEMCStorageIntegratorWindow.UIEMCStorageIntegratorWindow1.UIItemWindow.UIEMCStorageIntegratorTreeItem.UIHostsTreeItem;
            WinMenuItem uIAddHostSystemMenuItem = this.UIItemWindow.UIContextMenu.UIAddHostSystemMenuItem;
            WinEdit uIAddHostNodeHyperLinkEdit = this.UIEMCStorageIntegratorWindow.UIEMCStorageIntegratorWindow1.UIConnectionLayoutWindow.UIAddHostClient.UIAddHostNodeHyperLinkEdit;
            WinButton uIAddHostButton = this.UIEMCStorageIntegratorWindow.UIEMCStorageIntegratorWindow1.UIActionsPane.UIAddHostButton;
            WinButton uIAddHostSystemButton = this.UIEMCStorageIntegratorWindow.UIEMCStorageIntegratorWindow1.UIActionsPane.UIAddHostSystemButton;
            #endregion

            Playback.Wait(1000);

            switch(addHostSystem)
            {
                case AddSystem.LeftRootRightClick:
                    {
                        // Click 'EMC Storage Integrator' tree item
                        Mouse.Click(uIEMCStorageIntegratorTreeItem, new Point(54, 4));

                        Playback.Wait(500);

                        // Right-Click 'EMC Storage Integrator' tree item
                        Mouse.Click(uIEMCStorageIntegratorTreeItem, MouseButtons.Right, ModifierKeys.None, new Point(54, 4));

                        Playback.Wait(500);

                        // Click 'Add Host' menu item
                        Mouse.Click(uIAddHostMenuItem, new Point(23, 4));

                        break;
                    }

                case AddSystem.LeftBranchRightClick:
                    {

                        // Click 'EMC Storage Integrator' -> 'Hosts' tree item
                        Mouse.Click(uIHostsTreeItem, new Point(18, 6));

                        Playback.Wait(500);

                        // Right-Click 'EMC Storage Integrator' -> 'Hosts' tree item
                        Mouse.Click(uIHostsTreeItem, MouseButtons.Right, ModifierKeys.None, new Point(18, 6));

                        Playback.Wait(500);

                        // Click 'Add Host System' menu item
                        Mouse.Click(uIAddHostSystemMenuItem, new Point(17, 7));

                        break;
                    }

                case AddSystem.MiddlePaneClick:
                    {
                        // Click 'EMC Storage Integrator' tree item
                        Mouse.Click(uIEMCStorageIntegratorTreeItem, new Point(85, 9));

                        Playback.Wait(500);

                        // Click 'addHostNodeHyperLinkEdit' text box
                        Mouse.Click(uIAddHostNodeHyperLinkEdit, new Point(37, 10));

                        break;
                    }

                case AddSystem.LeftRootAction:
                    {

                        // Click 'EMC Storage Integrator' tree item
                        Mouse.Click(uIEMCStorageIntegratorTreeItem, new Point(98, 6));

                        uIAddHostButton.WaitForControlEnabled();

                        Playback.Wait(1000);

                        // Click 'Add Host' button
                        Mouse.Click(uIAddHostButton, new Point(48, 8));

                        break;
                    }

                case AddSystem.LeftBranchAction:
                    {
                        
                        // Click 'EMC Storage Integrator' -> 'Hosts' tree item
                        Mouse.Click(uIHostsTreeItem, new Point(13, 2));
                                               
                        Playback.Wait(1000);

                        uIAddHostSystemButton.SearchConfigurations.Add(SearchConfiguration.AlwaysSearch);

                        uIAddHostSystemButton.WaitForControlEnabled();                    
                        
                        // Click 'Add Host System' button 
                        Mouse.Click(uIAddHostSystemButton, new Point(72, 14));

                        break;
                    }
                default:
                    {
                        Console.WriteLine("invalid parameter of {0}",  (new System.Diagnostics.StackTrace()).GetFrame(0).ToString());
                        break;
                    }
             }
        }

        /// <summary>
        /// VerifyAddedHostSystem 
        /// Description:
        ///     Verify the host system is added
        /// Input:
        ///     iPAddress: the IP address of a host system
        ///     machineType:Virtual Machine or Hypervisor
        /// Output:
        ///     None
        /// </summary>
        public void VerifyAddedHostSystem(string iPAddress, MachineType machineType)
        {
            

            #region Variable Declarations           
            WinTreeItem uIHostSystemTreeItem = GetHostSystemTreeItem(iPAddress);
            #endregion

            // Click 'EMC Storage Integrator' -> 'Hosts' -> 'uIHostSystemTreeItem' tree item
            Mouse.Click(uIHostSystemTreeItem, new Point(41, 4));

            Playback.Wait(1000);

            AssertHostAdded();

            //TODO ---Sandy
            //if (MachineType.Hypervisor == machineType)
            //{
            //    // Verify that 'Virtual machines' tab's property 'DisplayText' equals 'SAN Initiators'
            //    Assert.AreEqual(this.AssertMethod1ExpectedValues.UISANInitiatorsTabPageDisplayText, uISANInitiatorsTabPage.DisplayText);
            //}
                        
        }


        /// <summary>
        /// GetStorageSystemRow 
        /// Description:
        ///     Get the row from DataPanelClient matching the friendly name of a storage system 
        /// Input:
        ///     friendly name: the friendly name of a storage system
        ///     system type: the type of a storage
        ///     uIStorageDataPanelClient:the control which contains multiple rows
        /// Output:
        ///     The row of a storage system in uIStorageDataPanelClient
        /// </summary>
        WinRow GetStorageSystemRow( WinClient uIStorageDataPanelClient, string friendlyName, string systemType)
        {
            WinRow uIRow = null;
            UITestControlCollection controls = uIStorageDataPanelClient.GetChildren();

            Assert.IsNotNull(friendlyName);
            //Assert.IsNotNull(serialNumber);
            Assert.IsNotNull(systemType);
            

            foreach (UITestControl ctrl in controls)
            {
                uIRow = (WinRow)ctrl;
                string[] values = uIRow.Value.Split(new char[] { ';' });
                if (values[1] == friendlyName && values[5] == systemType)
                {
                    return uIRow;
                }
            }

            return null;
        }

        /// <summary>
        /// GetStorageStatus
        /// Description:
        ///     Get the status from MainWindow Middel pane  
        /// Input:
        ///     friendly name: the friendly name of a storage system
        ///     system type: the type of a storage        
        /// Output:
        ///     The string of the status value
        /// </summary>
        string GetStorageStatus(string friendlyName, string systemType)
        {
            string status = null;
            WinClient uIStorageDataPanelClient = this.UIEMCStorageIntegratorWindow.UIEMCStorageIntegratorWindow1.UIStorageSystemNodeGriWindow.UIDataPanelClient;               

            Assert.IsNotNull(friendlyName);            
            Assert.IsNotNull(systemType);

            WinRow storageRow = GetStorageSystemRow(uIStorageDataPanelClient, friendlyName, systemType);
            string[] values = storageRow.Value.Split(new char[] { ';' });
            status = values[7];

            return status;
        }

        /// <summary>
        /// GetStorageSerialNumber
        /// Description:
        ///     Get the serial number of a storage from MainWindow Middel pane  
        /// Input:
        ///     friendly name: the friendly name of a storage system
        ///     system type: the type of a storage        
        /// Output:
        ///     The string of the serial number
        /// </summary>
        string GetStorageSerialNumber(string friendlyName, string systemType)
        {
            string serialNumber = null;
            WinClient uIStorageDataPanelClient = this.UIEMCStorageIntegratorWindow.UIEMCStorageIntegratorWindow1.UIStorageSystemNodeGriWindow.UIDataPanelClient;

            Assert.IsNotNull(friendlyName);
            Assert.IsNotNull(systemType);

            WinRow storageRow = GetStorageSystemRow(uIStorageDataPanelClient, friendlyName, systemType);
            string[] values = storageRow.Value.Split(new char[] { ';' });
            serialNumber = values[3];
            
            return serialNumber;
        }

        /// <summary>
        /// GetStorageSystemTreeItem 
        /// Description:
        ///     Get the tree item from left pane tree matching the friendly name
        ///     and serial number of a storage
        /// Input:
        ///     friendlyName: friendly name of a storage system
        ///     serialNumber: serial number of a storage system
        /// Output:
        ///     The tree item of a storage system 
        /// </summary>
        WinTreeItem GetStorageSystemTreeItem(string friendlyName, string systemType)
        {
            #region Variable Declarations
            WinTreeItem uIStorageTreeItem = this.UIEMCStorageIntegratorWindow.UIEMCStorageIntegratorWindow1.UIItemWindow.UIEMCStorageIntegratorTreeItem.UIStorageSystemsTreeItem;           
            WinTreeItem uISelectedStorageTreeItem = null;
            #endregion

            string serialNumber = GetStorageSerialNumber(friendlyName, systemType);
            Assert.IsNotNull(serialNumber);
            string storageName = friendlyName + " - " + serialNumber; 
            
            UITestControlCollection controls = uIStorageTreeItem.Nodes;

            foreach (UITestControl ctrl in controls)
            {
                string storage = ctrl.GetProperty("Name").ToString();

                if (storage == storageName)
                {
                    uISelectedStorageTreeItem = (WinTreeItem)ctrl;
                    //uISelectedStorageTreeItem.DrawHighlight();
                    break;
                }
            }
            return uISelectedStorageTreeItem;
        }

        /// <summary>
        /// AssertStorageOnline
        /// Description:
        ///     Get the serial number of a storage from MainWindow Middel pane  
        /// Input:
        ///     friendly name: the friendly name of a storage system
        ///     system type: the type of a storage        
        /// Output:
        ///     The string of the serial number
        /// </summary>
        public void AssertStorageOnline(string friendlyName, string systemType)
        {
            
            string status = GetStorageStatus(friendlyName, systemType);

            Assert.IsNotNull(status);

            Assert.AreEqual(status, "Online");

        }

        /// <summary>
        /// AddStorageSystem 
        /// Description:
        ///     Click "Add Storage System" in main window 
        /// Input:
        ///     addStorageSystem: the enumeration value of AddSystem     
        /// Output:
        ///     None
        /// </summary>
        public void AddStorageSystem(AddSystem addStorageSystem)
        {
            #region Variable Declarations
            WinTreeItem uIEMCStorageIntegratorTreeItem = this.UIEMCStorageIntegratorWindow.UIEMCStorageIntegratorWindow1.UIItemWindow.UIEMCStorageIntegratorTreeItem;
            WinMenuItem uIAddStorageMenuItem = this.UIItemWindow.UIContextMenu.UIAddStorageSystemMenuItem;
            WinTreeItem uIStorageSystemsTreeItem = this.UIEMCStorageIntegratorWindow.UIEMCStorageIntegratorWindow1.UIItemWindow.UIEMCStorageIntegratorTreeItem.UIStorageSystemsTreeItem;
            WinEdit uIAddStorageSystemHperEdit = this.UIEMCStorageIntegratorWindow.UIEMCStorageIntegratorWindow1.UIConnectionLayoutWindow.UIAddStorageSystemClient.UIAddStorageSystemHperEdit;            
            WinButton uIAddStorageSystemButton = this.UIEMCStorageIntegratorWindow.UIEMCStorageIntegratorWindow1.UIActionsPane.UIAddStorageSystemButton;     
            #endregion

            Playback.Wait(1000);

            switch (addStorageSystem)
            {
                case AddSystem.LeftRootRightClick:
                    {
                        // Click 'EMC Storage Integrator' tree item
                        Mouse.Click(uIEMCStorageIntegratorTreeItem, new Point(38, 8));

                        Playback.Wait(500);

                        // Right-Click 'EMC Storage Integrator' tree item
                        Mouse.Click(uIEMCStorageIntegratorTreeItem, MouseButtons.Right, ModifierKeys.None, new Point(38, 8));

                        Playback.Wait(500);

                        // Click 'Add Storage System' menu item
                        Mouse.Click(uIAddStorageMenuItem, new Point(35, 8));

                        break;
                    }

                case AddSystem.LeftBranchRightClick:
                    {

                        // Click 'EMC Storage Integrator' -> 'Storage Systems' tree item
                        Mouse.Click(uIStorageSystemsTreeItem, new Point(18, 6));

                        Playback.Wait(500);

                        // Right-Click 'EMC Storage Integrator' -> 'Storage Systems' tree item
                        Mouse.Click(uIStorageSystemsTreeItem, MouseButtons.Right, ModifierKeys.None, new Point(18, 6));

                        Playback.Wait(500);

                        // Click 'Add Storage System' menu item
                        Mouse.Click(uIAddStorageMenuItem, new Point(17, 7));

                        break;
                    }

                case AddSystem.MiddlePaneClick:
                    {
                        // Click 'EMC Storage Integrator' tree item
                        Mouse.Click(uIEMCStorageIntegratorTreeItem, new Point(85, 9));

                        Playback.Wait(500);

                        // Click 'addStorageSystemHyperLinkEdit' text box
                        Mouse.Click(uIAddStorageSystemHperEdit, new Point(46, 6));

                        break;
                    }

                case AddSystem.LeftRootAction:
                    {

                        // Click 'EMC Storage Integrator' tree item
                        Mouse.Click(uIEMCStorageIntegratorTreeItem, new Point(98, 6));

                        uIAddStorageSystemButton.WaitForControlEnabled();

                        Playback.Wait(1000);

                        // Click 'Add Storage System' button
                        Mouse.Click(uIAddStorageSystemButton, new Point(48, 8));

                        break;
                    }

                case AddSystem.LeftBranchAction:
                    {

                        // Click 'EMC Storage Integrator' -> 'Storage Systems' tree item
                        Mouse.Click(uIStorageSystemsTreeItem, new Point(18, 6));

                        uIAddStorageSystemButton.WaitForControlEnabled();

                        Playback.Wait(1000);

                        // Click 'Add Storage System' button
                        Mouse.Click(uIAddStorageSystemButton, new Point(72, 14));

                        break;
                    }
                default:
                    {
                        Console.WriteLine("invalid parameter of {0}", (new System.Diagnostics.StackTrace()).GetFrame(0).ToString());
                        break;
                    }
            }
        }

        /// <summary>
        /// SelectStorageInLeftTree 
        /// Description:
        ///     click the tree item from left tree matching the friendly name
        ///     and serial number of a storage
        /// Input:
        ///     friendlyName: friendly name of a storage system
        ///     systemType: system type of a storage system
        /// Output:
        ///     None 
        /// </summary>
        public void SelectStorageInLeftTree(string friendlyName, string systemType)
        {
            # region Variable Declarations
            WinTreeItem uIStorageSystemTreeItem = GetStorageSystemTreeItem(friendlyName, systemType);
            WinButton uICreateLUNButton = this.UIEMCStorageIntegratorWindow.UIEMCStorageIntegratorWindow1.UIActionsPane.UICreateLUNButton;
            # endregion
            Mouse.Click(uIStorageSystemTreeItem, new Point(41, 4));

            uICreateLUNButton.WaitForControlEnabled();

        }

        /// <summary>
        /// OpenCreateLUNWizard
        /// Description:
        ///     Navigate to the storage where you want to create a lun in the left tree
        ///     Click Create LUN button to open create lun wizard 
        ///      
        ///     where you want to create lun using SelectStorageInLeftTree
        /// Input:
        ///     createLUN: enum, specify how to open create lun wizard
        ///     friendlyName: friendly name of a storage system
        ///     systemType: system type of a storage system
        /// Output:
        ///     None
        /// </summary>
        public void OpenCreateLUNWizard(CreateLUN createLUNType, string friendlyName, string systemType)
        {
            #region Variable Declarations
            WinMenuItem uICreateLUNMenuItem = this.UIItemWindow.UIContextMenu.UICreateLUNMenuItem;
            WinButton uICreateLUNButton = this.UIEMCStorageIntegratorWindow.UIEMCStorageIntegratorWindow1.UIActionsPane.UICreateLUNButton;
            WinButton uICreateLUNButton1 = this.UIEMCStorageIntegratorWindow.UIEMCStorageIntegratorWindow1.UIActionsPane.UICreateLUNButton1;
            WinWindow uIStatusProgressControWindow = this.UIEMCStorageIntegratorWindow.UIEMCStorageIntegratorWindow1.UIStatusProgressControWindow;
            // get the storage tree item where you want to create a lun 
            WinTreeItem uIStorageSystemTreeItem = GetStorageSystemTreeItem(friendlyName, systemType);
            #endregion

            // navigate to the storage tree item
            Mouse.Click(uIStorageSystemTreeItem, new Point(41, 4));

            //uIStatusProgressControWindow.WaitForControlNotExist(20000);   
            bool state = uICreateLUNButton.WaitForControlEnabled();
            while (state != true)
            {
                state = uICreateLUNButton.WaitForControlEnabled();
            }
           
            switch (createLUNType)
            {
                case CreateLUN.RightClickStorage:
                    
                    // Right-Click 'EMC Storage Integrator' -> 'Storage Systems' -> 'Storage' tree item
                    Mouse.Click(uIStorageSystemTreeItem, MouseButtons.Right, ModifierKeys.None, new Point(39, 7));
                    Playback.Wait(1000);

                    // Click 'Create LUN' menu item
                    Mouse.Click(uICreateLUNMenuItem, new Point(25, 5));
                    
                    break;

                case CreateLUN.StorageActionPane:
                    
                    Mouse.Click(uICreateLUNButton, new Point(42, 8));
                    break;

                case CreateLUN.PoolActionPane:
                    
                    Assert.IsTrue(uICreateLUNButton1.Exists);
                    Mouse.Click(uICreateLUNButton1, new Point(42, 8));
                    break;

                default:
                     Console.WriteLine("invalid parameter of {0}", (new System.Diagnostics.StackTrace()).GetFrame(0).ToString());
                     break;
                
            }
            Playback.Wait(500);
        }
    }
}
