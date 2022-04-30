CREATE UNIQUE INDEX idx_unique_project_dimensions 
    ON main.configurations(ProjectId, Dimensions);