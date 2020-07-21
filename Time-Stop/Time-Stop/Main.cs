using MelonLoader;
using UnityEngine;
using Lakatrazz.Globals;
using Lakatrazz.Tools;

using System;
using System.Collections.Generic;
using Il2CppSystem;
using StressLevelZero.Interaction;
using StressLevelZero.Rig;
using StressLevelZero.UI;
using StressLevelZero.UI.Radial;
using TMPro;


namespace Lakatrazz
{
    public class TimeStop : MelonMod
    {

        #region APPLICATION START METHOD
        public override void OnApplicationStart()
        {
            MelonModLogger.Log("NOTICE: THIS MOD IS A FORK OF ZCUBED'S BONEWORKS MULTITOOL. HE CREATED THE FOUNDATIONS AND I USED THEM TO CREATE TIME STOP.");
            MelonModLogger.Log("--------------------");
            MelonModLogger.Log("Press M on your keyboard to change the flow of time. (Only affects currently spawned in and non-holstered objects.");

        }
        #endregion

        public override void OnLevelWasInitialized(int level)
        {
            Tools.RecursiveFunctions.SetNormalTime();
            MelonModLogger.Log("Set Normal Time to True");
        }


        public override void OnUpdate()
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                MelonModLogger.Log("Attempting to change the flow of time.");
                Tools.RecursiveFunctions.TimeStop("", false, false);
            }
        }
    }
}