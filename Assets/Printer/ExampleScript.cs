using UnityEngine;
using System.Collections;
using System.Diagnostics;
using System;
using System.IO;
using UnityEngine.UI;

public class ExampleScript : MonoBehaviour
{
    public Texture2D texture2D;
    public string printerName = "";     //打印机名称，如果为空使用默认打印机
    public int copies = 1;              //打印份数

    public InputField inputField;

    public void PrintTextureButton()
    {
        Print.PrintTexture(texture2D.EncodeToPNG(), copies, printerName);
    }

    public void PrintByPathButton()
    {
        Print.PrintTextureByPath(inputField.text.Trim(), copies, printerName);
    }
}