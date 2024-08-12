# Hướng dẫn sử dụng EmployeesESNET

### Yêu Cầu
1. .NET 8
2. Postgres


### Cấu hình

1. Cấu hình Database Postgres src/appsettings.json
```json
 "DataContext": "Host=[host];Port=[port];Database=[database name];User Id=[username];Password=[password];"
```
Thông tin máy database
- host: http://localhost
- port: 6543
- database name: tên database (anhdev99)
- username: tài khoản database
- password: mật khẩu database

2. Cấu hình ElasticSearch
```json
  "Elastic": {
    "username": [username],
    "password": [password],
    "host": [host]
  },
```
Thông tin máy ElasticSearch
- username: elastic
- password: anhdev99
- host: http://localhost:9200
