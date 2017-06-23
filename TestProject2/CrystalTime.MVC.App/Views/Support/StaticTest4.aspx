<%@ Page Language="C#" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html  lang="en" xmlns="http://www.w3.org/1999/xhtml">
<head id="Head" runat="server">
    <title>Search </title>    


    <script runat="server">


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest)
        {
            this.GridPanelTest.Store.Primary.DataSource = Enumerable.Range(0, 1000).Select(i => new object[]
            {
                i,
                i,
                i
            });


            this.GridPanelTest.Store.Primary.DataBind();
        }
    }




</script>
    <script type="text/javascript">

        /*https://forums.ext.net/showthread.php?61968-FIXED-1496-4-2-2-JS-error-on-tooltips-when-drag-drop-is-used-and-commit-from-MVC/page2  */

        var commandHandler = function (cmd, data, grid, record) {


        };


        var onAfterRenderDragAndDrop = function (view) {
            var cellDragDrop;


            Ext.Array.each(view.getPlugins()[0], function (plugin) {
                if (plugin instanceof Ext.ux.CellDragDrop) {
                    cellDragDrop = plugin;
                    return false;
                }
            });


            if (cellDragDrop.dragZone) {
                cellDragDrop.dragZone.onBeforeDrag = function (data, e) {
                    if (!data.columnName) { // It means no DataIndex
                        return false;
                    }
                }
            }
        };


        var notifyOver2 = function (ddSource, e, data) {
            return this.dropAllowed;
        };


        var notifyDrop2 = function (ddSource, e, data) {
            return true;
        };


        var prepareCreate1 = function (grid, toolbar, rowIndex, record) {
            var firstButton = toolbar.items.get(0);
            if (record.data.startTestData.indexOf('N') >= 0) {
                firstButton.setVisible(true);
            } else {
                firstButton.setVisible(false);
            }
        };
    </script>




