using System;

namespace Nacre.Renderer
{
    public class Nacre
    {
        public static readonly string Tag = "Nacre";

        public static bool IsInitialized { get; private set; }

        public static void Init()
        {
            if (IsInitialized) return;
            IsInitialized = true;
        }
    }
}
