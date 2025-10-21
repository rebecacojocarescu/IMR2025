using System.Data.Common;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class HandAnimationController : MonoBehaviour
{
    [Header("References")]
    [Tooltip("Animator component attached to the hand model")]
    public Animator handAnimator;

    [Header("Input Actions")]
    [Tooltip("Reference to the XR Controller (Action-based)")]
    public ActionBasedController controller;

    [Header("Animation Parameters")]
    [Tooltip("Name of the Grab float parameter in Animator (0-1)")]
    public string grabParameterName = "Grab";

    [Tooltip("Name of the Trigger float parameter in Animator (0-1)")]
    public string triggerParameterName = "Trigger";

    private float gripValue = 0f;
    private float triggerValue = 0f;

    void Start()
    {
        // Validation
        if (handAnimator == null)
        {
            Debug.LogError("Hand Animator not assigned! Please assign the Animator component.");
        }

        if (controller == null)
        {
            Debug.LogError("Controller not assigned! Please assign the ActionBasedController.");
        }
    }

    void Update()
    {
        if (controller == null || handAnimator == null) return;

        // Get input values from controller
        gripValue = controller.selectAction.action.ReadValue<float>();
        triggerValue = controller.activateAction.action.ReadValue<float>();

        // Update animator parameters
        handAnimator.SetFloat(grabParameterName, gripValue);
        handAnimator.SetFloat(triggerParameterName, triggerValue);
    }
}