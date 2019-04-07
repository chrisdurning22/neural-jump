using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelOnePlayer : MonoBehaviour {

    private bool onGround;
    private const float JUMPING_VELOCITY = 22f;
    private const float NO_VELOCITY = 0f;
    private const float RUNNING_VELOCITY = 8.8f;
    private const float THRUST = 2f;
    private const int FINISH_LOCATION_X_AXIS = 1797;
    private const int IDLE_ANIMATION = 2;
    private const int JUMPING_ANIMATION = 1;
    private const int RUNNING_ANIMATION = 0;
    private const string GROUND_TAG = "Ground";
    private const string LEVEL_COMPLETE_MESSAGE = "Level Complete";
    private const string PLAYER_TAG = "Player";
    private const string SPACE = "space";

    private Animator animator;
    private GameObject player;
    private Rigidbody2D body2D;
    public Text txt;

    void Start() {
        
        animator = GetComponent<Animator>();
        body2D = GetComponent<Rigidbody2D>();
        onGround = true;
        player = GameObject.FindWithTag(PLAYER_TAG);
        txt.text = "";   
    }

    void Update() {
        
        if(onGround) {
            
            PlayerUtility.PlayerAction(body2D, RUNNING_VELOCITY, NO_VELOCITY);
            PlayerUtility.ChangeAnimation(animator, RUNNING_ANIMATION);
        }

        if (PlayerUtility.CanJump(onGround, Input.GetKey(SPACE))) {
            
            PlayerUtility.PlayerAction(body2D, RUNNING_VELOCITY, JUMPING_VELOCITY);
            body2D.AddForce((Vector2.right * RUNNING_VELOCITY) * THRUST);
        }

        if (player.transform.position.x >= FINISH_LOCATION_X_AXIS) {
            
            PlayerUtility.PlayerAction(body2D, NO_VELOCITY, NO_VELOCITY);
            PlayerUtility.ChangeAnimation(animator, IDLE_ANIMATION);
            txt.text = LEVEL_COMPLETE_MESSAGE;
        }   
    }    

    private void OnCollisionEnter2D(Collision2D collision) {
        
        if (collision.gameObject.CompareTag(GROUND_TAG)) {
            
            onGround = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        
        onGround = false;
        PlayerUtility.ChangeAnimation(animator, JUMPING_ANIMATION);
    }
}
