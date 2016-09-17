using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;

// Put the file to Assets/Plugins/Rider/Editor/ for Unity 5.2.2+
// the file to Assets/Plugins/Editor/Rider for Unity prior 5.2.2

namespace Assets.Plugins.Editor.Rider
{
  [InitializeOnLoad]
  public static class Rider
  {
    public static readonly string SlnFile;
    private static readonly string DefaultApp = EditorPrefs.GetString("kScriptsDefaultApp");
    private static readonly FileInfo RiderFileInfo = new FileInfo(DefaultApp);

    internal static bool Enabled
    {
      get
      {
        if (string.IsNullOrEmpty(DefaultApp))
          return false;
        return DefaultApp.ToLower().Contains("rider"); // seems like app doesn't exist as file
      }
    }

    static Rider()
    {
      if (Enabled)
      {
        var newPath = RiderFileInfo.FullName;
        // try to search the new version

        switch (RiderFileInfo.Extension)
        {
          /*
              Unity itself transforms lnk to exe
              case ".lnk":
              {
                if (riderFileInfo.Directory != null && riderFileInfo.Directory.Exists)
                {
                  var possibleNew = riderFileInfo.Directory.GetFiles("*ider*.lnk");
                  if (possibleNew.Length > 0)
                    newPath = possibleNew.OrderBy(a => a.LastWriteTime).Last().FullName;
                }
                break;
              }*/
          case ".exe":
          {
            var possibleNew =
              RiderFileInfo.Directory.Parent.Parent.GetDirectories("*ider*")
                .SelectMany(a => a.GetDirectories("bin"))
                .SelectMany(a => a.GetFiles(RiderFileInfo.Name))
                .ToArray();
            if (possibleNew.Length > 0)
              newPath = possibleNew.OrderBy(a => a.LastWriteTime).Last().FullName;
            break;
          }
          default:
          {
            break;
          }
        }
        if (newPath != RiderFileInfo.FullName)
        {
          Log(string.Format("Update {0} to {1}", RiderFileInfo.FullName, newPath));
          EditorPrefs.SetString("kScriptsDefaultApp", newPath);
        }
      }

      // Open the solution file
      var projectDirectory = Directory.GetParent(Application.dataPath).FullName;
      var projectName = Path.GetFileName(projectDirectory);
      SlnFile = Path.Combine(projectDirectory, string.Format("{0}.sln", projectName));
    }

    /// <summary>
    /// Asset Open Callback (from Unity)
    /// </summary>
    /// <remarks>
    /// Called when Unity is about to open an asset.
    /// </remarks>
    [UnityEditor.Callbacks.OnOpenAssetAttribute()]
    static bool OnOpenedAsset(int instanceID, int line)
    {
      if (Enabled && (RiderFileInfo.Exists || RiderFileInfo.Extension == ".app"))
      {
        string appPath = Path.GetDirectoryName(Application.dataPath);

        // determine asset that has been double clicked in the project view
        var selected = EditorUtility.InstanceIDToObject(instanceID);

        if (selected.GetType().ToString() == "UnityEditor.MonoScript" ||
            selected.GetType().ToString() == "UnityEngine.Shader")
        {
          var completeFilepath = appPath + Path.DirectorySeparatorChar +
                                 AssetDatabase.GetAssetPath(selected);
          var args = string.Empty;
          // workaround for RIDER-1404 Exception There are opened projects, close them before
          if (GetPossibleRiderProcess() != null)
            args = string.Format(" -l {2} {0}{3}{0}", "\"", SlnFile, line, completeFilepath);
          else
            args = string.Format("{0}{1}{0} -l {2} {0}{3}{0}", "\"", SlnFile, line, completeFilepath);

          CallRider(RiderFileInfo.FullName, args);
          return true;
        }
      }
      return false;
    }

    private static void CallRider(string riderPath, string args)
    {
      var proc = new Process();
      if (new FileInfo(riderPath).Extension == ".app")
      {
        proc.StartInfo.FileName = "open";
        proc.StartInfo.Arguments = string.Format("-n {0}{1}{0} --args {2}", "\"", "/" + riderPath, args);
        Log(proc.StartInfo.FileName + " " + proc.StartInfo.Arguments);
      }
      else
      {
        proc.StartInfo.FileName = riderPath;
        proc.StartInfo.Arguments = args;
        Log("\"" + proc.StartInfo.FileName + "\"" + " " + proc.StartInfo.Arguments);
      }

      proc.StartInfo.UseShellExecute = false;
      proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
      proc.StartInfo.CreateNoWindow = true;
      proc.StartInfo.RedirectStandardOutput = true;
      proc.Start();

      if (new FileInfo(riderPath).Extension == ".exe")
      {
        try
        {
          ActivateWindow();
        }
        catch (Exception e)
        {
          Log("Exception on ActivateWindow: " + e);
        }
      }
    }

    private static void ActivateWindow()
    {
      var process = Process.GetProcesses().FirstOrDefault(b => !b.HasExited && b.ProcessName.Contains("Rider"));
      if (process != null)
      {
        // Collect top level windows
        var topLevelWindows = User32Dll.GetTopLevelWindowHandles();
        // Get process main window title
        var windowHandle =
          topLevelWindows.FirstOrDefault(hwnd => User32Dll.GetWindowProcessId(hwnd) == process.Id);
        if (windowHandle != IntPtr.Zero)
          User32Dll.SetForegroundWindow(windowHandle);
      }
    }

    private static Process GetPossibleRiderProcess()
    {
      var riderProcesses = Process.GetProcesses();
      foreach (var a in riderProcesses)
      {
        try
        {
          if (!a.HasExited && (a.ProcessName.ToLower().Contains("rider")
                               || (a.ProcessName.ToLower().Contains("mono-sgen")
                                   && a.MainModule.FileName.Contains("ReSharperHost"))))
            return a;
        }
        catch (Exception e)
        {
          // for some reason in mono HasExited doesn't help
          Log(e);
        }
      }
      return null;
    }

    [MenuItem("Assets/Open C# Project in Rider", false, 1000)]
    static void MenuOpenProject()
    {
      // Force the project files to be sync
      SyncSolution();

      // Load Project
      CallRider(RiderFileInfo.FullName, string.Format("{0}{1}{0}", "\"", SlnFile));
    }

    [MenuItem("Assets/Open C# Project in Rider", true, 1000)]
    static bool ValidateMenuOpenProject()
    {
      return Enabled;
    }

    /// <summary>
    /// Force Unity To Write Project File
    /// </summary>
    private static void SyncSolution()
    {
      System.Type T = System.Type.GetType("UnityEditor.SyncVS,UnityEditor");
      System.Reflection.MethodInfo SyncSolution = T.GetMethod("SyncSolution",
        System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);
      SyncSolution.Invoke(null, null);
    }

    public static void Log(object message)
    {
      Debug.Log("[Rider] " + message);
    }
  }
}

// Developed using JetBrains Rider =)