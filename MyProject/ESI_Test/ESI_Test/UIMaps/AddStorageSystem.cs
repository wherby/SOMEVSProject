namespace ESI_Test.UIMaps.AddStorageSystemClasses
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
    
    
    public partial class AddStorageSystem
    {
        public void ModifiedaddStorageSystem()
        {
            #region Variable Declarations
            WinButton uIAddStorageSystemButton = this.UIEMCStorageIntegratorWindow.UIEMCStorageIntegratorWindow1.UIActionsPane.UIAddStorageSystemButton;
            WinButton uIOpenButton = this.UIAddStorageSystemWindow.UICLARiiONCX4Client.UIOpenButton;
            WinListItem uIVMAXListItem = this.UIItemWindow.UIVMAXList.UIVMAXListItem;
            WinListItem uIVMAXeListItem = this.UIItemWindow.UIVMAXeList.UIVMAXeListItem;
            WinList uIVMAXeList = this.UIItemWindow.UIItemClient.UIVMAXeList;
            #endregion

            Console.WriteLine("UIItemWindow11111");
            // Click 'Add Storage System' button
            Mouse.Click(uIAddStorageSystemButton, new Point(77, 12));
               
      
            try
            {
                this.UIItemWindow.DrawHighlight();

            }
            catch
            {
                Console.WriteLine("UIItemWindow2");
            }

            // Click 'Open' button
            Mouse.Click(uIOpenButton, new Point(8, 8));
       

            // this.UIItemWindow.DrawHighlight();
            // this.UIItemWindow.UICLARiiONCX4List.DrawHighlight();
            // Mouse hover 'VMAX' list item at (34, 12)
            // Mouse.Hover(this.UIAddStorageSystemWindow, new Point(134, 72));
            Mouse.Location = new Point(600, 350);

            // Mouse hover 'VMAXe' list item at (32, 6)
            //  Mouse.Hover(uIVMAXeListItem, new Point(32, 6));

            // Select '' in 'VMAXe' list box
            Mouse.Click();
            Playback.Wait(10000);
            // this.UIItemWindow.UICLARiiONCX4List.SelectedItemsAsString = this.UIItemWindow.UICLARiiONCX4List.UICLARiiONCX4ListItem.DisplayText;
            //uIVMAXeList.SelectedItemsAsString = this.addStorageSystemParams.UIVMAXeListSelectedItemsAsString;
        }
    }
}
