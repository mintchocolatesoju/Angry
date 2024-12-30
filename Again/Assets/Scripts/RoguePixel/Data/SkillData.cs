using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skill Data", menuName = "Skill Data", order=1)]
public class SkillData : ScriptableObject
{
    public string skillName;
    public int cooldown;
    public Sprite skillIcon;
    public AnimationClip skillAnimation;
}
