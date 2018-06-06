create database ucubot;
use ucubot;
create user 'Arsen'@'%' identified by 'pasPKN-16-B';
GRANT ALL PRIVILEGES ON ucubot.* TO 'Arsen'@'%';
flush privileges;