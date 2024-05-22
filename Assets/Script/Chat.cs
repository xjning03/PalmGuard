using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chat : MonoBehaviour
{

    public Text text1;
    // Start is called before the first frame update
    public void reply()
    {
        text1.text = "Open Wounds:\r\nAppearance: Open wounds are characterized by a break in the skin's integrity, exposing underlying tissues. They may appear as cuts, lacerations, punctures, abrasions, or avulsions." +
            "\r\nBleeding: Open wounds often bleed, with the severity of bleeding varying based on the depth and location of the wound.\r\nVisible Tissue: " +
            "Depending on the depth of the wound, underlying tissues such as muscles, fat, or bones may be visible.\r\nRisk of Infection: Open wounds are at higher risk of infection due to exposure to external contaminants.\r\n" +
            "\nClosed Wounds:\r\nAppearance: Closed wounds do not have a break in the skin's surface. They may appear as bruises, hematomas, or contusions.\r\nNo External Bleeding: Closed wounds typically do not result in external bleeding, " +
            "although there may be internal bleeding.\r\nSwelling: Closed wounds may be accompanied by swelling, " +
            "especially if there's internal bleeding or tissue damage.\r\nNo Visible Tissue: Since the skin is intact, there's no visible exposure of underlying tissues.";

    }
}
