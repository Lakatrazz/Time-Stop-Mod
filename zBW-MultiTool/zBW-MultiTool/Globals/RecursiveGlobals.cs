using UnityEngine;
using System.Collections.Generic;

namespace zCubed.Globals
{
    class RecursiveGlobals
    {
        public static List<GameObject> pastObjects = new List<GameObject>();
        public static GameObject lastFound = null;

        public static List<Component> foundComponents = new List<Component>();
        public static bool isFinding = false;

        public static void RegisterFound(GameObject newFound)
        {
            if (lastFound)
            {
                pastObjects.Add(lastFound);
                lastFound = newFound;
            }
            else
                lastFound = newFound;
        }
    }
}
