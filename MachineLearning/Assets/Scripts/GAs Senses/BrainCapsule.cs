using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrainCapsule : MonoBehaviour {

    int DNALength = 2; //Because more than one decision
                        //When we can see the platform and what should we do when we don't see it
    public float timeAlive;
    public float timeWalking;
    public DNACapsule DNACapsule;
    public GameObject eyes;
    public bool alive = true;
    bool seeGround = true;

    public GameObject CharacterPrefab;
    GameObject Character;

    private void OnDestroy()
    {
        Destroy(Character);
    }

    private void OnCollisionEnter(Collision obj)
    {
        if (obj.gameObject.tag == "dead")
        {
            alive = false;
            timeAlive = 0;
            timeWalking = 0;
        }
    }

    public void Init()
    {
        //Initialize dna
        //0 foward
        //1 turn left
        //2 turn right
        DNACapsule = new DNACapsule(DNALength,3);
        timeAlive = 0;
        alive = true;
        Character = Instantiate(CharacterPrefab, this.transform.position, this.transform.rotation);
        Character.GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControl>().target = this.transform;
    }

    private void Update()
    {
        if (!alive) return;

        Debug.DrawRay(eyes.transform.position, eyes.transform.forward * 10, Color.green, 10);
        seeGround = false;
        RaycastHit hit;
        if (Physics.Raycast(eyes.transform.position, eyes.transform.forward * 10, out hit))
        {
            if (hit.collider.gameObject.tag == "platform")
            {
                seeGround = true;
            }
        }
        timeAlive = PopulationManagerCapsule.elapsed;

        //read DNA
        float turn = 0; //Horizontal, it determines whether we are turning around or not and which direction
        float move = 0;

        if (seeGround) //If can see the ground
        {
            if (DNACapsule.GetGene(0) == 0)
            {
                move = 1; //move forward
                timeWalking += 1; //Checks for how long it has been walking
            }
            else if (DNACapsule.GetGene(0) == 1) turn = -90; //turn left

            else if (DNACapsule.GetGene(0) == 2) turn = 90;

        }
        else
        {
            if (DNACapsule.GetGene(1) == 0)
            {
                move = 1; //move forward
                timeWalking += 1; //Checks for how long it has been walking
            }
            else if (DNACapsule.GetGene(1) == 1) turn = -90;
            else if (DNACapsule.GetGene(1) == 2) turn = 90;
        }

        this.transform.Translate(0, 0, move * 0.1f);
        this.transform.Rotate(0, turn, 0);
    }
}
