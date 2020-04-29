﻿using zCubed.Globals;
using MelonLoader;

namespace zCubed.Misc
{
    public static class ControlDocumentation
    {
        public static void ListCurrentControls()
        {
            MelonModLogger.Log("Controls: (Universal) Tab = List Mode's Controls");
            switch (CommonGlobals.GetInputLock())
            {
                default:
                    break;

                case Enums.InputLock.Root:
                    MelonModLogger.Log("Controls: G = Create / Take control of Free Camera");
                    MelonModLogger.Log("Controls: F = Fun Stuff");
                    MelonModLogger.Log("Controls: T = Tools");
                    break;

                case Enums.InputLock.CameraControl:
                    MelonModLogger.Log("Controls: E = Exit Mode");
                    MelonModLogger.Log("Controls: F = Face Player Mode");
                    MelonModLogger.Log("Controls: T = Third Person");
                    MelonModLogger.Log("Controls: H = Pilot Mode");
                    MelonModLogger.Log("Controls: J = First Person");
                    MelonModLogger.Log("Controls: U = TurnTable Mode");
                    MelonModLogger.Log("Controls: B = Physics Mode");
                    MelonModLogger.Log("Controls: G = Recenter On Player");
                    MelonModLogger.Log("Controls: L = Toggle HoloHead Visibility");
                    MelonModLogger.Log("Controls: R = Toggle FOV Modifier");
                    MelonModLogger.Log("Controls: Y = Toggle Speed Modifier");
                    MelonModLogger.Log("Controls: P = Toggle Post Processing");
                    MelonModLogger.Log("Controls: WS = Pilot Mode Forward / Back");
                    MelonModLogger.Log("Controls: AD = Pilot Mode Left / Right");
                    MelonModLogger.Log("Controls: Scroll Wheel = Offset Active Modifier");
                    MelonModLogger.Log("Controls: M = List Modifiers");
                    MelonModLogger.Log("Controls: (Third Person) B = Switch Axis Offset");
                    break;

                case Enums.InputLock.Fun:
                    MelonModLogger.Log("Controls: E = Exit Mode");
                    MelonModLogger.Log("Controls: B = Black Hole Spawn / Remove");
                    MelonModLogger.Log("Controls: C = Gravity Cube Spawn / Remove");
                    MelonModLogger.Log("Controls: N = Chroma Screen Spawn / Remove");
                    MelonModLogger.Log("Controls: M = Chroma Screen Flip Color");
                    MelonModLogger.Log("Controls: V = TurnTable Spawn / Remove");
                    MelonModLogger.Log("Controls: 0 = Toggle Zero G / Revert Gravity");
                    MelonModLogger.Log("Controls: Q / W = Incremenet / Decrement Gravity Scale");
                    MelonModLogger.Log("Controls: A / D = Increment / Decrement Time Scale");
                    MelonModLogger.Log("Controls: Z / X = Increment / Decrement Light Modifier (WIP)");
                    MelonModLogger.Log("Controls: R = Reset Values To Defaults");
                    MelonModLogger.Log("Controls: T = Output Values");
                    break;

                case Enums.InputLock.Tools:
                    MelonModLogger.Log("Controls: E = Exit Mode");
                    MelonModLogger.Log("Controls: Q = Output Object Instance IDs (Entire AssetDatabase, WILL FREEZE YOUR GAME FOR A BIT)");
                    MelonModLogger.Log("Controls: W = Strip Scene's Materials");
                    MelonModLogger.Log("Controls: A = List Entire Scene (GameObjects, Children, and Components, WILL FREEZE YOUR GAME FOR A BIT)");
                    MelonModLogger.Log("Controls: S = List Scene's Object Instance IDs (WILL FREEZE YOUR GAME FOR A BIT)");
                    break;
            }
        }
    }
}