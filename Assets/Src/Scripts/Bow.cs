using UnityEngine;

public class Bow : UsableItem {

    const float minPrimaryChargeTime = 1.0f;
    const float primaryUseCooldownTime = 0.5f;

    bool primaryState = false;
    float chargeTime = 0.0f;
    float cooldownTime = 0.0f;

    public void updateStates(bool newPrimaryState, bool newSecondaryState) {
        if(primaryState) {
            if(newPrimaryState) {
                chargeTime += Time.deltaTime;
            } else if(chargeTime >= minPrimaryChargeTime && cooldownTime <= 0) {
                // fire
                cooldownTime = primaryUseCooldownTime;
            }
        }
        cooldownTime -= Time.deltaTime;
    }

}