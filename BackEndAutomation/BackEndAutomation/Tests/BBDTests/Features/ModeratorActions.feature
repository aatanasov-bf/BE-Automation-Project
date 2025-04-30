Feature: ModeratorActions

A short summary of the feature

@ModeratorActions
Scenario: Teacher moves student in different class
	Given I login with "test_nasko_teacher" and "teacher1234"
	When I enter student move details
	And I move the student
	Then I get message "Only moderators can move students" and the student is not moved.

Scenario: Teacher deletes class
	Given I login with "test_nasko_teacher" and "teacher1234"
	When I enter delete class details
	And I delete the class
	Then I get message "Only moderators can delete clases" and the student is not moved.

Scenario: Moderator moves student in different class
	Given I login with "test_nasko_moderator" and "moderator1234"
	When I enter student move details
	And I move the student
	Then I get message "Student moved" and the student is moved.

Scenario: Moderator deletes existing class witho students
	Given I login with "test_nasko_moderator" and "moderator1234"
	When I enter student move details
	And I move the student
	Then I get message "Student moved" and the student is moved.

Scenario: Moderator deletes existing class without students
	Given I login with "test_nasko_moderator" and "moderator1234"
	When I enter student move details
	And I move the student
	Then I get message "Student moved" and the student is moved.

Scenario: Moderator deletes non existing class
	Given I login with "test_nasko_moderator" and "moderator1234"
	When I enter student move details
	And I move the student
	Then I get message "Student moved" and the student is moved.


