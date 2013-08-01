namespace UIAutomation.UIMaps.AddStorageWizardClasses
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Input;
    using System.CodeDom.Compiler;
    using System.Text.RegularExpressions;
    using Microsoft.VisualStudio.TestTools.UITest.Extension;
    using Microsoft.VisualStudio.TestTools.UITesting;
    using Microsoft.VisualStudio.TestTools.UITesting.WinControls;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Keyboard = Microsoft.VisualStudio.TestTools.UITesting.Keyboard;
    using Mouse = Microsoft.VisualStudio.TestTools.UITesting.Mouse;
    using MouseButtons = System.Windows.Forms.MouseButtons;    

    public partial class AddStorageWizard
    {      
        
        
        /// <summary>
        /// SelectStorageType 
        /// Description:
        ///     Select a storage type fromo the combo box list in AddStorageSystem dialog
        /// Input:
        ///     storageType: the type of storage system, should be one of "CLARiiON-CX4",
        ///     "VMAX", "VMAXe","VNX", "VNX-Block", "VNX-CIFS", "VNXe"
        /// Output:
        ///     None
        /// </summary>
        public void SelectStorageType(string storageType)
        {
            #region Variable Declarations
            WinButton uIOpenButton = this.UIAddStorageSystemWindow.UICLARiiONCX4ComboBox.UIOpenButton;
            WinListItem uICLARiiONCX4ListItem = this.UIItemWindow.UICLARiiONCX4List.UICLARiiONCX4ListItem;
            WinListItem uIVMAXListItem = this.UIItemWindow.UIVMAXList.UIVMAXListItem;
            WinListItem uIVMAXeListItem = this.UIItemWindow.UIVMAXeList.UIVMAXeListItem;
            WinListItem uIVNXListItem = this.UIItemWindow.UIVNXList.UIVNXListItem;
            WinListItem uIVNXBlockListItem = this.UIItemWindow.UIVNXBlockList.UIVNXBlockListItem;
            WinListItem uIVNXCIFSListItem = this.UIItemWindow.UIVNXCIFSList.UIVNXCIFSListItem;
            WinListItem uIVNXeListItem = this.UIItemWindow.UIVNXeList.UIVNXeListItem;
            #endregion

            // Click 'Open' button
            //Mouse.Click(uIOpenButton, uIOpenButton.GetClickablePoint());
            Mouse.Click(uIOpenButton, new Point(6, 6));            

            switch (storageType)
            {
                // Click 'CLARiiON-CX4' list item
                case "CLARiiON-CX4":
                    Mouse.Click(uICLARiiONCX4ListItem, new Point(61, 3));
                    break;
                // Click 'VMAX' list item 
                case "VMAX":
                    Mouse.Click(uIVMAXListItem, new Point(20, 9));
                    break;
                // Click 'VMAXe' list item 
                case "VMAXe":
                    Mouse.Click(uIVMAXeListItem, new Point(39, 9));
                    break;
                // Click 'VNX' list item 
                case "VNX":
                    Mouse.Click(uIVNXListItem, new Point(17, 7));
                    break;
                // Click 'VNX-Block' list item 
                case "VNX-Block":
                    Mouse.Click(uIVNXBlockListItem, new Point(39, 8));
                    break;
                // Click'VNX-CIFS' list item 
                case "VNX-CIFS":
                    Mouse.Click(uIVNXCIFSListItem, new Point(41, 7));
                    break;
                // Click 'VNXe' list item 
                case "VNXe":
                    Mouse.Click(uIVNXeListItem, new Point(29, 6));
                    break;
                default:                    
                    throw new ArgumentException("Incorrect Storage Type");                  
            }
            Playback.Wait(500);

        }

        /// <summary>
        /// InputFriendlyName 
        /// Description:
        ///     Input Friendly Name for a storage system
        /// Input:
        ///     name: the friendly name for a storage system
        ///                  
        /// Output:
        ///     None
        /// </summary>
        public void InputFriendlyName(string name)
        {
            #region Variable Declarations
            WinEdit uISystemNameTextEditEdit = this.UIAddStorageSystemWindow.UIPanelControl1Pane.UISystemNameTextEditEdit;
            WinEdit uIItemEdit = this.UIAddStorageSystemWindow.UIPanelControl1Pane.UIItemEdit;
            #endregion

            if (name != null)
            {
                // Click 'systemNameTextEdit' text box
                Mouse.Click(uISystemNameTextEditEdit, new Point(59, 6));

                // Type name in 'Friendly Name' text box
                Keyboard.SendKeys(uIItemEdit, name, ModifierKeys.None);
            }
        }

        
        /// <summary>
        /// InputBlockStorageInfo 
        /// Description:
        ///     Input storage information for CLARiiON_CX4, VNX-Block, VNXe
        /// Input:
        ///     storageInfo: an array which contrains "username", "password"
        ///                  "SPA's IP address", "SPB's IP addres"
        /// Output:
        ///     None
        /// </summary>
        public void InputBlockStorageInfo(string[] storageInfo)
        {
            #region Variable Declarations
            string username = storageInfo[0];
            string password = storageInfo[1];
            string spaIP = storageInfo[2];
            string spbIP = storageInfo[3];

            WinClient uIHeaderPanelClient = this.UIAddStorageSystemWindow.UICreationParametersVGTable.UIHeaderPanelClient;
            WinEdit uIEditingcontrolEdit = this.UIAddStorageSystemWindow.UICreationParametersVGTable.UIEditingcontrolEdit;
            WinEdit uICreationParametersVGEdit = this.UIAddStorageSystemWindow.UICreationParametersVGTable.UICreationParametersVGEdit;
            #endregion

            if (username != null)
            {
                // Click 'Header Panel' client
                Mouse.Click(uIHeaderPanelClient, new Point(276, 43));

                // Type username in 'Editing control' text box               
                uIEditingcontrolEdit.Text = username;                
            }

            if (password != null)
            {
                // Click 'Header Panel' client
                Mouse.Click(uIHeaderPanelClient, new Point(288, 68));                

                // Type password in 'creationParametersVGridControl' text box
                Keyboard.SendKeys(uICreationParametersVGEdit, Playback.EncryptText(password), true);
            }

            if (spaIP != null)
            {
                // Click 'Header Panel' client
                Mouse.Click(uIHeaderPanelClient, new Point(270, 89));

                // Type spaIP in 'Editing control' text box
                uIEditingcontrolEdit.Text = spaIP;
            }

            if (spbIP != null)
            {
                // Click 'Header Panel' client
                Mouse.Click(uIHeaderPanelClient, new Point(280, 110));

                // Type spbIP in 'Editing control' text box
                uIEditingcontrolEdit.Text = spbIP;
            }

        }

        /// <summary>
        /// InputCIFSStorageInfo 
        /// Description:
        ///     Input storage information for VNX-CIFS
        /// Input:
        ///     storageInfo: an array which contrains "username", "password"
        ///                  "IP Address", "Port Number", "Bypass server certificate validation"=true|false
        /// Output:
        ///     None
        /// </summary>
        public void InputCIFSStorageInfo(string[] storageInfo)
        {
            #region Variable Declarations
            string username = storageInfo[0];
            string password = storageInfo[1];
            string ip = storageInfo[2];
            string portNum = storageInfo[3];
            string validationFlag = storageInfo[4];

            WinClient uIHeaderPanelClient = this.UIAddStorageSystemWindow.UICreationParametersVGTable.UIHeaderPanelClient;
            WinEdit uIEditingcontrolEdit = this.UIAddStorageSystemWindow.UICreationParametersVGTable.UIEditingcontrolEdit;
            WinEdit uICreationParametersVGEdit = this.UIAddStorageSystemWindow.UICreationParametersVGTable.UICreationParametersVGEdit;
            WinCheckBox uIEditingcontrolCheckBox = this.UIAddStorageSystemWindow.UICreationParametersVGTable.UIEditingcontrolCheckBox;
            #endregion

            if (username != null)
            {
                // Click 'Header Panel' client
                Mouse.Click(uIHeaderPanelClient, new Point(276, 43));

                // Type username in 'Editing control' text box               
                uIEditingcontrolEdit.Text = username;
            }

            if (password != null)
            {
                // Click 'Header Panel' client
                Mouse.Click(uIHeaderPanelClient, new Point(288, 68));

                // Type password in 'creationParametersVGridControl' text box
                Keyboard.SendKeys(uICreationParametersVGEdit, Playback.EncryptText(password), true);
            }

            if (ip != null)
            {
                // Click 'Header Panel' client
                Mouse.Click(uIHeaderPanelClient, new Point(268, 87));
                
                // Type ip in 'Editing control' text box
                uIEditingcontrolEdit.Text = ip;
            }

            if (portNum != null)
            {
                // Click 'Header Panel' client
                Mouse.Click(uIHeaderPanelClient, new Point(280, 112));
                
                // Type port number in 'Editing control' text box
                uIEditingcontrolEdit.Text = portNum;
            }

            if (validationFlag == "true")
            {
                // Click 'Header Panel' client
                Mouse.Click(uIHeaderPanelClient, new Point(387, 136));
                // Select Check Box
                uIEditingcontrolCheckBox.Checked = true;

            }
            else
            {
                // Click 'Header Panel' client
                Mouse.Click(uIHeaderPanelClient, new Point(387, 136));
                // Unselect Check Box
                uIEditingcontrolCheckBox.Checked = false;
            }
        }

        /// <summary>
        /// InputVNXStorageInfo 
        /// Description:
        ///     Input storage information for VNX
        /// Input:
        ///     storageInfo: an array which contrains "Block-Username", "Block-Password"
        ///                  "SPA's IP Address", "SPB's IP Address", "CIFS-Username", 
        ///                  "CIFS-password", "CIFS IP Address", "CIFS Port Number", 
        ///                  "CIFS Bypass server certificate validation" = true | false
        /// Output:
        ///     None
        /// </summary>
        public void InputVNXStorageInfo(string[] storageInfo)
        {
            #region Variable Declarations
            string blockUsername = storageInfo[0];
            string blockPassword = storageInfo[1];
            string spaIP = storageInfo[2];
            string spbIP = storageInfo[3];
            string cifsUsername = storageInfo[4];
            string cifsPassword = storageInfo[5];
            string ip = storageInfo[6];
            string portNum = storageInfo[7];
            string validationFlag = storageInfo[8];

            WinClient uIHeaderPanelClient = this.UIAddStorageSystemWindow.UICreationParametersVGTable.UIHeaderPanelClient;
            WinEdit uIEditingcontrolEdit = this.UIAddStorageSystemWindow.UICreationParametersVGTable.UIEditingcontrolEdit;
            WinEdit uICreationParametersVGEdit = this.UIAddStorageSystemWindow.UICreationParametersVGTable.UICreationParametersVGEdit;
            WinCheckBox uIEditingcontrolCheckBox = this.UIAddStorageSystemWindow.UICreationParametersVGTable.UIEditingcontrolCheckBox;
            #endregion

            if (blockUsername != null)
            {
                // Click 'Header Panel' client
                Mouse.Click(uIHeaderPanelClient, new Point(276, 43));

                // Type username in 'Editing control' text box               
                uIEditingcontrolEdit.Text = blockUsername;
            }

            if (blockPassword != null)
            {
                // Click 'Header Panel' client
                Mouse.Click(uIHeaderPanelClient, new Point(288, 68));

                // Type password in 'creationParametersVGridControl' text box
                Keyboard.SendKeys(uICreationParametersVGEdit, Playback.EncryptText(blockPassword), true);
            }

            if (spaIP != null)
            {
                // Click 'Header Panel' client
                Mouse.Click(uIHeaderPanelClient, new Point(270, 89));

                // Type spaIP in 'Editing control' text box
                uIEditingcontrolEdit.Text = spaIP;
            }

            if (spbIP != null)
            {
                // Click 'Header Panel' client
                Mouse.Click(uIHeaderPanelClient, new Point(280, 110));

                // Type spbIP in 'Editing control' text box
                uIEditingcontrolEdit.Text = spbIP;
            }

            if (cifsUsername != null)
            {
                // Click 'Header Panel' client
                Mouse.Click(uIHeaderPanelClient, new Point(266, 134));

                // Type CIFS username in 'Editing control' text box
                uIEditingcontrolEdit.Text = cifsUsername;
            }

            if (cifsPassword != null)
            {
                // Click 'Header Panel' client
                Mouse.Click(uIHeaderPanelClient, new Point(268, 155));

                // Type CIFS password in 'PasswordTextEdit' text box
                Keyboard.SendKeys(uICreationParametersVGEdit, Playback.EncryptText(cifsPassword), true);
            }

            // Type '{Tab}' in 'PasswordTextEdit' text box
            Keyboard.SendKeys(uICreationParametersVGEdit, "{TAB}", ModifierKeys.None);
            if (ip != null)
            {
                
                // Type ip in 'Editing control' text box
                uIEditingcontrolEdit.Text = ip;
            }

            // Type '{Tab}' in 'Editing control' text box
            Keyboard.SendKeys(uIEditingcontrolEdit, "{TAB}", ModifierKeys.None);
            if (portNum != null)
            {                

                // Type port number in 'Editing control' text box
                uIEditingcontrolEdit.Text = portNum;
            }

            // Type '{Tab}' in 'Editing control' text box
            Keyboard.SendKeys(uIEditingcontrolEdit, "{TAB}", ModifierKeys.None);
            if (validationFlag == "true")
            {                
                // Select Check Box
                uIEditingcontrolCheckBox.Checked = true;

            }
            else
            {                
                // Unselect Check Box
                uIEditingcontrolCheckBox.Checked = false;
            }
        }

        /// <summary>
        /// InputVMAXStorageInfo 
        /// Description:
        ///     Input storage information for VMAX, VMAXe
        /// Input:
        ///     storageInfo: an array which contrains "Storage ID", "SpmServerAddress"
        ///                  "Spm Server Port number", "ClientID", "Password", 
        ///                  "Entiry ID", "CIFS Bypass server certificate validation" = true | false
        /// Output:
        ///     None
        /// </summary>
        public void InputVMAXStorageInfo(string[] storageInfo)
        {
            #region Variable Declarations
            string storageID = storageInfo[0];
            string spmIP = storageInfo[1];
            string portNum = storageInfo[2];
            string clientID = storageInfo[3];
            string password = storageInfo[4];
            string entityID = storageInfo[5];            
            string validationFlag = storageInfo[6];

            WinClient uIHeaderPanelClient = this.UIAddStorageSystemWindow.UICreationParametersVGTable.UIHeaderPanelClient;
            WinEdit uIEditingcontrolEdit = this.UIAddStorageSystemWindow.UICreationParametersVGTable.UIEditingcontrolEdit;
            WinEdit uICreationParametersVGEdit = this.UIAddStorageSystemWindow.UICreationParametersVGTable.UICreationParametersVGEdit;
            WinCheckBox uIEditingcontrolCheckBox = this.UIAddStorageSystemWindow.UICreationParametersVGTable.UIEditingcontrolCheckBox;
            #endregion

            if (storageID != null)
            {
                // Click 'Header Panel' client
                Mouse.Click(uIHeaderPanelClient, new Point(276, 43));

                // Type storage ID in 'Editing control' text box               
                uIEditingcontrolEdit.Text = storageID;
            }

            if (spmIP != null)
            {
                // Click 'Header Panel' client
                Mouse.Click(uIHeaderPanelClient, new Point(288, 68));

                // Type spmIP in 'Editing control' text box
                uIEditingcontrolEdit.Text = spmIP;
            }
           
            if (portNum != null)
            {
                // Click 'Header Panel' client
                Mouse.Click(uIHeaderPanelClient, new Point(270, 89));

                // Type port number in 'Editing control' text box
                uIEditingcontrolEdit.Text = portNum;
            }

            if (clientID != null)
            {
                // Click 'Header Panel' client
                Mouse.Click(uIHeaderPanelClient, new Point(280, 110));

                // Type client ID in 'Editing control' text box
                uIEditingcontrolEdit.Text = clientID;
            }

            if (password != null)
            {
                // Click 'Header Panel' client
                Mouse.Click(uIHeaderPanelClient, new Point(266, 134));

                // Type password in 'creationParametersVGridControl' text box
                Keyboard.SendKeys(uICreationParametersVGEdit, Playback.EncryptText(password), true);
            }
            
            if (entityID != null)
            {
                // Click 'Header Panel' client
                Mouse.Click(uIHeaderPanelClient, new Point(268, 155));

                // Type entity ID in 'Editing control' text box
                uIEditingcontrolEdit.Text = entityID;

            }

            // Type '{Tab}' in 'uIEditingcontrolEdit' text box
            Keyboard.SendKeys(uIEditingcontrolEdit, "{TAB}", ModifierKeys.None);
            
            if (validationFlag == "true")
            {
                // Select Check Box
                uIEditingcontrolCheckBox.Checked = true;

            }
            else
            {
                // Unselect Check Box
                uIEditingcontrolCheckBox.Checked = false;
            }
        }

        /// <summary>
        /// InputStorageInfo
        /// Description:
        ///     Input storage type, friendly name and connection info
        /// Input:
        ///     storageType: a string indicates the storage type
        ///     friendlyName: a string of the friendly name
        ///     storageInfo: an array which contrains "Storage ID", "SpmServerAddress"
        ///                  "Spm Server Port number", "ClientID", "Password", 
        ///                  "Entiry ID", "CIFS Bypass server certificate validation" = true | false
        /// Output:
        ///     None
        /// </summary>
        public void InputStorageInfo(string storageType, string friendlyName, string[] storageInfo)
        {

            // select storage type
            SelectStorageType(storageType);

            // input friendly name
            if (friendlyName != null)
            {
                InputFriendlyName(friendlyName);
            }

            // input sotrage info
            switch (storageType)
            {
                case "CLARiiON-CX4":
                case "VNX-Block":
                case "VNXe":
                    InputBlockStorageInfo(storageInfo);
                    break;
                case "VMAX":
                case "VMAXe":
                    InputVMAXStorageInfo(storageInfo);
                    break;
                case "VNXCIFS":
                    InputCIFSStorageInfo(storageInfo);
                    break;
                case "VNX":
                    InputVNXStorageInfo(storageInfo);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// ClickAddButton
        /// /// Description:
        ///     Click Add button to add storage system
        /// Input:
        ///     None
        /// Output:
        ///     None
        /// </summary>
        public void ClickAddButton()
        {
            #region Variable Declarations
            WinButton uIAddButton = this.UIAddStorageSystemWindow.UIAddStorageSystemClient.UIAddButton;
            #endregion

            Playback.Wait(1000);
            uIAddButton.WaitForControlEnabled();

            // Click 'Add' button
            //Mouse.Click(uIAddButton, new Point(48, 11)); 
            Mouse.Click(uIAddButton, uIAddButton.GetClickablePoint());
            this.UIAddStorageSystemWindow.WaitForControlNotExist();
        }
     
    }
   
}
