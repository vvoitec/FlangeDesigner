Feature: Project management
    
@clearDatabase
Scenario: Load project from file
    Given Global path to existing SolidWorks project is D:\dev\FlangeDesigner\FlangeDesigner.Main\szablon.sldprt
    And SolidWorks project name is szablon
    When User loads the project
    Then New project is created