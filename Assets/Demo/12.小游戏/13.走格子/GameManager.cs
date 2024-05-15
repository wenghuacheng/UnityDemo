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

        private bool isDoing = false;//�Ƿ����ڽ��в���
        private int playerIndex = 0;

        private bool isPlayerTurn = false;//�Ƿ�����һغ�

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

            //���ʹ�õ�ֹͣ�����
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
        /// �任�ֻ�
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
                //���
                isPlayerTurn = true;
                button.gameObject.SetActive(isPlayerTurn);
                StartCoroutine(ExecuteHumanPlayer());
            }

            playerIndex = (++playerIndex) % players.Count;          
        }

        /// <summary>
        /// ������Ϊ
        /// </summary>
        private IEnumerator ExecuteComputerPlayer()
        {
            yield return new WaitForSeconds(1);

            int rollCount = 0;
            int rollValue = 0;
            while (rollCount <= 5)//���ָ�����������
            {
                yield return new WaitForSeconds(0.05f);
                rollValue = RandomRoll();
                rollCount++;
            }

            //����������������ƶ�
            curPlayer.SetRoll(rollValue);

            yield return null;
        }

        /// <summary>
        /// �����Ϊ
        /// </summary>
        private IEnumerator ExecuteHumanPlayer()
        {
            int rollValue = 0;
            while (isPlayerTurn)//ֱ����ҵ����ť���ֹͣ���
            {
                yield return new WaitForSeconds(0.05f);
                rollValue = RandomRoll();
            }

            curPlayer.SetRoll(rollValue);

            yield return null;
        }

        /// <summary>
        /// ����ƶ������¼�
        /// </summary>
        private void GameManager_OnMovementEnd()
        {
            if (curPlayer != null && curPlayer.IsArrivedEnd)
            {
                Debug.Log("�����յ㣬��Ϸ����");
            }
            else
            {
                //�����غ�
                isDoing = false;
            }
        }

        /// <summary>
        /// �������
        /// </summary>
        private int RandomRoll()
        {
            var count = Random.Range(1, 6);
            text.text = count.ToString();
            return count;
        }

        /// <summary>
        /// ֹͣ�������
        /// </summary>
        private void StopRoll()
        {
            isPlayerTurn = false;
            button.gameObject.SetActive(false);
        }
    }
}