using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Detectiv
{
    public class SpawnerScript : MonoBehaviour
    {
        [SerializeField] Canvas can;
        [SerializeField] private GameObject _npc;
        [SerializeField] private List<Vector3> _vectors;
        private GameObject inst_obj;
        private List<int> _numberListSecondIteration;
        private List<int> _numberListFirstIteration;
        private int _count;
        private bool _flag = true;
        private bool _killerFlag = true;
        SpriteRenderer[] spriteRenderers;

        public List<SpriteRenderer> CorrectClues;

        private bool[] clues;

        float fg;


        private int[] randomIntArray() //length 8
        {
            List<int> ints = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7 };
            int[] result = new int[8];
            for (int maxRange = 7; maxRange >= 0; maxRange--)
            {
                int randomPosition = Random.Range(0, maxRange);
                result[7 - maxRange] = ints[randomPosition];
                ints.RemoveAt(randomPosition);
            }
            return result;
        }

        private bool[] cluesOnSuspect(int suspectNumber, int[] randomSeed) // bool [8] with clues on person with number suspectNumber 
                                                                           //suspect 0 is the killer      radnomSeed length = 8;
        {
            bool[] result = new bool[8];
            result[3] = suspectNumber % 2 == 1;
            result[2] = suspectNumber / 2 % 2 == 1;
            result[1] = suspectNumber / 4 % 2 == 1;
            result[0] = suspectNumber / 8 % 2 == 1;
            int clueCount = 0;
            for (int i = 0; i < 4; i++)
            {
                if (result[i])
                {
                    clueCount++;
                }
            }

            List<int> extraCLues = new List<int>() { 4, 5, 6, 7 };
            int maxRange = 3;
            for (; clueCount < 4; clueCount++)
            {
                int randomClue = Random.Range(0, maxRange);
                result[extraCLues[randomClue]] = true;
                extraCLues.RemoveAt(randomClue);
                maxRange--;
            }

            bool[] shuffledResult = new bool[8];
            for (int i = 0; i < 8; i++)
            {
                shuffledResult[randomSeed[i]] = result[i];
            }
            return shuffledResult;
        }



        int i;

        private void Awake()
        {
            _numberListSecondIteration = new List<int>();
            _numberListFirstIteration = new List<int>();
            i = 0;




            /*  int [] zero = new int[] { 0, 1, 2, 3, 4, 5, 6, 7 };
              int[] shuffled = randomIntArray();
              foreach (int i in shuffled)
              {
                  Debug.Log(i);
              }


              bool [] murderFlags = cluesOnSuspect(0, shuffled);
              foreach (bool b in murderFlags)
              {
                  Debug.Log(b);
              }
              bool[] suspectFlags = cluesOnSuspect(6, shuffled);
              foreach (bool b in suspectFlags)
              {
                  Debug.Log(b);
              }*/
            //int[] arrayStrange = randomIntArray();
            int[] arrayStrange = new int[] { 0, 1, 2, 4, 5, 6, 7, 3  };

            while (i < 16)
            {
                int randfirst = Random.Range(0, _vectors.Count);
                if (_flag)
                {
                    inst_obj = Instantiate(_npc, _vectors[randfirst], Quaternion.identity) as GameObject;
                    _vectors.RemoveAt(randfirst);

                    spriteRenderers = inst_obj.GetComponentsInChildren<SpriteRenderer>();

                    if (_killerFlag)
                    {
                        inst_obj.gameObject.tag = "Killer";
                        _killerFlag = false;

                        for (int i = 0; i < 4; i++)
                        {
                            CorrectClues.Add(spriteRenderers[arrayStrange[i]]);
                        }

                    }
                }


                bool[] arratBool = cluesOnSuspect(i, arrayStrange);

                Debug.Log(spriteRenderers.Length);


                for (int j = 1; j < spriteRenderers.Length; j++)
                {
                    if (!arratBool[j - 1])
                    {
                        Destroy(spriteRenderers[j].gameObject);
                    }
                }

                i++;
            }
        }

        private void Start()
        {
            fg = 1;
        }

        public void Update()
        {
            fg += 1 * Time.deltaTime;

            if(fg > 5)
            {
                can.gameObject.SetActive(false);
            }
            
        }

        //        while (i < 16)
        //        {
        //            int randfirst = Random.Range(0, _vectors.Count);
        //            if (_flag)
        //            {
        //                inst_obj = Instantiate(_npc, _vectors[randfirst], Quaternion.identity) as GameObject;

        //                if (_killerFlag)
        //                {
        //                    inst_obj.gameObject.tag = "Killer";
        //                    _killerFlag = false;
        //                }

        //                _vectors.RemoveAt(randfirst);

        //                spriteRenderers = inst_obj.GetComponentsInChildren<SpriteRenderer>();
        //            }

        //            _flag = false;

        //            int j = 0;
        //            _count = 0;
        //            while (j < 4)
        //            {
        //                int rand = Random.Range(1, 9);

        //                if (rand == 1 && !(_numberListFirstIteration.Contains(rand)))
        //                {
        //                    _numberListFirstIteration.Add(rand);
        //                    _count += 437;
        //                    j++;

        //                }
        //                else if (rand == 2 && !(_numberListFirstIteration.Contains(rand)))
        //                {
        //                    _numberListFirstIteration.Add(rand);
        //                    _count += 566;
        //                    j++;
        //                }
        //                else if (rand == 3 && !(_numberListFirstIteration.Contains(rand)))
        //                {
        //                    _numberListFirstIteration.Add(rand);
        //                    _count += 110;
        //                    j++;
        //                }
        //                else if (rand == 4 && !(_numberListFirstIteration.Contains(rand)))
        //                {
        //                    _numberListFirstIteration.Add(rand);
        //                    _count += 444;
        //                    j++;
        //                }
        //                else if (rand == 5 && !(_numberListFirstIteration.Contains(rand)))
        //                {
        //                    _numberListFirstIteration.Add(rand);
        //                    _count += 228;
        //                    j++;
        //                }
        //                else if (rand == 6 && !(_numberListFirstIteration.Contains(rand)))
        //                {
        //                    _numberListFirstIteration.Add(rand);
        //                    _count += 789;
        //                    j++;
        //                }
        //                else if (rand == 7 && !(_numberListFirstIteration.Contains(rand)))
        //                {
        //                    _numberListFirstIteration.Add(rand);
        //                    _count += 653;
        //                    j++;
        //                }
        //                else if (rand == 8 && !(_numberListFirstIteration.Contains(rand)))
        //                {
        //                    _numberListFirstIteration.Add(rand);
        //                    _count += 975;
        //                    j++;
        //                }
        //            }

        //            if (!_numberListSecondIteration.Contains(_count))
        //            {
        //                for (int g = 0; g < _numberListFirstIteration.Count; g++)
        //                {
        //                    int f = _numberListFirstIteration[g];
        //                    Destroy(spriteRenderers[f].gameObject);
        //                }

        //                _numberListSecondIteration.Add(_count);
        //                i++;
        //                _flag = true;
        //            }

        //            _numberListFirstIteration.Clear();
        //        }

        //    }


    }

       
    
}