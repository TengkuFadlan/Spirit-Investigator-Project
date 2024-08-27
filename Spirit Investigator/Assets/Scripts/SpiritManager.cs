using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

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

    GameObject currentSpirit;
    Rigidbody2D currentSpiritRigidbody;

    float lastSanityDrainTick = 0;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void TransformSpirit()
    {
        if (isSpirit)
            ExitSpirit();

        Transform playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        PlayerInputCharacterMovement playerMovement = playerTransform.GetComponent<PlayerInputCharacterMovement>();
        PlayerInputInteraction playerInteraction = playerTransform.GetComponent<PlayerInputInteraction>();
        SmoothCameraLock smoothCameraLock = playerTransform.GetComponent<SmoothCameraLock>();
        Highlightable highlightable = playerTransform.GetComponent<Highlightable>();

        playerMovement.enabled = false;
        playerInteraction.enabled = false;
        smoothCameraLock.enabled = false;
        highlightable.enabled = true;

        CharacterMovement characterMovement = playerTransform.GetComponent<CharacterMovement>();
        characterMovement.movementDirectionInput = Vector2.zero;

        playerTransform.AddComponent<PlayerReturnFromSpirit>();

        currentSpirit = Instantiate(spiritPrefab, playerTransform.position, playerTransform.rotation, spiritWorld);
        currentSpiritRigidbody = currentSpirit.GetComponent<Rigidbody2D>();

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

        isSpirit = false;
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (!isSpirit && !InventoryManager.instance.isOpen && !DialogueManager.instance.isDialogueActive)
            {
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
                }
                else
                {
                    ExitSpirit();
                }
            }
        }
        else
        {
            if (sanity < maxSanity)
            {
                sanity += sanityRegen * Time.deltaTime;
            }
        }
    }
}
