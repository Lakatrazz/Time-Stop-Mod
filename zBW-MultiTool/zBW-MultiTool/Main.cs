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

            #region TIME APPLICATION
            TimeModifier.RefreshTimeMod();
            #endregion
        }
        #endregion

        #region ON UPDATE METHOD
        public override void OnUpdate()
        {
            // Free Camera Methods
            if (CommonGlobals.CameraInstance != null)
            {
                // If piloting, call the pilot function
                if (CommonGlobals.CameraInstance.isPiloting)
                    CommonGlobals.CameraInstance.PilotCamera();

                // If following, call the follow function
                if (CommonGlobals.CameraInstance.isFollowing)
                    CommonGlobals.CameraInstance.LookAtTarget();

                // Toggle piloting
                if (Input.GetKeyDown(KeyCode.H))
                    CommonGlobals.CameraInstance.TogglePilot();

                // Lock these functions so they dont interfere with piloting the camera
                if (CommonGlobals.inputLock == Enums.InputLock.Normal)
                {
                    // Toggle following
                    if (Input.GetKeyDown(KeyCode.F))
                        CommonGlobals.CameraInstance.ToggleFollow();

                    // Recentering
                    if (Input.GetKeyDown(KeyCode.G))
                        CommonGlobals.CameraInstance.RecenterOnTarget();
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.G))
                    new FreeCamera();
            }
            
            // If the lock is set to normal, do these methods
            if (CommonGlobals.inputLock == Enums.InputLock.Normal)
            {
                // List all things in the current scene
                if (Input.GetKeyDown(KeyCode.Home))
                    Tools.RecursiveFunctions.SceneList();

                // Spawn or delete the gravity cube
                if (Input.GetKeyDown(KeyCode.C))
                {
                    if (CommonGlobals.GravCubeInstance == null)
                        new GravityCube();
                    else
                        CommonGlobals.GravCubeInstance.Delete();
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
        }
        #endregion
    }
}