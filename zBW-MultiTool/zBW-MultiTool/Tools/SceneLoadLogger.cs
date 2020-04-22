using MelonLoader;
using UnityEngine;
using UnityEngine.SceneManagement;
using zCubed.Globals;

namespace zCubed.Tools
{
    public static class SceneLoadLogger
    {
        public static void OnLoad()
        {
            // Get the current no. of scenes loaded
            int SceneCount = SceneManager.sceneCount;

            // Outputs the loaded scenes
            for (int s = 0; s < SceneCount; s++)
            {
                Scene scene = SceneManager.GetSceneAt(s);

                if (scene.name != "NEW OBJECT COLLECTOR")
                {
                    CommonGlobals.SceneName = scene.name;
                    MelonModLogger.Log("Scene Loaded: " + scene.name);
                }
            }
        }
    }
}
