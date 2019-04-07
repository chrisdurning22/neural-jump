using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class KillPlayerLevelTwo : MonoBehaviour
{
    public Text txt;

    void Start() {
        
        PlayerUtility.setAttemptText(txt, AttemptsTracker.getAttemptLevelTwo());
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        
        if (collision.gameObject.tag == "Danger") {
            
            Destroy(gameObject);
            PlayerUtility.setAttemptText(txt, AttemptsTracker.incrementAndGetAttemptLevelTwo());
            SceneManager.LoadScene(3);
        }
    }
}

