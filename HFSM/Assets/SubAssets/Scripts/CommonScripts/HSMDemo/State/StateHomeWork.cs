using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HSMTree;

/// <summary>
/// 写作业
/// </summary>
public class StateHomeWork : HSMStateBase, IHuman
{
    private Human human;
    public StateHomeWork() : base()
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();
        Debug.LogError("开始写作业");
    }

    public override void OnExit()
    {
        Debug.LogError("结束写作业");
    }

    public void SetHuman(Human human)
    {
        this.human = human;
    }

    /// <summary>
    /// 执行节点抽象方法
    /// </summary>
    /// <returns>返回执行结果</returns>
    public override void Execute()
    {
        if (!human.MoveTo(PlaceEnum.HomeWork))
        {
            return;
        }

        human.Read(0);
        human.Homework(-2);
        human.NetWrok(0);
        human.PlayGame(Random.Range(0.2f, 0.5F));
        human.WatchTV(Random.Range(0.2f, 0.5F));
        human.Basketball(Random.Range(0.2f, 0.5F));
        human.Hunger(0.5f);
        human.Enery(-0.5f);
    }

}
