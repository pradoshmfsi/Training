CREATE PROCEDURE getStates @stateId int
AS
SELECT * FROM state_table WHERE stateId = @stateId;