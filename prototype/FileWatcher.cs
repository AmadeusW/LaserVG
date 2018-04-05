using System;
using System.IO;

namespace Deo.LaserVg
{
    class FileWatcher
    {
        internal static void Track(string path, Action callback)
        {
            var watcher = new FileSystemWatcher(".", path);
            watcher.Changed += (s, args) => callback?.Invoke();
        }
    }
}
