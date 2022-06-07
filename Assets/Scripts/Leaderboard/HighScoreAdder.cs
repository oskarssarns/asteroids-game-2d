using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using TMPro;
using System.Linq;

public class HighScoreAdder : MonoBehaviour
{

    public GameObject ScrollView;
    public GameObject EntryTemplate;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetHighScores());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public class HighScore
    {
        public string nickname { get; set; }
        public string event_date { get; set; }
        public int score { get; set; }
        public string game { get; set; }
        public string game_version { get; set; }
    }

    IEnumerator GetHighScores()
    {
        WWWForm form = new WWWForm();
        form.AddField("game", Application.productName);

        using (UnityWebRequest www = UnityWebRequest.Post("http://www.gachi.run:8080/get_high_score", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);

                Dictionary<string, List<HighScore>> highScoreList = JsonConvert.DeserializeObject<Dictionary<string, List<HighScore>>>(www.downloadHandler.text);

                List<HighScore> items = highScoreList["message"];

                items = items.OrderByDescending(x => x.score).ToList();

                int rank = 0;
                foreach(HighScore highScore in items){
                    rank += 1;
                    GameObject entry = Instantiate(EntryTemplate) as GameObject;
                    entry.SetActive(true);

                    entry.transform.SetParent(EntryTemplate.transform.parent, false);

                    entry.transform.Find("Rank").gameObject.GetComponent<TextMeshProUGUI>().text = rank.ToString();
                    entry.transform.Find("Name").gameObject.GetComponent<TextMeshProUGUI>().text = highScore.nickname;
                    entry.transform.Find("Score").gameObject.GetComponent<TextMeshProUGUI>().text = highScore.score.ToString();
                }

            }
        }
    }
}
