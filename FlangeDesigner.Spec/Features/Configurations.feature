Feature: Configurations
    
    Background: 
        Given Project named szablon is loaded from global path D:\dev\FlangeDesigner\FlangeDesigner.Main\szablon.sldprt
    
        @clearDatabase
        Scenario: User can add project configuration
            When User adds configuration named test to project named szablon
              | Key                      | Value |
              | D1@Szkic1                | 15    |
              | D2@Szkic1                | 15    |
              | D1@Dodanie-wyciągnięcie1 | 15    |
            Then Project named szablon contains configuration named test with following dimensions
              | Key                      | Value |
              | D1@Szkic1                | 15    |
              | D2@Szkic1                | 15    |
              | D1@Dodanie-wyciągnięcie1 | 15    |