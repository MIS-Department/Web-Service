USE hr_bak
GO

CREATE PROCEDURE CalculatedTimeAll
AS
	SET NOCOUNT ON 

	SELECT * FROM CalculatedTime
GO

CREATE PROCEDURE CalculatedTimeSelectById(
	@CalculatedTimeId int
)
AS
	SELECT * FROM CalculatedTime WHERE CalculatedTimeId = @CalculatedTimeId
GO

CREATE PROCEDURE CalculatedTimeInsert(
	@TimeTypeId int,
	@Value time,
	@DailyTimeRecordId int
)
AS
	INSERT INTO dbo.CalculatedTime
	(
	    --CalculatedTimeId - this column value is auto-generated
	    TimeTypeId,
	    [Value],
	    DailyTimeRecordId
	)
	VALUES
	(
	    -- CalculatedTimeId - int
	    @TimeTypeId, -- TimeTypeId - int
	    @Value, -- Value - time
	    @DailyTimeRecordId-- DailyTimeRecordId - int
	)
GO 

CREATE PROCEDURE CalculatedTimeSelectByDailyTimeRecordId(
	@DailyTimeRecordId int
)
AS
	SET NOCOUNT ON

	SELECT * FROM CalculatedTime WHERE DailyTimeRecordId = @DailyTimeRecordId
GO 
