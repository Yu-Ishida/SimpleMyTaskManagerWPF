using System;
using System.Globalization;
using System.Windows.Data;
using TaskManagerWPF.Util;

namespace TaskManagerWPF.ConvertUtil
{
    /// <summary>
    /// タスク状態変換
    /// ※ タスク完了状態の bool 型を対応するテキストに変換するクラ(XAML 上で本クラスを使用)
    /// </summary>
    public class TaskStateConverter : IValueConverter
    {
        /// <summary>
        /// タスクの状態を表す値からタスクの状態を表すテキストに変換します。
        /// </summary>
        /// <param name="value">変換するタスクの状態を表す値</param>
        /// <param name="targetType">バインディングターゲット プロパティのデータ型</param>
        /// <param name="parameter">使用するコンバーター パラメータ</param>
        /// <param name="culture">コンバーターで使用するカルチャ</param>
        /// <returns>変換された値</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isCompleted = (bool)value;

            return isCompleted ? "完了" : "未完了";
        }

        /// <summary>
        /// タスクの状態を表すテキストからタスクの状態を表す値に変換します。
        /// </summary>
        /// <param name="value">変換するタスクの状態を表すテキスト</param>
        /// <param name="targetType">変換後のデータ型</param>
        /// <param name="parameter">使用するコンバーター パラメータ</param>
        /// <param name="culture">コンバーターで使用するカルチャ</param>
        /// <returns>変換された値</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString() == "完了";
        }
    }

    /// <summary>
    /// タスク期日変換
    /// ※ タスク期日の DateTime 型を対応するテキストに変換するクラス (XAML 上で本クラスを使用)
    /// </summary>
    public class TaskDeadLineConverter : IValueConverter
    {
        /// <summary>
        /// タスクの期日を表す値を変換します。
        /// </summary>
        /// <param name="value">変換するタスクの状態を表す値</param>
        /// <param name="targetType">バインディングターゲット プロパティのデータ型</param>
        /// <param name="parameter">使用するコンバーター パラメータ</param>
        /// <param name="culture">コンバーターで使用するカルチャ</param>
        /// <returns>変換された値</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            object res = null;

            DateTime? taskDeadLine = (DateTime?)value;

            if (taskDeadLine == null)
            {
                res = "-";
            }
            else
            {
                res = taskDeadLine.Value;
            }

            return res;
        }

        /// <summary>
        /// タスクの期日を表すテキストから値を変換します。
        /// </summary>
        /// <param name="value">変換するタスクの状態を表すテキスト</param>
        /// <param name="targetType">変換後のデータ型</param>
        /// <param name="parameter">使用するコンバーター パラメータ</param>
        /// <param name="culture">コンバーターで使用するカルチャ</param>
        /// <returns>変換された値</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            object res = null;

            string valueStr = SystemUtil.ConvertObjectToString(value);

            if (valueStr == "-")
            {
                res = null;
            }
            else
            {
                res = value;
            }

            return res;
        }
    }
}
