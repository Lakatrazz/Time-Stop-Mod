using MelonLoader;
using zCubed.Features; 

namespace zCubed.Globals
{
    public static class CommonGlobals
    {
        public static Enums.InputLock inputLock = Enums.InputLock.Normal;

        public static string SceneName = "";

        public static float oldGravityScale = -9.8f;
        public static float GravityScale = -9.8f;
        public static float TimeScale = 1f;

        public static FreeCamera CameraInstance = null;
        public static GravityCube GravCubeInstance = null;

        public static void Reset()
        {
            // Reset floats
            oldGravityScale = -9.8f;
            GravityScale = -9.8f;
            TimeScale = 1f;

            // Clear instances
            CameraInstance = null;
            GravCubeInstance = null;

            // Reset the input lock
            inputLock = Enums.InputLock.Normal;
        }

        public static void DefaultValues()
        {
            oldGravityScale = -9.8f;
            GravityScale = -9.8f;
            TimeScale = 1f;

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
    }
}
