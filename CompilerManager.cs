using System;
using System.IO;
using System.Text;

namespace ExRScriptCppNativeRMK
{
    /// <summary>
    /// 编译器管理类
    /// </summary>
    public static class CompilerManager
    {
        /// <summary>
        /// 运行编译方法
        /// </summary>
        public static void RunCompileMethod()
        {
            if (GlobalVariables.CommandLineArgs.Length < 2)
            {
                LanguageManager.Lang_CNE2();
                return;
            }

            string filePath = TextConverter.ReadTextAndConvertParamsMethod1(
                GlobalVariables.CommandLineArgs[1]);

            if (!File.Exists(filePath))
            {
                LanguageManager.Lang_CNE2();
                return;
            }

            if (Path.GetExtension(filePath).ToLower() != Constants.ERS_EXTENSION)
            {
                LanguageManager.Lang_CNE1();
                return;
            }

            ProcessCompilation(filePath, true);
        }

        /// <summary>
        /// 编译方法
        /// </summary>
        public static void CompileMethod()
        {
            if (GlobalVariables.CommandLineArgs.Length < 3)
            {
                LanguageManager.Lang_CNE2();
                return;
            }

            string filePath = TextConverter.ReadTextAndConvertParamsMethod1(
                GlobalVariables.CommandLineArgs[1]);

            if (!File.Exists(filePath))
            {
                LanguageManager.Lang_CNE2();
                return;
            }

            bool staticCompile = GlobalVariables.CommandLineArgs.Length == 4 &&
                GlobalVariables.CommandLineArgs[3].Replace("-", "").ToLower() == "static";

            ProcessCompilation(filePath, false, staticCompile);
        }

        /// <summary>
        /// H编译方法
        /// </summary>
        public static void HCompileMethod()
        {
            if (GlobalVariables.CommandLineArgs.Length < 3)
            {
                LanguageManager.Lang_CNE2();
                return;
            }

            string filePath = TextConverter.ReadTextAndConvertParamsMethod1(
                GlobalVariables.CommandLineArgs[1]);

            if (!File.Exists(filePath))
            {
                LanguageManager.Lang_CNE2();
                return;
            }

            bool staticCompile = GlobalVariables.CommandLineArgs.Length == 4 &&
                GlobalVariables.CommandLineArgs[3].Replace("-", "").ToLower() == "static";

            ProcessHCompilation(filePath, staticCompile);
        }

        /// <summary>
        /// 处理编译流程
        /// </summary>
        private static void ProcessCompilation(string filePath, bool runAfterCompile,
            bool staticCompile = false)
        {
            LanguageManager.Lang_CIG1();

            // 读取命令内容
            GlobalVariables.CommandContent = File.ReadAllLines(filePath);
            GlobalVariables.CommandCount = GlobalVariables.CommandContent.Length;

            LanguageManager.Lang_CIG2();

            // 生成C++代码
            string cppCode = CodeParseToCppMethod();

            LanguageManager.Lang_CIG3();

            // 创建输出目录
            string outputDir = Path.GetPathRoot(Environment.CurrentDirectory) +
                Constants.DEBUG_DIRECTORY;
            Directory.CreateDirectory(outputDir);

            // 写入临时C++文件
            string tempCppPath = Path.Combine(outputDir, Constants.TEMP_CPP_FILE);
            File.WriteAllText(tempCppPath, cppCode);

            LanguageManager.Lang_CIG4();

            // 检查g++是否存在
            string gccTest = ProcessHelper.RunConsoleProgram(Constants.GCC_COMPILER, "--version");

            if (gccTest.ToLower().StartsWith(Constants.GCC_COMPILER))
            {
                string exeName = Path.GetFileNameWithoutExtension(filePath);
                exeName = exeName.Replace(Constants.RUNPATH_PLACEHOLDER + "\\", "");

                string outputExePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                    $"Runner_{exeName}{Constants.EXE_EXTENSION}");

                string includePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                    Constants.INCLUDE_DIRECTORY);

                // 构建编译命令
                string compileCommand = BuildCompileCommand(tempCppPath, outputExePath,
                    includePath, staticCompile);

                ProcessHelper.RunCmdCommand(compileCommand);
                LanguageManager.Lang_COD1();

                if (runAfterCompile)
                {
                    // 运行生成的可执行文件
                    ProcessHelper.RunProgram(outputExePath, true);

                    // 删除生成的可执行文件
                    try { File.Delete(outputExePath); } catch { }
                }
                else
                {
                    // 输出编译完成信息
                    if (GlobalVariables.SystemLanguage == Constants.SIMPLIFIED_CHINESE)
                    {
                        Console.WriteLine($"编译完成\n\n已编译程序{GlobalVariables.CommandLineArgs[1]}到" +
                            $"{Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                            GlobalVariables.CommandLineArgs[2])}{Constants.EXE_EXTENSION}");
                    }
                }
            }
            else
            {
                LanguageManager.Lang_CGE1();
            }

