// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("7LTmLYv4wYq2hBgdzdHejJJaWS/hYmxjU+FiaWHhYmJj2fucOUfOaJq9L3bYKjC69JsXDx9yQSpnez3j7szZDCe+mIpD8APvAhhO9s9iUcS7zrVe7YgT4Pbo6JlpcstuyTFYI1PhYkFTbmVqSeUr5ZRuYmJiZmNgSaEuYEaLZ3jRO7XECs37SilzCUbm6hbSt0/Oyodqn70SUBfagQmleLS9UHT+AKtDUsogI44GIZ4u4gSLk3rLGnVW0OqGE1iZgcZiSjVoZ4STOTUjoJW+b+p4VX8WSdg3hpzHzsohcjFrtG7wXyR6WH1jeD6Z54YVPmBPCEuz8WnxyQXhDishVbk//Ej5HhobANqMJCgYiaey2kQiubvWBCJWPg0B7GVSqmFgYmNi");
        private static int[] order = new int[] { 12,1,9,8,11,12,12,12,8,11,12,12,12,13,14 };
        private static int key = 99;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
