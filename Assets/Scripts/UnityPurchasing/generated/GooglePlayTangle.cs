// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("rQcLHZ6rgFHURmtBKHfmCbii+fB3nxBeeLVZRu8Fi/o088V0F003eNDy5zIZgKa0fc490TwmcMjxXG/6AF5xNnWNz1fP9zvfMBUfa4cBwnb0H0wPVYpQzmEaRGZDXUYAp9m4K4Xwi2DTti3eyNbWp1dM9VD3D2Yd0orYE7XG/7SIuiYj8+/gsqxkZxGtRPUkS2ju1LgtZqe/+Fx0C1ZZuoqDbkrAPpV9bPQeHbA4H6AQ3Dq1xyAkJT7kshoWJreZjOR6HIeF6Dpt31x/bVBbVHfbFduqUFxcXFhdXtjUKOyJcfD0uVShgyxuKeS/N5tGpIMRSOYUDoTKpSkxIUx/FFlFA93fXFJdbd9cV1/fXFxd58WiB3nwVhxoADM/0ltslF9eXF1c");
        private static int[] order = new int[] { 7,5,8,8,10,11,12,10,8,13,10,12,13,13,14 };
        private static int key = 93;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
