// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("tgv1jyDtmp56nHt4GWqbqWwXP16BaG7mHewGH+Q3++VdNTUuGaU/mVeSf8HpA+WZEgVEXYguq3Gu1ZPUbs/xrz0G8DaKE0iGWGC/OAXfk0GPTGNDJeCFhVZcrAaCmYbPIfIrbmGKOzdGXPUwad+9fJnDlUBWH9i34PPFqDYiRkMhim08dnW27KeMyhU6ube4iDq5sro6ubm4eHUu1HTpDRu0x0AkioTfq+Clg6NuIy0sp8ykiDq5moi1vrGSPvA+T7W5ubm9uLtHOpdY6Gy2jOCKsxNwCmt2v9cSz/fgahv3fSOwCE76hNpqc22VgfQA947C6iTzPsBBu35/AsiStmaSXO2LeJdcAL1/d5NO+DrpV+fFDWLgWA2WpfOxrgT307q7ubi5");
        private static int[] order = new int[] { 3,6,4,4,7,13,9,9,10,9,12,13,13,13,14 };
        private static int key = 184;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
