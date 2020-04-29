using UnityEngine;
using MelonLoader;
using zCubed.Globals;
using zCubed.Tools;
using UnhollowerBaseLib;

namespace zCubed.Features
{
    public class BlackHole
    {
        GameObject Orb;

        // Constructor
        public BlackHole()
        {
            if (CommonGlobals.BlackHoleInstance == null)
            {
                Orb = GameObject.CreatePrimitive(PrimitiveType.Sphere);

                Orb.GetComponent<MeshRenderer>().material.color = Color.black;
                Orb.GetComponent<Collider>().enabled = false;

                RecursiveFunctions.SceneList("[RigManager (Default Brett)]");

                if (RecursiveGlobals.lastFound)
                {
                    if (RecursiveGlobals.lastFound.name == "[RigManager (Default Brett)]")
                    {
                        Transform Brett = RecursiveGlobals.lastFound.transform.Find("[PhysicsRig]").Find("Head").Find("offset");

                        if (!Brett)
                            return;

                        Orb.transform.position = Brett.transform.position + Vector3.up * 2;

                        MelonModLogger.Log("Instance: Black Hole created");
                        CommonGlobals.BlackHoleInstance = this;
                    }
                }
            }
            else
                MelonModLogger.LogError("Error: Only one Black Hole can exist at once!");
        }

        public void Delete()
        {
            GameObject.Destroy(Orb);
            CommonGlobals.BlackHoleInstance = null;
            MelonModLogger.Log("Instance: Black Hole destroyed");
        }

        public void Update()
        {
            if (Orb)
            {
                Il2CppReferenceArray<Collider> Objects = Physics.OverlapSphere(Orb.transform.position, 25f);

                if (Objects != null)
                {
                    for (int o = 0; o <= Objects.Count - 1; o++)
                    {
                        Rigidbody TargetRB = Objects[o].GetComponent<Rigidbody>();
                        Vector3 direction = Orb.transform.position - Objects[o].transform.position;

                        if (TargetRB)
                            TargetRB.AddForce(direction.normalized * 50f);
                    }
                }
            }
        }
    }
}
