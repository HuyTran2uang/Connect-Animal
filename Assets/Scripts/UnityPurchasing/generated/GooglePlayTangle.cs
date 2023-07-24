// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("z4fJvF63sIrCOW1JDQ3D/AsX6DcF6ByrT4tzu46wKDoO/BpN73AFINShjDF8BAol5lnRdMtN/nenV3IF8eFeraIMAkQy6+/uoR33gnjB6euvHZ69r5KZlrUZ1xlokp6enpqfnCNcf3KLelc0+gXsS3g+CRD2vaKB0nfNtyZRNibSE9nBCUln4QtsZA7sQSJTSjgIHHTC5KH2Syml/w4MaBQBBrb1G896ahGTsN0Fuv/GXtk94jla1Db5A5Wb2jcsqeD2eMa4CebD7IXjH2arGgiM2xT5kVAoziQgCc2I1gmPGJKlej6X61uvxV1JG82vHZ6Qn68dnpWdHZ6enyNWvR7g1nLVfUtCGapdsDPJUhydYBkQLKV4LeuNSFlrJKpPrp2cnp+e");
        private static int[] order = new int[] { 5,6,3,7,5,8,11,9,13,13,10,13,13,13,14 };
        private static int key = 159;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
