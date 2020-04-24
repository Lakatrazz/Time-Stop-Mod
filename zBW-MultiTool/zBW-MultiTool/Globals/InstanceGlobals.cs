using UnityEngine;

namespace zCubed.Globals
{
    public static class InstanceGlobals
    {
        public static GameObject holoHead;

        public static void AttemptToCacheAssets()
        {
            Object holoHead_tmp = Object.FindObjectFromInstanceID(118580);

            if (holoHead_tmp)
                if (holoHead_tmp.TryCast<GameObject>() != null)
                {
                    holoHead = holoHead_tmp.TryCast<GameObject>();
                    GameObject.DontDestroyOnLoad(holoHead);
                }
        }
    }
}
