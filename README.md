# MessageBoardWeb

## 簡介
以MVC架構撰寫之留言板，使用者可對此留言板進行新增、刪除、修改與查詢等動作。
![](https://i.imgur.com/PTwg4xU.png)


- 新增
點選add new message ，輸入使用者的名稱、留言標題以及留言內容，按下Add後即可返回Home，並看到新增的留言。
![](https://i.imgur.com/7U1L4KO.png)

![](https://i.imgur.com/DGmkBZI.png)

- 刪除
在Home頁面點選delete，即可將對應留言刪除。
![](https://i.imgur.com/sg9Eu3P.png)


- 修改
在Home頁面點選edit，即可編輯對應留言。
![](https://i.imgur.com/VruQtvG.png)
輸入欲調整之內容後，返回Home即可看到更改後的留言。
![](https://i.imgur.com/0EYYjLE.png)
![](https://i.imgur.com/fcKeEFW.png)

- 查詢
使用者可由Home介面，利用上方查詢欄選擇由使用者名稱查詢，或由文字內容查詢。
![](https://i.imgur.com/R1SYVgN.png)

    - 由使用者查詢
    使用者查詢會列出該使用者所建之留言
![](https://i.imgur.com/2PHAVAf.png)
![](https://i.imgur.com/Pr1vnrP.png)

    - 由文字內容查詢
    文字查詢會尋找在標題與內容中含有該文字之留言。
![](https://i.imgur.com/5iUWURa.png)
![](https://i.imgur.com/xSsmiuF.png)


## 程式架構
![](https://i.imgur.com/d1ko9Ue.png)
程式架構主要包含Models、Views和Controlls三部分。
- Models
主要包含使用到的資料模組以及與資料庫連接的DBManager。
- View
主要包含各頁面之頁面檔案。
- Controller
主要控制頁面切換，以及頁面間的資料傳送。
- DBManager 詳細內容
以singleton設計，內含User與Message資料表的增加、刪除、修改與查詢之method。
![](https://i.imgur.com/v8DEeFn.png)

## Database
local database 資料表設計如下，主要有UserInfo與Message兩表。
- relational schema
![](https://i.imgur.com/kT5URYe.png)
    - userInfo
    主要儲存使用者id與名稱
    - Message
    主要儲存留言id、標題、內容、留言者id以及留言時間
- user table
```sql=
CREATE TABLE [dbo].[UserInfo] (
    [UserId]   INT        IDENTITY (1, 1) NOT NULL,
    [UserName] NCHAR (10) NOT NULL,
    PRIMARY KEY CLUSTERED ([UserId] ASC),
    UNIQUE NONCLUSTERED ([UserName] ASC)
);
```
- message table
```sql=
CREATE TABLE [dbo].[Message] (
    [MessageId]      INT      IDENTITY (1, 1) NOT NULL,
    [MessageTitle]   TEXT     NULL,
    [MessageContent] TEXT     NULL,
    [UserId]         INT      NULL,
    [Time]           DATETIME DEFAULT (getdate()) NOT NULL,
    PRIMARY KEY CLUSTERED ([MessageId] ASC),
    CONSTRAINT [FK_Message_ToTable] FOREIGN KEY ([UserId]) REFERENCES [dbo].[UserInfo] ([UserId])
);
```
