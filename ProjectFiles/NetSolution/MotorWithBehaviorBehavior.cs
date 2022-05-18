#region Using directives
using System;
using UAManagedCore;
using OpcUa = UAManagedCore.OpcUa;
using QPlatform.NetLogic;
using QPlatform.NativeUI;
using QPlatform.HMIProject;
using QPlatform.UI;
using QPlatform.CoreBase;
using QPlatform.Alarm;
using QPlatform.Recipe;
using QPlatform.EventLogger;
using QPlatform.Datalogger;
using QPlatform.SQLiteStore;
using QPlatform.Store;
using QPlatform.Report;
using QPlatform.OPCUAServer;
using QPlatform.CoDeSys;
using QPlatform.Modbus;
using QPlatform.Retentivity;
using QPlatform.CommunicationDriver;
using QPlatform.Core;
#endregion

[CustomBehavior]
public class MotorWithBehaviorBehavior : BaseNetBehavior
{
    public override void Start()
    {
        // Insert code to be executed when the user-defined behavior is started
    }

    public override void Stop()
    {
        // Insert code to be executed when the user-defined behavior is stopped
    }

    [ExportMethod]
    public virtual void StartMethod()
    {
        Log.Info($"MotorTypeBehavior.Method1 executed on {Log.Node(Node)}");
        Node.Speed =+ 16;
        Node.Acceleration =+ 9;
        Node.PowerOn = true;
    }

    [ExportMethod]
    public virtual void StopMethod()
    {
        Log.Info($"MotorTypeBehavior.Method2 executed on {Log.Node(Node)}");
        Node.Speed = 0;
        Node.Acceleration = 0;
        Node.PowerOn = false;
    }

    #region Auto-generated code, do not edit!
    protected new MotorWithBehavior Node => (MotorWithBehavior)base.Node;
#endregion
}
