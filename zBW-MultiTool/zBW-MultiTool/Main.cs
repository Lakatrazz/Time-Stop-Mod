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
            MelonModLogger.Log("zBW-Tool is a multi-tool mod.");
            MelonModLogger.Log("Report any issues to the GitHub page or Boneworks Discord Server.");
            MelonModLogger.Log("Press TAB to list the current Input Mode's controls.");
            FileGlobals.VerifyDataPath();           
        }
        #endregion

        #region ON LEVEL INIT METHOD
        public override void OnLevelWasInitialized(int level)
        {
            // Reset globals
            CommonGlobals.Reset();

            // Call this to output the loaded scene
            SceneLoadLogger.OnLoad();

            // Call this to search for any needed to be cached content
            InstanceGlobals.AttemptToCacheAssets();
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
            // If the lock is not Normal, allow the user to exit back to normal mode by pressing E
            if (CommonGlobals.GetInputLock() != Enums.InputLock.Root && Input.GetKeyDown(KeyCode.E))
                CommonGlobals.SetInputLock(Enums.InputLock.Root);

            // List the controls
            if (Input.GetKeyDown(KeyCode.Tab))
                Misc.ControlDocumentation.ListCurrentControls();

            // If the lock is set to Normal, do these methods
            if (CommonGlobals.GetInputLock() == Enums.InputLock.Root)
            {
                if (Input.GetKeyDown(KeyCode.F))
                    CommonGlobals.SetInputLock(Enums.InputLock.Fun);

                if (Input.GetKeyDown(KeyCode.T))
                    CommonGlobals.SetInputLock(Enums.InputLock.Tools);
            }

            // Fun controls
            if (CommonGlobals.GetInputLock() == Enums.InputLock.Fun)
            {
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

                #region LIGHT MODIFICATION
                if (Input.GetKeyDown(KeyCode.Z))
                    GlobalLightModifier.IncrementMod();

                if (Input.GetKeyDown(KeyCode.X))
                    GlobalLightModifier.DecrementMod();
                #endregion

                // Reset the values
                if (Input.GetKeyDown(KeyCode.R))
                    CommonGlobals.DefaultValues();

                // Output the values
                if (Input.GetKeyDown(KeyCode.E))
                    CommonGlobals.OutputValues();
            }

            // Tool controls
            if (CommonGlobals.GetInputLock() == Enums.InputLock.Tools)
            {
                if (Input.GetKeyDown(KeyCode.Q))
                    ObjectIDLogger.OutputEntireAssetDatabase();

                if (Input.GetKeyDown(KeyCode.W))
                    MaterialStripper.StripMaterials();

                if (Input.GetKeyDown(KeyCode.A))
                    RecursiveFunctions.SceneList();

                if (Input.GetKeyDown(KeyCode.S))
                    RecursiveFunctions.SceneList("$ID_FINDER");

            }

            // Free Camera creation and usage
            if (CommonGlobals.CameraInstance != null)
            {
                CommonGlobals.CameraInstance.CameraUpdate();

                // Lock these functions so they dont interfere with piloting the camera
                if (CommonGlobals.GetInputLock() == Enums.InputLock.Root)
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

            // Instance Global Update
            InstanceGlobals.Update();
        }
        #endregion
    }
}