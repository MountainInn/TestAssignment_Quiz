using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelGrid : MonoBehaviour
{
    [SerializeField] Cell prefCell;

    GridConfig config;

    List<Cell> cells;


    void Awake()
    {
        cells = new List<Cell>();
    }


    public void SetConfig(GridConfig gridConfig)
    {
        config = gridConfig;
    }

    public void SetupCells(List<string> selectedQuestions, List<Sprite> selectedSprites)
    {
        int
            currentSize = cells.Count,
            targetSize = selectedQuestions.Count,
            diff = currentSize - targetSize;


        if (diff > 0)           /// Скрыть лишние клетки
        {
            for(int i = currentSize-1; i >= targetSize; i--)
            {
                cells[i].gameObject.SetActive(false);
            }
        }
        else if (diff < 0)      /// Добавить недостающие клетки
        {
            for(int i = 0; i < Mathf.Abs(diff); i++)
            {
                cells.Add(InstCell());
            }
        }

        for (int i = 0; i < targetSize; i++)
        {
            cells[i].Setup(selectedQuestions[i], selectedSprites[i]);
        }

        Resize();
    }

    private void Resize()
    {
        int
            column = 0,
            row = 0;

        Vector3 correction = config.GetCorrection();

        for (int i = 0; i < config.GetCellCount(); i++)
        {
            cells[i].transform.position =
                transform.position
                - correction
                + new Vector3(column, row, 0) * (config.cellsize + config.margin);

            column++;
            if (column >= config.width)
            {
                row++;
                column = 0;
            }
        }
    }

    private Cell InstCell()
    {
        return Instantiate(prefCell);
    }

    public void DestroyCells()
    {
        for (int i = cells.Count-1; i >= 0; i--)
        {
            Destroy(cells[i].gameObject);

            cells.RemoveAt(i);
        }
    }

    public void SetCellsUnclickable()
    {
        foreach(var item in cells)
        {
            item.SetUnclickable();
        }
    }

    public void MakeCellsAppear()
    {
        foreach(var item in cells)
        {
            item.PlayAppearTween();;
        }
    }

}
