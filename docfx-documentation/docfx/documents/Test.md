# テスト方法
.NET Coreの自動テストについて記載する。

# .NET Coreのテスト
1. .NETのコードを書く
  - [公式チュートリアル](https://docs.microsoft.com/ja-jp/dotnet/core/tutorials/library-with-visual-studio-code)を参考
2. .NETのテストコードを書く
  - [公式チュートリアル](https://docs.microsoft.com/ja-jp/dotnet/core/tutorials/testing-library-with-visual-studio-code)を参考
3. 以下のコマンドでテストコードを実行してCoverageをymlで出力する

  ```bash
  dotnet test --collect:"XPlat Code Coverage"
  ```

# .NETの自動テスト
## GitHub Actionsを利用した例

1. ワークフローのymlを作成する

    ```bash:.github/workflows/*.yml
    name: .NET Core CI
    on:
    push:
        branches:
        - master
    jobs:
    test:
        runs-on: ubuntu-latest
        steps:
        - uses: actions/checkout@master
        - name: Setup .NET Core
            uses: actions/setup-dotnet@v1
            with:
            dotnet-version: '3.1.x'
        - name: .NET Build
            run: dotnet build 
        - name: .NET Test
            id: dotnet_test
            run: |
                dotnet test --collect:"XPlat Code Coverage"
                REPORT_PATH=`find ./ -name "coverage*.xml"`
                echo ::set-output name=report_path::${REPORT_PATH}
        - name: Upload coverage to Codecov
            uses: codecov/codecov-action@v1
            with:
            token: ${{ secrets.CODECOV_TOKEN }}
            file: ${{ steps.dotnet_test.outputs.report_path }}
```

2. Gitにpushすると自動でテストが開始される。
  - codecovにカバレッジがアップロードされる。

> [!TIP]
> 上記コマンドでは、`./TestResults/{hash}/coverage.cobertura.xml`にレポートが出力される
> そのため、`find`コマンドでパスを特定してワークフロー間で変数の受け渡しをする必要がある<br>
> [GitHub Actionsのワークフローコマンド](https://help.github.com/ja/actions/reference/workflow-commands-for-github-actions#setting-an-output-parameter)<br>
> [GitHub Actionsのメタデータ構文](https://help.github.com/ja/actions/creating-actions/metadata-syntax-for-github-actions)<br>