using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] Character selectedCharacter;
    [SerializeField] List<Character> characterList;
    [SerializeField] Transform atkRef;
    [SerializeField] bool isBot;
    [SerializeField] UnityEvent onTakeDamage;
    public Character SelectedCharacter { get => selectedCharacter;}
    public List<Character> CharacterList { get => characterList;}

    private void Start()
    {
        if (isBot)
        {
            foreach (var character in characterList)
            {
                character.Button.interactable = false;
            }
        }
    }
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
        if( isBot )
        {
            List<Character> chanceList = new List<Character>();
            foreach (var character in characterList)
            {
                int chance = Mathf.CeilToInt(((float)character.CurrentHP/(float)character.MaxHP));
                for(int i=0; i< chance; i++)
                {
                    chanceList.Add(character);
                }
            };
            int index = Random.Range(0,chanceList.Count);
            selectedCharacter = chanceList[index];
        }
        else
        {
            foreach (var character in characterList)
            {
                character.Button.interactable = value;
            }
            
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
        if(selectedCharacter == null)
            return false;
        
        return DOTween.IsTweening(selectedCharacter.transform);
    }

    internal void TakeDamage(int damageValue)
    {
        selectedCharacter.ChangeHP(-damageValue);
        var spriteRend = selectedCharacter.GetComponent<SpriteRenderer>();
        spriteRend
            .DOColor(Color.red,0.05f)
            .SetLoops(6, LoopType.Yoyo) ;
        
        onTakeDamage.Invoke();
    }
    public bool isDamaging()
    {
        
        if(selectedCharacter == null)
            return false;
        
        var spriteRend = SelectedCharacter.GetComponent<SpriteRenderer>();
        return DOTween.IsTweening(spriteRend);
    }

    public void Remove(Character character)
    {
        if(characterList.Contains(character) == false)
            return;

        if(selectedCharacter == character)
           selectedCharacter = null;

        character.Button.interactable = false;
        character.gameObject.SetActive(false);
        characterList.Remove(character);
    }

    public void Return()
    {
        selectedCharacter.transform
            .DOMove(selectedCharacter.InitialPosition, 0.7f)
            .SetEase(Ease.OutCirc);
    }

    public bool IsReturning()
    {
        if(selectedCharacter == null)
            return false;
        
        return DOTween.IsTweening(selectedCharacter.transform);
    }
}
