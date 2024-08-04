using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*public struct Character{//a data structure for the thingy
    
    public Character(SpriteRenderer sprite, string name){
        s = sprite;
        n = name;
    }

    public SpriteRenderer s{get;}//{get;} --> It lets us do "Character.s" which actually be the sprite renderer
    public string n{get;}
}*/

public class CharacterSelect : MonoBehaviour
{

    public List<GameObject> characterList;
    public SpriteRenderer characterSprite;//gives a reference to the sprite renderer that's attached to the character frame
    public int characterLength;
    public int currentCharacter;
    // Start is called before the first frame update
    void Start()
    {
        characterLength = characterList.Count;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Navigate(bool direction){//up is true down is false
        if(direction){
            currentCharacter--;
            if(currentCharacter == 0){
                characterLength -= 1;
            } else {
                currentCharacter--;
            }
        }
    }
}
