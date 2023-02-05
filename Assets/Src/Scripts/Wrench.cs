using UnityEngine;

public class Wrench : UsableItem {

    const float primaryUseCooldown = 0.5f;

    bool menuOpen = false;
    float cooldownTime = 0.0f;


    public void updateStates(bool newPrimaryState, bool newSecondaryState) {
        if(newPrimaryState && cooldownTime <= 0) {
            // repair
            cooldownTime = 0.5f;
        } else if(newSecondaryState) {
            // open menu
            menuOpen = true;
        }
        cooldownTime -= Time.deltaTime;
    }
}