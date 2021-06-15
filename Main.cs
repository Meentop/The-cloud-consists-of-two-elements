using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    [SerializeField] GameObject fire, water, main, loseText, splitPar;
    bool isActiveFire = false, isLose = false, cloud = true;
    float timer = 0;

    Sounds sound;

    private void Start()
    {
        sound = Sounds.Instante;
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(1) && !cloud && !isLose)
        {
            if (isActiveFire)
                SetActiveWater();
            else
                SetActiveFire();
            sound.PlaySwitch();
        }
        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.R))
            ReturnLvl();

        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }

    public void SpawnFireAndWater(Vector3 mainPos)
    {
        if (timer <= 0)
        {
            sound.PlayDouble();
            Destroy(GameObject.FindGameObjectWithTag("Player"));
            Vector3 splitPos = new Vector3(mainPos.x, mainPos.y, mainPos.z - 2f);
            Instantiate(splitPar, splitPos, Quaternion.identity);
            Vector3 firePos = new Vector3(mainPos.x - 0.7f, mainPos.y, mainPos.z);
            Vector3 waterPos = new Vector3(mainPos.x + 0.7f, mainPos.y, mainPos.z);
            Instantiate(fire, firePos, Quaternion.Euler(0, 0, 0));
            Instantiate(water, waterPos, Quaternion.Euler(0, 0, 0));
            SetActiveFire();
            timer = 0.5f;
            cloud = false;
        }
    }

    public void SpawnMainPlayer(Vector3 playerPos)
    {
        if (timer <= 0)
        {
            sound.PlayDouble();
            Destroy(GameObject.FindGameObjectWithTag("FirePlayer"));
            Destroy(GameObject.FindGameObjectWithTag("WaterPlayer"));
            Vector3 splitPos = new Vector3(playerPos.x, playerPos.y, playerPos.z - 2f);
            Instantiate(splitPar, splitPos, Quaternion.identity);
            Instantiate(main, playerPos, Quaternion.Euler(0, 0, 0));
            Camera.main.GetComponent<CameraTargeting>().SetTarget(GameObject.FindGameObjectWithTag("Player").transform);
            timer = 0.5f;
            cloud = true;
        }
    }

    public void SetActiveFire()
    {
        GameObject.FindGameObjectWithTag("FirePlayer").GetComponent<Player>().active = true;
        GameObject.FindGameObjectWithTag("WaterPlayer").GetComponent<Player>().active = false;
        Camera.main.GetComponent<CameraTargeting>().SetTarget(GameObject.FindGameObjectWithTag("FirePlayer").transform);
        isActiveFire = true;
    }

    public void SetActiveWater()
    {
        GameObject.FindGameObjectWithTag("FirePlayer").GetComponent<Player>().active = false;
        GameObject.FindGameObjectWithTag("WaterPlayer").GetComponent<Player>().active = true;
        Camera.main.GetComponent<CameraTargeting>().SetTarget(GameObject.FindGameObjectWithTag("WaterPlayer").transform);
        isActiveFire = false;
    }

    public void ReturnLvl()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadNextLvl()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Lose()
    {
        sound.PlayDeath();
        loseText.SetActive(true);
        if(GameObject.FindGameObjectsWithTag("FirePlayer").Length == 1)
            GameObject.FindGameObjectWithTag("FirePlayer").GetComponent<Player>().active = false;
        if (GameObject.FindGameObjectsWithTag("WaterPlayer").Length == 1)
            GameObject.FindGameObjectWithTag("WaterPlayer").GetComponent<Player>().active = false;
        isLose = true;
    }
}
