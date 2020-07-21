using System;
using MelonLoader;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnhollowerBaseLib;
using StressLevelZero.Data;
using StressLevelZero.Rig;
using StressLevelZero.Zones;
using Lakatrazz.Globals;
using StressLevelZero.Player;
using StressLevelZero.VRMK;

namespace Lakatrazz.Tools
{
    public class RecursiveFunctions
    {
        static string[] hardcoreBlacklist = new string[] { "[RigManager (Default Brett)]" };
        static string[] blacklistedTimeNames = new string[] { "[RigManager (Default Brett)]" };
        static Il2CppReferenceArray<GameObject> SceneObjects = null;
        static bool normalTime = true;

        public static void SetNormalTime()
        {
            normalTime = true;
        }

        public static void TimeStop(string target = "", bool reset = false, bool dontActivate = false)
        {
            if (reset)
            {
                SceneObjects = null;
            }
            else if (!reset)
            {
                Scene scene = SceneManager.GetActiveScene();

                if (target == "")
                    MelonModLogger.Log("[REDACTED]");

                SceneObjects = scene.GetRootGameObjects();

                if (!dontActivate)
                {

                    for (int i = 0; i <= SceneObjects.Length - 1; i++)
                    {
                        bool isValid = true;
                        bool skip = false;
                        foreach (string blName in blacklistedTimeNames)
                        {
                            if (SceneObjects[i].name == blName)
                            {
                                isValid = false;
                                break;
                            }
                        }
                        foreach (string blName in hardcoreBlacklist)
                        {
                            if (SceneObjects[i].name == blName || SceneObjects[i].transform.root.name == blName)
                            {
                                skip = true;
                                break;
                            }
                        }

                        if (skip)
                        {
                            continue;
                        }

                        Transform targetTransform = SceneObjects[i].transform;

                        if (SceneObjects[i].GetComponent<Rigidbody>() != null)
                        {
                            if (isValid)
                            {
                                if (normalTime)
                                {
                                    if (SceneObjects[i].GetComponent<Rigidbody>().isKinematic == false)
                                    {
                                        SceneObjects[i].GetComponent<Rigidbody>().isKinematic = true;
                                        MelonModLogger.Log("Froze object");
                                    }
                                    else
                                    {
                                        Array.Resize(ref blacklistedTimeNames, blacklistedTimeNames.Length + 1);
                                        blacklistedTimeNames[blacklistedTimeNames.Length - 1] = SceneObjects[i].name;
                                        continue;
                                    }
                                }
                                else
                                {
                                    SceneObjects[i].GetComponent<Rigidbody>().isKinematic = false;
                                    MelonModLogger.Log("Un-Froze Object");
                                }
                            }
                            else
                            {
                                SceneObjects[i].GetComponent<Rigidbody>().isKinematic = true;
                                MelonModLogger.Log("Froze Object");
                            }
                        }

                        switch (target)
                        {
                            default:
                                TimeChildList(targetTransform, target);
                                break;

                            case "":
                                TimeChildList(targetTransform);
                                break;

                            case "$ID_FINDER":
                                IDList(targetTransform);
                                break;
                        }
                    }
                    MelonModLogger.Log("Toggled time.");
                    normalTime = !normalTime;
                }
            }
        }

        // This is still public so that people can use it for their own thing
        public static void TimeChildList(Transform trans, string target = "")
        {
            for (int c = 0; c <= trans.childCount - 1; c++)
            {
                bool isValid = true;
                bool skip = false;
                foreach (string blName in blacklistedTimeNames)
                {
                    if (trans.GetChild(c).gameObject.name == blName)
                    {
                        isValid = false;
                        break;
                    }
                }
                foreach (string blName in hardcoreBlacklist)
                {
                    if (trans.GetChild(c).gameObject.name == blName || trans.GetChild(c).gameObject.transform.root.name == blName)
                    {
                        skip = true;
                        break;
                    }
                }

                if (skip)
                {
                    continue;
                }

                if (trans.GetChild(c).gameObject.GetComponent<Rigidbody>() != null)
                {
                    if (isValid)
                    {
                        if (normalTime)
                        {
                            if (trans.GetChild(c).gameObject.GetComponent<Rigidbody>().isKinematic == false)
                            {
                                trans.GetChild(c).gameObject.GetComponent<Rigidbody>().isKinematic = true;
                                MelonModLogger.Log("Froze child object");
                            }
                            else
                            {
                                Array.Resize(ref blacklistedTimeNames, blacklistedTimeNames.Length + 1);
                                blacklistedTimeNames[blacklistedTimeNames.Length - 1] = trans.GetChild(c).gameObject.name;
                            }
                        }
                        else
                        {
                            trans.GetChild(c).gameObject.GetComponent<Rigidbody>().isKinematic = false;
                            MelonModLogger.Log("Un-Froze Object");
                        }
                    }
                    else
                    {
                        trans.GetChild(c).gameObject.GetComponent<Rigidbody>().isKinematic = true;
                        MelonModLogger.Log("Froze Object");
                    }
                }

                // If there is no target, this just works as an outputter 
                if (target == "")
                {
                    // Get the objects entire list of components
                    Il2CppReferenceArray<Component> Components = (Il2CppReferenceArray<Component>)trans.GetComponents<Component>();

                    // This is where the recursiveness comes from
                    TimeChildList(trans.GetChild(c));

                }
                else
                {
                    if (trans.name == target)
                        Globals.RecursiveGlobals.RegisterFound(trans.gameObject);
                    else
                        TimeChildList(trans.GetChild(c), target);
                }
            }
        }
        // Lists all findable asset IDs
        public static void IDList(Transform transform)
        {
            for (int c = 0; c <= transform.childCount - 1; c++)
            {
                // This is where the recursiveness comes from
                IDList(transform.GetChild(c));
            }
        }
    }
}
