```mermaid
flowchart TD

subgraph L1["Level 1 (Always Open)"]

Worker["Janitor"]

Medic["Medic"]

Staff["Staff"]

Lead["Guest"]

Storage["Storage"]

Reception["Security"]

Administration["Administration"]

end

subgraph L2["Level 2 (Weak Doors)"]

Secure_Wing["Secure Wing"]

Break_Room["Break Room"]

Offices["Offices"]

Infirmary["Infirmary"]

Senior_Staff["Senior Staff"]

Tech["Tech"]

Custodian["Custodian"]

Servers["Servers"]

Power_Room["Power Room"]

Admin["Admin"]

Admin_Office@{ label: "Admin's Office" }

Secure_Storage["Secure Storage"]

end

subgraph L3["Level 3 (Strong Doors)"]

Guard["Guard Pass"]

Soldier["Soldier"]

Warden["Warden"]

Armory["Armory"]

Guard_Full["Guard"]

end

Secure_Wing --> Reception --> Guard

Storage --> Worker & Medic & Staff & Lead

Administration --> Worker & Medic & Staff & Lead

Medic --> Infirmary

Worker --> Break_Room

Break_Room --> Admin & Senior_Staff

Staff --> Offices

Offices --> Tech & Senior_Staff & Custodian

Tech --> Servers & Power_Room

Admin --> Admin_Office & Secure_Wing

Custodian --> Secure_Storage

Guard --> Armory & Secure_Wing

Guard_Full --> Armory -.-> Soldier

Armory --> Guard_Full

Lead --> Secure_Wing

Senior_Staff ==> Warden

Warden --> Exit["Exit"]

Soldier --> Exit

O5["O5"]

Admin_Office@{ shape: rect}

Secure_Storage -.-> Guard
```

| Level | Room           | Loot                      | Others                |
| ----- | -------------- | ------------------------- | --------------------- |
| 1     | Storage        | Basic                     |                       |
| 1     | Administration | Basic                     | Map                   |
| 1     | Security       | Bag                       | Computer?, Contraband |
| 2     | Office         | Cards                     |                       |
| 2     | Break Room     | Cards, Better             | Bin                   |
| 2     | Admin          | Weapon, Basic             | Computer, Bin         |
| 2     | Power Room     | Generator                 | Backup Power, Bin     |
| 2     | Servers        | Discs                     |                       |
| 2     | Infirmary      | Meds, (Unlock Warden)     |                       |
| 2     | Secure Storage | Secure                    |                       |
| 3     | Warden         | Warden Card, Weapon       |                       |
| 3     | Armory         | Weapons, Guard, (Soldier) | Big has double door   |
- Rethink 05 Access
## Escape Paths
```mermaid
flowchart TD

%% Warden Access

%% Guest --> Security

Worker --> Break_Room --> Admin & Senior_Staff

Admin --> Computer

Staff --> Office --> Tech & Senior_Staff

Tech --> Servers -.-> Unlock_Warden

Medic --> Infirmary --> Unlock_Warden

Unlock_Warden & Senior_Staff & Computer ==> Warden_Access

Warden_Access --> Warden

%% ---

  

%% Soldier Access

Basic_Loot & Secure_Storage -.-> Guard_Pass

Guest & Admin --> Security --> Guard_Pass

Office --> Custodian --> Secure_Storage

Guard_Pass -.-> Guard --> Soldier

  

%% ---

Weapons -.-> Soldier & Warden

Soldier & Warden --> Escape

  

%% 05

Servers -.-> 05_Protocol

05_Protocol & Computer ==> 05 --> Escape

%% ---
```
