using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using RoninUtils.Helper;

namespace RoninUtils.Helper {
    public static class StringHelper {


        public const int SecretString = 0;


        #region String Builder Enhancement

        public static void Clear(this StringBuilder sb) {
            sb.Length   = 0;
            sb.Capacity = 0;
        }

        #endregion



        #region IP To String

        /// <summary>
        /// 将 ip 地址转换成 string
        /// </summary>
        public static string LongToIP(this long ip) {
            StringBuilder sb = TmpValueHelper.GetStringBuilder();
            sb.Append( ip & 0xFF ).Append(".");
            sb.Append((ip >> 8) & 0xFF).Append(".");
            sb.Append((ip >> 16) & 0xFF).Append(".");
            sb.Append((ip >> 24) & 0xFF);
            return sb.ToString();
        }

        #endregion


        #region String To Color

        /// <summary>
        /// 将 0xRRGGBB 或是 RRGGBB 这样的字符串转换成 Color，如果转换失败，则返回默认值白色
        /// </summary>
        public static Color ToColor (this string str) {
            return ToColor(str, Color.white);
        }

        /// <summary>
        /// 将 0xRRGGBB 或是 RRGGBB 这样的字符串转换成 Color，如果转换失败，则返回默认值
        /// </summary>
        public static Color ToColor (this string str, Color defaultValue) {
            if (str != null && str.ToLower().StartsWith("0x"))
                str = str.Substring(2);

            int color = 0;
            if (Int32.TryParse(str, System.Globalization.NumberStyles.HexNumber, null, out color)) {
                int r = ((color & 0xff0000) >> 16);
                int g = ((color & 0x00ff00) >> 8);
                int b =   color & 0x0000ff;
                return new Color(r / 255f, g / 255f, b / 255f);
            }
            return defaultValue;
        }

        #endregion




        #region String To Other Basic Type

        /**
         * return true if success
         */
        private delegate bool ParseString<T>(string str, out T value);
        private static T ToValue<T>(string str, T defaultValue, ParseString<T> parseAction) {
            T value = defaultValue;
            if (string.IsNullOrEmpty(str) || !parseAction(str, out value)) {
                return defaultValue;
            }
            return value;
        }

        /// <summary>
        /// 将 bool 字符串转换成 bool
        /// 可以正常解析的 bool 字符串（忽略中括号）有 [false] [ trUe  ]
        /// 不可以正常解析的会返回默认值，包括 [False1] [ tr ue]
        /// </summary>
        public static bool ToBool (this string str, bool defaultValue = false) {
            if (string.IsNullOrEmpty(str))
                return defaultValue;
            try {
                return Convert.ToBoolean(str);
            } catch (Exception) {
                return defaultValue;
            }
        }

        /// <summary>
        /// 将浮点数的字符串转换成浮点数
        /// </summary>
        public static float ToFloat (this string str, float defaultValue = 0) {
            return ToValue(str, defaultValue, float.TryParse);
        }

        /// <summary>
        /// 将 unsigned short 字符串转换成 ushort
        /// </summary>
        public static ushort ToUShort (this string str, ushort defaultValue = 0) {
            return ToValue(str, defaultValue, ushort.TryParse);
        }

        /// <summary>
        /// 将 int 字符串转换成 int
        /// </summary>
        public static int ToInt (this string str, int defaultValue = 0) {
            return ToValue(str, defaultValue, Int32.TryParse);
        }

        /// <summary>
        /// 将字符串 0xFF11 或是 FF11 转换成普通的 int
        /// </summary>
        public static int HexToInt (this string str, int defaultValue = 0) {
            if (str != null && str.ToLower().StartsWith("0x"))
                str = str.Substring(2);
            return string.IsNullOrEmpty(str) ? defaultValue : Convert.ToInt32(str, 16);
        }

        /// <summary>
        /// 将 unsigned long 字符串转换成 ulong
        /// </summary>
        public static ulong ToULong (this string str, ulong defaultValue = 0) {
            return ToValue(str, defaultValue, UInt64.TryParse);
        }

        #endregion



        #region String To Array

