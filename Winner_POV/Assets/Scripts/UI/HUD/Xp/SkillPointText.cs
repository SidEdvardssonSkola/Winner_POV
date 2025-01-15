using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillPointText : MonoBehaviour
{
    [SerializeField] private XpSystem xpManager;
    private TextMeshProUGUI text;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void Awake()
    {
        xpManager.onLevelUp.AddListener(UpdateText);
    }

    public void UpdateText()
    {
        if (xpManager.levelUpPoints > 0)
        {
            text.text = "Unspent Skill Points: " + xpManager.levelUpPoints;
        }
        else
        {
            text.text = "";
        }
    }

    private void OnDisable()
    {
        xpManager.onLevelUp.RemoveListener(UpdateText);
    }
}
