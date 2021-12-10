using UnityEngine;

/// <summary>
/// The ConsumptionControllers primary purpose was to handle the technical side of consuming a potion.
/// This also included consuming the potion itself, applying the properties of said potion,
/// applying a different material when using the golden potion, etc.
/// </summary>
public class ConsumptionController : MonoBehaviour
{
    public InventoryUI playerInventory;
    public Journal journal;

    [Header("Potion Visual Effects")]
    public GameObject stealthParticles; // stealth particles
    public GameObject speedParticles; // speed particles
    public Material playerMaterial; // player materials
    public Material goldenMaterial; // golden player-applied materials
    public Animator netSwing; // net swing animation
    public GameObject playerRenderer; // renderer for player

    [Header("Audio")]
    public AudioSource drinkSuccess; // audio source for successful consumption
    public AudioSource drinkFailure; // audio source for failure of consumption

    private SkinnedMeshRenderer skinRenderer; // skin renderer?
    private Material[] mats; // materials for (something)

    private int topSpeedTier = 0; //the highest tier speed potion drunk today
    private int topStealthTier = 0; //the highest tier stealth potion drunk today

    //initialise controller
    public void Init(Journal bestiary)
    {
        //set the renderer and material arrays
        skinRenderer = playerRenderer.GetComponent<SkinnedMeshRenderer>();
        mats = skinRenderer.materials;

        //attach the journal
        journal = bestiary;
    }

    // Update is called once per frame
    void Update()
    {
        // check alpha numeric keys and consume potion slot
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            ConsumePotion(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ConsumePotion(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ConsumePotion(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ConsumePotion(3);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            ConsumePotion(4);
        }
    }

    // consumption of potion in a given slot
    private void ConsumePotion(int slot)
    {
        // check inventory capacity is <= slot size
        if (slot < playerInventory.inventory.items.Length)
        {
            // get inventory data item
            Data item = playerInventory.inventory.items[slot];
            if (item && item is PotionData)
            {
                // play drinking noise
                drinkSuccess.Play();

                // if speed and the strongest potion consumed today
                if(item.trait == Traits.Speed && item.tier > topSpeedTier)
                {
                    // construct potion from potion data
                    SpeedPotion potion = new SpeedPotion();
                    potion.SetData((PotionData)item);

                    //set potion data to variable
                    PotionData pData = (PotionData)item;

                    //set the player and net movement speeds higher
                    netSwing.speed = 1.0f + pData.effectsIntensity;

                    //set speed particles active
                    speedParticles.SetActive(true);

                    // consume potion
                    potion.Consume();

                    //set this potion tier as the highest
                    topSpeedTier = item.tier;
                }

                // if stealth and the strongest potion consumed today
                else if (item.trait == Traits.Stealth && item.tier > topStealthTier) 
                {
                    // construct potion from potion data
                    StealthPotion potion = new StealthPotion();
                    potion.SetData((PotionData)item);

                    //set potion data to variable
                    PotionData pData = (PotionData)item;

                    //set the alpha value based on tier
                    var main = stealthParticles.GetComponent<ParticleSystem>().main;
                    main.startColor = new Color(53.0f/255.0f, 104.0f/255.0f, 111.0f/255.0f, pData.effectsIntensity);

                    //set stealth particles active
                    stealthParticles.SetActive(true);

                    // consume potion
                    potion.Consume();

                    //set this potion tier as the highest
                    topStealthTier = item.tier;
                }

                // if golden and no golden already consumed today
                else if (item.trait == Traits.Golden && topSpeedTier < 4) 
                {
                    // construct potion from potion data
                    GoldenPotion potion = new GoldenPotion();
                    potion.SetData((PotionData)item);

                    //clear speed effects
                    netSwing.speed = 1;

                    //clear stealth effects
                    stealthParticles.SetActive(false);
                    stealthParticles.GetComponent<ParticleSystem>().Clear();

                    //set the player material to a golden appearance
                    mats[1] = goldenMaterial;
                    skinRenderer.materials = mats;

                    // consume potion
                    potion.Consume();

                    //set this potion as the highest of both categories
                    topSpeedTier = 4;
                    topStealthTier = 4;
                }

                //send potion to journal
                journal.AddPotionToJournal((PotionData)item);

                // clear slot
                playerInventory.RemoveFromInventory(slot);
            }
            else
            {
                // play drinking failure noise
                drinkFailure.Play();
            }
        }
    }


    //resets all visual potion effects at the end of the day
    public void EndOfDay()
    {
        //set stealth and speed particles inactive
        stealthParticles.SetActive(false);
        speedParticles.SetActive(false);

        //reset the player animation speed
        netSwing.speed = 1;

        //reset the player material
        mats[1] = playerMaterial;
        skinRenderer.materials = mats;

        //reset the top consumed potion tier
        topSpeedTier = 0;
        topStealthTier = 0;
    }
}
