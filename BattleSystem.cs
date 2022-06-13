using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{

    public GameObject playerPrefab;
    public GameObject WurmPrefab;
    public GameObject ZigPrefab;
    public GameObject PoochPrefab;

    public Transform playerBattleStation;
    public Transform enemyBattleStation;

    Unit playerUnit;
    Unit enemyUnit;

    public BattleHudPlayer playerHUD;
    public BattleHud enemyHUD;

    public BattleState state;

    public GrassCollision encounter;

    public GameObject player;
    public GameObject battleScene;
    public Text DialogueText;

    public GameObject attackTextButton;
    public GameObject bagTextButton;
    public GameObject runTextButton;
    public GameObject pokemonTextButton;
    public GameObject selector;

    public string PKMN;
    public bool foundPKMN;
    public bool attackTime;
    public bool attackMenu;

    public int totalButtonsY = 2;
    public int totalButtonsX = 2;
    public float yOffset = 15f;
    public float xOffset = 15f;
    public int yIndex = 0;
    public int xIndex = 0;

    public bool hasHappenedAlready = false;

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        foundPKMN = false;
        attackTime = false;
        attackMenu = false;

    }

    void Update()
    {
        GrassCollision grassCollision = player.GetComponent<GrassCollision>();
        PKMN = grassCollision.whatPKMN;

        if (battleScene.activeInHierarchy == true)
        {
            foundPKMN = true;
        }
        else
        {
            foundPKMN = false;
        }

        if (foundPKMN == true && attackTime == false)
        {
            DialogueText.text = "A wild " +PKMN+ " approaches...";
            if (Input.GetButtonDown("Interact"))
            {
                attackTime = true;
                attackMenu = true;
            }
        }
        else if (foundPKMN == true && attackTime == true)
        {
            attacking();
        }

        if (PKMN.Length != 0 && hasHappenedAlready == false)
        {
            battle();
            hasHappenedAlready = true;
        }
        if(PKMN.Length != 0)
        {
            MenuMoving();
        }
    }

    void battle()
    {
        GameObject playerGo = Instantiate(playerPrefab, playerBattleStation);
        playerUnit = playerGo.GetComponent<Unit>();
        if (PKMN == "Wurmple")
        {
            GameObject enemyGo = Instantiate(WurmPrefab, enemyBattleStation);
            enemyUnit = enemyGo.GetComponent<Unit>();
        }
         else if (PKMN == "Poochyena")
        {
            GameObject enemyGo = Instantiate(PoochPrefab, enemyBattleStation);
            enemyUnit = enemyGo.GetComponent<Unit>();
        }
        else if (PKMN == "Zigzagoon")
        {
            GameObject enemyGo = Instantiate(ZigPrefab, enemyBattleStation);
            enemyUnit = enemyGo.GetComponent<Unit>();
        }
    }

    void MenuMoving()
    { 
        if (attackMenu == true)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                if (yIndex < totalButtonsY - 1)
                {
                    yIndex++;
                    Vector2 position = selector.transform.position;
                    position.y -= yOffset;
                    selector.transform.position = position;

                }
            }
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                if (yIndex > 0)
                {
                    yIndex--;
                    Vector2 position = selector.transform.position;
                    position.y += yOffset;
                    selector.transform.position = position;

                }
            }
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                if (xIndex < totalButtonsX - 1)
                {
                    xIndex++;
                    Vector2 position = selector.transform.position;
                    position.x += xOffset;
                    selector.transform.position = position;

                }
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                if (xIndex > 0)
                {
                    xIndex--;
                    Vector2 position = selector.transform.position;
                    position.x -= xOffset;
                    selector.transform.position = position;

                }
            }
        }

        enemyHUD.SetHUD(enemyUnit);
        playerHUD.SetHudPlayer(playerUnit);

    }
    
    public void attacking()
    {
        DialogueText.text = "Choose an action:";
        state = BattleState.PLAYERTURN;
        attackTextButton.SetActive(true);
        runTextButton.SetActive(true);
        bagTextButton.SetActive(true);
        pokemonTextButton.SetActive(true);
        selector.SetActive(true);
    }
}
