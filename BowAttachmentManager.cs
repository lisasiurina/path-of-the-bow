using UnityEngine;
using UnityEngine.Events;

public class BowAttachmentManager : MonoBehaviour
{
    [SerializeField] private Transform bow;
    [SerializeField] private Transform bowAfterAttachmentParent;
    [Space]
    [SerializeField] private Animator mainAnimator;
    [SerializeField] private RuntimeAnimatorController controller;
    [Space]
    [Header("Attachment Points (Bow Parts)")]
    public Transform topAttachmentPoint;
    public Transform middleAttachmentPoint;
    public Transform bottomAttachmentPoint;
    [Space]
    public Transform righFoot;
    public Transform leftFoot;
    [Space]
    public Animator animator;

    [Header("String Endpoints")]
    public Transform topStringEnd;
    public Transform bottomStringEnd;
    public Transform mainString;

    [Header("Ghost Visuals")]
    public MeshRenderer topGhost;
    public MeshRenderer bottomGhost;

    [Header("Attachment Thresholds")]
    public float topAttachThreshold = 0.2f;
    public float bottomAttachThreshold = 0.2f;
    public float footDistance = 0.5f;

    [Header("Attachment Event")]
    public UnityEvent onBothAttached;
    [Space]
    [SerializeField] private UnityEvent topIsAttached;
    [SerializeField] private UnityEvent bottomIsAttached;

    // Internal state tracking
    private bool topAttached = false;
    private bool bottomAttached = false;


    private void Start()
    {
        isOnePartAttached = false;
    }

    void Update()
    {
        if (!isOnePartAttached)
        {
            // Continuously check the distance for each endpoint
            bool topInRange = Vector3.Distance(topStringEnd.position, topAttachmentPoint.position) <= topAttachThreshold;
            bool bottomInRange = Vector3.Distance(bottomStringEnd.position, bottomAttachmentPoint.position) <= bottomAttachThreshold;

            // Update ghost visuals based on range and if not already attached
            if (topGhost)
                topGhost.enabled = (topInRange && !topAttached);
            if (bottomGhost)
                bottomGhost.enabled = (bottomInRange && !bottomAttached);
        }
        else
        {
            bool isInRangeTop = (Vector3.Distance(topStringEnd.position, topAttachmentPoint.position) <= 0.25f);
            bool isInRangeBottom = (Vector3.Distance(topStringEnd.position, topAttachmentPoint.position) <= 0.25f);


            bool topInRange = (Vector3.Distance(topStringEnd.position, topAttachmentPoint.position) <= topAttachThreshold);
            bool bottomInRange = (Vector3.Distance(bottomStringEnd.position, bottomAttachmentPoint.position) <= bottomAttachThreshold);

            bool leftFootInRange = Vector3.Distance(topAttachmentPoint.position, leftFoot.position) <= footDistance;
            bool rightFootInRange = Vector3.Distance(topAttachmentPoint.position, righFoot.position) <= footDistance;

            topInRange = topInRange && (leftFootInRange || rightFootInRange);
            bottomInRange = bottomInRange && (rightFootInRange || topInRange);

            isInRangeBottom = isInRangeBottom && (leftFootInRange || rightFootInRange);
            isInRangeTop = isInRangeTop && (leftFootInRange || rightFootInRange);

            if(isInRangeBottom || isInRangeTop)
            {
               // animator.SetTrigger("streach");
            }

            if (topGhost)
            {
                topGhost.enabled = (topInRange && !topAttached);
            }
            if (bottomGhost)
            {
                bottomGhost.enabled = (bottomInRange && !bottomAttached);
            }
        }
    }

    private bool isOnePartAttached;

    private ConfigurableJoint middleJoint;
    private ConfigurableJoint first_Joint;

