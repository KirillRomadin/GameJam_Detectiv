using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Detectiv
{
    public class SpawnerScript : MonoBehaviour
    {
        [SerializeField] private GameObject _npc;
        [SerializeField] private List<Vector3> _vectors;
        private GameObject inst_obj;
        private List<int> _numberListSecondIteration;
        private List<int> _numberListFirstIteration;
        private int _count;
        private bool _flag = true;
        private bool _killerFlag = true;
        SpriteRenderer[] spriteRenderers;
        int i;

        private void Start()
        {
            _numberListSecondIteration = new List<int>();
            _numberListFirstIteration = new List<int>();
            i = 0;

            while (i < 16)
            {
                int randfirst = Random.Range(0, _vectors.Count);
                if (_flag)
                {
                    inst_obj = Instantiate(_npc, _vectors[randfirst], Quaternion.identity) as GameObject;

                    if (_killerFlag)
                    {
                        inst_obj.gameObject.tag = "Killer";
                        _killerFlag = false;
                    }

                    _vectors.RemoveAt(randfirst);

                    spriteRenderers = inst_obj.GetComponentsInChildren<SpriteRenderer>();
                }

                _flag = false;

                int j = 0;
                _count = 0;
                while (j < 4)
                {
                    int rand = Random.Range(1, 9);

                    if (rand == 1 && !(_numberListFirstIteration.Contains(rand)))
                    {
                        _numberListFirstIteration.Add(rand);
                        _count += 437;
                        j++;

                    }
                    else if (rand == 2 && !(_numberListFirstIteration.Contains(rand)))
                    {
                        _numberListFirstIteration.Add(rand);
                        _count += 566;
                        j++;
                    }
                    else if (rand == 3 && !(_numberListFirstIteration.Contains(rand)))
                    {
                        _numberListFirstIteration.Add(rand);
                        _count += 110;
                        j++;
                    }
                    else if (rand == 4 && !(_numberListFirstIteration.Contains(rand)))
                    {
                        _numberListFirstIteration.Add(rand);
                        _count += 444;
                        j++;
                    }
                    else if (rand == 5 && !(_numberListFirstIteration.Contains(rand)))
                    {
                        _numberListFirstIteration.Add(rand);
                        _count += 228;
                        j++;
                    }
                    else if (rand == 6 && !(_numberListFirstIteration.Contains(rand)))
                    {
                        _numberListFirstIteration.Add(rand);
                        _count += 789;
                        j++;
                    }
                    else if (rand == 7 && !(_numberListFirstIteration.Contains(rand)))
                    {
                        _numberListFirstIteration.Add(rand);
                        _count += 653;
                        j++;
                    }
                    else if (rand == 8 && !(_numberListFirstIteration.Contains(rand)))
                    {
                        _numberListFirstIteration.Add(rand);
                        _count += 975;
                        j++;
                    }
                }

                if (!_numberListSecondIteration.Contains(_count))
                {
                    for (int g = 0; g < _numberListFirstIteration.Count; g++)
                    {
                        int f = _numberListFirstIteration[g];
                        Destroy(spriteRenderers[f].gameObject);
                    }

                    _numberListSecondIteration.Add(_count);
                    i++;
                    _flag = true;
                }

                _numberListFirstIteration.Clear();
            }
        }

        private void Update()
        {
            
            
        }

        //public bool ChekIntFirstIteration(int Rand)
        //{
        //    flag = true;
        //    for(int i = 0; i < _numberListFirstIteration.Count; i++)
        //    {
        //        if ( _numberListFirstIteration[i] == Rand)
        //        {
        //            flag = false;
        //        }
        //    }
        //    return flag;
        //}
    }
}
