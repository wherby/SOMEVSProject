IF NOT EXISTS (SELECT * FROM sys.change_tracking_tables WHERE object_id = OBJECT_ID(N'[dbo].[Class]')) 
   ALTER TABLE [dbo].[Class] 
   ENABLE  CHANGE_TRACKING
GO
