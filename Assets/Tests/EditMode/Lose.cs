using System.Collections.Generic;
using Models;
using Models.GameModels;
using NUnit.Framework;

namespace Tests.EditMode
{
    public class Lose
    {
        // A Test behaves as an ordinary method
        [Test]
        public void WinModelLogicTest()
        {
            TargetsModel targetsModel = new TargetsModel();
            int[][] targetsMatrix = {new int[]{0,0,0},new int[]{0,0,0}, new int[] {0,0,0} };
            targetsModel.SetMatrix(targetsMatrix);
            
            //O lose - Columns
            targetsModel.AddMoveToList(new KeyValuePair<int, int>(0,0),PlayerType.X);
            targetsModel.AddMoveToList(new KeyValuePair<int, int>(0,1),PlayerType.X);
            targetsModel.AddMoveToList(new KeyValuePair<int, int>(0,2),PlayerType.X);
            targetsModel.AddMoveToList(new KeyValuePair<int, int>(1,0),PlayerType.O);
            targetsModel.AddMoveToList(new KeyValuePair<int, int>(1,1),PlayerType.O);
            targetsModel.AddMoveToList(new KeyValuePair<int, int>(2,2),PlayerType.O);
            Assert.NotNull(targetsModel.GetGameState());
            Assert.False(targetsModel.GetGameState()==GameState.OWin);

            //O lose - Rows
            targetsModel.DeleteLastMoveFromList();
            targetsModel.DeleteLastMoveFromList();
            targetsModel.DeleteLastMoveFromList();
            targetsModel.AddMoveToList(new KeyValuePair<int, int>(0,0),PlayerType.X);
            targetsModel.AddMoveToList(new KeyValuePair<int, int>(1,0),PlayerType.X);
            targetsModel.AddMoveToList(new KeyValuePair<int, int>(2,0),PlayerType.X);
            targetsModel.AddMoveToList(new KeyValuePair<int, int>(1,1),PlayerType.O);
            targetsModel.AddMoveToList(new KeyValuePair<int, int>(1,2),PlayerType.O);
            targetsModel.AddMoveToList(new KeyValuePair<int, int>(2,2),PlayerType.O);
            Assert.NotNull(targetsModel.GetGameState());
            Assert.False(targetsModel.GetGameState()==GameState.OWin);
            
            //X lose - Diagonal
            targetsModel.DeleteLastMoveFromList();
            targetsModel.DeleteLastMoveFromList();
            targetsModel.DeleteLastMoveFromList();
            targetsModel.AddMoveToList(new KeyValuePair<int, int>(0,0),PlayerType.O);
            targetsModel.AddMoveToList(new KeyValuePair<int, int>(0,1),PlayerType.X);
            targetsModel.AddMoveToList(new KeyValuePair<int, int>(0,2),PlayerType.X);
            targetsModel.AddMoveToList(new KeyValuePair<int, int>(1,0),PlayerType.X);
            targetsModel.AddMoveToList(new KeyValuePair<int, int>(1,1),PlayerType.O);
            targetsModel.AddMoveToList(new KeyValuePair<int, int>(2,2),PlayerType.O);
            Assert.NotNull(targetsModel.GetGameState());
            Assert.False(targetsModel.GetGameState()==GameState.XWin);
        }
    }
}