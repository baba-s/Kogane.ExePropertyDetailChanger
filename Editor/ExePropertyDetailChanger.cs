using System.Diagnostics;
using System.IO;
using UnityEngine.Assertions;

// ReSharper disable InconsistentNaming

namespace Kogane.ExePropertyDetailChanger
{
    public static class ExePropertyDetailChanger
    {
        private const string TEMPORARY_FOLDER       = @"Library\Kogane.ExePropertyDetailChanger";
        private const string RESOURCES_RC_PATH      = TEMPORARY_FOLDER + @"\resources.rc";
        private const string RESOURCES_RES_FILENAME = "resources.res";
        private const string RESOURCES_RES_PATH     = TEMPORARY_FOLDER + @"\" + RESOURCES_RES_FILENAME;
        private const string SCRIPT_PATH            = TEMPORARY_FOLDER + @"\script.txt";
        private const string LOG_FILENAME           = "log.log";

        public static void Change( in ExePropertyDetailChangerSettings settings )
        {
            if ( Directory.Exists( TEMPORARY_FOLDER ) )
            {
                Directory.Delete( TEMPORARY_FOLDER, true );
            }

            Directory.CreateDirectory( TEMPORARY_FOLDER );

            var headerFileVersion = settings.FileVersion.Replace( ".", "," );

            var rc = $@"VS_VERSION_INFO VERSIONINFO
FILEVERSION {headerFileVersion}
{{
    BLOCK ""StringFileInfo""
    {{
        BLOCK ""041104b0""
        {{
            VALUE ""FileDescription"",    ""{settings.FileDescription}""
            VALUE ""ProductName"",        ""{settings.ProductName}""
            VALUE ""ProductVersion"",     ""{settings.ProductVersion}""
            VALUE ""LegalCopyright"",     ""{settings.LegalCopyright}""
            VALUE ""OriginalFilename"",   ""{settings.OriginalFilename}""
        }}
    }}
    BLOCK ""VarFileInfo""
    {{
        VALUE ""Translation"", 0x411, 1200
    }}
}}";

            File.WriteAllText( RESOURCES_RC_PATH, rc, settings.Encoding );

            Assert.IsTrue( File.Exists( RESOURCES_RC_PATH ) );

            var goRCExePath      = settings.GoRCExePath;
            var goRCExeArguments = $@"/fo ""{RESOURCES_RES_PATH}"" ""{RESOURCES_RC_PATH}""";
            var goRCProcess      = Process.Start( goRCExePath, goRCExeArguments );

            goRCProcess?.WaitForExit();

            Assert.IsTrue( File.Exists( RESOURCES_RES_PATH ) );

            var exePath = settings.ExePath;

            var script = $@"[FILENAMES]
Exe=""{exePath}""
SaveAs=""{exePath}""
Log=""{LOG_FILENAME}""

[COMMANDS]
-addoverwrite {RESOURCES_RES_FILENAME}";

            File.WriteAllText( SCRIPT_PATH, script );

            Assert.IsTrue( File.Exists( SCRIPT_PATH ) );

            var resourceHackerExePath      = settings.ResourceHackerExePath;
            var resourceHackerExeArguments = $@"-script {SCRIPT_PATH}";
            var resourceHackerProcess      = Process.Start( resourceHackerExePath, resourceHackerExeArguments );

            resourceHackerProcess?.WaitForExit();
        }
    }
}