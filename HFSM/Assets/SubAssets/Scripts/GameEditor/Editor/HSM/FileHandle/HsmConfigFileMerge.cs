using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

namespace HSMTree
{

    public class HsmConfigFileMerge
    {
        public static void BeatchMergeAllFile()
        {
            string filePath = HsmDataController.Instance.FilePath;
            DirectoryInfo dInfo = new DirectoryInfo(filePath);
            FileInfo[] fileInfoArr = dInfo.GetFiles("*.txt", SearchOption.TopDirectoryOnly);

            List<PBConfigWriteFile> fileList = new List<PBConfigWriteFile>();
            for (int i = 0; i < fileInfoArr.Length; ++i)
            {
                string fullName = fileInfoArr[i].FullName;
                HSMReadWrite readWrite = new HSMReadWrite();
                HsmConfigTreeData skillHsmData = readWrite.ReadJson(fullName);
                skillHsmData = FormatData(skillHsmData);

                CheckDataTool.CheckData(skillHsmData);

                string fileName = System.IO.Path.GetFileNameWithoutExtension(fullName);
                //skillHsmData.FileName = fileName;

                string content = LitJson.JsonMapper.ToJson(skillHsmData);
                byte[] byteData = System.Text.Encoding.UTF8.GetBytes(content);
                if (byteData.Length <= 0)
                {
                    Debug.LogError("无效得配置文件");
                    return;
                }

                PBConfigWriteFile skillConfigWriteFile = new PBConfigWriteFile();
                skillConfigWriteFile.filePath = filePath;
                skillConfigWriteFile.byteData = byteData;
                fileList.Add(skillConfigWriteFile);

                Debug.Log("end mergeFile:" + filePath);
            }

            ByteBufferWrite bbw = new ByteBufferWrite();
            bbw.WriteInt32(fileList.Count);

            int start = 4 + fileList.Count * (4 + 4);
            for (int i = 0; i < fileList.Count; ++i)
            {
                PBConfigWriteFile skillConfigWriteFile = fileList[i];
                bbw.WriteInt32(start);
                bbw.WriteInt32(skillConfigWriteFile.byteData.Length);
                start += skillConfigWriteFile.byteData.Length;
            }

            for (int i = 0; i < fileList.Count; ++i)
            {
                PBConfigWriteFile skillHsmWriteFile = fileList[i];
                bbw.WriteBytes(skillHsmWriteFile.byteData, skillHsmWriteFile.byteData.Length);
            }

            {
                string mergeFilePath = CommonUtils.FileUtils.CombinePath(Application.dataPath, "StreamingAssets", "Bina", "SkillHsmConfig.bytes"); //string.Format("{0}/StreamingAssets/Bina/SkillHsmConfig.bytes", Application.dataPath);   // string.Format("{0}/MergeFile/SkillConfig.txt", path);

                if (System.IO.File.Exists(mergeFilePath))
                {
                    System.IO.File.Delete(mergeFilePath);
                    AssetDatabase.Refresh();
                }
                byte[] byteData = bbw.GetBytes();
                FileReadWrite.Write(mergeFilePath, byteData);
            }
        }

        private static HsmConfigTreeData FormatData(HsmConfigTreeData skillHsmData)
        {
            Dictionary<int, HsmConfigNode> nodeDic = new Dictionary<int, HsmConfigNode>();
            for (int i = 0; i < skillHsmData.nodeList.Count; ++i)
            {
                HsmConfigNode nodeValue = skillHsmData.nodeList[i];
                nodeDic[nodeValue.id] = nodeValue;
            }

            for (int i = 0; i < skillHsmData.nodeList.Count; ++i)
            {
                HsmConfigNode nodeValue = skillHsmData.nodeList[i];
                if (nodeValue.childIdList.Count <= 0)
                {
                    continue;
                }

                for (int j = 0; j < nodeValue.childIdList.Count; ++j)
                {
                    int childId = nodeValue.childIdList[j];
                    HsmConfigNode childNode = nodeDic[childId];
                    if (null == childNode)
                    {
                        continue;
                    }

                    childNode.parentId = nodeValue.id;
                }
            }

            return skillHsmData;
        }
    }
}


public class PBConfigWriteFile
{
    public string filePath;
    public byte[] byteData;
}