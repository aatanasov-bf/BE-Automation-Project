Feature: AdminActions

A short summary of the feature

@AdminActions
Scenario: Admin can't create admins
	Given I login with admin1 and admin123
	When I enter user detils "test_nasko_admin" "admin1234" "admin" 
	And I create the user
	Then I get message "Invalid role" and the user is not created

Scenario: Admin can't create students
	Given I login with admin1 and admin123
	When I enter user details "test_nasko_student" "student1234" "admin" 
	And I create the user
	Then I get message "Invalid role" and the user is not created

Scenario: Admin can create parents
	Given I login with admin1 and admin123
	When I enter user details "test_nasko_parent" "parent1234" "parent"
	And I create the user
	Then I get message "parent 'test_nasko_parent' created successfully" and the user is created

Scenario: Admin can create teachers
	Given I login with admin1 and admin123
	When I enter user details "test_nasko_teacher" "teacher1234" "teacher"
	And I create the user
	Then I get message "teacher 'test_nasko_teacher' created successfully" and the user is created

Scenario: Admin can create moderators
	Given I login with admin1 and admin123
	When I enter user details "test_nasko_moderator" "moderator1234" "moderator"
	And I create the user
	Then I get message "moderator 'test_nasko_moderator' created successfully" and the user is created
