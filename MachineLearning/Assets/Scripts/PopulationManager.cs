using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PopulationManager : MonoBehaviour {

    public GameObject personPrefab; // reference to the go person
    public int populationSize = 10;

    List<GameObject> population = new List<GameObject>();
    public static float elapsed = 0; //Timer to count how long the people in the scene have survived


    [SerializeField]
    int trialTime = 10; //Define how much time we have to click on the people
    int generation = 1;

    GUIStyle guiStyle = new GUIStyle(); //Set up to quickly modify things that happen on the gui

    private void OnGUI()
    {
        guiStyle.fontSize = 50;
        guiStyle.normal.textColor = Color.white;
        GUI.Label(new Rect(10, 10, 100, 20), "Generation: " + generation, guiStyle);
        GUI.Label(new Rect(10, 65, 100, 20), "Trial Time " + (int)elapsed, guiStyle);

    }


    // Use this for initialization
    void Start () {

        for (int i = 0; i < populationSize; i++)
        {
            Vector3 pos = new Vector3(Random.Range(-9, 9), Random.Range(-4.5f, 4.5f), 0);
            GameObject go = Instantiate(personPrefab, pos, Quaternion.identity);
            go.GetComponent<DNA>().r = Random.Range(0.0f, 1.0f);
            go.GetComponent<DNA>().g = Random.Range(0.0f, 1.0f);
            go.GetComponent<DNA>().b = Random.Range(0.0f, 1.0f);
            go.GetComponent<DNA>().sX = Random.Range(0.1f, 0.5f);
            go.GetComponent<DNA>().sY = Random.Range(0.1f, 0.5f);
            population.Add(go);
        }
	}
	
	// Update is called once per frame
	void Update () {

        elapsed += Time.deltaTime; //set the counter to actually go up by the real time
        if (elapsed > trialTime) //If the trial time is over..
        {
            BreedNewPopulation(); //...Breed new population
            elapsed = 0; // Set timer to zero
  
        }

	}

    GameObject Breed(GameObject parent1, GameObject parent2)
    {
        Vector3 pos = new Vector3(Random.Range(-9, 9), Random.Range(-4.5f, 4.5f), 0);
        GameObject offSpring = Instantiate(personPrefab, pos, Quaternion.identity);
        DNA dna1 = parent1.GetComponent<DNA>();
        DNA dna2 = parent2.GetComponent<DNA>();

        //swap parent dna
        if (Random.Range(0, 1000) > 5)
        {
            //Run through each gene value of their parents and you randomly swap them
            offSpring.GetComponent<DNA>().r = Random.Range(0, 10) < 5 ? dna1.r : dna2.r; //If the random number is < 5 then it is the r value from dna 1, otherwise it is from dna2
            offSpring.GetComponent<DNA>().g = Random.Range(0, 10) < 5 ? dna1.g : dna2.g;
            offSpring.GetComponent<DNA>().b = Random.Range(0, 10) < 5 ? dna1.b : dna2.b;
            offSpring.GetComponent<DNA>().sX = Random.Range(0, 10) < 5 ? dna1.sX : dna2.sX;
            offSpring.GetComponent<DNA>().sY = Random.Range(0, 10) < 5 ? dna1.sY : dna2.sY;
        }
        else
        {
            //Mutations
            offSpring.GetComponent<DNA>().r = Random.Range(0.0f, 1.0f);
            offSpring.GetComponent<DNA>().g = Random.Range(0.0f, 1.0f);
            offSpring.GetComponent<DNA>().b = Random.Range(0.0f, 1.0f);
            offSpring.GetComponent<DNA>().sX = Random.Range(0.1f, 0.5f);
            offSpring.GetComponent<DNA>().sY = Random.Range(0.1f, 0.5f);
        }

        return offSpring;
    }

    private void BreedNewPopulation()
    {
        List<GameObject> newPopulation = new List<GameObject>();

        //get rid of unfit individuals - sort them based on how long it took before the player clicked on them
        List<GameObject> sortedList = population.OrderByDescending(o => o.GetComponent<DNA>().timeToDie).ToList();

        population.Clear();

        //Breed upper half of sorted list
        for (int i = (int) (sortedList.Count / 2.0f) - 1; i < sortedList.Count - 1; i++) //Looping around the whole list, starting halfway down
        {
            population.Add(Breed(sortedList[i], sortedList[i + 1]));
            population.Add(Breed(sortedList[i + 1], sortedList[i]));
        }

        //destroy all parent and previous population
        for (int i = 0; i < sortedList.Count; i++)
        {
            Destroy(sortedList[i]);
        }

        generation++;
    }
}
