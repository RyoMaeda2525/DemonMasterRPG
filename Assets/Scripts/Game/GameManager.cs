using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    #region �ϐ�
    [SerializeField]
    CameraChange _cameraChange;
    [SerializeField]
    TacticsTree[] _tacticsTrees = null;
    [SerializeField]
    StatusSheet[] _ss;
    [SerializeField]
    PauseManager _pauseManager;
    #endregion

    #region �v���p�e�B
    /// <summary>�^�[�Q�b�g����ۂ̃J���������N���X</summary>
    public CameraChange CameraChange => _cameraChange;
    /// <summary>���ԃ����X�^�[�̍s���c���[</summary>
    public TacticsTree[] TacticsTrees => _tacticsTrees;
    /// <summary>�����X�^�[�̃X�e�[�^�X</summary>
    public StatusSheet[] StatusSheet => _ss;
    /// <summary>�Q�[���̈ꎞ��~���Ǘ�����N���X</summary>
    public PauseManager PauseManager => _pauseManager;
    #endregion

    /// <summary>�G�����X�^�[��|�����ۂɂ��̃����X�^�[�������Ă���o���l���l��</summary>
    public void GainExp(int exp) 
    {
        foreach (var pms in Player.Instance.MonstersStatus) 
        {
            pms.GetExp(exp);
        }
    }

    /// <summary>�����X�^�[���S���|���ꂽ���̃Q�[���I�[�o�[����</summary>
    public void GameOver()
    {
        UiManager.Instance.GameOver();
    }
}
