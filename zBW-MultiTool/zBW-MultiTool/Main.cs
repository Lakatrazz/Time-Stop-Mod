using System;
using MelonLoader;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnhollowerBaseLib;
using zCubed.Features;
using zCubed.Globals;
using zCubed.Tools;

namespace zCubed
{
    public class MultiTool : MelonMod
    {
        #region APPLICATION START METHOD
        public override void OnApplicationStart()
        {
            MelonModLogger.Log("zBW-Tools is a multi-tool mod, be warned it is buggy.");
            MelonModLogger.Log("Report any issues to the GitHub page or Boneworks Discord Server.");
        }
        #endregion

        #region ON LEVEL INIT METHOD
        public override void OnLevelWasInitialized(int level)
        {
            // Reset globals
            CommonGlobals.Reset();

            // Call this to output the loaded scene
            SceneLoadLogger.OnLoad();
        }
        #endregion

        #region ON LATE UPDATE METHOD
        public override void OnLateUpdate()
        {
            #region GRAVITY CUBE
            // Update the gravity cube, if it exists
            if (CommonGlobals.GravCubeInstance == null)
                Physics.gravity = Vector3.up * CommonGlobals.GravityScale;
            else
                CommonGlobals.GravCubeInstance.SetGravity();
            #endregion

            #region BLACK HOLE
            if (CommonGlobals.BlackHoleInstance != null)
                CommonGlobals.BlackHoleInstance.Update();
            #endregion
        }
        #endregion

        #region ON UPDATE METHOD
        public override void OnUpdate()
        {            
            // If the lock is set to Normal, do these methods
            if (CommonGlobals.GetInputLock() == Enums.InputLock.Normal)
            {
                // List all things in the current scene
                if (Input.GetKeyDown(KeyCode.Home))
                    RecursiveFunctions.SceneList();

                // List all findable id's in the current scene
                if (Input.GetKeyDown(KeyCode.Insert))
                    RecursiveFunctions.SceneList("$ID_FINDER");

                // Spawn or delete the gravity cube
                if (Input.GetKeyDown(KeyCode.C))
                {
                    if (CommonGlobals.GravCubeInstance == null)
                        new GravityCube();
                    else
                        CommonGlobals.GravCubeInstance.Delete();
                }

                // Spawn or delete the black hole
                if (Input.GetKeyDown(KeyCode.B))
                {
                    if (CommonGlobals.BlackHoleInstance == null)
                        new BlackHole();
                    else
                        CommonGlobals.BlackHoleInstance.Delete();
                }

                #region GRAVITY MODIFICATION
                if (Input.GetKeyDown(KeyCode.Q))
                    GravityModifier.IncrementGravity();

                if (Input.GetKeyDown(KeyCode.W))
                    GravityModifier.DecrementGravity();

                if (Input.GetKeyDown(KeyCode.Alpha0))
                    GravityModifier.ZeroGravity();
                #endregion

                #region TIME MODIFICATION
                if (Input.GetKeyDown(KeyCode.A))
                    TimeModifier.IncrementTimeMod();

                if (Input.GetKeyDown(KeyCode.S))
                    TimeModifier.DecrementTimeMod();
                #endregion

                // Reset the values
                if (Input.GetKeyDown(KeyCode.R))
                    CommonGlobals.DefaultValues();

                // Output the values
                if (Input.GetKeyDown(KeyCode.Tab))
                    CommonGlobals.OutputValues();
            }

            // Free Camera creation and usage
            if (CommonGlobals.CameraInstance != null)
            {
                CommonGlobals.CameraInstance.CameraUpdate();

                // Lock these functions so they dont interfere with piloting the camera
                if (CommonGlobals.GetInputLock() == Enums.InputLock.Normal)
                {
                    if (Input.GetKeyDown(KeyCode.G))
                        CommonGlobals.SetInputLock(Enums.InputLock.CameraControl);
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.G))
                    new FreeCamera();
            }
        }
        #endregion
    }
}