        private delegate T ParseArrayItem<T>(string item, T defaultValue);
        private static T[] ToArrayInternal<T>(string str, char splitChar, T defaultValue, ParseArrayItem<T> parseAction) {
            if (string.IsNullOrEmpty(str))
                return new T[0];

            string[] strArray = str.Split(splitChar);
            T [] items = new T[strArray.Length];
            for (int i = 0; i < strArray.Length; i++) {
                items[i] = parseAction(strArray[i], defaultValue);
            }
            return items;
        }


        /// <summary>
        /// 将用 splitChar 分割的 int 数组字符串，如 1#2#3 ，转换成 int 数组
        /// </summary>
        public static int[] ToIntArray (this string str, char splitChar = '#', int defaultValue = 0) {
            return ToArrayInternal(str, splitChar, defaultValue, ToInt);
        }

        /// <summary>
        /// 将用 splitChar 分割的 hex int 数组字符串，如 0x11#0x22 ， 转换成 int 数组
        /// </summary>
        public static int[] HexToIntArray (this string str, char splitChar = '#', int defaultValue = 0) {
            return ToArrayInternal(str, splitChar, defaultValue, HexToInt);
        }

        /// <summary>
        /// 将用 splitChar 分割的 unsigned long 数组字符串，如 1#2 ， 转换成 ulong 数组
        /// </summary>
        public static ulong[] ToUlongArray (this string str, char splitChar = '#', ulong defaultValue = 0) {
            return ToArrayInternal(str, splitChar, defaultValue, ToULong);
        }


        #endregion



        #region Array To String

        /// <summary>
        /// 将数组转化为字符串，并用 sliptChar 分割
        /// </summary>
        public static string ArrayToString<T> (this T[] array, char splitChar = '#') {
            if (array == null)
                return string.Empty;

            StringBuilder ret = TmpValueHelper.GetStringBuilder();
            array.ValueForeach(item => ret.Append(item.ToString()).Append(splitChar) );
            return ret.ToString().TrimEnd(splitChar);
        }


        #endregion




        //        public static string Utf8BytesToString (byte[] utf8Bytes, bool checkEmoji = false) {
        //            if (utf8Bytes == null) {
        //                return null;
        //            }


        //            string ret = string.Empty;
        //            byte[] checkU8byte;
        //            if (CheckAndFilterUtf8bytes(utf8Bytes, checkEmoji, out checkU8byte)) {
        //                if (checkU8byte != null) {
        //                    ret = System.Text.Encoding.UTF8.GetString(checkU8byte);
        //                }
        //            }
        //            /*
        //            string ret = System.Text.Encoding.UTF8.GetString(utf8Bytes);
        //            if (checkEmoji)
        //            {
        //                ret = CleanString(ret);
        //            }
        //            */
        //            return ret;
        //        }

        //        public static byte[] StringToUtf8Bytes (string str) {
        //            byte[] utf8Bytes = System.Text.Encoding.UTF8.GetBytes(str);
        //            return utf8Bytes;
        //        }

        //        public static string HexToString (byte[] bytes) {
        //            string s = string.Empty;
        //            if (bytes != null) {
        //                for (int i = 0; i < bytes.Length; i++) {
        //                    s += bytes[i].ToString("X2");
        //                }
        //            }
        //            return s;
        //        }


        //        public static T StringToEnum<T> (string str)
        //            where T : struct, IConvertible {
        //            T output = default(T);
        //            object temp = null;

        //            try {
        //                temp = Enum.Parse(typeof(T), str);
        //                output = (T)temp;
        //            } catch {
        //                Logger.GeneralWarning("fail to parese string({0}) to T, use default value {1}", str, output.ToString());
        //            }

        //            return output;
        //        }

        //        public static bool TryParseEnum<T> (string str, out T output)
        //        where T : struct, IConvertible {
        //            output = default(T);
        //            object temp = null;

        //            try {
        //                temp = Enum.Parse(typeof(T), str);
        //                output = (T)temp;
        //            } catch {
        //                return false;
        //            }

        //            return true;
        //        }

        //        const string PortraitUrlPrefix = "http://q.qlogo.cn/qqapp/";
        //        const string PortraitUrlSuffix40 = "/40";
        //        const string PortraitUrlSuffix100 = "/100";


