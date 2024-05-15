Feature: EmployeeAuthentication
    As an employee
    I want to be able to authenticate myself
    So that I can access secure features

Scenario: Successful employee authentication
    Given an employee with email "test@example.com" and password "password" exists in the system
    When the employee attempts to authenticate
    Then the system should return a token
    And the token should be valid