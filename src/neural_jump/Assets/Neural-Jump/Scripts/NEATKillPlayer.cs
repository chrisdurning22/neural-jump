using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NEATKillPlayer : MonoBehaviour {

	private void OnTriggerEnter2D(Collider2D collision) {
        
        if (collision.gameObject.tag == "Danger") {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
            gameObject.transform.position = new Vector3(0.0f, 0.0f, 0.0f);

            //sets players position of death (to be used in fitness function)
            gameObject.GetComponent<NEATPlayerOne>().setDeathPositionX(gameObject.transform.position.x);
        }
    }
}
