using Microsoft.VisualBasic;

namespace ExRScriptCppNativeRMK.kernel
{
    /// <summary>
    /// 全局变量类
    /// </summary>
    public static class GlobalVariables
    {
        // 命令相关变量
        public static string[] CommandContent { get; set; } = new string[0];
        public static int CommandCount { get; set; } = 0;
        public static string[] CommandLineArgs { get; set; } = new string[0];

        // 系统相关变量
        public static int SystemLanguage { get; set; } = Constants.ENGLISH;
        public static string VersionInfo { get; set; } = Constants.VERSION_INFO;

        // 代码框架
        public static string IostreamCodeFramework { get; set; } = string.Empty;
        public static string IostreamCodeFramework1 { get; set; } = string.Empty;

        // 编译器路径
        public static string MinGW64Path { get; set; } = string.Empty;
    }
}