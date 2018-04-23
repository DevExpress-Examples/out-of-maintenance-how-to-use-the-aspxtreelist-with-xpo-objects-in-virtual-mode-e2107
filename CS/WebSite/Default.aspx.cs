using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Web.ASPxTreeList;

public partial class _Default : System.Web.UI.Page {

    Session uow = XpoHelper.GetNewSession();

    protected void Page_Init(object sender, EventArgs e) {
    }

    protected void tree_VirtualModeCreateChildren(object sender, DevExpress.Web.ASPxTreeList.TreeListVirtualModeCreateChildrenEventArgs e) {
        XPCollection<MyObject> collection = new XPCollection<MyObject>(uow);
        if(e.NodeObject == null)
            collection.Criteria = new NullOperator("Parent");
        else
            collection.Criteria = (new OperandProperty("Parent") == new OperandValue(e.NodeObject));

        e.Children = collection;
    }

    protected void tree_VirtualModeNodeCreating(object sender, DevExpress.Web.ASPxTreeList.TreeListVirtualModeNodeCreatingEventArgs e) {
        MyObject obj = e.NodeObject as MyObject;

        e.NodeKeyValue = obj.Oid;
        e.IsLeaf = !obj.HasChildren;

        e.SetNodeValue("Text", obj.Text);
        e.SetNodeValue("ObjectNo", obj.ObjectNo);
    }
}