#region StandardUsing
using System;
using QPlatform.Core;
using QPlatform.CoreBase;
using QPlatform.HMIProject;
using UAManagedCore;
using OpcUa = UAManagedCore.OpcUa;
using QPlatform.NetLogic;
using QPlatform.OPCUAServer;
using QPlatform.UI;
using QPlatform.Retentivity;
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
#endregion

public class UserEditorPanelLoaderLogic : QPlatform.NetLogic.BaseNetLogic
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
	public void GoToUserDetailsPanel()
	{
		var userCountVariable = LogicObject.Get<IUAVariable>("UserCount");
		if (userCountVariable == null)
			return;

		var noUsersPanelVariable = LogicObject.Get<IUAVariable>("NoUsersPanel");
		if (noUsersPanelVariable == null)
			return;

		var userDetailPanelVariable = LogicObject.Get<IUAVariable>("UserDetailPanel");
		if (userDetailPanelVariable == null)
			return;

        var panelLoader = (PanelLoader)Owner;

		NodeId newPanelNode = userCountVariable.Value > 0 ? userDetailPanelVariable.Value : noUsersPanelVariable.Value;
        NodeId userAlias = userCountVariable.Value > 0 ? Owner.Owner.Get<ListBox>("UsersList").SelectedItem : NodeId.Empty;

        panelLoader.ChangePanel(newPanelNode, userAlias);
	}
}
