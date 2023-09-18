using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Demo.Common.SaveLoad
{
    /// <summary>
    /// 以文件方式对数据进行存储
    /// </summary>
    public class FileDataHandler
    {
        private string _dataDirPath = "";
        private string _dataFileName = "";

        public FileDataHandler(string dataDirPath, string dataFileName)
        {
            this._dataDirPath = dataDirPath;
            this._dataFileName = dataFileName;
        }

        /// <summary>
        /// 数据保存
        /// </summary>
        /// <param name="data"></param>
        public void Save(GameData data)
        {
            string fullPath = Path.Combine(_dataDirPath, _dataFileName);

            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

                string dataToStore = JsonUtility.ToJson(data);
                using (FileStream fs = new FileStream(fullPath, FileMode.OpenOrCreate))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        sw.Write(dataToStore);
                    }
                }

            }
            catch (System.Exception ex)
            {
                Debug.LogException(ex);
            }
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        /// <returns></returns>
        public GameData Load()
        {
            string fullPath = Path.Combine(_dataDirPath, _dataFileName);

            if (File.Exists(fullPath))
            {
                try
                {
                    string dataToLoad = "";

                    using (FileStream fs = new FileStream(fullPath, FileMode.Open))
                    {
                        using (StreamReader sr = new StreamReader(fs))
                        {
                            dataToLoad = sr.ReadToEnd();
                        }
                    }

                    return JsonUtility.FromJson<GameData>(dataToLoad);
                }
                catch (System.Exception ex)
                {
                    Debug.LogException(ex);
                }
            }
            return null;
        }
    }
}