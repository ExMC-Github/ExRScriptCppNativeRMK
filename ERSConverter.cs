using System;
using System.Text.RegularExpressions;

namespace ExRScriptCppNativeRMK
{
    /// <summary>
    /// ERS代码转换器类
    /// </summary>
    public static class ERSConverter
    {
        /// <summary>
        /// 解析ERS行
        /// </summary>
        public static string ParseERSLine(string codeLine)
        {
            string param = ExtractParameters(codeLine);
            string lowerCodeLine = codeLine.ToLower();
            string trimmedLowerCodeLine = RemoveAllSpaces(lowerCodeLine);

            Console.WriteLine(codeLine);

            // 检查是否包含大括号或为空行
            if (lowerCodeLine.Contains("{") || lowerCodeLine.Contains("}") ||
                string.IsNullOrWhiteSpace(trimmedLowerCodeLine))
            {
                return codeLine;
            }

            // 处理string声明
            if (lowerCodeLine.Contains("string "))
            {
                return ProcessStringDeclaration(codeLine);
            }

            // 处理return语句
            if (lowerCodeLine.Contains("return "))
            {
                return ProcessReturnStatement(codeLine);
            }

            // 处理int声明
            if (lowerCodeLine.StartsWith("int "))
            {
                return ProcessIntDeclaration(codeLine);
            }

            // 处理import语句
            if (lowerCodeLine.StartsWith("import "))
            {
                return ProcessImportStatement(codeLine);
            }

            // 处理注释
            if (lowerCodeLine.Contains("// "))
            {
                return string.Empty;
            }

            // 处理各种函数调用
            return ProcessFunctionCalls(codeLine, param, trimmedLowerCodeLine);
        }

        /// <summary>
        /// HelloDLL测试方法
        /// </summary>
        public static int HelloDLLTest()
        {
            return 1;
        }

        /// <summary>
        /// 提取括号内的参数
        /// </summary>
        private static string ExtractParameters(string codeLine)
        {
            int leftParenthesisIndex = codeLine.IndexOf('(');
            int rightParenthesisIndex = codeLine.IndexOf(')');

            if (leftParenthesisIndex > -1 && rightParenthesisIndex > leftParenthesisIndex)
            {
                return codeLine.Substring(leftParenthesisIndex + 1,
                    rightParenthesisIndex - leftParenthesisIndex - 1);
            }

            return string.Empty;
        }

        /// <summary>
        /// 处理string声明
        /// </summary>
        private static string ProcessStringDeclaration(string codeLine)
        {
            string content = GetContentAfterKeyword(RemoveAllSpaces(codeLine), 7);
            if (!content.EndsWith(";"))
            {
                return "    string " + content + ";";
            }
            return "    string " + content;
        }

        /// <summary>
        /// 处理return语句
        /// </summary>
        private static string ProcessReturnStatement(string codeLine)
        {
            string content = GetContentAfterKeyword(RemoveAllSpaces(codeLine), 7);
            if (!content.EndsWith(";"))
            {
                return "    return " + content + ";";
            }
            return "    return " + content;
        }

        /// <summary>
        /// 处理int声明
        /// </summary>
        private static string ProcessIntDeclaration(string codeLine)
        {
            string content = GetContentAfterKeyword(RemoveAllSpaces(codeLine), 4);
            if (!content.EndsWith(";"))
            {
                return "    int " + content + ";";
            }
            return "    int " + content;
        }

        /// <summary>
        /// 处理import语句
        /// </summary>
        private static string ProcessImportStatement(string codeLine)
        {
            return "#include " + GetContentAfterKeyword(RemoveAllSpaces(codeLine), 7);
        }

        /// <summary>
        /// 处理函数调用
        /// </summary>
        private static string ProcessFunctionCalls(string codeLine, string param, string trimmedCodeLine)
        {
            if (trimmedCodeLine.StartsWith("print("))
            {
                return $"    print({param});";
            }
            else if (trimmedCodeLine.Contains("msgbox("))
            {
                return $"    msgbox({param});";
            }
            else if (trimmedCodeLine.Contains("msgboxo("))
            {
                return $"    msgbox({param}.c_str());";
            }
            else if (trimmedCodeLine.Contains("msgbox_info("))
            {
                return $"    msgbox_info({param});";
            }
            else if (trimmedCodeLine.Contains("msgbox_infoo("))
            {
                return $"    msgbox_info({param}.c_str());";
            }
            else if (trimmedCodeLine.Contains("msgbox_error("))
            {
                return $"    msgbox_error({param});";
            }
            else if (trimmedCodeLine.Contains("msgbox_erroro("))
            {
                return $"    msgbox_error({param}.c_str());";
            }
            else if (trimmedCodeLine.Contains("msgbox_ask("))
            {
                return $"    msgbox_ask({param});";
            }
            else if (trimmedCodeLine.Contains("msgbox_asko("))
            {
                return $"    msgbox_ask({param}.c_str());";
            }
            else if (trimmedCodeLine.Contains("msgbox_warn("))
            {
                return $"    msgbox_warn({param});";
            }
            else if (trimmedCodeLine.Contains("msgbox_warno("))
            {
                return $"    msgbox_warn({param}.c_str());";
            }
            else if (trimmedCodeLine.Contains("systemo("))
            {
                return $"    system({param}.c_str());";
            }
            else if (trimmedCodeLine.Contains("system("))
            {
                return $"    system({param});";
            }
            else if (trimmedCodeLine.Contains("remove("))
            {
                return $"    remove({param.Replace("\\", "\\\\")});";
            }
            else if (trimmedCodeLine.Contains("removeo("))
            {
                return $"    remove({param.Replace("\\", "\\\\")}.c_str());";
            }
            else if (RemoveAllSpaces(codeLine).Contains("ExRScriptInitialize("))
            {
                return $"    ExRScriptInitialize({param});";
            }

            return "    " + codeLine;
        }

        /// <summary>
        /// 获取关键词后的内容
        /// </summary>
        private static string GetContentAfterKeyword(string input, int keywordLength)
        {
            if (input.Length > keywordLength)
            {
                return input.Substring(keywordLength);
            }
            return string.Empty;
        }

        /// <summary>
        /// 移除所有空白字符
        /// </summary>
        private static string RemoveAllSpaces(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            return Regex.Replace(input, @"\s", "");
        }
    }
}