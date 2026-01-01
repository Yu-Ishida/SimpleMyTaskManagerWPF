using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace TaskManagerWPF.CustomControl
{
    /// <summary>
    /// 拡張数値ボックス
    /// </summary>
    /// <remarks>
    /// 参考資料: 【WPF】数値しか入力できないTextBox
    /// URL: https://zenn.dev/nuits_jp/articles/2024-02-25-numeric-text-box
    /// </remarks>
    public class ExNumBox : TextBox
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ExNumBox()
        {
            // IME を無効化する。
            InputMethod.SetIsInputMethodEnabled(this, false);

            // ペーストイベントをハンドリング
            DataObject.AddPastingHandler(this, onPaste);
        }

        /// <summary>
        /// PreviewKeyDown イベント
        /// </summary>
        /// <param name="e">イベントオブジェクト</param>
        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            base.OnPreviewKeyDown(e);

            // スペースキーが押下された場合、イベントをキャンセルする。
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// PreaviewTextInput イベント
        /// </summary>
        /// <param name="e">イベントオブジェクト</param>
        protected override void OnPreviewTextInput(TextCompositionEventArgs e)
        {
            base.OnPreviewTextInput(e);

            // 入力されたテキストから反映後のテキストを予測する。
            string afterText = GetAfterText(e.Text);

            if (!IsInt(afterText))
            {
                // 反映後の値が int 型ではないのでイベントをキャンセルする。
                e.Handled = true;
            }
        }

        /// <summary>
        /// ペーストされたテキストを値の反映前に検証します。
        /// </summary>
        /// <param name="sender">イベント元オブジェクト</param>
        /// <param name="e">イベントオブジェクト</param>
        private void onPaste(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                // ペーストされたデータがテキストの場合、
                // ペーストされたテキストを取得する。
                string pastedText = (string)e.DataObject.GetData(typeof(string)) ?? string.Empty;

                // ペーストされたテキストから反映後の値を予測
                string afterText = GetAfterText(pastedText);


                if(!IsInt(afterText))
                {
                    // 反映後の値が int 型の入力値ではない場合、イベントをキャンセル。
                    e.CancelCommand();
                }
            }
            else
            {
                // ペーストされたデータがテキストではない場合、イベントをキャンセル。
                e.CancelCommand();
            }
        }

        /// <summary>
        /// テキストがint型の値かどうかを判定します。
        /// </summary>
        /// <param name="text">入力したテキスト</param>
        /// <returns>int 型に変換出来るテキストの場合: true, そうでない場合: false</returns>
        private bool IsInt(string text)
        {
            if ("-".Equals(text))
            {
                // マイナス記号のみの場合はtrueを返す
                return true;
            }

            // まずint形にパースして確認する。
            if (int.TryParse(text, out var intValue))
            {
                // 01などを除外するために、パースした値を文字列に変換して、入力値と一致するかどうかを確認する。
                return string.Equals(text, intValue.ToString());
            }
            return false;
        }


        /// <summary>
        /// ペースト後のテキストを予測します。
        /// </summary>
        /// <param name="text"></param>
        /// <returns>ペースト後のテキスト</returns>
        private string GetAfterText(string text)
        {
            // ペースト後のテキストを予測（例えば、数値以外の文字が含まれる場合はペーストをキャンセル）
            return Text.Substring(0, SelectionStart)
                   + text
                   + Text.Substring(SelectionStart + SelectionLength);
        }
    }
}
