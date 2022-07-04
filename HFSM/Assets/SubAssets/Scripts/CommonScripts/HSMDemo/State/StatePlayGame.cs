using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HSMTree;

/// <summary>
/// 玩游戏
/// </summary>
public class StatePlayGame : HSMStateBase, IHuman
{
    private Human human;
    public StatePlayGame() : base()
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
        if (!human.MoveTo(PlaceEnum.PlayGame))
        {
            return;
        }

        human.Read(Random.Range(0.2f, 0.5F));
        human.Homework(Random.Range(0.2f, 0.5F));
        human.NetWrok(Random.Range(0.2f, 0.5F));
        human.PlayGame(-2);
        human.WatchTV(0);
        human.Basketball(0);
        human.Hunger(0.5f);
        human.Enery(-0.5f);
    }
}
