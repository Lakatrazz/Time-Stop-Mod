using UnityEngine;
using MelonLoader;

namespace zCubed.Globals
{
    public static class InstanceGlobals
    {
        public static GameObject holoHead;

        static float LastCheck = 0;
        static float Delay = 20;

        public static void AttemptToCacheAssets()
        {
            Object holoHead_tmp = Object.FindObjectFromInstanceID(118580);

            if (holoHead_tmp && !holoHead)
            {
                if (holoHead_tmp.TryCast<GameObject>() != null)
                {
                    holoHead = GameObject.Instantiate(holoHead_tmp.TryCast<GameObject>());
                    holoHead.active = false;
                    GameObject.DontDestroyOnLoad(holoHead);
                    MelonModLogger.Log("Instance Globals: Cached HoloHead");
                }
            }
        }

        public static void Update()
        {
            if (Time.time > LastCheck + Delay)
            {
                AttemptToCacheAssets();
                LastCheck = Time.time;
            }
        }
    }
}
