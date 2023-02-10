How to set up:
1. Create an empty "UI Canvas" object and attach the "MenuController.cs" script to it.

2. Change the "Screen Match Mode" in the "Canvas Scaler" component to "shrink".
3. Change the "UI Scale Mode" in the "Canvas Scaler" component to "shrink".

4. Attach a "Player Input" component to the canvas, and set the behaviour to Broadcast Messages.

5. Create new empty gameobjects and parent them to the UI canvas object.
	- those objects will be your different pages

6. Attach a "Page.cs" script to every page gameobject and adjust its settings to your liking.

Now you are ready to use the UI

Tips:
1. If you want to add new transition animations:
	- Open the UIModes.cs script and give the new transition a name in the ETransition enum
	- Next, Open the Transitions.cs script and create a new coroutine for the entry and exit transition
	- Next, open the Page.cs script and add the new Transition to the "Push" and "Pop" function
	- Finally, set the new transition in the Page.cs component in your inspector
Important: Try to keep the codes structure when adding new transitions to maintain code functionality, but feel free to change anything if you need to.

2. To better understand certain features, read the scripts comments or tooltips by hovering over certain inspector variables

3. Do not set the timescale to 0, that would stop the transition animations from working, thus you not being able to open or exit pages.