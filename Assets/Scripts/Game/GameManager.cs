using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    #region 変数
    [SerializeField]
    CameraChange _cameraChange;
    [SerializeField]
    TacticsTree[] _tacticsTrees = null;
    [SerializeField]
    StatusSheet[] _ss;
    [SerializeField]
    PauseManager _pauseManager;
    #endregion

    #region プロパティ
    /// <summary>ターゲットする際のカメラ処理クラス</summary>
    public CameraChange CameraChange => _cameraChange;
    /// <summary>仲間モンスターの行動ツリー</summary>
    public TacticsTree[] TacticsTrees => _tacticsTrees;
    /// <summary>モンスターのステータス</summary>
    public StatusSheet[] StatusSheet => _ss;
    /// <summary>ゲームの一時停止を管理するクラス</summary>
    public PauseManager PauseManager => _pauseManager;
    #endregion

    /// <summary>敵モンスターを倒した際にそのモンスターが持っている経験値を獲得</summary>
    public void GainExp(int exp) 
    {
        foreach (var pms in Player.Instance.MonstersStatus) 
        {
            pms.GetExp(exp);
        }
    }

    /// <summary>モンスターが全員倒された時のゲームオーバー処理</summary>
    public void GameOver()
    {
        UiManager.Instance.GameOver();
    }
}
