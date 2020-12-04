using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint5
{
    public enum DoorType
    {
        normal,
        locked,
        special_open,
        special_closed,//special doors open/close when certain conditions are met
        secret
    }

    public static class DoorTypes
    {
        public static DoorType Parse(string str)
        {
            if (str.Equals("locked")) return DoorType.locked;
            else if (str.Equals("normal")) return DoorType.normal;
            else if (str.Equals("special_closed")) return DoorType.special_closed;
            else return DoorType.special_open;
        }
    }
}
