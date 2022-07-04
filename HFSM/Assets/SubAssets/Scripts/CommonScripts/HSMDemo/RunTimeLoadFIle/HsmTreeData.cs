using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HSMTree;
using LitJson;

public class HsmTreeData
{
    public static readonly HsmTreeData Instance = new HsmTreeData();

    #region  HsmTreeData
    private Dictionary<string, HsmConfigTreeData> _behaviorDic = new Dictionary<string, HsmConfigTreeData>();
    public void LoadData(byte[] loadByteData)
    {
        AnalysisBin.AnalysisData(loadByteData, Analysis);
    }

    private void Analysis(byte[] byteData)
    {
        string content = System.Text.Encoding.Default.GetString(byteData);
        HsmConfigTreeData skillHsmData = JsonMapper.ToObject<HsmConfigTreeData>(content);
        _behaviorDic[skillHsmData.fileName] = skillHsmData;
    }

    public HsmConfigTreeData GetHsmInfo(string handleFile)
    {
        HsmConfigTreeData hsmTreeData = null;
        if (_behaviorDic.TryGetValue(handleFile, out hsmTreeData))
        {
            return hsmTreeData;
        }

        return hsmTreeData;
    }

    public IEnumerator LoadConfig()
    {
        string filePath = CommonUtils.FileUtils.GetStreamingAssetsFilePath("SkillHsmConfig.bytes", "Bina");
        WWW www = new WWW(filePath);
        yield return www.isDone;

        byte[] byteData = www.bytes;
        LoadData(byteData);
    }

    #endregion

}
