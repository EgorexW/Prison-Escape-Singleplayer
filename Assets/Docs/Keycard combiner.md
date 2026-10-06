Requires a common item for each operation(wrench), behind worker card
## Combination ideas
- Admin + Guard = Chief Guard
- Warden + Soldier = 05
- Soldier + Admin or Guard + Warden = 05 (one use)
## Fallback mechanism
If recipe undefined - give a broken card
## Table
| ↓ \ →        | Admin         | Custodian     | Guard        | Medic            | Secure Wing | Senior Staff | Soldier | Staff        | Tech | Warden | Worker |
| ------------ | ------------- | ------------- | ------------ | ---------------- | ----------- | ------------ | ------- | ------------ | ---- | ------ | ------ |
| Worker       | --            | Archcustodian | -            | -                | -           | -            | -       | Senior Staff |      | -      | X      |
| Warden       | 05 (one-use)  | Archcustodian | 05 (one-use) | Field Medic      | -           | -            | 05      | -            |      | X      | X      |
| Tech         | Guard         | Archcustodian | Guard        | Safety Inspector | Guard       | Senior Tech  | -       | -            | X    | X      | X      |
| Staff        | -             | Archcustodian | -            | -                | -           | -            | -       | X            | X    | X      | X      |
| Soldier      | 05 (one-use)  | Archcustodian | 05 (one-use) | Field Medic      | -           | -            | X       | X            | X    | X      | X      |
| Senior Staff | Admin         | Archcustodian | Admin        | Field Medic      | Admin       | X            | X       | X            | X    | X      | X      |
| Secure Wing  | -             | Archcustodian | -            | Field Medic      | X           | X            | X       | X            | X    | X      | X      |
| Medic        | Field Medic   | Archcustodian | Field Medic  | X                | X           | X            | X       | X            | X    | X      | X      |
| Guard        | Chief Guard   | Archcustodian | X            | X                | X           | X            | X       | X            | X    | X      | X      |
| Custodian    | Archcustodian | X             | X            | X                | X           | X            | X       | X            | X    | X      | X      |
| Admin        | X             | X             | X            | X                | X           | X            | X       | X            | X    | X      | X      |
