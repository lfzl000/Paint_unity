using System;
using System.Diagnostics;
using System.Drawing.Printing;
using System.IO;
using UnityEngine;

public static class Print
{
    public static void PrintTexture(byte[] texture2DBytes, int numCopies, string printerName)
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
        Print.PrintCMD(text, numCopies, printerName);
    }

    public static void PrintTextureByPath(string path, int numCopies, string printerName)
    {
        PrinterSettings printerSettings = new PrinterSettings();
        if (printerName == null || printerName.Equals(""))
        {
            printerName = printerSettings.PrinterName;
            UnityEngine.Debug.Log("Printing to default printer (" + printerName + ").");
        }
        Print.PrintCMD(path, numCopies, printerName);
    }

    private static void PrintCMD(string path, int numCopies, string printerName)
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