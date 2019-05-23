SELECT 'SELECT setval(''' || quote_ident(min(seq_name)) || ''',' ||' COALESCE((select max( '|| quote_ident(min(column_name)) ||') from '|| quote_ident(min(table_name)) ||') + 1,1));' as sql_column
FROM (
    SELECT 
        n.nspname AS schema_name,
        c.relname AS table_name,
        a.attname AS column_name,
        substring(d.adsrc FROM E'^nextval\\(''([^'']*)''(?:::text|::regclass)?\\)') AS seq_name 
    FROM pg_class c 
    JOIN pg_attribute a ON (c.oid=a.attrelid) 
    JOIN pg_attrdef d ON (a.attrelid=d.adrelid AND a.attnum=d.adnum) 
    JOIN pg_namespace n ON (c.relnamespace=n.oid)
    WHERE has_schema_privilege(n.oid,'USAGE')
      AND n.nspname NOT LIKE 'pg!_%' escape '!'
      AND has_table_privilege(c.oid,'SELECT')
      AND (NOT a.attisdropped)
      AND d.adsrc ~ '^nextval'
) seq
GROUP BY seq_name HAVING count(*)=1;