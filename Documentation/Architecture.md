# Game Engine Architecture:

To be detailed and improved as we go. Below is an initial design concept.

## Class Reference:

### GameEngine

#### Overview:

Entry point for the game. Acts as an orchestrator for everything else.

Is responsible for initializing all managers and engines before entering and handling the main game loop and game cleardown.

> note - could consider moving to a hosted service so that setup is handled in a more framework-consistent way

### EntityManager

#### Overview:

Maintains the complete collection of entities which exist in the game.

Provides methods to add, get and remove entities by ID.

Entitites are identified by ID.

### EventManager

#### Overview:

Not well defined yet - should allow clients to subscribe for events, and other clients to publish events.

Exactly what this will look like or why this would be used is as yet unclear, but I remember thinking this would be a good idea.

### Map

#### Overview:

Tracks entities by location (maintains a list of entities present at each location).

Provides methods to:
- identify presence of entities at a tile
- publish events to a tile

### Input[Engine?]

#### Overview:

A lightweight wrapper for handling user inputs

Publishes any events relevent to entities to that entity (user will probably subscribe to [wasd] and so on)

### Display

#### Overview:

Wraps the display to which graphics will be painted, providing a consistent interface for the GraphicsEngine to work with - could be:
- Console
- Window
    - presumably different implementations might be needed for windows, linux and mac
- Full Screen (however this is done)
    - presumably different implementations might be needed for windows, linux and mac

### GraphicsEngine

#### Overview:

Keeps the graphics pane up to date with the current state of the current viewport

