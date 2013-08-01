namespace UIAutomation.UIMaps.AddHostWizardClasses
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
    using UIAutomation.UIMaps.MainWindowClasses;


    public enum OKButton
    {
        Validation,
        TestConnection,
        Unabletoconnect
    }

    public partial class AddHostWizard
    {
       
        public string InputHostInfoAndTest( string[] infor, bool hostNameBlank = false, bool iPAddressBlank = false )
        {           

            #region Variable Declarations            
            WinEdit uIHostNameTextEditEdit = null;
            WinEdit uIItemEdit = this.UIAddHostWindow.UIPanelControl1Pane.UIItemEdit;
            WinEdit uIIpAddressComboBoxEdiEdit = this.UIAddHostWindow.UIPanelControl1Pane.UIIpAddressComboBoxEdiEdit;          
            WinRadioButton uICurrentCredentialsRadioButton = this.UIAddHostWindow.UIPanelControl1Pane.UICurrentCredentialsRadioButton;
            WinRadioButton uISpecifyCredentialsRadioButton = this.UIAddHostWindow.UIPanelControl1Pane.UISpecifyCredentialsRadioButton;
            WinEdit uIUserNameTextEditEdit = this.UIAddHostWindow.UIGroupBox1Window.UIUserNameTextEditEdit;
            WinEdit uIItemEdit1 = this.UIAddHostWindow.UIGroupBox1Window.UIItemEdit;
            WinEdit uIPasswordTextEditEdit = this.UIAddHostWindow.UIGroupBox1Window.UIPasswordTextEditEdit;            
            #endregion                       

            uIHostNameTextEditEdit = new WinEdit(uIIpAddressComboBoxEdiEdit);
            uIHostNameTextEditEdit.SearchConfigurations.Add(SearchConfiguration.NextSibling);         

            if (infor[0] != null)
            {
                // Click 'hostNameTextEdit' text box
                Mouse.Click(uIHostNameTextEditEdit, new Point(61, 7));

                // Type '1' in 'Unknown Name' text box
                Keyboard.SendKeys(uIHostNameTextEditEdit, infor[0], ModifierKeys.None);
            }

            if (infor[1] != null)
            {
                // Click 'ipAddressComboBoxEdit' text box
                Mouse.Click(uIIpAddressComboBoxEdiEdit, new Point(64, 10));

                // Type '1' in '0.0.0.1' text box
                Keyboard.SendKeys(uIIpAddressComboBoxEdiEdit, infor[1], ModifierKeys.None);
            }

            if (infor[2] == "Specify")
            {
                // Select 'Specify Credentials' radio button
                uISpecifyCredentialsRadioButton.Selected = this.addhostsystemParams.UISpecifyCredentialsRadioButtonSelected;

                if (infor[3] != null)
                {

                    // Click 'userNameTextEdit' text box
                    Mouse.Click(uIUserNameTextEditEdit, new Point(77, 1));

                    // Type '1' in 'Unknown Name' text box
                    Keyboard.SendKeys(uIItemEdit1, infor[3], ModifierKeys.None);
                }

                if (infor[4] != null)
                {
                    // Click 'passwordTextEdit' text box
                    Mouse.Click(uIPasswordTextEditEdit, new Point(63, 15));

                    // Type '********' in 'passwordTextEdit' text box
                    Keyboard.SendKeys(uIPasswordTextEditEdit, infor[4], true);
                }
            }

            if (hostNameBlank)
            {
                // Click 'hostNameTextEdit' text box
                Mouse.Click(uIHostNameTextEditEdit, new Point(245, 6));

                // Move 'hostNameTextEdit' text box from (243, 8) to 'Host Name:' label (32, 3)
                uIHostNameTextEditEdit.EnsureClickable(new Point(32, 3));
                Mouse.StartDragging(uIHostNameTextEditEdit, new Point(243, 8));
                Mouse.StopDragging(uIHostNameTextEditEdit, new Point(0, 3));

                // Type '{Back}' in uIHostNameTextEditEdit text box
                Keyboard.SendKeys(uIHostNameTextEditEdit, "{Back}", ModifierKeys.None);
            }

            if (iPAddressBlank)
            {
                // Click 'ipAddressComboBoxEdit' text box
                Mouse.Click(uIIpAddressComboBoxEdiEdit, new Point(128, 6));

                // Move 'ipAddressComboBoxEdit' text box from (126, 9) to 'IP Address:' label (42, 7)
                uIIpAddressComboBoxEdiEdit.EnsureClickable(new Point(42, 7));
                Mouse.StartDragging(uIIpAddressComboBoxEdiEdit, new Point(126, 9));
                Mouse.StopDragging(uIIpAddressComboBoxEdiEdit, new Point(0, 7));

                // Type '{Back}' in uIIpAddressComboBoxEdiEdit text box
                Keyboard.SendKeys(uIIpAddressComboBoxEdiEdit, "{Back}", ModifierKeys.None);
            }

            if (null == uIIpAddressComboBoxEdiEdit.Text)
            {
                return infor[1];
            }
            else
            {
                return uIIpAddressComboBoxEdiEdit.Text;
            }
        }


        //void checkTestConnection()
        //{
        //    #region Variable Declarations
            
        //    WinTitleBar uIUnabletoconnectthesyTitleBar = this.UIUnabletoconnectthesyWindow.UIUnabletoconnectthesyTitleBar;
        //    WinButton uICloseButton = this.UIUnabletoconnectthesyWindow.UIUnabletoconnectthesyTitleBar.UICloseButton;
        //    WinText uITheRPCserverisunavaiText = this.UIUnabletoconnectthesyWindow.UIUnabletoconnectthesyDialog.UITheRPCserverisunavaiText;
        //    WinButton uIOKButton = this.UIUnabletoconnectthesyWindow.UIUnabletoconnectthesyDialog.UIOKButton;
           
           
        //    WinTitleBar uIValidationerrorTitleBar = this.UIValidationerrorWindow.UIValidationerrorTitleBar;
        //    WinText uIPleaseinputhostsysteText1 = this.UIValidationerrorWindow.UIValidationerrorDialog.UIPleaseinputhostsysteText1;
        //    WinButton uIOKButton1 = this.UIValidationerrorWindow.UIValidationerrorDialog.UIOKButton;
        //    WinButton uICancelButton = this.UIAddHostWindow.UIAddHostClient.UICancelButton;
        //    #endregion
           
            
        //    // Click 'Unable to connect the system' title bar
        //    Mouse.Click(uIUnabletoconnectthesyTitleBar, new Point(128, 6));

        //    // Click 'Close' button
        //    Mouse.Click(uICloseButton, new Point(13, 9));

           

        //    // Click '&OK' button
        //    Mouse.Click(uIOKButton, new Point(46, 7));

            

           
           

        //    // Click 'Validation error' title bar
        //    Mouse.Click(uIValidationerrorTitleBar, new Point(78, 9));

        //    // Click 'Please input host system IP Address.' label
        //    Mouse.Click(uIPleaseinputhostsysteText1, new Point(66, 10));

        //    // Click '&OK' button
        //    Mouse.Click(uIOKButton1, new Point(60, 3));

        //    // Click 'Cancel' button
        //    Mouse.Click(uICancelButton, new Point(33, 12));
                      

           
        //}

        public void ClickTestConnectionButton()
        {
            WinButton uITestConnectionButton = this.UIAddHostWindow.UITestConnectionWindow.UITestConnectionButton;

            Playback.Wait(1000);

            // Click 'Test Connection' button
            Mouse.Click(uITestConnectionButton, new Point(92, 18));
        }

        public void ClickAddButton()
        {
            WinButton uIAddButton = this.UIAddHostWindow.UIAddHostClient.UIAddButton;

            Playback.Wait(1000);

            Mouse.Click(uIAddButton, uIAddButton.GetClickablePoint());
        }

        public void ClickOKButton(OKButton oKButton)
        {
            WinButton uIOKButton = null;
            switch (oKButton)
            {
                case OKButton.Validation:
                    uIOKButton = this.UIValidationerrorWindow.UIValidationerrorDialog.UIOKButton;
                        break;
                case OKButton.TestConnection:
                    uIOKButton = this.UITestConnectionWindow.UITestConnectionDialog.UIOKButton;
                        break;
                case OKButton.Unabletoconnect:
                        uIOKButton = this.UIUnabletoconnectthesyWindow.UIUnabletoconnectthesyDialog.UIOKButton;
                        break;
                default:
                        break;

            }
            Playback.Wait(1000);
            Mouse.Click(uIOKButton, uIOKButton.GetClickablePoint());
            Playback.Wait(1000);
        }
            
        //WinTreeItem FindExistedHostSystem(string host)
        //{
        //    #region Variable Declarations
        //    WinTreeItem uIHostsTreeItem = this.UIEmc_Console1EMCStoraWindow1.UIEMCStorageIntegratorWindow.UIItemWindow.UIEMCStorageIntegratorTreeItem.UIHostsTreeItem;         
        //    #endregion

        //    UITestControlCollection controls = uIHostsTreeItem.Nodes;

        //    foreach (UITestControl ctrl in controls)
        //    {
        //        if (ctrl.GetProperty(WinTreeItem.PropertyNames.Name).ToString().Contains(host))
        //        {                    
        //            return (WinTreeItem)ctrl;
        //        }
        //    }

        //    return null;
        //}

        //void RemoveHostSystemIfExist(string host, int LeftOrRight)// 0 for left, 1 for right
        //{
        //    #region Variable Declarations
        //    WinTreeItem uIHostsTreeItem = this.UIEmc_Console1EMCStoraWindow1.UIEMCStorageIntegratorWindow.UIItemWindow.UIEMCStorageIntegratorTreeItem.UIHostsTreeItem;            
        //    WinMenuItem uIRemoveSystemMenuItem = this.UIItemWindow.UIContextMenu.UIRemoveSystemMenuItem;
        //    WinButton uIYesButton = this.UIRemoveSystemWindow.UIRemoveSystemDialog.UIYesButton;
        //    WinTreeItem uIRemoveTreeItem = null;
        //    WinButton uIRemoveSystemButton = this.UIEmc_Console1EMCStoraWindow1.UIEMCStorageIntegratorWindow.UIActionsPane.UIRemoveSystemButton;
        //    #endregion

        //    uIRemoveTreeItem = FindExistedHostSystem(host);

        //    if (null == uIRemoveTreeItem)
        //    {
        //        return;
        //    }

        //    // Click 'EMC Storage Integrator' -> 'Hosts' -> 'LAMANNA-SRV-VM3.sr5dom.eng.emc.com - 10.5.222.54' tree item
        //    Mouse.Click(uIRemoveTreeItem, uIRemoveTreeItem.GetClickablePoint());

        //    if (0 == LeftOrRight)
        //    {
        //        // Right-Click 'EMC Storage Integrator' -> 'Hosts' -> 'LAMANNA-SRV-VM3.sr5dom.eng.emc.com - 10.5.222.54' tree item
        //        Mouse.Click(uIRemoveTreeItem, MouseButtons.Right, ModifierKeys.None, new Point(98, 6));

        //        // Click 'Remove System' menu item
        //        Mouse.Click(uIRemoveSystemMenuItem, new Point(36, 6));
        //    }
        //    else
        //    {
        //        // Click 'Remove System' button
        //        Mouse.Click(uIRemoveSystemButton, new Point(84, 10));
        //    }

        //    // Click '&Yes' button
        //    Mouse.Click(uIYesButton, new Point(33, 13));
        //}

         


    }
}
