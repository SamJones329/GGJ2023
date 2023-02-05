using UnityEngine;

public class Wrench : UsableItem {

    const float primaryUseCooldown = 0.5f;

    bool menuOpen = false;
    float cooldownTime = 0.0f;


    public void updateStates(bool primaryState, bool secondaryState) {
        if(primaryState && cooldownTime <= 0) {
            // repair
            cooldownTime = 0.5f;
        } else if(secondaryState) {
            // open menu
            menuOpen = true;
        }
        cooldownTime -= Time.deltaTime;
    }
}