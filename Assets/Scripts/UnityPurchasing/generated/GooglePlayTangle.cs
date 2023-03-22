// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("cQR/lCdC2So8IiJTo7gBpAP7kumDa+SqjEGtshvxfw7ABzGA47nDjPSqhcKBeTujOwPPK8Th659z9TaCM9TQ0coQRu7i0kNteBCO6HNxHM4kBhPG7XRSQIk6ySXI0oQ8BaibDlB35bwS4PpwPlHdxdW4i+CtsfcpWbAB0L+cGiBM2ZJTSwyogP+irU4A67j7oX6kOpXusJK3qbL0Uy1M31nz/+lqX3SlILKftdyDEv1MVg0EJn4s50EyC0B8TtLXBxsURliQk+UrqKapmSuoo6srqKipEzFW840Eon53mr40ymGJmADq6UTM61TkKM5BmSuoi5mkr6CDL+EvXqSoqKisqaosINwYfYUEAE2gVXfYmt0QS8Nvsuic9MfLJq+YYKuqqKmo");
        private static int[] order = new int[] { 11,5,2,13,8,9,8,10,10,12,12,13,13,13,14 };
        private static int key = 169;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
