using UnityEngine;

public class ControlStep : MonoBehaviour
{
    public float value;
    [SerializeField] private GameObject fill = null;

    public void Up()
    {
        fill.SetActive(true);
    }

    public void Down()
    {
        fill.SetActive(false);
    }
}
