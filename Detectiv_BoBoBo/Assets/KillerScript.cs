using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Detectiv
{
    public class KillerScript : MonoBehaviour
    {
        [SerializeField] private GameObject _hero;
        [SerializeField] private Sprite _sprite;

        private GameObject[] _foundNPC;
        private GameObject _foundKiller;

        private bool _find = true;
        private bool _getClothes = true; 

        private float _time;

        SpriteRenderer[] spriteRenderers;
        SpriteRenderer[] spriteRenderersKiller;

        void Start()
        {

        }

        void Update()
        {
            _time += 1 * Time.deltaTime;

            if (_find)
            {
                _foundNPC = GameObject.FindGameObjectsWithTag("NPC");
                _foundKiller = GameObject.FindGameObjectWithTag("Killer");
                spriteRenderersKiller = _foundKiller.GetComponentsInChildren<SpriteRenderer>();
                _find = false;
            }

            if (_getClothes)
            {
                spriteRenderers = _foundKiller.GetComponentsInChildren<SpriteRenderer>();

            }


            if(_time > 15)
            {
                for (int i = 0; i < _foundNPC.Length; i++)
                {
                    if (_foundNPC[i].transform.position.x > (_hero.transform.position.x + 30)
                        || _foundNPC[i].transform.position.x < (_hero.transform.position.x - 30)
                        || _foundNPC[i].transform.position.y > (_hero.transform.position.y + 17)
                        || _foundNPC[i].transform.position.y < (_hero.transform.position.y - 17))
                    {
                        spriteRenderers = _foundNPC[i].GetComponentsInChildren<SpriteRenderer>();
                        for (int j = 0; j < spriteRenderers.Length; j++)
                        {
                            for (int y = 0; y < spriteRenderersKiller.Length; y++)
                            {
                                if (spriteRenderers[j].gameObject.tag == spriteRenderersKiller[y].tag && _time > 15)
                                {
                                    Debug.Log("Hay");
                                    var sp = _foundNPC[i].GetComponent<SpriteRenderer>();
                                    sp.sprite = _sprite;
                                    _time = 0;
                                    _foundNPC[i].gameObject.tag = "Dezist";

                                    break;
                                }
                            }
                        }
                    }
                }
            }

            Debug.Log(_time);


        }
    }

}
