using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrassCollision : MonoBehaviour
{
    public GameObject routePannel;
    public GameObject nightScreen;
    public GameObject playerObj;
    public Sprite nightSprite;
    public Sprite daySprite;
    public Sprite townSprite;
    public Image routeImage;
    public Text routeText;
    public Transform player;

    public GameObject battleScene;
    public string whatPKMN;
    public string whatLevel;

    int route;
    int hour;
    int randomEncounter, randomPokemon, randomLevel;
    bool isNight;
    string pokemon;


    // Start is called before the first frame update
    void Start()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        whatPKMN = null;
    }

    // Update is called once per frame
    void Update()
    {
        hour = System.DateTime.Now.Hour;
        if ((hour >= 18 || hour <= 5) && (playerObj.transform.position.x < 800f && playerObj.transform.position.y < 800f))
        {
            nightScreen.SetActive(true);
            isNight = true;
        } else if ((hour < 18 || hour > 5) || (playerObj.transform.position.x >= 800f && playerObj.transform.position.y >= 800f))
        {
            nightScreen.SetActive(false);
            isNight = false;
        }
        
    }

    void HideText ()
    {
        routePannel.SetActive(false);
    }

    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Grass")
        {
            Encounters();
        }
        if (collision.gameObject.tag == "Route 1")
        {
            CheckNight();
            if (route == 0)
            {
                routeText.text = "Route 1";
                routePannel.SetActive(true);
                Invoke("HideText", 2.0f);
                route++;
            } else
            {
                routeImage.sprite = townSprite;
                routeText.color = Color.black;
                routeText.text = "Starting Town";
                routePannel.SetActive(true);
                Invoke("HideText", 2.0f);
                route--;
            }
        }
    }

    void CheckNight()
    {
        if (isNight)
        {
            nightScreen.SetActive(true);
            routeImage.sprite = nightSprite;
            routeText.color = Color.white;
            
        }
    }

    public void Encounters ()
    {
        if (route == 1)
        {
            randomEncounter = (int)Random.Range(1f, 6f);
            if (randomEncounter == 1f)
            {
                randomPokemon = (int)Random.Range(1f, 101f);
                if (randomPokemon >= 1 && randomPokemon <= 45)
                {
                    pokemon += "Wurmple";
                    
                } else if (randomPokemon > 45 && randomPokemon <= 90)
                {
                    pokemon += "Poochyena";
                } else
                {
                    pokemon += "Zigzagoon";
                }
                randomLevel = (int)Random.Range(1f, 3f);
                if (randomLevel == 1)
                {
                    whatLevel = "Lvl 2 ";
                } else
                {
                    whatLevel = "Lvl 3 ";
                }
                //Debug.Log(pokemon);
                whatPKMN = pokemon;
                battleScene.SetActive(true);
                /*dialouge = "A wild " + whatPKMN + " approaches...";
                Encounter.text = dialouge*/
                pokemon = "";
                Time.timeScale = 0f;
            }
        }
    }
}
