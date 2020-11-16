using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class GameInput : MonoBehaviour
{

    public static int board_Width;
    public static int board_Height;


    [SerializeField]
    private InputField input;

    [SerializeField]
    private InputField input1;


    [SerializeField]
    private Text text;

    public GameBoard setg;
    public Text AlertMessage;
    public void GetInput(string guess)
    {
        int.TryParse(guess, out board_Width);
        input1.text = "";
        input.text = "";
        //       Debug.Log("this  " + board_Height);
        //        Debug.Log("this  " + board_Width);

    }


    public void GetInput1(string guess1)
    {
        int.TryParse(guess1, out board_Height);
    }


    // This Method is called when User Click On 'Ready'  Button on "GameBoardScene"
    public void Create()
    {
        if ((board_Height & board_Width) < 9)
        {
            setg.CreateBoard(board_Width, board_Height);
        }
        
    }

    // After Input of Height,width of board User click on Submit Button. This Method is then called and simply 
    // Load GameBoardScene
    public void Submit()
    {
        if (board_Height < 9)
        {
            if (board_Width < 9)
            {
                if (board_Height > 1)
                {
                    if (board_Width > 1)
                    {
                        SceneManager.LoadScene("GameBoardScene");
                    }
                    else
                        AlertMessage.text = "Enter values Between 2 and 8";
                }
                else
                    AlertMessage.text = "Enter values Between 2 and 8";
            }
            else
                AlertMessage.text = "Enter values Between 2 and 8";
        }
        else
            AlertMessage.text = "Enter values Between 2 and 8";


        //setg.CreateBoard(board_Width, board_Height);


    }



}