using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HSMTree;

/// <summary>
/// 网上听课 
/// </summary>
public class StateNetWork : HSMStateBase, IHuman
{
    private Human human;
    public StateNetWork() : base()
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();
        Debug.LogError("开始网上听课");
    }

    public override void OnExit()
    {
        Debug.LogError("结束网上听课");
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
        if (!human.MoveTo(PlaceEnum.NetWork))
        {
            return;
        }

        human.Read(0);
        human.Homework(0);
        human.NetWrok(-2);
        human.PlayGame(Random.Range(0.2f, 0.5F));
        human.WatchTV(Random.Range(0.2f, 0.5F));
        human.Basketball(Random.Range(0.2f, 0.5F));
        human.Hunger(0.5f);
        human.Enery(-0.5f);
    }
}
