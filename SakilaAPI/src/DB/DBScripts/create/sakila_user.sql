CREATE USER 'apiuser'@'%' 
  IDENTIFIED BY 'securePassword!';
GRANT SELECT,INSERT,UPDATE,DELETE
  ON sakila.* TO 'apiuser'@'%';
FLUSH PRIVILEGES;
