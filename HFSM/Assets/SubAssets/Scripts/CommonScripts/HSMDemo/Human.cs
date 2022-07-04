using System.Collections;
using UnityEngine;
using HSMTree;

public interface IHuman
{
    void SetHuman(Human human);
}

public class Human : MonoBehaviour
{
    // 层次状态机实例
    private HsmEntity hsmEntity;

    private float read = 0;        // 看书
    private float homework = 0;    // 写作业
    private float network = 0;     // 网上听课
    private float playGame = 0;    // 玩游戏
    private float watchTV = 0;     // 看电视
    private float basketball = 0;  // 打篮球
    private float senseHunger = 10;// 饥饿感
    private float energy = 0;      // 精力

    void Start()
    {
        // 加载配置文件
        StartCoroutine(HsmTreeData.Instance.LoadConfig());
        StartCoroutine(LoadConfigData());
    }

    void Update()
    {
        // 每帧执行层次状态机逻辑
        if (null != hsmEntity)
        {
            hsmEntity.Execute();
        }
    }

    // 加载配置文件
    private IEnumerator LoadConfigData()
    {
        yield return new WaitForSeconds(1);

        // 根据自己配置的 HSM 配置文件名 "Student" 加载数据 
        HsmConfigTreeData hsmConfigData = HsmTreeData.Instance.GetHsmInfo("Student");
        // 将读取到的 配置文件数据解析为 层次状态机类对象
        hsmEntity = new HsmEntity(hsmConfigData);

        // 遍历所有节点，将 Human 赋值给自定义的状态
        foreach (var kv in hsmEntity.HsmStateMachine.AllNode())
        {
            HSMStateBase abstractNode = kv.Value;
            if (typeof(IHuman).IsAssignableFrom(abstractNode.GetType()))
            {
                IHuman iHuman = abstractNode as IHuman;
                // 将 Human 赋值给自定义的状态
                iHuman.SetHuman(this);
            }
        }
    }

    // 移动
    public bool MoveTo(PlaceEnum place)
    {
        Vector3 desirtPos = Place.Instance._placeDic[place];
        if (Vector3.Distance(transform.position, desirtPos) <= 0.5f)
        {
            return true;
        }

        transform.Translate((desirtPos - transform.position).normalized * Time.deltaTime * 5, Space.World);
        return false;
    }

    public void Read(float value)
    {
        read = Mathf.Clamp(read + value * Time.deltaTime, 0, 10);
        // 更新环境变量的值
        hsmEntity.ConditionCheck.SetParameter("read", read);
    }

    public void Homework(float value)
    {
        homework = Mathf.Clamp(homework + value * Time.deltaTime, 0, 10);
        // 更新环境变量的值
        hsmEntity.ConditionCheck.SetParameter("homework", homework);
    }

    public void NetWrok(float value)
    {
        network = Mathf.Clamp(network + value * Time.deltaTime, 0, 10);
        // 更新环境变量的值
        hsmEntity.ConditionCheck.SetParameter("network", network);
    }

    public void PlayGame(float value)
    {
        playGame = Mathf.Clamp(playGame + value * Time.deltaTime, 0, 10);
        // 更新环境变量的值
        hsmEntity.ConditionCheck.SetParameter("playGame", playGame);
    }

    public void WatchTV(float value)
    {
        watchTV = Mathf.Clamp(watchTV + value * Time.deltaTime, 0, 10);
        // 更新环境变量的值
        hsmEntity.ConditionCheck.SetParameter("watchTV", watchTV);
    }

    public void Basketball(float value)
    {
        basketball = Mathf.Clamp(basketball + value * Time.deltaTime, 0, 10);
        // 更新环境变量的值
        hsmEntity.ConditionCheck.SetParameter("basketball", basketball);
    }

    public void Hunger(float value)
    {
        senseHunger = Mathf.Clamp(senseHunger + value * Time.deltaTime, 0, 10);
        // 更新环境变量的值
        hsmEntity.ConditionCheck.SetParameter("senseHunger", senseHunger);
    }

    public void Enery(float value)
    {
        energy = Mathf.Clamp(energy + value * Time.deltaTime, 0, 10);
        // 更新环境变量的值
        hsmEntity.ConditionCheck.SetParameter("energy", energy);
    }
}