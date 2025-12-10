using System;
using System.Diagnostics;

namespace ExRScriptCppNativeRMK.kernel
{
    /// <summary>
    /// 进程辅助类
    /// </summary>
    public static class ProcessHelper
    {
        /// <summary>
        /// 运行控制台程序
        /// </summary>
        public static string RunConsoleProgram(string program, string arguments)
        {
            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = program,
                    Arguments = arguments,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                };

                using (Process process = new Process { StartInfo = startInfo })
                {
                    process.Start();

                    string output = process.StandardOutput.ReadToEnd();
                    string error = process.StandardError.ReadToEnd();

                    process.WaitForExit();

                    return string.IsNullOrEmpty(output) ? error : output;
                }
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 运行cmd命令
        /// </summary>
        public static void RunCmdCommand(string command)
        {
            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = Constants.CMD_EXECUTOR,
                    Arguments = $"/c {command}",
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                };

                using (Process process = new Process { StartInfo = startInfo })
                {
                    process.Start();

                    // 读取输出和错误信息
                    string output = process.StandardOutput.ReadToEnd();
                    string error = process.StandardError.ReadToEnd();

                    process.WaitForExit();

                    // 可以选择输出到控制台
                    if (!string.IsNullOrEmpty(output))
                    {
                        Console.Write(output);
                    }

                    if (!string.IsNullOrEmpty(error))
                    {
                        Console.Write(error);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"执行命令失败: {ex.Message}");
            }
        }

        /// <summary>
        /// 运行程序
        /// </summary>
        public static void RunProgram(string program, bool waitForExit)
        {
            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = program,
                    UseShellExecute = true
                };

                Process process = Process.Start(startInfo);

                if (waitForExit && process != null)
                {
                    process.WaitForExit();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"运行程序失败: {ex.Message}");
            }
        }
    }
}