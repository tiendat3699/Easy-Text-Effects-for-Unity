# Documentation

## Installation

Easiest way is to [install Easy Text Effects as a package](https://docs.unity3d.com/Manual/upm-ui-giturl.html).

1. Open **Window/Package Manager** in Unity.
2. Click on the **+** button in the top left corner.
3. Select **Add package from git URL** and paste the following URL:

```
https://github.com/LeiQiaoZhi/Easy-Text-Effect.git
```

> If you are new to this package, I highly recommend you to import the samples in the "**Samples**" tab in the [details panel](https://docs.unity3d.com/6000.0/Documentation/Manual/upm-ui-details.html) of this package in the package manager. There is a demo scene and some ready-to-use effects.

## Getting Started

Animate your text with 3 simple steps:

1. Add a `TextEffect` component, drag your `TMP Text` component to the `Text` field.
2. Create a new effect in the project view, by right-clicking and selecting
   `Create/Easy Text Effect/[Text Effect Type]`.
3. Add an element to an effect list, then drag the effect to the `Effect` field.

>You should see your effects right away in the editor! (_If not, press the **Refresh** button, or **Play** the scene._)

See the [Effects](#effects) page for more information on the available effects.

There are 2 effect lists:

- `Tag Effects`: Effects that are applied to the text based on rich text tags.
- `Global Effects`: Effects that are applied to every character in the text.

<img src="component.png" width="50%">

## Effects

There are 4 types of effects: `Color`, `Move`, `Rotate`, and `Scale`. What properties they change are self-explanatory.

### Common Properties

There are some common properties that are shared between all effects:

`Effect Name`: The name of the effect, used to identify the effect for rich text tags and manual control.

Animations' timing are different for each character:
- `Duration Per Char`: The duration of one cycle of the effect for each character.
- `Time Between Chars`: The time between the start of each character's effect. You can set it to 0 to make the effect text-wise.

<img src="time.png" width="50%" alt="">

`No Delay For Later Chars`: If enabled, the effect will start immediately for all characters, instead of waiting for the previous character to finish.

<img src="nodelay.png" width="50%" alt="">

`Reverse Char Order`: If enabled, the effect will start from the last character instead of the first. This is useful for exit animations.

<img src="reverse.png" width="50%" alt="">

`Animation Type`: determines how the effect acts when time exceeds the duration of the effect.
- `One Time`: The effect will **stop** when the time exceeds the duration. All other types will loop (in different ways).  
- `Ping Pong`: The effect will **reverse** when the time exceeds the duration. This makes the effect go back and forth smoothly.
- `Loop`: The effect will **restart** when the time exceeds the duration. If start and end values are not the same, the effect will have an abrupt jump.

<img src="notonetime.png" width="50%" alt="">

<img src="clamp.png" width="50%" alt="">


### Color

### Move

### Rotate

### Scale

### Creating Effects

Effects are ScriptableObjects that can be created in the project view. Right-click and select `Create/Easy Text Effect/[Text Effect Type]`. Since effects are assets, they can be shared between multiple `TextEffect` components, and changes to the effect will be reflected in all components.

### Applying Effects

There are 2 effect lists:

- `Tag Effects`: Effects that are applied to the text based on rich text tags.
- `Global Effects`: Effects that are applied to every character in the text.

Global effects are very easy to apply, just add an element to the list and drag the effect to the `Effect` field.

### Controlling Effects

Every element of an effect list has a `Trigger When` field, which determines when the effect is triggered. 
- `On Start`: The effect will start when the text is enabled.
- `Manual`: The effect will start only when a script tells it to.
   - `StartAllManualEffects()`: start all manual effects in the global list.
   - `StartManualEffects(string effectName)`: start the manual effect with the given name in the global list.
   - `StartManualTagEffects()`: start all manual effects in the tag list.
   - `StartManualTagEffects(string effectName)`: start the manual effect with the given name in the tag list.

There are some debug buttons to help you test manual effects in the editor:

<img src="debug.png" width="50%" alt="">
