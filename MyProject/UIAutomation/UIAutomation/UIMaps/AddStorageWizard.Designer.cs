﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by coded UI test builder.
//      Version: 10.0.0.0
//
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------

namespace UIAutomation.UIMaps.AddStorageWizardClasses
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Text.RegularExpressions;
    using System.Windows.Input;
    using Microsoft.VisualStudio.TestTools.UITest.Extension;
    using Microsoft.VisualStudio.TestTools.UITesting;
    using Microsoft.VisualStudio.TestTools.UITesting.WinControls;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Keyboard = Microsoft.VisualStudio.TestTools.UITesting.Keyboard;
    using Mouse = Microsoft.VisualStudio.TestTools.UITesting.Mouse;
    using MouseButtons = System.Windows.Forms.MouseButtons;
    
    
    [GeneratedCode("Coded UITest Builder", "10.0.30319.1")]
    public partial class AddStorageWizard
    {
        
        #region Properties
        public UIAddStorageSystemWindow UIAddStorageSystemWindow
        {
            get
            {
                if ((this.mUIAddStorageSystemWindow == null))
                {
                    this.mUIAddStorageSystemWindow = new UIAddStorageSystemWindow();
                }
                return this.mUIAddStorageSystemWindow;
            }
        }
        
        public UITestConnectionWindow1 UITestConnectionWindow
        {
            get
            {
                if ((this.mUITestConnectionWindow == null))
                {
                    this.mUITestConnectionWindow = new UITestConnectionWindow1();
                }
                return this.mUITestConnectionWindow;
            }
        }
        
        public UIItemWindow UIItemWindow
        {
            get
            {
                if ((this.mUIItemWindow == null))
                {
                    this.mUIItemWindow = new UIItemWindow();
                }
                return this.mUIItemWindow;
            }
        }
        #endregion
        
        #region Fields
        private UIAddStorageSystemWindow mUIAddStorageSystemWindow;
        
        private UITestConnectionWindow1 mUITestConnectionWindow;
        
        private UIItemWindow mUIItemWindow;
        #endregion
    }
    
    [GeneratedCode("Coded UITest Builder", "10.0.30319.1")]
    public class UIAddStorageSystemWindow : WinWindow
    {
        
        public UIAddStorageSystemWindow()
        {
            #region Search Criteria
            this.SearchProperties[WinWindow.PropertyNames.Name] = "Add Storage System:";
            this.SearchProperties.Add(new PropertyExpression(WinWindow.PropertyNames.ClassName, "WindowsForms10.Window", PropertyExpressionOperator.Contains));
            this.WindowTitles.Add("Add Storage System:");
            #endregion
        }
        
        #region Properties
        public WinTitleBar UIAddStorageSystemTitleBar
        {
            get
            {
                if ((this.mUIAddStorageSystemTitleBar == null))
                {
                    this.mUIAddStorageSystemTitleBar = new WinTitleBar(this);
                    #region Search Criteria
                    this.mUIAddStorageSystemTitleBar.WindowTitles.Add("Add Storage System:");
                    #endregion
                }
                return this.mUIAddStorageSystemTitleBar;
            }
        }
        
        public UICLARiiONCX4ComboBox UICLARiiONCX4ComboBox
        {
            get
            {
                if ((this.mUICLARiiONCX4ComboBox == null))
                {
                    this.mUICLARiiONCX4ComboBox = new UICLARiiONCX4ComboBox(this);
                }
                return this.mUICLARiiONCX4ComboBox;
            }
        }
        
        public UIPanelControl1Pane UIPanelControl1Pane
        {
            get
            {
                if ((this.mUIPanelControl1Pane == null))
                {
                    this.mUIPanelControl1Pane = new UIPanelControl1Pane(this);
                }
                return this.mUIPanelControl1Pane;
            }
        }
        
        public UICreationParametersVGTable UICreationParametersVGTable
        {
            get
            {
                if ((this.mUICreationParametersVGTable == null))
                {
                    this.mUICreationParametersVGTable = new UICreationParametersVGTable(this);
                }
                return this.mUICreationParametersVGTable;
            }
        }
        
        public UITestConnectionWindow UITestConnectionWindow
        {
            get
            {
                if ((this.mUITestConnectionWindow == null))
                {
                    this.mUITestConnectionWindow = new UITestConnectionWindow(this);
                }
                return this.mUITestConnectionWindow;
            }
        }
        
        public UIAddStorageSystemClient UIAddStorageSystemClient
        {
            get
            {
                if ((this.mUIAddStorageSystemClient == null))
                {
                    this.mUIAddStorageSystemClient = new UIAddStorageSystemClient(this);
                }
                return this.mUIAddStorageSystemClient;
            }
        }
        #endregion
        
        #region Fields
        private WinTitleBar mUIAddStorageSystemTitleBar;
        
        private UICLARiiONCX4ComboBox mUICLARiiONCX4ComboBox;
        
        private UIPanelControl1Pane mUIPanelControl1Pane;
        
        private UICreationParametersVGTable mUICreationParametersVGTable;
        
        private UITestConnectionWindow mUITestConnectionWindow;
        
        private UIAddStorageSystemClient mUIAddStorageSystemClient;
        #endregion
    }
    
    [GeneratedCode("Coded UITest Builder", "10.0.30319.1")]
    public class UICLARiiONCX4ComboBox : WinComboBox
    {
        
        public UICLARiiONCX4ComboBox(UITestControl searchLimitContainer) : 
                base(searchLimitContainer)
        {
            #region Search Criteria
            this.WindowTitles.Add("Add Storage System:");
            #endregion
        }
        
        #region Properties
        public WinButton UIOpenButton
        {
            get
            {
                if ((this.mUIOpenButton == null))
                {
                    this.mUIOpenButton = new WinButton(this);
                    #region Search Criteria
                    this.mUIOpenButton.SearchProperties[WinButton.PropertyNames.Name] = "Open";
                    this.mUIOpenButton.WindowTitles.Add("Add Storage System:");
                    #endregion
                }
                return this.mUIOpenButton;
            }
        }
        #endregion
        
        #region Fields
        private WinButton mUIOpenButton;
        #endregion
    }
    
    [GeneratedCode("Coded UITest Builder", "10.0.30319.1")]
    public class UIPanelControl1Pane : WinPane
    {
        
        public UIPanelControl1Pane(UITestControl searchLimitContainer) : 
                base(searchLimitContainer)
        {
            #region Search Criteria
            this.SearchProperties[WinControl.PropertyNames.Name] = "panelControl1";
            this.WindowTitles.Add("Add Storage System:");
            #endregion
        }
        
        #region Properties
        public WinEdit UISystemNameTextEditEdit
        {
            get
            {
                if ((this.mUISystemNameTextEditEdit == null))
                {
                    this.mUISystemNameTextEditEdit = new WinEdit(this);
                    #region Search Criteria
                    this.mUISystemNameTextEditEdit.WindowTitles.Add("Add Storage System:");
                    #endregion
                }
                return this.mUISystemNameTextEditEdit;
            }
        }
        
        public WinEdit UIItemEdit
        {
            get
            {
                if ((this.mUIItemEdit == null))
                {
                    this.mUIItemEdit = new WinEdit(this);
                    #region Search Criteria
                    this.mUIItemEdit.WindowTitles.Add("Add Storage System:");
                    #endregion
                }
                return this.mUIItemEdit;
            }
        }
        #endregion
        
        #region Fields
        private WinEdit mUISystemNameTextEditEdit;
        
        private WinEdit mUIItemEdit;
        #endregion
    }
    
    [GeneratedCode("Coded UITest Builder", "10.0.30319.1")]
    public class UICreationParametersVGTable : WinTable
    {
        
        public UICreationParametersVGTable(UITestControl searchLimitContainer) : 
                base(searchLimitContainer)
        {
            #region Search Criteria
            this.WindowTitles.Add("Add Storage System:");
            #endregion
        }
        
        #region Properties
        public WinClient UIHeaderPanelClient
        {
            get
            {
                if ((this.mUIHeaderPanelClient == null))
                {
                    this.mUIHeaderPanelClient = new WinClient(this);
                    #region Search Criteria
                    this.mUIHeaderPanelClient.SearchProperties[WinControl.PropertyNames.Name] = "Header Panel";
                    this.mUIHeaderPanelClient.WindowTitles.Add("Add Storage System:");
                    #endregion
                }
                return this.mUIHeaderPanelClient;
            }
        }
        
        public WinEdit UIEditingcontrolEdit
        {
            get
            {
                if ((this.mUIEditingcontrolEdit == null))
                {
                    this.mUIEditingcontrolEdit = new WinEdit(this);
                    #region Search Criteria
                    this.mUIEditingcontrolEdit.SearchProperties[WinEdit.PropertyNames.Name] = "Editing control";
                    this.mUIEditingcontrolEdit.WindowTitles.Add("Add Storage System:");
                    #endregion
                }
                return this.mUIEditingcontrolEdit;
            }
        }
        
        public WinEdit UICreationParametersVGEdit
        {
            get
            {
                if ((this.mUICreationParametersVGEdit == null))
                {
                    this.mUICreationParametersVGEdit = new WinEdit(this);
                    #region Search Criteria
                    this.mUICreationParametersVGEdit.SearchProperties[WinEdit.PropertyNames.Name] = "PasswordTextEdit";
                    this.mUICreationParametersVGEdit.WindowTitles.Add("Add Storage System:");
                    #endregion
                }
                return this.mUICreationParametersVGEdit;
            }
        }
        
        public WinCheckBox UIEditingcontrolCheckBox
        {
            get
            {
                if ((this.mUIEditingcontrolCheckBox == null))
                {
                    this.mUIEditingcontrolCheckBox = new WinCheckBox(this);
                    #region Search Criteria
                    this.mUIEditingcontrolCheckBox.SearchProperties[WinCheckBox.PropertyNames.Name] = "Editing control";
                    this.mUIEditingcontrolCheckBox.WindowTitles.Add("Add Storage System:");
                    #endregion
                }
                return this.mUIEditingcontrolCheckBox;
            }
        }
        #endregion
        
        #region Fields
        private WinClient mUIHeaderPanelClient;
        
        private WinEdit mUIEditingcontrolEdit;
        
        private WinEdit mUICreationParametersVGEdit;
        
        private WinCheckBox mUIEditingcontrolCheckBox;
        #endregion
    }
    
    [GeneratedCode("Coded UITest Builder", "10.0.30319.1")]
    public class UITestConnectionWindow : WinWindow
    {
        
        public UITestConnectionWindow(UITestControl searchLimitContainer) : 
                base(searchLimitContainer)
        {
            #region Search Criteria
            this.SearchProperties[WinWindow.PropertyNames.ControlName] = "testConnectionButton";
            this.WindowTitles.Add("Add Storage System:");
            #endregion
        }
        
        #region Properties
        public WinButton UITestConnectionButton
        {
            get
            {
                if ((this.mUITestConnectionButton == null))
                {
                    this.mUITestConnectionButton = new WinButton(this);
                    #region Search Criteria
                    this.mUITestConnectionButton.SearchProperties[WinButton.PropertyNames.Name] = "Test Connection";
                    this.mUITestConnectionButton.WindowTitles.Add("Add Storage System:");
                    #endregion
                }
                return this.mUITestConnectionButton;
            }
        }
        #endregion
        
        #region Fields
        private WinButton mUITestConnectionButton;
        #endregion
    }
    
    [GeneratedCode("Coded UITest Builder", "10.0.30319.1")]
    public class UIAddStorageSystemClient : WinClient
    {
        
        public UIAddStorageSystemClient(UITestControl searchLimitContainer) : 
                base(searchLimitContainer)
        {
            #region Search Criteria
            this.SearchProperties[WinControl.PropertyNames.Name] = "Add Storage System:";
            this.WindowTitles.Add("Add Storage System:");
            #endregion
        }
        
        #region Properties
        public WinButton UICancelButton
        {
            get
            {
                if ((this.mUICancelButton == null))
                {
                    this.mUICancelButton = new WinButton(this);
                    #region Search Criteria
                    this.mUICancelButton.SearchProperties[WinButton.PropertyNames.Name] = "Cancel";
                    this.mUICancelButton.WindowTitles.Add("Add Storage System:");
                    #endregion
                }
                return this.mUICancelButton;
            }
        }
        
        public WinButton UIAddButton
        {
            get
            {
                if ((this.mUIAddButton == null))
                {
                    this.mUIAddButton = new WinButton(this);
                    #region Search Criteria
                    this.mUIAddButton.SearchProperties[WinButton.PropertyNames.Name] = "Add";
                    this.mUIAddButton.WindowTitles.Add("Add Storage System:");
                    #endregion
                }
                return this.mUIAddButton;
            }
        }
        #endregion
        
        #region Fields
        private WinButton mUICancelButton;
        
        private WinButton mUIAddButton;
        #endregion
    }
    
    [GeneratedCode("Coded UITest Builder", "10.0.30319.1")]
    public class UITestConnectionWindow1 : WinWindow
    {
        
        public UITestConnectionWindow1()
        {
            #region Search Criteria
            this.SearchProperties[WinWindow.PropertyNames.Name] = "Test Connection";
            this.SearchProperties.Add(new PropertyExpression(WinWindow.PropertyNames.ClassName, "WindowsForms10.Window", PropertyExpressionOperator.Contains));
            this.WindowTitles.Add("Test Connection");
            #endregion
        }
        
        #region Properties
        public UITestConnectionDialog UITestConnectionDialog
        {
            get
            {
                if ((this.mUITestConnectionDialog == null))
                {
                    this.mUITestConnectionDialog = new UITestConnectionDialog(this);
                }
                return this.mUITestConnectionDialog;
            }
        }
        #endregion
        
        #region Fields
        private UITestConnectionDialog mUITestConnectionDialog;
        #endregion
    }
    
    [GeneratedCode("Coded UITest Builder", "10.0.30319.1")]
    public class UITestConnectionDialog : WinControl
    {
        
        public UITestConnectionDialog(UITestControl searchLimitContainer) : 
                base(searchLimitContainer)
        {
            #region Search Criteria
            this.SearchProperties[UITestControl.PropertyNames.Name] = "Test Connection";
            this.SearchProperties[UITestControl.PropertyNames.ControlType] = "Dialog";
            this.WindowTitles.Add("Test Connection");
            #endregion
        }
        
        #region Properties
        public WinButton UIOKButton
        {
            get
            {
                if ((this.mUIOKButton == null))
                {
                    this.mUIOKButton = new WinButton(this);
                    #region Search Criteria
                    this.mUIOKButton.SearchProperties[WinButton.PropertyNames.Name] = "&OK";
                    this.mUIOKButton.WindowTitles.Add("Test Connection");
                    #endregion
                }
                return this.mUIOKButton;
            }
        }
        
        public WinControl UITestConnectionImage
        {
            get
            {
                if ((this.mUITestConnectionImage == null))
                {
                    this.mUITestConnectionImage = new WinControl(this);
                    #region Search Criteria
                    this.mUITestConnectionImage.SearchProperties[UITestControl.PropertyNames.Name] = "Test Connection";
                    this.mUITestConnectionImage.SearchProperties[UITestControl.PropertyNames.ControlType] = "Image";
                    this.mUITestConnectionImage.WindowTitles.Add("Test Connection");
                    #endregion
                }
                return this.mUITestConnectionImage;
            }
        }
        
        public WinText UIParameterStorageIDisText
        {
            get
            {
                if ((this.mUIParameterStorageIDisText == null))
                {
                    this.mUIParameterStorageIDisText = new WinText(this);
                    #region Search Criteria
                    this.mUIParameterStorageIDisText.SearchProperties[WinText.PropertyNames.Name] = "Parameter \'StorageID\' is needed to create the CLARiiON Storage System Object.";
                    this.mUIParameterStorageIDisText.WindowTitles.Add("Test Connection");
                    #endregion
                }
                return this.mUIParameterStorageIDisText;
            }
        }
        #endregion
        
        #region Fields
        private WinButton mUIOKButton;
        
        private WinControl mUITestConnectionImage;
        
        private WinText mUIParameterStorageIDisText;
        #endregion
    }
    
    [GeneratedCode("Coded UITest Builder", "10.0.30319.1")]
    public class UIItemWindow : WinWindow
    {
        
        public UIItemWindow()
        {
            #region Search Criteria
            this.SearchProperties.Add(new PropertyExpression(WinWindow.PropertyNames.ClassName, "WindowsForms10.Window", PropertyExpressionOperator.Contains));
            #endregion
        }
        
        #region Properties
        public UIVMAXList UIVMAXList
        {
            get
            {
                if ((this.mUIVMAXList == null))
                {
                    this.mUIVMAXList = new UIVMAXList(this);
                }
                return this.mUIVMAXList;
            }
        }
        
        public UICLARiiONCX4List UICLARiiONCX4List
        {
            get
            {
                if ((this.mUICLARiiONCX4List == null))
                {
                    this.mUICLARiiONCX4List = new UICLARiiONCX4List(this);
                }
                return this.mUICLARiiONCX4List;
            }
        }
        
        public UIVMAXeList UIVMAXeList
        {
            get
            {
                if ((this.mUIVMAXeList == null))
                {
                    this.mUIVMAXeList = new UIVMAXeList(this);
                }
                return this.mUIVMAXeList;
            }
        }
        
        public UIVNXList UIVNXList
        {
            get
            {
                if ((this.mUIVNXList == null))
                {
                    this.mUIVNXList = new UIVNXList(this);
                }
                return this.mUIVNXList;
            }
        }
        
        public UIVNXBlockList UIVNXBlockList
        {
            get
            {
                if ((this.mUIVNXBlockList == null))
                {
                    this.mUIVNXBlockList = new UIVNXBlockList(this);
                }
                return this.mUIVNXBlockList;
            }
        }
        
        public UIVNXeList UIVNXeList
        {
            get
            {
                if ((this.mUIVNXeList == null))
                {
                    this.mUIVNXeList = new UIVNXeList(this);
                }
                return this.mUIVNXeList;
            }
        }
        
        public UIVNXCIFSList UIVNXCIFSList
        {
            get
            {
                if ((this.mUIVNXCIFSList == null))
                {
                    this.mUIVNXCIFSList = new UIVNXCIFSList(this);
                }
                return this.mUIVNXCIFSList;
            }
        }
        #endregion
        
        #region Fields
        private UIVMAXList mUIVMAXList;
        
        private UICLARiiONCX4List mUICLARiiONCX4List;
        
        private UIVMAXeList mUIVMAXeList;
        
        private UIVNXList mUIVNXList;
        
        private UIVNXBlockList mUIVNXBlockList;
        
        private UIVNXeList mUIVNXeList;
        
        private UIVNXCIFSList mUIVNXCIFSList;
        #endregion
    }
    
    [GeneratedCode("Coded UITest Builder", "10.0.30319.1")]
    public class UIVMAXList : WinList
    {
        
        public UIVMAXList(UITestControl searchLimitContainer) : 
                base(searchLimitContainer)
        {
            #region Search Criteria
            this.SearchProperties[WinList.PropertyNames.Name] = "VMAX";
            #endregion
        }
        
        #region Properties
        public WinListItem UIVMAXListItem
        {
            get
            {
                if ((this.mUIVMAXListItem == null))
                {
                    this.mUIVMAXListItem = new WinListItem(this);
                    #region Search Criteria
                    this.mUIVMAXListItem.SearchProperties[WinListItem.PropertyNames.Name] = "VMAX";
                    #endregion
                }
                return this.mUIVMAXListItem;
            }
        }
        #endregion
        
        #region Fields
        private WinListItem mUIVMAXListItem;
        #endregion
    }
    
    [GeneratedCode("Coded UITest Builder", "10.0.30319.1")]
    public class UICLARiiONCX4List : WinList
    {
        
        public UICLARiiONCX4List(UITestControl searchLimitContainer) : 
                base(searchLimitContainer)
        {
            #region Search Criteria
            this.SearchProperties[WinList.PropertyNames.Name] = "CLARiiON-CX4";
            #endregion
        }
        
        #region Properties
        public WinListItem UICLARiiONCX4ListItem
        {
            get
            {
                if ((this.mUICLARiiONCX4ListItem == null))
                {
                    this.mUICLARiiONCX4ListItem = new WinListItem(this);
                    #region Search Criteria
                    this.mUICLARiiONCX4ListItem.SearchProperties[WinListItem.PropertyNames.Name] = "CLARiiON-CX4";
                    #endregion
                }
                return this.mUICLARiiONCX4ListItem;
            }
        }
        #endregion
        
        #region Fields
        private WinListItem mUICLARiiONCX4ListItem;
        #endregion
    }
    
    [GeneratedCode("Coded UITest Builder", "10.0.30319.1")]
    public class UIVMAXeList : WinList
    {
        
        public UIVMAXeList(UITestControl searchLimitContainer) : 
                base(searchLimitContainer)
        {
            #region Search Criteria
            this.SearchProperties[WinList.PropertyNames.Name] = "VMAXe";
            #endregion
        }
        
        #region Properties
        public WinListItem UIVMAXeListItem
        {
            get
            {
                if ((this.mUIVMAXeListItem == null))
                {
                    this.mUIVMAXeListItem = new WinListItem(this);
                    #region Search Criteria
                    this.mUIVMAXeListItem.SearchProperties[WinListItem.PropertyNames.Name] = "VMAXe";
                    #endregion
                }
                return this.mUIVMAXeListItem;
            }
        }
        #endregion
        
        #region Fields
        private WinListItem mUIVMAXeListItem;
        #endregion
    }
    
    [GeneratedCode("Coded UITest Builder", "10.0.30319.1")]
    public class UIVNXList : WinList
    {
        
        public UIVNXList(UITestControl searchLimitContainer) : 
                base(searchLimitContainer)
        {
            #region Search Criteria
            this.SearchProperties[WinList.PropertyNames.Name] = "VNX";
            #endregion
        }
        
        #region Properties
        public WinListItem UIVNXListItem
        {
            get
            {
                if ((this.mUIVNXListItem == null))
                {
                    this.mUIVNXListItem = new WinListItem(this);
                    #region Search Criteria
                    this.mUIVNXListItem.SearchProperties[WinListItem.PropertyNames.Name] = "VNX";
                    #endregion
                }
                return this.mUIVNXListItem;
            }
        }
        #endregion
        
        #region Fields
        private WinListItem mUIVNXListItem;
        #endregion
    }
    
    [GeneratedCode("Coded UITest Builder", "10.0.30319.1")]
    public class UIVNXBlockList : WinList
    {
        
        public UIVNXBlockList(UITestControl searchLimitContainer) : 
                base(searchLimitContainer)
        {
            #region Search Criteria
            this.SearchProperties[WinList.PropertyNames.Name] = "VNX-Block";
            #endregion
        }
        
        #region Properties
        public WinListItem UIVNXBlockListItem
        {
            get
            {
                if ((this.mUIVNXBlockListItem == null))
                {
                    this.mUIVNXBlockListItem = new WinListItem(this);
                    #region Search Criteria
                    this.mUIVNXBlockListItem.SearchProperties[WinListItem.PropertyNames.Name] = "VNX-Block";
                    #endregion
                }
                return this.mUIVNXBlockListItem;
            }
        }
        #endregion
        
        #region Fields
        private WinListItem mUIVNXBlockListItem;
        #endregion
    }
    
    [GeneratedCode("Coded UITest Builder", "10.0.30319.1")]
    public class UIVNXeList : WinList
    {
        
        public UIVNXeList(UITestControl searchLimitContainer) : 
                base(searchLimitContainer)
        {
            #region Search Criteria
            this.SearchProperties[WinList.PropertyNames.Name] = "VNXe";
            #endregion
        }
        
        #region Properties
        public WinListItem UIVNXeListItem
        {
            get
            {
                if ((this.mUIVNXeListItem == null))
                {
                    this.mUIVNXeListItem = new WinListItem(this);
                    #region Search Criteria
                    this.mUIVNXeListItem.SearchProperties[WinListItem.PropertyNames.Name] = "VNXe";
                    #endregion
                }
                return this.mUIVNXeListItem;
            }
        }
        #endregion
        
        #region Fields
        private WinListItem mUIVNXeListItem;
        #endregion
    }
    
    [GeneratedCode("Coded UITest Builder", "10.0.30319.1")]
    public class UIVNXCIFSList : WinList
    {
        
        public UIVNXCIFSList(UITestControl searchLimitContainer) : 
                base(searchLimitContainer)
        {
            #region Search Criteria
            this.SearchProperties[WinList.PropertyNames.Name] = "VNX-CIFS";
            #endregion
        }
        
        #region Properties
        public WinListItem UIVNXCIFSListItem
        {
            get
            {
                if ((this.mUIVNXCIFSListItem == null))
                {
                    this.mUIVNXCIFSListItem = new WinListItem(this);
                    #region Search Criteria
                    this.mUIVNXCIFSListItem.SearchProperties[WinListItem.PropertyNames.Name] = "VNX-CIFS";
                    #endregion
                }
                return this.mUIVNXCIFSListItem;
            }
        }
        #endregion
        
        #region Fields
        private WinListItem mUIVNXCIFSListItem;
        #endregion
    }
}