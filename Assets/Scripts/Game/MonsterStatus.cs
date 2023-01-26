using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MonsterStatus : MonsterBase
{
    Player player;

    protected override void Start()
    {
        base.Start();
        if (this.CompareTag("PlayerMonster"))
        {
            UiManager.Instance.MonsterPanel.MonsterPanalSet(this);
        }
    }
}
