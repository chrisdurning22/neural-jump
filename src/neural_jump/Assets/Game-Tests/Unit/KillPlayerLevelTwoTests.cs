using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class KillPlayerLevelTwoTests
{

    private GameObject playerTwo;
    //private GameObject levelTwo;

    [SetUp]
    public void Init()
    {

        playerTwo = (GameObject)Object.Instantiate(Resources.Load("Test/player_2"));
        //levelTwo = (GameObject)Object.Instantiate(Resources.Load("Test/level_2"));
    }

    [UnityTest]
    public IEnumerator Is_Text_Object_Connected_To_Script()
    {

        KillPlayerLevelTwo killPlayer = playerTwo.GetComponent<KillPlayerLevelTwo>();

        if (killPlayer.txt != null)
        {
            yield break;
        }

        Assert.Fail();
    }

    [UnityTest]
    public IEnumerator Set_Attempt_Text_Plus()
    {

        KillPlayerLevelTwo killPlayer = playerTwo.GetComponent<KillPlayerLevelTwo>();

        PlayerUtility.setAttemptText(killPlayer.txt, 3);

        Assert.AreEqual("Attempts: 3", killPlayer.txt.text);

        yield return null;
    }

    [UnityTest]
    public IEnumerator Set_Attempt_Text_Minus()
    {

        KillPlayerLevelTwo killPlayer = playerTwo.GetComponent<KillPlayerLevelTwo>();

        PlayerUtility.setAttemptText(killPlayer.txt, -5);

        Assert.AreEqual("Attempts: 0", killPlayer.txt.text);

        yield return null;
    }

    [UnityTest]
    public IEnumerator Set_Player_Position_On_Obstacle()
    {
        playerTwo.transform.position = new Vector3(1633.68f, -1628.23f, 0);

        //playerTwo.transform.position += temp;


        Time.timeScale = 10;

        yield return new WaitForSeconds(1);

        if (playerTwo == null)
        {
            Debug.Log("-- player_2 is equal null --");
        }

        yield break;

        //Debug.Log(playerTwo.transform.position);
        //playerTwo.transform.position += temp;
        // cadd more obstacle tests later
    }
}
