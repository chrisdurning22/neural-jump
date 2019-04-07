using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerUtility : MonoBehaviour {

    public static bool CanJump(bool isOnGround, bool isSpacePressed)
    {

        return isOnGround && isSpacePressed;
    }

    public static void ChangeAnimation(Animator anim, int option)
    {

        anim.SetInteger("AnimState", option);
    }

    public static void PlayerAction(Rigidbody2D body, float velovityX, float velocityY)
    {

        body.velocity = new Vector2(velovityX, velocityY);
    }

    public static void setAttemptText(Text txt, int attempts)
    {
        if(attempts >= 0) {
            txt.text = "Attempts: " + attempts;
        } else {
            txt.text = "Attempts: " + 0;
        }
    }
}
