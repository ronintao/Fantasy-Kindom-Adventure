namespace RoninUtils.ResouceIndex {

    public static class ResouceIndexConfig {

        /**
         * 是否自动生成 ResouceIndex.cs，如果为 true，在修改对应的 csv 的时候，就会触发文件的生成
         */
        public const bool AUTO_GENERATE = true;


        /**
         * 如果 CSV 文件名称以此开头，则表示这个文件中的 value 是 int 类型，否则默认是 String 类型
         */
        public const string CSV_NAME_INT_VALUE = "[INT]";


        /**
         * CSV 文件中 Key 对应的 Tag
         */
        public const string CSV_KEY = "KEY";


        /**
         * CSV 文件中 value 对应的 Tag
         */
        public const string CSV_VALUE = "VALUE";


        /**
         * 放置 CSV 文件的文件夹路径
         */
        public const string CSV_FOLDER_PATH = @"Assets/RoninUtils/ResouceIndex/ResouceIndexCsv";


        /**
         * 生成的 ResouceIndex 文件的路径，注意不用写 Assets
         */
        public const string CODE_FOLDER_PATH = @"/RoninUtils/ResouceIndex/GeneratedIndexScript";

    }

}

