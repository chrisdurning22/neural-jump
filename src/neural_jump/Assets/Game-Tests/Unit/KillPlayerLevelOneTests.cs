using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class KillPlayerLevelOneTests {

    private GameObject playerOne;

    [SetUp]
    public void Init()
    {

        playerOne = (GameObject)Object.Instantiate(Resources.Load("Test/player_1"));
    }

    [UnityTest]
    public IEnumerator Is_Text_Object_Connected_To_Script()
    {

        KillPlayerLevelOne killPlayer = playerOne.GetComponent<KillPlayerLevelOne>();

        if(killPlayer.txt != null) {
            yield break;
        }

        Assert.Fail();
    }

    [UnityTest]
    public IEnumerator Set_Attempt_Text_Plus()
    {
        
        KillPlayerLevelOne killPlayer = playerOne.GetComponent<KillPlayerLevelOne>();

        PlayerUtility.setAttemptText(killPlayer.txt, 3);

        Assert.AreEqual("Attempts: 3", killPlayer.txt.text);

        yield return null;
    }

    [UnityTest]
    public IEnumerator Set_Attempt_Text_Minus()
    {

        KillPlayerLevelOne killPlayer = playerOne.GetComponent<KillPlayerLevelOne>();

        PlayerUtility.setAttemptText(killPlayer.txt, -5);

        Assert.AreEqual("Attempts: 0", killPlayer.txt.text);

        yield return null;
    }
}
