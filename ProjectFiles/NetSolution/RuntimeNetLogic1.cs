#region Using directives
using System;
using UAManagedCore;
using OpcUa = UAManagedCore.OpcUa;
using FTOptix.HMIProject;
using FTOptix.NetLogic;
using FTOptix.UI;
using FTOptix.NativeUI;
using FTOptix.EventLogger;
using FTOptix.SQLiteStore;
using FTOptix.Store;
using FTOptix.Retentivity;
using FTOptix.CoreBase;
using FTOptix.Core;
using System.IO;
#endregion

public class RuntimeNetLogic1 : BaseNetLogic
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
    public void CheckExportedFile()
    {
        var csvPathVariable = LogicObject.Owner.GetObject("AlarmsHistoryExporter").GetVariable("CSVPath");
        var csvPath = new ResourceUri(csvPathVariable.Value).Uri;
        if (!File.Exists(csvPath))
        {
            Log.Error("File " + csvPath + " does not exist");
            return;
        }

        Log.Info("Dumping file " + csvPath + " contents");
        using (var reader = new StreamReader(csvPath))
        {
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                Log.Info("\t" + line);
            }
        }
        Log.Info("End of file dump");
    }
}
