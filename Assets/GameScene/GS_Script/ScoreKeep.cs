using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeep : MonoBehaviour
{

    public Text Scoretext;
    int score;

    public Image Srank;
    public Image Arank;
    public Image Brank;
    public Image Crank;
    public Image Drank;

    // Start is called before the first frame update
    void Start()
    {

        score = UI.GetScore();
        Scoretext.text = string.Format("Score:{0}",score);

        Drank.enabled = false; Crank.enabled = false; Brank.enabled = false; Arank.enabled = false; Srank.enabled = false;

        if (score >= 0 && score < 300)
        {
            Drank.enabled = true;
        }

        if (score >= 300 && score < 600)
        {
            Crank.enabled = true;
        }

        if (score >= 600 && score < 900)
        {
            Brank.enabled = true;
        }

        if (score >= 900 && score < 1500)
        {
            Arank.enabled = true;
        }

        if (score >= 1500 )
        {
            Srank.enabled = true;
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
