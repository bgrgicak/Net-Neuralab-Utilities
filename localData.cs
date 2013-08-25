using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Net_Neuralab_Utilities
{
    /// <summary>
    /// metode za rad s lokalnim podacima
    /// </summary>
    /// <param name="src"></param>
    class localData
    {
        /// <summary>
        /// projectDir vrača trenutnu lokaciju projekta na računalu
        /// </summary>
        /// <returns></returns>
        public string projectDir() {
            return Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
        }
        /// <summary>
        /// renameFile preimenuje podatak koji se nalazi na source lokaciji u naziv zadan drugim parametrom
        /// </summary>
        /// <param name="source"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public void renameFile(string source, string name) {
            var path = Path.GetDirectoryName(source);
            File.Move(source, path + name);
        }
    }
}
