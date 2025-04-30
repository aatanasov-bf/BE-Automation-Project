Feature: ModeratorActions

A short summary of the feature

@ModeratorActions
Scenario: Teacher moves student in different class
	Given I login with test_nasko_teacher and teacher1234
	When I enter student move details "b6bc6146-19de-42b3-a7cd-a6c9146c4fb9" "25789a09-4033-4738-aed3-d7b74e4b0f0b"
	And I move the student
	Then I get message "Only moderators can move students" and the student is not moved.

Scenario: Teacher deletes class
	Given I login with test_nasko_teacher and teacher1234
	When I enter delete class details "25789a09-4033-4738-aed3-d7b74e4b0f0b"
	And I delete the class
	Then I get message "Only moderators can delete classes" and the class is not deleted.

Scenario: Moderator moves student in different class
	Given I login with test_nasko_moderator and moderator1234
	When I enter student move details "b6bc6146-19de-42b3-a7cd-a6c9146c4fb9" "25789a09-4033-4738-aed3-d7b74e4b0f0b"
	And I move the student
	Then I get message "Student moved" and the student is moved.

Scenario: Moderator deletes non existing class
	Given I login with test_nasko_moderator and moderator1234
	When I enter delete class details "8fea2b2a-6f8d-44dc-a83f-8bd5de2c696d"
	And I delete the class
	Then I get message "Class not found" and the class is not deleted.

Scenario: Moderator deletes existing class with students
	Given I login with test_nasko_moderator and moderator1234
	When I enter delete class details "8fea2b2a-6f8d-44dc-a83f-8bd5de2c696d"
	And I delete the class
	Then I get message "Class not found" and the class is not deleted.

Scenario: Moderator deletes existing class without students
	Given I login with test_nasko_moderator and moderator1234
	When I enter delete class details "f3ea4c53-5197-4d30-91d7-304b114d026e"
	And I delete the class
	Then I get message "Class deleted" and the class is deleted.


