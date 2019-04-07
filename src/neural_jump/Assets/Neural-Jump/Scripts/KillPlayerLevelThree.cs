using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class KillPlayerLevelThree : MonoBehaviour
{
    public Text txt;

    void Start() {
        
        PlayerUtility.setAttemptText(txt, AttemptsTracker.getAttemptLevelThree());
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        
        if (collision.gameObject.tag == "Danger") {
            
            Destroy(gameObject);
            PlayerUtility.setAttemptText(txt, AttemptsTracker.incrementAndGetAttemptLevelThree());
            SceneManager.LoadScene(5);
        }
    }
}

