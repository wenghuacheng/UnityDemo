using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public int xDim;
    public int yDim;
    public PiecePrefab[] prefabs;
    public GameObject bgPrefab;//网格背景
    public float fillTime = 2;//每一行单元格下落的时间
    private bool isverse = false;//元素下移时是否可以斜向移动

    private Dictionary<PieceType, GameObject> piecePrefabDict = new Dictionary<PieceType, GameObject>();
    private GamePiece[,] pieces;

    void Start()
    {
        ToPrefabDict();
        InitializeGridBg();
        InitializePiece();

        Destroy(pieces[4, 4].gameObject);
        SpawnNewPiece(4, 4, PieceType.Bubble);

        StartCoroutine(Fill());
    }

    void Update()
    {

    }

    #region Method

    /// <summary>
    /// 填充所有网格
    /// </summary>
    /// <returns></returns>
    private IEnumerator Fill()
    {
        while (FillStep())
        {
            yield return new WaitForSeconds(fillTime);
        }
    }

    /// <summary>
    /// 填充单个网格
    /// </summary>
    /// <returns></returns>
    public bool FillStep()
    {
        bool movePiece = false;

        for (int y = yDim - 2; y >= 0; y--)
        {
            for (int x = 0; x < xDim; x++)
            {
                var piece = pieces[x, y];

                if (piece.IsMoveable)
                {
                    GamePiece pieceBelow = pieces[x, y + 1];
                    //检查下一行是否是空元素，是则将元素下移一格
                    if (pieceBelow.Type == PieceType.Empty)
                    {
                        piece.MoveablePiece.Move(x, y + 1, fillTime);
                        pieces[x, y + 1] = piece;
                        SpawnNewPiece(x, y, PieceType.Empty);
                        movePiece = true;
                    }
                }
            }
        }

        //顶行生成元素
        for (int x = 0; x < xDim; x++)
        {
            GamePiece pieceBelow = pieces[x, 0];

            if (pieceBelow.Type == PieceType.Empty)
            {
                //这里使用-1坐标，让其生成在顶行上一层
                GameObject newPiece = (GameObject)Instantiate(piecePrefabDict[PieceType.Normal], GetWorldPosition(x, -1), Quaternion.identity, this.transform);

                //移动到0行
                pieces[x, 0] = newPiece.GetComponent<GamePiece>();
                pieces[x, 0].Initialize(x, -1, this, PieceType.Normal);
                pieces[x, 0].MoveablePiece.Move(x, 0, fillTime);
                pieces[x, 0].ColorPiece.SetColor((ColorType)Random.Range(0, pieces[x, 0].ColorPiece.NumColors));

                movePiece = true;
            }
        }

        return movePiece;
    }

    /// <summary>
    /// 初始化背景网格
    /// </summary>
    private void InitializeGridBg()
    {
        for (int x = 0; x < xDim; x++)
        {
            for (int y = 0; y < yDim; y++)
            {
                Instantiate(bgPrefab, GetWorldPosition(x, y), Quaternion.identity, this.transform);
            }
        }
    }

    /// <summary>
    /// 初始化元素阵列
    /// </summary>
    private void InitializePiece()
    {
        pieces = new GamePiece[xDim, yDim];

        for (int x = 0; x < xDim; x++)
        {
            for (int y = 0; y < yDim; y++)
            {
                var gamePiece = SpawnNewPiece(x, y, PieceType.Empty);

                if (gamePiece.IsColored)
                {
                    gamePiece.ColorPiece.SetColor((ColorType)Random.Range(0, gamePiece.ColorPiece.NumColors));
                }
            }
        }
    }

    /// <summary>
    /// 新建
    /// </summary>
    private GamePiece SpawnNewPiece(int x, int y, PieceType type)
    {
        var newPiece = Instantiate(piecePrefabDict[type], GetWorldPosition(x, y), Quaternion.identity, this.transform);

        pieces[x, y] = newPiece.GetComponent<GamePiece>();
        pieces[x, y].Initialize(x, y, this, type);

        return pieces[x, y];
    }


    /// <summary>
    /// 基于(0,0)点偏移网格
    /// 【让整个网格居中显示】
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public Vector2 GetWorldPosition(int x, int y)
    {
        return new Vector2(
            transform.position.x - xDim / 2 + x,
            transform.position.y + yDim / 2 - y);
    }

    private void ToPrefabDict()
    {
        foreach (var piece in prefabs)
        {
            if (!piecePrefabDict.ContainsKey(piece.type))
                piecePrefabDict.Add(piece.type, piece.prefab);
        }
    }
    #endregion
}