            // 清理临时文件
            try { File.Delete(tempCppPath); } catch { }
        }

        /// <summary>
        /// 处理H编译流程
        /// </summary>
        private static void ProcessHCompilation(string filePath, bool staticCompile)
        {
            // 检查g++是否存在
            string gccTest = ProcessHelper.RunConsoleProgram(Constants.GCC_COMPILER, "--version");

            if (!gccTest.ToLower().StartsWith(Constants.GCC_COMPILER))
            {
                LanguageManager.Lang_CGE1();
                return;
            }

            LanguageManager.Lang_CIG1();

            // 读取命令内容
            GlobalVariables.CommandContent = File.ReadAllLines(filePath);
            GlobalVariables.CommandCount = GlobalVariables.CommandContent.Length;

            LanguageManager.Lang_CIG2();

            // 生成C++代码
            string cppCode = CodeParseToCppMethod();

            // 创建输出目录
            string outputDir = Path.GetPathRoot(Environment.CurrentDirectory) +
                Constants.DEBUG_DIRECTORY;
            Directory.CreateDirectory(outputDir);

            // 写入临时C++文件
            string tempCppPath = Path.Combine(outputDir, Constants.TEMP_CPP_FILE);
            File.WriteAllText(tempCppPath, cppCode);

            LanguageManager.Lang_HCI01();

            string includePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                Constants.INCLUDE_DIRECTORY);

            // 预处理
            ProcessHelper.RunCmdCommand(
                $"{Constants.GCC_COMPILER} -E \"{tempCppPath}\" -o " +
                $"\"{Path.Combine(outputDir, Constants.CODE_I_FILE)}\" " +
                $"-I \"{includePath}\"");

            LanguageManager.Lang_HCI1();

            // 编译为汇编
            ProcessHelper.RunCmdCommand(
                $"{Constants.GCC_COMPILER} -S \"{Path.Combine(outputDir, Constants.CODE_I_FILE)}\" " +
                $"-o \"{Path.Combine(outputDir, Constants.CODE_S_FILE)}\" " +
                $"-I \"{includePath}\"");

            // 编译为目标文件
            ProcessHelper.RunCmdCommand(
                $"{Constants.GCC_COMPILER} -c \"{Path.Combine(outputDir, Constants.CODE_S_FILE)}\" " +
                $"-o \"{Path.Combine(outputDir, Constants.CODE_O_FILE)}\" " +
                $"-I \"{includePath}\"");

            LanguageManager.Lang_COD1();
            LanguageManager.Lang_HCI2();

            // 链接
            string outputName = GlobalVariables.CommandLineArgs[2]
                .Replace(Constants.RUNPATH_PLACEHOLDER + "\\", "");
            string outputExePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                $"{outputName}{Constants.EXE_EXTENSION}");

            if (staticCompile)
            {
                ProcessHelper.RunCmdCommand(
                    $"{Constants.GCC_COMPILER} -o \"{outputExePath}\" " +
                    $"\"{Path.Combine(outputDir, Constants.CODE_O_FILE)}\" -static");
            }
            else
            {
                ProcessHelper.RunCmdCommand(
                    $"{Constants.GCC_COMPILER} -o \"{outputExePath}\" " +
                    $"\"{Path.Combine(outputDir, Constants.CODE_O_FILE)}\"");
            }

            // 输出完成信息
            if (GlobalVariables.SystemLanguage == Constants.SIMPLIFIED_CHINESE)
            {
                Console.WriteLine($"\n已编译程序 {GlobalVariables.CommandLineArgs[1]} 到 " +
                    $"{Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                    GlobalVariables.CommandLineArgs[2])}{Constants.EXE_EXTENSION}\n");
            }

            // 清理临时文件
            try { File.Delete(tempCppPath); } catch { }
        }

        /// <summary>
        /// 代码解析到C++方法
        /// </summary>
        private static string CodeParseToCppMethod()
        {
            StringBuilder cppCode = new StringBuilder();
            cppCode.Append(GlobalVariables.IostreamCodeFramework);

            for (int i = 0; i < GlobalVariables.CommandCount; i++)
            {
                string line = TextConverter.ReadTextAndConvertParamsMethod2(
                    GlobalVariables.CommandContent[i]);
                string parsedLine = ERSConverter.ParseERSLine(line);
                cppCode.AppendLine(parsedLine);
            }

            cppCode.Append(GlobalVariables.IostreamCodeFramework1);
            return cppCode.ToString();
        }

        /// <summary>
        /// 构建编译命令
        /// </summary>
        private static string BuildCompileCommand(string cppPath, string outputPath,
            string includePath, bool staticCompile)
        {
            string command = $"{Constants.GCC_COMPILER} -o \"{outputPath}\" \"{cppPath}\"";

            if (staticCompile)
            {
                command += " -static";
            }

            command += $" -I \"{includePath}\"";

            return command;
        }
    }
}