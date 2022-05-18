#region Using directives
using System;
using QPlatform.Core;
using UAManagedCore;
using OpcUa = UAManagedCore.OpcUa;
using QPlatform.CoreBase;
using QPlatform.UI;
using QPlatform.NetLogic;
using QPlatform.Report;
using QPlatform.CoDeSys;
using QPlatform.Alarm;
using QPlatform.Recipe;
using QPlatform.OPCUAServer;
using QPlatform.Store;
using QPlatform.Retentivity;
using QPlatform.SQLiteStore;
using QPlatform.Modbus;
using QPlatform.EventLogger;
using QPlatform.CommunicationDriver;
using QPlatform.HMIProject;
using QPlatform.Datalogger;
using QPlatform.NativeUI;
#endregion

public class CSVExportOutputMessageLogic : BaseNetLogic
{
    public override void Start()
    {
        messageVariable = Owner.GetVariable("Message");
        task = new DelayedTask(() =>
        {
            if (messageVariable == null)
            {
                Log.Error("Unable to find variable Message in LoginFormOutputMessage label");
                return;
            }

            messageVariable.Value = "";
            taskStarted = false;
        }, 5000, LogicObject);
    }

    public override void Stop()
    {
        task?.Dispose();
    }
    [ExportMethod]
    public void SetOutputMessage(string message)

    {
        if (messageVariable == null)
        {
            Log.Error("Unable to find variable Message in LoginFormOutputMessage label");
            return;
        }

        messageVariable.Value = message;

        if (taskStarted)
        {
            task?.Cancel();
            taskStarted = false;
        }

        task.Start();
        taskStarted = true;
    }

    DelayedTask task;
    bool taskStarted = false;
    IUAVariable messageVariable;

}
