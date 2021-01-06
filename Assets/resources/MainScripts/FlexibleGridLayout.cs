using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum FitType
{
    Width,
    Heigth
}
public class FlexibleGridLayout : LayoutGroup
{
    public int rows;
    public int columns;
    public Vector2 cellSize;
    public Vector2 spacing;
    private FitType _fitType;
    public FitType fitType{
        get {return _fitType;}
        set{
            _fitType = value;
            CalculateLayoutInputHorizontal();
        }
    }

    public override void CalculateLayoutInputHorizontal()
    {
        base.CalculateLayoutInputHorizontal();

        float parentWidth = rectTransform.rect.width;
        float parentHeight = rectTransform.rect.height;

        float cellWidth = 0f;
        float cellHeight = 0f;

        switch(fitType)
        {
            case FitType.Heigth:
                columns = transform.childCount;
                rows = 1;
            break;
            case FitType.Width:
                columns = 1;
                rows = transform.childCount;
            break;
        }

        cellWidth = parentWidth / (float)columns - ((spacing.x / (float)columns) * 2) - (padding.left / (float)columns) - (padding.right / (float)columns);
        cellHeight = parentHeight / (float)rows - ((spacing.y / (float)rows) * 2) - (padding.top / (float)rows) - (padding.bottom / (float)rows);


        cellSize.y = cellHeight;
        cellSize.x = cellWidth;

        int columnCount = 0;
        int rowCount = 0;

        for (int i = 0; i < rectChildren.Count; i++)
        {
        if(fitType == FitType.Heigth)
        {
            rowCount = 0;
            columnCount = i;
        }else{
            rowCount = i;
            columnCount = 0;
        }

            var item = rectChildren[i];

            var xPos = (cellSize.x * columnCount) + (spacing.x * columnCount) + padding.left;
            var yPos = (cellSize.y * rowCount) + (spacing.y * rowCount) + padding.top;

            SetChildAlongAxis(item, 0, xPos, cellSize.x);
            SetChildAlongAxis(item, 1, yPos, cellSize.y);
        }
    }
    public override void CalculateLayoutInputVertical()
    {
    }

    public override void SetLayoutHorizontal()
    {
    }

    public override void SetLayoutVertical()
    {
    }
}
