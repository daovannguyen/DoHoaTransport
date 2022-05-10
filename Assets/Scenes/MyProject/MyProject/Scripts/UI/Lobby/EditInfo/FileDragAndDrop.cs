using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using B83.Win32;
using UnityEngine.UI;
using System.IO;
using System;


public class FileDragAndDrop : MonoSingleton<FileDragAndDrop>
{
    public string FilePath;
    void OnEnable ()
    {
        // must be installed on the main thread to get the right thread id.
        UnityDragAndDropHook.InstallHook();
        UnityDragAndDropHook.OnDroppedFiles += OnFiles;
    }
    void OnDisable()
    {
        UnityDragAndDropHook.OnDroppedFiles -= OnFiles;
        UnityDragAndDropHook.UninstallHook();
    }

    public void OnFiles(List<string> aFiles, POINT aPos)
    {
        // do something with the dropped file names. aPos will contain the 
        // mouse position within the window where the files has been dropped.
        string str = aFiles.Aggregate((a, b) => a + "\n\t" + b);
        FilePath = (str).ToString();
        DataOnClient.Instance.playerData.Avatar = Convert.ToBase64String(File.ReadAllBytes(FilePath));
        EventManager.ReceivePlayerData?.Invoke(true);
    }
}
