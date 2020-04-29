using System;
using System.IO;
using MelonLoader;
using zCubed.Globals;

namespace zCubed.Tools
{
    public static class FileSystem
    {
        /// <summary>
        /// Writes a plain .txt file to said location, overwriting the previous instance.
        /// </summary>
        /// <param name="path">Prefixed with /, the path to the file</param>
        /// <param name="text"></param>
        /// <param name="fileMode"></param>
        /// <returns></returns>
        public static bool WritePlainText(string path, string[] text)
        {
            if (FileGlobals.IsVerified())
            {
                MelonModLogger.Log("File System: Starting Write");
                string compressedString = "";

                for (int s = 0; s <= text.Length - 1; s++)
                {
                    compressedString += text[s];
                }

                File.WriteAllText(FileGlobals.DataPath + path + ".txt", compressedString);

                MelonModLogger.Log("File System: Finished Write, @" + FileGlobals.DataPath + path);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Writes a plain .txt file to said location, overwriting the previous instance.
        /// </summary>
        /// <param name="path">Prefixed with /, the path to the file</param>
        /// <param name="text"></param>
        /// <param name="fileMode"></param>
        /// <returns></returns>
        public static bool WritePlainText(string path, string text) { return WritePlainText(path, new string[] { text }); }
    }
}
