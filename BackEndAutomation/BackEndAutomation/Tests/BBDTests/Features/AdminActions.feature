Feature: AdminActions

A short summary of the feature

@AdminActions
Scenario: Admin can't create admins
	Given I login with admin1 and admin123
	When I enter user details "test_nasko_admin" "admin1234" "admin" 
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

Scenario: Admin connects existing parent to existing student
	Given I login with admin1 and admin123
	When I enter parent student pair details "test_nasko_parent_grades" "a892f5cb-0040-4e6b-841b-65520e72bf49"
	And I pair the parent and the student
	Then I get message "Parent linked to student" and the pair is connected

Scenario: Admin connects non existing parent to student
	Given I login with admin1 and admin123
	When I enter parent student pair details "test_nasko_parent_gradesasdas" "a892f5cb-0040-4e6b-841b-65520e72bf49"
	And I pair the parent and the student
	Then I get message "Parent not found" and the pair is not connected

#this test is passing but it should fail
Scenario: Admin connects existing parent to no existing student
	Given I login with admin1 and admin123
	When I enter parent student pair details "test_nasko_parent_grades" "sdasdasdasdassda"
	And I pair the parent and the student
	Then I get message "Parent linked to student" and the pair is not connected
