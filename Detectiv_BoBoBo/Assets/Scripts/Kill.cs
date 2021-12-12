using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Detectiv
{
    public class Kill : MonoBehaviour
    {
        [SerializeField] GameObject bandage;
        [SerializeField] GameObject beard;
        [SerializeField] GameObject boots;
        [SerializeField] GameObject bow;
        [SerializeField] GameObject dress;
        [SerializeField] GameObject glasses;
        [SerializeField] GameObject hat;
        [SerializeField] GameObject tube;

        [SerializeField] private GameObject _hero;
        [SerializeField] private Sprite _sprite;
        [SerializeField] private SpawnerScript _spawn;


        private GameObject[] _foundNPC;
        private GameObject _foundKiller;

        private bool _find = true;
        private bool _getClothes = true;

        private float _time;

        SpriteRenderer[] spriteRenderers;
        SpriteRenderer[] spriteRenderersKiller;

        private List<SpriteRenderer> _rendererList;

        void Start()
        {
            _rendererList = _spawn.CorrectClues;
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


            if (_time > 15)
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
                            for (int y = 0; y < _rendererList.Count; y++)
                            {
                                if (spriteRenderers[j].gameObject.tag == "NPC" && _time > 15)
                                {
                                    Debug.Log("Hay");
                                    var sp = _foundNPC[i].GetComponent<SpriteRenderer>();
                                    sp.sprite = _sprite;
                                    _time = 0;
                                    _foundNPC[i].gameObject.tag = "Dezist";
                                    
                                    for(int a = 1; a < spriteRenderers.Length; a++)
                                    {
                                        Destroy(spriteRenderers[a].gameObject);
                                        if (spriteRenderers[a].gameObject.tag == "Hat")
                                        {
                                            var inst_obj = Instantiate(hat, new Vector3(spriteRenderers[a].gameObject.transform.position.x, spriteRenderers[a].gameObject.transform.position.y, spriteRenderers[a].gameObject.transform.position.z), Quaternion.identity)
                                                as GameObject;
                                        }
                                        else if(spriteRenderers[a].gameObject.tag == "Pipe")
                                        {
                                            var inst_obj = Instantiate(tube, new Vector3(spriteRenderers[a].gameObject.transform.position.x, spriteRenderers[a].gameObject.transform.position.y, spriteRenderers[a].gameObject.transform.position.z), Quaternion.identity)
                                                as GameObject;
                                        }
                                        else if(spriteRenderers[a].gameObject.tag == "Glasses")
                                        {
                                            var inst_obj = Instantiate(glasses, new Vector3(spriteRenderers[a].gameObject.transform.position.x, spriteRenderers[a].gameObject.transform.position.y, spriteRenderers[a].gameObject.transform.position.z), Quaternion.identity)
                                                as GameObject;
                                        }
                                        else if (spriteRenderers[a].gameObject.tag == "Moustache")
                                        {
                                            var inst_obj = Instantiate(beard, new Vector3(spriteRenderers[a].gameObject.transform.position.x, spriteRenderers[a].gameObject.transform.position.y, spriteRenderers[a].gameObject.transform.position.z), Quaternion.identity)
                                                as GameObject;
                                        }
                                        else if (spriteRenderers[a].gameObject.tag == "Bowtie")
                                        {
                                            var inst_obj = Instantiate(bow, new Vector3(spriteRenderers[a].gameObject.transform.position.x, spriteRenderers[a].gameObject.transform.position.y, spriteRenderers[a].gameObject.transform.position.z), Quaternion.identity)
                                                as GameObject;
                                        }
                                        else if(spriteRenderers[a].gameObject.tag == "Dress")
                                        {
                                            var inst_obj = Instantiate(dress, new Vector3(spriteRenderers[a].gameObject.transform.position.x, spriteRenderers[a].gameObject.transform.position.y, spriteRenderers[a].gameObject.transform.position.z), Quaternion.identity)
                                                as GameObject;
                                        }
                                        else if(spriteRenderers[a].gameObject.tag == "Bandage")
                                        {
                                            var inst_obj = Instantiate(bandage, new Vector3(spriteRenderers[a].gameObject.transform.position.x, spriteRenderers[a].gameObject.transform.position.y, spriteRenderers[a].gameObject.transform.position.z), Quaternion.identity)
                                                as GameObject;
                                        }
                                        else if(spriteRenderers[a].gameObject.tag == "Boots")
                                        {
                                            var inst_obj = Instantiate(boots, new Vector3(spriteRenderers[a].gameObject.transform.position.x, spriteRenderers[a].gameObject.transform.position.y, spriteRenderers[a].gameObject.transform.position.z), Quaternion.identity)
                                                as GameObject;
                                        }
                                        
                                    }

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
