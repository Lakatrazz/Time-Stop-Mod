using UnityEngine;
using MelonLoader;
using zCubed.Globals;

namespace zCubed.Features
{
    public class GravityCube
    {
        GameObject CubePrimitive = null;

        public GravityCube()
        {
            if (CommonGlobals.GravCubeInstance == null)
            {
                CubePrimitive = GameObject.CreatePrimitive(PrimitiveType.Cube);
                CubePrimitive.transform.localScale = Vector3.one / 5f;
                CubePrimitive.transform.position = Vector3.up;
                CubePrimitive.AddComponent<Rigidbody>();
                CubePrimitive.GetComponent<MeshRenderer>().material.color = Color.red;
                CubePrimitive.name = "Gravity Cube";

                CommonGlobals.GravCubeInstance = this;

                MelonModLogger.Log("Instance: Created Gravity Cube");
            }
            else
                MelonModLogger.LogError("Error: No more than 1 Gravity Cube can exist at once!");
        }

        public void Delete()
        {
            CommonGlobals.GravCubeInstance = null;
            MelonModLogger.Log("Instance: Destroyed Gravity Cube");
            GameObject.Destroy(CubePrimitive);
        }

        public void SetGravity()
        {
            Physics.gravity = CubePrimitive.transform.up * CommonGlobals.GravityScale;
        }
    }
}
