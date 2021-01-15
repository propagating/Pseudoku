using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager instance;

    public TMP_InputField sequenceInput;

    public string storedSequence = "";
    public int maxStates => states.Count;
    public int currentState;
    public int CurrentState { get { return currentState; } set { currentState = value; boardStateText.text = $"{currentState} of {maxStates-1}"; } }

    public List<string> states = new List<string>();

    public TMP_Text boardStateText;

    public Button previousState, nextState;

    private Grid[] allGrids;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
    }

    private void Start()
    {
        CurrentState = 0;
        allGrids = FindObjectsOfType<Grid>();
        sequenceInput.textComponent.enableWordWrapping = true;
        UpdateStateArrowInteracts();

        for (int i = 0; i < allGrids.Length; i++)
        {
            if (allGrids[i].inputAllowed)
                AddBoardState(GetCurrentBoardState(allGrids[i]));
        }
        
    }

    public void OnImportSequence()
    {
        storedSequence = sequenceInput.text;

        SetBoardState(storedSequence, true);
    }

    public void OnClearSequence()
    {
        sequenceInput.text = "";
    }

    public void OnResetBoard()
    {
        for (int i = states.Count - 1; i > 0; i--)
        {
            states.RemoveAt(i);
        }

        SetBoardState(states[0], false);
        CurrentState = 0;
        UpdateStateArrowInteracts();
    }

    public void SetBoardState(string state, bool add = false)
    {
        for (int i = 0; i < allGrids.Length; i++)
        {
            if (!allGrids[i].inputAllowed)
                continue;

            for (int j = 0; j < allGrids[i].transform.childCount; j++)
            {
                allGrids[i].transform.GetChild(j).GetComponent<Cell>().ClearCell();
            }

            string[] splitSeq = new string[state.Length];
            int y = 1;

            for (int k = 0; k < state.Length; k++)
            {
                splitSeq[k] = System.Convert.ToString(state[k]);
            }

            for (int j = 0; j < splitSeq.Length; j++)
            {
                if (allGrids[i].transform.childCount - 1 < j)
                    continue;

                int x = j % 9;

                allGrids[i].GetCell(x + 1, y).correctValue = int.Parse(splitSeq[j].ToString());
                allGrids[i].GetCell(x + 1, y).StoredValue = allGrids[i].GetCell(x + 1, y).correctValue;
                if ((x + 1) / 9 == 1)
                    y++;
            }

            if(add)
                AddBoardState(GetCurrentBoardState(allGrids[i]));
        }

    }

    public void AddBoardState(string state)
    {
        states.Add(state);
        CurrentState = maxStates-1;
        UpdateStateArrowInteracts();
    }

    public void OnSolveSequence()
    {

    }

    public void OnCycleState(bool forward)
    {
        if (forward)
            CurrentState += 1;
        else CurrentState -= 1;

        SetBoardState(states[currentState], false);
        UpdateStateArrowInteracts();
    }

    public void UpdateStateArrowInteracts()
    {
        nextState.interactable = currentState < maxStates-1 && maxStates > 0;
        previousState.interactable = currentState > 0 && maxStates > 1;
    }

    public string GetCurrentBoardState(Grid g)
    {
        string state = "";

        int y = 1;
        for (int i = 0; i < g.transform.childCount; i++)
        {
            if (g.transform.childCount - 1 < i)
                continue;

            int x = i % 9;

            state += g.GetCell(x + 1, y).StoredValue;
            if ((x + 1) / 9 == 1)
                y++;
        }

        return state;
    }

    public void OnSelectImportBox()
    {
        for (int i = 0; i < allGrids.Length; i++)
        {
            if (allGrids[i].inputAllowed)
                allGrids[i].DeSelectCell();
        }
    }
}
