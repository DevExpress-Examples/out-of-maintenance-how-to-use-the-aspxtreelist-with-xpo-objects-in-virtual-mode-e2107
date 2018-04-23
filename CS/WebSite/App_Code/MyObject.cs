using System;
using DevExpress.Xpo;

public class MyObject : XPObject {
    public MyObject(Session session)
        : base(session) {
    }

    MyObject fParent;
    [Association("ParentChild"), Indexed]
    public MyObject Parent {
        get { return fParent; }
        set { SetPropertyValue<MyObject>("Parent", ref fParent, value); }
    }

    [Association("ParentChild"), Aggregated]
    public XPCollection<MyObject> Children {
        get { return GetCollection<MyObject>("Children"); }
    }

    String fText;
    public String Text {
        get { return fText; }
        set { SetPropertyValue<String>("Text", ref fText, value); }
    }

    private int _ObjectNo;
    public int ObjectNo {
        get { return _ObjectNo; }
        set { SetPropertyValue("ObjectNo", ref _ObjectNo, value); }
    }

    [PersistentAlias("Children[].Count > 0")]
    public bool HasChildren {
        get {
            return (bool)EvaluateAlias("HasChildren");
        }
    }
}