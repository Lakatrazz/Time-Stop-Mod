﻿using UnhollowerBaseLib;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using MelonLoader;
using zCubed.Globals;

namespace zCubed.Features
{
    public class FreeCamera
    {
        Transform PlayerHead = null;
        Transform CameraFollowTarget = null;
        Camera CameraComponent = null;
        Transform CameraTransform = null;
        Transform CameraAnchor = null;

        Collider CubeCollider = null;
        MeshRenderer CubeRenderer = null;
        Rigidbody CubeRigidbody = null;

        public bool isPiloting = false;
        public bool isFollowing = false;
        public bool isThirdPerson = false;
        public bool isFirstPerson = false;
        public bool isTurnTabling = false;
        public bool isPhysics = false;

        bool isManipulatingSpeed = false;
        bool isManipulatingFOV = false;

        float FOV = 70f;
        float SpeedMultiplier = 8f;

        int axisValue = 0;
        Enums.Axes MovingAxis = Enums.Axes.Null;

        float xOffset = 0.65f;
        float yOffset = 0.6f;
        float zOffset = -1.5f;

        bool isPostProcessingEnabled = true;

        // Constructor
        public FreeCamera()
        {
            if (CommonGlobals.CameraInstance == null)
            {
                Il2CppReferenceArray<GameObject> SceneObjects = Object.FindObjectsOfType<GameObject>();

                for (int o = 0; o < SceneObjects.Length; o++)
                {
                    if (SceneObjects[o].name == "[RigManager (Default Brett)]")
                    {
                        PlayerHead = SceneObjects[o].transform.Find("[PhysicsRig]").Find("Head").Find("offset");
                        Transform PlayerCamera = SceneObjects[o].transform.Find("[SkeletonRig (GameWorld Brett)]").Find("Head").Find("FollowCamera");

                        if (!PlayerHead)
                            return;

                        if (!PlayerCamera)
                            return;

                        CameraFollowTarget = PlayerHead;

                        GameObject FreeCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        FreeCube.transform.position = PlayerHead.transform.position;
                        FreeCube.transform.localScale = Vector3.one / 12f;
                        FreeCube.name = "Free Camera";

                        CubeCollider = FreeCube.GetComponent<Collider>();
                        CubeRenderer = FreeCube.GetComponent<MeshRenderer>();
                        CubeRigidbody = FreeCube.AddComponent<Rigidbody>();

                        CubeRigidbody.isKinematic = true;

                        PlayerCamera.transform.parent = FreeCube.transform;
                        Component.Destroy(PlayerCamera.GetComponent<VLB.Samples.FreeCameraController>());
                        Component.Destroy(PlayerCamera.GetComponent<ThirdPersonCameraController>());
                        Component.Destroy(PlayerCamera.GetComponent<ValveCamera>());

                        CameraAnchor = new GameObject().transform;
                        CameraAnchor.transform.parent = PlayerHead;
                        CameraAnchor.localPosition = (Vector3.right * xOffset) + (Vector3.up * yOffset) + (Vector3.forward * zOffset);
                        CameraAnchor.localEulerAngles = new Vector3(0, 0, 0);

                        CameraTransform = FreeCube.transform;
                        CameraComponent = PlayerCamera.GetComponent<Camera>();
                        CameraComponent.transform.localEulerAngles = Vector3.zero;
                        CameraComponent.transform.localPosition = Vector3.zero;
                        CameraComponent.nearClipPlane = 0.3f;

                        Transform geo = SceneObjects[o].transform.Find("[SkeletonRig (GameWorld Brett)]/Brett@neutral/geoGrp");

                        geo.Find("brett_face").gameObject.SetActive(true);
                        geo.Find("brett_hairCap").gameObject.SetActive(true);
                        geo.Find("brett_hairCards").gameObject.SetActive(true);
                        geo.Find("brett_shadowCast_face").gameObject.SetActive(false);
                        geo.Find("brett_shadowCast_hair").gameObject.SetActive(false);

                        CommonGlobals.CameraInstance = this;
                        MelonModLogger.Log("Instance: Created Free Camera, press G to control it");
                    }
                }
            }
            else //Just in case something happens
                MelonModLogger.LogError("Error: A FreeCamera instance already exists!");
        }

