namespace UIAutomation.UIMaps.CreateDiskWizardClasses
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
    
    
    public partial class CreateDiskWizard
    {
        public void SelectStorageSystem(Dictionary<string, string> dict)
        {
            #region Variable Declarations
            WinButton uIOpenButton = this.UICreateDiskWindow.UIItemStorageSystemClient.UIOpenButton;            
            WinButton uINextButton = this.UICreateDiskWindow.UIWizardTitleClient.UINextButton;
            WinListItem uIStorageSystemListItem = new WinListItem(this.UIItemWindow);
            #endregion

            Assert.IsTrue(dict.ContainsKey("StorageName"));
            Assert.AreNotEqual(string.Empty, dict["StorageName"]);

            // Click 'Open' button
            Mouse.Click(uIOpenButton, new Point(6, 11));

            uIStorageSystemListItem.SearchProperties.Add(WinListItem.PropertyNames.Name, dict["StorageName"]);
            uIStorageSystemListItem.WindowTitles.Add("Create Disk");        
           
            Mouse.Click(uIStorageSystemListItem);

             // Click '&Next >' button
            Mouse.Click(uINextButton, new Point(53, 13));
        }

        public void SelectStoragePool(Dictionary<string, string> dict)
        {
            #region Variable Declarations
            UIDataPanelGroup uIDataPanelGroup = this.UICreateDiskWindow.UIDataPanelGroup;
            WinButton uINextButton = this.UICreateDiskWindow.UIWizardTitleClient.UINextButton;
            WinTreeItem uITreeItem = null;
            #endregion

            Assert.IsTrue(dict.ContainsKey("PoolName"));
            Assert.AreNotEqual(string.Empty, dict["PoolName"]);

            UITestControlCollection controls = uIDataPanelGroup.GetChildren();
           foreach (UITestControl ctrl in controls)
           {               
               UITestControlCollection nodes = (UITestControlCollection)ctrl.GetProperty(WinTreeItem.PropertyNames.Nodes);

               if (dict["PoolName"] == nodes[0].FriendlyName)
               {
                   uITreeItem = (WinTreeItem)ctrl;
                   break;
               }
           }

           Assert.IsNotNull(uITreeItem, "No " + dict["PoolName"] + " pool in storage system");

            // Click 'Node2' tree item
            Mouse.Click(uITreeItem, new Point(12, 6));
            
            // Click '&Next >' button
            Mouse.Click(uINextButton, new Point(37, 14));

        }

        public void InputNewLunInfo(Dictionary<string, string> dict)
        {
            #region Variable Declarations
            WinButton uIOpenButton1 = this.UICreateDiskWindow.UILunNameComboBoxEditClient.UIOpenButton;
            WinListItem uINotApplicableListItem1 = this.UIItemWindow1.UINotApplicableList.UINotApplicableListItem;
            WinButton uIOpenButton2 = null;
            WinListItem uINotApplicableListItem2 = this.UIItemWindow9.UINotApplicableList.UINotApplicableListItem;
            WinEdit uILunNameComboBoxEditEdit = this.UICreateDiskWindow.UILunNameComboBoxEditClient.UILunNameComboBoxEditEdit;
            WinEdit uILunDescriptionComboBEdit = null;
            WinEdit uILunSizeTextEditEdit = null;
            WinButton uIOpenButton3 = null;
            WinListItem uIMBListItem = this.UIItemWindow2.UIMBList.UIMBListItem;
            WinListItem uIGBListItem = this.UIItemWindow2.UIGBList.UIGBListItem;
            WinButton uIOpenButton4 = this.UICreateDiskWindow.UIAutoClient.UIOpenButton;
            WinListItem uIAutoListItem = this.UIItemWindow10.UIAutoList.UIAutoListItem;
            WinListItem uISPAListItem = this.UIItemWindow10.UISPAList.UISPAListItem;
            WinListItem uISPBListItem = this.UIItemWindow10.UISPBList.UISPBListItem;
            WinListItem uIThickListItem = this.UICreateDiskWindow.UIProvisioningTypeRadiList.UIThickListItem;
            WinListItem uIThinListItem = this.UICreateDiskWindow.UIProvisioningTypeRadiList1.UIThinListItem;
            WinButton uINextButton = this.UICreateDiskWindow.UIWizardTitleClient.UINextButton;
            #endregion

            uIOpenButton2 = new WinButton(uIOpenButton1);
            uIOpenButton2.SearchConfigurations.Add(SearchConfiguration.NextSibling);

            uIOpenButton3 = new WinButton(uIOpenButton2);
            uIOpenButton3.SearchConfigurations.Add(SearchConfiguration.NextSibling);

            uILunDescriptionComboBEdit = new WinEdit(uILunNameComboBoxEditEdit);
            uILunDescriptionComboBEdit.SearchConfigurations.Add(SearchConfiguration.NextSibling);

            uILunSizeTextEditEdit = new WinEdit(uILunDescriptionComboBEdit);
            uILunSizeTextEditEdit.SearchConfigurations.Add(SearchConfiguration.NextSibling);


            if (!dict.ContainsKey("Name") || string.Empty == dict["Name"])
            {

                // Click 'Open' button
                Mouse.Click(uIOpenButton1, new Point(8, 8));                

                // Select uINotApplicableListItem1
                Mouse.Click(uINotApplicableListItem1, uINotApplicableListItem1.GetClickablePoint());
            }
            else
            {
                // Click 'lunNameComboBoxEdit' text box
                Mouse.Click(uILunNameComboBoxEditEdit, new Point(63, 9));

                Keyboard.SendKeys(uILunNameComboBoxEditEdit, dict["Name"], ModifierKeys.None);
            }

            if (!dict.ContainsKey("Description") || string.Empty == dict["Description"])
            {
                
                // Click 'Open' button
                Mouse.Click(uIOpenButton2, new Point(8, 11));

                // Select uINotApplicableListItem2
                Mouse.Click(uINotApplicableListItem2, new Point(30, 8));
            }
            else
            {
               
                // Click 'lunDescriptionComboBoxEdit' text box
                Mouse.Click(uILunDescriptionComboBEdit, new Point(60, 11));

                // Type 'Created By Ssandy' in '<Not Applicable>' text box
                Keyboard.SendKeys(uILunDescriptionComboBEdit, dict["Description"], ModifierKeys.None);
            }

            if (dict.ContainsKey("Size") && dict["Size"] != string.Empty)
            {
                
                // Click 'lunSizeTextEdit' text box
                Mouse.Click(uILunSizeTextEditEdit, new Point(72, 8));

                uILunSizeTextEditEdit.Text = dict["Size"];                
            }

            if (dict.ContainsKey("Unit") && "GB" == dict["Unit"])
            {
                
                // Click 'Open' button
                Mouse.Click(uIOpenButton3, new Point(6, 13));

                Mouse.Click(uIGBListItem, uIGBListItem.GetClickablePoint());
            }

            if (dict.ContainsKey("ServiceNode") && "SPA" == dict["ServiceNode"])
            {

                // Click 'Open' button
                Mouse.Click(uIOpenButton4, new Point(11, 9));

                // Select '' in 'SPA' list box
                Mouse.Click(uISPAListItem, uISPAListItem.GetClickablePoint());
            }
            else if (dict.ContainsKey("ServiceNode") && "SPB" == dict["ServiceNode"])
            {
                // Click 'Open' button
                Mouse.Click(uIOpenButton4, new Point(11, 9));

                // Select '' in 'SPA' list box
                Mouse.Click(uISPBListItem, uISPBListItem.GetClickablePoint());
            }

            if (dict.ContainsKey("ProvisionType") && "Thick" == dict["ProvisionType"])
            {
                // Click 'Thick' list item
                Mouse.Click(uIThickListItem, new Point(14, 9));
            }
            else if (dict.ContainsKey("ProvisionType") && "Thin" == dict["ProvisionType"])
            {
                // Click 'Thin' list item
                Mouse.Click(uIThinListItem, new Point(6, 10));
            }

            // Click '&Next >' button
            Mouse.Click(uINextButton, new Point(42, 14));
        }

        public void SetDiskPreparation(Dictionary<string, string> dict)
        {
            #region Variable Declarations
            WinButton uIOpenButton1 = this.UICreateDiskWindow.UIMBRClient.UIOpenButton;
            WinListItem uIGPTListItem = this.UIItemWindow3.UIGPTList.UIGPTListItem;
            WinButton uIOpenButton2 = null;
            WinListItem uIFAT32ListItem = this.UIItemWindow4.UIFAT32List.UIFAT32ListItem;
            WinButton uIOpenButton3 = null;
            WinControl uIPositionIndicator = this.UIItemWindow13.UIScrollbarScrollBar.UIPositionIndicator;
            WinEdit uIVolumeLabelTextEditEdit = this.UICreateDiskWindow.UIPanel1Window.UIVolumeLabelTextEditEdit;
            WinEdit uIItemEdit1 = this.UICreateDiskWindow.UIPanel1Window.UIItemEdit;
            WinButton uIOpenButton4 = this.UICreateDiskWindow.UIAvailableDriverLetteClient.UIOpenButton;
            WinRadioButton uIMountPathRadioButton = this.UICreateDiskWindow.UIPanel1Window.UIMountPathRadioButton;
            WinButton uIItemButton = this.UICreateDiskWindow.UIPanel1Window.UIItemButton;
            WinTreeItem uINode0TreeItem = this.UIBrowseFolderWindow.UIDataPanelGroup.UINode0TreeItem;
            WinCell uICell0 = this.UIBrowseFolderWindow.UINode0TreeItem.UIItem10522254Cell;
            WinButton uICreateFolderButton = this.UIBrowseFolderWindow.UIBrowseFolderClient.UICreateFolderButton;
            WinEdit uIItemEdit2 = this.UIPleaseinputthenamefoWindow.UIPleaseinputthenamefoClient.UIItemEdit;
            WinButton uIOKButton = this.UIPleaseinputthenamefoWindow.UIPleaseinputthenamefoClient.UIOKButton;
            WinButton uIOKButton1 = this.UIBrowseFolderWindow.UIBrowseFolderClient.UIOKButton;
            WinButton uINextButton = this.UICreateDiskWindow.UIWizardTitleClient.UINextButton;
            #endregion
           
            uIOpenButton2 = new WinButton(uIOpenButton1);
            uIOpenButton2.SearchProperties.Add(WinButton.PropertyNames.Name, "Open");
            uIOpenButton2.SearchConfigurations.Add(SearchConfiguration.NextSibling);
            
            uIOpenButton3 = new WinButton(uIOpenButton2);
            uIOpenButton3.SearchProperties.Add(WinButton.PropertyNames.Name, "Open");
            uIOpenButton3.SearchConfigurations.Add(SearchConfiguration.NextSibling);
          
            uIOpenButton4 = new WinButton(uIOpenButton3);
            uIOpenButton4.SearchProperties.Add(WinButton.PropertyNames.Name, "Open");
            uIOpenButton4.SearchConfigurations.Add(SearchConfiguration.NextSibling);
           
            if (dict.ContainsKey("PartitionType") && "GPT" == dict["PartitionType"])
            {
                // Click 'Open' button
                Mouse.Click(uIOpenButton1, new Point(8, 14));

                // Select '' in 'GPT' list box
                Mouse.Click(uIGPTListItem, uIGPTListItem.GetClickablePoint());
            }
            if (dict.ContainsKey("VolumeSize") && string.Empty != dict["VolumeSize"])
            {
                //TODO:
            }

            if (dict.ContainsKey("FileSystem") && "FAT32" == dict["FileSystem"])
            {
                // Click 'Open' button
                Mouse.Click(uIOpenButton2, new Point(9, 16));

                // Select '' in 'FAT32' list box
                Mouse.Click(uIFAT32ListItem, uIFAT32ListItem.GetClickablePoint());
            }

            if (dict.ContainsKey("AllocationUnitSize") && string.Empty != dict["AllocationUnitSize"])
            {
                // Click 'Open' button
                Mouse.Click(uIOpenButton3, new Point(9, 9));

                WinListItem uIAllocationUnitSizeListItem = new WinListItem(this.UIItemWindow12);

                uIAllocationUnitSizeListItem.SearchProperties.Add(UITestControl.PropertyNames.Name, dict["AllocationUnitSize"]);

                uIAllocationUnitSizeListItem.EnsureClickable(new Point(3, 53));
                   
                Mouse.Click(uIAllocationUnitSizeListItem, uIAllocationUnitSizeListItem.GetClickablePoint());
            }

            if (dict.ContainsKey("VolumeLable") && string.Empty != dict["VolumeLable"])
            {
                // Click 'volumeLabelTextEdit' text box
                Mouse.Click(uIVolumeLabelTextEditEdit, new Point(129, 6));

                // Type 'Sandy_Test' in 'Unknown Name' text box
                Keyboard.SendKeys(uIItemEdit1, dict["VolumeLable"], ModifierKeys.None);
            }

            if (dict.ContainsKey("DriveLetter") && string.Empty != dict["DriveLetter"])
            {
                // Click 'Open' button
                Mouse.Click(uIOpenButton4, new Point(10, 17));

                // Click 'Open' button
                Mouse.Click(uIOpenButton4, new Point(1, 16));

                WinListItem uIDriveLetter = new WinListItem();
                uIDriveLetter.SearchProperties.Add(WinListItem.PropertyNames.Name, dict["DriveLetter"]);
                uIDriveLetter.WindowTitles.Add("Create Disk");

                uIDriveLetter.EnsureClickable();

                Mouse.Click(uIDriveLetter, uIDriveLetter.GetClickablePoint());
            }
            else if (dict.ContainsKey("MountPath") && string.Empty != dict["MountPath"])
            {
                // Select 'Mount Path:' radio button                
                Mouse.Click(uIMountPathRadioButton, new Point(5, 5));

                // Click '...' button
                Mouse.Click(uIItemButton, new Point(25, 24));

                // Click 'Node0' tree item
                Mouse.Click(uINode0TreeItem, new Point(9, 8));

                string[] place = dict["MountPath"].Split(new char[] { ':', '\\' }, StringSplitOptions.RemoveEmptyEntries);               

                for (int i = 0; i < place.Length; i++)
                {                    
                    WinCell uICell = new WinCell(this.UIBrowseFolderWindow);

                    uICell.SearchProperties.Add(WinCell.PropertyNames.Value, place[i]);

                    uICell.EnsureClickable();
                                        
                    Assert.IsFalse(i == 0 && !uICell.Exists, "disk " + place[0] + " donesn't exist");

                    if (!uICell.Exists)
                    {
                        Mouse.Click(uICreateFolderButton, uICreateFolderButton.GetClickablePoint());
                        Keyboard.SendKeys(uIItemEdit2, place[i], ModifierKeys.None);
                        Mouse.Click(uIOKButton, uIOKButton.GetClickablePoint());
                    }

                    WinTreeItem uINodeTreeItem = (WinTreeItem)uICell.GetParent();
                    Console.WriteLine(place.Length);
                                        
                    if (i < place.Length - 1)
                    {                        
                        ExpandTreeItem(uINodeTreeItem);
                    }

                    Mouse.Click(uICell, uICell.GetClickablePoint()); 
                }
                // Click 'OK' button
                Mouse.Click(uIOKButton1, new Point(40, 19));
            }
            // Click '&Next >' button
            Mouse.Click(uINextButton, new Point(25, 13));
        }

        void ExpandTreeItem(WinTreeItem uINodeTreeItem)
        {
          //  Mouse.Click(uINodeTreeItem, new Point(25, 12));
            //Keyboard.SendKeys(uINodeTreeItem, "*", ModifierKeys.None);
          //  uINodeTreeItem.SetProperty(WinTreeItem.PropertyNames.Expanded, "True");
            uINodeTreeItem.Expanded = true;
        }

        //public void ReviewInputParameters()
        //{
        //    WinButton uINextButton = this.UICreateDiskWindow.UIWizardTitleClient.UINextButton;

        //    // Click '&Next >' button
        //    Mouse.Click(uINextButton, new Point(25, 13));
        //}
        
        //public void Progress()
        //{
        //    WinButton uINextButton = this.UICreateDiskWindow.UIWizardTitleClient.UINextButton;

        //    // Click '&Next >' button
        //    Mouse.Click(uINextButton, new Point(23, 13));
        //}

        //public void ClickFinishButton()
        //{
        //    WinButton uIFinishButton = this.UICreateDiskWindow.UIWizardTitleClient.UIFinishButton;
        //    // Click '&Finish' button
        //    Mouse.Click(uIFinishButton, new Point(23, 13));
        //}

    }
}
