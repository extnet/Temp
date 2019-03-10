using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Web.Mvc;

using Ext.Net;
using Ext.Net.MVC;

namespace Crystal.Views.Support
{
    public partial class StaticTest4 : System.Web.Mvc.ViewPage
    {
        protected override void OnPreRender(EventArgs e)
        {
            //Ext.Net.FormPanel parent = (Ext.Net.FormPanel)this.Page.FindControl("testTT");

            //Ext.Net.Container.Config containerConfig = new Ext.Net.Container.Config();
            //containerConfig.ItemID = "test4";
            //containerConfig.Layout = "HBoxLayout";
            //containerConfig.FieldLabel = "Test4";

            //Ext.Net.Container test5 = new Ext.Net.Container(containerConfig);

            //Ext.Net.DateField.Config test6 = new Ext.Net.DateField.Config();
            //test6.ItemID = "test6";
            //test6.Hidden = true;
            //test6.Format = "m/d/Y";

            //Ext.Net.DateField test7 = new Ext.Net.DateField(test6);
            //test7.ID = "test7";
            //test7.IndicatorText = "*";
            //test7.IndicatorCls = "red-text";
            //test7.AllowBlank = false;

            //test5.Add(test7);

            //parent.Add(test5);

            base.OnPreRender(e);
        }


    }
}