# ExpressionTree

Example for converting below json to efcore execuable query.



```json
{
  "Name": "Customer",
  "Properties": [
    {
      "Name": "Id",
      "Type": "Integer",
      "Constraints": [
        {
          "Operator": "Equal",
          "Value": 1,
          "Unit": null
        }
      ]
    },
    {
      "Name": "Name",
      "Type": "String",
      "Constraints": [
        {
          "Operator": "Equal",
          "Value": "Alice",
          "Unit": null
        }
      ]
    }
  ]
}
```



Console output:



```shell
/Users/jwhu/Repo/Learning/ExpressionTree/ExpressionTree/ExpressionTree/bin/Debug/net6.0/ExpressionTree
SELECT [c].[Id], [c].[Name]
FROM [Customers] AS [c]
WHERE ([c].[Id] = 1) AND ([c].[Name] = N'Alice')
Done

Process finished with exit code 0.


```

