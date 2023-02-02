using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    [SerializeField]
    SkillAssets[] _skillAssets = null;

    public SkillAssets[] SkillAssets => _skillAssets;
}
