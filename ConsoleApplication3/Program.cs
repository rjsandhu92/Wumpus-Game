/* 
 * Main Program  Version 1.0  22/2/2015
 * Description: Main Program of wumpus game.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleApplication3
{
    class Program  // Main wumpus game program.
    {
        static void Main(string[] args)
        {

            TextReader tr;       // Text reader is initialized 
            Room[] ObjectArray = new Room[10];    // Object of class Room is created 

                tr = new StreamReader("wumpusData.txt");  // Text file is inserted in program.

                int numberOfDataLines = Convert.ToInt32(tr.ReadLine());
                for (int i = 0; i < numberOfDataLines; i++)
                {
                    string sample = tr.ReadLine();
                    string[] Array = new string[3];   // Array to store information for rooms
                    Array[0] = sample.Substring(0, 2);
                    Array[1] = sample.Substring(2, 8);
                    Array[2] = sample.Substring(10);

                    // Used to trim the whitespaces before and after the string
                    Array[0] = Array[0].Trim();
                    Array[1] = Array[1].Trim();
                    Array[2] = Array[2].Trim();

                    Room Object = new Room(Array[0], Array[1], Array[2]); // Data stored in array.
                    ObjectArray[i] = Object;

                }

                tr.Close();
            // the reader ends here 

            // Random number generator to generate random rooms for wumpus,spiders and pit.
            var random = new Random();     
            var values = Enumerable.Range(2, 10).OrderBy(x => random.Next()).ToArray();
            string wumpusRoom = values[0].ToString();
            string pitRoom = values[1].ToString();
            string spiderRoom1 = values[2].ToString();
            string spiderRoom2 = values[3].ToString();

            int arrowsLeft = 3;       // No. of arrows in game specified.
            Room currentRoom = new Room();  // New object of class Room containing current room information.
            currentRoom = ObjectArray[0];

            while (true)      // infinite loop used to run the game.
            {
                // Welcome message at beginning of game.
                Console.WriteLine("Welcome to  ***HUNT THE WUMPUS!!*** \n");
                Console.WriteLine("You are in room {0}", currentRoom.roomNumber);
                Console.WriteLine("You have {0} arrows left", arrowsLeft);
                Console.WriteLine(currentRoom.roomDescription);
                Console.WriteLine("There are tunnels to rooms {0}", currentRoom.threeAdjacentRooms);
                
                // Display hints to user about location of wumpus,pit and spiders.
                DisplayHints(wumpusRoom, pitRoom, spiderRoom1, spiderRoom2, currentRoom);


                do  //do while loop to begin game.
                {

                    Console.WriteLine("(M)ove or (S)hoot");  // Asks user to move or shoot in a room.
                    string getMoveOrShoot = Console.ReadLine();

                    if (getMoveOrShoot.Equals("m"))  // User chooses to move.
                    {

                        Console.WriteLine("Which room?");    // Asks user for room number.
                        string userMoveRoom = Console.ReadLine().Trim();
                        if (currentRoom.threeAdjacentRooms.Contains(userMoveRoom))
                        {
                            // When user enters room containing wumpus, pit or spiders.
                            if (userMoveRoom.Equals(wumpusRoom))
                            {
                                Console.WriteLine("Game Over.!! You have been eaten by a wumpus..Press any key to exit.!");
                                Console.ReadKey();
                                System.Environment.Exit(0);
                            }
                            else if (userMoveRoom.Equals(pitRoom))
                            {
                                Console.WriteLine("Game Over.!! You fell in a bottomless pit..Press any key to exit.!");
                                Console.ReadKey();
                                System.Environment.Exit(0);
                            }
                            else if (userMoveRoom.Equals(spiderRoom1))
                            {
                                Console.WriteLine("Game Over.!! You have been bitten by spiders..Press any key to exit.!");
                                Console.ReadKey();
                                System.Environment.Exit(0);
                            }
                            else if (userMoveRoom.Equals(spiderRoom2))
                            {
                                Console.WriteLine("Game Over.!! You have been bitten by spiders..Press any key to exit.!");
                                Console.ReadKey();
                                System.Environment.Exit(0);
                            }
                            else
                            {
                                // When user enters correct room.
                                Room newRoom = Array.Find(ObjectArray, e => e.roomNumber.Equals(userMoveRoom)); // Lambda expression to search data in a array 
                                currentRoom = newRoom;
                                Console.WriteLine("You are in room {0}", currentRoom.roomNumber);
                                Console.WriteLine("You have {0} arrows left", arrowsLeft);
                                Console.WriteLine(currentRoom.roomDescription);
                                Console.WriteLine("There are tunnels to rooms {0}", currentRoom.threeAdjacentRooms);

                                // Display hints to user about location of wumpus,pit and spiders.
                                DisplayHints(wumpusRoom, pitRoom, spiderRoom1, spiderRoom2, currentRoom);
                            }
                        }
                        else
                        {
                            // When user enters wrong room.
                            Console.WriteLine("Dimwit! You cant get there from here");
                            Console.WriteLine("There are tunnels to rooms " + currentRoom.threeAdjacentRooms);
                        }

                    }
                    else if (getMoveOrShoot.Equals("s"))   // User chooses to shoot.
                    {
                        Console.WriteLine("Which room?");   // Asks user which room to enter.
                        string userShootRoom = Console.ReadLine().Trim();

                        if (currentRoom.threeAdjacentRooms.Contains(userShootRoom))
                        {
                            if ((userShootRoom.Equals(wumpusRoom)))
                            {
                                // When user shoots in room containing wumpus.
                                Console.WriteLine("You Win...Killed the Wumpus..!!..Press any key to exit.!");
                                Console.ReadKey();
                                System.Environment.Exit(0);
                            }
                            else
                            {
                                arrowsLeft--;  // Arrows decrease.
                                if (arrowsLeft > 0)
                                {
                                    // When user shoots in wrong room.
                                    Console.WriteLine("Your arrow goes down the tunnel and is lost. You missed");
                                    Console.WriteLine("You are in room {0}", currentRoom.roomNumber);
                                    Console.WriteLine("You have {0} arrows left", arrowsLeft);
                                    Console.WriteLine(currentRoom.roomDescription);
                                    Console.WriteLine("There are tunnels to rooms {0}", currentRoom.threeAdjacentRooms);
                                    Console.WriteLine("The tunnels are " + currentRoom.threeAdjacentRooms);

                                    // Display hints to user about location of wumpus,pit and spiders.
                                    DisplayHints(wumpusRoom, pitRoom, spiderRoom1, spiderRoom2, currentRoom);
                                }
                                else
                                {
                                    // When user goes out of arrows.
                                    Console.WriteLine("Your arrow goes down the tunnel and is lost. You missed");
                                    Console.WriteLine("No arrows left dimwit!!  Game Over  Press any key to exit.!");
                                    Console.ReadKey();
                                    System.Environment.Exit(0);

                                }
                            }

                        }

                        else
                        {
                            // When users shoots to room other than available tunnels.
                            Console.WriteLine("You cannot shoot to this room.");
                            Console.WriteLine("You are in room {0}", currentRoom.roomNumber);
                            Console.WriteLine("You have {0} arrows left", arrowsLeft);
                            Console.WriteLine(currentRoom.roomDescription);
                            Console.WriteLine("There are tunnels to rooms {0}", currentRoom.threeAdjacentRooms);
                            Console.WriteLine("The tunnels are " + currentRoom.threeAdjacentRooms);

                            // Display hints to user about location of wumpus,pit and spiders.
                            DisplayHints(wumpusRoom, pitRoom, spiderRoom1, spiderRoom2, currentRoom);

                        }
                    }

                    else
                    {
                        // When user inputs wrong keyword.
                        Console.WriteLine("Invalid Entry...Try Again.!");
                    }

                }

                while (true); // return to infinite loop.
            }

          }

        // Display Hints Method
        private static void DisplayHints(string wumpusRoom, string pitRoom, string spiderRoom1, string spiderRoom2, Room currentRoom)
        {
            if (currentRoom.threeAdjacentRooms.Contains(wumpusRoom))
            {
                Console.WriteLine("You smell some nasty Wumpus!");
            }
            if (currentRoom.threeAdjacentRooms.Contains(pitRoom))
            {
                Console.WriteLine("You smell a dank odor");
            }
            if (currentRoom.threeAdjacentRooms.Contains(spiderRoom1))
            {
                Console.WriteLine("You hear a faint clicking noise");
            }
            if (currentRoom.threeAdjacentRooms.Contains(spiderRoom2))
            {
                Console.WriteLine("You hear a faint clicking noise");
            }
        }
        
       }
    
    }
