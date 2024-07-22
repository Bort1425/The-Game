using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; //Static meaning that it refers to only this instance 
    public bool isGameOver = false;
    public List<GameObject> characterList;
    public GameObject playerOne; //So playerOne is the actual character in the scene that moves up and down
    public GameObject playerTwo;

    public GameObject playerOneCharacter; //Copy of a character that can move up and down (basically what players select in the first scene)
    public GameObject playerTwoCharacter;
    float initPosX = 5.5f;//initial posiition

    const int endingScore = 5;
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
