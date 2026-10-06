# Prison Escape — Design Decision Log  
  
Record of design verdicts from a review pass. Numbered items refer to that review's takes.  
Verdicts are the designer's; items marked **CORRECTION** are places the review had it wrong.  
  
---  
  
## Core design principles (extracted)  
  
- **Every run behaves on the same rules.** No global run modifiers, no biomes, no per-run  
  rulesets. The game should be one consistent game.  
- **Discovery-based mechanics.** Several systems are deliberately never explained  
  (e.g. Broken Keycard hacking). Players are meant to find them.  
- **Both main escape routes are meant to be always guaranteed.**  
- **Routes should work in different ways**, not be reskins of each other.  
- Don't duplicate mechanics.  
- New ideas must be *smartly integrated* — a good idea still needs the right room and the  
  right framing before it goes in. Ideas and time are the constraint, not the idea list.  
  
---  
  
## Escape routes — confirmed intent  
  
### The two guaranteed routes  
  
| Route | Mechanism | Cost shape |  
|---|---|---|  
| **Heavy Armory → Soldier** | Guard **passes** | consumable cards |  
| **Administration → Warden** | **disc** + health drop | permanent HP |  
  
The asymmetry is deliberate and is the point — the armoury route runs on passes, the warden  
route runs on a disc and a health cost.  
  
- **Soldier card can be one-use.** Its only required use is the Exit, so one use is enough.  
- **You should not be able to win with a single Guard card.** Having one Guard card but still  
  not being able to escape is intentional and makes for more interesting play.  
- **A third "normal keycard" route is missing.** Acknowledged, not a concern.  
  
### Guarantees and accepted risk  
  
- **The Warden Missing Protocol disc is guaranteed inside Big Infirmary.**  
  **CORRECTION** — the review treated it as a drop chance.  
- The Warden route technically depends on an Admin card, which is not strictly guaranteed.  
  Chance of the route being genuinely undoable is **below 0.1** — accepted.  
- **It is close to impossible for a run to spawn no Guard keycards.** (Review take 2 claimed  
  tripling the Guard cost could strand the player — rejected on these grounds. Worth  
  confirming with real math.)  
- Hints pointing at the Warden route exist across the game; players should find it early.  
  
### Guard Pass economy  
  
- Guard Passes live in **Secure Storage**, in **Lockers**, and **one in Security**.  
- **The Security one is deliberately free** so the military route is nearly always viable.  
- Security sits inside the secure wing, which is already not easy to enter — so "ungated  
  door" does not mean "free access."  
  
### The Exit  
  
- Main exit is the **Vault**. The 05 Room is not a vault (Strong door).  
- **The player is told the win condition at spawn**: a poster at the spawn point states you  
  need the Warden or Soldier card, with the vault door pictured behind it.  
- The vault's card reader has **EXIT written on it explicitly**. Players notice immediately  
  on first pass.  
- **Grenade breach of the Vault stays possible.** Five grenades is fine — you can also make  
  multiple trips, and Bags increase slot count. (Review take 32, bump Vault HP — rejected.)  
  
---  
  
## Verdicts by area  
  
### Heavy Armory / gating  
  
| # | Take | Verdict |  
|---|---|---|  
| 1 | Three Guard gates make the Reactivater load-bearing | **Agree** |  
| 2 | Tripling Guard cost may strand the player | **Disagree** — near-impossible not to spawn Guard cards. Check the math |  
| 4 | Make Soldier card one-use | Rejected as framed (rationale was wrong) — but one-use *is* viable, see above |  
| 5 | One Guard door accepts Soldier/Chief Guard | **Rejected** — illogical; if you hold Soldier you've already won |  
| 6 | A shared "Guard-tier" access level | **Rejected** |  
| 7 | Guaranteed Guard Pass inside the Armory | **Rejected** — no point in the Armory having free gates |  
| 8 | Steal-card readers need telegraphing | **Already handled** — steal readers are always visible |  
| 9/10 | Security is the Guard Pass faucet; narrow it | **Rejected** — Security is *meant* to have it |  
| 13 | Late-run safety-valve announcement | **Rejected** |  
| 14 | Vault hackResistance decays over time | **Rejected** |  
  
> **Open question:** noted that "Security needs to be optional because it's where all the  
> contraband items go." Confiscated items are routed to a `Confiscated Items` trigger, which  
> implies that room must *always* exist. Worth resolving whether Security should be  
> necessary or optional.  
  
### Cards  
  
| # | Take | Verdict |  
|---|---|---|  
| 15 | Passes need a stronger visual difference | **Rejected** — a strong difference already exists |  
| 16 | Reactivater needs a second source | Reactivater is **meant to be optional** |  
| 18 | Uploader should consume the card | **CORRECTION** — `destroyOnUse` refers to the *Uploader itself*, not the card. Take was based on a misread |  
| 19 | Uploader would open both Armory doors | **Better than that** — it lets you **skip steal-keycard readers**. Not previously considered; good find |  
| 20 | Custodian is a key-XOR-reagent dilemma | **CORRECTION** — opening Secure Storage does **not** consume the Custodian. No XOR |  
| 21 | Broken Keycard tiering is fine as-is | Agreed |  
| 22 | Telegraph tamper resistance | **Partial** — strong doors are visible, so players can assume they're tougher; but they can't infer higher *hack* resistance specifically |  
| 23 | Failed combines producing Broken cards is good | Agreed |  
  
