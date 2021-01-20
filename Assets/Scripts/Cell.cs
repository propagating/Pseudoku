using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Cell : MonoBehaviour
{
    public int xCoord, yCoord;
    public GameObject possibleAnswersHolder;

    [SerializeField]
    private int storedValue;
    public int StoredValue { get { return storedValue; } 
        set
        {
            storedValue = value;
            HandleDisplay();
            SendValueOnCorrect(value);
        } 
    }

    public int correctValue = 0;

    public Grid grid;

    public SpriteRenderer sr;

    public TMP_Text number;

    private void Start()
    {
        possibleAnswersHolder.SetActive(grid.inputAllowed);
    }

    public void SetCoordinates(int x, int y)
    {
        xCoord = x+1;
        yCoord = y+1;
    }

    public void HandleDisplay()
    {
        if (storedValue > 0 && storedValue < 10)
        {
            number.text = $"{storedValue}";
            possibleAnswersHolder.SetActive(false);
        }
        else
        {
            number.text = "";
            if(grid.inputAllowed)
                possibleAnswersHolder.SetActive(true);
        }
    }

    private void OnMouseDown()
    {
        grid.SelectCell(this);
    }

    public void SendValueOnCorrect(int num)
    {
        Grid[] grids = FindObjectsOfType<Grid>();

        for (int i = 0; i < grids.Length; i++)
        {
            if (grids[i].inputAllowed)
                continue;

            grids[i].GetCell(xCoord, yCoord).storedValue = num;
            grids[i].GetCell(xCoord, yCoord).HandleDisplay();
        }
    }

    public void ClearCell()
    {
        correctValue = 0;
        StoredValue = 0;
    }

    public void ParsePossibleAnswers(List<int> answers)
    {
        for (int i = 0; i < possibleAnswersHolder.transform.childCount; i++)
        {
            possibleAnswersHolder.transform.GetChild(i).gameObject.SetActive(answers.Contains(i + 1));
        }
    }
}
