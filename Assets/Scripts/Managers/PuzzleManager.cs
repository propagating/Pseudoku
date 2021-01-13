using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PuzzleManager : MonoBehaviour
{
    public TMP_InputField sequenceInput;

    public string storedSequence = "";

    private Grid[] allGrids;

    private void Start()
    {
        allGrids = FindObjectsOfType<Grid>();
        sequenceInput.textComponent.enableWordWrapping = true;
    }

    public void OnImportSequence()
    {
        storedSequence = sequenceInput.text;

        for (int i = 0; i < allGrids.Length; i++)
        {
            if (!allGrids[i].inputAllowed)
                continue;

            for (int j = 0; j < allGrids[i].transform.childCount; j++)
            {
                allGrids[i].transform.GetChild(j).GetComponent<Cell>().ClearCell();
            }

            string[] splitSeq = new string[storedSequence.Length];
            int y = 1;

            for (int k = 0; k < storedSequence.Length; k++)
            {
                splitSeq[k] = System.Convert.ToString(storedSequence[k]);
            }

            for (int j = 0; j < splitSeq.Length; j++)
            {
                if (allGrids[i].transform.childCount-1 < j)
                    continue;

                int x = j % 9;

                allGrids[i].GetCell(x+1, y).correctValue = int.Parse(splitSeq[j].ToString());
                allGrids[i].GetCell(x + 1, y).StoredValue = allGrids[i].GetCell(x + 1, y).correctValue;
                if ((x+1) / 9 == 1)
                    y++;
            }
        }
    }

    public void OnSolveSequence()
    {

    }
}
