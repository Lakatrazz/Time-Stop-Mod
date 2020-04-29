using MelonLoader;
using zCubed.Features;
using UnityEngine;
using System.Collections.Generic;

namespace zCubed.Globals
{
    public static class CommonGlobals
    {
        static Enums.InputLock inputLock = Enums.InputLock.Root;

        public static string SceneName = "";

        // Values
        public static float oldGravityScale = -9.8f;
        public static float GravityScale = -9.8f;
        public static float TimeScale = 1f;
        public static float LightOffset = 0f;

        // Instances of features
        public static FreeCamera CameraInstance = null;
        public static GravityCube GravCubeInstance = null;
        public static BlackHole BlackHoleInstance = null;
        public static ChromaScreen ChromaInstance = null;
        public static TurnTable TurnTableInstance = null;

        public static void Reset()
        {
            // Reset floats
            oldGravityScale = -9.8f;
            GravityScale = -9.8f;
            TimeScale = 1f;

            // Clear instances
            CameraInstance = null;
            GravCubeInstance = null;
            BlackHoleInstance = null;
            ChromaInstance = null;
            TurnTableInstance = null;

            // Reset the input lock
            inputLock = Enums.InputLock.Root;
        }

        public static void DefaultValues()
        {
            oldGravityScale = -9.8f;
            GravityScale = -9.8f;
            TimeScale = 1f;
            LightOffset = 0f;

            TimeModifier.RefreshTimeMod();
        }

        public static void OutputValues()
        {
            MelonModLogger.Log("Value: Gravity Scale is " + GravityScale.ToString());
            MelonModLogger.Log("Value: Old Gravity Scale is " + oldGravityScale.ToString());
            MelonModLogger.Log("Value: Time Scale is " + TimeScale.ToString());

            MelonModLogger.Log(GravCubeInstance == null ? "Instance: Grav Cube not spawned" : "Instance: Grav Cube spawned");
            MelonModLogger.Log(CameraInstance == null ? "Instance: Free Camera not spawned" : "Instance: Free Camera spawned");
        }

        public static Enums.InputLock GetInputLock() { return inputLock; }

        public static void SetInputLock(Enums.InputLock targetLock)
        {
            MelonModLogger.Log("Input Lock: Switched to " + Enums.FormalName_InputLock[(int)targetLock]);
            inputLock = targetLock;
        }
    }
}
