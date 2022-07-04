using UnityEngine;
using HSMTree;

/// <summary>
/// 休息
/// </summary>
public class StateTakeReset : HSMStateBase, IHuman
{
    private Human human;

    public StateTakeReset() : base()
    {    }

    public override void OnEnter()
    {
        base.OnEnter();
        Debug.LogError("开始休息");
    }

    public override void OnExit()
    {
        Debug.LogError("结束休息");
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
        if (!human.MoveTo(PlaceEnum.Reset))
        {
            return;
        }

        human.Read(Random.Range(0.2f, 1));
        human.Homework(Random.Range(0.2f, 1));
        human.NetWrok(Random.Range(0.2f, 1));
        human.PlayGame(Random.Range(0.2f, 1));
        human.WatchTV(Random.Range(0.2f, 1));
        human.Basketball(Random.Range(0.2f, 1));
        human.Hunger(0);
        human.Enery(2);
    }
}
