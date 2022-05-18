#region Using directives
using System;
using UAManagedCore;
using OpcUa = UAManagedCore.OpcUa;
using QPlatform.CoDeSys;
using QPlatform.NetLogic;
using QPlatform.UI;
using QPlatform.CoreBase;
using QPlatform.Datalogger;
using QPlatform.Report;
using QPlatform.CommunicationDriver;
using QPlatform.Recipe;
using QPlatform.EventLogger;
using QPlatform.SQLiteStore;
using QPlatform.Store;
using QPlatform.Alarm;
using QPlatform.OPCUAServer;
using QPlatform.Retentivity;
using QPlatform.HMIProject;
using QPlatform.NativeUI;
using QPlatform.Core;
using QPlatform.Modbus;
#endregion

public class CreateDeleteAlarm : BaseNetLogic
{
    [ExportMethod]
    public void CreateAlarm(string alarmName, string alarmMessage, string inputVariable)
    {
        var variable = Project.Current.GetVariable("Model/PrototypesInstances/RuntimeAlarms/" + inputVariable);
        var myAlarm = InformationModel.MakeObject<DigitalAlarm>(alarmName);
        myAlarm.InputValueVariable.SetDynamicLink(variable, DynamicLinkMode.Read);
        myAlarm.NormalStateValue = 0;
        myAlarm.Message = alarmMessage;
        Project.Current.Get<Folder>("Alarms/RuntimeAlarms").Add(myAlarm);
    }

    [ExportMethod]
    public void DeleteAlarm(string alarmName)
    { 
        Project.Current.Get<Folder>("Alarms/RuntimeAlarms").Remove(Project.Current.Get<DigitalAlarm>("Alarms/RuntimeAlarms/" + alarmName));
    }
}
