using UnityEngine;
using MelonLoader;
using UnhollowerBaseLib;
using zCubed.Globals;

namespace zCubed.Features
{
    public static class GlobalLightModifier
    {
        static void UpdateLights()
        {
            MelonModLogger.Log("Light Modifier: Light Offset is " + CommonGlobals.LightOffset.ToString());
            Il2CppReferenceArray<Light> lights = Object.FindObjectsOfType<Light>();

            for (int l = 0; l <= lights.Length - 1; l++)
            {
                if (lights[l])
                {
                    lights[l].intensity = 1 + CommonGlobals.LightOffset;
                    if (CommonGlobals.LightOffset <= -1)
                    {
                        RenderSettings.ambientSkyColor = Color.black;
                        RenderSettings.ambientGroundColor = Color.black;
                        RenderSettings.ambientEquatorColor = Color.black;
                    }                   
                }
            }
        }

        public static void IncrementMod()
        {
            CommonGlobals.LightOffset += 0.1f;
            UpdateLights();
        }

        public static void DecrementMod()
        {
            CommonGlobals.LightOffset -= 0.1f;
            UpdateLights();
        }

        public static void StripLights()
        {
            MelonModLogger.Log("Light Modifier: Stripping scene of lights");
            Il2CppReferenceArray<Light> lights = Object.FindObjectsOfType<Light>();

            for (int l = 0; l <= lights.Length - 1; l++)
            {
                if (lights[l])
                {
                    Component.Destroy(lights[l]);
                }
            }
        }
    }
}
