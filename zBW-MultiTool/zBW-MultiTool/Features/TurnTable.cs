using MelonLoader;
using UnityEngine;
using zCubed.Globals;
using UnhollowerBaseLib;

namespace zCubed.Features
{
    public class TurnTable
    {
        GameObject Platform = null;
        Transform PlatformSpinner = null;

        public Transform CameraSlot = null;

        public TurnTable()
        {
            if (CommonGlobals.TurnTableInstance == null)
            {
                Platform = GameObject.CreatePrimitive(PrimitiveType.Cube);
                Platform.name = "TurnTable";

                Platform.transform.localScale = new Vector3(0.5f, 0.1f, 0.5f);

                GameObject Player = GameObject.Find("[RigManager (Default Brett)]");

                if (Player)
                {
                    Transform PlayerHead = Player.transform.Find("[PhysicsRig]").Find("Head").Find("offset");

                    if (PlayerHead)
                    {
                        Ray ray = new Ray(PlayerHead.transform.position, PlayerHead.transform.forward);
                        Il2CppStructArray<RaycastHit> hits = Physics.RaycastAll(ray, 10);

                        if (hits.Length > 0)
                            Platform.transform.position = hits[0].point + (Vector3.up / 5);
                    }
                }

                GameObject PlatformLamp = new GameObject();
                PlatformLamp.transform.parent = Platform.transform;
                PlatformLamp.transform.localPosition = Vector3.up * 5;
                Light light = PlatformLamp.AddComponent<Light>();

                light.type = LightType.Spot;
                light.spotAngle = 50f;

                GameObject Spinner = new GameObject();
                PlatformSpinner = Spinner.transform;
                PlatformSpinner.parent = Platform.transform;
                PlatformSpinner.localPosition = Vector3.zero;

                GameObject Slot = new GameObject();
                CameraSlot = Slot.transform;
                CameraSlot.parent = PlatformSpinner;
                CameraSlot.localPosition = (Vector3.back / 2) + (Vector3.up / 2);
                CameraSlot.LookAt(PlatformSpinner);

                CommonGlobals.TurnTableInstance = this;
                MelonModLogger.Log("Instance: Created TurnTable");
            }
            else
                MelonModLogger.LogError("Error: Only one Turn Table can exist at once!");
        }

        public void Delete()
        {
            GameObject.Destroy(Platform);
            CommonGlobals.TurnTableInstance = null;
        }

        public void Update()
        {
            if (Platform)
            {
                PlatformSpinner.Rotate(Vector3.up, 5 * Time.deltaTime);
            }
        }
    }
}
