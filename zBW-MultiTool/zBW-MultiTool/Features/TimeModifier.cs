using UnityEngine;
using MelonLoader;
using zCubed.Globals;

namespace zCubed.Features
{
    public static class TimeModifier
    {
        public static void IncrementTimeMod()
        {
            CommonGlobals.TimeScale += 0.1f;
            MelonModLogger.Log("Value: Time Scale increased to " + CommonGlobals.TimeScale.ToString());
        }

        public static void DecrementTimeMod()
        {
            CommonGlobals.TimeScale -= 0.1f;
            MelonModLogger.Log("Value: Time Scale decreased to " + CommonGlobals.TimeScale.ToString());
        }

        public static void RefreshTimeMod()
        {
            Time.timeScale = CommonGlobals.TimeScale;
        }
    }
}
