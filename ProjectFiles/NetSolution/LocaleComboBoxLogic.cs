#region StandardUsing
using System;
using QPlatform.Core;
using QPlatform.CoreBase;
using QPlatform.HMIProject;
using UAManagedCore;
using OpcUa = UAManagedCore.OpcUa;
using QPlatform.NetLogic;
using QPlatform.UI;
using QPlatform.OPCUAServer;
using QPlatform.Alarm;
using QPlatform.EventLogger;
using QPlatform.SQLiteStore;
using QPlatform.Store;
using QPlatform.CoDeSys;
using QPlatform.CommunicationDriver;
using QPlatform.Modbus;
using QPlatform.Datalogger;
using QPlatform.Report;
using QPlatform.Recipe;
using QPlatform.Retentivity;
#endregion

public class LocaleComboBoxLogic : QPlatform.NetLogic.BaseNetLogic
{
    public override void Start()
    {
        var localeCombo = (ComboBox)Owner;

        var projectLocales = (string[])Project.Current.GetVariable("Locales").Value;
        var modelLocales = InformationModel.MakeObject("Locales");
        modelLocales.Children.Clear();

        foreach (var locale in projectLocales)
        {
            var language = InformationModel.MakeVariable(locale, OpcUa.DataTypes.String);
            language.Value = locale;
            modelLocales.Children.Add(language);
        }

        LogicObject.Children.Add(modelLocales);
        localeCombo.Model = modelLocales.NodeId;
    }

    public override void Stop()
    {
        // Insert code to be executed when the user-defined logic is stopped
    }
}
