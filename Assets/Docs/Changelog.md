# Version 0.10.0:
## Features:
- Signs work with lightning
- Background ambience
- Flickering Lights
- New Keycard looks
- Passes now become inactive and can be reactivated again with a new item
- Weight System disabled
- Poison Spreads
	- AC stops spread
# Version 0.9.0:
## Features:
- Time left announcements
- Power Holder
- Garden
- %%Alarm Trap%% Blackout has visuals and works when entered, not when opened
- Keycard Uploader
- Rooms can have multiple traps
- Weight System
	- Heavy items increase stamina usage
	- Player uses less stamina when empty handed that before
- Visual Effect for Adrenaline
- New posters
- EMPs can open keycard readers, but more often break them
## Tweaks:
- ??? is now a strong Door
- Changed damage values
- Rebalanced Ascensions
	- Changed power loss over time, into no power in the starting sector
- Added an inventory slot
- Decreased player collider size
- Adjusted loot a bit
- Improvements to announcements
## Bugfixes:
- FIXED UI NOT CLICKABLE, FINALLY AFTER AN HOUR
# Version 0.8.0:
## Features:
- Portable Portal
- Corridor Props and a Rug;), light colors
## Tweaks:
- The Conduit now rejects two same cards
- Gave more time base
## Bugfixes:
- Lockers size
# Version 0.7.0:
## Features:
- Achievement icons in the main menu
- 05 Room
- Lockers
- Rooms Catalogue
- Keycard Detector
- Guests Management
- %%Tests%%
- Task Room
- Unique Items Picked Up Stat
## Changes:
- The Conduit and Status Terminal Room have changed loot
- EMP now only lower to minimal Power
- Metal Detectors have a 15s cooldown
- Removed Laser Door trap (deemed uninteresting)
- Linked loot is removed when there is only 1 or 0 loot
- Keycards look a bit better now
- Glass boxes have 2x HP
- Improved The Conduits feedback
# Version 0.6.0:
## Features:
- New Posters
- Volume Setting
- Stat for objects destroyed
- Broken keycard has a new mechanic
- Gave player bigger stamina bank
- Status Terminal Room
- Controls reminder
- More Announcements
## Changes:
- Revolver more common
- Senior Staff box is now transparent
- Camera steals more time, disc gives more time
- Use keycard faster
# Version 0.5.0:
## Features:
- Big Poison Room with equipment
	- Note about big infirmary
- Keycard Combiner Room
## Changes:
- Unified equipment and linked items work on it
- Flashlight is now equipment
- Made bags secure loot, rebalance loot a bit
- Added smart map to the infirmary
- More items in secure storage
- Changed smart map model
- Card heavy armory is no longer secret
# Version 0.4.1:
## Features:
- Hidden Room
- Passes have an icon now
- Scan Facility Disc
## Changes:
- Breakable wall has 25 HP
- Base Door now has 50H
# Version 0.4.0:
## Features:
- [[Ascensions]]
## Changes:
- Seed no longer depends on the game number
# Version 0.3.1:
## Changes:
- Nerfed EMP Grenade
- Administrations is likely to give Secure Wing
## Bugfixes:
- Maybe purify protocol at init fixed (seems to work)
# Version 0.3.0:
## Features:
- Electric Mask, protects from keycard readers electrocution
- Some rooms have a high probability for a specific trap now
- Keycard steal trap, steal the keycard used to open the room
## Changes:
- Adrenaline heals slightly
- Potion nerf (weaker heavy heal and a bit shorter)
- Renamed permanent and normal damage to heavy and light damage
- Icons for resistances show only when applicable
- Admin loot adjusted
- Guard passes can't appear in basic loot
- Sounds don't play while loading
## Internal:
- Removed A* pathfinding project
## Bugfixes
- Heavy armory secret spawns
- Menu always showing administration, hopefully fixed for the first and last time
- Fixed grenade double door damage
# Version 0.2.0:
## Features:
- Room background in the main menu (is the last entered room during gameplay)
- Unique rooms entered stat
## Internal:
- Rewriting room spawning and game init in general
	- Traps and loot spawners in rooms are now activated by the game init
- Cleaned up Level Nodes
- Main power system improved
	- Removed power loss, if reimplemented make it it's own component
- Rooms Prefab List for saving reference
## Bugfixes:
- Player damage and heals are discarded if zero
- Keycard electrocution sound not actually playing
- Usable items removed player for themselfes, and tried accesing it later
- Hurt effect when healing removed
# Version 0.1.1:
## Gameplay Changes:
- Made secret harder to spot
## Other:
- Code cleanup
# Version 0.1.0:
## Features:
- Poster now drop keycards
- EMP Poster
- 05 Secret
- End game stats
## Gameplay Changes:
- Pistol has same damage as revolver, but has less ammo
- Added poster in the test room, forced to EXIT poster in first game.
- No longer timed crates, now senior staff crates
- Reworked Exit Room
## Bugfixes:
- Loading was bugged each time you quit, was cause by timeScale not reseting