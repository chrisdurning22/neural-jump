using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class CameraTests {

    private GameObject cameraOne;
    private GameObject cameraTwo;
    private GameObject cameraThree;

    [SetUp]
    public void Init()
    {

        cameraOne = (GameObject)Object.Instantiate(Resources.Load("Test/camera_level_1"));
        cameraTwo = (GameObject)Object.Instantiate(Resources.Load("Test/camera_level_1"));
        cameraThree = (GameObject)Object.Instantiate(Resources.Load("Test/camera_level_1"));
    }

	[UnityTest]
	public IEnumerator Is_Camera_Connected_To_Player_Level_One() {
        
        CameraFollow player_1 = cameraOne.GetComponent<CameraFollow>();
        if (player_1.target != null)
        {
            yield break;
        }
        Assert.Fail();
	}

    [UnityTest]
    public IEnumerator Is_Camera_Connected_To_Player_Level_Two()
    {

        CameraFollow player_2 = cameraTwo.GetComponent<CameraFollow>();
        if (player_2.target != null)
        {
            yield break;
        }
        Assert.Fail();
    }

    [UnityTest]
    public IEnumerator Is_Camera_Connected_To_Player_Level_Three()
    {

        CameraFollow player_3 = cameraThree.GetComponent<CameraFollow>();
        if (player_3.target != null)
        {
            yield break;
        }
        Assert.Fail();
    }
}