**Uploader power level:** not strong on a permanent high card — you could have walked the  
facility opening everything anyway. It is essentially a convenience/time-saver.  
It does **not** trivialise gates, because passes are one-use and the Uploader unlocks only  
**one random** matching room before the pass is spent.  
  
**Archcustodian** is intended as a **niche secret**, not a genuine strategy.  
  
**Broken Keycard hacking is never explained to the player by design.** The whole mechanic  
should stay discovery-based.  
  
### Combiner  
  
| # | Take | Verdict |  
|---|---|---|  
| 24 | Teach fusion even when The Conduit is absent | **Rejected** — players will find The Conduit; spawn rate is above 50% |  
| 25 | Recipe terminal | **Rejected** |  
| 26 | Announce the combiner blackout | **True, but** — there is already a spark effect from the combiner. Leave until players start abusing it |  
| 27 | Strip Exit inheritance from fusion results | **Rejected** — no point removing it |  
| 29 | Portable combiner | **Rejected** — don't want to duplicate mechanics |  
  
### Exit / endgame  
  
| # | Take | Verdict |  
|---|---|---|  
| 32 | Close the 5-grenade breach | **Rejected** — should stay possible |  
| 34 | Show the Vault early | **Already done** — spawn poster + "EXIT" on the reader |  
| 36 | Add a third, easier exit | **Rejected** — the 05 secret already serves as the alternate route |  
  
### Discs / protocols  
  
| # | Take | Verdict |  
|---|---|---|  
| 38 | Discs are under-explained; mark terminals | **Rejected** — terminals say "insert disk", terminals are always in the same rooms, and the Smart Map shows room names |  
| 39 | Postpone Purify (+180s) may be too common | **Rejected** — finding and using it costs ~1 minute, so net value is ~2 minutes. It should be rewarding |  
| 40 | A disc that costs time but grants something | **Maybe** — discs already require investment more often than not. Will implement given a good idea |  
| 41 | More protocols should carry a body cost | **Agree in principle** — but can't just add damaging things arbitrarily; needs to be smart. Open question whether the game has too few damage sources |  
| 42 | Disc that permanently sets/kills a sector's power | **Interesting — parked in the obsolete/ideas folder** |  
  
### Traps  
  
| # | Take | Verdict |  
|---|---|---|  
| 45 | Surface that Linked Items is destructible | A poster already says you can shoot a lot of things. Considering a **discoverable list of shootable things**, placed deep enough that players don't find it first |  
| 46 | Steal Card Trap is invisible | **Rejected** — it is visible |  
| 47 | Poison steering via destructible vents | **Rejected as framed** — no point destroying vents to move poison. But poison **could be expanded further** given proper ideas |  
  
### Power  
  
| # | Take | Verdict |  
|---|---|---|  
| 49 | Power Holder is under-exposed | **Rejected** — it also spawns rarely in general loot. Deliberate: most runs should *not* be shaped by a Power Holder |  
| 50 | Graded electrocution instead of 100% | **Rejected** |  
| 51 | Preview power zones before a Blackout | **Rejected** — blackout always affects your own zone |  
| 52 | A room built around power routing | Needs to be a **smart room**. Related to 42. Finding a room for a specific item is doable, but it has to be integrated well — needs ideas and time |  
  
### Wilder ideas  
  
| # | Take | Verdict |  
|---|---|---|  
| 53 | Let the player lock doors behind them | **Questioned** — what would the point be? |  
| 54 | Reprogram readers with a disc | **Can't be that simple** |  
| 55 | An item that reveals what a reader wants | **Rejected** — readers display what they want all the time |  
| 56 | Rotate room variants per run | **Deprioritised** — how would variants differ *meaningfully*? More work, and there are likely more interesting things to focus on |  
| 57 | Player-chosen run modifiers | **Rejected** — see core principle. Biomes / differing room-type sets also rejected. The game should be one consistent game |  
  
---  
  
## Parked ideas  
  
- **Sector power protocol disc** (take 42) — permanently set one sector to full power, or  
  permanently kill one. Filed in obsolete/ideas.  
- **A power-routing room** (take 52) — pairs with the above. Needs the right room design.  
- **Discoverable list of shootable things** — hidden deep, so players discover shooting  
  organically first.  
- **Poison expansion** — the mechanic has room to grow, pending a proper idea.  
- **A disc with a time cost and a large payoff** (take 40) — pending a good idea.  
  
---  
  
## Open questions  
  
1. **Security: necessary or optional?** Stated as needing to be optional, but it houses  
   confiscated items, which implies it must always spawn.  
2. **Guard card spawn math** — confirm it really is near-impossible for a run to produce no  
   Guard-tier cards (relevant to take 2).  
3. **Does the game have too few damaging things?** Leaning yes, but additions must be  
   deliberate rather than sprinkled.  
  
---  
  
## Corrections to the review's factual claims  
  
- Warden Missing Protocol disc is **guaranteed** in Big Infirmary, not a drop chance.  
- `KeycardUploader.destroyOnUse` destroys the **Uploader**, not the keycard.  
- Opening Secure Storage does **not** consume the Custodian card.  
- The Uploader spends a pass after **one random** matching room, which is why it doesn't  
  trivialise multi-gate rooms.  
- A strong visual difference between passes and keycards already exists.  
- Steal Card Trap and steal-readers are already visible.  
- The player is told the win condition at spawn via poster, and the vault reader reads EXIT.  
- Security is inside the secure wing — an ungated door does not mean easy access.