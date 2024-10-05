using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Games.Pieces
{
    /// <summary>
    /// µ¥¸ö¿é½Å±¾
    /// </summary>
    public class GamePiece : MonoBehaviour
    {
        public int X { get; set; }
        public int Y { get; set; }
        public PieceType Type { get; private set; }

        public Grid GridRef { get; private set; }

        public MoveablePiece MoveablePiece { get; private set; }
        public ColorPiece ColorPiece { get; private set; }
        public ClearablePiece ClearablePiece { get; private set; }

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
            ClearablePiece = GetComponent<ClearablePiece>();
        }

        public bool IsMoveable { get { return MoveablePiece != null; } }

        public bool IsColored { get { return ColorPiece != null; } }

        public bool IsClearable { get { return ClearablePiece != null; } }

        private void OnMouseEnter()
        {
            GridRef.EnterPiece(this);
        }

        private void OnMouseDown()
        {
            GridRef.PressPiece(this);
        }

        private void OnMouseUp()
        {
            GridRef.ReleasePiece();
        }

    }
}