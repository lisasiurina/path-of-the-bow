# Path of the Bow

A solo VR prototype built in Unity for the HTC Vive, where the player steps into a traditional Japanese dojo to manually craft, string, and shoot a bow — using physical gestures instead of standard UI menus.

**Portfolio case study:** [link to my portfolio page]

---

## Concept

The goal was to recreate the physical process of building and stringing a bow without floating 2D menus or text prompts breaking the calm atmosphere of the dojo. Instructions are taught diegetically — through wall scrolls in the environment — rather than through UI overlays.

## Key Mechanic: Foot-Anchored Stringing

In real life, stringing a bow is the hardest part of the process — you can't bend the wood with your hands alone, you have to press the frame against your foot to leverage your body weight. Skipping or simplifying this step would make the bow feel weightless and fake.

To keep it authentic, the string can only be attached once the player physically lowers the bow toward their own foot in VR. The system continuously checks the distance between the bow's attachment points and the player's tracked foot position, and only unlocks the final stringing state once that physical posture is met.

## Scripts in this repo

- **`BowAttachmentManager.cs`** — Core interaction logic. Tracks distance between the bowstring endpoints and the bow's attachment points, and — critically — the distance to the player's feet (`leftFoot` / `rightFoot`). The string only locks into place when both the string-to-frame distance *and* the foot proximity conditions are satisfied. Also handles the physical joint setup (`ConfigurableJoint`) that lets the string behave like a real attached object once connected.

- **`BowAssemblyTracker.cs`** — Tracks the earlier assembly stage (attaching the upper and lower limbs to the middle grip). Uses distance checks and `UnityEvent`s to fire visual/audio feedback when a part comes into range and when it's attached, and flags when the full frame is assembled.

> Note: these are working prototype scripts, not cleaned-up production code. Some debug/test paths (e.g. keyboard shortcuts used for testing in `BowAttachmentManager.cs`) remain from iteration and would be removed in a polished build.

## Tech

- Unity, C#
- Platform: HTC Vive (PCVR)
- Physics-based attachment via `ConfigurableJoint`
- Full-body posture tracking for the foot-anchor mechanic

## Links

- Portfolio case: [add link]
- Contact: lisa.siurina@gmail.com
