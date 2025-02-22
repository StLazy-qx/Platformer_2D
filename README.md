Description
A simple 2D platformer with core mechanics and some interesting features.

Features:
- Character controls: run, jump
- Smooth character & enemy animations
- Patrolling enemy AI with a defined route
  
Combat mechanics:
- Both the player and the enemy can damage each other
- Enemy chases the player when spotted, otherwise continues patrolling
- The animation of the strike is done by a particle system
- The animation of the weapon movement (the player's sword) is also done by a particle system

Health system:
- Healing through medkits scattered around the level
- Flexible UI health display for both player and enemies via slider

Vampirism ability:
- Drains health from the nearest enemy
- Activated by a button, lasts 6 seconds, deals gradual damage
- If no enemies are in range, the ability doesn’t turn off
- 4-second cooldown before reuse
