using UnityEngine;
using MelonLoader;
using zCubed.Globals;

namespace zCubed.Features
{
    public class FreeCamera
    {
        Transform CameraFollowTarget = null;
        Camera CameraComponent = null;
        Transform CameraTransform = null;

        public bool isPiloting = false;
        public bool isFollowing = false;

        bool isManipulatingSpeed = false;
        bool isManipulatingFOV = false;

        float FOV = 70f;
        float SpeedMultiplier = 8f;

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

                        RecursiveGlobals.lastFound.transform.parent = GoProCube.transform;
                        Component.Destroy(RecursiveGlobals.lastFound.GetComponent<VLB.Samples.FreeCameraController>());
                        Component.Destroy(RecursiveGlobals.lastFound.GetComponent<ThirdPersonCameraController>());
                        Component.Destroy(RecursiveGlobals.lastFound.GetComponent<ValveCamera>());
                         
                        MelonModLogger.Log("Instance: Created Free Camera");

                        CameraTransform = GoProCube.transform;
                        CameraComponent = RecursiveGlobals.lastFound.GetComponent<Camera>();
                        CameraComponent.transform.localEulerAngles = Vector3.zero;

                        CommonGlobals.CameraInstance = this;
                    }
                }
            }
            else //Just in case something happens
                MelonModLogger.LogError("Error: A FreeCamera instance already exists!");
        }

        // Recenters the camera on its target
        public void RecenterOnTarget()
        {
            CameraTransform.position = CameraFollowTarget.position;
            CameraTransform.rotation = CameraFollowTarget.rotation;
        }

        // WASD + Mouse piloting
        public void PilotCamera()
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

            // Toggle fov changer
            if (Input.GetKeyDown(KeyCode.F))
            {
                isManipulatingSpeed = false;
                isManipulatingFOV = !isManipulatingFOV;
            }

            // Toggle speed changer
            if (Input.GetKeyDown(KeyCode.E))
            {
                isManipulatingFOV = false;
                isManipulatingSpeed = !isManipulatingSpeed;
            }

            // Fov change based on scrollwheel
            if (isManipulatingFOV)
            {
                isManipulatingSpeed = false; // Just in case

                FOV += Input.GetAxisRaw("Mouse ScrollWheel") * 5;
                CameraComponent.orthographicSize += Input.GetAxisRaw("Mouse ScrollWheel") * 5;
            }

            // Speed change based on scrollwheel
            if (isManipulatingSpeed)
            {
                isManipulatingFOV = false; // Just in case

                SpeedMultiplier += Input.GetAxisRaw("Mouse ScrollWheel") * 5;
                SpeedMultiplier = Mathf.Clamp(SpeedMultiplier, 0, int.MaxValue); // I honestly don't care for speed limits
            }

            // Orthographic for fun
            if (Input.GetKeyDown(KeyCode.O))
            {
                CameraComponent.orthographic = !CameraComponent.orthographic;
            }

            // Output the current Speed and FOV
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                if (isManipulatingSpeed)
                    MelonModLogger.Log("State: Modifying Speed Multiplier Value");

                if (isManipulatingFOV)
                    MelonModLogger.Log("State: Modifying FOV Value");

                MelonModLogger.Log("Value: Speed Multiplier = " + SpeedMultiplier.ToString());
                MelonModLogger.Log("Value: FOV Multiplier = " + FOV.ToString());
            }

            if (FOV != CameraComponent.fieldOfView)
                CameraComponent.fieldOfView = FOV;
        }

        // Faces the camera towards the target
        public void LookAtTarget()
        {
            CameraComponent.transform.localEulerAngles = Vector3.zero;
            CameraTransform.LookAt(CameraFollowTarget.position);
        }

        // Toggle the Pilot mode or Follow mode
        public void TogglePilot()
        {
            if (isFollowing)
                isFollowing = false;

            isPiloting = !isPiloting;

            if (isPiloting)
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                CommonGlobals.inputLock = Enums.InputLock.PilotingCamera;
            }
            else
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                CommonGlobals.inputLock = Enums.InputLock.Normal;
            }
        }
        public void ToggleFollow()
        {
            if (isPiloting)
                isPiloting = false;

            isFollowing = !isFollowing;
        }
    }
}