</head>
<body>
    <form id="FormSearch" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server"/>


        <ext:DropTarget ID="DropTarget2"
                            runat="server"      
                    Target="={Ext.getCmp('GridPanelTest').getView()}"
                    Group="ddGroup" >        


            <NotifyDrop Fn="notifyDrop2" />
            <NotifyOver Fn="notifyOver2" />
        </ext:DropTarget>


        <ext:Viewport ID="Viewport1" runat="server" Layout="FitLayout">          
            <Items>  
                <ext:GridPanel 
                    ID="GridPanelTest" 
                    runat="server" 
                    Title="Test Defect" 
                    Layout="FitLayout">
                    <Store>
                        <ext:Store ID="StoreTest"
                                            runat="server"
                                            WarningOnDirty="false"
                                            PageSize="50"
                                            RemoteSort="false"
                                            AutoLoad="false"
                                    >
                            <Proxy>
                                <ext:AjaxProxy  Url="/ta/Support/GetData/" Timeout="60000" >
                                    <ActionMethods READ="GET" />
                                    <Reader>
                                        <ext:JsonReader IDProperty="id" RootProperty="data" TotalProperty="total"/>
                                    </Reader>
                                </ext:AjaxProxy>
                            </Proxy>
                            <Model>
                                <ext:Model ID="Model1" IDProperty="id" runat="server">
                                    <Fields>
                                        <ext:ModelField Name="id" Type="Int" />
                                        <ext:ModelField Name="startTestData" Type="String" />
                                        <ext:ModelField Name="endTestData" Type="String" />
                                    </Fields>
                                </ext:Model>
                            </Model>
                        </ext:Store>
                    </Store>
                    <ColumnModel ID="ColumnModelSea" runat="server">
                        <Columns>
                            <ext:ImageCommandColumn ID="ImageCommandColumn1" runat="server" Width="35">
                                <Commands>
                                    <ext:ImageCommand CommandName="Delete" Icon="Delete">
                                        <ToolTip Text="Delete Row" />
                                    </ext:ImageCommand>
                                </Commands>
                                <Listeners>
                                    <Command Handler="commandHandler(command, record.data, GridPanelTest, record);" />
                                </Listeners>
                            </ext:ImageCommandColumn>
                                <ext:ImageCommandColumn ID="ImageCommandColumn2" runat="server" Width="35">
                                <Commands>
                                    <ext:ImageCommand CommandName="Open" Icon="NoteEdit">
                                        <ToolTip Text="Open Timecard" />
                                    </ext:ImageCommand>
                                </Commands>
                                <Listeners>
                                    <Command Handler="commandHandler(command, record.data, GridPanelTest, record);" />
                                </Listeners>
                            </ext:ImageCommandColumn>
                            <ext:Column ID="Column1" runat="server" Text="Time In" DataIndex="startTestData" >
                                <Editor>
                                    <ext:TextField ID="TextField1" runat="server" />
                                </Editor>
                            </ext:Column>
                            <ext:Column ID="Column2" runat="server" Text="Time Out"  DataIndex="endTestData">
                                <Editor>
                                    <ext:TextField ID="TextField2" runat="server" />
                                </Editor>
                            </ext:Column>
                            <ext:CommandColumn ID="CommandColumn1" runat="server" Width="35">
                                <Commands>
                                    <ext:GridCommand CommandName="Create1" Icon="CalendarAdd">
                                        <ToolTip Text="Create 1" />
                                    </ext:GridCommand>
                                </Commands>
                                <PrepareToolbar Fn="prepareCreate1" />             
                                <Listeners>
                                    <Command Handler="commandHandler(command, record.data, GridPanelTest, record);" />
                                </Listeners>
                            </ext:CommandColumn>
                        </Columns>
                    </ColumnModel>
                    <SelectionModel>
                        <ext:RowSelectionModel ID="RowSelectionModelTimecard" runat="server" Mode="Single" />
                    </SelectionModel>
                    <Plugins>
                        <ext:CellEditing ID="CellEditing1" Enabled="true"  ClicksToEdit="1" runat="server" />
                    </Plugins>
                    <View>
                        <ext:GridView ID="GridView7" runat="server" StripeRows="true">
                            <Plugins>
                                <ext:CellDragDrop ID="CellDragDrop1" runat="server" ApplyEmptyText="true" EnforceType="true" DDGroup="ddGroup" />
                            </Plugins>
                            <Listeners>
                                <AfterRender Fn="onAfterRenderDragAndDrop" />
                            </Listeners>
                        </ext:GridView>
                    </View>
                    <BottomBar>
                    <ext:PagingToolbar ID="PagingToolbar1" 
                        runat="server" 
                        DisplayInfo="true" 
                        DisplayMsg="Displaying results {0} - {1} of {2}" 
                        EmptyMsg="No results to display">
                        <Items>
                            <ext:Label ID="Label1" runat="server" Text="Page size:" />
                            <ext:ToolbarSpacer ID="ToolbarSpacer1" runat="server" Width="5" />
                            <ext:ComboBox ID="ComboBoxPaging" runat="server" Width="50" Editable="false">
                                <Items>
                                    <ext:ListItem Text="50" />
                                    <ext:ListItem Text="100" />
                                    <ext:ListItem Text="250" />
                                    <ext:ListItem Text="500" />
                                </Items>
                                <SelectedItems><ext:ListItem Value="50" /></SelectedItems>
                                <ListConfig Width="50" />
                                <Listeners>
                                    <Select Handler="#{StoreTest}.pageSize = parseInt(this.getValue()); #{PagingToolbar1}.getStore().load();" />
                                    <AfterRender Handler="var size = 50; #{StoreTest}.pageSize = parseInt(size); this.setValue(size);" />
                                </Listeners>
                            </ext:ComboBox>
                                <ext:Button ID="Button1" runat="server" Text="load">
                                    <Listeners>
                                        <Click Handler="App.GridPanelTest.getStore().reload();" />
                                    </Listeners>
                                </ext:Button>
                                <ext:Button ID="Button2" runat="server" Text="test">
                                    <Listeners>
                                        <Click Handler="App.StoreTest.getAt(1).set('endTestData', '5');" />
                                    </Listeners>
                                </ext:Button>
                                <ext:Button ID="ButtonSaveExceptionPunches" runat="server" Text="Save" Icon="Disk">
                                    <ToolTips>
                                        <ext:ToolTip ID="ToolTip8" runat="server" Title="Save Timecard" Html="Click to Save your timecard data updates" />
                                    </ToolTips>
                                    <DirectEvents> 
                                        <Click 
                                            Url="/ta/Support/SaveData/" 
                                            Timeout="240000"
                                            CleanRequest="true" 
                                            Method="POST">
                                            <ExtraParams> 
                                                <ext:Parameter Name="data" Value="#{StoreTest}.getChangedData()" Mode="Raw" Encode="true" />
                                            </ExtraParams> 
                                        </Click>    
                                    </DirectEvents>                                                     
                                </ext:Button>
                            </Items>
                        </ext:PagingToolbar>
                    </BottomBar>
                </ext:GridPanel>
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>