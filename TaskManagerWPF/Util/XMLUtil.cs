using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using TaskManagerWPF.Data;

namespace TaskManagerWPF.Util
{
    /// <summary>
    /// XML ユーティリティ
    /// </summary>
    public static class XMLUtil
    {
        /// <summary>
        /// XML ファイルとして保存されるタスク情報のファイル名
        /// </summary>
        private const string XML_FILE_NAME = @".\TaskData.xml";

        /// <summary>
        /// 保存された XML のタスク情報を取得します。
        /// </summary>
        /// <returns>XML から取得したタスク情報, 取得できない場合は null</returns>
        public static List<TaskManagerData> GetTaskDataFromXMLFile()
        {
            List<TaskManagerData> lsRes = null;

            StreamReader sr = null;

            try
            {

                // タスク XML ファイルの存在確認
                if (SystemUtil.GetExistFile(XML_FILE_NAME) == false)
                {
                    // ファイルが無いので以後の処理を行わない。
                    return  null;
                }

                lsRes = new List<TaskManagerData>();
                sr = new StreamReader(XML_FILE_NAME, Encoding.UTF8);
                XmlSerializer xmlSerializer = new XmlSerializer(lsRes.GetType());

                // XML ファイルをデシリアライズして List へ書き込む
                lsRes = (List<TaskManagerData>)xmlSerializer.Deserialize(sr);

                sr.Close();
                sr.Dispose();
                sr = null;
            }
            catch(Exception ex)
            {
                SystemUtil.ConsoleOutputLine(ex.Message);
                SystemUtil.ConsoleOutputLine(ex.StackTrace);

                throw;
            }
            finally
            {
                if (sr != null)
                {
                    sr.Close();
                    sr.Dispose();
                    sr = null;
                }
            }

            return lsRes;
        }

        /// <summary>
        /// タスク情報を XML ファイルとして出力します。
        /// </summary>
        /// <param name="saveTaskData">保存するタスクデータが格納されているリスト</param>
        /// <returns>正常に保存出来た場合: true, そうでない場合: false</returns>
        public static bool SaveTaskData(List<TaskManagerData> saveTaskData)
        {
            bool res = false;

            StreamWriter sw = null;

            try
            {
                sw = new StreamWriter(XML_FILE_NAME, false, Encoding.UTF8);

                XmlSerializer xmlSerializer = new XmlSerializer(saveTaskData.GetType());
                xmlSerializer.Serialize(sw, saveTaskData);

                sw.Close();
                sw.Dispose();
                sw = null;

                res = true;
            }
            catch(Exception ex)
            {
                SystemUtil.ConsoleOutputLine(ex.Message);
                SystemUtil.ConsoleOutputLine(ex.StackTrace);

                throw;
            }
            finally
            {
                if (sw != null)
                {
                    sw.Close();
                    sw.Dispose();
                    sw = null;
                }
            }

            return res;
        }
    }
}
