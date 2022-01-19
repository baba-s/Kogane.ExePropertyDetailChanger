# Kogane Exe Property Detail Changer

.exe のプロパティの詳細を設定するエディタ拡張

## 使用例

```cs
using System.IO;
using System.Text;
using Kogane.ExePropertyDetailChanger;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

public sealed class Example : IPostprocessBuildWithReport
{
    public int callbackOrder => 0;

    public void OnPostprocessBuild( BuildReport report )
    {
        if ( report.summary.platformGroup != BuildTargetGroup.Standalone ) return;

        var goRxExePath           = Path.GetFullPath( @"Tools\GoRC.exe" );
        var resourceHackerExePath = Path.GetFullPath( @"Tools\ResourceHacker.exe" );

        // 詳細に日本語を設定する場合は文字化けしないように
        // Shift_JIS の Encoding を指定する必要があります
        var rcEncoding = Encoding.GetEncoding( "Shift_JIS" );

        var settings = new ExePropertyDetailChangerSettings
        (
            exePath: report.summary.outputPath,
            fileDescription: "【ファイルの説明】",
            fileVersion: "1.2.3.4",
            productName: "【製品名】",
            productVersion: "5.6.7.8",
            legalCopyright: "【著作権】",
            originalFilename: "【元のファイル名】",
            goRxExePath: goRxExePath,
            resourceHackerExePath: resourceHackerExePath,
            rcEncoding: rcEncoding
        );

        ExePropertyDetailChanger.Change( settings );

        Debug.Log( "完了" );
    }
}
```

![](https://user-images.githubusercontent.com/6134875/150134842-935bc9d6-30ef-4ecb-a756-70e01cc675a1.png)

## 必要なツール

このエディタ拡張を使う場合は以下の .exe を各ページからダウンロードして  
ExePropertyDetailChangerSettings の引数の「goRxExePath」と「resourceHackerExePath」に  
それぞれの .exe の絶対パスを指定する必要があります  

* GoRC.exe：http://www.godevtool.com/
* Resource Hacker.exe：http://www.angusj.com/resourcehacker/