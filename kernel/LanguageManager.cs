using System;
using System.Globalization;
using System.Windows.Forms;

namespace ExRScriptCppNativeRMK.kernel
{
    /// <summary>
    /// 语言管理类
    /// </summary>
    public static class LanguageManager
    {
        /// <summary>
        /// 获取系统语言
        /// </summary>
        public static int GetSystemLanguage()
        {
            try
            {
                CultureInfo ci = CultureInfo.InstalledUICulture;
                if (ci.Name.StartsWith("zh-CN"))
                {
                    return Constants.SIMPLIFIED_CHINESE;
                }
                else if (ci.Name.StartsWith("ja"))
                {
                    return Constants.JAPANESE;
                }
                else
                {
                    return Constants.ENGLISH;
                }
            }
            catch
            {
                return Constants.ENGLISH;
            }
        }

        /// <summary>
        /// Not Found Kernel 1
        /// </summary>
        public static void NFK1()
        {
            switch (GlobalVariables.SystemLanguage)
            {
                case Constants.SIMPLIFIED_CHINESE:
                    Console.WriteLine("ExRScript: 文件krnln.dll不存在");
                    MessageBox.Show("不能载入系统核心支持库！");
                    break;

                case Constants.ENGLISH:
                    Console.WriteLine("ExRScript: Not Found File \"krnln.dll\"");
                    Console.WriteLine("Failed to load the System Kernel Support Library!");
                    break;

                case Constants.JAPANESE:
                    Console.WriteLine("ExRScript: ファイル krnln.dll が存在しません");
                    Console.WriteLine("システムコアサポートライブラリを読み込めません！");
                    break;
            }
        }

        /// <summary>
        /// Not Found Kernel 2
        /// </summary>
        public static void NFK2()
        {
            switch (GlobalVariables.SystemLanguage)
            {
                case Constants.SIMPLIFIED_CHINESE:
                    Console.WriteLine("不能载入系统核心支持库！");
                    break;

                case Constants.ENGLISH:
                    Console.WriteLine("Failed to load the System Kernel Support Library!");
                    break;

                case Constants.JAPANESE:
                    Console.WriteLine("システムコアサポートライブラリを読み込めません！");
                    break;
            }
        }

        /// <summary>
        /// Debug's Quit or Exit1
        /// </summary>
        public static void QXT1()
        {
            switch (GlobalVariables.SystemLanguage)
            {
                case Constants.SIMPLIFIED_CHINESE:
                    Console.WriteLine("按下回车退出...");
                    break;

                case Constants.ENGLISH:
                    Console.WriteLine("Press Enter to Exit...");
                    break;
            }
        }

        /// <summary>
        /// Compile Name Error
        /// </summary>
        public static void Lang_CNE1()
        {
            switch (GlobalVariables.SystemLanguage)
            {
                case Constants.SIMPLIFIED_CHINESE:
                    Console.WriteLine("ExRScript: 必须要.ers后缀名才能编译运行哦");
                    break;

                case Constants.ENGLISH:
                    Console.WriteLine("ExRScript: Compile need .ers FileName Ext");
                    break;
            }
        }

        /// <summary>
        /// Compiling 1
        /// </summary>
        public static void Lang_CIG1()
        {
            switch (GlobalVariables.SystemLanguage)
            {
                case Constants.SIMPLIFIED_CHINESE:
                    Console.WriteLine("正在统计需要编译的命令");
                    break;

                case Constants.ENGLISH:
                    Console.WriteLine("Counting the commands that need to be compiled");
                    break;
            }
        }

        /// <summary>
        /// Compiling 2
        /// </summary>
        public static void Lang_CIG2()
        {
            switch (GlobalVariables.SystemLanguage)
            {
                case Constants.SIMPLIFIED_CHINESE:
                    Console.WriteLine("正在生成主程序代码");
                    break;

                case Constants.ENGLISH:
                    Console.WriteLine("Generating main program code");
                    break;
            }
        }

        /// <summary>
        /// Compiling 3
        /// </summary>
        public static void Lang_CIG3()
        {
            switch (GlobalVariables.SystemLanguage)
            {
                case Constants.SIMPLIFIED_CHINESE:
                    Console.WriteLine("程序代码编译成功");
                    break;

                case Constants.ENGLISH:
                    Console.WriteLine("Program code compiled successfully");
                    break;
            }
        }

        /// <summary>
        /// Compile g++ Not Found Error
        /// </summary>
        public static void Lang_CGE1()
        {
            switch (GlobalVariables.SystemLanguage)
            {
                case Constants.SIMPLIFIED_CHINESE:
                    Console.WriteLine("ExRScript: 疑似g++未添加到PATH");
                    break;

                case Constants.ENGLISH:
                    Console.WriteLine("ExRScript: Not Found g++");
                    break;
            }
        }

        /// <summary>
        /// Compile Not Found File Error
        /// </summary>
        public static void Lang_CNE2()
        {
            switch (GlobalVariables.SystemLanguage)
            {
                case Constants.SIMPLIFIED_CHINESE:
                    Console.WriteLine("ExRScript: 编译错误，未识别到文件");
                    break;

                case Constants.ENGLISH:
                    Console.WriteLine("ExRScript: Not Found target file.");
                    break;
            }
        }

        /// <summary>
        /// Compile Done
        /// </summary>
        public static void Lang_COD1()
        {
            switch (GlobalVariables.SystemLanguage)
            {
                case Constants.SIMPLIFIED_CHINESE:
                    Console.WriteLine("编译完成");
                    break;

                case Constants.ENGLISH:
                    Console.WriteLine("Compile successfully.");
                    break;
            }
        }

        /// <summary>
        /// Compiling _ But g++
        /// </summary>
        public static void Lang_CIG4()
        {
            switch (GlobalVariables.SystemLanguage)
            {
                case Constants.SIMPLIFIED_CHINESE:
                    Console.WriteLine("正在使用g++编译程序...");
                    break;

                case Constants.ENGLISH:
                    Console.WriteLine("Compiling...");
                    break;
            }
        }

        /// <summary>
        /// HCompiling 0.1
        /// </summary>
        public static void Lang_HCI01()
        {
            switch (GlobalVariables.SystemLanguage)
            {
                case Constants.SIMPLIFIED_CHINESE:
                    Console.WriteLine("正在预处理程序代码");
                    break;

                case Constants.ENGLISH:
                    Console.WriteLine("Preprocessing program code");
                    break;
            }
        }

        /// <summary>
        /// HCompiling 1
        /// </summary>
        public static void Lang_HCI1()
        {
            switch (GlobalVariables.SystemLanguage)
            {
                case Constants.SIMPLIFIED_CHINESE:
                    Console.Write("正在编译程序代码... ");
                    break;

                case Constants.ENGLISH:
                    Console.Write("Compiling program code... ");
                    break;
            }
        }

        /// <summary>
        /// HComiling 2
        /// </summary>
        public static void Lang_HCI2()
        {
            switch (GlobalVariables.SystemLanguage)
            {
                case Constants.SIMPLIFIED_CHINESE:
                    Console.WriteLine("正在进行名称链接...");
                    break;

                case Constants.ENGLISH:
                    Console.WriteLine("Linking \".o\" object file...");
                    break;
            }
        }
    }
}