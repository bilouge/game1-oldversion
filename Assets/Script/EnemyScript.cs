using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyScript : MonoBehaviour
{
    public GameObject[] spawnerArr;
    public GameObject enemy;
    public GameObject character;
    public float enemySpeed = 10f;
    Collider2D enemyCollider;
    GameObject instEnemy;
    float spawnTime = 5f;
    Vector3 enemyDirection;
    public bool hasGameEnded = false;
    int[] randomArr = new int[2];
    int randomNb;
    string[] colorArr = new string[2];
    int tally;
    float blinkSpeed = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        colorArr[0] = "blue";
        colorArr[1] = "red";
        InvokeRepeating("ColorFunction", spawnTime, spawnTime);
    }

    // Update is called once per frame
    void Update()
    {
        //If the enemy is instantiated
        if (instEnemy != null && enemyCollider != null)
        {
            instEnemy.transform.Translate(enemyDirection * enemySpeed * Time.deltaTime, relativeTo: Space.World);
            if (enemyCollider.IsTouching(character.GetComponent<Collider2D>()))
            {
                hasGameEnded = true;
                Destroy(instEnemy);
            }

        }
        //Destroy instantiated enemy if out of bounds
        if (instEnemy != null && (instEnemy.transform.position.x > 10f || instEnemy.transform.position.x < -10f || instEnemy.transform.position.y > 10f || instEnemy.transform.position.y < -10f))
        {
            Destroy(instEnemy);
        }
        
    }
    //Instantiate and calculates requires variables
    void ColorFunction()
    {
        if (!hasGameEnded)
        {
            randomNb = Random.Range(0, 2);
            StartCoroutine("SpawnEnemy");
        }
    }
    public IEnumerator SpawnEnemy()
    {
        //tally count, the greater the value, the more blinks and the easier the game
        tally = 4;
        //Code for the blue spawners
        if (colorArr[randomNb] == "blue")
        {
            randomNb = Random.Range(0, 4);
            //Makes one of the spawners blink white and blue
            while (tally > 0)
            {
                spawnerArr[randomNb].GetComponent<SpriteRenderer>().color = Color.blue;
                yield return new WaitForSeconds(blinkSpeed);
                spawnerArr[randomNb].GetComponent<SpriteRenderer>().color = Color.white;
                yield return new WaitForSeconds(blinkSpeed);
                tally--;
            }
            if (blinkSpeed - 0.01f > 0.01f) blinkSpeed -= 0.01f;
            instEnemy = Instantiate(enemy, spawnerArr[randomNb].transform.position, spawnerArr[randomNb].transform.rotation);
        }
        //
        else
        {
            //Makes one of the spawners blink white and red
            randomNb = Random.Range(0, 4);
            while (tally > 0)
            {   
                spawnerArr[randomNb].GetComponent<SpriteRenderer>().color = Color.red;
                yield return new WaitForSeconds(blinkSpeed);
                spawnerArr[randomNb].GetComponent<SpriteRenderer>().color = Color.white;
                yield return new WaitForSeconds(blinkSpeed);
                tally--;
            }
            if(blinkSpeed - 0.01f > 0.01f) blinkSpeed -= 0.01f;
            //If the spawner that changes colors is Up OR Down, randomly select Left OR Right to instantiate the enemy
            if (randomNb == 0 || randomNb == 2)
            {
                randomArr[0] = 1;
                randomArr[1] = 3;
            }
            //If the spawner that changes colors is Left OR Right, randomly select Up OR Down to instantiate the enemy
            else
            {
                randomArr[0] = 0;
                randomArr[1] = 2;
            }
            //Instantatiate based on the values above
            randomNb = Random.Range(0, 2);
            instEnemy = Instantiate(enemy, spawnerArr[randomArr[randomNb]].transform.position, spawnerArr[randomArr[randomNb]].transform.rotation);
        }
        enemyCollider = instEnemy.GetComponent<Collider2D>();
        enemyDirection = new Vector3(0, 0, 0) - instEnemy.transform.position;
        enemyDirection = enemyDirection / enemyDirection.magnitude;
    }
}
