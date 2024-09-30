using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����Ԫ�ص�Ԫ
/// </summary>
public class GamePiece : MonoBehaviour
{
    public int X { get; set; }
    public int Y { get; set; }
    public PieceType Type { get; private set; }

    public Grid GridRef { get; private set; }

    public MoveablePiece MoveablePiece { get; private set; }
    public ColorPiece ColorPiece { get; private set; }

    public void Initialize(int x, int y, Grid grid, PieceType type)
    {
        this.X = x;
        this.Y = y;
        this.GridRef = grid;
        this.Type = type;
    }

    private void Awake()
    {
        MoveablePiece = GetComponent<MoveablePiece>();
        ColorPiece = GetComponent<ColorPiece>();
    }

    public bool IsMoveable { get { return MoveablePiece != null; } }

    public bool IsColored { get { return ColorPiece != null; } }

}
