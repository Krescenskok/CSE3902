using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sprint5
{
    public class MapGenerator
    {

        public static MapGenerator Instance { get; } = new MapGenerator();
        private MapGenerator()
        {

        }

        private struct Room
        {
            public bool hasRightDoor { get; set; }
            public bool hasLeftDoor { get; set; }
            public bool hasTopDoor { get; set; }
            public bool hasBottomDoor;

            public int set { get; set; }
        }

        private Random random = new Random();
        private Point startingRoom;

        private List<List<int>> binaryGrid = new List<List<int>>();
        private Dictionary<string,Room> roomList = new Dictionary<string,Room>();

        private Dictionary<int, List<string>> Sets = new Dictionary<int, List<string>>();
        private Dictionary<int, bool> SetGoesForward = new Dictionary<int, bool>();

        private RoomWriter newWriter;

        public void GenerateMap(int size, string fileName)
        {

            newWriter = new RoomWriter(fileName);

            for (int i = 0; i < size; i++)
            {
                binaryGrid.Add(new List<int>());
                
                for(int j = 0; j < size; j++)
                {
                    binaryGrid[i].Add(0);

                    roomList.Add(i + "," + j, new Room());
                }
            }


            for(int i = 0; i < size; i++)
            {
                ResetSetGoesForwardBoolean();

                for(int j = 0; j < size; j++)
                {
                    if (binaryGrid[i][j] == 0) binaryGrid[i][j] = NewSet(i,j); 
                }



                for(int j = 0; j < size; j++)
                {
                    if(j < size -1 && Random(size) )
                    {
                        AddRightSpaceToSet(i, j, binaryGrid[i][j], binaryGrid[i][j + 1]);
                    }
                }

                for(int j = 0; j < size; j++)
                {
                    int currentSet = binaryGrid[i][j];
                    bool lastInRow = j == size - 1, lastInSet = (!lastInRow && binaryGrid[i][j + 1] != currentSet) || lastInRow;

                    if(i < size - 1 && (Random(size) || (lastInSet && !SetGoesForward[currentSet])))
                    {
                        AddTopSpaceToSet(i, j, binaryGrid[i][j]);
                        SetGoesForward[currentSet] = true;
                    }
                }

                if(i < size - 1)
                    RoomGenerator.GenerateRooms(newWriter, new List<List<int>> { binaryGrid[i], binaryGrid[i + 1] },i);
                else
                    RoomGenerator.GenerateRooms(newWriter, new List<List<int>> { binaryGrid[i]}, i);
            }

            RoomGenerator.FinishGeneration(newWriter);

            Random rand = new Random();
            startingRoom = new Point(rand.Next(0,size), size - 1);
            Camera.Instance.ResetTransformLocation(startingRoom, startingRoom.X);
        }

        

        private bool Random(int odds)
        {
            return random.Next(0, 3) == 0;
        }


        private int NewSet(int i, int j)
        {
            int num = 1;
            while (Sets.ContainsKey(num)) num++;
            Sets.Add(num, new List<string>());
            SetGoesForward.Add(num, false);
            Sets[num].Add(i + "," + j);
            return num;
        }

        private void AddRightSpaceToSet(int i, int j, int leftSet, int rightSet)
        {

            if (Sets[rightSet].Count == 1) { Sets.Remove(rightSet); SetGoesForward.Remove(rightSet); }
            binaryGrid[i][j + 1] = leftSet;
            Sets[leftSet].Add(i + "," + (j + 1));
        }

        private void AddTopSpaceToSet(int i, int j, int set)
        {
            binaryGrid[i + 1][j] = binaryGrid[i][j];
            Sets[set].Add((i + 1) + "," + j);
        }

        private void ResetSetGoesForwardBoolean()
        {
            foreach(var value in SetGoesForward.Keys.ToList())
            {
                SetGoesForward[value] = false;
            }
        }
    }
}
