#region Using directives
using System;
using QPlatform.Core;
using UAManagedCore;
using OpcUa = UAManagedCore.OpcUa;
using QPlatform.CoreBase;
using QPlatform.NetLogic;
using QPlatform.Report;
using QPlatform.Alarm;
using QPlatform.UI;
using QPlatform.Recipe;
using QPlatform.OPCUAServer;
using QPlatform.Store;
using QPlatform.Retentivity;
using QPlatform.SQLiteStore;
using QPlatform.Modbus;
using QPlatform.EventLogger;
using QPlatform.CoDeSys;
using QPlatform.CommunicationDriver;
using QPlatform.HMIProject;
using QPlatform.Datalogger;
using QPlatform.NativeUI;
#endregion

public class ReportEditorOutputMessageLogic : BaseNetLogic
{
    public override void Start()
    {
        messageVariable = Owner.Children.Get<IUAVariable>("Message");
        if (messageVariable == null)
            throw new ArgumentNullException("Unable to find variable Message in OutputMessage label");
    }


    public override void Stop()
    {
        lock (lockObject)
        {
            task?.Dispose();
        }
    }

    [ExportMethod]
    public void SetOutputMessage(string message)
    {
        lock (lockObject)
        {
            task?.Dispose();

            messageVariable.Value = message;
            task = new DelayedTask(() => { messageVariable.Value = ""; }, 5000, LogicObject);
            task.Start();
        }
    }

    [ExportMethod]
    public void SetOutputLocalizedMessage(LocalizedText message)
    {
        SetOutputMessage(InformationModel.LookupTranslation(message).Text);
    }

    DelayedTask task;
    IUAVariable messageVariable;
    object lockObject = new object();
}
