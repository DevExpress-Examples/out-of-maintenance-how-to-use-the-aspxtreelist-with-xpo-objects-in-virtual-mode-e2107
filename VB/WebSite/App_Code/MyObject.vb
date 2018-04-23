Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.Xpo

Public Class MyObject
	Inherits XPObject
	Public Sub New(ByVal session As Session)
		MyBase.New(session)
	End Sub

	Private fParent As MyObject
	<Association("ParentChild"), Indexed> _
	Public Property Parent() As MyObject
		Get
			Return fParent
		End Get
		Set(ByVal value As MyObject)
			SetPropertyValue(Of MyObject)("Parent", fParent, value)
		End Set
	End Property

	<Association("ParentChild"), Aggregated> _
	Public ReadOnly Property Children() As XPCollection(Of MyObject)
		Get
			Return GetCollection(Of MyObject)("Children")
		End Get
	End Property

	Private fText As String
	Public Property Text() As String
		Get
			Return fText
		End Get
		Set(ByVal value As String)
			SetPropertyValue(Of String)("Text", fText, value)
		End Set
	End Property

	Private _ObjectNo As Integer
	Public Property ObjectNo() As Integer
		Get
			Return _ObjectNo
		End Get
		Set(ByVal value As Integer)
			SetPropertyValue("ObjectNo", _ObjectNo, value)
		End Set
	End Property

	<PersistentAlias("Children[].Count > 0")> _
	Public ReadOnly Property HasChildren() As Boolean
		Get
			Return CBool(EvaluateAlias("HasChildren"))
		End Get
	End Property
End Class