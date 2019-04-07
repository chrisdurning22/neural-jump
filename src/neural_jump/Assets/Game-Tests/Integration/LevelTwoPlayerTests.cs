using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System;

public class LevelTwoPlayerTests {
    private GameObject playerPrefab;
    private GameObject levelPrefab;

    [SetUp]
    public void Init()
    {

        playerPrefab = (GameObject)UnityEngine.Object.Instantiate(Resources.Load("Test/player_2"));
        levelPrefab = (GameObject)UnityEngine.Object.Instantiate(Resources.Load("Test/level_2"));
    }

    [UnityTest]
    public IEnumerator Player_Runs_East_Along_X_Axis()
    {

        float xBeforeUpdateIsCalled = playerPrefab.transform.position.x;

        yield return null;

        float xAfterUpdateIsCalled = playerPrefab.transform.position.x;

        if (xBeforeUpdateIsCalled < xAfterUpdateIsCalled)
        {
            yield break;
        }

        Assert.Fail();

    }

    [UnityTest]
    public IEnumerator Player_Stays_Fixed_On_Y_Axis()
    {

        float yBeforeUpdateIsCalled = playerPrefab.transform.position.y;

        yield return null;

        float yAfterUpdateIsCalled = playerPrefab.transform.position.y;

        float difference = Math.Abs(yBeforeUpdateIsCalled - yAfterUpdateIsCalled);

        Debug.Log(difference);

        if (difference < 0.01)
        {

            yield break;
        }

        Assert.Fail();

    }

    [UnityTest]
    public IEnumerator PlayerAction_Jump()
    {
        Rigidbody2D body = playerPrefab.GetComponent<Rigidbody2D>();

        float yBeforePlayerJumps = playerPrefab.transform.position.y;

        yield return null;

        PlayerUtility.PlayerAction(body, 9f, 22f);

        float yAfterPlayerJumps = playerPrefab.transform.position.y;

        if (yAfterPlayerJumps > yBeforePlayerJumps)
        {

            yield break;
        }

        Assert.Fail();
    }

    [UnityTest]
    public IEnumerator PlayerAction_Run_No_Jump()
    {
        Rigidbody2D body = playerPrefab.GetComponent<Rigidbody2D>();

        float yBeforePlayerJumps = playerPrefab.transform.position.x;

        yield return null;

        PlayerUtility.PlayerAction(body, 9f, 0f);

        float yAfterPlayerJumps = playerPrefab.transform.position.x;

        if (yAfterPlayerJumps > yBeforePlayerJumps)
        {

            yield break;
        }

        Assert.Fail();
    }
}
