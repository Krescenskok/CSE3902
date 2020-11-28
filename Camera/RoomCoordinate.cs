using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint5
{
    public static class RoomCoordinate
    {

        static Dictionary<int, List<int>> coordinates = new Dictionary<int, List<int>>()
        {
            { 1, new List<int> { 2,5}},
            { 2, new List<int> { 1,5}},
            { 3, new List<int> { 3,5}},
            { 4, new List<int> { 2,4}},
            { 5, new List<int> { 2,3}},
            { 6, new List<int> { 1,3}},
            { 7, new List<int> { 3,3}},
            { 8, new List<int> { 2,2}},
            { 9, new List<int> { 0,2}},
            { 10, new List<int> { 1,2}},
            { 11, new List<int> {3,2}},
            { 12, new List<int> { 4,2}},
            { 13, new List<int> { 2,1}},
            { 14, new List<int> { 4,1}},
            { 15, new List<int> { 5,1}},
            { 16, new List<int> { 2,0}},
            { 17, new List<int> { 1,0}},
            { 18, new List<int> { 1,1}},


        };

        public static Vector3 position(int num, int width, int height)
        {
            List<int> xy = coordinates[num];
            return new Vector3(xy[0] * -width, xy[1] * -height, 0);
        }
    }
}
