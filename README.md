# osu! db reader

## osu! db file format

### common

String

| head | uint8 | char(length) |
| :--: | :----: | :----------: |
| 0x0B | length | string data |

### scores.db

Structure

|     Description     |           Type           | Example |
| :------------------: | :----------------------: | :------: |
|       Version       |          uint32          | 20211122 |
| Conut of Collections |          unit32          |    10    |
|  Record Collections  | **RecordCollection(N)** |  Object  |

RecordCollection

|   Description   |        Type        |  Example  |
| :--------------: | :-----------------: | :--------: |
|   Beatmap MD5   |       string       | string(32) |
| Count of Replays |       unit32       |     3     |
|     Records     | **Record(N)** |   Object   |

Record

|     Description     |  Type  |  Example  |
| :------------------: | :----: | :--------: |
|      Game Mode      | uint8 |     3     |
|     Osu Version     | uint32 |  20210821  |
|     Beatmap MD5     | string | string(32) |
|     Player Name     | string |   Rain 7   |
|      Replay MD5      | string | string(32) |
|        Unkown        | unit16 |    730    |
|        Unkown        | unit16 |     32     |
|        Unkown        | unit16 |     6     |
|        Unkown        | unit16 |     94     |
|        Unkown        | unit16 |     95     |
|        Unkown        | unit16 |     24     |
|        Score        | unit32 |   853922   |
|        Combo        | unit32 |    792    |
|    Is Full Combo    | uint8 |     0     |
|         Mods         | unit16 |     0     |
|        Unkown        | string | string(0) |
| Play Time(Timestamp) | uint32 | 1152598674 |
|        Unkown        | 4bytes |           |
|        Unkown        | 4bytes | 0xFFFFFFFF |
|        Unkown        | uint64 | 1357537685 |


> Gamemode: 3-mania
