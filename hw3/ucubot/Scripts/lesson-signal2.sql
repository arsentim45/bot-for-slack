use ucubot;
alter table lesson_signal drop user_id;
alter table lesson_signal add student_id int;
ALTER TABLE lesson_signal ADD
	constraint fk_student_id
	FOREIGN KEY (student_id) REFERENCES student(id)
  ON UPDATE RESTRICT ON DELETE RESTRICT;