using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTable : MonoBehaviour
{
    private Transform _container;
    private Transform _template;

    private List<ScoreModel> scoreModelList = new List<ScoreModel>();
    private List<Transform> scoreTransformList = new List<Transform>();

    private void Awake()
    {
        //获取容器与分数模板
        _container = transform.Find("Container");
        _template = _container.Find("Template");

        //用于生成的模板需要隐藏
        _template.gameObject.SetActive(false);

        //测试数据
        scoreModelList.Add(new ScoreModel(100, "aaa"));
        scoreModelList.Add(new ScoreModel(99, "bbb"));
        scoreModelList.Add(new ScoreModel(97, "ccc"));
        scoreModelList.Add(new ScoreModel(66, "ddd"));
        scoreModelList.Add(new ScoreModel(55, "eee"));
        scoreModelList.Add(new ScoreModel(44, "fff"));
        scoreModelList.Add(new ScoreModel(33, "ggg"));

        foreach (var model in scoreModelList)
        {
            CreateTemplate(model, _container, scoreTransformList);
        }
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void CreateTemplate(ScoreModel model, Transform container, List<Transform> currentScoreList)
    {
        //一个模板的高度
        float templateHeight = 40f;

        var playerScoreControl = Instantiate(_template, container);
        //设置新分数控件的位置
        var playerScoreControlRect = playerScoreControl.GetComponent<RectTransform>();
        playerScoreControlRect.anchoredPosition = new Vector2(0, -templateHeight * currentScoreList.Count);
        playerScoreControlRect.gameObject.SetActive(true);

        //设置新分数的内容
        int rank = currentScoreList.Count + 1;
        string rankString = $"第{rank}名";
        playerScoreControlRect.Find("PositionText").GetComponent<Text>().text = rankString;
        playerScoreControlRect.Find("ScoreText").GetComponent<Text>().text = model.score.ToString();
        playerScoreControlRect.Find("NameText").GetComponent<Text>().text = model.name.ToString();

        currentScoreList.Add(playerScoreControl);
    }

    public class ScoreModel
    {
        public ScoreModel(float score, string name)
        {
            this.score = score;
            this.name = name;
        }

        public float score;
        public string name;
    }
}
