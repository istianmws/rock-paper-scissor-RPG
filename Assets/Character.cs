using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    [SerializeField] new string name;
    [SerializeField] CharacterType type;
    [SerializeField] int currentHP;
    [SerializeField] int maxHP;
    [SerializeField] int attackPower;
    [SerializeField] TMP_Text overHeadText;
    [SerializeField] Image avatar;
    [SerializeField] TMP_Text nameText;
    [SerializeField] TMP_Text typeText;
    [SerializeField] Image healthBar;
    [SerializeField] TMP_Text hpText;
    [SerializeField] Button button;

    public Button Button { get => button; }
    public CharacterType Type { get => type; set => type = value; }
    public int AttackPower { get => attackPower; set => attackPower = value; }
    public int CurrentHP { get => CurrentHP1; set => CurrentHP1 = value; }
    public int CurrentHP1 { get => currentHP; set => currentHP = value; }

    private void Start() {
        overHeadText.text = name;
        nameText.text = name;
        typeText.text = Type.ToString();
        UpdateHpUI();
        button.interactable = false;
    }

    public void ChangeHP(int amount)
    {
        CurrentHP1 += amount;
        CurrentHP1 = Mathf.Clamp(CurrentHP1,0, maxHP);
        UpdateHpUI();
    }

    private void UpdateHpUI()
    {
        healthBar.fillAmount = (float) CurrentHP1 / (float)maxHP;
        hpText.text = CurrentHP1 + "/" + maxHP;
    }
}
