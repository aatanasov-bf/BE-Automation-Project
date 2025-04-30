Feature: TeacherActions

A short summary of the feature

@TeacherActions
Scenario: Teacher creates class
	Given I login with test_nasko_teacher and teacher1234
	When I enter class details "class_nasko_name" "test_nasko_sub_1" "test_nasko_sub_2" "test_nasko_sub_3"
	And I create the class
	Then I get message "Class created" and the class is created
	And I get the class identifier

Scenario: Teacher adds student to class
	Given I login with test_nasko_teacher and teacher1234
	When I enter stident details "test_student111" "1597c0e6-e8e4-4890-b0d4-1e3e1de8ed32"
	And I add the student to the class
	Then I get message "Student added" and the student is added
	And I get the student identifier

Scenario: Teacher adds grade to student
	Given I login with test_nasko_teacher and teacher1234
	When I enter new grade details "f9e213e2-af8f-41af-8487-467d5d60a06e" "subject1" "4"
	And I add new grade for the student
	Then I get message "Grade added" and the grade is added

Scenario: Teacher updates student grade
	Given I login with test_nasko_teacher and teacher1234
	When I enter new grade details "f9e213e2-af8f-41af-8487-467d5d60a06e" "subject1" "2"
	And I update student grade
	Then I get message "Grade updated" and the grade is updated