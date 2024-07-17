using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Goal : MonoBehaviour
{
    public bool goalSide;//If it's true it's the right side - False is left side
    public TextMeshProUGUI scoreTextComponent;//

    void Start(){
        scoreTextComponent.text = "0";
    }
    void OnTriggerEnter2D(Collider2D other){//other - basically the ball
        if(other.CompareTag("Ball")){//does it have tag "Ball"?
            Destroy(other.gameObject);//heheh destroy it
            if(goalSide == true){
                GameManager.instance.UpdateScore(goalSide, 1);
                scoreTextComponent.text = GameData.PlayerOneScore + "";
            }else{
                GameManager.instance.UpdateScore(goalSide, 1);
                scoreTextComponent.text = GameData.PlayerTwoScore + "";
            }
        }
    }


}