        // The Update function
        public void CameraUpdate()
        {
            if (CameraTransform)
            {
                // Controls
                if (CommonGlobals.GetInputLock() == Enums.InputLock.CameraControl)
                {
                    if (Input.GetKeyDown(KeyCode.H))
                        EnablePilot();

                    if (Input.GetKeyDown(KeyCode.F))
                        EnableFollow();

                    if (Input.GetKeyDown(KeyCode.T))
                        EnableThirdPerson();

                    if (Input.GetKeyDown(KeyCode.J))
                        EnableFirstPerson();

                    if (Input.GetKeyDown(KeyCode.G))
                        RecenterOnTarget();

                    if (Input.GetKeyDown(KeyCode.U))
                        EnableTurnTabling();

                    if (Input.GetKeyDown(KeyCode.B))
                        EnablePhysics();

                    // Modifiers
                    if (Input.GetKeyDown(KeyCode.R))
                        EnableModFOV();

                    if (Input.GetKeyDown(KeyCode.Y))
                        EnableModSpeed();

                    // FOV mod
                    if (isManipulatingFOV)
                    {
                        FOV += Input.GetAxisRaw("Mouse ScrollWheel") * 5;
                        CameraComponent.orthographicSize += Input.GetAxisRaw("Mouse ScrollWheel") * 5;
                    }

                    // Speed mod
                    if (isManipulatingSpeed)
                    {
                        SpeedMultiplier += Input.GetAxisRaw("Mouse ScrollWheel") * 5;
                        SpeedMultiplier = Mathf.Clamp(SpeedMultiplier, 0, int.MaxValue); // I honestly don't care for speed limits
                    }

                    // XYZ Offsets for the free cam
                    if (isThirdPerson)
                    {
                        if (Input.GetKeyDown(KeyCode.B))
                        {
                            axisValue++;

                            if (axisValue >= 4)
                                axisValue = 1;

                            MovingAxis = (Enums.Axes)axisValue;
                            MelonModLogger.Log("Free Camera: Offsetting Axis " + Enums.FormalName_Axes[(int)MovingAxis]);
                        }

                        switch (MovingAxis)
                        {
                            default:
                                break;

                            case Enums.Axes.X:
                                xOffset += Input.GetAxisRaw("Mouse ScrollWheel") * 1;
                                break;

                            case Enums.Axes.Y:
                                yOffset += Input.GetAxisRaw("Mouse ScrollWheel") * 1;
                                break;

                            case Enums.Axes.Z:
                                zOffset += Input.GetAxisRaw("Mouse ScrollWheel") * 1;
                                break;
                        }
                    }

                    // Extras
                    if (Input.GetKeyDown(KeyCode.O))
                        CameraComponent.orthographic = !CameraComponent.orthographic;

                    if (Input.GetKeyDown(KeyCode.P))
                        TogglePostProcessing();

                    // Output the current Speed and FOV
                    if (Input.GetKeyDown(KeyCode.M))
                    {
                        if (isManipulatingSpeed)
                            MelonModLogger.Log("Free Camera: Modifying Speed Multiplier Value");

                        if (isManipulatingFOV)
                            MelonModLogger.Log("Free Camera: Modifying FOV Value");

                        if (isThirdPerson)
                            MelonModLogger.Log("Free Camera: Offsetting " + Enums.FormalName_Axes[(int)MovingAxis]);

                        MelonModLogger.Log("Free Camera: Speed Multiplier = " + SpeedMultiplier.ToString());
                        MelonModLogger.Log("Free Camera: FOV Multiplier = " + FOV.ToString());
                        MelonModLogger.Log("Free Camera: X Offset = " + xOffset.ToString());
                        MelonModLogger.Log("Free Camera: Y Offset = " + yOffset.ToString());
                        MelonModLogger.Log("Free Camera: Z Offset = " + zOffset.ToString());
                    }
                }
                else
                    isPiloting = false;

                // Locks the cursor for piloting
                if (isPiloting)
                {
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                }
                else
                {
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }

                // Redirects
                if (isPiloting)
                    PilotCamera();

                if (isThirdPerson)
                    ThirdPerson();

                if (isFollowing)
                    LookAtTarget();

                if (isFirstPerson)
                    FirstPerson();

                if (isTurnTabling)
                    TurnTable();

                // Sync the FOV if it is out of sync
                if (FOV != CameraComponent.fieldOfView)
                    CameraComponent.fieldOfView = FOV;
            }
            else
                CommonGlobals.CameraInstance = null;
        }

