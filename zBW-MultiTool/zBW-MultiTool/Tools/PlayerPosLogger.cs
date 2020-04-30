using UnityEngine;
using MelonLoader;

namespace zCubed.Tools
{
    public static class PlayerPosLogger
    {
        public static void LogHeadPos()
        {
            GameObject Player = GameObject.Find("[RigManager (Default Brett)]");

            if (Player)
            {
                Transform Head = Player.transform.Find("[PhysicsRig]").Find("Head").Find("offset");

                if (Head)
                    MelonModLogger.Log("Player Head @ " + Head.transform.position.ToString());
            }
        }
    }
}
