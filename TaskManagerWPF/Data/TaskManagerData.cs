using System;
using System.Xml.Serialization;

namespace TaskManagerWPF.Data
{
    /// <summary>
    /// タスク管理データ
    /// </summary>
    [XmlRoot("TaskManagerData")]
    public class TaskManagerData
    {
        /// <summary>
        /// タスク管理状態を取得または設定します。
        /// </summary>
        [XmlElement("IsTaskComplete")]
        public bool IsTaskComplete { get; set; }
        /// <summary>
        /// タスク名を取得または設定します。
        /// </summary>
        [XmlElement("TaskName")]
        public string TaskName { get; set; }
        /// <summary>
        /// タスク進捗率を取得または設定します。
        /// </summary>
        [XmlElement("TaskProgress")]
        public int TaskProgress { get; set; }
        /// <summary>
        /// タスク期限日を取得または設定します。
        /// </summary>
        [XmlElement("TaskDeadline")]
        public DateTime? TaskDeadline { get; set; }
        /// <summary>
        /// タスク備考を取得または設定します。
        /// </summary>
        [XmlElement("TaskRemarks")]
        public string TaskRemarks { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public TaskManagerData()
        { }
    }
}
