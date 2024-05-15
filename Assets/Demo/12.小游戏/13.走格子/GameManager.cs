using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Demo.Games.GoGrid
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Map map;
        [SerializeField] private List<Player> players;
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private Button button;

        private bool isDoing = false;//是否正在进行操作
        private int playerIndex = 0;

        private bool isPlayerTurn = false;//是否是玩家回合

        private Player curPlayer;

        #region Instance
        private static GameManager _instance;
        public static GameManager Instance
        {
            get { return _instance; }
        }
        #endregion

        private void Awake()
        {
            _instance = this;

            //玩家使用的停止随机数
            button.onClick.AddListener(new UnityEngine.Events.UnityAction(StopRoll));

            for (int i = 0; i < players.Count; i++)
            {
                players[i].OnMovementEnd += GameManager_OnMovementEnd;
            }
        }

        private void Update()
        {
            if (!isDoing)
            {
                Turn();
            }
        }

        /// <summary>
        /// 变换轮回
        /// </summary>
        private void Turn()
        {
            isDoing = true;

            curPlayer = players[playerIndex];
            if (curPlayer is ComputerPlayer)
            {
                //AI
                isPlayerTurn = false;
                button.gameObject.SetActive(isPlayerTurn);
                StartCoroutine(ExecuteComputerPlayer());
            }
            else if (curPlayer is HumanPlayer)
            {
                //玩家
                isPlayerTurn = true;
                button.gameObject.SetActive(isPlayerTurn);
                StartCoroutine(ExecuteHumanPlayer());
            }

            playerIndex = (++playerIndex) % players.Count;          
        }

        /// <summary>
        /// 电脑行为
        /// </summary>
        private IEnumerator ExecuteComputerPlayer()
        {
            yield return new WaitForSeconds(1);

            int rollCount = 0;
            int rollValue = 0;
            while (rollCount <= 5)//随机指定数量后结束
            {
                yield return new WaitForSeconds(0.05f);
                rollValue = RandomRoll();
                rollCount++;
            }

            //设置随机数，进行移动
            curPlayer.SetRoll(rollValue);

            yield return null;
        }

        /// <summary>
        /// 玩家行为
        /// </summary>
        private IEnumerator ExecuteHumanPlayer()
        {
            int rollValue = 0;
            while (isPlayerTurn)//直到玩家点击按钮后才停止随机
            {
                yield return new WaitForSeconds(0.05f);
                rollValue = RandomRoll();
            }

            curPlayer.SetRoll(rollValue);

            yield return null;
        }

        /// <summary>
        /// 玩家移动结束事件
        /// </summary>
        private void GameManager_OnMovementEnd()
        {
            if (curPlayer != null && curPlayer.IsArrivedEnd)
            {
                Debug.Log("到达终点，游戏结束");
            }
            else
            {
                //继续回合
                isDoing = false;
            }
        }

        /// <summary>
        /// 随机点数
        /// </summary>
        private int RandomRoll()
        {
            var count = Random.Range(1, 6);
            text.text = count.ToString();
            return count;
        }

        /// <summary>
        /// 停止随机点数
        /// </summary>
        private void StopRoll()
        {
            isPlayerTurn = false;
            button.gameObject.SetActive(false);
        }
    }
}