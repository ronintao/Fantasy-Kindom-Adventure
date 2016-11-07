using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace RoninUtils.Helper {


    /// <summary>
    /// Ronin Framework 要求文件路径 不加 Assets 开头
    /// </summary>
    public static class FileHelper {

        public static string ReadFileRelativePath(string relativePath) {
            return ReadFile(Application.dataPath + "/" + relativePath);
        }


        public static string ReadFile(string filePath) {
            string text;
            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8)) {
                text = streamReader.ReadToEnd();
            }
            return text;
        }


    }
}
