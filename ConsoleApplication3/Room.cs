/* 
 * Room Class Version 1.0 22/2/2015
 * Description: Room class containing three main attributes.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication3
{
    class Room
    {
        private string _roomNumber;
        private string _threeAdjacentRooms;
        private string _roomDescription;

        public Room()  // constructor used to accept arguments of each property 
        { }


        public Room(string roomNumber,string threeAdjacentRooms,string roomDescription) // constructor with defined parameters
        {
            _roomNumber = roomNumber;
            _threeAdjacentRooms= threeAdjacentRooms;
            _roomDescription = roomDescription;

        }

        public string roomNumber  // class method roomNumber is initialized by getter and setter attributes
        {
            get { return _roomNumber; }
            set { _roomNumber = value; }
        }

        public string threeAdjacentRooms   // class method threeAdjacentRooms is initialized by getter and setter attributes
        {
            get { return _threeAdjacentRooms; }
            set { _threeAdjacentRooms = value; }
        }

        public string roomDescription   // class method roomDescription is initialized by getter and setter attributes
        {
            get { return _roomDescription; }
            set { _roomDescription = value; }
        }




    }
}
