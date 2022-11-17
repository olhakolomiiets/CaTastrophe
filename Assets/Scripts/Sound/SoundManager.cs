using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
public static SoundManager snd;
private AudioSource audioSrc;
private AudioClip[] vaseSounds;
private AudioClip[] VaseWithSounds;
private AudioClip[] potSounds;
private AudioClip[] SmallPotSounds;
private AudioClip[] BottlesSounds;
private AudioClip[] buttonsSound;
private AudioClip[] DogSounds;
private AudioClip[] BallSounds;
private AudioClip[] MetalStuffSounds;
private AudioClip[] FartSounds;
private AudioClip[] PeeSounds;
private AudioClip[] PaintSounds;
private AudioClip[] TVandOtherSounds;
private AudioClip[] EatSounds;
private AudioClip[] PlasticFallSounds;
private AudioClip[] BigCabinetSounds;
private AudioClip[] buySounds;
private AudioClip[] CatSounds;
private AudioClip[] CatLongSounds;
private AudioClip[] CatDamageSounds;
private AudioClip[] ScratchSounds;
private AudioClip[] ScratchLoudSounds;
private AudioClip[] GlassDidntDestroySounds;
private AudioClip[] ForWeightDestroy;
private AudioClip[] DizzySounds;
private AudioClip[] DogWhiningSounds;
private AudioClip[] RepairSoft;
private AudioClip[] RepairHard;
private AudioClip[] PlasticImpactBigSounds;
public AudioClip damage;
private int randomSounds;

  
    void Start()
    {
        snd = this;
        audioSrc = GetComponent <AudioSource>();
        vaseSounds = Resources.LoadAll<AudioClip>("vaseSounds");
        VaseWithSounds = Resources.LoadAll<AudioClip>("VaseWithSounds");
        potSounds = Resources.LoadAll<AudioClip>("potSounds");
        SmallPotSounds = Resources.LoadAll<AudioClip>("SmallPotSounds");
        BottlesSounds = Resources.LoadAll<AudioClip>("BottlesSounds");
        buttonsSound = Resources.LoadAll<AudioClip>("ButtonsSound");
        DogSounds = Resources.LoadAll<AudioClip>("DogSounds");
        BallSounds = Resources.LoadAll<AudioClip>("BallSounds");
        MetalStuffSounds = Resources.LoadAll<AudioClip>("MetalStuffSounds");
        FartSounds = Resources.LoadAll<AudioClip>("FartSounds");
        PeeSounds = Resources.LoadAll<AudioClip>("PeeSounds");
        PaintSounds = Resources.LoadAll<AudioClip>("PaintSounds");
        TVandOtherSounds = Resources.LoadAll<AudioClip>("TVandOtherSounds");
        EatSounds = Resources.LoadAll<AudioClip>("EatSounds");
        PlasticFallSounds = Resources.LoadAll<AudioClip>("PlasticFallSounds");
        BigCabinetSounds = Resources.LoadAll<AudioClip>("BigCabinetSounds");
        buySounds = Resources.LoadAll<AudioClip>("buySounds");
        CatSounds = Resources.LoadAll<AudioClip>("CatSounds");
        CatLongSounds = Resources.LoadAll<AudioClip>("CatLongSounds");
        CatDamageSounds = Resources.LoadAll<AudioClip>("CatDamageSounds");
        ScratchSounds = Resources.LoadAll<AudioClip>("ScratchSounds");
        ScratchLoudSounds = Resources.LoadAll<AudioClip>("ScratchLoudSounds");
        GlassDidntDestroySounds = Resources.LoadAll<AudioClip>("GlassDidntDestroySounds");
        ForWeightDestroy = Resources.LoadAll<AudioClip>("ForWeightDestroy");
        DizzySounds = Resources.LoadAll<AudioClip>("DizzySounds");
        DogWhiningSounds = Resources.LoadAll<AudioClip>("DogWhiningSounds");
        RepairSoft = Resources.LoadAll<AudioClip>("RepairSoft");
        RepairHard = Resources.LoadAll<AudioClip>("RepairHard");
        PlasticImpactBigSounds = Resources.LoadAll<AudioClip>("PlasticImpactsBig");
        
    }

    public void PlayVaseSounds()
    {
        randomSounds = Random.Range(0,2);
        audioSrc.PlayOneShot(vaseSounds[randomSounds]);
    }
    public void PlayVaseWithSounds()
    {
        randomSounds = Random.Range(0,0);
        audioSrc.PlayOneShot(VaseWithSounds[randomSounds]);
    }
     public void PlayPotSounds()
    {
        randomSounds = Random.Range(0,2);
        audioSrc.PlayOneShot(potSounds[randomSounds]);
    }
    public void PlaySmallPotSounds()
    {
        randomSounds = Random.Range(0,2);
        audioSrc.PlayOneShot(SmallPotSounds[randomSounds]);
    }
    public void PlayBottlesSounds()
    {
        randomSounds = Random.Range(0,3);
        audioSrc.PlayOneShot(BottlesSounds[randomSounds]);
    }
     public void PlayPaintSounds()
    {
        randomSounds = Random.Range(0,1);
        audioSrc.PlayOneShot(PaintSounds[randomSounds]);
    }
    public void PlayTVandOtherSounds()
    {
        randomSounds = Random.Range(0,1);
        audioSrc.PlayOneShot(TVandOtherSounds[randomSounds]);
    }
      public void PlayBigCabinetSounds()
    {
        randomSounds = Random.Range(0,1);
        audioSrc.PlayOneShot(BigCabinetSounds[randomSounds]);
    }
     public void PlayButtonsSound()
    {
        randomSounds = Random.Range(0,1);
        audioSrc.PlayOneShot(buttonsSound[randomSounds]);
    }
     public void PlayDogSound()
    {
        randomSounds = Random.Range(0,4);
        audioSrc.PlayOneShot(DogSounds[randomSounds]);
    }
    public void PlayBallSounds()
    {
        randomSounds = Random.Range(0,1);
        audioSrc.PlayOneShot(BallSounds[randomSounds]);
    }
    public void PlayMetalStuffSounds()
    {
        randomSounds = Random.Range(0,2);
        audioSrc.PlayOneShot(MetalStuffSounds[randomSounds]);
    }
    public void PlayPlasticFallSounds()
    {
        randomSounds = Random.Range(0,3);
        audioSrc.PlayOneShot(PlasticFallSounds[randomSounds]);
    }
     public void PlayFartSounds()
    {
        randomSounds = Random.Range(0,6);
        audioSrc.PlayOneShot(FartSounds[randomSounds]);
    }
      public void PlayPeeSounds()
    {
        randomSounds = Random.Range(0,0);
        audioSrc.PlayOneShot(PeeSounds[randomSounds]);
    }
     public void PlayEatSounds()
    {
        randomSounds = Random.Range(0,2);
        audioSrc.PlayOneShot(EatSounds[randomSounds]);
    }
    public void PlaybuySounds()
    {
        randomSounds = Random.Range(0,0);
        audioSrc.PlayOneShot(buySounds[randomSounds]);
    }
    public void PlayCatSounds()
    {
        randomSounds = Random.Range(0,2);
        audioSrc.PlayOneShot(CatSounds[randomSounds]);
    }
    public void PlayLongCatSounds()
    {
        randomSounds = Random.Range(0,2);
        audioSrc.PlayOneShot(CatLongSounds[randomSounds]);
    }
    public void PlayDamage()
    {
        
       randomSounds = Random.Range(0,2);
        audioSrc.PlayOneShot(CatDamageSounds[randomSounds]);
    }
    public void PlayScratchSounds()
    {
        randomSounds = Random.Range(0,0);
        audioSrc.PlayOneShot(ScratchSounds[randomSounds]);
    }
    public void PlayScratchLoudSounds()
    {
        randomSounds = Random.Range(0,1);
        audioSrc.PlayOneShot(ScratchLoudSounds[randomSounds]);
    }
    public void PlayGlassDidntDestroySounds()
    {
        randomSounds = Random.Range(0,3);
        audioSrc.PlayOneShot(GlassDidntDestroySounds[randomSounds]);
    }
    public void PlayForWeightDestroy()
    {
        randomSounds = Random.Range(0,0);
        audioSrc.PlayOneShot(ForWeightDestroy[randomSounds]);
    }
     public void PlayDizzySounds()
    {
        randomSounds = Random.Range(0,0);
        audioSrc.PlayOneShot(DizzySounds[randomSounds]);
    }
     public void PlayDogWhiningSounds()
    {
        randomSounds = Random.Range(0,0);
        audioSrc.PlayOneShot(DogWhiningSounds[randomSounds]);
    }
         public void PlayRepairHard()
    {
        randomSounds = Random.Range(0,2);
        audioSrc.PlayOneShot(RepairHard[randomSounds]);
    }
     public void PlayRepairSoft()
    {
        randomSounds = Random.Range(0,3);
        audioSrc.PlayOneShot(RepairSoft[randomSounds]);
    }
       public void PlayPlasticImpactBigSounds()
    {
        randomSounds = Random.Range(0,3);
        audioSrc.PlayOneShot(PlasticImpactBigSounds[randomSounds]);
    }
}
