<%@ Page Language="vb" AutoEventWireup="true" CodeFile="Default.aspx.vb" Inherits="_Default" %>

<%@ Register Assembly="DevExpress.Web.v13.1, Version=13.1.14.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
	Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v13.1, Version=13.1.14.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
	Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>ASPxTreeList - Virtual mode using XPObjects</title>
</head>
<body>
	<form id="form1" runat="server">
		<div>
			<dx:ASPxTreeList ID="tree" runat="server" AutoGenerateColumns="False" OnVirtualModeCreateChildren="tree_VirtualModeCreateChildren"
				OnVirtualModeNodeCreating="tree_VirtualModeNodeCreating" Width="400px">
				<Columns>
					<dx:TreeListTextColumn FieldName="Text" VisibleIndex="0" />
					<dx:TreeListTextColumn FieldName="ObjectNo" VisibleIndex="1" />
				</Columns>
			</dx:ASPxTreeList>
			&nbsp;
		</div>
	</form>
</body>
</html>