using ExRScriptCppNativeRMK.kernel;
using System;
using System.IO;

namespace ExRScriptCppNativeRMK
{
    class Program
    {
        static void Main(string[] args)
        {
            // 设置命令行参数
            GlobalVariables.CommandLineArgs = args;

            // 设置DLL加载目录
            string libPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Constants.LIB_DIRECTORY);
            Environment.SetEnvironmentVariable("PATH",
                Environment.GetEnvironmentVariable("PATH") + ";" + libPath);

            Console.WriteLine();

            // 检查核心库
            CheckKernelLibrary();

            // 初始化系统
            InitializeSystem();

            // 处理命令行参数
            ProcessCommandLineArgs();

            // 如果是调试版，等待退出
#if DEBUG
            LanguageManager.QXT1();
            Console.ReadKey();
#endif
        }

        /// <summary>
        /// 检查核心库
        /// </summary>
        private static void CheckKernelLibrary()
        {
            string krnlnPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                Constants.LIB_DIRECTORY, Constants.KRNLN_DLL);

            if (File.Exists(krnlnPath))
            {
                // 测试DLL
                if (ERSConverter.HelloDLLTest() != 1)
                {
                    LanguageManager.NFK2();
                }
            }
            else
            {
                LanguageManager.NFK1();
            }
        }

        /// <summary>
        /// 初始化系统
        /// </summary>
        private static void InitializeSystem()
        {
            // 获取系统语言
            GlobalVariables.SystemLanguage = LanguageManager.GetSystemLanguage();

            // 读取代码框架文件
            string codeHeaderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                Constants.CODE_HEADER_FILE);
            string codeFooterPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                Constants.CODE_FOOTER_FILE);

            if (File.Exists(codeHeaderPath))
            {
                GlobalVariables.IostreamCodeFramework = File.ReadAllText(codeHeaderPath);
            }

            if (File.Exists(codeFooterPath))
            {
                GlobalVariables.IostreamCodeFramework1 = File.ReadAllText(codeFooterPath);
            }
        }

        /// <summary>
        /// 处理命令行参数
        /// </summary>
        private static void ProcessCommandLineArgs()
        {
            if (GlobalVariables.CommandLineArgs.Length == 0)
                return;

            string command = GlobalVariables.CommandLineArgs[0].ToLower();

            switch (command)
            {
                case Constants.COMMAND_RUN:
                    CompilerManager.RunCompileMethod();
                    break;

                case Constants.COMMAND_COMPILE:
                    CompilerManager.CompileMethod();
                    break;

                case Constants.COMMAND_HCOMPILE:
                    CompilerManager.HCompileMethod();
                    break;

                case Constants.COMMAND_ABOUT:
                    ShowAboutInfo();
                    break;

                default:
                    Console.WriteLine($"未知命令: {command}");
                    break;
            }
        }

        /// <summary>
        /// 显示关于信息
        /// </summary>
        private static void ShowAboutInfo()
        {
            Console.WriteLine(GlobalVariables.VersionInfo);
        }
    }
}