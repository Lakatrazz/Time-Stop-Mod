using UnhollowerBaseLib;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using System.Reflection;
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

        GameObject HoloHead = null;
        Il2CppReferenceArray<MeshRenderer> HoloRenderers = null;

        MeshRenderer CameraMesh = null;

        public bool isPiloting = false;
        public bool isFollowing = false;
        public bool isThirdPerson = false;
        public bool isFirstPerson = false;

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
        bool isHoloHeadEnabled = true;

        // Constructor
        public FreeCamera()
        {
            if (CommonGlobals.CameraInstance == null)
            {
                Tools.RecursiveFunctions.SceneList("FollowCamera");

                if (RecursiveGlobals.lastFound)
                {
                    if (RecursiveGlobals.lastFound.name == "FollowCamera")
                    {
                        CameraFollowTarget = RecursiveGlobals.lastFound.transform.parent;

                        GameObject GoProCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        GoProCube.transform.position = RecursiveGlobals.lastFound.transform.position;
                        GoProCube.transform.localScale = Vector3.one / 10f;
                        GoProCube.name = "Free Camera";

                        CameraMesh = GoProCube.GetComponent<MeshRenderer>();

                        PlayerHead = RecursiveGlobals.lastFound.transform.root.transform.Find("[PhysicsRig]").Find("Head").Find("offset");

                        if (!PlayerHead)
                            return;

                        RecursiveGlobals.lastFound.transform.parent = GoProCube.transform;
                        Component.Destroy(RecursiveGlobals.lastFound.GetComponent<VLB.Samples.FreeCameraController>());
                        Component.Destroy(RecursiveGlobals.lastFound.GetComponent<ThirdPersonCameraController>());
                        Component.Destroy(RecursiveGlobals.lastFound.GetComponent<ValveCamera>());
                                            
                        MelonModLogger.Log("Instance: Created Free Camera, press G to control it");

                        CameraAnchor = new GameObject().transform;
                        CameraAnchor.transform.parent = PlayerHead;
                        CameraAnchor.localPosition = (Vector3.right * xOffset) + (Vector3.up * yOffset) + (Vector3.forward * zOffset);
                        CameraAnchor.localEulerAngles = new Vector3(0, 0, 0);

                        CameraTransform = GoProCube.transform;
                        CameraComponent = RecursiveGlobals.lastFound.GetComponent<Camera>();
                        CameraComponent.transform.localEulerAngles = Vector3.zero;

                        InstanceGlobals.AttemptToCacheAssets();
                        if (InstanceGlobals.holoHead)
                        {
                            HoloHead = GameObject.Instantiate(InstanceGlobals.holoHead);

                            HoloHead.transform.parent = PlayerHead;
                            HoloHead.transform.localPosition = Vector3.zero;
                            HoloHead.transform.localScale = Vector3.one;
                            HoloHead.transform.localRotation = Quaternion.identity;

                            HoloHead.transform.Find("geoGrp").transform.localPosition = Vector3.up * -1.65f;
                            HoloHead.transform.Find("SHJntGrp").transform.localPosition = Vector3.up * -1.65f;
                        }

                        CommonGlobals.CameraInstance = this;
                    }
                }
            }
            else //Just in case something happens
                MelonModLogger.LogError("Error: A FreeCamera instance already exists!");
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

        // The Update function
        public void CameraUpdate()
        {
            if (CameraTransform)
            {
                // Controls
                if (CommonGlobals.GetInputLock() == Enums.InputLock.CameraControl)
                {
                    // Lift the lock if pressed again
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        isPiloting = false;
                        CommonGlobals.SetInputLock(Enums.InputLock.Normal);
                    }

                    if (Input.GetKeyDown(KeyCode.H))
                        EnablePilot();

                    if (Input.GetKeyDown(KeyCode.F))
                        EnableFollow();

                    if (Input.GetKeyDown(KeyCode.T))
                        EnableThirdPerson();

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
                    {
                        CameraComponent.orthographic = !CameraComponent.orthographic;
                    }

                    if (Input.GetKeyDown(KeyCode.P))
                        TogglePostProcessing();

                    if (Input.GetKeyDown(KeyCode.L))
                        ToggleHoloHead();

                    // Output the current Speed and FOV
                    if (Input.GetKeyDown(KeyCode.Tab))
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

                // Sync the FOV if it is out of sync
                if (FOV != CameraComponent.fieldOfView)
                    CameraComponent.fieldOfView = FOV;
            }
            else
                CommonGlobals.CameraInstance = null;
        }

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
            if (CameraMesh)
                CameraMesh.enabled = false;

            isFirstPerson = true;
        }

        // Enable Modifiers
        public void EnableModFOV()
        {
            isManipulatingSpeed = false;
            isManipulatingFOV = true;
            MelonModLogger.Log("Free Camera: Scrolling will now change FOV");
        }

        public void EnableModSpeed()
        {
            isManipulatingSpeed = true;
            isManipulatingFOV = false;
            MelonModLogger.Log("Free Camera: Scrolling will now change speed");
        }

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

        // Toggle the HoloHead's visibility
        public void ToggleHoloHead()
        {
            /*
            if (HoloRenderers != null)
            {
                isHoloHeadEnabled = !isHoloHeadEnabled;

                for (int r = 0; r < HoloRenderers.Length; r++)
                {
                    HoloRenderers[r].enabled = isHoloHeadEnabled;
                }
            }*/
        }

        // General function for resetting all bools
        void ResetState()
        {
            isPiloting = false;
            isFollowing = false;
            isThirdPerson = false;
            isFirstPerson = false;

            if (CameraMesh)
                CameraMesh.enabled = true;
        }
    }
}