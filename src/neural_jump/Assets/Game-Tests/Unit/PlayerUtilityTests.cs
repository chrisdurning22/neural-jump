using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class PlayerUtilityTests {

    private GameObject playerPrefabOne;
    private GameObject playerPrefabTwo;
    private GameObject playerPrefabThree;

    [SetUp]
    public void Init()
    {

        playerPrefabOne = (GameObject)UnityEngine.Object.Instantiate(Resources.Load("Test/player_1"));
        playerPrefabTwo = (GameObject)UnityEngine.Object.Instantiate(Resources.Load("Test/player_2"));
        playerPrefabThree = (GameObject)UnityEngine.Object.Instantiate(Resources.Load("Test/player_3"));
    }

    [UnityTest]
    public IEnumerator Player_One_Has_RigidBody_Attached() {
        
        Rigidbody2D body2D = playerPrefabOne.GetComponent<Rigidbody2D>();
        if(body2D != null) {
            yield break;
        }
        Assert.Fail();
    }

    [UnityTest]
    public IEnumerator Player_One_Has_Collider_Attached()
    {
        Collider2D collider2D = playerPrefabOne.GetComponent<Collider2D>();
        if (collider2D != null)
        {
            yield break;
        }
        Assert.Fail();
    }

    [UnityTest]
    public IEnumerator Player_Two_Has_RigidBody_Attached()
    {
        Rigidbody2D body2D = playerPrefabTwo.GetComponent<Rigidbody2D>();
        if (body2D != null)
        {
            yield break;
        }
        Assert.Fail();
    }

    [UnityTest]
    public IEnumerator Player_Two_Has_Collider_Attached()
    {
        Collider2D collider2D = playerPrefabTwo.GetComponent<Collider2D>();
        if (collider2D != null)
        {
            yield break;
        }
        Assert.Fail();
    }

    [UnityTest]
    public IEnumerator Player_Three_Has_RigidBody_Attached()
    {
        Rigidbody2D body2D = playerPrefabThree.GetComponent<Rigidbody2D>();
        if (body2D != null)
        {
            yield break;
        }
        Assert.Fail();
    }

    [UnityTest]
    public IEnumerator Player_Three_Has_Collider_Attached()
    {
        Collider2D collider2D = playerPrefabThree.GetComponent<Collider2D>();
        if (collider2D != null)
        {
            yield break;
        }
        Assert.Fail();
    }

	[Test]
	public void Player_Jumps_When_onGround_And_Space_Is_Pressed() {
        Assert.AreEqual(true, PlayerUtility.CanJump(true, true));
	}

    [Test]
    public void Player_Doesnt_Jump_When_Not_onGround()
    {
        Assert.AreNotEqual(true, PlayerUtility.CanJump(false, true));
    }

    [Test]
    public void Player_Doesnt_Jump_When_Space_Is_Not_Pressed()
    {
        Assert.AreNotEqual(true, PlayerUtility.CanJump(true, false));
    }
}

