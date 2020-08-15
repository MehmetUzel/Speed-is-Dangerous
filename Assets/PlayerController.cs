using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    float movespeed = 1.5f;
    bool lookingright = true;
    public Text ScoreText;
    public Text HScoreText;
    int score, hscore;
    public ParticleSystem particlePrefab;
    public Transform RayOrigin;
    public GameManager gameManager;
    delegate void TurnDelegate();
    TurnDelegate turnDelegate;

    public Animator animator { get { return GetComponent<Animator>(); } }

    // Start is called before the first frame update
    void Start()
    {
        LoadHScore();
        HScoreText.text = hscore.ToString();

#if UNITY_EDITOR_OSX
        turnDelegate = TurnWithKeyboard;
#endif
#if UNITY_EDITOR_WIN
        turnDelegate = TurnWithKeyboard;
#endif
#if UNITY_ANDROID 
        turnDelegate = TurnWithTouch;
#endif
#if UNITY_IOS
        turnDelegate = TurnWithTouch;
#endif

    }

    private void LoadHScore()
    {
        hscore = PlayerPrefs.GetInt("HS");
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameManager.GameStarted) return;
        animator.SetTrigger("GameStarted");
        //transform.Translate(new Vector3(0, 0, 1) * movespeed * Time.deltaTime);
        transform.position += transform.forward * movespeed * Time.deltaTime;
        //Turn();
        turnDelegate();
        CheckFalling();
        movespeed += 0.002f;
    }

    private void CheckFalling()
    {

        if(!Physics.Raycast(RayOrigin.position,new Vector3(0, -1, 0)))
        {
            animator.SetTrigger("Falling");
            gameManager.RestartGame();
        }
    }

    private void TurnWithKeyboard()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Turn();
        }
    }

    private void TurnWithTouch()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Turn();
        }
    }

    private void Turn()
    {
        if (lookingright)
        {
            transform.Rotate(new Vector3(0, -90, 0));
        }
        else
        {
            transform.Rotate(new Vector3(0, 90, 0));
        }

        lookingright = !lookingright;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("crystal"))
        {
            MakeScore();
            var effect = Instantiate(particlePrefab);
            effect.transform.position = transform.position+ new Vector3(0.3f , 0, 0.3f);
            Destroy(effect, 1f);
            Destroy(other.gameObject);
        }
    }

    private void MakeScore()
    {
        score++;
        if(score > hscore)
        {
            hscore = score;
            HScoreText.text = hscore.ToString();
            SaveHScore();
        }
        ScoreText.text = score.ToString();
    }

    private void SaveHScore()
    {
        PlayerPrefs.SetInt("HS", hscore);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(collision.gameObject, 3f);
    }
}
