using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Player : MonoBehaviour
{
    [SerializeField] Character selectedCharacter;
    [SerializeField] List<Character> characterList;
    [SerializeField] Transform atkRef;
    public Character SelectedCharacter { get => selectedCharacter;}

    public void Prepare()
    {
        selectedCharacter = null;
    }

    public void SelectCharacter(Character character)
    {
        selectedCharacter = character;
    }

    public void SetPlay(bool value)
    {
        foreach (var character in characterList)
        {
            character.Button.interactable = value;
        }
    }

    private void Update() 
    {

    }
    public void Attack()
    {
        selectedCharacter.transform
            .DOMove(atkRef.position, 1)
            .SetEase(Ease.InCirc);
    }

    public bool IsAttacking()
    {
        return DOTween.IsTweening(selectedCharacter.transform);
    }

    internal void TakeDamage(int damageValue)
    {
        selectedCharacter.ChangeHP(-damageValue);
        var spriteRend = selectedCharacter.GetComponent<SpriteRenderer>();
        spriteRend
            .DOColor(Color.red,0.1f)
            .SetLoops(6, LoopType.Yoyo) ;
    }
    public bool isDamaging()
    {
        var spriteRend = SelectedCharacter.GetComponent<SpriteRenderer>();
        return DOTween.IsTweening(spriteRend);
    }

    internal void Remove(Character character)
    {
        if(characterList.Contains(character) == false)
            return;
            
        selectedCharacter.Button.interactable = false;
        selectedCharacter.gameObject.SetActive(false);
        characterList.Remove(selectedCharacter);
    }
}
