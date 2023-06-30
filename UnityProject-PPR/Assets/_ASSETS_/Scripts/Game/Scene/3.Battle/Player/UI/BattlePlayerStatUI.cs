using PSW.Core.Extensions;
using UnityEngine;
using TMPro;

public class BattlePlayerStatUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI actText;
    [SerializeField] private TextMeshProUGUI atkText;
    [SerializeField] private TextMeshProUGUI defText;

    public void SetText(int act, int atk, int def)
    {
        this.actText.text = act.ToString();
        this.atkText.text = atk.ToString();
        this.defText.text = def.ToString();
    }

    public void SetActText(int act)
    {
        this.actText.text = act.ToString();
    }

    public void UpdateAnimateUI(int oldPoint, int newPoint)
    {
         StartCoroutine(this.actText.UpdateTextCoroutine(oldPoint, newPoint, 1.0f));
    }
}
