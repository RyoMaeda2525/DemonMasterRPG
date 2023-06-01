using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEditor;
using System.Security.Cryptography;
using System.Linq;

public class TrailObject : MonoBehaviour
{
    [SerializeField, Tooltip("行動選択先を示すのに使う")]
    GameObject _trailPrefab;

    TrailRenderer _trailRenderer;

    Material[] _materials;

    CharacterType _characterType;

    bool _complete = true;

    float _interval = 2.5f;

    float _timer = 0;

    private void Awake()
    {
        _materials = GameManager.Instance.Materials;
    }

    private void Start()
    {
        _trailRenderer = Instantiate(_trailPrefab, this.transform).GetComponent<TrailRenderer>();
        _characterType = GetComponentInParent<MonsterStatus>().CharacterType;
        Complete();
        _timer = _interval;
    }

    private void Complete() 
    {
        _trailRenderer.enabled = false;
        _trailRenderer.transform.position = this.transform.position;
    }

    public void Trail(Transform target , CharacterType type)
    {
        _timer += Time.deltaTime;

        if (_timer > _interval)
        {
            _timer = 0;
            _trailRenderer.enabled = true;

            if (_characterType != type)
            {
                //相手に対しての行動
                _trailRenderer.material = _materials[(int)_characterType];
            }
            else 
            {   
                //味方に対しての行動
                _trailRenderer.material = _materials.Last();
            }

            Vector3 centerPoint = Vector3.Lerp(this.transform.position, target.position, 0.5f);

            Vector3[] path = {
            this.transform.position,
            new Vector3(centerPoint.x,this.transform.position.y + 3f,centerPoint.z),
            target.position
            };

            _trailRenderer.transform.DOPath(path, 1f, PathType.CatmullRom)
                .SetEase(Ease.Linear)
                .SetLookAt(target.position)
                .OnComplete(() => Complete());
        }
    }

    public void Pause() //停止処理
    {
        transform.DOPause();
    }

    public void Resum() //再開
    {
        transform.DOPlay();
    }
}
