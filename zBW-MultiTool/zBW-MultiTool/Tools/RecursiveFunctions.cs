using System;
using MelonLoader;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnhollowerBaseLib;

namespace zCubed.Tools
{
    public class RecursiveFunctions
    {
        // This is a helper function that starts the listing of the entire scene.
        public static void SceneList(string target = "")
        {
            Scene scene = SceneManager.GetActiveScene();

            if (target == "")
                MelonModLogger.Log("Notice: Outputting all objects in " + Globals.CommonGlobals.SceneName + " PREPARE FOR LAG");

            Il2CppReferenceArray<GameObject> SceneObjects = scene.GetRootGameObjects();

            for (int i = 0; i <= SceneObjects.Length - 1; i++)
            {
                Transform targetTransform = SceneObjects[i].transform;

                switch (target)
                {
                    default:
                        ChildList(targetTransform, target);
                        break;

                    case "":
                        MelonModLogger.Log(ConsoleColor.Blue, "List Start, Root: " + SceneObjects[i].name);
                        ChildList(targetTransform);
                        MelonModLogger.Log(ConsoleColor.Blue, "List End");
                        break;

                    case "$ID_FINDER":
                        IDList(targetTransform);
                        break;
                }                  
            }
        }

        // This is still public so that people can use it for their own thing
        public static void ChildList(Transform transform, string target = "")
        {
            for (int c = 0; c <= transform.childCount - 1; c++)
            {
                // If there is no target, this just works as an outputter 
                if (target == "")
                {
                    // Organization log
                    MelonModLogger.Log(ConsoleColor.Magenta, "Object Start: " + transform.name);

                    // If it has a parent, say it is a child of it
                    if (transform.parent)
                        MelonModLogger.Log(ConsoleColor.Magenta, "Child of: " + transform.parent.name);

                    // Organization log
                    MelonModLogger.Log(ConsoleColor.Red, "Components Start");

                    // Get the objects entire list of components
                    Il2CppReferenceArray<Component> Components = transform.GetComponents<Component>();

                    // Iterate through each one, outputting its name 
                    for (int co = 0; co <= Components.Length - 1; co++)
                    {
                        MelonModLogger.Log(ConsoleColor.Green, Components[co].GetIl2CppType().Name);
                    }

                    // Organization logs
                    MelonModLogger.Log(ConsoleColor.Red, "Components End");
                    MelonModLogger.Log(ConsoleColor.Magenta, "Object End");

                    // This is where the recursiveness comes from
                    ChildList(transform.GetChild(c));
                }
                else
                {
                    if (transform.name == target)
                        Globals.RecursiveGlobals.RegisterFound(transform.gameObject);
                    else
                        ChildList(transform.GetChild(c), target);
                }
            }
        }

        // Lists all findable asset IDs
        public static void IDList(Transform transform)
        {
            for (int c = 0; c <= transform.childCount - 1; c++)
            {
                // Organization log
                MelonModLogger.Log(ConsoleColor.Blue, transform.name + " ID: " + transform.gameObject.GetInstanceID().ToString());

                // This is where the recursiveness comes from
                IDList(transform.GetChild(c));
            }
        }
    }
}
