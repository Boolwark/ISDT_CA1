using System;
using System.Collections.Generic;
using System.Drawing;

using UnityEngine;
using Random = UnityEngine.Random;

namespace DefaultNamespace.ProceduralTerrain
{
    public class DungeonGenerator : MonoBehaviour
    {
        public class Cell
        {
            public bool visited = false;
            public bool[] status = new bool[4];
        }
        public Vector2 size;
        public GameObject gamePortalPrefab;
        public int startPos = 0;
        public GameObject room;
        public Vector2 offset;
        private List<Cell> board;
        private GameObject lastRoom;
        public List<GameObject> roomGameObjects = new List<GameObject>();

        public delegate void OnDungeonRoomsGenerated();

        public OnDungeonRoomsGenerated onDungeonRoomsGenerated;
    
        public void  Generate()
        {
            MazeGenerator();
            
        }

        void Update()
        {
            
        }

        void GenerateDungeon()
        {
            for (int i = 0; i < size.x; i++)
            {
                for (int j = 0; j < size.y; j++)
                {
                    Cell currentCell = board[Mathf.FloorToInt(i + j * size.x)];
                    if (currentCell.visited)
                    {
                        var newRoom =Instantiate(room,new  Vector3(i * offset.x, 0, -j * offset.y),Quaternion.identity,transform);
                        var roomBehaviour =  newRoom.GetComponent<RoomBehaviour>();
                        roomBehaviour.UpdateRoom(board[Mathf.FloorToInt(i + j * size.x)].status);
                        newRoom.name += " " + i + "-" + j;
                        lastRoom = newRoom;
                        // if this is the last room, Instantiate a GamePortal object at the center of the room.
                        // one in 3 rooms has a weapon in it, so create a random weapon spawner class. 
                        roomGameObjects.Add(newRoom);
                    }
                   
                }
            }

            if (lastRoom != null)
            {
                var portal = Instantiate(gamePortalPrefab, lastRoom.transform);
                portal.transform.localPosition = Vector3.zero;
                portal.transform.rotation = Quaternion.identity;
            }
          
        }

        void MazeGenerator()
        {
            board = new List<Cell>();
            for (int i = 0; i < size.x; i++)
            {
                for (int j = 0; j < size.y; j++)
                {
                    board.Add(new Cell());
                }
                
            }

            int currentCell = startPos;
            Stack<int> path = new Stack<int>();
            int k = 0;
            while (k < 1000)
            {
                k++;
                board[currentCell].visited = true;
                if (currentCell == board.Count - 1)
                {
                    break;
                }
                List<int> neighbors = CheckNeighbors(currentCell);
                if (neighbors.Count == 0)
                {
                    if (path.Count == 0)
                    {
                        break;
                    }

                    else
                    {
                        currentCell = path.Pop();
                    }

                }
                else
                {
                    path.Push(currentCell);
                    int newCell = neighbors[Random.Range(0, neighbors.Count)];
                    if (newCell > currentCell)
                    {
                        if (newCell - 1 == currentCell)
                        {
                            board[currentCell].status[2] = true;
                            currentCell = newCell;
                            board[currentCell].status[3] = true;
                        }
                        else
                        {
                            board[currentCell].status[1] = true;
                            currentCell = newCell;
                            board[currentCell].status[0] = true;
                        }
                    }
                    else
                    {
                        //up or left
                        if (newCell + 1 == currentCell)
                        {
                            board[currentCell].status[3] = true;
                            currentCell = newCell;
                            board[currentCell].status[2] = true;
                        }
                        else
                        {
                            board[currentCell].status[0] = true;
                            currentCell = newCell;
                            board[currentCell].status[1] = true;
                        }
                    }
                }
                

            }
            GenerateDungeon();
        }

        List<int> CheckNeighbors(int cell)
        {
            List<int> neighbors = new List<int>();
            if (cell - size.x >= 0 && !board[Mathf.FloorToInt(cell-size.x)].visited)
            {
                // not on the first row. Has up neighbor.
                neighbors.Add(Mathf.FloorToInt(cell - size.x));
                
            }
            if (cell + size.x < board.Count && !board[Mathf.FloorToInt(cell+size.x)].visited)
            {
                // not on the first row. Has up neighbor.
                neighbors.Add(Mathf.FloorToInt(cell + size.x));
                
            }
            if ((cell+1)%size.x != 0 && !board[Mathf.FloorToInt(cell+1)].visited)
            {
                // not on the first row. Has up neighbor.
                neighbors.Add(Mathf.FloorToInt(cell + 1));
                
            }
            if (cell % size.x != 0 && !board[Mathf.FloorToInt(cell-1)].visited)
            {
                // not on the first row. Has up neighbor.
                neighbors.Add(Mathf.FloorToInt(cell - 1));
                
            }
            return neighbors;
        }
    }
}