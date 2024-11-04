using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Demo.Games.Pieces
{
    /// <summary>
    /// ��Ϸ����
    /// </summary>
    public class Grid : MonoBehaviour
    {
        public int xDim;
        public int yDim;
        public PiecePrefab[] prefabs;
        public GameObject bgPrefab;//���񱳾�
        public float fillTime = 2;//ÿһ�е�Ԫ�������ʱ��
        private bool inverse = false;//Ԫ������ʱ�Ƿ����б���ƶ�

        private Dictionary<PieceType, GameObject> piecePrefabDict = new Dictionary<PieceType, GameObject>();
        private GamePiece[,] pieces;

        private GamePiece pressPiece;//��갴�µĵؿ�
        private GamePiece enterPiece;//��껬���ĵؿ�

        #region ��ʼ��
        void Start()
        {
            ToPrefabDict();
            InitializeGridBg();
            InitializePiece();

            ////����
            //Destroy(pieces[4, 4].gameObject);
            //SpawnNewPiece(4, 4, PieceType.Bubble);

            StartCoroutine(Fill());
        }

        /// <summary>
        /// ��ʼ����������
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
        /// ��ʼ��Ԫ������
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
        /// �����������
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
        /// ��䵥������
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
                        //�����һ���Ƿ��ǿ�Ԫ�أ�����Ԫ������һ��
                        if (pieceBelow.Type == PieceType.Empty)
                        {
                            piece.MoveablePiece.Move(x, y + 1, fillTime);
                            pieces[x, y + 1] = piece;
                            SpawnNewPiece(x, y, PieceType.Empty);
                            movePiece = true;
                        }
                        else
                        {
                            //б���ƶ��������ֱ��䲻�˾�б����䣩
                            for (int diag = -1; diag <= 1; diag++)
                            {
                                if (diag != 0)
                                {
                                    //�ж�����б���ƶ�����������б���ƶ�
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
                                            //�����ǰ�ؿ��ǿյģ����Դ�����ĵؿ��ƶ�����ǰ
                                            bool hasPieceAbove = true;

                                            //todo:������
                                            int aboveY = y;
                                            //for (int aboveY = y; aboveY >= 0; aboveY--)
                                            {
                                                GamePiece pieceAbove = pieces[diagX, aboveY];
                                                if (pieceAbove.IsMoveable)
                                                {
                                                    //��ǰָ����б�Ϸ��ؿ�����ƶ�����
                                                    break;
                                                }
                                                else if (!pieceAbove.IsMoveable && pieceAbove.Type != PieceType.Empty)
                                                {
                                                    //��ǰָ����б�Ϸ��ؿ鲻���ƶ�
                                                    hasPieceAbove = false;
                                                    break;
                                                }
                                            }

                                            if (!hasPieceAbove)
                                            {
                                                //�����ƶ��ؿ�
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

            //��������Ԫ��
            for (int x = 0; x < xDim; x++)
            {
                GamePiece pieceBelow = pieces[x, 0];

                if (pieceBelow.Type == PieceType.Empty)
                {
                    //����ʹ��-1���꣬���������ڶ�����һ��
                    GameObject newPiece = Instantiate(piecePrefabDict[PieceType.Normal], GetWorldPosition(x, -1), Quaternion.identity, this.transform);

                    //�ƶ���0��
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
        /// �½�
        /// </summary>
        private GamePiece SpawnNewPiece(int x, int y, PieceType type)
        {
            var newPiece = Instantiate(piecePrefabDict[type], GetWorldPosition(x, y), Quaternion.identity, this.transform);

            pieces[x, y] = newPiece.GetComponent<GamePiece>();
            pieces[x, y].Initialize(x, y, this, type);

            return pieces[x, y];
        }


        /// <summary>
        /// ����(0,0)��ƫ������
        /// �����������������ʾ��
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
        /// �Ƿ�����
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
        /// ���������ؿ�
        /// </summary>
        /// <param name="piece1"></param>
        /// <param name="piece2"></param>
        public void SwapPiece(GamePiece piece1, GamePiece piece2)
        {
            if (!piece1.IsMoveable || !piece2.IsMoveable)
                return;

            //��Ҫ�Ƚ���Ȼ�����ȥ����Ƿ���Խ���
            pieces[piece1.X, piece1.Y] = piece2;
            pieces[piece2.X, piece2.Y] = piece1;

            //�ж��Ƿ���ڽ�����������������
            if (GetMatch(piece1, piece2.X, piece2.Y) != null ||
                GetMatch(piece2, piece1.X, piece1.Y) != null)
            {
                //���Ͻ�������
                int piece1X = piece1.X;
                int piece1Y = piece1.Y;
                int piece2X = piece2.X;
                int piece2Y = piece2.Y;

                piece1.MoveablePiece.Move(piece2X, piece2Y, fillTime);
                piece2.MoveablePiece.Move(piece1X, piece1Y, fillTime);

                //�����صؿ�
                ClearAllValidMatches();
            }
            else
            {
                //�����Ͻ����������ָ�
                pieces[piece1.X, piece1.Y] = piece1;
                pieces[piece2.X, piece2.Y] = piece2;
            }
        }

        /// <summary>
        /// ��¼�ƶ��ؿ�ʱ��갴�µĵؿ�
        /// </summary>
        /// <param name="piece"></param>
        public void PressPiece(GamePiece piece)
        {
            pressPiece = piece;
        }

        /// <summary>
        /// ��¼�ƶ��ؿ�ʱ����϶��ĵؿ�
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
        /// �����صؿ�
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
        /// ����ؿ�
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

        #region ƥ�䷽��
        /// <summary>
        /// �ж��ƶ����Ƿ���ڿ������������
        /// </summary>
        /// <param name="piece"></param>
        /// <param name="newX"></param>
        /// <param name="newY"></param>
        /// <returns></returns>
        public List<GamePiece> GetMatch(GamePiece piece, int newX, int newY)
        {
            if (piece.IsColored)
            {
                ColorType color = piece.ColorPiece.Color;//ƥ������ܵ���ɫ�Ƿ�һ��

                var horizontalPieces = new List<GamePiece>();
                var verticalPieces = new List<GamePiece>();
                var matchingPieces = new List<GamePiece>();

                //ƥ��ˮƽ
                horizontalPieces.Add(piece);
                for (int direction = 0; direction <= 1; direction++)
                {
                    //ƥ��ˮƽ���ҷ���
                    for (int xOffest = 1; xOffest < xDim; xOffest++)
                    {
                        int x = 0;
                        if (direction == 0)
                            x = newX - xOffest; //����ƥ��
                        else
                            x = newX + xOffest;//����ƥ��

                        //Խ����
                        if (x < 0 || x >= xDim)
                            break;

                        //ƥ���ƶ�����λ��ͬһ���Ƿ�����ͬ��ɫ
                        if (pieces[x, newY].IsColored && pieces[x, newY].ColorPiece.Color == color)
                            horizontalPieces.Add(pieces[x, newY]);
                        else
                            break;
                    }
                }

                //�����ͬ��ɫ������������뵽��ƥ����б���
                if (horizontalPieces.Count >= 3)
                    matchingPieces.AddRange(horizontalPieces);

                //��ѯT�ͻ�L��
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
                                    y = newY - yOffest; //����ƥ��
                                else
                                    y = newY + yOffest;//����ƥ��

                                //Խ����
                                if (y < 0 || y >= yDim)
                                    break;

                                //ÿ����ƥ���ˮƽԪ�ض�ȥ����ƥ�䴹ֱ����
                                var hPiece = horizontalPieces[i];
                                if (pieces[hPiece.X, y].IsColored && pieces[hPiece.X, y].ColorPiece.Color == color)
                                    verticalPieces.Add(pieces[hPiece.X, y]);
                                else
                                    break;
                            }
                        }

                        //��������������Ҫ���
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


                //û��ƥ��ɹ������֮ǰ��ƥ����
                horizontalPieces.Clear();
                verticalPieces.Clear();

                //��ֱˮƽ
                verticalPieces.Add(piece);
                for (int direction = 0; direction <= 1; direction++)
                {
                    //ƥ��ˮƽ���ҷ���
                    for (int yOffest = 1; yOffest < yDim; yOffest++)
                    {
                        int y = 0;
                        if (direction == 0)
                            y = newX - yOffest; //����ƥ��
                        else
                            y = newX + yOffest;//����ƥ��

                        //Խ����
                        if (y < 0 || y >= yDim)
                            break;

                        //ƥ���ƶ�����λ��ͬһ���Ƿ�����ͬ��ɫ
                        if (pieces[newX, y].IsColored && pieces[newX, y].ColorPiece.Color == color)
                            verticalPieces.Add(pieces[newX, y]);
                        else
                            break;
                    }
                }

                //�����ͬ��ɫ������������뵽��ƥ����б���
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
                                    x = newX - xOffest; //����ƥ��
                                else
                                    x = newX + xOffest;//����ƥ��

                                //Խ����
                                if (x < 0 || x >= xDim)
                                    break;

                                //ÿ����ƥ���ˮƽԪ�ض�ȥ����ƥ�䴹ֱ����
                                var vPiece = verticalPieces[i];
                                if (pieces[x, vPiece.Y].IsColored && pieces[x, vPiece.Y].ColorPiece.Color == color)
                                    horizontalPieces.Add(pieces[x, vPiece.Y]);
                                else
                                    break;
                            }
                        }

                        //��������������Ҫ���
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