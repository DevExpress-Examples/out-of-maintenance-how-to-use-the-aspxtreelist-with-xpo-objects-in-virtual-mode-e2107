using System;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;

/// <summary>
/// Summary description for XpoHelper
/// </summary>
public static class XpoHelper {
    static XpoHelper() {
        CreateDefaultObjects();
    }

    public static Session GetNewSession() {
        return new Session(DataLayer);
    }

    public static UnitOfWork GetNewUnitOfWork() {
        return new UnitOfWork(DataLayer);
    }

    private readonly static object lockObject = new object();

    static IDataLayer fDataLayer;
    static IDataLayer DataLayer {
        get {
            if(fDataLayer == null) {
                lock(lockObject) {
                    fDataLayer = GetDataLayer();
                }
            }
            return fDataLayer;
        }
    }

    private static IDataLayer GetDataLayer() {
        XpoDefault.Session = null;

        InMemoryDataStore ds = new InMemoryDataStore();
        DevExpress.Xpo.Metadata.XPDictionary dict = new DevExpress.Xpo.Metadata.ReflectionDictionary();
        dict.GetDataStoreSchema(typeof(MyObject).Assembly);

        return new ThreadSafeDataLayer(dict, ds);
    }

    const int ChildCount = 5;
    const int LevelCount = 4;
    static int ObjectCount = 1;

    static void CreateDefaultObjects() {

        using(UnitOfWork uow = GetNewUnitOfWork()) {
            for(int j = 1; j <= ChildCount; j++) {
                MyObject obj = new MyObject(uow);
                obj.Text = string.Format("Root {0}", j);
                obj.ObjectNo = ObjectCount++;
                CreateChildObjects(uow, 1, obj);
                uow.CommitChanges();  // makes transactions smaller when executed in a loop
            }
        }
    }
    static void CreateChildObjects(UnitOfWork uow, int level, MyObject parent) {
        for(int j = 1; j <= ChildCount; j++) {
            MyObject obj = new MyObject(uow);
            obj.Text = string.Format("Child {0}-{1}", level + 1, j);
            obj.Parent = parent;
            obj.ObjectNo = ObjectCount++;
            if(level < LevelCount)
                CreateChildObjects(uow, level + 1, obj);
            else
                obj.Text = obj.Text.Replace("Child", "Last child");
        }
    } 
}
