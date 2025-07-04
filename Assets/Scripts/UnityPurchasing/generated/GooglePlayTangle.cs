// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("dg9Da6Vyv0HAOv/+g0kTN+cT3WzgC7q2x910sehePP0YQhTB155ZNu9OcC68h3G3C5LJB9nhPrmEXhLADs3iwqRhBATX3S2HAxgHTqBzqu+aNUbBpQsFXiphJAIi76KsrSZNJQr5Ft2BPP72Es95u2jWZkSM42HZuzg2OQm7ODM7uzg4Ofn0r1X1aIw3inQOoWwbH/sd+vmY6xoo7Za+38a7Ftlp7TcNYQsykvGL6vc+VpNO1hP+QGiCZBiThMXcCa8q8C9UElVhckQpt6PHwqAL7L339DdtJg1LlHZh65p2/KIxic97BVvr8uwUAHWBCbs4Gwk0PzATv3G/zjQ4ODg8OToA6e9nnG2HnmW2emTctLSvmCS+GIwXJHIwL4V2Ujs6ODk4");
        private static int[] order = new int[] { 11,13,2,7,10,8,13,7,12,10,10,12,12,13,14 };
        private static int key = 57;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
