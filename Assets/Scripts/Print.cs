using System;
using System.Collections;
using System.Diagnostics;
using System.Drawing.Printing;
using System.IO;
using UnityEngine;

public class Print : MonoSingleton<Print>
{
    public void PrintTexture(byte[] texture2DBytes, int numCopies, string printerName)
    {
        if (texture2DBytes == null)
        {
            UnityEngine.Debug.LogWarning("Texture is empty.");
            return;
        }
        PrinterSettings printerSettings = new PrinterSettings();
        if (printerName == null || printerName.Equals(""))
        {
            printerName = printerSettings.PrinterName;
            UnityEngine.Debug.Log("Printing to default printer (" + printerName + ").");
        }
        string str = string.Concat(new string[]
        {
                DateTime.Now.Year.ToString(),
                "-",
                DateTime.Now.Month.ToString(),
                "-",
                DateTime.Now.Day.ToString(),
                "-",
                DateTime.Now.Hour.ToString(),
                "-",
                DateTime.Now.Minute.ToString(),
                "-",
                DateTime.Now.Second.ToString(),
                "-",
                DateTime.Now.Millisecond.ToString()
        });
        string text = (Application.persistentDataPath + "\\PrinterFiletmp_" + str + ".png").Replace("/", "\\");
        UnityEngine.Debug.Log("Temporary Path - " + text);
        File.WriteAllBytes(text, texture2DBytes);
        PrintCMD(text, numCopies, printerName);
    }

    public void PrintTextureByPath(string path, int numCopies, string printerName)
    {
        PrinterSettings printerSettings = new PrinterSettings();
        if (printerName == null || printerName.Equals(""))
        {
            printerName = printerSettings.PrinterName;
            UnityEngine.Debug.Log("Printing to default printer (" + printerName + ").");
        }
        PrintCMD(path, numCopies, printerName);
    }

    public void PrintWebTexture(string path, int numCopies, string printerName)
    {
        StartCoroutine(DownLoadToLocal(path, numCopies, printerName));
    }

    private IEnumerator DownLoadToLocal(string url, int numCopies, string printerName)
    {
        WWW w_DownLoad = new WWW(url);
        //file_Format = url.Substring(url.LastIndexOf('.') + 1); //根据URL获取文件的名字。
        yield return w_DownLoad;
        if (w_DownLoad.error == null)
        {
            string str = string.Concat(new string[]
            {
                DateTime.Now.Year.ToString(),
                "-",
                DateTime.Now.Month.ToString(),
                "-",
                DateTime.Now.Day.ToString(),
                "-",
                DateTime.Now.Hour.ToString(),
                "-",
                DateTime.Now.Minute.ToString(),
                "-",
                DateTime.Now.Second.ToString(),
                "-",
                DateTime.Now.Millisecond.ToString()
            });
            string text = (Application.persistentDataPath + "\\PrinterFiletmp_" + str + ".png").Replace("/", "\\");
            FileStream fs = File.Create(text); //path为你想保存文件的路径。
            fs.Write(w_DownLoad.bytes, 0, w_DownLoad.bytes.Length);
            fs.Close();
            if (w_DownLoad.isDone)
            {
                PrintTextureByPath(text, numCopies, printerName);
            }
        }
        else
            UnityEngine.Debug.LogError(w_DownLoad.error);
    }

    private void PrintCMD(string path, int numCopies, string printerName)
    {
        Process process = new Process();
        try
        {
            for (int i = 0; i < numCopies; i++)
            {
                process.StartInfo.FileName = "rundll32";
                process.StartInfo.Arguments = string.Concat(new string[]
                {
                        "C:\\WINDOWS\\system32\\shimgvw.dll,ImageView_PrintTo \"",
                        path,
                        "\" \"",
                        printerName,
                        "\""
                });
                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                process.StartInfo.UseShellExecute = true;
                process.Start();
            }
        }
        catch (Exception arg)
        {
            UnityEngine.Debug.LogError(arg);
        }
        finally
        {
            process.Close();
            UnityEngine.Debug.Log("Texture printing.");
        }
    }
}