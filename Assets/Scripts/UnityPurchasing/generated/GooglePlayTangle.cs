// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("EDIn8tlAZnS9Dv0R/OawCDGcrzpKQ66KAP5Vvaw03t1w+N9g0Bz6dWRD0Ygm1M5ECmXp8eGMv9SZhcMdH5ySna0fnJefH5ycnScFYse5MJYYFOgsSbEwNHmUYUPsrukkf/dbhrdf0J64dZmGL8VLOvQzBbTXjfe4bcfL3V5rQJEUhquB6LcmyXhiOTBFMEugE3btHggWFmeXjDWQN8+m3TTfjM+VSpAOodqEpoOdhsBnGXjrrR+cv62Qm5S3G9UbapCcnJyYnZ7AnrH2tU0Plw83+x/w1d+rR8ECtgfg5OX+JHLa1uZ3WUwkutxHRSj6EkoY03UGP3RIeubjMy8gcmykp9FthDXki6guFHjtpmd/OJy0y5aZetyowPP/EpusVJ+enJ2c");
        private static int[] order = new int[] { 8,3,9,3,6,5,7,11,10,10,10,13,12,13,14 };
        private static int key = 157;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
