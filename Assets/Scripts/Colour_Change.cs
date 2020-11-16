using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Colour_Change : MonoBehaviour
{
    public Sprite sp1, sph1, sph2, sp4, spv1, spv2, pb1, pb2;
    SpriteRenderer sr;
    // Use this for initialization


    public static int p1 = 1;
    public static int p2 = 0;

    public static int update_player1 = 0;
    public static int update_player2 = 0;

    public static int p1_point = 0;
    public static int p2_point = 0;

    public static int max_box;
    public static int current_box;
    public static int max_score = GameInput.board_Width * GameInput.board_Height;

    public Text GameOver;
    public Text p1Score;
    public Text p2Score;
    public Text p1_dynamicScore;

    bool CheckRightBox(int ver_x_index, int ver_y_index)
    {
        bool top, down, right;
        bool result = false;
        top = GameBoard.horizontal_line[ver_x_index, ver_y_index].CompareTag("is play");
        down = GameBoard.horizontal_line[ver_x_index, ver_y_index + 1].CompareTag("is play");
        right = GameBoard.vertical_line[ver_x_index + 1, ver_y_index].CompareTag("is play");
        if (top && down && right)
            result = true;
        return (false || result);
    }

    bool CheckLeftBox(int ver_x_index, int ver_y_index)
    {
        bool top, down, left;
        bool result = false;
        top = GameBoard.horizontal_line[ver_x_index - 1, ver_y_index].CompareTag("is play");
        down = GameBoard.horizontal_line[ver_x_index - 1, ver_y_index + 1].CompareTag("is play");
        left = GameBoard.vertical_line[ver_x_index - 1, ver_y_index].CompareTag("is play");
        if (top && down && left)
            result = true;
        return (false || result);
    }

    bool CheckTopBox(int hor_x_index, int hor_y_index)
    {
        bool top, right, left;
        bool result = false;
        top = GameBoard.horizontal_line[hor_x_index, hor_y_index - 1].CompareTag("is play");
        left = GameBoard.vertical_line[hor_x_index, hor_y_index - 1].CompareTag("is play");
        right = GameBoard.vertical_line[hor_x_index + 1, hor_y_index - 1].CompareTag("is play");
        if (top && right && left)
            result = true;
        return (false || result);
    }

    bool CheckDownBox(int hor_x_index, int hor_y_index)
    {
        bool down, right, left;
        bool result = false;
        down = GameBoard.horizontal_line[hor_x_index, hor_y_index + 1].CompareTag("is play");
        left = GameBoard.vertical_line[hor_x_index, hor_y_index].CompareTag("is play");
        right = GameBoard.vertical_line[hor_x_index + 1, hor_y_index].CompareTag("is play");
        if (down && right && left)
            result = true;
        return (false || result);
    }


    void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
        max_box = GameInput.board_Height * GameInput.board_Width;
    }
    void OnMouseDown()
    {
        float xCor = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        float yCor = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;
        Vector2 origin = new Vector2(xCor, yCor);
        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.zero, 0);

        //////////////////////////////////////////////PLAYER_1////////////////////////////////////////////////////////////////////////

        if (p1 == 1)
        {

            int x_index = 0;
            int y_index = 0;


            for (int y = 0; y <= GameInput.board_Height; y++)
                for (int x = 0; x <= GameInput.board_Width; x++)
                {
                    if (y != GameInput.board_Height && hit.transform == GameBoard.vertical_line[x, y])
                    {
                        x_index = x;
                        y_index = y;
                    }
                    else
                    if (x != GameInput.board_Width && hit.transform == GameBoard.horizontal_line[x, y])
                    {
                        x_index = x;
                        y_index = y;
                    }

                }

            //HORIZONTAL_PLAYER1
            if (sr.sprite.Equals(sp1))
            {
                hit.transform.gameObject.GetComponent<SpriteRenderer>().sprite = sph1;
                hit.transform.tag = "is play";

                //Checking DOWNBOX
                if (y_index != GameInput.board_Height)
                {
                    if (CheckDownBox(x_index, y_index))
                    {
                        p1_point++;
                        GameBoard.boxs[x_index, y_index].GetComponent<SpriteRenderer>().sprite = pb1;
                        GameBoard.boxs[x_index, y_index].tag = "mark box";
                    }
                }
                //Checking TOPBOX
                if (y_index != 0)
                {
                    if (CheckTopBox(x_index, y_index) && y_index != 0)
                    {
                        p1_point++;
                        //show_player1_point.text = "player1 point: " + player1_score;
                        GameBoard.boxs[x_index, y_index - 1].GetComponent<SpriteRenderer>().sprite = pb1;
                        GameBoard.boxs[x_index, y_index - 1].tag = "mark box";
                    }
                }
                //Debug.Log(sp2);
            }

            //VERTICAL_PLAYER1        
            else if (sr.sprite.Equals(sp4))
            {
                hit.transform.gameObject.GetComponent<SpriteRenderer>().sprite = spv1;
                hit.transform.tag = "is play";

                //Checking LEFTBOX
                if (x_index != 0)
                {
                    if (CheckLeftBox(x_index, y_index))
                    {
                        p1_point++;
                        //show_player2_point.text = "Player2 Point: " + player2_score;
                        GameBoard.boxs[x_index - 1, y_index].GetComponent<SpriteRenderer>().sprite = pb1;
                        GameBoard.boxs[x_index - 1, y_index].tag = "mark box";
                    }
                }

                //Checking RIGHTBOX
                if (x_index != GameInput.board_Width)
                {
                    if (CheckRightBox(x_index, y_index))
                    {
                        p1_point++;
                        //show_player2_point.text = "Player2 Point: " + player2_score;
                        GameBoard.boxs[x_index, y_index].GetComponent<SpriteRenderer>().sprite = pb1;
                        GameBoard.boxs[x_index, y_index].tag = "mark box";
                    }
                }
                // Debug.Log(sp5);
            }

            else
            {
                Debug.Log("Player1 Score: " + p1_point);
            }
            //Updating Turn
            if (p1_point == update_player1)
            {
                p1 = 0;
                p2 = 1;
            }

            else
            {
                p1 = 1;
                p2 = 0;
                update_player1 = p1_point;
            }

        }//END OF PLAYER_1

        ////////////////////////////////////////////////////////PLAYER_2/////////////////////////////////////////////////////////////////////////////////
        else
        {
            int x_index = 0;
            int y_index = 0;

            for (int y = 0; y <= GameInput.board_Height; y++)
                for (int x = 0; x <= GameInput.board_Width; x++)
                {

                    if (y != GameInput.board_Height && hit.transform == GameBoard.vertical_line[x, y])
                    {
                        x_index = x;
                        y_index = y;
                    }
                    else
                    if (x != GameInput.board_Width && hit.transform == GameBoard.horizontal_line[x, y])
                    {
                        x_index = x;
                        y_index = y;
                    }

                }

            //HORIZONTAL_PLAYER2
            if (sr.sprite.Equals(sp1))
            {
                hit.transform.gameObject.GetComponent<SpriteRenderer>().sprite = sph2;
                hit.transform.tag = "is play";
                //Debug.Log(sp3);

                //Checking DOWNBOX
                if (y_index != GameInput.board_Height)
                {
                    if (CheckDownBox(x_index, y_index))
                    {
                        p2_point++;
                        GameBoard.boxs[x_index, y_index].GetComponent<SpriteRenderer>().sprite = pb2;
                        GameBoard.boxs[x_index, y_index].tag = "mark box";
                    }
                }

                //  Checking TOPBOX
                if (y_index != 0)
                {
                    if (CheckTopBox(x_index, y_index))
                    {
                        p2_point++;
                        //show_player1_point.text = "Player1 Point: " + player1_score;
                        GameBoard.boxs[x_index, y_index - 1].GetComponent<SpriteRenderer>().sprite = pb2;
                        GameBoard.boxs[x_index, y_index - 1].tag = "mark box";
                    }
                }

            }

            //VERTICAL_PLAYER2
            else if (sr.sprite.Equals(sp4))
            {
                hit.transform.gameObject.GetComponent<SpriteRenderer>().sprite = spv2;
                hit.transform.tag = "is play";

                ////Checking LeftBox
                if (x_index != 0)
                {
                    if (CheckLeftBox(x_index, y_index))
                    {
                        p2_point++;
                        //show_player2_point.text = "Player2 Point: " + player2_score;
                        GameBoard.boxs[x_index - 1, y_index].GetComponent<SpriteRenderer>().sprite = pb2;
                        GameBoard.boxs[x_index - 1, y_index].tag = "mark box";
                    }
                }

                //Checking RightBox
                if (x_index != GameInput.board_Width)
                {
                    if (CheckRightBox(x_index, y_index))
                    {
                        p2_point++;
                        //show_player2_point.text = "Player2 Point: " + player2_score;
                        GameBoard.boxs[x_index, y_index].GetComponent<SpriteRenderer>().sprite = pb2;
                        GameBoard.boxs[x_index, y_index].tag = "mark box";
                    }
                }
                ////   Debug.Log(sp5);
            }

            else
            {
                Debug.Log("Player2 Score: " + p2_point);
            }
            //Updating the Turn

            if (p2_point == update_player2)
            {
                p2 = 0;
                p1 = 1;
            }

            else
            {
                p2 = 1;
                p1 = 0;
                update_player2 = p2_point;
            }

        }//END OF PLAYER_2

        current_box = p1_point + p2_point;

        if (current_box == max_box)
        {
            On_max_score(p1_point, p2_point);
            EndGame();
        }


    }//END OF MAX_SCORE


    public void On_max_score(int p1_x, int p2_x)
    {

        p1_point = p1_x;
        p2_point = p2_x;

        if (p1_point == p2_point)
            Debug.Log("Draw");

        else if (p1_point > p2_point)
            Debug.Log("Player 1 Wins");

        else
            Debug.Log("Player 2 wins");




    }

    public void PlayGame()
    {
        
        SceneManager.LoadScene("SampleScene");
        
    }

    public void Restart()
    {
        p1_point = 0;
        p2_point = 0;
        current_box = 0;
        update_player1 = 0;
        update_player2 = 0;

        SceneManager.LoadScene("SampleScene");
        


    }

    public void EndGame()
    {

        if (max_score == p1_point)
            SceneManager.LoadScene("GameOverScene");

        else if (max_score == p2_point)
            SceneManager.LoadScene("GameOverScene");

        else
            SceneManager.LoadScene("GameOverScene");


    }

    public void ClickExit()
    {
        Debug.Log("Exit button pressed");
        Application.Quit();
    }
    public void MainMenu()
    {
        p1_point = 0;
        p2_point = 0;
        current_box = 0;
        update_player1 = 0;
        update_player2 = 0;
        SceneManager.LoadScene("MainMenuScene");
        
    }

    public void Update()
    {
        p1Score.text = "Score: " + p1_point.ToString();
        p2Score.text = "Score: " + p2_point.ToString();


        if (p1_point == p2_point)
            GameOver.text = "Draw";

        else if (p1_point > p2_point)
            GameOver.text = "Player 1 is WINNER";

        else
            GameOver.text = "Player 2 is WINNER";


    }

}

        
    



//END OF ON MOUSE DOWN

//SpriteRenderer sprite;
/*
void Start()
{
    Sprite = GetComponent<SpriteRenderer>();
}

void Update()
{
if (Input.GetMouseButtonDown(0))
{
    // change the 'color' property of the 'sprite renderer'
    Sprite.Color = new Color(1, 0, 0, 1);
}
*/
//END OF CLASS

