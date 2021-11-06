using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public AudioSource theMusic;
    public bool startPlaying;
    public bool stopPlaying;
    public BeatScroller theBS; // holder das notas
    public List<GameObject> PrefabList;
    public static GameManager instance;
    public GameObject arrow;
    public float temporario;

    public GameObject TelaInicio;
    public GameObject TelaHistoria;
    public GameObject TelaGameOver;
    public GameObject TelaVitoria;
    public GameObject TelaSim;
    public GameObject TelaNao;



    float Max_X = 1.266071f;
    public GameObject barra;

    public float beatTempo;
    public float velSpawnArrow;
    public float timer = 0;

    public float currentScore;
    public float scorePerNote;

    public int currentMultiplier;
    public int multiplierTracker;
 //   public int[] multiplierThresholds = new int[3] { 4, 12, 28 };
    public List<string> Arrows;

    void Start()
    {
        instance = this;
        barra.GetComponent<Transform>().localScale = new Vector3(0.15f, barra.GetComponent<Transform>().localScale.y,barra.GetComponent<Transform>().localScale.z);
        currentMultiplier = 1;
        scorePerNote = 0.008f;
        Arrows.Add("Right");
        Arrows.Add("Left");
        Arrows.Add("Up");
        Arrows.Add("Down");
    }


    void Update()
    {
        if(startPlaying == true && stopPlaying == false)
        {
            if ((timer += Time.deltaTime) >= (velSpawnArrow))
            {
                timer = 0;

                int sorteado = Random.Range(0, 4);
                MakeSpawnArrow(Arrows[sorteado]);
            }
        }

        if (barra.GetComponent<Transform>().localScale.x < Max_X)
        {
           barra.GetComponent<Transform>().localScale = new Vector3((barra.GetComponent<Transform>().localScale.x) + temporario, barra.GetComponent<Transform>().localScale.y,barra.GetComponent<Transform>().localScale.z);

            temporario = 0;
        }
        else
        {
            Vitoria();
            theMusic.Stop();
            stopPlaying = true;
            //se musica acabou entao stopplaying true e vitoria
        }


    }

    public void NoteHit()
    {
        Debug.Log("Hit On Time");

        currentScore += scorePerNote;
        temporario = scorePerNote;
    }

    public void NoteMissed()
    {
        Debug.Log("Missed Note");
        temporario = -scorePerNote * 2;

        if(barra.GetComponent<Transform>().localScale .x < 0.15)
        {
            TelaGameOver.SetActive(true);
            theMusic.Stop();
            stopPlaying = true;
            
        }
    }

    public void SelectArrow(string direction)
    {
        switch (direction)
        {
            case "Left":
                arrow = PrefabList[0];
                break;
            case "Up":
                arrow = PrefabList[1];
                break;
            case "Down":
                arrow = PrefabList[2];
                break;
            case "Right":
                arrow = PrefabList[3];
                break;
            default:
                Debug.Log("Opção inválida");
                break;

        }  
    }

    public void MakeSpawnArrow(string arrowDirection)
    {
        SelectArrow(arrowDirection);
        GameObject spawnArrow = UnityEngine.Object.Instantiate(arrow);
        spawnArrow.transform.parent = theBS.transform;


    //    spawnArrow.GetComponent<Transform>().position = new Vector3(20f, 20f, -20f);
    }


    public void Tela_Inicio()
    {
        TelaInicio.SetActive(false);
    }
    public void Tela_Historia()
    {
        TelaHistoria.SetActive(false);
        
        stopPlaying = false;
        startPlaying = true;
        theMusic.Play();

    }
    public void Vitoria()
    {
        TelaVitoria.SetActive(true);
        //ganhou o jogo
    }
    public void Tela_Sim()
    {
        TelaSim.SetActive(true);
        TelaVitoria.SetActive(false);
    }
    public void Tela_Nao()
    {
        TelaNao.SetActive(true);
        TelaVitoria.SetActive(false);
    }

    public void Tela_GameOver()
    {
        SceneManager.LoadScene(0);
    }

    

}
