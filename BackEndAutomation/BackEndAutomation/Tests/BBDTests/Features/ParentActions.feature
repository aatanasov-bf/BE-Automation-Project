Feature: ParentActions

A short summary of the feature

@ParentActions
Scenario: Parent views his child's grades
	Given I login with test_nasko_parent_grades and parent1234
	When I enter child's details "99ca22e2-0fdc-4d2a-aec0-46177543f2dc"
	And I open the grades
	Then I get message "" and the grades are shown.

Scenario: Parent doesn'r views other children's grades
	Given I login with test_nasko_parent and parent1234
	When I enter child's details "99ca22e2-0fdc-4d2a-aec0-46177543f2dc"
	And I open the grades
	Then I get message "You can't view this student's grades" and the grades are not shown.
