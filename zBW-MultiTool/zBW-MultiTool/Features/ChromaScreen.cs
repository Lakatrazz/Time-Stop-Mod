using MelonLoader;
using UnityEngine;
using zCubed.Globals;

namespace zCubed.Features
{
    public class ChromaScreen
    {
        GameObject[] Planes = new GameObject[2];

        Material Green;
        Material Blue;

        bool ColorFlip = false;

        public ChromaScreen()
        {
            if (CommonGlobals.ChromaInstance == null)
            {
                GameObject MaterialTemp = GameObject.CreatePrimitive(PrimitiveType.Cube);

                Green = new Material(MaterialTemp.GetComponent<MeshRenderer>().material);
                Blue = new Material(MaterialTemp.GetComponent<MeshRenderer>().material);

                Green.color = new Color(0, 1, 0);
                Blue.color = new Color(0, 0, 1);

                GameObject.Destroy(MaterialTemp);

                Planes[0] = GameObject.CreatePrimitive(PrimitiveType.Plane);
                Planes[1] = GameObject.CreatePrimitive(PrimitiveType.Plane);


                GameObject Player = GameObject.Find("[RigManager (Default Brett)]");

                Planes[0].GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
                Planes[1].GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

                Planes[0].GetComponent<MeshRenderer>().receiveShadows = false;
                Planes[1].GetComponent<MeshRenderer>().receiveShadows = false;

                Planes[0].GetComponent<MeshRenderer>().material = Green;
                Planes[1].GetComponent<MeshRenderer>().material = Green;

                if (Player)
                {
                    Transform PlayerHead = Player.transform.Find("[PhysicsRig]").Find("Head").Find("offset");

                    if (PlayerHead)
                    {
                        Planes[0].transform.localScale = new Vector3(2, 1, 1);

                        Planes[0].transform.position = PlayerHead.transform.position + PlayerHead.transform.forward;
                        Planes[1].transform.parent = Planes[0].transform;

                        Planes[1].transform.localPosition = new Vector3(0, 5, -5);
                        Planes[1].transform.localEulerAngles = new Vector3(90, 0, 0);
                        Planes[1].transform.parent = null;
                        Planes[1].transform.localScale = new Vector3(2, 1, 1);
                    }
                }

                MelonModLogger.Log("Instance: Chroma Screen created");
                CommonGlobals.ChromaInstance = this;
            }
            else
                MelonModLogger.LogError("Error: Only one Chroma Screen can exist at once!");
        }

        public void Delete()
        {
            GameObject.Destroy(Planes[0]);
            GameObject.Destroy(Planes[1]);

            CommonGlobals.ChromaInstance = null;
        }

        public void FlipColor()
        {
            ColorFlip = !ColorFlip;

            if (ColorFlip)
            {
                Planes[0].GetComponent<MeshRenderer>().material = Blue;
                Planes[1].GetComponent<MeshRenderer>().material = Blue;
            }
            else
            {
                Planes[0].GetComponent<MeshRenderer>().material = Green;
                Planes[1].GetComponent<MeshRenderer>().material = Green;
            }
        }
    }
}
