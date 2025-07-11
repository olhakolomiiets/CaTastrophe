// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("oEdDQlmD1X1xQdD+64Mde+Dij12TeCtoMu03qQZ9IwEkOiFnwL7fTGc5FlES6qgwqJBcuFdyeAzgZqUREPh3OR/SPiGIYuydU5SiE3AqUB/t5Aktp1nyGguTeXrXX3jHd7td0rXtv3TSoZjT791BRJSIh9XLAwB24pfsB7TRSrmvsbHAMCuSN5BoAXrD5HYvgXNp463CTlZGKxhzPiJkugq4OxgKNzwzELxyvM03Ozs7Pzo5yiOSQywPibPfSgHA2J87E2wxPt2/s0+L7haXk94zxuRLCU6D2FD8IbeVgFV+58HTGqlatltBF6+WOwidymBsevnM5zazIQwmTxCBbt/Fnpe4OzU6Crg7MDi4Ozs6gKLFYB6XMXsPZ1RYtTwL8zg5Ozo7");
        private static int[] order = new int[] { 13,10,2,5,5,12,11,9,13,12,11,13,12,13,14 };
        private static int key = 58;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