        //        /// <summary>
        //        ///  CF只允许16进制值,从28到7e的字符
        //        ///  http://ascii.911cha.com/
        //        /// </summary>
        //        static readonly string[] RANDOM_CHARS = {
        //        //"A","B","C","D","E","F","G","H","I","J","K","L","M","N","O","P","Q","R","S","T","U","V","W","X","Y","Z",
        //        //"a","b","c","d","e","f","g","h","i","j","k","l","m","n","o","p","q","r","s","t","u","v","w","x","y","z",
        //        //"0","1","2","3","4","5","6","7","8","9",
        //        "-","=","`","\\",";",",",".","/", //,"[","]" 去掉BBCode中括号
        //        "@","^","*","(",")","_","+","~","{","}",":","<",">","?",
        //                                      };
        //        /// <summary>
        //        /// 返回指定的随机字符
        //        /// </summary>
        //        /// <param name="length">随机字符个数</param>
        //        /// <param name="seed">随机数种子，如果在同一侦里连续调用函数，需要用不同的值才会有不同的结果</param>
        //        /// <returns></returns>
        //        public static string GetRandomChars (int length, int seed = 0) {
        //            string result = null;
        //            int randomCharsCount = RANDOM_CHARS.Length;
        //            seed += (int)DateTimeUtil.GetCurrentTimeStamp();
        //            for (int i = 0; i < length; i++) {
        //                Random random = new Random(seed + i);
        //                int randomIndex = random.Next(randomCharsCount);
        //                string randomString = RANDOM_CHARS[randomIndex];
        //                result += randomString;
        //            }
        //            return result;
        //        }

        //        /// <summary>
        //        /// 获取字符串字节数(UTF-8一个中文算3个字符)
        //        /// </summary>
        //        /// <param name="text"></param>
        //        /// <returns></returns>
        //        public static int GetByteCount (string text) {
        //            return Encoding.UTF8.GetByteCount(text);
        //        }





        //        private static readonly Regex regexEmoji = new Regex(@"\ud83c[\udf00-\udfff]|\ud83d[\udc00-\ude4f]|\ud83d[\ude80-\udeff]");

        //        private static readonly Regex regexOutOfMem = new Regex("[\udc00-\uffff]");

        //        /// <summary>
        //        /// 清除一些特殊字符
        //        /// </summary>
        //        /// <param name="str"></param>
        //        /// <returns></returns>
        //        public static string CleanString (string str) {
        //            string result = string.Empty;

        //            if (string.IsNullOrEmpty(str)) {
        //                return result;
        //            }

        //            result = regexEmoji.Replace(str, string.Empty);
        //            result = regexOutOfMem.Replace(result, string.Empty);
        //            return result;
        //        }

        //        public static bool IsValidChar (char inputChar) {
        //            return inputChar < 0xdc00;
        //        }

        //        public static bool IsEmojiFirstChar (char inputChar) {
        //            bool result = false;
        //            if (
        //                inputChar == 0xd83c ||
        //                inputChar == 0xd83d ||
        //                inputChar > 0xdc00
        //                ) {
        //                result = true;
        //            }
        //            return result;
        //        }

        //        public static bool IsEmojiSecondChar (char inputChar) {
        //            bool result = false;
        //            if (
        //                (inputChar >= 0xdf00 && inputChar <= 0xdfff) ||
        //                (inputChar >= 0xdc00 && inputChar <= 0xde4f) ||
        //                (inputChar >= 0xde80 && inputChar <= 0xdeff)
        //                ) {
        //                result = true;
        //            }
        //            return result;
        //        }



        //        public static string GetStackTrace () {
        //            System.Diagnostics.StackTrace ss = new System.Diagnostics.StackTrace(true);
        //            StringBuilder stringBuilder = new StringBuilder();
        //            for (int i = 1, count = ss.FrameCount; i < count; i++) {
        //                String flName = ss.GetFrame(i).GetFileName();
        //                int lineNo = ss.GetFrame(i).GetFileLineNumber();
        //                String methodName = ss.GetFrame(i).GetMethod().ToString();
        //                stringBuilder.AppendFormat("[{3}] {2}@file:{0}:{1}\r\n", flName, lineNo, methodName, i);
        //            }
        //            return stringBuilder.ToString();
        //        }

