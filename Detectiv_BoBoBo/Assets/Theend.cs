using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Detectiv
{
    public class Theend : MonoBehaviour
    {
        private GameObject can;
        private AudioSource sound;
        GameObject panel1;
        GameObject panel2;

        void Start()
        {
            sound = GetComponent<AudioSource>();


            can = GameObject.Find("Canvas");
            panel2 = GameObject.Find("Panel");
            panel2 = GameObject.Find("Panel (1)");
        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnTriggerEnter2D(Collider2D col)
        {
            if(gameObject.tag == "NPC")
            {
                sound.Play();
            }
            else
            {
                can.gameObject.SetActive(true);
                panel2.gameObject.SetActive(false);
                panel1.gameObject.SetActive(true);
            }
        }
    }
}

