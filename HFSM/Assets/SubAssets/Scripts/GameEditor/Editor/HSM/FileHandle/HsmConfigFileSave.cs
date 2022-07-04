using UnityEngine;
using System.IO;
using UnityEditor;

namespace HSMTree
{
    public class HsmConfigFileSave
    {
        public static void CreateSaveFile()
        {
            string fileName = HsmDataController.Instance.FileName;
            HsmConfigTreeData hsmConfigTreeData = HsmDataController.Instance.HSMTreeData;
            if (hsmConfigTreeData == null)
            {
                Debug.LogError("rootNode is null");
                return;
            }

            string path = HsmDataController.Instance.GetFilePath(fileName);
            if (File.Exists(path))
            {
                if (!EditorUtility.DisplayDialog("已存在文件", "是否替换新文件", "替换", "取消"))
                {
                    return;
                }
            }

            // 如果没有该路径，创建一个
            string directory = Path.GetDirectoryName(path);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            string name = System.IO.Path.GetFileNameWithoutExtension(path);
            CheckDataTool.CheckData(hsmConfigTreeData);

            HSMReadWrite readWrite = new HSMReadWrite();
            hsmConfigTreeData.fileName = System.IO.Path.GetFileNameWithoutExtension(path);
            readWrite.WriteJson(hsmConfigTreeData, path);
        }
    }
}
