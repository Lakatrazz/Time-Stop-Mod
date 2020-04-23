using System;
using System.IO;
using UnityEngine;
using MelonLoader;

namespace zCubed.Globals
{
    public static class FileGlobals
    {
        public static string DataPath = "";
        static bool DataPathVerified = false;

        public static void VerifyDataPath()
        {
            DataPath = Application.dataPath.Replace("/BONEWORKS_Data", "/Mods/Data");

            if (!Directory.Exists(DataPath))
            {
                MelonModLogger.Log("File System: Data Folder not found, creating...");
                Directory.CreateDirectory(DataPath);
            }

            MelonModLogger.Log("File System: Data Folder path = (" + DataPath + ")");
            DataPathVerified = true;
        }

        public static bool IsVerified()
        {
            if (!DataPathVerified)
                MelonModLogger.LogError("File System: Data Folder not verified, please verify");

            return DataPathVerified;
        }
    }
}
