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

        void Start()
        {

        }

        void Update()
        {
            _time = Time.timeScale;
            if (_find)
            {
                _foundNPC = GameObject.FindGameObjectsWithTag("NPC");
                _foundKiller = GameObject.FindGameObjectWithTag("Killer");
                _find = false;
            }

            if (_getClothes)
            {
                spriteRenderers = _foundKiller.GetComponentsInChildren<SpriteRenderer>();

            }



            for (int i = 0; i < _foundNPC.Length; i++)
            {
                if (_foundNPC[i].transform.position.x > (_hero.transform.position.x + 30)
                    || _foundNPC[i].transform.position.x < (_hero.transform.position.x - 30)
                    || _foundNPC[i].transform.position.y > (_hero.transform.position.y + 17)
                    || _foundNPC[i].transform.position.y < (_hero.transform.position.y - 17))
                {
                    Debug.Log("Yes");
                }
            }


        }
    }

}
