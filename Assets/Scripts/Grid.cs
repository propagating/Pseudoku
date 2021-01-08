using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public GameObject cellPrefab;

    public Cell selectedCell;

    public Color selectedCellColor;
    public Color nonInputCellColor;

    public Sprite cellTL, cellT, cellTR, cellL, cellR, cellBR, cellB, cellBL;

    public int width, height;

    public bool inputAllowed = true;

    private void Start()
    {
        this.transform.position = inputAllowed ? new Vector2(width * 0.75f, -height/2) : new Vector2(-width * 0.75f, -height /2);
        Grid[] allGrids = FindObjectsOfType<Grid>();
        Vector3 cam = allGrids[0].transform.position + allGrids[1].transform.position;
        cam.z = -10f;
        cam.y = 0f;
        cam.x += Mathf.Abs(allGrids[0].transform.position.y);
        Camera.main.transform.position = cam;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Cell g = Instantiate(cellPrefab, this.transform).GetComponent<Cell>();

                g.transform.localPosition = new Vector2(x, y);
                g.SetCoordinates(x, 8-y);
                g.name = $"Cell: {g.xCoord},{g.yCoord}";
                g.grid = this;
                g.sr.sprite = GetSpriteFromLayout(g);
                g.sr.color = inputAllowed ? Color.white : nonInputCellColor;
                g.GetComponent<Collider2D>().enabled = inputAllowed;
            }
        }
    }

    private void Update()
    {
        if (!selectedCell)
            return;

        if(Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
        {
            selectedCell.StoredValue = selectedCell.StoredValue == 1 ? 0 : 1;
        }
        if(Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
        {
            selectedCell.StoredValue = selectedCell.StoredValue == 2 ? 0 : 2;
        }
        if(Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
        {
            selectedCell.StoredValue = selectedCell.StoredValue == 3 ? 0 : 3;
        }
        if(Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4))
        {
            selectedCell.StoredValue = selectedCell.StoredValue == 4 ? 0 : 4;
        }
        if(Input.GetKeyDown(KeyCode.Alpha5) || Input.GetKeyDown(KeyCode.Keypad5))
        {
            selectedCell.StoredValue = selectedCell.StoredValue == 5 ? 0 : 5;
        }
        if(Input.GetKeyDown(KeyCode.Alpha6) || Input.GetKeyDown(KeyCode.Keypad6))
        {
            selectedCell.StoredValue = selectedCell.StoredValue == 6 ? 0 : 6;
        }
        if(Input.GetKeyDown(KeyCode.Alpha7) || Input.GetKeyDown(KeyCode.Keypad7))
        {
            selectedCell.StoredValue = selectedCell.StoredValue == 7 ? 0 : 7;
        }
        if(Input.GetKeyDown(KeyCode.Alpha8) || Input.GetKeyDown(KeyCode.Keypad8))
        {
            selectedCell.StoredValue = selectedCell.StoredValue == 8 ? 0 : 8;
        }
        if(Input.GetKeyDown(KeyCode.Alpha9) || Input.GetKeyDown(KeyCode.Keypad9))
        {
            selectedCell.StoredValue = selectedCell.StoredValue == 9 ? 0 : 9;
        }
        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Delete))
        {
            selectedCell.StoredValue = 0;
        }
    }

    public Cell GetCell(int x, int y)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Cell c = transform.GetChild(i).GetComponent<Cell>();
            if (c.xCoord == x && c.yCoord == y)
                return c;
        }

        return null;
    }

    public void SelectCell(Cell c)
    {
        DeSelectCell();
        selectedCell = c;
        selectedCell.sr.color = selectedCellColor;
    }

    public void DeSelectCell()
    {
        if (!selectedCell)
            return;

        selectedCell.sr.color = Color.white;
        selectedCell = null;
    }

    public Sprite GetSpriteFromLayout(Cell c)
    {
        int x = c.xCoord;
        int y = c.yCoord;

        if(x == 1 || x == 4 || x == 7)
        {
            if(y == 1 || y == 4 || y == 7)
            {
                return cellTL;
            }
            else if(y == 2 || y == 5 || y == 8)
            {
                return cellL;
            }
            else if(y == 3 || y == 6|| y == 9)
            {
                return cellBL;
            }
        }
        if(x == 3 || x == 6|| x == 9)
        {
            if(y == 1 || y == 4 || y == 7)
            {
                return cellTR;
            }
            else if(y == 2 || y == 5 || y == 8)
            {
                return cellR;
            }
            else if(y == 3 || y == 6|| y == 9)
            {
                return cellBR;
            }
        }

        if(x == 2 || x == 5 || x == 8)
        {
            if(y == 1 || y == 4 || y == 7)
            {
                return cellT;
            }
            else if(y == 3 || y == 6|| y == 9)
            {
                return cellB;
            }
        }

        return c.sr.sprite;
    }
}
