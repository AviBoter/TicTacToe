using System.Collections.Generic;
using Models;
using NUnit.Framework;

namespace Tests.EditMode
{
    public class Hint
    {
        // A Test behaves as an ordinary method
        [Test]
        public void HintModelLogicTest()
        {
            TargetsModel targetsModel = new TargetsModel();
           
            int[][] targetsMatrix = {new int[]{0,0,0},new int[]{0,0,0}, new int[] {0,0,0} };
            targetsModel.SetMatrix(targetsMatrix);
            
            //empty matrix
            Assert.NotNull(targetsModel.FindAvailableTarget());
            Assert.AreNotEqual(targetsModel.FindAvailableTarget(),new KeyValuePair<int,int>(-1,-1));
            
            //add some player moves to the matrix
            targetsModel.AddMoveToList(new KeyValuePair<int, int>(0,0),PlayerType.X);
            targetsModel.AddMoveToList(new KeyValuePair<int, int>(0,1),PlayerType.X);
            Assert.NotNull(targetsModel.FindAvailableTarget());
            Assert.AreNotEqual(targetsModel.FindAvailableTarget(),new KeyValuePair<int,int>(-1,-1));
            
            //fill almost all matrix (except 2,2) to see algo behavior 
            targetsModel.AddMoveToList(new KeyValuePair<int, int>(0,2),PlayerType.O);
            targetsModel.AddMoveToList(new KeyValuePair<int, int>(1,0),PlayerType.X);
            targetsModel.AddMoveToList(new KeyValuePair<int, int>(1,1),PlayerType.O);
            targetsModel.AddMoveToList(new KeyValuePair<int, int>(1,2),PlayerType.X);
            targetsModel.AddMoveToList(new KeyValuePair<int, int>(2,0),PlayerType.X);
            targetsModel.AddMoveToList(new KeyValuePair<int, int>(2,1),PlayerType.O);
            Assert.NotNull(targetsModel.FindAvailableTarget());
            Assert.AreNotEqual(targetsModel.FindAvailableTarget(),new KeyValuePair<int,int>(-1,-1));
            
            //fill all matrix
            targetsModel.AddMoveToList(new KeyValuePair<int, int>(2,2),PlayerType.X);
            Assert.NotNull(targetsModel.FindAvailableTarget());
            Assert.AreEqual(targetsModel.FindAvailableTarget(),new KeyValuePair<int,int>(-1,-1));
        }
    }
}
