using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TaskManagerWPF.Data;
using TaskManagerWPF.Util;

namespace TaskManagerWPF
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ウィンドウ Loaded イベント
        /// </summary>
        /// <param name="sender">イベント元オブジェクト</param>
        /// <param name="e">イベントオブジェクト</param>
        private void MainWnd_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                // 保存したタスクの XML からデータを取得
                List<TaskManagerData> lsTaskManagerData = XMLUtil.GetTaskDataFromXMLFile();

                if (lsTaskManagerData != null && lsTaskManagerData.Count > 0)
                {
                    // レコードが存在している場合、DataGrid で設定する。
                    this.DgTask.ItemsSource = lsTaskManagerData;
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

        /// <summary>
        /// ウィンドウ Cosing イベント
        /// </summary>
        /// <param name="sender">イベント元オブジェクト</param>
        /// <param name="e">イベントオブジェクト</param>
        private void MainWnd_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                // DataGrid 上のタスクデータを保存する。
                if (this.DgTask.ItemsSource != null)
                {
                    List<TaskManagerData> lsTaskManagerData = (List<TaskManagerData>)this.DgTask.ItemsSource;

                    XMLUtil.SaveTaskData(lsTaskManagerData);
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
        /// 「終了」メニュークリックイベント
        /// </summary>
        /// <param name="sender">イベント元オブジェクト</param>
        /// <param name="e">イベントオブジェクト</param>
        private void MenuExit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Application.Current.Shutdown();
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

        /// <summary>
        /// 「バージョン情報メニュークリックイベント
        /// </summary>
        /// <param name="sender">イベント元オブジェクト</param>
        /// <param name="e">イベントオブジェクト</param>
        private void MenuVersionInfo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string message = Messages.VERSION_INFO;
                message = string.Format(message, SystemUtil.VERSION_NUMBER);
                MessageBox.Show(message, Messages.VERSION_INFO_DIALOG_TITLE, MessageBoxButton.OK, MessageBoxImage.Information);
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
        /// 『タスク追加』ボタンクリックイベント
        /// </summary>
        /// <param name="sender">イベント元オブジェクト</param>
        /// <param name="e">イベントオブジェクト</param>
        private void BtnAddTask_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TaskEdit taskEdit = new TaskEdit();
                taskEdit.Owner = this;
                bool? isDialogResult =  taskEdit.ShowDialog();

                if (isDialogResult.Value)
                {
                    // タスク編集ダイアログで「OK」ボタンをクリックしてダイアログが閉じられた。
                    TaskManagerData data = taskEdit.TaskData;
                    if (data != null && taskEdit.IsCreateNewTask)
                    {
                        // タスクデータが存在し、新規作成タスクの場合
                        List<TaskManagerData> lsData = (List<TaskManagerData>)this.DgTask.ItemsSource;
                        if (lsData == null)
                        {
                            lsData = new List<TaskManagerData>();
                        }

                        lsData.Add(data);
                        this.DgTask.ItemsSource = null;
                        this.DgTask.ItemsSource = lsData;

                        // ★★★ 2025年10月27日 機能改善対応
                        // レコードを保存する。
                        List<TaskManagerData> lsTaskManagerData = (List<TaskManagerData>)this.DgTask.ItemsSource;
                        XMLUtil.SaveTaskData(lsTaskManagerData);
                        // ★★★ 機能改善対応 END
                    }
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
        /// 『タスク削除』ボタンクリックイベント
        /// </summary>
        /// <param name="sender">イベントオブジェクト</param>
        /// <param name="e">イベントオブジェクト</param>
        private void BtnDeleteTaskk_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // DataGrid 上で選択されているタスクを削除する。

                if (this.DgTask.SelectedItem == null)
                {
                    // DataGrid 上でレコードが選択されていない為、以後の処理を行わない。
                    return;
                }

                TaskManagerData data = (TaskManagerData)this.DgTask.SelectedItem;

                string message = Messages.COMFIRM_DELETE_TASK;
                message = string.Format(message, data.IsTaskComplete, data.TaskName, data.TaskProgress, data.TaskDeadline);

                if (MessageBox.Show(message, Messages.DIALOG_TITLE, MessageBoxButton.OKCancel, MessageBoxImage.Exclamation) != MessageBoxResult.OK)
                {
                    // OK ボタン以外がクリックされた為、以後の処理を行わない。
                    return;
                }

                // DataGrid 上から該当するレコードを削除する。
                List<TaskManagerData> lsData = (List<TaskManagerData>)this.DgTask.ItemsSource;
                lsData.Remove(data);
                this.DgTask.ItemsSource = null;
                this.DgTask.ItemsSource = lsData;
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
        /// DataGrid 上でのマウスダブルクリックイベント
        /// </summary>
        /// <param name="sender">イベント元オブジェクト</param>
        /// <param name="e">イベントオブジェクト</param>
        private void DgTask_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DataGrid dg = (DataGrid)sender;

                if (dg.SelectedItem == null)
                {
                    // DataGrid 上のレコードが無い余白部分等をダブルクリックした為、
                    // 以後の処理を行わない。
                    return;
                }

                // ダブルクリックしたセルのタスクデータを子ウィンドウに表示して編集を行う。
                TaskManagerData data = (TaskManagerData)dg.SelectedItem;

                // for debug
                SystemUtil.ConsoleOutputLine(data);

                TaskEdit taskEdit = new TaskEdit(data);
                taskEdit.Owner = this;
                taskEdit.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                taskEdit.Closed += TaskEdit_Closed_FromDGDoubleClicked;
                taskEdit.ShowDialog();
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
        /// DataGrid 上でダブルクリックによる TaskEdit ウィンドウクローズ後のイベント
        /// </summary>
        /// <param name="sender">イベント元オブジェクト</param>
        /// <param name="e">イベントオブジェクト</param>
        private void TaskEdit_Closed_FromDGDoubleClicked(object sender, EventArgs e)
        {
            try
            {
                TaskEdit taskEdit = (TaskEdit)sender;

                TaskManagerData tData = taskEdit.TaskData;

                if (taskEdit.DialogResult == false)
                {
                    // ダイアログで OK 以外が押下されて終了した為、以後の処理を行わない。
                    return;
                }

                if (tData == null)
                {
                    // タスクデータが null なので以後の処理を行わない。
                    return;
                }

                List<TaskManagerData> lsTaskData = (List<TaskManagerData>)this.DgTask.ItemsSource;
                int index = this.DgTask.SelectedIndex;
                lsTaskData.RemoveAt(index);

                lsTaskData.Insert(index, tData);

                // ItemSource を更新する。
                this.DgTask.ItemsSource = null;
                this.DgTask.ItemsSource = lsTaskData;
            }
            catch(Exception ex)
            {
                SystemUtil.ConsoleOutputLine(ex.Message);
                SystemUtil.ConsoleOutputLine(ex.StackTrace);

                SystemUtil.ShowErrorDialog(ex);
            }
            finally
            {
                // 登録した本イベントを削除する。
                ((TaskEdit)sender).Closed -= TaskEdit_Closed_FromDGDoubleClicked;
            }
        }
    }
}
