# Print_Unity
unity中调用打印机打印图片

---
## 说明
此工程是参考一个插件修改而来，在原功能基础上进行了增加。

---
## 工程
里面包含一个示例场景和源代码

示例场景位置：Assets-Printer-ExampleScene

---
## API
- **打印工程内图片** ： 
``` csharp
Print.instance.PrintTexture(Texture2D, copies, printerName);
```
- **打印本地图片** ： 
``` csharp
Print.instance.PrintTextureByPath(localPath, copies, printerName);
```
- **打印网络图片** ： 
``` csharp
Print.instance.PrintWebTexture(webUrl, copies, printerName);
```

---
## 字段说明
- **printerName** : 打印机名称，如果为空，则使用默认打印机
- **copies** : 打印份数