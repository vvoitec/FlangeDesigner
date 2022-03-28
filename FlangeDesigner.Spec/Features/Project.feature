Feature: Project management
    
@clearDatabase
Scenario: User loads project from file
    Given Global path to existing SolidWorks project is D:\dev\FlangeDesigner\FlangeDesigner.Main\szablon.sldprt
    And SolidWorks project name is szablon
    When User loads the project
    Then New project is created
    
#@clearDatabase
Scenario: Project configurations are created
    Given Global path to existing SolidWorks project is D:\dev\FlangeDesigner\FlangeDesigner.Main\szablon.sldprt
    And SolidWorks project name is szablon
    When User loads the project
    Then Project configurations are created
    
Scenario: User can add project configuration
    Given Project named szablon contains following configuration
      | Key                      | Value |
      | D1@Szkic1                | 10    |
      | D2@Szkic1                | 10    |
      | D1@Dodanie-wyciągnięcie1 | 10    |
    When User adds a following configuration to project named szablon
      | Key                      | Value |
      | D1@Szkic1                | 15    |
      | D2@Szkic1                | 15    |
      | D1@Dodanie-wyciągnięcie1 | 15    |
    Then Project named szablon contains following configuration
      | Key                      | Value |
      | D1@Szkic1                | 15    |
      | D2@Szkic1                | 15    |
      | D1@Dodanie-wyciągnięcie1 | 15    |
    