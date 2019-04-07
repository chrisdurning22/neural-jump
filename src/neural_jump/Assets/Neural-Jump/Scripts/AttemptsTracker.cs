using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttemptsTracker : MonoBehaviour {

    public static int attemptLevelOne;
    public static int attemptLevelTwo;
    public static int attemptLevelThree;

    public static int getAttemptLevelOne()
    {
        return attemptLevelOne;
    }

    public static int incrementAndGetAttemptLevelOne()
    {
        return attemptLevelOne++;
    }

    public static int getAttemptLevelTwo()
    {
        return attemptLevelTwo;
    }

    public static int incrementAndGetAttemptLevelTwo()
    {
        return attemptLevelTwo++;
    }

    public static int getAttemptLevelThree()
    {
        return attemptLevelThree;
    }

    public static int incrementAndGetAttemptLevelThree()
    {
        return attemptLevelThree++;
    }

    public static void setAttemptLevelOneToZero()
    {
        attemptLevelOne = 0;
    }

    public static void setAttemptLevelTwoToZero()
    {
        attemptLevelTwo = 0;
    }

    public static void setAttemptLevelThreeToZero()
    {
        attemptLevelThree = 0;
    }
}
