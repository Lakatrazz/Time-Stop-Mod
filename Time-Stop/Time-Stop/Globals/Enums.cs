namespace Lakatrazz.Globals
{
    public static class Enums
    {
        // So the policy here is that you define the Enum, and then its FormalName below it
        public enum InputLock
        {
            Root = 0,
            CameraControl = 1,
            Fun = 2,
            Tools = 3
        }

        public static string[] FormalName_InputLock = new string[]
        {
            "Normal",
            "Camera",
            "Fun",
            "Tools"
        };

        public enum Axes
        {
            Null = 0,
            X = 1,
            Y = 2,
            Z = 3
        }

        public static string[] FormalName_Axes = new string[]
        {
            "None",
            "X",
            "Y",
            "Z"
        };
    }
}
