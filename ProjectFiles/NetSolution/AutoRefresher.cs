#region Using directives
using System;
using QPlatform.Core;
using UAManagedCore;
using OpcUa = UAManagedCore.OpcUa;
using QPlatform.Store;
using QPlatform.OPCUAServer;
using QPlatform.UI;
using QPlatform.CoreBase;
using QPlatform.SQLiteStore;
using QPlatform.NativeUI;
using QPlatform.HMIProject;
using QPlatform.NetLogic;
using System.Threading;
using QPlatform.OPCUAClient;
using QPlatform.Datalogger;
using QPlatform.CommunicationDriver;
using QPlatform.EventLogger;
using QPlatform.Recipe;
#endregion

public class AutoRefresher : BaseNetLogic
{
    QPlatform.UI.DataGrid dataGrid;
    public override void Start()
    {
        var autoRefreshCheckBox = LogicObject.Owner.Owner.Get<CheckBox>("ValueAutoRefresh");
        var activeVariable = autoRefreshCheckBox.CheckedVariable;
        activeVariable.VariableChange += OnActiveVariableChanged;
    }

    private void OnActiveVariableChanged(object sender, VariableChangeEventArgs e)
    {
        if ((bool)e.NewValue)
        {
            dataGrid = (QPlatform.UI.DataGrid)Owner;
            refreshTask = new PeriodicTask(RefreshDataGrid, 4000, LogicObject);
            refreshTask.Start();
        }    
        else
        {
            refreshTask?.Dispose();
        }    
    }

    public override void Stop()
    {
        refreshTask?.Dispose();
    }

    public void RefreshDataGrid()
    {
        dataGrid.Refresh();
    }

    private PeriodicTask refreshTask;
}
