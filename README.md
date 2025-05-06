# Carcassonne

カルカソンヌガイドシステムの開発用

From 2025/05/06 ~ To 2025//

## git clone

このリポジトリをダウンロードしたいとき．

```shell
git clone https://github.com/AL23075/Carcassonne.git
```

これでできます．

## git add remote

このリポジトリで共同開発を行うとき．

```shell
git remote add origin https://github.com/AL23075/Carcassonne.git
git branch -M main
git push -u origin main
```

`git clone`->`git add remote`までやれば，セットアップは終了です．

## git add

変更を加えたすべてのファイルをgit上にあげたい場合

```shell
git add .
```

特定のファイルだけgit上にあげたい場合

```shell
git add <your-file-path>
```

`your-file-path`の部分は現在地からの相対パスでも絶対パスでもいいです．  
`<your-file-name>`の部分は適宜変えること．

## git reset HEAD

間違えて`git add`した場合．

```shell
git reset HEAD
```

これで，`git add`したファイルがすべて取り消されます．
特定のファイルのみを取り消したいときは，以下を実行すること．

```shell
git reset HEAD <your-file-path>
```

`your-file-path`の部分は現在地からの相対パスでも絶対パスでもいいです．  
`<your-file-name>`の部分は適宜変えること．

## git commit -m "comment"

`git add`した内容をcommit（皆が見れる状態にしたいとき）

```shell
git commit -m "your-comment"
```

`your-comment`の部分は適宜変えること．  
できるだけ「どこを変更した」とか，「どこを直した」的なのを簡潔に書いてくれると助かります．

## git status

現在何が`add`されているのかやどのブランチにいるのか確かめたいとき．

```shell
git status
```

これで詳細が表示されます．

## git branch

現在いる`branch`名を確認できます．

```shell
git branch
```

## git checkout <branch-name>

`branch`を切り替えたいとき

```shell
git checkout <branch-name>
```

これで`branch`の切り替えができます．  
`<branch-name>`の部分は適宜変えること．

## git checkout -b <branch-name>

`branch`を作成したいとき

```shell
git checkout -b <branch-name>
```

これで`branch`の作成ができます．  
`<branch-name>`の部分は適宜変えること．  
ただし，このままではローカルにあるだけ（皆が見れない）ので，次の`git push -u origin <branch-name>`で確認してください．

## git push -u origin <branch-name>

`branch`を登録したいとき．

```shell
git push -u origin <branch-name>
```

これで登録できます．  
`branch`を作成したら，ここまでやること．

## Contact

その他分からないことがあったら，`Issues`かSlackで連絡ください．
