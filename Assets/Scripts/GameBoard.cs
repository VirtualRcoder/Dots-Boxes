using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameBoard : MonoBehaviour
{
    // object
    public Transform vertical;
    public Transform horizontal;
    public Transform box;


    // Main camera
    public Camera mainCam;

    // Board Size
    public static int board_Width;
    public static int board_Height ;

    // Structure of game
    public static Transform[,] vertical_line;
    public static Transform[,] horizontal_line;
    public static Transform[,] boxs;

    // Create Tic tac toe board
    public void CreateBoard(int board_Width, int board_Height)
    {
        

        float vertical_Line_Width = vertical.GetComponent<SpriteRenderer>().bounds.size.x;
        float vertical_Line_Height = vertical.GetComponent<SpriteRenderer>().bounds.size.y;
        
        float horizontal_Line_Width = horizontal.GetComponent<SpriteRenderer>().bounds.size.x;
        float horizontal_Line_Height = horizontal.GetComponent<SpriteRenderer>().bounds.size.y;
  

        float box_Width = box.GetComponent<SpriteRenderer>().bounds.size.x;
        float box_Height = box.GetComponent<SpriteRenderer>().bounds.size.y;
        
 
        float startX = Screen.width / 2;
        float startY = Screen.height / 2;
        //initial game object structure

        vertical_line = new Transform[board_Width + 1, board_Height];
        horizontal_line = new Transform[board_Width, board_Height + 1];
        boxs = new Transform[board_Width, board_Height];
        


        float yCor = mainCam.ScreenToWorldPoint(new Vector3(0, startY, 0f)).y;
        // middle screen
        


        yCor = yCor + (board_Height * vertical_Line_Height + (board_Height + 1) * horizontal_Line_Height) / 2;

        
        
        for (int y = 0; y <= board_Height; y++)
        {
            float xCor = mainCam.ScreenToWorldPoint(new Vector3(startX, 0, 0f)).x;
            //middle screen
            xCor = xCor - (board_Width * horizontal_Line_Width + (board_Width + 1) * vertical_Line_Width) / 2;


            // check.text = "x =  " + xCor + " y = " + yCor;
            for (int x = 0; x <= board_Width; x++)
            {
                Vector3 center_of_Box = new Vector3(xCor, yCor, 0f);
                Vector3 center_of_Vertical = new Vector3(xCor - horizontal_Line_Width / 2 - vertical_Line_Width / 2, yCor, 0f);
                Vector3 center_of_Horizontal = new Vector3(xCor, yCor + horizontal_Line_Height / 2 + vertical_Line_Height / 2, 0f);

                if (x != board_Width && y != board_Height)
                {
                    boxs[x, y] = Instantiate(box, center_of_Box, Quaternion.identity) as Transform;
                }
                if (y != board_Height)
                {
                    vertical_line[x, y] = Instantiate(vertical, center_of_Vertical, Quaternion.identity) as Transform;
                }

                if (x != board_Width)
                {
                    horizontal_line[x, y] = Instantiate(horizontal, center_of_Horizontal, Quaternion.identity) as Transform;
                }

                xCor += box_Width + vertical_Line_Width;
            }
            yCor -= box_Height + horizontal_Line_Height;
        }
    }

}
