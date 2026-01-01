
namespace TaskManagerWPF.Util
{
    /// <summary>
    /// メッセージ定義
    /// </summary>
    public static class Messages
    {
        /// <summary>
        /// ダイアログ上に表示するタイトル
        /// </summary>
        public const string DIALOG_TITLE = "タスク管理";

        /// <summary>
        /// バージョン情報ダイアログ上に表示するタイトル
        /// </summary>
        public const string VERSION_INFO_DIALOG_TITLE = "バージョン情報";

        /// <summary>
        /// 例外エラー発生時のダイアログに表示するメッセージ
        /// </summary>
        public const string EXCEPTION_DIALOG_MESSAGE = "下記のエラーが発生しました。\r\n\r\n【オリジナルメッセージ】\r\n{0}\r\n\r\n【エラー発生箇所】\r\n{1}";

        /// <summary>
        /// バージョン情報に表示するテキスト
        /// </summary>
        public const string VERSION_INFO = "シンプルタスク管理 WPF 版 Ver {0}\r\n(c) 2025 Yu Ishida.";

        /// <summary>
        /// 検証: タスク名が入力されていない場合に表示するメッセージ
        /// </summary>
        public const string INVALID_TASK_NAME_NOT_INPUTED = "タスク名が入力されていません。\r\nタスクを追加するにはタスク名を入力する必要があります。";

        /// <summary>
        /// 検証: タスク進捗率の入力に誤りがある場合に表示するメッセージ
        /// </summary>
        public const string IVNALID_TASK_PROGRESS_INPUT = "タスク進捗率の入力に誤りがあります。\r\n進捗率は 0% ～ 100% の範囲で入力して下さい。";

        /// <summary>
        /// タスク削除前の確認ダイアログに表示するメッセージ
        /// </summary>
        public const string COMFIRM_DELETE_TASK = "下記のタスクを削除します。\r\n\r\nタスク完了状態: {0}\r\nタスク名: {1}\r\nタスク進捗率: {2}\r\nタスク期限日: {3}\r\n\r\n ※ 削除したデータは元に戻せません!";
    }
}
