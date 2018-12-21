﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNACapsule {

    List<int> genes = new List<int>();
    int dnaLength = 0;
    int maxValues = 0;

    //Constructor
    public DNACapsule(int l, int v)
    {
        dnaLength = l;
        maxValues = v;
        SetRandom();
    }

    public void SetRandom()
    {
        genes.Clear();
        for (int i = 0; i < dnaLength; i++)
        {
            genes.Add(UnityEngine.Random.Range(0, maxValues));
        }
    }

    public void SetInt(int pos, int value)
    {
        genes[pos] = value;
    }

    public void Combine(DNACapsule parent1, DNACapsule parent2)
    {
        for (int i = 0; i < dnaLength; i++)
        {
            if (i < dnaLength / 2.0)
            {
                int c = parent1.genes[i];
                genes[i] = c;
            }
            else
            {
                int c = parent2.genes[i];
                genes[i] = c;
            }
        }
    }

    public void Mutate()
    {
        genes[UnityEngine.Random.Range(0, dnaLength)] = UnityEngine.Random.Range(0, maxValues);
    }

    public int GetGene(int pos)
    {
        return genes[pos];
    }
}
