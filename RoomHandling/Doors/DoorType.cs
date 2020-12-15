using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint5
{
    public enum DoorType
    {
        normal,
        locked,
        closed,//special doors open/close when certain conditions are met
        open,
        secret,
        none
    }

    public static class DoorTypes
    {
        public static DoorType Parse(string str)
        {
            if (str.Contains("locked")) return DoorType.locked;
            else if (str.Contains("normal")) return DoorType.normal;
            else if (str.Contains("closed")) return DoorType.closed;
            else if (str.Contains("open")) return DoorType.open;
            else return DoorType.none;
        }
    }
}
