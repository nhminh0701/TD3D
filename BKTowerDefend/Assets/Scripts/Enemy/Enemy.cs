using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Managing all associated parameters
/// </summary>
public class Enemy : MonoBehaviour
{
    public float startSpeed = 12f;

    public float speed;

    public int worth = 5;

    public int startDamage = 3;
    [HideInInspector]
    public int damage;

    Dictionary<DebuffType, RunningEffectPars> runEffectDict = default;

    [SerializeField] float startHealth = 100;
    public Health health;

    private void Start()
    {
        health.SetmaxHealth(startHealth);
        health.enabled = true;
        speed = startSpeed;
        ResetDamage();

        runEffectDict = new Dictionary<DebuffType, RunningEffectPars>()
        {
            [DebuffType.DamageRed] = new RunningEffectPars(),
            [DebuffType.SLow] = new RunningEffectPars(),
            [DebuffType.DPS] = new RunningEffectPars()
        };
    }


    public void TriggerEffect(DebuffHolderData debuffHolderData)
    {
        Debuff[] listDebuff = debuffHolderData.listDebuff;
        float duration = debuffHolderData.duration;
        for (var i = 0; i < listDebuff.Length; i++)
        {
            switch (listDebuff[i].debuffType)
            {
                case (DebuffType.SLow):
                    RunningEffectPars slowEffectPars = runEffectDict[DebuffType.SLow];
                    slowEffectPars.effectPar = listDebuff[i].debuffParams.msSlowPer;
                    slowEffectPars.timer = 0;
                    slowEffectPars.duration = duration;
                    if (!slowEffectPars.isActive) slowEffectPars.isActive = true;
                    break;
                case (DebuffType.DamageRed):
                    RunningEffectPars damageRedEffectPars = runEffectDict[DebuffType.DamageRed];
                    damageRedEffectPars.effectPar = listDebuff[i].debuffParams.damageRedPer;
                    damageRedEffectPars.timer = 0;
                    damageRedEffectPars.duration = duration;
                    if (!damageRedEffectPars.isActive) damageRedEffectPars.isActive = true;
                    break;
                case (DebuffType.DPS):
                    RunningEffectPars dPSEffectPars = runEffectDict[DebuffType.DPS];
                    dPSEffectPars.effectPar = listDebuff[i].debuffParams.damagePerSeconds*Time.deltaTime;
                    dPSEffectPars.timer = 0;
                    dPSEffectPars.duration = duration;
                    if (!dPSEffectPars.isActive) dPSEffectPars.isActive = true;
                    break;
            } 
        }
    }

    private void Update()
    {
        if (runEffectDict[DebuffType.SLow].isActive)
        {
            if (runEffectDict[DebuffType.SLow].timer > runEffectDict[DebuffType.SLow].duration)
            {
                runEffectDict[DebuffType.SLow].ResetState();
                ResetMS();
            }
            else
            {
                Slow(runEffectDict[DebuffType.SLow].effectPar);
                runEffectDict[DebuffType.SLow].timer += Time.deltaTime;
            }
        }

        if (runEffectDict[DebuffType.DamageRed].isActive)
        {
            if (runEffectDict[DebuffType.DamageRed].timer > runEffectDict[DebuffType.DamageRed].duration)
            {
                runEffectDict[DebuffType.DamageRed].ResetState();
                ResetDamage();
            }
            else
            {
                ChangeDamage(runEffectDict[DebuffType.DamageRed].effectPar);
                runEffectDict[DebuffType.DamageRed].timer += Time.deltaTime;
            }
        }

        if (runEffectDict[DebuffType.DPS].isActive)
        {
            if (runEffectDict[DebuffType.DPS].timer > runEffectDict[DebuffType.DPS].duration)
            {
                runEffectDict[DebuffType.DPS].ResetState();
            }
            else
            {
                health.TakeDamage(runEffectDict[DebuffType.DPS].effectPar);
                runEffectDict[DebuffType.DPS].timer += Time.deltaTime;
            }
        }
    }

    public void ResetEnemyPar()
    {
        ResetDamage();
        ResetMS();
    }

    public void ResetDamage() { damage = startDamage; }

    public void ChangeDamage(float changePer) { damage = Mathf.RoundToInt(damage * (1 - changePer)); }
    public void RecoverDamage(float changedPer)
    {
        if (changedPer == 1) ResetDamage();
        else damage = Mathf.RoundToInt(damage / (1 - changedPer));
    }

    private void OnDisable()
    {
        WaveSpawner.numberAliveEnemies--;
    }

    public void Slow(float percentage) {
        speed = startSpeed * (1 - percentage);
    }

    public void ResetMS() { speed = startSpeed; }

    public void RecoverMS(float changedMsPer) {

        if (changedMsPer == 1) ResetMS();
        else speed = speed / (1 - changedMsPer);
    }
}

public class RunningEffectPars
{
    public bool isActive;
    public float timer;
    public float duration;
    public float effectPar;

    public void ResetState()
    {
        isActive = false;
        timer = 0;
    }

    public RunningEffectPars()
    {
        isActive = false;
        timer = 0;
        duration = 0;
        effectPar = 0;
    }
}
