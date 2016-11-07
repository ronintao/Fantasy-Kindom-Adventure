这个工具类似于安卓中的 R.java
将资源配置 CSV ，生成成对应的索引 id
配置 XML 的写法应该如下：
[TypeName].csv:
key,value

对应会生成的索引为：
public static class [TypeName] {
    public static string key = value;
}

由于自用，为简单记，需要将这个csv 放在 Assets\RoninUtils\ResouceIndex\Resouces\ResouceIndexCsv 路径下

依赖 RoninUtils.Helper