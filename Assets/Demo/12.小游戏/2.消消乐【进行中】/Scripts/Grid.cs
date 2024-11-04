using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Demo.Games.Pieces
{
    /// <summary>
    /// 游戏网格
    /// </summary>
    public class Grid : MonoBehaviour
    {
        public int xDim;
        public int yDim;
        public PiecePrefab[] prefabs;
        public GameObject bgPrefab;//网格背景
        public float fillTime = 2;//每一行单元格下落的时间
        private bool inverse = false;//元素下移时是否可以斜向移动

        private Dictionary<PieceType, GameObject> piecePrefabDict = new Dictionary<PieceType, GameObject>();
        private GamePiece[,] pieces;

        private GamePiece pressPiece;//鼠标按下的地块
        private GamePiece enterPiece;//鼠标滑动的地块

        #region 初始化
        void Start()
        {
            ToPrefabDict();
            InitializeGridBg();
            InitializePiece();

            ////测试
            //Destroy(pieces[4, 4].gameObject);
            //SpawnNewPiece(4, 4, PieceType.Bubble);

            StartCoroutine(Fill());
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


        private void ToPrefabDict()
        {
            foreach (var piece in prefabs)
            {
                if (!piecePrefabDict.ContainsKey(piece.type))
                    piecePrefabDict.Add(piece.type, piece.prefab);
            }
        }
        #endregion

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
            bool needRefill = true;

            while (needRefill)
            {
                yield return new WaitForSeconds(fillTime);

                while (FillStep())
                {
                    inverse = !inverse;
                    yield return new WaitForSeconds(fillTime);
                }

                needRefill = ClearAllValidMatches();
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
                for (int loopX = 0; loopX < xDim; loopX++)
                {
                    int x = loopX;
                    if (inverse)
                    {
                        x = xDim - loopX - 1;
                    }

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
                        else
                        {
                            //斜向移动（如果垂直填充不了就斜向填充）
                            for (int diag = -1; diag <= 1; diag++)
                            {
                                if (diag != 0)
                                {
                                    //判断向左斜向移动，还是向右斜向移动
                                    int diagX = x + diag;
                                    if (inverse)
                                    {
                                        diagX = x - diagX;
                                    }

                                    if (diagX >= 0 && diagX < xDim)
                                    {
                                        GamePiece diagonalPiece = pieces[diagX, y + 1];
                                        if (diagonalPiece.Type == PieceType.Empty)
                                        {
                                            //如果当前地块是空的，则尝试从上面的地块移动到当前
                                            bool hasPieceAbove = true;

                                            //todo:有问题
                                            int aboveY = y;
                                            //for (int aboveY = y; aboveY >= 0; aboveY--)
                                            {
                                                GamePiece pieceAbove = pieces[diagX, aboveY];
                                                if (pieceAbove.IsMoveable)
                                                {
                                                    //当前指定的斜上方地块可以移动下来
                                                    break;
                                                }
                                                else if (!pieceAbove.IsMoveable && pieceAbove.Type != PieceType.Empty)
                                                {
                                                    //当前指定的斜上方地块不可移动
                                                    hasPieceAbove = false;
                                                    break;
                                                }
                                            }

                                            if (!hasPieceAbove)
                                            {
                                                //可以移动地块
                                                Destroy(diagonalPiece.gameObject);
                                                piece.MoveablePiece.Move(diagX, y + 1, fillTime);
                                                pieces[diagX, y + 1] = piece;
                                                SpawnNewPiece(x, y, PieceType.Empty);
                                                movePiece = true;
                                                break;
                                            }
                                        }
                                    }
                                }

                            }
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
                    GameObject newPiece = Instantiate(piecePrefabDict[PieceType.Normal], GetWorldPosition(x, -1), Quaternion.identity, this.transform);

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

        /// <summary>
        /// 是否相邻
        /// </summary>
        /// <param name="piece1"></param>
        /// <param name="piece2"></param>
        /// <returns></returns>
        public bool IsAdjacent(GamePiece piece1, GamePiece piece2)
        {
            return (piece1.X == piece2.X && (int)Mathf.Abs(piece1.Y - piece2.Y) == 1)
                || (piece1.Y == piece2.Y && (int)Mathf.Abs(piece1.X - piece2.X) == 1);
        }

        /// <summary>
        /// 交换两个地块
        /// </summary>
        /// <param name="piece1"></param>
        /// <param name="piece2"></param>
        public void SwapPiece(GamePiece piece1, GamePiece piece2)
        {
            if (!piece1.IsMoveable || !piece2.IsMoveable)
                return;

            //需要先交换然后才能去检查是否可以交换
            pieces[piece1.X, piece1.Y] = piece2;
            pieces[piece2.X, piece2.Y] = piece1;

            //判断是否存在交换后可以消除的情况
            if (GetMatch(piece1, piece2.X, piece2.Y) != null ||
                GetMatch(piece2, piece1.X, piece1.Y) != null)
            {
                //符合交换条件
                int piece1X = piece1.X;
                int piece1Y = piece1.Y;
                int piece2X = piece2.X;
                int piece2Y = piece2.Y;

                piece1.MoveablePiece.Move(piece2X, piece2Y, fillTime);
                piece2.MoveablePiece.Move(piece1X, piece1Y, fillTime);

                //清除相关地块
                ClearAllValidMatches();
            }
            else
            {
                //不符合交换条件，恢复
                pieces[piece1.X, piece1.Y] = piece1;
                pieces[piece2.X, piece2.Y] = piece2;
            }
        }

        /// <summary>
        /// 记录移动地块时鼠标按下的地块
        /// </summary>
        /// <param name="piece"></param>
        public void PressPiece(GamePiece piece)
        {
            pressPiece = piece;
        }

        /// <summary>
        /// 记录移动地块时鼠标拖动的地块
        /// </summary>
        /// <param name="piece"></param>
        public void EnterPiece(GamePiece piece)
        {
            enterPiece = piece;
        }

        public void ReleasePiece()
        {
            if (IsAdjacent(pressPiece, enterPiece))
                SwapPiece(pressPiece, enterPiece);
        }

        /// <summary>
        /// 清除相关地块
        /// </summary>
        /// <returns></returns>
        public bool ClearAllValidMatches()
        {
            bool needsRefill = false;

            for (int y = 0; y < yDim; y++)
            {
                for (int x = 0; x < xDim; x++)
                {
                    if (!pieces[x, y].IsClearable) continue;

                    List<GamePiece> match = GetMatch(pieces[x, y], x, y);

                    if (match == null) continue;


                  
                }
            }

            return needsRefill;
        }

        /// <summary>
        /// 清除地块
        /// </summary>
        /// <returns></returns>
        public bool ClearPiece(int x, int y)
        {
            if (pieces[x, y].IsClearable && !pieces[x, y].ClearablePiece.isBeingCleared)
            {
                pieces[x, y].ClearablePiece.Clear();
                SpawnNewPiece(x, y, PieceType.Empty);
                return true;
            }
            return false;
        }
        #endregion

        #region 匹配方法
        /// <summary>
        /// 判断移动后是否存在可以消除的情况
        /// </summary>
        /// <param name="piece"></param>
        /// <param name="newX"></param>
        /// <param name="newY"></param>
        /// <returns></returns>
        public List<GamePiece> GetMatch(GamePiece piece, int newX, int newY)
        {
            if (piece.IsColored)
            {
                ColorType color = piece.ColorPiece.Color;//匹配块四周的颜色是否一致

                var horizontalPieces = new List<GamePiece>();
                var verticalPieces = new List<GamePiece>();
                var matchingPieces = new List<GamePiece>();

                //匹配水平
                horizontalPieces.Add(piece);
                for (int direction = 0; direction <= 1; direction++)
                {
                    //匹配水平左右方向
                    for (int xOffest = 1; xOffest < xDim; xOffest++)
                    {
                        int x = 0;
                        if (direction == 0)
                            x = newX - xOffest; //向左匹配
                        else
                            x = newX + xOffest;//向右匹配

                        //越界检查
                        if (x < 0 || x >= xDim)
                            break;

                        //匹配移动后新位置同一行是否是相同颜色
                        if (pieces[x, newY].IsColored && pieces[x, newY].ColorPiece.Color == color)
                            horizontalPieces.Add(pieces[x, newY]);
                        else
                            break;
                    }
                }

                //如果相同颜色超过三个则加入到已匹配的列表中
                if (horizontalPieces.Count >= 3)
                    matchingPieces.AddRange(horizontalPieces);

                //查询T型或L型
                if (horizontalPieces.Count >= 3)
                {
                    for (int i = 0; i < horizontalPieces.Count; i++)
                    {
                        for (int direction = 0; direction <= 1; direction++)
                        {
                            for (int yOffest = 1; yOffest < yDim; yOffest++)
                            {
                                int y = 0;
                                if (direction == 0)
                                    y = newY - yOffest; //向上匹配
                                else
                                    y = newY + yOffest;//向下匹配

                                //越界检查
                                if (y < 0 || y >= yDim)
                                    break;

                                //每个已匹配的水平元素都去尝试匹配垂直方向
                                var hPiece = horizontalPieces[i];
                                if (pieces[hPiece.X, y].IsColored && pieces[hPiece.X, y].ColorPiece.Color == color)
                                    verticalPieces.Add(pieces[hPiece.X, y]);
                                else
                                    break;
                            }
                        }

                        //不满足条件则需要清空
                        if (verticalPieces.Count < 2)
                            verticalPieces.Clear();
                        else
                        {
                            matchingPieces.AddRange(verticalPieces);
                            break;
                        }
                    }
                }

                if (matchingPieces.Count >= 3)
                    return matchingPieces;


                //没有匹配成功，清除之前的匹配结果
                horizontalPieces.Clear();
                verticalPieces.Clear();

                //垂直水平
                verticalPieces.Add(piece);
                for (int direction = 0; direction <= 1; direction++)
                {
                    //匹配水平左右方向
                    for (int yOffest = 1; yOffest < yDim; yOffest++)
                    {
                        int y = 0;
                        if (direction == 0)
                            y = newX - yOffest; //向上匹配
                        else
                            y = newX + yOffest;//向下匹配

                        //越界检查
                        if (y < 0 || y >= yDim)
                            break;

                        //匹配移动后新位置同一行是否是相同颜色
                        if (pieces[newX, y].IsColored && pieces[newX, y].ColorPiece.Color == color)
                            verticalPieces.Add(pieces[newX, y]);
                        else
                            break;
                    }
                }

                //如果相同颜色超过三个则加入到已匹配的列表中
                if (verticalPieces.Count >= 3)
                    matchingPieces.AddRange(verticalPieces);

                if (verticalPieces.Count >= 3)
                {
                    for (int i = 0; i < verticalPieces.Count; i++)
                    {
                        for (int direction = 0; direction <= 1; direction++)
                        {
                            for (int xOffest = 1; xOffest < xDim; xOffest++)
                            {
                                int x = 0;
                                if (direction == 0)
                                    x = newX - xOffest; //向左匹配
                                else
                                    x = newX + xOffest;//向右匹配

                                //越界检查
                                if (x < 0 || x >= xDim)
                                    break;

                                //每个已匹配的水平元素都去尝试匹配垂直方向
                                var vPiece = verticalPieces[i];
                                if (pieces[x, vPiece.Y].IsColored && pieces[x, vPiece.Y].ColorPiece.Color == color)
                                    horizontalPieces.Add(pieces[x, vPiece.Y]);
                                else
                                    break;
                            }
                        }

                        //不满足条件则需要清空
                        if (horizontalPieces.Count < 2)
                            horizontalPieces.Clear();
                        else
                        {
                            matchingPieces.AddRange(horizontalPieces);
                            break;
                        }
                    }
                }

                if (matchingPieces.Count >= 3)
                    return matchingPieces;
            }

            return null;
        }

        #endregion
    }
}