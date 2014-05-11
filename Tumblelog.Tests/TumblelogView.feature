Feature: TumblelogView
	In order to launch my blog
    I have to make my tumblelog site viewable

@homepage
Scenario: Browse to home page
	Given I have to view the home page
	When I browse to the home page
	Then the page should have the title of 'My Tumblelog'
    And the heading on the page should be 'My Tumblelog'
