namespace ExRScriptCppNativeRMK
{
    /// <summary>
    /// 文本转换类
    /// </summary>
    public static class TextConverter
    {
        /// <summary>
        /// 读入文本并转换参数方法1
        /// </summary>
        public static string ReadTextAndConvertParamsMethod1(string inputText)
        {
            if (!string.IsNullOrEmpty(inputText))
            {
                return inputText
                    .Replace(Constants.RUNPATH_PLACEHOLDER,
                        System.AppDomain.CurrentDomain.BaseDirectory)
                    .Replace(Constants.EXENAME_PLACEHOLDER, "kzxdexename");
            }
            return "0";
        }

        /// <summary>
        /// 读入文本并转换参数方法2
        /// </summary>
        public static string ReadTextAndConvertParamsMethod2(string inputText)
        {
            if (!string.IsNullOrEmpty(inputText))
            {
                return inputText
                    .Replace(Constants.RUNPATH_PLACEHOLDER, "runpath")
                    .Replace(Constants.EXENAME_PLACEHOLDER, "exeName");
            }
            return string.Empty;
        }
    }
}