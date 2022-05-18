#region Using directives
using System;
using UAManagedCore;
using OpcUa = UAManagedCore.OpcUa;
using QPlatform.NativeUI;
using QPlatform.NetLogic;
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
using QPlatform.S7TCP;
using QPlatform.OmronFins;
#endregion

public class VariablesSimulator : BaseNetLogic
{

    private PeriodicTask MyTask;
    private int iCounter;
    private double dCounter;
    private bool bRun;

    public override void Start()
    {
        MyTask = new PeriodicTask(Simulation, 250, LogicObject);
        iCounter = 0;
        dCounter = 0;
        MyTask.Start();
    }

    public void Simulation()
    {
        bRun = LogicObject.GetVariable("bRunSimulation").Value;
        if (bRun == true)
        {
            if (iCounter<=99)
            {
                iCounter =  iCounter + 1;
            }
            else
            {
                iCounter = 0;
            }
            dCounter = dCounter + 0.05;
            LogicObject.GetVariable("iRamp").Value = iCounter;
            LogicObject.GetVariable("iSin").Value = Math.Sin(dCounter) * 100;
            LogicObject.GetVariable("iCos").Value = Math.Cos(dCounter) * 50;
        }

    }

    public override void Stop()
    {
        if (MyTask != null)
        {
            MyTask.Dispose();
            MyTask = null;
        }
    }
}
