using PSW.Core.Enums;
using UnityEngine;

[CreateAssetMenu(fileName = "EL_Skill_", menuName = "Blueprint/Element/Element_Skill")]
public class ElementSkillBlueprint : ElementBlueprint
{
    [Header("Skill")]
    [SerializeField] private string skillName;
    [SerializeField] ElementSkillType skillType;

    public string SkillName => this.skillName;
    public ElementSkillType SkillType => this.skillType;
    public override ElementDetailType ElementDetailType => ElementDetailType.Skill;
}