    public void AttachPart_Top(Rigidbody rigidbody)
    {
        first_Joint = mainString.gameObject.AddComponent<ConfigurableJoint>();
        first_Joint.connectedBody = rigidbody;
        first_Joint.autoConfigureConnectedAnchor = false;
        first_Joint.anchor = new Vector3(0f, 0f, -0.8f); ;    //-0.8f
        first_Joint.connectedAnchor = Vector3.zero;
        first_Joint.xMotion = ConfigurableJointMotion.Locked;
        first_Joint.yMotion = ConfigurableJointMotion.Locked;
        first_Joint.zMotion = ConfigurableJointMotion.Locked;
        
        first_Joint.angularXMotion = ConfigurableJointMotion.Free;
        first_Joint.angularYMotion = ConfigurableJointMotion.Free;
        first_Joint.angularZMotion = ConfigurableJointMotion.Free;


        middleJoint = mainString.gameObject.AddComponent<ConfigurableJoint>();
        middleJoint.connectedBody = middleAttachmentPoint.GetComponent<Rigidbody>();
        middleJoint.autoConfigureConnectedAnchor = false;
        middleJoint.anchor = Vector3.zero;
        middleJoint.connectedAnchor = Vector3.zero;
        middleJoint.xMotion = ConfigurableJointMotion.Locked;
        middleJoint.yMotion = ConfigurableJointMotion.Locked;
        middleJoint.zMotion = ConfigurableJointMotion.Locked;
        
        middleJoint.angularXMotion = ConfigurableJointMotion.Free;
        middleJoint.angularYMotion = ConfigurableJointMotion.Free;
        middleJoint.angularZMotion = ConfigurableJointMotion.Free;

        

    }
    public void AttachPart_Bottom(Rigidbody rigidbody)
    {
        first_Joint = mainString.gameObject.AddComponent<ConfigurableJoint>();
        first_Joint.connectedBody = rigidbody;
        first_Joint.autoConfigureConnectedAnchor = false;
        first_Joint.anchor = new Vector3(0f, 0f, 0.56f);
        first_Joint.connectedAnchor = Vector3.zero;
        first_Joint.xMotion = ConfigurableJointMotion.Locked;
        first_Joint.yMotion = ConfigurableJointMotion.Locked;
        first_Joint.zMotion = ConfigurableJointMotion.Locked;
        
        first_Joint.angularXMotion = ConfigurableJointMotion.Free;
        first_Joint.angularYMotion = ConfigurableJointMotion.Free;
        first_Joint.angularZMotion = ConfigurableJointMotion.Free;


        middleJoint = mainString.gameObject.AddComponent<ConfigurableJoint>();
        middleJoint.connectedBody = middleAttachmentPoint.GetComponent<Rigidbody>();
        middleJoint.autoConfigureConnectedAnchor = false;
        middleJoint.anchor = Vector3.zero;
        middleJoint.connectedAnchor = Vector3.zero;
        middleJoint.xMotion = ConfigurableJointMotion.Locked;
        middleJoint.yMotion = ConfigurableJointMotion.Locked;
        middleJoint.zMotion = ConfigurableJointMotion.Locked;
        
        middleJoint.angularXMotion = ConfigurableJointMotion.Free;
        middleJoint.angularYMotion = ConfigurableJointMotion.Free;
        middleJoint.angularZMotion = ConfigurableJointMotion.Free;

    }

    public void AttachedBoth()
    {
        bow.SetParent(bowAfterAttachmentParent, true);
        bow.SetAsFirstSibling();

        mainAnimator.runtimeAnimatorController = controller;
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            Grab();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Release();
        }
    }

    public void Grab()
    {
        //if (!topAttached && !bottomAttached) return;

        //if (topAttached && bottomAttached) return;

        //topAttachThreshold = 0.1f;
        //bottomAttachThreshold = 0.1f;

        animator.SetBool("s", true);

        //middleJoint.zMotion = ConfigurableJointMotion.Free;
        //first_Joint.zMotion = ConfigurableJointMotion.Free;
    }

    public void Release()
    {
        //if (!topAttached && !bottomAttached) return;

        //if (topAttached && bottomAttached) return;

        //topAttachThreshold = 0.1f;
        //bottomAttachThreshold = 0.1f;

        //middleJoint.zMotion = ConfigurableJointMotion.Locked;
        //first_Joint.zMotion = ConfigurableJointMotion.Locked;

        animator.SetBool("s", false);

        //animator.Play("BowArmature|BowArmatureAction");
    }

    public void AttachTop()
    {
        if (!topGhost.enabled)
            return;

        if (topAttached)
            return;

        topAttached = true;
        isOnePartAttached = true;

        topIsAttached?.Invoke();

        if (topGhost)
            topGhost.gameObject.SetActive(false);
        Debug.Log("Top attached!");

        CheckBothAttached();
    }

    public void AttachBottom()
    {
        if(!bottomGhost.enabled)
            return;

        if (bottomAttached)
            return;

        bottomAttached = true;
        isOnePartAttached = true;

        bottomIsAttached?.Invoke();

        if (bottomGhost)
            bottomGhost.gameObject.SetActive(false);
        Debug.Log("Bottom attached!");

        CheckBothAttached();
    }

    private void CheckBothAttached()
    {
        // When both ends are attached, trigger the event
        if (topAttached && bottomAttached)
        {
            Debug.Log("Both ends attached! Triggering event.");
            onBothAttached?.Invoke();
        }
    }
}
