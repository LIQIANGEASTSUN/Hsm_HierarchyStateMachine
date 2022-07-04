using UnityEngine;
using System.IO;
using UnityEditor;
using GraphicTree;

namespace HSMTree
{
    public class HsmConfigFileLoad
    {

        public static void LoadFile()
        {
            string filePath = HsmConfigFileSelect.SelectFile();
            if (string.IsNullOrEmpty(filePath))
            {
                return;
            }

            string fileName = System.IO.Path.GetFileNameWithoutExtension(filePath);
            LoadFile(fileName);
        }

        public static void LoadFile(string fileName)
        {
            string path = HsmDataController.Instance.GetFilePath(fileName);
            if (!File.Exists(path))
            {
                if (!EditorUtility.DisplayDialog("提示", "文件不存在", "yes"))
                { }
                return;
            }

            HSMReadWrite readWrite = new HSMReadWrite();
            HsmConfigTreeData HSMTreeData = readWrite.ReadJson(path);
            if (null == HSMTreeData)
            {
                Debug.LogError("file is null:" + fileName);
                return;
            }

            HsmDataController.Instance.PlayType = HSMPlayType.STOP;
            NodeNotify.SetPlayState((int)HsmDataController.Instance.PlayType);

            HsmDataController.Instance.FileName = fileName;
            HsmDataController.Instance.HSMTreeData = HSMTreeData;
            HsmDataController.Instance.CurrentOpenSubMachineId = -1;

            HSMRunTime.Instance.Reset(HSMTreeData);
        }
    }
}

