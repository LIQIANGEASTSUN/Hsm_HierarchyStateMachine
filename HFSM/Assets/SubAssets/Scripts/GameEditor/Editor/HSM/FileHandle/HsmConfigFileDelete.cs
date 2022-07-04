using System.IO;
using UnityEditor;

namespace HSMTree
{
    public class HsmConfigFileDelete
    {

        public static void DeleteFile()
        {
            string fileName = HsmDataController.Instance.FileName;
            string path = HsmDataController.Instance.GetFilePath(fileName);
            if (!File.Exists(path))
            {
                if (!EditorUtility.DisplayDialog("提示", "文件不存在", "yes"))
                { }
                return;
            }

            File.Delete(path);
        }

    }
}

