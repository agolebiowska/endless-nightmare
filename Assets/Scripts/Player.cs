using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private Vector2 targetPos;
    public int yStep;
    public float speed;
    public int health = 3;
    public Image[] hearts;

    public float maxHeight, minHeight;
    public GameObject effect;
    private Shake shake;
    public Text healthDisplay;

    public GameObject gameOver;
    public GameObject tapSound;

    private Vector2 touchOrigin = -Vector2.one;

    void Start()
    {
        shake = GameObject.FindGameObjectWithTag("ScreenShake").GetComponent<Shake>();
    }

	void Update ()
    {
        healthDisplay.text = health.ToString();

        for (int i = 0; i < hearts.Length; i++) {
            if (i < health) {
                hearts[i].enabled = true;
            } else {
                hearts[i].enabled = false;
            }
        }

        if (health <= 0)
        {
            gameOver.SetActive(true);
            Destroy(gameObject);
        }

        transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

        // Mobile input
        if (Input.touchCount > 0)
        {
            Touch touch = Input.touches[0];

            if (touch.phase == TouchPhase.Began)
            {
                if ((touch.position.y > Screen.height / 2) && transform.position.y < maxHeight)
                {
                    Instantiate(effect, transform.position, Quaternion.identity);
                    Instantiate(tapSound, transform.position, Quaternion.identity);
                    shake.CamShake();
                    targetPos = new Vector2(transform.position.x, transform.position.y + yStep);
                }
                else if ((touch.position.y < Screen.height / 2) && transform.position.y > minHeight)
                {
                    Instantiate(effect, transform.position, Quaternion.identity);
                    Instantiate(tapSound, transform.position, Quaternion.identity);
                    shake.CamShake();
                    targetPos = new Vector2(transform.position.x, transform.position.y - yStep);
                }
            }
        }

        // PC input
        if (Input.GetKeyDown(KeyCode.UpArrow) && transform.position.y < maxHeight)
        {
            Instantiate(effect, transform.position, Quaternion.identity);
            Instantiate(tapSound, transform.position, Quaternion.identity);
            shake.CamShake();
            targetPos = new Vector2(transform.position.x, transform.position.y + yStep);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && transform.position.y > minHeight)
        {
            Instantiate(effect, transform.position, Quaternion.identity);
            Instantiate(tapSound, transform.position, Quaternion.identity);
            shake.CamShake();
            targetPos = new Vector2(transform.position.x, transform.position.y - yStep);
        }
    }
}
