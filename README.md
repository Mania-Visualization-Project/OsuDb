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
|       Unknown       | 4bytes |     0xFFFFFFFF     |
|     Record Id     | uint64 |     1357537685     |

> Gamemode: 0-std, 1-taiko, 2-catch, 3-mania

### osu!.db

Structure

| Description | Type | Example |
| :---: | :---: | :---: |
| Version | unit32 | 20211122 |
| File Count | int32 | 234 |
| Unknown Boolean | uint8 | 1 |
| Unknown Time | int64 | ticks |
| Unknown | string | |
| Unknown | int32 | |
| Beatmaps | Beatmap(Count) | object |
| Unknown | int32 | |

Beatmap

| Description | Type | Example |
| :---: | :---: | :---: |
| Artist | string | string |
| Artist Unicode | string | string |
| Title | string | string |
| Title Unicode | string | string |
| Creator | string | string |
| Difficuty | string | string |
| Audio File | string | string |
| Md5 | string | string(32) |
| Beatmap File | string | string |
| Unknown | uint8 | 0 |
| Count of Key | uint16 | 123 |
| Count of Slider | unit16 | 14 |
| Count of Spinner | uint16 | 0 |
| Date Modified | int64 | ticks |
| Approach Rate | float32 | 9.2 |
| Circle Size | float32 | 4.2 |
| HP Drain Rate | float32 | 6 |
| Overall Difficulty | float32 | 8.5 |
| Unknown | float64 | 1.8 |
| Unknown A | int32 | 9 |
| Unknown Data Segment | bytes(A*14) | |
| Unknown B | int32 | 0 |
| Unknown Data Segment | bytes(B*14) | |
| Unknown C | int32 | 0 |
| Unknown Data Segment | bytes(C*14) | |
| Unknown D | int32 | 0 |
| Unknown Data Segment | bytes(D*14) | |
| Length | int32 | 86 |
| Unknown | int32 | 90150 |
| Unknown | int32 | 67040 |
| Unknown E | int32 | 50 |
| Unknown Data Segment | bytes(E*17) | |
| Beatmap Id | int32 | 2088231 |
| Beatmap Set Id | int32 | 998403 |
| Unknown | int32 | 0 |
| Rank of Std | unit8 | 9 |
| Rank of Catch | uint8 | 9 |
| Rank of Taiko | uint8 | 9 |
| Rank of Mania | uint8 | 9 |
| Offset | unit16 | 0 |
| Unknown | float32 | 0.4 |
| Mode | unit8 | 0 |
| Source | string | string |
| Tags | string | string |
| Unknown | uint16 | 0 |
| Unknown | string | string |
| Unknown Boolean | uint8 | 1 |
| Last Played Time | int64 | ticks |
| Unknown Boolean | uint8 | 1 |
| Folder Name | string | string |
| Last Info Update Time | int64 | ticks |
| Ignore Hit Sound | uint8 | 0 |
| Ignore Skin | uint8 | 1 |
| Disable Storyboard | uint8 | 1 |
| Disable Videp | uint8 | 1 |
| Unknown Boolean | uint8 | 0 |
| Unknown | int32 | 67097 |
| Mania Speed | uint8 | 0 |

> Rank: 0-SSH, 1-SH, 2-SS, 3-S, 4-A, 5-B, 6-C, 7-D, 8-F, 9-NeverPlayed
