using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEditor;

public class TrailObject : MonoBehaviour
{
    [SerializeField, Tooltip("s“®‘I‘ğæ‚ğ¦‚·‚Ì‚Ég‚¤")]
    private GameObject _trailPrefab;

    TrailRenderer _trailRenderer;

    bool _complete = true;

    float _interval = 2.5f;

    float _timer = 0;

    private void Start()
    {
        _trailRenderer = Instantiate(_trailPrefab, this.transform).GetComponent<TrailRenderer>();
        Complete();
        _timer = _interval;
    }

    private void Complete() 
    {
        _trailRenderer.enabled = false;
        _trailRenderer.transform.position = this.transform.position;
    }

    public void Trail(Transform target)
    {
        _timer += Time.deltaTime;

        if (_timer > _interval)
        {
            _timer = 0;
            _trailRenderer.enabled = true;

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
}
