using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private Sprite bgImage;

    [SerializeField]
    public Sprite[] puzzles; //puzzle buttons ya3ni 
    public List<Sprite> gamePuzzles = new List<Sprite>();

    public List<Button> btns = new List<Button>();

    private bool firstGuess, secondGuess; //bool variables are initialised as false
    private int countGuesses;
    private int countCorrectGuesses;
    private int gameGuesses;

    private int firstGuessIndex, secondGuessIndex;

    private string firstGuessPuzzle, secondGuessPuzzle;


    void Awake()
    {
        puzzles = Resources.LoadAll<Sprite>("Sprites"); //get all sprites inside the array instead of dragging and dropping them one by one inside Unity editor
    }

    // using Start here because I used Awake() in AddButton() to get the buttons. And I can't use it twice to get the buttons twice.
    void Start()
    {
        GetButtons();
        AddListeners();
        AddGamePuzzles();
    }

    void GetButtons()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("puzzleButton");

        for(int i=0; i < objects.Length; i++)
        {
            btns.Add(objects[i].GetComponent<Button>()); //a button is not a game object, it is a component. So we need to first get the game object that contains it,
                                                         //then get the button itself by GetComponent()

            btns[i].image.sprite = bgImage; //changing background image for every button
        }
    }

    void AddGamePuzzles()
    {
        int looper = btns.Count; //number of button elements we have
        int index = 0;
        for(int i=0; i < looper; i++)
        {
            if (index == looper / 2)
                index = 0;
            gamePuzzles.Add(puzzles[index]);
            index++;

        }
    }

    void AddListeners()
    {
        //foreach is better for processing lists - for loops works for arrays
        foreach (Button btn in btns)
        {
            btn.onClick.AddListener(() => PickAPuzzle());  //attaching the PickAPuzzle() to each button's onclick
        }
    }
    public void PickAPuzzle() //needs to be public void
    {
        Debug.Log("You are clicking button " + name);

        if (!firstGuess) //if we did not guess for the firs time
        {
            firstGuess = true;
            //store the index of the button of the first guess
            firstGuessIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name); //to get the name of the button being clicked);
            firstGuessPuzzle = gamePuzzles[firstGuessIndex].name; //get the name of our image and store it in firstGuessPuzzle
            btns[firstGuessIndex].image.sprite = gamePuzzles[firstGuessIndex];
        }
        else if (!secondGuess)
        {
            secondGuess = true;
            //store the index of the button of the second guess
            secondGuessIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name); //to get the name of the button being clicked);
            secondGuessPuzzle = gamePuzzles[secondGuessIndex].name;
            btns[secondGuessIndex].image.sprite = gamePuzzles[secondGuessIndex];
        }
        if (firstGuessPuzzle == secondGuessPuzzle)
            Debug.Log("The Puzzles match");
        else
            Debug.Log("The Puzzles do not match");
    }
}
