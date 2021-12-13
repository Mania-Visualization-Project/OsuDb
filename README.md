# osu! db reader

## osu! db file format

### common

String

| head | uint8 | char(length) |
| :--: | :----: | :----------: |
| 0x0B | length | string data |

**In some cases, empty string can be presented by a single byte 0x00, so both pattens bellow are legal empty strings:**

- 0x0B00
- 0x00

### scores.db

Structure

|     Description     |             Type             | Example |
| :------------------: | :---------------------------: | :------: |
|       Version       |            uint32            | 20211122 |
| Conut of Collections |            unit32            |    10    |
|  Record Collections  | **RecordCollection(N)** |  Object  |

RecordCollection

|   Description   |        Type        |  Example  |
| :--------------: | :-----------------: | :--------: |
|   Beatmap MD5   |       string       | string(32) |
| Count of Replays |       unit32       |     3     |
|     Records     | **Record(N)** |   Object   |

Record

|    Description    |  Type  |      Example      |
| :----------------: | :----: | :----------------: |
|     Game Mode     | uint8 |         3         |
|    Osu Version    | uint32 |      20210821      |
|    Beatmap MD5    | string |     string(32)     |
|    Player Name    | string |       Rain 7       |
|     Replay MD5     | string |     string(32)     |
|     300 Count     | unit16 |        730        |
|     100 Count     | unit16 |         32         |
|      50 Count      | unit16 |         6         |
| Colorful 300 Count | unit16 |         94         |
|     200 Count     | unit16 |         95         |
|     Miss Count     | unit16 |         24         |
|       Score       | unit32 |       853922       |
|       Combo       | unit16 |        792        |
|   Is Full Combo   | uint8 |         0         |
|        Mods        | uint32 |         0         |
|    Performance    | string |       string       |
|   Play Time(UTC)   | uint64 | 637742886299537246 |
|       Unkown       | 4bytes |     0xFFFFFFFF     |
|     Record Id     | uint64 |     1357537685     |

> Gamemode: 0-std, 1-taiko, 2-catch, 3-mania
