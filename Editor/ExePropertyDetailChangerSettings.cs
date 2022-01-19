using System.Text;

// ReSharper disable InconsistentNaming

namespace Kogane.ExePropertyDetailChanger
{
    public readonly struct ExePropertyDetailChangerSettings
    {
        public string   ExePath               { get; }
        public string   FileDescription       { get; }
        public string   FileVersion           { get; }
        public string   ProductName           { get; }
        public string   ProductVersion        { get; }
        public string   LegalCopyright        { get; }
        public string   OriginalFilename      { get; }
        public string   GoRCExePath           { get; }
        public string   ResourceHackerExePath { get; }
        public Encoding Encoding              { get; }

        public ExePropertyDetailChangerSettings
        (
            string   exePath,
            string   fileDescription,
            string   fileVersion,
            string   productName,
            string   productVersion,
            string   legalCopyright,
            string   originalFilename,
            string   goRxExePath,
            string   resourceHackerExePath,
            Encoding rcEncoding
        )
        {
            ExePath               = exePath;
            FileDescription       = fileDescription;
            FileVersion           = fileVersion;
            ProductName           = productName;
            ProductVersion        = productVersion;
            LegalCopyright        = legalCopyright;
            OriginalFilename      = originalFilename;
            GoRCExePath           = goRxExePath;
            ResourceHackerExePath = resourceHackerExePath;
            Encoding              = rcEncoding;
        }
    }
}