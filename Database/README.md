# Hướng dẫn Import Database Employees


## Hướng dẫn

1. Tạo database và schema
```sql
CREATE DATABASE employees;
\c employees
CREATE SCHEMA employees;
```

2. Tải dữ liệu mẫu về
```code
wget https://raw.githubusercontent.com/neondatabase/postgres-sample-dbs/main/employees.sql.gz
```

3. Mở terminal và import data bằng lệnh sau
```code
pg_restore -d postgresql://[user]:[password]@[neon_hostname]/employees -Fc employees.sql.gz -c -v --no-owner --no-privileges
```