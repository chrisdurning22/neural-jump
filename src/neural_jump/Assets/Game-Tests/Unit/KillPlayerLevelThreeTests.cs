using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class KillPlayerLevelThreeTests {

    private GameObject playerThree;

    [SetUp]
    public void Init()
    {

        playerThree = (GameObject)Object.Instantiate(Resources.Load("Test/player_3"));
    }

    [UnityTest]
    public IEnumerator Is_Text_Object_Connected_To_Script()
    {

        KillPlayerLevelThree killPlayer = playerThree.GetComponent<KillPlayerLevelThree>();

        if (killPlayer.txt != null)
        {
            yield break;
        }

        Assert.Fail();
    }

    [UnityTest]
    public IEnumerator Set_Attempt_Text_Plus()
    {

        KillPlayerLevelThree killPlayer = playerThree.GetComponent<KillPlayerLevelThree>();

        PlayerUtility.setAttemptText(killPlayer.txt, 3);

        Assert.AreEqual("Attempts: 3", killPlayer.txt.text);

        yield return null;
    }

    [UnityTest]
    public IEnumerator Set_Attempt_Text_Minus()
    {

        KillPlayerLevelThree killPlayer = playerThree.GetComponent<KillPlayerLevelThree>();

        PlayerUtility.setAttemptText(killPlayer.txt, -5);

        Assert.AreEqual("Attempts: 0", killPlayer.txt.text);

        yield return null;
    }
}
