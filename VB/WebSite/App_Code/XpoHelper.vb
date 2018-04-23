Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.Xpo
Imports DevExpress.Xpo.DB

''' <summary>
''' Summary description for XpoHelper
''' </summary>
Public NotInheritable Class XpoHelper
	Private Sub New()
	End Sub
	Shared Sub New()
		CreateDefaultObjects()
	End Sub

	Public Shared Function GetNewSession() As Session
		Return New Session(DataLayer)
	End Function

	Public Shared Function GetNewUnitOfWork() As UnitOfWork
		Return New UnitOfWork(DataLayer)
	End Function

	Private ReadOnly Shared lockObject As Object = New Object()

	Private Shared fDataLayer As IDataLayer
	Private Shared ReadOnly Property DataLayer() As IDataLayer
		Get
			If fDataLayer Is Nothing Then
				SyncLock lockObject
					fDataLayer = GetDataLayer()
				End SyncLock
			End If
			Return fDataLayer
		End Get
	End Property

	Private Shared Function GetDataLayer() As IDataLayer
		XpoDefault.Session = Nothing

		Dim ds As New InMemoryDataStore()
		Dim dict As DevExpress.Xpo.Metadata.XPDictionary = New DevExpress.Xpo.Metadata.ReflectionDictionary()
		dict.GetDataStoreSchema(GetType(MyObject).Assembly)

		Return New ThreadSafeDataLayer(dict, ds)
	End Function

	Private Const ChildCount As Integer = 5
	Private Const LevelCount As Integer = 4
	Private Shared ObjectCount As Integer = 1

	Private Shared Sub CreateDefaultObjects()

		Using uow As UnitOfWork = GetNewUnitOfWork()
			For j As Integer = 1 To ChildCount
				Dim obj As New MyObject(uow)
				obj.Text = String.Format("Root {0}", j)
				obj.ObjectNo = ObjectCount
				ObjectCount += 1
				CreateChildObjects(uow, 1, obj)
				uow.CommitChanges() ' makes transactions smaller when executed in a loop
			Next j
		End Using
	End Sub
	Private Shared Sub CreateChildObjects(ByVal uow As UnitOfWork, ByVal level As Integer, ByVal parent As MyObject)
		For j As Integer = 1 To ChildCount
			Dim obj As New MyObject(uow)
			obj.Text = String.Format("Child {0}-{1}", level + 1, j)
			obj.Parent = parent
			obj.ObjectNo = ObjectCount
			ObjectCount += 1
			If level < LevelCount Then
				CreateChildObjects(uow, level + 1, obj)
			Else
				obj.Text = obj.Text.Replace("Child", "Last child")
			End If
		Next j
	End Sub
End Class
