using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;

using Ext.Net;

namespace Crystal.Views.Support
{
    public partial class StaticTest3 : System.Web.Mvc.ViewPage
    {
        protected override void OnPreLoad(EventArgs e)
        {
            Ext.Net.TreePanel treeGridCapability = (Ext.Net.TreePanel)this.Page.FindControl("TestItemStatusTreeGridPanel");

            Ext.Net.TreeColumn.Config columnCfg = new TreeColumn.Config();
            columnCfg.Text = "User";
            columnCfg.Width = 180;
            columnCfg.DataIndex = "name";

            Ext.Net.TreeColumn clmn = new TreeColumn(columnCfg);
            treeGridCapability.ColumnModel.Columns.Add(clmn);

            Ext.Net.Column.Config columnEmployeeBadgeCfg = new Column.Config();
            columnEmployeeBadgeCfg.Text = "Badge";
            columnEmployeeBadgeCfg.Width = 90;
            columnEmployeeBadgeCfg.DataIndex = "badgeNumber";
           
            base.OnPreLoad(e);
        }
    }
}