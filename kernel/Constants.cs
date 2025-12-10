using System;

namespace ExRScriptCppNativeRMK.kernel
{
    /// <summary>
    /// 常量定义类
    /// </summary>
    public static class Constants
    {
        // 系统语言常量
        public const int SIMPLIFIED_CHINESE = 1;
        public const int ENGLISH = 2;
        public const int JAPANESE = 3;

        // 文件扩展名
        public const string ERS_EXTENSION = ".ers";
        public const string EXE_EXTENSION = ".exe";
        public const string CPP_EXTENSION = ".cpp";

        // 路径常量
        public const string RUNPATH_PLACEHOLDER = "{runpath}";
        public const string EXENAME_PLACEHOLDER = "{exeName}";

        // 目录名
        public const string LIB_DIRECTORY = "lib";
        public const string INCLUDE_DIRECTORY = "include";
        public const string DEBUG_DIRECTORY = "ExRScriptCppDebug";

        // 文件名
        public const string KRNLN_DLL = "krnln.dll";
        public const string CODE_HEADER_FILE = "codeheader.exh";
        public const string CODE_FOOTER_FILE = "codefooter.exh";

        // 临时文件名
        public const string TEMP_CPP_FILE = "ExRTemp.cpp";
        public const string CODE_I_FILE = "Code.i";
        public const string CODE_S_FILE = "Code.s";
        public const string CODE_O_FILE = "Code.o";

        // 编译器命令
        public const string GCC_COMPILER = "g++";
        public const string CMD_EXECUTOR = "cmd.exe";

        // 版本信息
        public const string VERSION_INFO = "ExRScript Native .NET Windows RMK v0.1.2";

        // 命令常量
        public const string COMMAND_RUN = "run";
        public const string COMMAND_COMPILE = "compile";
        public const string COMMAND_HCOMPILE = "hcompile";
        public const string COMMAND_ABOUT = "about";
        public const string COMMAND_STATIC = "-static";
    }
}