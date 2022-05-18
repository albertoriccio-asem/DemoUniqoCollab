#region Using directives
using System;
using QPlatform.Core;
using UAManagedCore;
using OpcUa = UAManagedCore.OpcUa;
using QPlatform.NetLogic;
using QPlatform.UI;
using QPlatform.CoreBase;
using QPlatform.Store;
using QPlatform.HMIProject;
using QPlatform.SQLiteStore;
using QPlatform.OPCUAServer;
using QPlatform.NativeUI;
using QPlatform.Alarm;
using QPlatform.ODBCStore;
#endregion

public class CheckPresentationEngineType : BaseNetLogic
{
    public override void Start()
    {
        var isNativeUI = Session.GetVariable("IsNativeUI");
        if (isNativeUI == null)
        {
            isNativeUI = InformationModel.MakeVariable("IsNativeUI", OpcUa.DataTypes.Boolean);
            Session.Add(isNativeUI);
        }

        var presentationEngine = FindPresentationEngine();
        if (presentationEngine != null)
            isNativeUI.Value = presentationEngine.IsInstanceOf(QPlatform.NativeUI.ObjectTypes.NativeUIPresentationEngine);
    }

    IUAObject FindPresentationEngine()
    {
        IUANode currentNode = Session;

        while (true)
        {
            if (currentNode == null)
                return null;

            var currentObject = (IUAObject)currentNode;
            if (currentObject != null && currentObject.IsInstanceOf(QPlatform.UI.ObjectTypes.PresentationEngine))
                return currentObject;

            currentNode = currentNode.Owner;
        }
    }
}
