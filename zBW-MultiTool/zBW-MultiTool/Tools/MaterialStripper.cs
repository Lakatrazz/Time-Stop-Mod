using MelonLoader;
using UnityEngine;
using UnhollowerBaseLib;

namespace zCubed.Tools
{
    public static class MaterialStripper
    {
        public static void StripMaterials()
        {
            MelonModLogger.Log("Tools: Stripping scene of all current materials");

            GameObject tempMaterialObj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            Material whiteMaterial = tempMaterialObj.GetComponent<MeshRenderer>().material;

            whiteMaterial.color = Color.white;

            GameObject.Destroy(tempMaterialObj);

            Il2CppReferenceArray<MeshRenderer> meshRenderers = Object.FindObjectsOfType<MeshRenderer>();
            Il2CppReferenceArray<MeshRenderer> skinnedRenderers = Object.FindObjectsOfType<MeshRenderer>();

            for (int r = 0; r <= meshRenderers.Length - 1; r++)
            {
                if (meshRenderers[r])
                {
                    for (int m = 0; m <= meshRenderers[r].materials.Count - 1; m++)
                        skinnedRenderers[r].materials[m] = whiteMaterial;
                }
            }

            for (int s = 0; s <= skinnedRenderers.Length - 1; s++)
            {
                if (skinnedRenderers[s])
                {
                    for (int m = 0; m <= skinnedRenderers[s].materials.Count - 1; m++)
                        skinnedRenderers[s].materials[m] = whiteMaterial;
                }
            }

            MelonModLogger.Log("Tools: Done stripping scene");
        }
    }
}
