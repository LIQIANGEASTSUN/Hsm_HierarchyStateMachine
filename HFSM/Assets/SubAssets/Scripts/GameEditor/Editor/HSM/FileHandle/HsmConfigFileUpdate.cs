using UnityEngine;
using System.IO;

namespace HSMTree
{

    public class HsmConfigFileUpdate
    {
        public static void UpdateAllFile()
        {
            string filePath = HsmDataController.Instance.FilePath;
            DirectoryInfo dInfo = new DirectoryInfo(filePath);
            FileInfo[] fileInfoArr = dInfo.GetFiles("*.txt", SearchOption.TopDirectoryOnly);
            for (int i = 0; i < fileInfoArr.Length; ++i)
            {
                string fullName = fileInfoArr[i].FullName;
                HSMReadWrite readWrite = new HSMReadWrite();
                HsmConfigTreeData skillHsmData = readWrite.ReadJson(fullName);

                string fileName = System.IO.Path.GetFileNameWithoutExtension(fullName);
                skillHsmData.fileName = fileName;

                CheckDataTool.CheckData(skillHsmData);

                skillHsmData = UpdateData(skillHsmData);

                string jsonFilePath = CommonUtils.FileUtils.CombinePath(filePath, "Json", System.IO.Path.GetFileName(fullName));// System.IO.Path.GetDirectoryName(filePath) + "/Json/" + System.IO.Path.GetFileName(fullName);
                jsonFilePath = System.IO.Path.ChangeExtension(jsonFilePath, "txt");
                bool value = readWrite.WriteJson(skillHsmData, jsonFilePath);
                if (!value)
                {
                    Debug.LogError("WriteError:" + jsonFilePath);
                }
            }
        }

        public static HsmConfigTreeData UpdateData(HsmConfigTreeData skillHsmData)
        {
            return skillHsmData;
        }

    }
}