        //        /// <summary>
        //        ///  版本号比较
        //        /// </summary>
        //        /// <param name="A"></param>
        //        /// <param name="B"></param>
        //        /// <returns>A < B : -1, A > B : 1, A = B : 0, 出错：-2</returns>
        //        public static int VersionCompare_A_GreaterThen_B (string A, string B) {
        //            if (string.IsNullOrEmpty(A) || string.IsNullOrEmpty(B)) {
        //                return -2;
        //            }

        //            var partsA = A.Split('.');
        //            var partsB = B.Split('.');

        //            if (partsA.Length != 4 || partsB.Length != 4) {
        //                Logger.GeneralError("Version string format not expected: string A: {0}, string B: {1}", A, B);
        //                return -2;
        //            }

        //            for (int i = 0; i < 4; ++i) {
        //                int noA = int.Parse(partsA[i]);
        //                int noB = int.Parse(partsB[i]);
        //                if (noA < noB)
        //                    return -1;
        //                else if (noA > noB)
        //                    return 1;
        //            }
        //            return 0;
        //        }

        //        public static bool ContainsIgnoreCase (string inSrc, string inCheck, StringComparison inComp) {
        //            return inSrc.IndexOf(inCheck, inComp) >= 0;
        //        }

        //        const string FORMATSTRING_OPENBRACE = "{";
        //        const string FORMATSTRING_OPENBRACEDOUBLIE = "{{";
        //        const string FORMATSTRING_CLOSEBRACE = "}";
        //        const string FORMATSTRING_CLOSEBRACEDOUBLIE = "}}";

        //        /// <summary>
        //        /// 去除“｛”、“｝”等符号，避免Format时出错
        //        /// </summary>
        //        /// <param name="source"></param>
        //        /// <returns></returns>
        //        public static string ClearFormatString (string source) {
        //            string result = source;
        //            if (!string.IsNullOrEmpty(result)) {
        //                result = result.Replace(FORMATSTRING_OPENBRACE, FORMATSTRING_OPENBRACEDOUBLIE);
        //                result = result.Replace(FORMATSTRING_CLOSEBRACE, FORMATSTRING_CLOSEBRACEDOUBLIE);
        //            }
        //            return result;
        //        }

        //        /// <summary>
        //        /// 检查是否有UTF-16等特殊字符
        //        /// </summary>
        //        /// <param name="source"></param>
        //        /// <returns></returns>
        //        public static bool CheckStringValidate (string source) {
        //            bool result = true;
        //            try {
        //                Debug.Log(source);
        //                result = true;
        //            } catch (Exception ex) {
        //                Logger.GeneralWarning("check string validate fail\r\n{0}", ex.StackTrace);
        //                result = false;
        //            }
        //            return result;
        //        }


        //        //utf8 code page head
        //        static Byte[] cov_map_from_utf8 = {
        //                0x00,       // 0xxxxxxx
        //				0xc0,       // 110xxxxx
        //				0xe0,       // 1110xxxx
        //				0xf0,       // 11110xxx
        //				0xf8,       // 111110xx
        //				0xfc        // 1111110x
        //			};

        //        //https://en.wikipedia.org/wiki/Emoji
        //        //range U2600–U27BF
        //        static int minEmoji1 = 0xE29880;        //0x2600 -->11100010 10 011000 10 000000 //0x2700 --> 11100010	10 011100	10 000000
        //        static int maxEmoji1 = 0xE29EBF;        //0x27BF --> 11100010   10 011110	10 111111

        //        //range U2194–U2199
        //        static int minEmoji2 = 0xE28694;                    //0x2194 -->1110 0010   10 000110   10 010100
        //        static int maxEmoji2 = 0xE28699;                //0x2199 -->1110 0010   10 000110   10 011001

        //        //range U21A9 - U21AA
        //        static int minEmoji3 = 0xE286A9;                    //0x21A9 -->1110 0010   10 000110   10 010100
        //        static int maxEmoji3 = 0xE286AA;                    //0x21AA -->1110 0010   10 000110   10 101010

        //        //range U21A9 - U21AA
        //        static int minEmoji4 = 0xE286A9;
        //        static int maxEmoji4 = 0xE286AA;

        //        //range U21A9 - U21AA
        //        static int minEmoji5 = 0xE2AC85;
        //        static int maxEmoji5 = 0xE2AC87;

        //        static int minEmoji6 = 0xE28FA9;
        //        static int maxEmoji6 = 0xE28FB3;