        #region CAMERA MODES
        // WASD + Mouse piloting
        void PilotCamera()
        {
            if (CameraTransform)
            {
                float horizontal = Input.GetAxisRaw("Horizontal");
                float vertical = Input.GetAxisRaw("Vertical");
                float up = 0;

                up = Input.GetKey(KeyCode.C) ? 1 : 0;
                up = Input.GetKey(KeyCode.V) ? -1 : 0;

                Vector3 forwardVec = (Vector3.forward * vertical * SpeedMultiplier) * Time.deltaTime;
                Vector3 rightVec = (Vector3.right * horizontal * SpeedMultiplier) * Time.deltaTime;
                Vector3 upVec = (Vector3.up * up * SpeedMultiplier) * Time.deltaTime;

                CameraTransform.Translate(forwardVec + rightVec + upVec);

                CameraTransform.eulerAngles = new Vector3(CameraTransform.localEulerAngles.x, CameraTransform.localEulerAngles.y, 0);
                CameraTransform.eulerAngles += new Vector3(-Input.GetAxisRaw("Mouse Y"), Input.GetAxisRaw("Mouse X"), 0);
            }
        }

        // Faces the camera towards the target
        void LookAtTarget()
        {
            if (CameraTransform)
            {
                CameraComponent.transform.localEulerAngles = Vector3.zero;
                CameraTransform.LookAt(CameraFollowTarget.position);
            }
        }

        // Moves the camera behind the head, in a TPP fashion
        void ThirdPerson()
        {
            if (CameraTransform)
            {
                CameraAnchor.localPosition = (Vector3.right * xOffset) + (Vector3.up * yOffset) + (Vector3.forward * zOffset);
                CameraTransform.position = CameraAnchor.position;
                CameraTransform.rotation = CameraAnchor.rotation;
            }
        }

        // Moves the camera with the player's head like normal
        void FirstPerson()
        {
            if (CameraTransform)
            {
                CameraTransform.position = PlayerHead.position;
                CameraTransform.rotation = PlayerHead.rotation;
            }
        }

        // Recenters the camera on its target
        void RecenterOnTarget()
        {
            if (CameraTransform)
            {
                CameraTransform.position = CameraFollowTarget.position;
                CameraTransform.rotation = CameraFollowTarget.rotation;
            }
        }

        // Turn table centering
        void TurnTable()
        {
            if (CommonGlobals.TurnTableInstance != null)
            {
                CameraTransform.transform.position = CommonGlobals.TurnTableInstance.CameraSlot.position;
                CameraTransform.transform.rotation = CommonGlobals.TurnTableInstance.CameraSlot.rotation;
            }
            else
            {
                ResetState();
                MelonModLogger.Log("Error: No turn table");
            }
        }
        #endregion

        #region ENABLES
        // Enable the Pilot mode
        public void EnablePilot()
        {
            CameraComponent.transform.localEulerAngles = Vector3.zero;
            ResetState();
            isPiloting = true;
        }

        // Enable the Follow mode
        public void EnableFollow()
        {
            ResetState();
            isFollowing = true;
        }

        // Enable Third Person
        public void EnableThirdPerson()
        {
            ResetState();
            isThirdPerson = true;
        }

        // Enable Head Camera
        public void EnableFirstPerson()
        {
            ResetState();
            CubeRenderer.enabled = false;
            isFirstPerson = true;
        }

        // Enable Turn Tabling
        public void EnableTurnTabling()
        {
            ResetState();
            isTurnTabling = true;
        }

        // Enable Physics mode
        public void EnablePhysics()
        {
            ResetState();
            CubeRigidbody.isKinematic = false;
            isPhysics = true;
        }
        #endregion

        #region MODIFIERS
        // FOV modifier
        public void EnableModFOV()
        {
            isManipulatingSpeed = false;
            isManipulatingFOV = true;
            MelonModLogger.Log("Free Camera: Scrolling will now change FOV");
        }

        // Speed modifier
        public void EnableModSpeed()
        {
            isManipulatingSpeed = true;
            isManipulatingFOV = false;
            MelonModLogger.Log("Free Camera: Scrolling will now change speed");
        }
        #endregion

        // Toggle Post Processing
        public void TogglePostProcessing()
        {
            PostProcessLayer postLayer = CameraComponent.GetComponent<PostProcessLayer>();
            PostProcessVolume postVolume = CameraComponent.GetComponent<PostProcessVolume>();

            isPostProcessingEnabled = !isPostProcessingEnabled;

            if (postLayer)
                postLayer.enabled = isPostProcessingEnabled;

            if (postVolume)
                postVolume.enabled = isPostProcessingEnabled;

            string state = isPostProcessingEnabled ? "On" : "Off";

            MelonModLogger.Log("Free Camera: Post Processing " + state);
        }

        // General function for resetting all bools
        void ResetState()
        {
            isPiloting = false;
            isFollowing = false;
            isThirdPerson = false;
            isFirstPerson = false;
            isTurnTabling = false;
            isPhysics = false;

            CubeRenderer.enabled = true;
            CubeCollider.enabled = true;
            CubeRigidbody.isKinematic = true;
        }
    }
}