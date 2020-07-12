using UnityEngine;
using UnityEngine.UI;

public class AchievementCard : MonoBehaviour
{
    public Achievement Achievement;
    public Text Title;
    public Text Description;
    public float TimeBeforeHide;
    float timeElapsed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
        if (timeElapsed > TimeBeforeHide)
        {
            Destroy(gameObject);
        }
    }

    private void OnDisable()
    {
        Destroy(gameObject);
    }

    public void UseForAchivement(Achievement achievement)
    {
        Debug.Assert(achievement != null);
        Achievement = achievement;
        Title.text = Achievement.title;
        Description.text = Achievement.description;
    }
}
