using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    
    [SerializeField]
    private TacticsManager _tacticsManager = null;
    [SerializeField]
    private CameraChange _cameraChange;
    [SerializeField]
    private MonsterSkill _monsterSkill = null;
    [SerializeField]
    private SkillManager _skillManager = null;
    [SerializeField]
    private SetStatus _setStatus = null;

    [SerializeField]
    StatusSheet[] _ss;

    public CameraChange CameraChange => _cameraChange;
    public TacticsManager TacticsManager => _tacticsManager;
    public MonsterSkill MonsterSkill => _monsterSkill;
    public SkillManager SkillManager => _skillManager;
    public SetStatus SetStatus => _setStatus;
    public StatusSheet[] StatusSheet => _ss;

    public void CriticalHit() { StartCoroutine(HitStop());  }

    private IEnumerator HitStop() 
    {
        Time.timeScale = 0.2f;
        yield return new WaitForSecondsRealtime(1.5f);
        Time.timeScale = 1f;
    }

    /// <summary>敵モンスターを倒した際にそのモンスターが持っている経験値を獲得</summary>
    public void GainExp(int exp) 
    {
        foreach (var pms in Player.Instance.MonsterStatus) 
        {
            pms.GetExp(exp);
        }
    }

    public void GameOver()
    {
        UiManager.Instance.GameOver();
    }
}
