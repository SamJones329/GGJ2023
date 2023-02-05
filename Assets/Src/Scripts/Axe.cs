using UnityEngine;

public class Axe : UsableItem {

    const float primaryUseCooldown = 1.0f;
    const float secondaryUseCooldown = 3.0f;
    const float minSecondaryChargeTime = 1.0f;

    bool primaryState = false;
    bool secondaryState = false;
    float secondaryChargeTime = 0.0f;
    float cooldownTime = 0.0f;

    public void updateStates(bool primaryState, bool secondaryState) {
        setPrimaryInputState(primaryState);
        setSecondaryInputState(secondaryState);
    }

    private void setPrimaryInputState(bool value) {
        cooldownTime -= Time.deltaTime; // NOTE: this only works in the context of something called once per frame from a monobehavior's update method
        if(!primaryState && value && cooldownTime <= 0) { // newly true
            if(secondaryState && secondaryChargeTime > minSecondaryChargeTime) { // heavy attack
                cooldownTime = secondaryUseCooldown;
            } else { // light attack
                cooldownTime = primaryUseCooldown;
            }
            secondaryChargeTime = 0;
        }
        primaryState = value;
    }

    private void setSecondaryInputState(bool value) {
        if(secondaryState) {
            if(value) { // still true
                secondaryChargeTime += Time.deltaTime; // NOTE: this only works in the context of something called once per frame from a monobehavior's update method
            } else if(cooldownTime <= 0) { //newly false
                if(secondaryChargeTime > minSecondaryChargeTime) {
                    // heavy attack
                    cooldownTime = secondaryUseCooldown;
                } else {
                    // light attack
                    cooldownTime = primaryUseCooldown;
                }
            }
        }
        secondaryState = value;
    }
}