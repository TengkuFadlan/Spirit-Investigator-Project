using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SpiritManager : MonoBehaviour
{
    public static SpiritManager instance;

    public float maxSanity = 100;
    public float sanity = 100;
    public float sanityDrain = 1;
    public float sanityRegen = 1;
    public bool isSpirit = false;

    public GameObject spiritPrefab;
    public Transform spiritWorld;
    public Slider sanitySlider;
    public Animator globalLightAnimator;

    GameObject currentSpirit;
    Rigidbody2D currentSpiritRigidbody;

    void Awake()
    {
        if (instance != null)
            Debug.LogError("Instance already existed");
        instance = this;
    }

    public void TransformSpirit()
    {
        if (isSpirit)
            ExitSpirit();

        spiritWorld.gameObject.SetActive(true);

        Transform playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        PlayerInputCharacterMovement playerMovement = playerTransform.GetComponent<PlayerInputCharacterMovement>();
        PlayerInputInteraction playerInteraction = playerTransform.GetComponent<PlayerInputInteraction>();
        SmoothCameraLock smoothCameraLock = playerTransform.GetComponent<SmoothCameraLock>();
        Highlightable highlightable = playerTransform.GetComponent<Highlightable>();
        AudioListener audioListener = playerTransform.GetComponent<AudioListener>();

        playerMovement.enabled = false;
        playerInteraction.enabled = false;
        smoothCameraLock.enabled = false;
        highlightable.enabled = true;
        Destroy(audioListener);

        CharacterMovement characterMovement = playerTransform.GetComponent<CharacterMovement>();
        characterMovement.movementDirectionInput = Vector2.zero;

        playerTransform.AddComponent<PlayerReturnFromSpirit>();

        currentSpirit = Instantiate(spiritPrefab, playerTransform.position, playerTransform.rotation, spiritWorld);
        currentSpiritRigidbody = currentSpirit.GetComponent<Rigidbody2D>();

        globalLightAnimator.SetBool("SpiritDimension", true);

        isSpirit = true;
    }

    public void ExitSpirit()
    {
        if (!isSpirit)
            return;

        Destroy(currentSpirit);
        currentSpirit = null;
        currentSpiritRigidbody = null;

        Transform playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        PlayerInputCharacterMovement playerMovement = playerTransform.GetComponent<PlayerInputCharacterMovement>();
        PlayerInputInteraction playerInteraction = playerTransform.GetComponent<PlayerInputInteraction>();
        SmoothCameraLock smoothCameraLock = playerTransform.GetComponent<SmoothCameraLock>();
        Highlightable highlightable = playerTransform.GetComponent<Highlightable>();
        PlayerReturnFromSpirit playerReturnFromSpirit = playerTransform.GetComponent<PlayerReturnFromSpirit>();
        
        playerMovement.enabled = true;
        highlightable.enabled = false;
        playerInteraction.enabled = true;
        smoothCameraLock.enabled = true;
        
        Destroy(playerReturnFromSpirit);
        playerTransform.AddComponent<AudioListener>();

        spiritWorld.gameObject.SetActive(false);

        globalLightAnimator.SetBool("SpiritDimension", false);

        isSpirit = false;
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (!isSpirit && !InventoryManager.instance.isOpen && !DialogueManager.instance.isDialogueActive)
            {
                if (Time.timeScale != 0)
                    TransformSpirit();
            }
        }


        if (isSpirit)
        {
            if (currentSpiritRigidbody.velocity.magnitude > 0.01f)
            {
                if (sanity > 0)
                {
                    sanity -= sanityDrain * Time.deltaTime;
                    if (sanity < 0)
                        sanity = 0;
                }
                else
                {
                    ExitSpirit();

                    Debug.Log("Investigator went insane.");

                    // TODO: Game Over Screen
                }
            }
        }
        else
        {
            if (sanity < maxSanity)
            {
                sanity += sanityRegen * Time.deltaTime;
                if (sanity > maxSanity)
                    sanity = maxSanity;
            }
        }

        sanitySlider.value = sanity/maxSanity;
    }
}
