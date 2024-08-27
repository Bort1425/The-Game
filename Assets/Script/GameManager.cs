using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; //Static meaning that it refers to only this instance 
    public bool isGameOver = false;
    public List<GameObject> characterList;

    public int playerOneCharacterIndex = 0;
    public int playerTwoCharacterIndex = 0;

    public SpriteRenderer playerOneSR;
    public SpriteRenderer playerTwoSR;

    public TextMeshProUGUI playerOneNameText;
    public TextMeshProUGUI playerTwoNameText;
    public GameObject playerOne; //So playerOne is the actual character in the scene that moves up and down
    public GameObject playerTwo;

    public GameObject playerOneCharacter; //Copy of a character that can move up and down (basically what players select in the first scene)
    public GameObject playerTwoCharacter;
    float initPosX = 5.5f;//initial posiition
    const int endingScore = 100;
    bool winner;
    float ballRespawnDelay = 0.6f;
    public GameObject ballPrefab;
    //Like start but happens before start

    void Awake(){ 
        if(instance == null){//If there isn't a gameManager make me the gameManager
            instance = this; 
        }else if(instance != this){ //If there's an instance that's not myself...destroy myself
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);//refers to this.gameObject

    }
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded; //when the scene loads we check this function...and "OnSceneLoaded" is a reference to the method
        
               //StartMatch();
    }

   public void CycleUp(bool playerIdentity){
        if(playerIdentity == false){//false ---> left player
        playerOneCharacterIndex--;
            if(playerOneCharacterIndex == -1){
                playerOneCharacterIndex = characterList.Count- 1;         
            }
        }
        else{
            playerTwoCharacterIndex--;
            if(playerTwoCharacterIndex == -1){
                playerTwoCharacterIndex = characterList.Count -1;
            }
        }
        UpdateCharacterSelected(playerIdentity);
    }

    public void CycleDown(bool playerIdentity){
        if(playerIdentity == false){
            playerOneCharacterIndex++;
            if(playerOneCharacterIndex >= characterList.Count){
                playerOneCharacterIndex = 0;
                
            }
        }
        else{
            playerTwoCharacterIndex++;
            if(playerTwoCharacterIndex >= characterList.Count){
                playerTwoCharacterIndex = 0;
            }
        }
        UpdateCharacterSelected(playerIdentity);
    }
    void UpdateCharacterSelected(bool playerIdentity){
        if(playerIdentity == false){
            //change name 
            playerOneNameText.text = characterList[playerOneCharacterIndex].name;
            //the sprite
            playerOneSR.sprite = characterList[playerOneCharacterIndex].GetComponent<SpriteRenderer>().sprite;//change the character in the box to what i want through the sprite renderer
            //change character selected
            playerOneCharacter = characterList[playerOneCharacterIndex];
        }else{
            //change name 
            playerTwoNameText.text = characterList[playerTwoCharacterIndex].name;
            //the sprite
            playerTwoSR.sprite = characterList[playerTwoCharacterIndex].GetComponent<SpriteRenderer>().sprite;//change the character in the box to what i want through the sprite renderer
            //change character selected
            playerTwoCharacter = characterList[playerTwoCharacterIndex];
        }
    }
    

    void OnSceneLoaded(Scene theSceneInQuestion, LoadSceneMode loadSceneModeInQuestion){

        Scene currentScene = SceneManager.GetActiveScene();//gets the scene that's loaded and stores it in the current scene
        string sceneName = currentScene.name;//sets the current scene's name to "Start" b/c we are in the start scene
        Debug.Log(sceneName);
        if(sceneName == "Game"){ 
            InitializeMatch();  
        }
    }
    void InitializeMatch(){
        isGameOver = false;
        GameData.PlayerOneScore = 0;
        GameData.PlayerTwoScore = 0;

        InitPlayers();
    }
    void StartMatch(){
        
    }
//GameObject.Instantiate(prefab, position, Quaternion.identity-rotation);
    void InitPlayers(){
        playerOne = GameObject.Instantiate(playerOneCharacter, new Vector3(-initPosX, 0, 0), Quaternion.identity);
        playerTwo = GameObject.Instantiate(playerTwoCharacter, new Vector3(initPosX, 0, 0), Quaternion.identity);
        playerTwo.GetComponent<SpriteRenderer>().flipX = true; //make sure player 2 is facing the corect way 
        
        playerOne.GetComponent<PlayerMovement>().verticalInputAxis = "P1_Vertical";
        playerOne.GetComponent<PlayerMovement>().rotateInputAxis = "P1_Rotate";

        playerTwo.GetComponent<PlayerMovement>().verticalInputAxis = "P2_Vertical";
        playerTwo.GetComponent<PlayerMovement>().rotateInputAxis = "P2_Rotate";
    }

    public void UpdateScore(bool goalSide, int scoreAmount){ 
        if(!isGameOver){//if gameover is false
            if(goalSide == true){
                GameData.PlayerOneScore += scoreAmount;
            }else {
                GameData.PlayerTwoScore += scoreAmount;
            }    
            TryEndMatch();
            StartCoroutine(RespawnBall());
        }

        //yeah 5 cause then ppl will be like "naah nahhh again again"
       
    } 

    public void TryEndMatch(){
     if(GameData.PlayerOneScore >= endingScore){
            winner = true;//Winner = true means p1 won and w=F means p2 won
            EndMatch();
        }else if(GameData.PlayerTwoScore >= endingScore){
            winner = false;
            EndMatch();
        }
    }

    public void EndMatch(){
        isGameOver = true;
    }

    IEnumerator RespawnBall(){//public IEnumerator means you have to return an IEN
        yield return new WaitForSeconds(ballRespawnDelay);
        if(!isGameOver){
            GameObject.Instantiate(ballPrefab, Vector3.zero, Quaternion.identity);//QI - rotation of 0,0,0 
        }
    }

    public static void LoadGameScene(){
        SceneManager.LoadScene(1);
    }
    

}
