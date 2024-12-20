using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class characterRespawn : MonoBehaviour
{
    static public bool isAlive = true;

    //Timer para respawnear
    [SerializeField] private float respawnTime;

    public AudioClip deathSound;
    private AudioSource audioSource;

    [SerializeField] Animator[] animatorCharacters;
    private Animator characterAnimator;

    [SerializeField] GameObject[] skinsCharacter;
    [SerializeField] int skinActive;

    private Rigidbody rb;
    private Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        characterAnimator = GetComponent<Animator>();

        //SetSkin(skinActive);
    }

    //private void SetSkin(int index)
    //{
    //    for (int i = 0; i < skinsCharacter.Length; i++)
    //    {
    //        skinsCharacter[i].SetActive(false);
    //    }

    //    //skinsCharacter[index].SetActive(true);

    //    if (index >= 0 && index < animatorCharacters.Length)
    //    {
    //        characterAnimator.runtimeAnimatorController = animatorCharacters[index].runtimeAnimatorController;
    //    }
    //}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            //Para verificar desde que lado esta colisionando
            Vector3 normal = collision.contacts[0].normal;

            if (normal.y < 0.5f)
            {
                TriggerDeath();
            }
        }

        if (collision.gameObject.CompareTag("Spikes"))
        {
            TriggerDeath();
        }

        if (collision.gameObject.CompareTag("Arrow"))
        {
            TriggerDeath();
        }
    }

    private void TriggerDeath()
    {
        if(isAlive)
        {
            audioSource.PlayOneShot(deathSound);
            isAlive = false;
            StartCoroutine(HandleRespawn());
        }
    }


    //Funcion para que una vez el personaje muera, tarde cierto tiempo es spawnear
    private IEnumerator HandleRespawn()
    {
        animator.SetBool("Die", true);

        rb.velocity = Vector3.zero;

        yield return new WaitForSeconds(respawnTime);

        functionCharacterRespawn();
    }

    //Funcion para reiniciar la escena
    private void functionCharacterRespawn()
    {
        shurikenScript.shurikensCollectedInThisTry = 0;
        isAlive = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
