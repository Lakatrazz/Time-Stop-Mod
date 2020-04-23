using MelonLoader;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnhollowerBaseLib;
using zCubed.Tools;
using zCubed.Globals;
using System.Collections.Generic;

namespace zCubed.Features
{
    // Due to the required behaviour, this can't be static
    public class GlobalLightMultiplier
    {
        public bool isControlled = false;

        public GlobalLightMultiplier()
        {
            if (CommonGlobals.LightMultiplier == null)
            {
                MelonModLogger.Log("Instance: Created GlobalLightMultiplier");
                CommonGlobals.LightMultiplier = this;

            }
            else
                MelonModLogger.LogError("Error: No more than 1 GlobalLightMultiplier can exist at once!");
        }

        public void DestroyAllStaticLights()
        {
            RecursiveFunctions.SceneList("$DEST_LIGHT");
        }

        public void ToggleControl()
        {
            isControlled = !isControlled;

            if (isControlled)
                CommonGlobals.SetInputLock(Enums.InputLock.LightOffset);
            else
                CommonGlobals.SetInputLock(Enums.InputLock.Normal);
        }
    }
}
