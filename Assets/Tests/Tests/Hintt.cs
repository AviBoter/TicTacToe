using System.Collections;
using System.Collections.Generic;
using Models;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class Hintt
{
    // A Test behaves as an ordinary method
    [Test]
    public void HinttSimplePasses()
    {
        TargetsModel targetsModel = new TargetsModel();
           
        int[][] _argetsMatrix = {new int[]{0,1,1},new int[]{1,2,1}, new int[] {0,2,2} };
        targetsModel.SetMatrix(_argetsMatrix);
        Assert.NotNull(targetsModel.FindAvailableTarget());
        int[][] _argetsMatrix2 = {new int[]{1,1,1},new int[]{1,2,1}, new int[] {1,2,2} };
        targetsModel.SetMatrix(_argetsMatrix2);
        Assert.Null(targetsModel.FindAvailableTarget());
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator HinttWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}
