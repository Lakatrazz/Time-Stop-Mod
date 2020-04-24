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
            whiteMaterial.mainTexture = null;

            GameObject.Destroy(tempMaterialObj);

            Il2CppReferenceArray<MeshRenderer> meshRenderers = Object.FindObjectsOfType<MeshRenderer>();
            Il2CppReferenceArray<SkinnedMeshRenderer> skinnedRenderers = Object.FindObjectsOfType<SkinnedMeshRenderer>();

            for (int r = 0; r < meshRenderers.Length; r++)
            {
                if (meshRenderers[r])
                {
                    meshRenderers[r].material = whiteMaterial;

                    for (int m = 0; m < meshRenderers[r].materials.Count; m++)
                        meshRenderers[r].materials[m] = whiteMaterial;
                }
            }

            for (int s = 0; s < skinnedRenderers.Length; s++)
            {
                if (skinnedRenderers[s])
                {
                    MelonModLogger.Log(skinnedRenderers[s].transform.name);
                    skinnedRenderers[s].material = whiteMaterial;
                    skinnedRenderers[s].sharedMaterial = whiteMaterial;

                    for (int m = 0; m < skinnedRenderers[s].materials.Count; m++)
                        skinnedRenderers[s].materials[m] = whiteMaterial;

                    for (int sm = 0; sm < skinnedRenderers[s].sharedMaterials.Count; sm++)
                        skinnedRenderers[s].sharedMaterials[sm] = whiteMaterial;
                }
            }

            MelonModLogger.Log("Tools: Done stripping scene");
        }
    }
}