        //        static int minEmoji7 = 0xE28FB8;
        //        static int maxEmoji7 = 0xE28FBA;

        //        static int[] ThreeEmojiValue = { 0xE380B0, 0xE380BD, 0xE29382, 0xE38A97, 0xE38A99, 0xE280BC, 0xE28189,
        //                                   0xE296AA,0xE296AB,0xE296B6,0xE297B0,0xE297BB,0xE297BE,0xE284A2,0xE284B9,
        //                                     0xE2AC9B,  0xE2AC9C,  0xE2AD90, 0xE2AD95, 0xE28C9A,0xE28C9B,0xE28CA8,0xE28F8F,
        //                                       0xE2A4B4,0xE2A4B5 };

        //        static int[] TwoByteEmojiValue = { 0xC2A9, 0xC2AE };
        //        static bool CheckAndFilterUtf8bytes (Byte[] utf8Bytes, bool checkEmoji, out Byte[] CheckValue) {
        //            CheckValue = null;
        //            ArrayList target = new ArrayList();
        //            int count = utf8Bytes.Length;
        //            for (int j = 0; j < count;) {
        //                int size = 0;
        //                if (!CheckUtf8CodeLenth(utf8Bytes[j], out size)) {
        //                    //invalid utf8 stream
        //                    return false;
        //                }
        //                if (checkEmoji) {
        //                    if (size < 4 && count >= j + size) {
        //                        bool isEmoji = false;
        //                        if (size == 3) {
        //                            //check emoji range
        //                            int checkvalue = utf8Bytes[j] << 16 + utf8Bytes[j + 1] << 8 + utf8Bytes[j + 2];
        //                            if (isInThreeBytesEmojiBlock(checkvalue)) {
        //                                isEmoji = true;
        //                            }
        //                        }
        //                        if (size == 2) {
        //                            int checkvalue = utf8Bytes[j] << 8 + utf8Bytes[j + 1];
        //                            if (isInTwoBytesEmojiBlock(checkvalue)) {
        //                                isEmoji = true;
        //                            }
        //                        }
        //                        if (!isEmoji) {
        //                            for (int k = 0; k < size; ++k) {
        //                                target.Add(utf8Bytes[k + j]);
        //                            }
        //                        }
        //                    } else {
        //                        //ignore the utf8 bytes code which length beyond 3;
        //                    }
        //                }
        //                j += size;
        //            }
        //            if (!checkEmoji) {
        //                CheckValue = utf8Bytes;
        //            } else {
        //                CheckValue = (Byte[])target.ToArray(typeof(Byte));
        //            }

        //            return true;
        //        }

        //        static bool isInThreeBytesEmojiBlock (int checkvalue) {
        //            for (int i = 0; i < ThreeEmojiValue.Length; ++i) {
        //                if (checkvalue == ThreeEmojiValue[i]) {
        //                    return true;
        //                }
        //            }
        //            if ((checkvalue >= minEmoji1 && checkvalue <= maxEmoji1) || (checkvalue >= minEmoji2 && checkvalue <= maxEmoji2)
        //                || (checkvalue >= minEmoji3 && checkvalue <= maxEmoji3) || (checkvalue >= minEmoji4 && checkvalue <= maxEmoji4)
        //                || (checkvalue >= minEmoji5 && checkvalue <= maxEmoji5) || (checkvalue >= minEmoji6 && checkvalue <= maxEmoji6)
        //                || (checkvalue >= minEmoji7 && checkvalue <= maxEmoji7)
        //                ) {
        //                return true;
        //            }
        //            return false;
        //        }

        //        static bool isInTwoBytesEmojiBlock (int checkvalue) {
        //            for (int i = 0; i < TwoByteEmojiValue.Length; ++i) {
        //                if (checkvalue == TwoByteEmojiValue[i]) {
        //                    return true;
        //                }
        //            }
        //            return false;
        //        }

        //        static bool CheckUtf8CodeLenth (Byte from, out int size) {
        //            size = 6;
        //            for (int i = size - 1; i >= 0; --i) {
        //                Byte c = cov_map_from_utf8[i];
        //                int ct = from & c;
        //                if (c == ct) {
        //                    return true;
        //                }
        //                --size;
        //            }
        //            return false;
        //        }


    }
}
