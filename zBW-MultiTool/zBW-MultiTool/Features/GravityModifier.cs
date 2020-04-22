using UnityEngine;
using MelonLoader;
using zCubed.Globals;

namespace zCubed.Features
{
    public static class GravityModifier
    {
        public static void IncrementGravity()
        {
            CommonGlobals.GravityScale -= 1f;
            MelonModLogger.Log("Value: Gravity increased to " + CommonGlobals.GravityScale.ToString());
        }

        public static void DecrementGravity()
        {
            CommonGlobals.GravityScale += 1f;
            MelonModLogger.Log("Value: Gravity Scale decreased to " + CommonGlobals.GravityScale.ToString());
        }

        public static void ZeroGravity()
        {
            if (CommonGlobals.GravityScale == 0)
            {
                CommonGlobals.GravityScale = CommonGlobals.oldGravityScale;
                MelonModLogger.Log("Value: Gravity Scale restored to " + CommonGlobals.GravityScale.ToString());
            }
            else
            {
                CommonGlobals.oldGravityScale = CommonGlobals.GravityScale;
                CommonGlobals.GravityScale = 0;
                MelonModLogger.Log("Value: Gravity Scale set to 0");
            }
        }
    }
}
