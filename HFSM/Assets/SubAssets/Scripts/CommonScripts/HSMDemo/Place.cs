using System.Collections.Generic;
using UnityEngine;

public enum PlaceEnum
{
    Reset = 0,      // 休息的地方
    Eat = 1,        // 吃东西的地方
    PlayGame = 2,   // 打游戏的地方
    WatchTV = 3,    // 看电视的地方
    Basketball = 4, // 打篮球的地方
    NetWork = 5,    // 网上听课的地方
    Read  = 6,      // 看书的地方
    HomeWork = 7,   // 写作业的地方
}

public class Place : MonoBehaviour
{
    public static Place Instance = null;
    public Dictionary<PlaceEnum, Vector3> _placeDic = new Dictionary<PlaceEnum, Vector3>();
    void Start()
    {
        Instance = this;

        _placeDic[PlaceEnum.Reset] = Create("休息区", new Vector3(-12, 0, 5), new Vector3(90, 0, 0));
        _placeDic[PlaceEnum.Eat] = Create("吃饭", new Vector3(-12, 0, 0), new Vector3(90, 0, 0));
        _placeDic[PlaceEnum.PlayGame] = Create("玩游戏", new Vector3(10, 0, 5), new Vector3(90, 0, 0));
        _placeDic[PlaceEnum.WatchTV] = Create("看电视", new Vector3(10, 0, 0), new Vector3(90, 0, 0));
        _placeDic[PlaceEnum.Basketball] = Create("打篮球", new Vector3(10, 0, -5), new Vector3(90, 0, 0));
        _placeDic[PlaceEnum.NetWork] = Create("网上听课", new Vector3(0, 0, 5), new Vector3(90, 0, 0));
        _placeDic[PlaceEnum.Read] = Create("读书", new Vector3(0, 0, 0), new Vector3(90, 0, 0));
        _placeDic[PlaceEnum.HomeWork] = Create("写作业", new Vector3(0, 0, -5), new Vector3(90, 0, 0));
    }

    private Vector3 Create(string name, Vector3 pos, Vector3 rot)
    {
        GameObject go = new GameObject(name);
        go.transform.position = pos;
        go.transform.rotation = Quaternion.Euler(rot);
        go.transform.localScale = Vector3.one * 0.1f;

        TextMesh textMesh = go.AddComponent<TextMesh>();
        if (null != textMesh)
        {
            textMesh.text = name;
            textMesh.fontSize = 100;
        }

        return go.transform.position;
    }
}
