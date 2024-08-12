# Queries ES


1. Add data
```code
PUT /orders/username/customer1
{
  "username": "customer1",
  "totalPrice": 2000,
  "firstName": "Kiet",
  "lastName": "Nguyen"
}
```

2. Get data
```code
GET /orders/username/customer1
```

3. Delete data
```code
DELETE /orders/username/customer1
```

4. Search all
```
GET /employee/_search?q=userId:>11321
```
```
GET /orders/_search
{
  "query": {
    "match_all": {}
  }
}
```
5. Search with match
```
GET /employee/_search
{
  "query": {
    "match": {
      "userId": "10051"
    }
  }
}
```
```
GET /employee/_search
{
  "query": {
    "match": {
      "firstName": "Hidefumi"
    }
  }
}
```
5. Search with term
```
GET /employee/_search
{
  "query": {
    "bool": {
      "must": [
        { "term": { "hireDate.year": 1992 } },
        { "term": { "hireDate.month": 10 } },
        { "term": { "hireDate.day": 15 } }
      ]
    }
  }
}
```

6. Search Fuzzy
```
GET employee/_search
{
  "query": {
    "fuzzy": {
      "firstName": {
        "value": "hpig",
        "fuzziness": 3
      }
    }
  }
}
```
```
GET /employee/_search
{
  "query": {
    "bool": {
      "should": [
        {
          "fuzzy": {
            "firstName": {
              "value": "hpig",
              "fuzziness": 3
            }
          }
        },
        {
          "fuzzy": {
            "lastName": {
              "value": "hpig",
              "fuzziness": 3
            }
          }
        }
      ],
      "minimum_should_match": 1
    }
  }
}
```

Giải Thích

	1.	Truy Vấn Bool:
            bool query với should cho phép bạn kết hợp nhiều truy vấn. Trong ví dụ này, có hai truy vấn fuzzy, một cho firstName và một cho lastName.
	2.	Truy Vấn Fuzzy:
	        fuzzy query cho phép tìm kiếm các tài liệu mà trường tìm kiếm gần giống như giá trị cung cấp, với độ mờ được chỉ định.
	        value: Giá trị tìm kiếm với độ mờ.
	        fuzziness: Độ mờ cho phép sai lệch trong tìm kiếm, ở đây là 3 ký tự.
	3.	minimum_should_match:
	        Xác định số lượng điều kiện trong mảng should phải khớp để tài liệu được trả về. minimum_should_match: 1 có nghĩa là ít nhất một trong các điều kiện phải khớp.