Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Configuration
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports DevExpress.Web.ASPxTreeList

Partial Public Class _Default
	Inherits System.Web.UI.Page

	Private uow As Session = XpoHelper.GetNewSession()

	Protected Sub Page_Init(ByVal sender As Object, ByVal e As EventArgs)
	End Sub

	Protected Sub tree_VirtualModeCreateChildren(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxTreeList.TreeListVirtualModeCreateChildrenEventArgs)
		Dim collection As New XPCollection(Of MyObject)(uow)
		If e.NodeObject Is Nothing Then
			collection.Criteria = New NullOperator("Parent")
		Else
			collection.Criteria = (New OperandProperty("Parent") = New OperandValue(e.NodeObject))
		End If

		e.Children = collection
	End Sub

	Protected Sub tree_VirtualModeNodeCreating(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxTreeList.TreeListVirtualModeNodeCreatingEventArgs)
		Dim obj As MyObject = TryCast(e.NodeObject, MyObject)

		e.NodeKeyValue = obj.Oid
		e.IsLeaf = Not obj.HasChildren

		e.SetNodeValue("Text", obj.Text)
		e.SetNodeValue("ObjectNo", obj.ObjectNo)
	End Sub
End Class