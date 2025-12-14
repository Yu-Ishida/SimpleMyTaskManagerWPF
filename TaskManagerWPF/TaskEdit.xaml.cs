using System;
using System.Windows;
using System.Windows.Controls;
using TaskManagerWPF.Data;
using TaskManagerWPF.Util;

namespace TaskManagerWPF
{
    /// <summary>
    /// TaskEdit.xaml の相互作用ロジック
    /// </summary>
    public partial class TaskEdit : Window
    {
        /// <summary>
        /// タスクデータを取得します。
        /// </summary>
        public TaskManagerData TaskData { get; private set; }

        /// <summary>
        /// 新規タスクデータの作成かを示すフラグ値を取得または設定します。
        /// </summary>
        public bool IsCreateNewTask { get; private set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="taskData">タスクデータ (Optional)</param>
        public TaskEdit(TaskManagerData taskData = null)
        {
            InitializeComponent();

            if (taskData != null)
            {
                this.TaskData = taskData;
                this.IsCreateNewTask = false;
            }
            else
            {
                this.IsCreateNewTask = true;
            }
        }

        /// <summary>
        /// ウィンドウ Activate イベント
        /// </summary>
        /// <param name="sender">イベント元オブジェクト</param>
        /// <param name="e">イベントオブジェクト</param>
        private void TaskEditWnd_Activated(object sender, EventArgs e)
        {
            try
            {
                if (this.TaskData != null)
                {
                    // 既存レコードの編集と判断し、TaskData オブジェクトの内容を該当するコントロールに設定する。
                    this.CbIsTaskComplete.IsChecked = this.TaskData.IsTaskComplete;
                    this.TbTaskName.Text = this.TaskData.TaskName;
                    this.NbTaskProgress.Text = SystemUtil.ConvertObjectToString(this.TaskData.TaskProgress);
                    if (this.TaskData.TaskDeadline == null)
                    {
                        this.DpDeadLine.SelectedDate = null;
                    }
                    else
                    {
                        this.DpDeadLine.SelectedDate = SystemUtil.ConvertObejctToDateTime(this.TaskData.TaskDeadline);
                    }
                    this.TbRemarks.Text = SystemUtil.ConvertObjectToString(this.TaskData.TaskRemarks);
                }
            }
            catch (Exception ex)
            {
                SystemUtil.ConsoleOutputLine(ex.Message);
                SystemUtil.ConsoleOutputLine(ex.StackTrace);

                SystemUtil.ShowErrorDialog(ex);
            }
            finally
            { }
        }


        /// <summary>
        /// 「OK」ボタンクリックイベント
        /// </summary>
        /// <param name="sender">イベント元オブジェクト</param>
        /// <param name="e">イベントオブジェクト</param>
        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // 入力値検証を行う。
                string validMessage = string.Empty;
                if (this.isInputDataValidate(ref validMessage))
                {
                    MessageBox.Show(validMessage, Messages.DIALOG_TITLE, MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }
                else
                {
                    // 正常時

                    // 入力内容を元に TaskData オブジェクトを作成して本ダイアログを閉じる
                    this.TaskData = new TaskManagerData();
                    this.TaskData.IsTaskComplete = this.CbIsTaskComplete.IsChecked.Value;
                    this.TaskData.TaskName = this.TbTaskName.Text;
                    this.TaskData.TaskProgress = SystemUtil.ConvertObjectToInt(this.NbTaskProgress.Text);
                    this.TaskData.TaskDeadline = this.DpDeadLine.SelectedDate;
                    this.TaskData.TaskRemarks = this.TbRemarks.Text;

                    // ダイアログ終了時の呼び出し元への返却
                    this.DialogResult = true;
                }
            }
            catch(Exception ex)
            {
                SystemUtil.ConsoleOutputLine(ex.Message);
                SystemUtil.ConsoleOutputLine(ex.StackTrace);

                SystemUtil.ShowErrorDialog(ex);
            }
            finally
            { }
        }

        // ★★★ 2025年10月27日 機能改善対応 追加処理
        /// <summary>
        /// タスク完了チェックボックスチェック時のイベント
        /// </summary>
        /// <param name="sender">イベント元オブジェクト</param>
        /// <param name="e">イベントオブジェクト</param>
        private void CbIsTaskComplete_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                CheckBox cb = (CheckBox)sender;

                if (cb.IsChecked.Value)
                {
                    // 進捗率を 100% にする。
                    this.NbTaskProgress.Text = SystemUtil.ConvertObjectToString(100);
                }
            }
            catch (Exception ex)
            {
                SystemUtil.ConsoleOutputLine(ex.Message);
                SystemUtil.ConsoleOutputLine(ex.StackTrace);

                SystemUtil.ShowErrorDialog(ex);
            }
            finally
            { }
        }
        // ★★★ 追加処理 END

        /// <summary>
        /// 入力値検証を行います。
        /// </summary>
        /// <param name="message">異常時におけるメッセージを格納する変数</param>
        /// <returns>検証の結果異常の場合: true, 正常な場合: false</returns>
        private bool isInputDataValidate(ref string message)
        {
            bool res = false;

            try
            {
                // タスク名が入力されていない場合は異常とする。
                if (string.IsNullOrEmpty(this.TbTaskName.Text))
                {
                    message = Messages.INVALID_TASK_NAME_NOT_INPUTED;
                    res = true;
                }

                if (string.IsNullOrEmpty(this.NbTaskProgress.Text) == false)
                {
                    int progress = SystemUtil.ConvertObjectToInt(this.NbTaskProgress.Text);

                    if (progress > 100 || progress < 0)
                    {
                        // 進捗率が不正な場合

                        message = Messages.IVNALID_TASK_PROGRESS_INPUT;
                        res = true;
                    }
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
    }
}
