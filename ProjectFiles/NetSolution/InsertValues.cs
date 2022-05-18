#region Using directives
using System;
using QPlatform.Core;
using UAManagedCore;
using OpcUa = UAManagedCore.OpcUa;
using QPlatform.CoreBase;
using QPlatform.NetLogic;
using QPlatform.UI;
using QPlatform.Datalogger;
using QPlatform.HMIProject;
using QPlatform.Store;
using QPlatform.OPCUAServer;
using QPlatform.SQLiteStore;
using QPlatform.CommunicationDriver;
#endregion

public class InsertValues : BaseNetLogic
{
    public override void Start()
    {
        // Insert code to be executed when the user-defined logic is started
    }

    public override void Stop()
    {
        // Insert code to be executed when the user-defined logic is stopped
    }

    [ExportMethod]
    public void InsertRandomValues()
    {
        // Get the current project folder.
        var project = Project.Current;

        // Save the names of the columns of the table to an array
        string[] columns = { "Timestamp", "Variable1", "Variable2", "Variable3" };

        // Create and populate a matrix with values to insert into the odbc table
        var rawValues = new object[1, 4];

        // Column TimeStamp
        rawValues[0, 0] = DateTime.UtcNow;

        // Column VariableToLog1
        rawValues[0, 1] =(int)Project.Current.GetVariable("Model/Data/Query/InsertVariable1").Value;

        // Column VariableToLog2
        rawValues[0, 2] = (int)Project.Current.GetVariable("Model/Data/Query/InsertVariable2").Value;

        // Column VariableToLog3
        rawValues[0, 3] = (int)Project.Current.GetVariable("Model/Data/Query/InsertVariable3").Value;
                
        var myStore = LogicObject.Owner as Store;

        // Get Table1 from myStore
        var table1 = myStore.Tables.Get<Table>("DataLogger");

        // Insert values into table1
        table1.Insert(columns, rawValues);
    }
}
