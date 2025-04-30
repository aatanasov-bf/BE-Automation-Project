Feature: UserActions

A short summary of the feature

@UserActions
Scenario: Login as an admin
	When I login with admin1 and admin123
	Then I get bearer token

Scenario: Login as parent
	When I login with test_nasko_parent and parent1234
	Then I get bearer token

Scenario: Login as teacher
	When I login with test_nasko_teacher and teacher1234
	Then I get bearer token

Scenario: Login as moderator
	When I login with test_nasko_moderator and moderator1234
	Then I get bearer token
