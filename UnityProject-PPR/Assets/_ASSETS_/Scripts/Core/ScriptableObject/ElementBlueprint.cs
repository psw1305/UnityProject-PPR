using PSW.Core.Enums;
using UnityEngine;

[CreateAssetMenu(fileName = "Element", menuName = "Blueprint/Element")]
public class ElementBlueprint : ScriptableObject
{
    [SerializeField] private Sprite elementImage;
    [SerializeField] private Color elementColor;
    [SerializeField] private Sprite elementCase;
    [SerializeField] private ElementType elementType;
    [SerializeField] private float randomWeighted;

    public Sprite ElementImage => this.elementImage;
    public Color ElementColor => this.elementColor;
    public Sprite ElementCaseImage => this.elementCase;
    public ElementType ElementType => this.elementType;
    public float RandomWeighted => this.randomWeighted;
}
