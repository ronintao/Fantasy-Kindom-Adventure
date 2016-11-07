using System.Collections.Generic;
using System.Linq;

namespace RoninUtils.Helper {

    public static class CSVReader {


        /// <summary>
        /// 解析 CSV 文件，将其解析为 Dictionary 数组，每行为该数组的一个 Dictionary 元素，其 key 是 tag （来自于tag行），value 来自本行对应值，如果该格未填则为 null
        /// </summary>
        public static Dictionary<string, string>[] ParseWithTag(string csvText, int tagLineIndex = 0, int dataBeginLineIndex = 1) {
            // 先将文本解析为行数组
            List<string[]> parsedList = Parse(csvText);

            // 获取 Tag 行
            string [] tagLine = parsedList[tagLineIndex];
            int      tagCount = tagLine.Length;

            // 将 data 填充到结果中
            Dictionary<string, string>[] parsedDic = new Dictionary<string, string>[parsedList.Count - dataBeginLineIndex];

            for (int lineIndex = dataBeginLineIndex; lineIndex < parsedList.Count; lineIndex ++) {
                Dictionary<string, string> parsedLine = new Dictionary<string, string>();
                parsedDic[lineIndex - dataBeginLineIndex] = parsedLine;

                string [] rawLine = parsedList[lineIndex];
                for (int i = 0; i < tagCount; i ++) {
                    parsedLine.Add(tagLine[i], rawLine.SafeIndex(i, null));
                }
            }

            return parsedDic;
        }


        /// <summary>
        /// 解析 CSV 文件，将其解析为行数组
        /// </summary>
        public static List<string[]> Parse(string csvText) {
            List<string[]> parsedLine = new List<string[]>();

            csvText = csvText.Replace("\r\n", "\n").Replace("\r", "\n").Trim();

            string[] lines = csvText.Split("\n"[0]);
            for (int lineIndex = 0; lineIndex < lines.Length; lineIndex++ ) {
                string[] row = SplitCsvLine( lines[lineIndex].Trim() );
                parsedLine.Add(row);
            }

            return parsedLine;
        }


        // splits a CSV row
        public static string[] SplitCsvLine (string line) {
            return (from System.Text.RegularExpressions.Match m in System.Text.RegularExpressions.Regex.Matches(line,
                @"(((?<x>(?=[,\r\n]+))|""(?<x>([^""]|"""")+)""|(?<x>[^,\r\n]+)),?)",
                System.Text.RegularExpressions.RegexOptions.ExplicitCapture)
                    select m.Groups[1].Value).ToArray();
        }
    }

}
