using MelonLoader;
using UnityEngine;
using System.IO;

namespace zCubed.Tools
{
    // Credit: AnTDD, thank you for being a genius and coming up with the idea

    public static class ObjectIDLogger
    {
        public static void OutputEntireAssetDatabase()
        {
            const int targetNumber = 1000000;
            string[] foundRegisters = new string[targetNumber / 2];

            MelonModLogger.Log("Asset Database: Listing all object instance IDs");

            int s = 0;
            for (int o = 2; o <= targetNumber - 1; o += 2)
            {
                Object testObject = Object.FindObjectFromInstanceID(o);

                // Screw switch statements, they didn't work...
                // Is cast statements, didn't work...

                if (testObject)
                {
                    System.Type testType = testObject.GetType();
                    foundRegisters[s] = "[ID " + o.ToString() + "]: ";

                    GameObject gameObject_cast = testObject.TryCast<GameObject>();
                    AudioClip audioClip_cast = testObject.TryCast<AudioClip>();
                    UnityEngine.Video.VideoClip videoClip_cast = testObject.TryCast<UnityEngine.Video.VideoClip>();
                    Shader shader_cast = testObject.TryCast<Shader>();
                    TextAsset textAsset_cast = testObject.TryCast<TextAsset>();
                    Material material_cast = testObject.TryCast<Material>();
                    MonoBehaviour behaviour_cast = testObject.TryCast<MonoBehaviour>();
                    ScriptableObject scriptableObject_cast = testObject.TryCast<ScriptableObject>();
                    Mesh mesh_cast = testObject.TryCast<Mesh>();
                    Texture2D texture2d_cast = testObject.TryCast<Texture2D>();
                    Texture3D texture3d_cast = testObject.TryCast<Texture3D>();
                    RenderSettings renderSettings_cast = testObject.TryCast<RenderSettings>();
                    AnimationClip animationClip_cast = testObject.TryCast<AnimationClip>();

                    if (gameObject_cast)
                        foundRegisters[s] += "[GameObject] " + gameObject_cast.name;
                    else if (audioClip_cast)
                        foundRegisters[s] += "[AudioClip] " + testObject.name;
                    else if (videoClip_cast)
                        foundRegisters[s] += "[VideoClip] " + testObject.name;
                    else if (shader_cast)
                        foundRegisters[s] += "[Shader] " + testObject.name;
                    else if (textAsset_cast)
                        foundRegisters[s] += "[Text Asset] " + testObject.name;
                    else if (material_cast)
                        foundRegisters[s] += "[Material] " + testObject.name;
                    else if (behaviour_cast)
                        foundRegisters[s] += "[MonoBehaviour] " + testObject.name;
                    else if (scriptableObject_cast)
                        foundRegisters[s] += "[ScriptableObject] " + testObject.name;
                    else if (mesh_cast)
                        foundRegisters[s] += "[Mesh] " + testObject.name;
                    else if (texture2d_cast)
                        foundRegisters[s] += "[Texture2D] " + testObject.name;
                    else if (texture3d_cast)
                        foundRegisters[s] += "[Texture3D] " + testObject.name;
                    else if (renderSettings_cast)
                        foundRegisters[s] += "[RenderSettings] " + testObject.name;
                    else if (audioClip_cast)
                        foundRegisters[s] += "[AudioClip] " + testObject.name;
                    else
                        foundRegisters[s] += "[Null or Unknown] " + testObject.name;

                    foundRegisters[s] += "\n";
                    s++;
                }
            }

            MelonModLogger.Log("Asset Database: Done listing all object instance IDs");
            FileSystem.WritePlainText("/AssetDatabase_Listing", foundRegisters);
        }
    }
}
