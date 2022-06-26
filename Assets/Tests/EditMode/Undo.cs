using System.Collections.Generic;
using Models;
using NUnit.Framework;

namespace Tests.EditMode
{
    public class Undo
    {
        // A Test behaves as an ordinary method
        [Test]
        public void UndoModelLogicTest()
        {
            TargetsModel targetsModel = new TargetsModel();
            int[][] targetsMatrix = {new int[]{0,0,0},new int[]{0,0,0}, new int[] {0,0,0} };
            targetsModel.SetMatrix(targetsMatrix);
            
            //fill almost all matrix (except 2,2)
            targetsModel.AddMoveToList(new KeyValuePair<int, int>(0,0),PlayerType.X);
            targetsModel.AddMoveToList(new KeyValuePair<int, int>(0,1),PlayerType.O);
            targetsModel.AddMoveToList(new KeyValuePair<int, int>(0,2),PlayerType.O);
            targetsModel.AddMoveToList(new KeyValuePair<int, int>(1,0),PlayerType.X);
            targetsModel.AddMoveToList(new KeyValuePair<int, int>(1,1),PlayerType.O);
            targetsModel.AddMoveToList(new KeyValuePair<int, int>(1,2),PlayerType.X);
            targetsModel.AddMoveToList(new KeyValuePair<int, int>(2,0),PlayerType.X);
            targetsModel.AddMoveToList(new KeyValuePair<int, int>(2,1),PlayerType.O);
            Assert.False(targetsModel.IsTargetAvailable(0,0));
            Assert.False(targetsModel.IsTargetAvailable(0,2));
            Assert.False(targetsModel.IsTargetAvailable(1,1));
            Assert.False(targetsModel.IsTargetAvailable(2,1));
            
            //undo last step
            targetsModel.DeleteLastMoveFromList();
            Assert.True(targetsModel.IsTargetAvailable(2,1));
            Assert.False(targetsModel.IsTargetAvailable(0,0));
            
            //undo all steps
            targetsModel.DeleteLastMoveFromList();
            targetsModel.DeleteLastMoveFromList();
            targetsModel.DeleteLastMoveFromList();
            Assert.True(targetsModel.IsTargetAvailable(0,0));
            Assert.True(targetsModel.IsTargetAvailable(2,2));
            Assert.True(targetsModel.IsTargetAvailable(1,1));
            Assert.True(targetsModel.IsTargetAvailable(2,1));
        }
    }
}