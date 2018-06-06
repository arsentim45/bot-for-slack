use ucubot;
CREATE TABLE student(
    id int not null auto_increment primary key,
    first_name VARCHAR(255),
    last_name VARCHAR(255),
	  user_id VARCHAR(255) unique
);