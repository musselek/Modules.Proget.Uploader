using System;

namespace Toolset.Http.ProGetHome.AssetDirectory.Get
{
    public sealed class AssetDirectoryItem
    {
        public string Name { get; set; }
        public string Parent { get; set; }
        public long Size { get; set; }
        public string Type { get; set; }
        public string Content { get; set; }
        public string MD5 { get; set; }
        public string SHA256 { get; set; }
        public string SHA512 { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }
}
