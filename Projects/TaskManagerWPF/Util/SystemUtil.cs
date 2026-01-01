using System;
using System.Diagnostics;
using System.Windows;
using System.IO;

namespace TaskManagerWPF.Util
{
    /// <summary>
    /// システムユーティリティ
    /// </summary>
    public static class SystemUtil
    {
        /// <summary>
        /// バージョン番号
        /// </summary>
        public const string VERSION_NUMBER = "1.5";

        /// <summary>
        /// 指定したオブジェクトをコンソール上に出力します。
        /// </summary>
        /// <param name="value">コンソール上に出力するオブジェクト</param>
        public static void ConsoleOutputLine(object value)
        {
            if (Debugger.IsAttached)
            {
                Console.WriteLine(value.ToString());
            }
        }

        /// <summary>
        /// 指定した例外オブジェクトをダイアログとして表示します。
        /// </summary>
        /// <param name="ex">例外オブジェクト</param>
        public static void ShowErrorDialog(Exception ex)
        {
            string message = Messages.EXCEPTION_DIALOG_MESSAGE;
            message = string.Format(message, ex.Message, ex.StackTrace);

            MessageBox.Show(message, Messages.DIALOG_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        /// <summary>
        /// 指定したオブジェクトを string 型に変換します。
        /// </summary>
        /// <param name="value">string 型に変換するオブジェクト</param>
        /// <returns>string 型に変換されたオブジェクト</returns>
        /// <remarks>※ 変換に失敗した場合は string.Empty が返却されます。</remarks>
        public static string ConvertObjectToString(object value)
        {
            string res = string.Empty;

            try
            {
                value = (value == DBNull.Value || value == null) ? string.Empty : value;

                res = value.ToString();
            }
            catch(Exception ex)
            {
                SystemUtil.ConsoleOutputLine(ex.Message);
                SystemUtil.ConsoleOutputLine(ex.StackTrace);

                throw;
            }
            finally
            { }

            return res;
        }

        /// <summary>
        /// 指定したオブジェクトを int 型に変換します。
        /// </summary>
        /// <param name="value">int 型に変換するオブジェクト</param>
        /// <returns>int 型に変換されたオブジェクト</returns>
        /// <remarks>※ 変換に失敗した場合: 0 が返却されます。</remarks>
        public static int ConvertObjectToInt(object value)
        {
            int res = 0;

            try
            {
                value = (value == DBNull.Value || value == null) ? string.Empty : value;

                if (int.TryParse(value.ToString(), out res) == false)
                {
                    // int 型への変換に失敗
                    res = 0;
                }
            }
            catch(Exception ex)
            {
                SystemUtil.ConsoleOutputLine(ex.Message);
                SystemUtil.ConsoleOutputLine(ex.StackTrace);

                throw;
            }
            finally
            { }

            return res;
        }

        /// <summary>
        /// 指定したオブジェクトを DateTime 型に変換します。
        /// </summary>
        /// <param name="value">DateTime 型に変換するオブジェクト</param>
        /// <returns>DateTime 型に変換されたオブジェクト</returns>
        /// <remarks>※ 変換に失敗した場合は DateTime.MinValue が返却されます。</remarks>
        public static DateTime ConvertObejctToDateTime(object value)
        {
            DateTime res = DateTime.MinValue;

            try
            {
                value = (value == DBNull.Value || value == null) ? string.Empty : value;

                if (DateTime.TryParse(value.ToString(), out res) == false)
                {
                    // DateTime 型への変換に失敗
                    res = DateTime.MinValue;
                }
            }
            catch (Exception ex)
            {
                SystemUtil.ConsoleOutputLine(ex.Message);
                SystemUtil.ConsoleOutputLine(ex.StackTrace);

                throw;
            }
            finally
            { }

            return res;
        }

        /// <summary>
        /// 指定したオブジェクトを bool 型に変換します。
        /// </summary>
        /// <param name="value">bool 型に変換するオブジェクト</param>
        /// <returns>bool 型に変換されたオブジェクト</returns>
        /// <remarks>
        /// ※ 変換に失敗した場合は false が返却されます。
        /// </remarks>
        public static bool ConvertObjectToBool(object value)
        {
            bool res = false;

            try
            {
                value = (value == DBNull.Value || value == null) ? string.Empty : value;

                if (bool.TryParse(value.ToString(), out res) == false)
                {
                    // bool 型への変換に失敗
                    res = false;
                }
            }
            catch(Exception ex)
            {
                SystemUtil.ConsoleOutputLine(ex.Message);
                SystemUtil.ConsoleOutputLine(ex.StackTrace);

                throw;
            }
            finally
            { }

            return res;
        }

        /// <summary>
        /// 指定したファイルが存在するかを確認します。
        /// </summary>
        /// <param name="fileNamePath">確認するファイルのファイル名を含むパス</param>
        /// <returns>指定したファイルが存在する場合: true, そうでない場合: false</returns>
        public static bool GetExistFile(string fileNamePath)
        {
            bool res = false;

            try
            {
                res = File.Exists(fileNamePath);
            }
            catch(Exception ex)
            {
                SystemUtil.ConsoleOutputLine(ex.Message);
                SystemUtil.ConsoleOutputLine(ex.StackTrace);

                throw;
            }
            finally
            { }

            return res;
        }
    }
}
